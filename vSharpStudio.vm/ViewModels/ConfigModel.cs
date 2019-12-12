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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.vm.Migration;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ConfigModel : ITreeModel, IMigration, ICanGoLeft
    {
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.Children;
        }

        public override bool HasChildren(object parent)
        {
            return this.Children.Count > 0;
        }

        // public static readonly string DefaultName = "Config";
        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }

        protected IMigration _migration = null;
        public string ConnectionString = null;

        partial void OnInit()
        {
            this.Name = "ConfigModel";
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
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
        }

        protected override void OnInitFromDto()
        {
            this.Name = "ConfigModel";
            this.RecreateSubNodes();
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
                this.ValidateSubTreeFromNode(node);
            }).ConfigureAwait(false); // not keeping context because doing nothing after await
        }

        public void ValidateSubTreeFromNode(ITreeConfigNode node, ILogger logger = null)
        {
            if (node == null)
            {
                return;
            }

            if (this.cancellationSourceForValidatingFullConfig != null)
            {
                this.cancellationSourceForValidatingFullConfig.Cancel();
                // if (logger != null && logger.IsEnabled)
                if (logger != null)
                {
                    logger.LogInformation("=== Cancellation request ===");
                }
            }
            this.cancellationSourceForValidatingFullConfig = new CancellationTokenSource();
            var token = this.cancellationSourceForValidatingFullConfig.Token;

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
            return this._migration.IsDatabaseServiceOn();
        }

        Task<bool> IMigration.IsDatabaseServiceOnAsync(CancellationToken cancellationToken)
        {
            return this._migration.IsDatabaseServiceOnAsync(cancellationToken);
        }

        bool IMigration.IsDatabaseExists()
        {
            return this._migration.IsDatabaseExists();
        }

        Task<bool> IMigration.IsDatabaseExistsAsync(CancellationToken cancellationToken)
        {
            return this._migration.IsDatabaseExistsAsync(cancellationToken);
        }

        void IMigration.CreateDatabase()
        {
            this._migration.CreateDatabase();
        }

        Task IMigration.CreateDatabaseAsync(CancellationToken cancellationToken)
        {
            return this._migration.CreateDatabaseAsync(cancellationToken);
        }

        void IMigration.DropDatabase()
        {
            this._migration.DropDatabase();
        }

        Task IMigration.DropDatabaseAsync(CancellationToken cancellationToken)
        {
            return this._migration.DropDatabaseAsync(cancellationToken);
        }

        #endregion IMigration

        #region ITreeNode

        void RecreateSubNodes()
        {
        }

        partial void OnGroupConstantsChanged()
        {
            this.RecreateSubNodes();
        }

        partial void OnGroupCatalogsChanged()
        {
            this.RecreateSubNodes();
        }

        partial void OnGroupEnumerationsChanged()
        {
            this.RecreateSubNodes();
        }

        #endregion ITreeNode

        #region Objects
        public IEnumerable<ITreeConfigNode> GetAllNodes()
        {
            yield return this.GroupEnumerations;
            foreach (var t in this.GroupEnumerations.ListEnumerations)
            {
                yield return t;
            }

            yield return this.GroupConstants;
            foreach (var t in this.GroupConstants.ListConstants)
            {
                yield return t;
            }

            yield return this.GroupCatalogs;
            foreach (var t in this.GroupCatalogs.ListCatalogs)
            {
                yield return t;
                yield return t.GroupProperties;
                foreach (var tt in t.GroupProperties.ListProperties)
                {
                    yield return tt;
                }

                yield return t.GroupPropertiesTabs;
                foreach (var tt in t.GroupPropertiesTabs.ListPropertiesTabs)
                {
                    yield return tt;
                    yield return tt.GroupProperties;
                    foreach (var ttt in tt.GroupProperties.ListProperties)
                    {
                        yield return ttt;
                    }
                }
                yield return t.GroupForms;
                foreach (var tt in t.GroupForms.ListForms)
                {
                    yield return tt;
                }

                yield return t.GroupReports;
                foreach (var tt in t.GroupReports.ListReports)
                {
                    yield return tt;
                }
            }
            yield return this.GroupDocuments;
            yield return this.GroupDocuments.GroupSharedProperties;
            foreach (var t in this.GroupDocuments.GroupSharedProperties.ListProperties)
            {
                yield return t;
            }

            yield return this.GroupDocuments.GroupListDocuments;
            foreach (var t in this.GroupDocuments.GroupListDocuments.ListDocuments)
            {
                yield return t;
                yield return t.GroupProperties;
                foreach (var tt in t.GroupProperties.ListProperties)
                {
                    yield return tt;
                }

                yield return t.GroupPropertiesTabs;
                foreach (var tt in this.GetTabNodes(t.GroupPropertiesTabs))
                {
                    yield return tt;
                }

                yield return t.GroupForms;
                foreach (var tt in t.GroupForms.ListForms)
                {
                    yield return tt;
                }

                yield return t.GroupReports;
                foreach (var tt in t.GroupReports.ListReports)
                {
                    yield return tt;
                }
            }
            yield return this.GroupJournals;
            foreach (var t in this.GroupJournals.ListJournals)
            {
                yield return t;
            }
        }

        private IEnumerable<ITreeConfigNode> GetTabNodes(GroupListPropertiesTabs tab)
        {
            foreach (var tt in tab.ListPropertiesTabs)
            {
                yield return tt;
                yield return tt.GroupProperties;
                foreach (var ttt in tt.GroupProperties.ListProperties)
                {
                    yield return ttt;
                }

                yield return tt.GroupPropertiesTabs;
                foreach (var ttt in this.GetTabNodes(tt.GroupPropertiesTabs))
                {
                    yield return tt;
                }
            }
        }
        #endregion Objects

        [Editor(typeof(FolderPickerEditor), typeof(ITypeEditor))]
        public string SolutionPath
        {
            get
            {
                return this._SolutionPath;
            }

            set
            {
                this._SolutionPath = value;
                this.NotifyPropertyChanged();
            }
        }

        private string _SolutionPath;

        [BrowsableAttribute(false)]
        public ITreeConfigNode SelectedNode
        {
            get
            {
                return this._SelectedNode;
            }

            set
            {
                if (this._SelectedNode != value)
                {
                    this._SelectedNode = value;
                    this.NotifyPropertyChanged();
                }
                if (this.OnSelectedNodeChanged != null)
                {
                    this.OnSelectedNodeChanged();
                }
            }
        }

        private ITreeConfigNode _SelectedNode;
        public Action OnSelectedNodeChanged;

        #region Connection string editor

        // public Action<string> OnProviderSelectionChanged = null;
        // public List<ConnStringVM> ListConnectionStringVMs { get; set; }
        // public List<string> ListDbProviders { get; set; }
        // public string SelectedDbProvider
        // {
        //    get { return _SelectedDbProvider; }
        //    set
        //    {
        //        _SelectedDbProvider = value;
        //        OnProviderSelectionChanged(value);
        //    }
        // }
        // private string _SelectedDbProvider;

        #endregion Connection string editor
        public Dictionary<vPluginLayerTypeEnum, List<PluginRow>> DicPlugins { get; set; }
    }
}
