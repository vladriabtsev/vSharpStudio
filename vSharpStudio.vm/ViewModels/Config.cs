﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Config : ITreeModel, IMigration, ICanGoLeft, IEditableNodeGroup
    {
        public Config() : this((ITreeConfigNode?)null)
        {
        }
        public Config(ConfigShortHistory history) : this((ITreeConfigNode?)null)
        {
        }
        public Config(Proto.Config.proto_config pconfig) : this((ITreeConfigNode?)null)
        {
            Config.ConvertToVM(pconfig, this);
        }
        public Config(string configJson) : this((ITreeConfigNode?)null)
        {
            var pconfig = CommonUtils.ParseJson<Proto.Config.proto_config>(configJson, true);
            Config.ConvertToVM(pconfig, this);
        }
        public static Config Clone(ConfigShortHistory parent, IConfig from, bool isDeep = true, bool isNewGuid = false) // Clone.tt Line: 27
        {
            var vm = Config.Clone(default(ITreeConfigNode)!, from, isDeep, isNewGuid);
            return vm;
        }
        // to use xxxIsChanging(x from, x to)
        public bool IsInitialized = false;

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return new ConfigNodesCollection<ITreeConfigNodeSortable>(this);
        }
        public override bool HasChildren()
        {
            return true;
        }
        #endregion ITree

        public DictionaryExt<string, ITreeConfigNode> _DicNodes = new(1000, true, true,
                (ki, v) => { }, (kr, v) => { }, () => { });
        public IReadOnlyDictionary<string, ITreeConfigNode> DicNodes { get { return (IReadOnlyDictionary<string, ITreeConfigNode>)_DicNodes; } }
        // Only active Plugin generators (generator selected in AppProjectGenerator) Guid  AppProjectGenerator node
        public DictionaryExt<string, IvPluginGenerator> _DicActiveAppProjectGenerators = new(100, false, true,
                        (ki, v) => { }, (kr, v) => { }, () => { });
        public IReadOnlyDictionary<string, IvPluginGenerator> DicActiveAppProjectGenerators { get { return (IReadOnlyDictionary<string, IvPluginGenerator>)_DicActiveAppProjectGenerators; } }
        protected IMigration? _migration { get; set; }
        public string? ConnectionString { get; set; }
        public string? DebugTag;
        partial void OnCreating()
        {
            _logger?.Trace();
        }
        [Browsable(false)]
        public new string IconName { get { return "icon3DScene"; } }
        //protected override string GetNodeIconName() { return "icon3DScene"; }
        partial void OnCreated()
        {
            _logger?.Trace();
            if (string.IsNullOrWhiteSpace(this._Name))
            {
                this._Name = Defaults.ConfigName;
            }
            _logger?.Trace();
            Init();
        }
        protected override void OnInitFromDto()
        {
            _logger?.Trace();
            Init();
        }
        private void Init()
        {
            if (this.Children.Count > 0)
                return;
            VmBindable.IsNotifyingStatic = false;
            var children = (ConfigNodesCollection<ITreeConfigNodeSortable>)this.Children;
            children.Add(this.GroupConfigLinks, 0);
            children.Add(this.Model, 1);
            children.Add(this.GroupPlugins, 9);
            children.Add(this.GroupAppSolutions, 10);
            VmBindable.IsNotifyingStatic = true;
            //this.ListRoles.OnAddingAction = (t) =>
            //{
            //    t.IsNew = true;
            //};
            //this.ListRoles.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            //this.ListRoles.OnRemovedAction = (t) =>
            //{
            //    this.OnRemoveChild();
            //};
            //this.ListRoles.OnClearedAction = () =>
            //{
            //    this.OnRemoveChild();
            //};
        }
        public string ExportToJson()
        {
            _logger?.Trace();
            var pconfig = Config.ConvertToProto(this);
            var res = JsonFormatter.Default.Format(pconfig);
            return res;
        }

        #region Validation

        private CancellationTokenSource? cancellationSourceForValidatingFullConfig = null;

        //public async Task ValidateSubTreeFromNodeAsync(ITreeConfigNode node)
        //{
        //    // https://msdn.microsoft.com/en-us/magazine/jj991977.aspx
        //    // https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap
        //    // https://devblogs.microsoft.com/pfxteam/asynclazyt/
        //    // https://github.com/StephenCleary/AsyncEx
        //    // https://msdn.microsoft.com/en-us/magazine/dn818493.aspx
        //    //SortedObservableCollection<ValidationMessage> valCollection = null;
        //    await Task.Run(() =>
        //    {
        //        this.ValidateSubTreeFromNode(node);
        //    }).ConfigureAwait(false); // not keeping context because doing nothing after await
        //}
        //public void ValidateSubTreeFromNode(ILogger? logger = null)
        //{
        //    this.ValidateSubTreeFromNode(this, logger);
        //}
        public bool IsValidatingSubTreeFromNode = false;
        public async Task ValidateSubTreeFromNodeAsync(ITreeConfigNode node, ProgressVM? progressVM, CancellationToken cancellationToken, ILogger? logger = null)
        {
            try
            {
                _logger?.Trace();
                int i = 0;
                while (this.IsValidatingSubTreeFromNode)
                {
                    i++;
                    int delayTime = 300;
                    _logger?.Trace("Another validation in progress. Wait {delayTime} mc.");
                    await Task.Delay(delayTime);
#if DEBUG
                    if (i >= 10)
                        throw new Exception($"Waiting period to start validation exceeded {i * delayTime}mc. Need optimization.");
#else
                    if (i >= 100)
                        break;
#endif
                }
                this.IsValidatingSubTreeFromNode = true;

                // Count nodes
                if (progressVM != null)
                    progressVM.Title = "Counting nodes for validation";
                var visitor = new ValidationConfigVisitor(cancellationToken, progressVM, logger);
                (node as IConfigAcceptVisitor)!.AcceptConfigNodeVisitor(visitor);

                // prepare visitor for validation
                visitor.IsCountOnly = false;
                visitor.CountTotalValidatableNodes = visitor.CountCurrentValidatableNode;

                // validate and update progress
                if (progressVM != null)
                {
                    if (node is Config)
                        progressVM.Title = "Validating Configuration";
                    else
                        progressVM.Title = $"Validating {node.Name}";
                }
                visitor.CountCurrentValidatableNode = 0;
                UIDispatcher.Invoke(() =>
                {
                    visitor.UpdateSubstructCounts(node);
                });
                (node as IConfigAcceptVisitor)!.AcceptConfigNodeVisitor(visitor);
                if (!cancellationToken.IsCancellationRequested)
                {
                    // update for UI from another Thread (if from async version) (it is not only update, many others including CountErrors, CountWarnings ...
                    UIDispatcher.Invoke(() =>
                    {
                        node.ValidationCollection.Clear();
                        node.ValidationCollection = visitor.Result;
                    });
                    //Debug.Assert(node.ValidationCollection.Count == node.CountErrors + node.CountInfos + node.CountWarnings);
                }
                else
                {
                    logger?.LogInformation("=== Cancelled ===");
                }
            }
            catch (TaskCanceledException)
            {
                Trace.WriteLine("TaskCanceledException");
            }
#if DEBUG
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                throw;
            }
#endif
            finally
            {
                this.IsValidatingSubTreeFromNode = false;
            }
        }

        #endregion Validation

        #region IMigration

        public bool IsDatabaseServiceOn()
        {
            Debug.Assert(this._migration != null);
            return this._migration.IsDatabaseServiceOn();
        }

        public Task<bool> IsDatabaseServiceOnAsync(CancellationToken cancellationToken)
        {
            Debug.Assert(this._migration != null);
            return this._migration.IsDatabaseServiceOnAsync(cancellationToken);
        }

        public bool IsDatabaseExists()
        {
            Debug.Assert(this._migration != null);
            return this._migration.IsDatabaseExists();
        }

        public Task<bool> IsDatabaseExistsAsync(CancellationToken cancellationToken)
        {
            Debug.Assert(this._migration != null);
            return this._migration.IsDatabaseExistsAsync(cancellationToken);
        }

        public void CreateDatabase()
        {
            Debug.Assert(this._migration != null);
            this._migration.CreateDatabase();
        }

        public Task CreateDatabaseAsync(CancellationToken cancellationToken)
        {
            Debug.Assert(this._migration != null);
            return this._migration.CreateDatabaseAsync(cancellationToken);
        }

        public void DropDatabase()
        {
            Debug.Assert(this._migration != null);
            this._migration.DropDatabase();
        }

        public Task DropDatabaseAsync(CancellationToken cancellationToken)
        {
            Debug.Assert(this._migration != null);
            return this._migration.DropDatabaseAsync(cancellationToken);
        }

        #endregion IMigration

        #region ITreeNode


        #endregion ITreeNode

        [Browsable(false)]
        public string? CurrentCfgFolderPath { get; set; }
        [Browsable(false)]
        public ITreeConfigNode? SelectedNode
        {
            get { return this._SelectedNode; }
            set
            {
                if (this._SelectedNode != value)
                {
#if DEBUG
                    var stopWatch = new Stopwatch();
                    stopWatch.Start();
#endif
                    if (ITreeConfigNode.IsSorting)
                        return;
                    if (this.OnSelectedNodeChanging != null)
                    {
                        this.OnSelectedNodeChanging(this._SelectedNode, value);
                    }
                    this._SelectedNode = value;
                    this.NotifyPropertyChanged();
                    if (this.OnSelectedNodeChanged != null)
                    {
                        this.OnSelectedNodeChanged();
                    }
#if DEBUG
                    stopWatch.Stop();
                    TimeSpan ts = stopWatch.Elapsed;
                    //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                    //Console.WriteLine("RunTime " + elapsedTime);
                    string elapsedTime = String.Format("{0:00}:{1:00}.{2:000}", ts.Minutes, ts.Seconds, ts.Milliseconds);
                    Debug.WriteLine($"RunTime {elapsedTime} {t4.FilePos()}");
#endif
                }
            }
        }
        private ITreeConfigNode? _SelectedNode;
        public Action? OnSelectedNodeChanged { get; set; }
        public Action<ITreeConfigNode?, ITreeConfigNode?>? OnSelectedNodeChanging { get; set; }

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
        public Dictionary<vPluginLayerTypeEnum, List<PluginRow>>? DicPluginLists { get; set; }
        public Dictionary<string, IvPlugin>? DicPlugins { get; set; }
        // by Guid of generator
        public Dictionary<string, IvPluginGenerator>? DicGenerators { get; set; }

        public IConfig? PrevCurrentConfig { get; set; }
        public IConfig? PrevStableConfig { get; set; }
        public IReadOnlyList<IConfig> GetListConfigs()
        {
            var lst = new List<IConfig>();
            var dic = new Dictionary<string, IConfig>
            {
                [this.Guid] = this
            };
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
                    Debug.Assert(t.ConfigBase != null);
                    dic[t.ConfigBase.Guid] = t.ConfigBase;
                    GetSubConfigs(t.ConfigBase);
                }
            }
        }
        public void RefillDicGenerators()
        {
            //_logger.LogTrace("".StackInfo());
            _logger?.Trace();
            this._DicActiveAppProjectGenerators.Clear();
            _logger?.LogTrace("{DicAppGenerators}", this.DicActiveAppProjectGenerators);
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
            _logger?.LogTrace("{DicAppGenerators}", this.DicActiveAppProjectGenerators);
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
        private void IsHasMarkedPath2(StringBuilder sb, IChildrenCollection children)
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
                        if (t is IEditableNode tt)
                        {
                            sb.Append(" IsChanged=");
                            sb.Append(tt.IsChanged);
                        }
                        sb.AppendLine();
                        var tr = (ITree)t;
                        IsHasMarkedPath2(sb, tr.GetListChildren());
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
        private void IsHasChangedPath2(StringBuilder sb, IChildrenCollection children)
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
                        if (t is IEditableNode tt)
                        {
                            sb.Append(" IsChanged=");
                            sb.Append(tt.IsChanged);
                        }
                        sb.AppendLine();
                        var tr = (ITree)t;
                        IsHasChangedPath2(sb, tr.GetListChildren());
                        break;
                    }
                }
                else
                {
                    sb.Append(t.GetType().Name);
                    if (t is IEditableNode tt)
                    {
                        sb.Append(" IsChanged=");
                        sb.Append(tt.IsChanged);
                    }
                    sb.AppendLine();
                }
            }
        }
        public void SetIsNeedCurrentUpdate(bool val)
        {
            if (this._IsNeedCurrentUpdate != val)
                this._IsNeedCurrentUpdate = val;
        }
        public void SetLastUpdated(DateTime moment)
        {
            this._LastUpdated = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);
        }
#if DEBUG
        public void DicDiffDebug(Config anotherCfg)
        {
            Debug.Assert(this.DicGenerators != null);
            Debug.Assert(anotherCfg.DicGenerators != null);
            Debug.Assert(this.DicPlugins != null);
            Debug.Assert(anotherCfg.DicPlugins != null);
            Debug.Assert(this.DicPluginLists != null);
            Debug.Assert(anotherCfg.DicPluginLists != null);
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
    public class DicDiffResult<TKey, TValue> where TKey : notnull
    {
        public DicDiffResult()
        {
            this.Dic1ButNotInDic2 = new Dictionary<TKey, TValue>();
            this.Dic2ButNotInDic1 = new Dictionary<TKey, TValue>();
        }
        public Dictionary<TKey, TValue> Dic1ButNotInDic2 { get; private set; }
        public Dictionary<TKey, TValue> Dic2ButNotInDic1 { get; private set; }
        public static DicDiffResult<TK, TV> DicDiff<TK, TV>(IReadOnlyDictionary<TK, TV> dic1, IReadOnlyDictionary<TK, TV> dic2) where TK : notnull
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
