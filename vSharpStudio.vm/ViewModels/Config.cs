using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Google.Protobuf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.vm.Migration;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Config : IMigration, ICanGoLeft
    {
        //public static readonly string DefaultName = "Config";
        public SortedObservableCollection<ITreeConfigNode> Children { get; private set; }

        protected IMigration _migration = null;
        public string ConnectionString = null;
        partial void OnInit()
        {
            this.Name = "Config";
            this.PrimaryKeyType = EnumPrimaryKeyType.INT;
            this.Children = new SortedObservableCollection<ITreeConfigNode>();
#if DEBUG
            //SubNodes.Add(this.GroupConstants, 1);
#endif
            this.GroupPlugins.Parent = this;
            this.Children.Add(this.GroupPlugins, 0);
            this.GroupConfigs.Parent = this;
            this.Children.Add(this.GroupConfigs, 4);

            this.Children.Add(this, 5);
            this.GroupCommon.Parent = this;
            this.Children.Add(this.GroupCommon, 6);
            this.GroupConstants.Parent = this;
            this.Children.Add(this.GroupConstants, 7);
            this.GroupEnumerations.Parent = this;
            this.Children.Add(this.GroupEnumerations, 8);
            this.GroupCatalogs.Parent = this;
            this.Children.Add(this.GroupCatalogs, 9);
            this.GroupDocuments.Parent = this;
            this.Children.Add(this.GroupDocuments, 10);
            this.GroupJournals.Parent = this;
            this.Children.Add(this.GroupJournals, 11);
            if (string.IsNullOrWhiteSpace(this.DbSettings.DbSchema))
                this.DbSettings.DbSchema = "v";
        }
        protected override void OnInitFromDto()
        {
            RecreateSubNodes();
        }
        public Config(string configJson)
            : this()
        {
            var pconfig = Proto.Config.proto_config.Parser.ParseJson(configJson);
            Config.ConvertToVM(pconfig, this);
        }
        public string ExportToJson()
        {
            var pconfig = Config.ConvertToProto(this);
            var res = JsonFormatter.Default.Format(pconfig);
            return res;
        }

        #region Validation

        private CancellationTokenSource cancellationSourceForValidatingFullConfig = null;
        public async Task ValidateSubTreeFromNodeAsync(ITreeConfigNode node)
        {
            // https://msdn.microsoft.com/en-us/magazine/jj991977.aspx
            // https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap
            // https://devblogs.microsoft.com/pfxteam/asynclazyt/
            // https://github.com/StephenCleary/AsyncEx
            // https://msdn.microsoft.com/en-us/magazine/dn818493.aspx
            await Task.Run(() =>
            {
                ValidateSubTreeFromNode(node);
            }).ConfigureAwait(false); // not keeping context because doing nothing after await
        }
        public void ValidateSubTreeFromNode(ITreeConfigNode node, ILogger logger = null)
        {
            if (node == null)
                return;
            if (cancellationSourceForValidatingFullConfig != null)
            {
                cancellationSourceForValidatingFullConfig.Cancel();
                //                if (logger != null && logger.IsEnabled)
                if (logger != null)
                    logger.LogInformation("=== Cancellation request ===");
            }
            this.cancellationSourceForValidatingFullConfig = new CancellationTokenSource();
            var token = cancellationSourceForValidatingFullConfig.Token;

            var visitor = new ValidationConfigVisitor(token, logger);
            visitor.UpdateSubstructCounts(node);
            (node as IConfigAcceptVisitor).AcceptConfigNodeVisitor(visitor);
            if (!token.IsCancellationRequested)
            {
                // update for UI from another Thread (if from async version) (it is not only update, many others including CountErrors, CountWarnings ...
                node.ValidationCollection.Clear();
                node.ValidationCollection = visitor.Result;
            }
            else
            {
                logger.LogInformation("=== Cancelled ===");
            }
        }

        #endregion Validation

        #region IMigration

        bool IMigration.IsDatabaseServiceOn()
        {
            return _migration.IsDatabaseServiceOn();
        }
        Task<bool> IMigration.IsDatabaseServiceOnAsync(CancellationToken cancellationToken)
        {
            return _migration.IsDatabaseServiceOnAsync(cancellationToken);
        }
        bool IMigration.IsDatabaseExists()
        {
            return _migration.IsDatabaseExists();
        }
        Task<bool> IMigration.IsDatabaseExistsAsync(CancellationToken cancellationToken)
        {
            return _migration.IsDatabaseExistsAsync(cancellationToken);
        }
        void IMigration.CreateDatabase()
        {
            _migration.CreateDatabase();
        }
        Task IMigration.CreateDatabaseAsync(CancellationToken cancellationToken)
        {
            return _migration.CreateDatabaseAsync(cancellationToken);
        }
        void IMigration.DropDatabase()
        {
            _migration.DropDatabase();
        }
        Task IMigration.DropDatabaseAsync(CancellationToken cancellationToken)
        {
            return _migration.DropDatabaseAsync(cancellationToken);
        }

        #endregion IMigration

        #region ITreeNode

        void RecreateSubNodes()
        {
        }
        partial void OnGroupConstantsChanged() { RecreateSubNodes(); }
        partial void OnGroupCatalogsChanged() { RecreateSubNodes(); }
        partial void OnGroupEnumerationsChanged() { RecreateSubNodes(); }

        #endregion ITreeNode

        [Editor(typeof(FolderPickerEditor), typeof(ITypeEditor))]
        public string SolutionPath
        {
            set
            {
                _SolutionPath = value;
                NotifyPropertyChanged();
                //ValidateProperty();
            }
            get { return _SolutionPath; }
        }
        private string _SolutionPath;
        [BrowsableAttribute(false)]
        public ITreeConfigNode SelectedNode
        {
            set
            {
                if (_SelectedNode != value)
                {
                    _SelectedNode = value;
                    NotifyPropertyChanged();
                }
                if (OnSelectedNodeChanged != null)
                    OnSelectedNodeChanged();
            }
            get { return _SelectedNode; }
        }
        private ITreeConfigNode _SelectedNode;
        public Action OnSelectedNodeChanged;
    }
}
