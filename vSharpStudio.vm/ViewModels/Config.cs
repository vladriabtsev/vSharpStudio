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
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Config : ITreeModel, IMigration, ICanGoLeft
    {
        // to use xxxIsChanging(x from, x to)
        public static bool IsLoading;
        public Dictionary<string, ITreeConfigNode> DicNodes { get; set; }
        public override IEnumerable<object> GetChildren(object parent) { return this.Children; }
        //public bool HasChildren(object parent) { return this.Children.Count > 0; }
        public override bool HasChildren(object parent) { return this.Children.Count > 0; }
        //public static readonly string DefaultName = "Config";
        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }

        protected IMigration _migration = null;
        public string ConnectionString = null;
        partial void OnInitBegin()
        {
            this.DicNodes = new Dictionary<string, ITreeConfigNode>(1000);
        }
        partial void OnInit()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
                this.Name = "Config";
            this.PrimaryKeyType = EnumPrimaryKeyType.INT;
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
#if DEBUG
            //SubNodes.Add(this.GroupConstants, 1);
#endif
            this.GroupConfigLinks.Parent = this;
            this.Children.Add(this.GroupConfigLinks, 0);

            this.Model.Parent = this;
            this.Children.Add(this.Model, 1);

            //this.Children.Add(this, 5);


            if (string.IsNullOrWhiteSpace(this.DbSettings.DbSchema))
                this.DbSettings.DbSchema = "v";
            //this.ListConnectionStringVMs = new List<ConnStringVM>();
            //this.ListDbProviders = new List<string>();

            this.GroupPlugins.Parent = this;
            this.Children.Add(this.GroupPlugins, 9);
            this.GroupAppSolutions.Parent = this;
            this.Children.Add(this.GroupAppSolutions, 10);
        }
        protected override void OnInitFromDto()
        {
            RecreateSubNodes();
        }
        public Config() : this((ITreeConfigNode)null)
        {
        }
        public Config(string configJson) : this((ITreeConfigNode)null)
        {
            OnInitBegin();
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

        #region Connection string editor

        //public Action<string> OnProviderSelectionChanged = null;
        //public List<ConnStringVM> ListConnectionStringVMs { get; set; }
        //public List<string> ListDbProviders { get; set; }
        //public string SelectedDbProvider
        //{
        //    get { return _SelectedDbProvider; }
        //    set
        //    {
        //        _SelectedDbProvider = value;
        //        OnProviderSelectionChanged(value);
        //    }
        //}
        //private string _SelectedDbProvider;

        #endregion Connection string editor
        public Dictionary<vPluginLayerTypeEnum, List<PluginRow>> DicPlugins { get; set; }

        public IConfig PrevStableConfig { get; set; }
        public IConfig OldStableConfig { get; set; }
        public List<IConfig> ListAnnotated
        {
            get
            {
                var oldests = GetListConfigs(this.OldStableConfig);
                var prevs = GetListConfigs(this.PrevStableConfig);
                var currents = GetListConfigs(this);
                var diff = new DiffLists<IConfig>(oldests, prevs, currents);
                return diff.ListAll;
            }
        }
        private static List<IConfig> GetListConfigs(IConfig cfg)
        {
            var lst = new List<IConfig>();
            if (cfg == null)
                return lst;
            var dic = new Dictionary<string, IConfig>();
            dic[cfg.Guid] = cfg;
            GetSubConfigs(cfg);
            foreach (var t in dic)
            {
                lst.Add(t.Value);
            }
            dic.Clear();
            return lst;
            void GetSubConfigs(IConfig _cfg)
            {
                foreach (var t in _cfg.IGroupConfigLinks.IListBaseConfigLinks)
                {
                    dic[t.IConfig.Guid] = t.IConfig;
                    GetSubConfigs(t.IConfig);
                }
            }
        }
    }
}
