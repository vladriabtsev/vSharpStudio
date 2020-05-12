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
    public partial class Config : ITreeModel, IMigration, ICanGoLeft
    {
        // to use xxxIsChanging(x from, x to)
        public static bool IsLoading;

        public DictionaryExt<string, ITreeConfigNode> DicNodes { get; set; }
        public DictionaryExt<string, IvPluginGenerator> DicAppGenerators = new DictionaryExt<string, IvPluginGenerator>(100, false, true,
            (ki, v) =>
            {
            }, (kr, v) =>
            {
            }, () =>
            {
            });

        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.Children;
        }

        // public bool HasChildren(object parent) { return this.Children.Count > 0; }
        public override bool HasChildren(object parent)
        {
            return this.Children.Count > 0;
        }

        // public static readonly string DefaultName = "Config";
        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }
        //public ConfigNodesCollection<ITreeConfigNode> Children
        //{
        //    get { return this._Children; }
        //    set
        //    {
        //        this._Children = value;
        //        this.NotifyPropertyChanged();
        //    }
        //}
        //private ConfigNodesCollection<ITreeConfigNode> _Children;

        protected IMigration _migration = null;
        public string ConnectionString = null;

        partial void OnInitBegin()
        {
            _logger.Trace();
            this.DicNodes = new DictionaryExt<string, ITreeConfigNode>(1000, true);
        }

        partial void OnInit()
        {
            _logger.Trace();
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                this.Name = "Config";
            }

            this.DbSettings.PKeyType = EnumPrimaryKeyType.INT;
            this.DbSettings.KeyName = "Id";
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
#if DEBUG
            // SubNodes.Add(this.GroupConstants, 1);
#endif
            this.GroupConfigLinks.Parent = this;
            this.Model.Parent = this;
            if (string.IsNullOrWhiteSpace(this.DbSettings.DbSchema))
            {
                this.DbSettings.DbSchema = "v";
            }
            this.GroupPlugins.Parent = this;
            this.GroupAppSolutions.Parent = this;
            this.RefillChildren();
        }

        protected override void OnInitFromDto()
        {
            _logger.Trace();
            base.OnInitFromDto();
            this.RefillChildren();
        }
        void RefillChildren()
        {
            this.Children.Clear();
            this.Children.Add(this.GroupConfigLinks, 0);
            this.Children.Add(this.Model, 1);
            this.Children.Add(this.GroupPlugins, 9);
            this.Children.Add(this.GroupAppSolutions, 10);
        }

        public Config()
            : this((ITreeConfigNode)null)
        {
        }

        public Config(string configJson)
            : this((ITreeConfigNode)null)
        {
            this.OnInitBegin();
            var pconfig = Proto.Config.proto_config.Parser.ParseJson(configJson);
            Config.ConvertToVM(pconfig, this);
        }

        public string ExportToJson()
        {
            _logger.Trace();
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
                this.ValidateSubTreeFromNode(node);
            }).ConfigureAwait(false); // not keeping context because doing nothing after await
        }

        public void ValidateSubTreeFromNode(ITreeConfigNode node, ILogger logger = null)
        {
            _logger.Trace();
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


        #endregion ITreeNode

        [BrowsableAttribute(false)]
        public string CurrentCfgFolderPath { get; set; }
        //[Editor(typeof(EditorFolderPicker), typeof(ITypeEditor))]
        //public string SolutionPath
        //{
        //    get
        //    {
        //        return this._SolutionPath;
        //    }
        //    set
        //    {
        //        this._SolutionPath = value;
        //        this.NotifyPropertyChanged();
        //    }
        //}
        //private string _SolutionPath;

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
                    if (this.OnSelectedNodeChanging != null)
                    {
                        this.OnSelectedNodeChanging(this._SelectedNode, value);
                    }
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
        public Action<ITreeConfigNode, ITreeConfigNode> OnSelectedNodeChanging;

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
        public Dictionary<string, IvPluginGenerator> DicGenerators { get; set; }

        public IConfig PrevCurrentConfig { get; set; }
        public IConfig PrevStableConfig { get; set; }
        public IConfig OldStableConfig { get; set; }

        //public List<IConfig> SetAnnotations(IConfig prev, IConfig old)
        //{
        //    var oldests = GetListConfigs(old);
        //    var prevs = GetListConfigs(prev);
        //    var currents = GetListConfigs(this);
        //    var diff = new DiffLists<IConfig>(oldests, prevs, currents);
        //    return diff.ListAll;
        //}

        public List<IConfig> GetListConfigs()
        {
            var lst = new List<IConfig>();
            var dic = new Dictionary<string, IConfig>();
            dic[this.Guid] = this;
            GetSubConfigs(this);
            foreach (var t in dic)
            {
                lst.Add(t.Value);
            }
            dic.Clear();
            return lst;
            void GetSubConfigs(IConfig cfg)
            {
                foreach (var t in cfg.GroupConfigLinks.ListBaseConfigLinks)
                {
                    dic[t.Config.Guid] = t.Config;
                    GetSubConfigs(t.Config);
                }
            }
        }
        public void RefillDicGenerators()
        {
            //_logger.LogTrace("".StackInfo());
            _logger.Trace();
            this.DicAppGenerators.Clear();
            _logger.LogTrace("{DicAppGenerators}", this.DicAppGenerators);
            foreach (var t in this.GroupAppSolutions.ListAppSolutions)
            {
                foreach (var tt in t.ListAppProjects)
                {
                    foreach (var ttt in tt.ListAppProjectGenerators)
                    {
#if DEBUG
                        if (string.IsNullOrWhiteSpace(ttt.Guid))
                            throw new Exception("PluginGenerator Guid is empty");
#endif
                        if (!this.DicAppGenerators.ContainsKey(ttt.Guid))
                        {
                            AppProjectGenerator g = (AppProjectGenerator)this.DicNodes[ttt.Guid];
                            this.DicAppGenerators[ttt.Guid] = this.DicGenerators[g.PluginGeneratorGuid];
                        }
                    }
                }
            }
            _logger.LogTrace("{DicAppGenerators}", this.DicAppGenerators);
        }
    }
}
