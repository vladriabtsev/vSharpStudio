using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Google.Protobuf;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.vm.Migration;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Config : ITreeModel, IMigration, ICanGoLeft, IEditableNodeGroup
    {
        // to use xxxIsChanging(x from, x to)
        public bool IsInitialized = false;

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            return new List<ITreeConfigNode>();
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public DictionaryExt<string, ITreeConfigNode> _DicNodes;
        public IReadOnlyDictionary<string, ITreeConfigNode> DicNodes { get { return _DicNodes; } }
        // Only active Plugin generators (generator selected in AppProjectGenerator) Guid  AppProjectGenerator node
        public DictionaryExt<string, IvPluginGenerator> _DicActiveAppProjectGenerators = new DictionaryExt<string, IvPluginGenerator>(100, false, true,
                        (ki, v) => { }, (kr, v) => { }, () => { });
        public IReadOnlyDictionary<string, IvPluginGenerator> DicActiveAppProjectGenerators { get { return _DicActiveAppProjectGenerators; } }
        // public static readonly string DefaultName = "Config";
        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }
        protected IMigration _migration { get; set; }
        public string ConnectionString { get; set; }
        public string DebugTag;
        partial void OnCreating()
        {
            _logger.Trace();
            this._DicNodes = new DictionaryExt<string, ITreeConfigNode>(1000, true, true,
                (ki, v) => { }, (kr, v) => { }, () => { });
        }
        [Browsable(false)]
        new public string IconName { get { return "icon3DScene"; } }
        //protected override string GetNodeIconName() { return "icon3DScene"; }
        partial void OnCreated()
        {
            _logger.Trace();
            if (string.IsNullOrWhiteSpace(this._Name))
            {
                this._Name = "Config";
            }

            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
#if DEBUG
            // SubNodes.Add(this.GroupConstants, 1);
#endif
            this.GroupConfigLinks.Parent = this;
            this.Model.Parent = this;
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
            VmBindable.IsNotifyingStatic = false;
            this.Children.Clear();
            this.Children.Add(this.GroupConfigLinks, 0);
            this.Children.Add(this.Model, 1);
            this.Children.Add(this.GroupPlugins, 9);
            this.Children.Add(this.GroupAppSolutions, 10);
            VmBindable.IsNotifyingStatic = true;
        }
        public Config(ConfigShortHistory history)
            : this((ITreeConfigNode)null)
        {
            this.OnCreating();
        }
        public static Config Clone(ConfigShortHistory parent, IConfig from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            var vm = Config.Clone((ITreeConfigNode)null, from, isDeep, isNewGuid);
            return vm;
        }
        public Config(Proto.Config.proto_config pconfig)
            : this((ITreeConfigNode)null)
        {
            this.OnCreating();
            Config.ConvertToVM(pconfig, this);
        }
        public Config(string configJson)
            : this((ITreeConfigNode)null)
        {
            this.OnCreating();
            var pconfig = Proto.Config.proto_config.Parser.WithDiscardUnknownFields(true).ParseJson(configJson);
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
        public void ValidateSubTreeFromNode(ILogger logger = null)
        {
            this.ValidateSubTreeFromNode(this, logger);
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
            Debug.Assert(node.ValidationCollection.Count == node.CountErrors + node.CountInfos + node.CountWarnings);
        }

        #endregion Validation

        #region IMigration

        public bool IsDatabaseServiceOn()
        {
            return this._migration.IsDatabaseServiceOn();
        }

        public Task<bool> IsDatabaseServiceOnAsync(CancellationToken cancellationToken)
        {
            return this._migration.IsDatabaseServiceOnAsync(cancellationToken);
        }

        public bool IsDatabaseExists()
        {
            return this._migration.IsDatabaseExists();
        }

        public Task<bool> IsDatabaseExistsAsync(CancellationToken cancellationToken)
        {
            return this._migration.IsDatabaseExistsAsync(cancellationToken);
        }

        public void CreateDatabase()
        {
            this._migration.CreateDatabase();
        }

        public Task CreateDatabaseAsync(CancellationToken cancellationToken)
        {
            return this._migration.CreateDatabaseAsync(cancellationToken);
        }

        public void DropDatabase()
        {
            this._migration.DropDatabase();
        }

        public Task DropDatabaseAsync(CancellationToken cancellationToken)
        {
            return this._migration.DropDatabaseAsync(cancellationToken);
        }

        #endregion IMigration

        #region ITreeNode


        #endregion ITreeNode

        [BrowsableAttribute(false)]
        public string CurrentCfgFolderPath { get; set; }
        [BrowsableAttribute(false)]
        public ITreeConfigNode SelectedNode
        {
            get { return this._SelectedNode; }
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
        public Action OnSelectedNodeChanged { get; set; }
        public Action<ITreeConfigNode, ITreeConfigNode> OnSelectedNodeChanging { get; set; }

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
        public Dictionary<vPluginLayerTypeEnum, List<PluginRow>> DicPluginLists { get; set; }
        public Dictionary<string, IvPlugin> DicPlugins { get; set; }
        // by GroupGuid of generator
        public Dictionary<string, IvPluginGenerator> DicGroupSettingGenerators { get; set; }
        // by Guid of generator
        public Dictionary<string, IvPluginGenerator> DicGenerators { get; set; }

        public IConfig PrevCurrentConfig { get; set; }
        public IConfig PrevStableConfig { get; set; }
        public IReadOnlyList<IConfig> GetListConfigs()
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
                    dic[t.ConfigBase.Guid] = t.ConfigBase;
                    GetSubConfigs(t.ConfigBase);
                }
            }
        }
        public void RefillDicGenerators()
        {
            //_logger.LogTrace("".StackInfo());
            _logger.Trace();
            this._DicActiveAppProjectGenerators.Clear();
            _logger.LogTrace("{DicAppGenerators}", this.DicActiveAppProjectGenerators);
            foreach (var t in this.GroupAppSolutions.ListAppSolutions)
            {
                foreach (var tt in t.ListAppProjects)
                {
                    foreach (var ttt in tt.ListAppProjectGenerators)
                    {
                        if (!string.IsNullOrWhiteSpace(ttt.PluginGeneratorGuid))
                        {
                            foreach (var tp in this.GroupPlugins.ListPlugins)
                            {
                                foreach (var ttp in tp.ListGenerators)
                                {
                                    if (ttp.Generator?.Guid == ttt.PluginGeneratorGuid)
                                    {
                                        this._DicActiveAppProjectGenerators[ttt.Guid] = ttp.Generator;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            _logger.LogTrace("{DicAppGenerators}", this.DicActiveAppProjectGenerators);
        }
        public void PluginSettingsToModel()
        {
            foreach (var t in this.GroupAppSolutions.ListAppSolutions)
            {
                t.SaveGroupSettings();
                foreach (var tt in t.ListAppProjects)
                {
                    tt.SaveGroupSettings();
                    foreach (var ttt in tt.ListAppProjectGenerators)
                    {
                        ttt.SaveSettings();
                    }
                }
            }
            // Save Node Settings VM for all nodes, which are supporting INodeGenSettings
            var nv = new ModelVisitorNodeGenSettings();
            nv.NodeGenSettingsApplyAction(this, (p) =>
            {
                p.SaveNodeAppGenSettings();
            });
        }
        public string IsHasMarkedPath
        {
            get
            {
                var sb = new StringBuilder();
                if (this.IsHasChanged)
                {
                    sb.AppendLine("Config");
                    IsHasMarkedPath2(sb, this.GetListChildren());
                }
                return sb.ToString();
            }
        }
        private void IsHasMarkedPath2(StringBuilder sb, IEnumerable<ITreeConfigNode> children)
        {
            foreach (var t in children)
            {
                var p = t as IEditableNodeGroup;
                if (p != null)
                {
                    if (p.IsHasChanged)
                    {
                        sb.Append(t.GetType().Name);
                        sb.Append(" IsHasChanged=");
                        sb.Append(p.IsHasChanged);
                        if (t is IEditableNode)
                        {
                            sb.Append(" IsChanged=");
                            sb.Append((t as IEditableNode).IsChanged);
                        }
                        sb.AppendLine();
                        IsHasMarkedPath2(sb, t.GetListChildren());
                        break;
                    }
                }
            }
        }
        public string IsHasChangedPath
        {
            get
            {
                var sb = new StringBuilder();
                if (this.IsHasChanged)
                {
                    sb.AppendLine("Config");
                    IsHasChangedPath2(sb, this.GetListChildren());
                }
                return sb.ToString();
            }
        }
        private void IsHasChangedPath2(StringBuilder sb, IEnumerable<ITreeConfigNode> children)
        {
            foreach (var t in children)
            {
                var p = t as IEditableNodeGroup;
                if (p != null)
                {
                    if (p.IsHasChanged)
                    {
                        sb.Append(t.GetType().Name);
                        sb.Append(" IsHasChanged=");
                        sb.Append(p.IsHasChanged);
                        if (t is IEditableNode)
                        {
                            sb.Append(" IsChanged=");
                            sb.Append((t as IEditableNode).IsChanged);
                        }
                        sb.AppendLine();
                        IsHasChangedPath2(sb, t.GetListChildren());
                        break;
                    }
                }
            }
        }
        public void SetIsNeedCurrentUpdate(bool val)
        {
            if (this._IsNeedCurrentUpdate != val)
                this._IsNeedCurrentUpdate = val;
        }
#if DEBUG
        public void DicDiffDebug(Config anotherCfg)
        {
            var diffActiveAppProjectGenerators = DicDiffResult<string, IvPluginGenerator>.DicDiff(this._DicActiveAppProjectGenerators, anotherCfg._DicActiveAppProjectGenerators);
            Debug.Assert(0 == diffActiveAppProjectGenerators.Dic1ButNotInDic2.Count);
            Debug.Assert(0 == diffActiveAppProjectGenerators.Dic2ButNotInDic1.Count);
            var diffGenerators = DicDiffResult<string, IvPluginGenerator>.DicDiff(this.DicGenerators, anotherCfg.DicGenerators);
            Debug.Assert(0 == diffGenerators.Dic1ButNotInDic2.Count);
            Debug.Assert(0 == diffGenerators.Dic2ButNotInDic1.Count);
            var diffNodes = DicDiffResult<string, ITreeConfigNode>.DicDiff(this._DicNodes, anotherCfg._DicNodes);
            Debug.Assert(0 == diffNodes.Dic1ButNotInDic2.Count);
            Debug.Assert(0 == diffNodes.Dic2ButNotInDic1.Count);
            var diffPlugins = DicDiffResult<string, IvPlugin>.DicDiff(this.DicPlugins, anotherCfg.DicPlugins);
            Debug.Assert(0 == diffPlugins.Dic1ButNotInDic2.Count);
            Debug.Assert(0 == diffPlugins.Dic2ButNotInDic1.Count);
            var diffPluginLists = DicDiffResult<vPluginLayerTypeEnum, List<PluginRow>>.DicDiff(this.DicPluginLists, anotherCfg.DicPluginLists);
            Debug.Assert(0 == diffPluginLists.Dic1ButNotInDic2.Count);
            Debug.Assert(0 == diffPluginLists.Dic2ButNotInDic1.Count);
        }
#endif
    }
#if DEBUG
    public class DicDiffResult<TKey, TValue>
    {
        public DicDiffResult()
        {
            this.Dic1ButNotInDic2 = new Dictionary<TKey, TValue>();
            this.Dic2ButNotInDic1 = new Dictionary<TKey, TValue>();
        }
        public Dictionary<TKey, TValue> Dic1ButNotInDic2 { get; private set; }
        public Dictionary<TKey, TValue> Dic2ButNotInDic1 { get; private set; }
        static public DicDiffResult<TK, TV> DicDiff<TK, TV>(IReadOnlyDictionary<TK, TV> dic1, IReadOnlyDictionary<TK, TV> dic2)
        {
            var res = new DicDiffResult<TK, TV>();
            foreach (var t in dic1)
            {
                if (!dic2.ContainsKey(t.Key))
                    res.Dic1ButNotInDic2[t.Key] = t.Value;
            }
            foreach (var t in dic2)
            {
                if (!dic1.ContainsKey(t.Key))
                    res.Dic2ButNotInDic1[t.Key] = t.Value;
            }
            return res;
        }
    }
#endif
}
