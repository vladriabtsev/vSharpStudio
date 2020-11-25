using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Packaging;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    public partial class AppProjectGenerator : ICanRemoveNode, INodeGenSettings, IEditableNode, IEditableNodeGroup
    {
        public static readonly string DefaultName = "Generator";
        private Config cfg;
        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }
        //protected override string GetNodeIconName() { return "iconFolder"; }
        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as AppProject;
            return p.ListAppProjectGenerators;
        }
        public override bool HasChildren()
        {
            return false;
        }
        #endregion ITree
        private IvPluginGenerator gen
        {
            get { return _gen; }
            set
            {
                _gen = value;
                if (this.IsConnectString() ?? false)
                {
                    (_gen as IvPluginDbConnStringGenerator).OnConnectionStringChanged =
                        (connStr) =>
                        {
                            this._ConnStr = connStr;
                            this.NotifyPropertyChanged(() => this.ConnStr);
                        };
                }
            }
        }
        private IvPluginGenerator _gen = null;
        partial void OnConnStrChanged()
        {
            if (this.IsConnectString() ?? false)
            {
                (_gen as IvPluginDbConnStringGenerator).ConnectionStringToVM(this.ConnStr);
            }
        }
        partial void OnInit()
        {
            this._RelativePathToGenFolder = @"Generated\";
            this.ListGenerators = new SortedObservableCollection<PluginGenerator>();
            cfg = (Config)this.GetConfig();
        }
        protected override void OnInitFromDto()
        {
            //base.OnInitFromDto();
            cfg = (Config)this.GetConfig();
        }
        public bool? IsConnectString()
        {
            if (gen == null)
                return null;
            return gen is IvPluginDbConnStringGenerator;
        }
        [Browsable(false)]
        new public string IconName
        {
            get
            {
                if (gen == null && cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
                    gen = cfg.DicActiveAppProjectGenerators[this.Guid];
                if (gen == null)
                {
                    return null;
                }
                else if (gen is IvPluginDbConnStringGenerator)
                {
                    return "iconAddBehavior";
                }
                else
                {
                    return "iconGenerateFile";
                }
            }
        }
        [BrowsableAttribute(false)]
        public SortedObservableCollection<PluginGenerator> ListGenerators { get; private set; }
        [PropertyOrderAttribute(10)]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [DisplayName("Settings ConnStr")]
        [Description("DB connection string generator settings")]
        public object DynamicMainConnStrSettings
        {
            get
            {
                return this._DynamicMainConnStrSettings;
            }
            set
            {
                if (this._DynamicMainConnStrSettings != value)
                {
                    this._DynamicMainConnStrSettings = value;
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private object _DynamicMainConnStrSettings;
        [PropertyOrderAttribute(11)]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [DisplayName("Settings")]
        [Description("General generator settings. Config model nodes can contain additional settings if generator supports node settings")]
        public object DynamicMainSettings
        {
            get
            {
                return this._DynamicMainSettings;
            }
            set
            {
                if (this._DynamicMainSettings != value)
                {
                    this._DynamicMainSettings = value;
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private object _DynamicMainSettings;

        [PropertyOrderAttribute(12)]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [DisplayName("Defaults")]
        [Description("Model node settings for generator")]
        public object DynamicNodeDefaultSettings
        {
            get
            {
                return this._DynamicNodeDefaultSettings;
            }
            set
            {
                if (this._DynamicNodeDefaultSettings != value)
                {
                    this._DynamicNodeDefaultSettings = value;
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private object _DynamicNodeDefaultSettings;

        public IvPluginGeneratorNodeSettings GetDefaultNodeSettings(string guidNodeSettings)
        {
            return this.GetSettings(this.Guid, guidNodeSettings);
        }

        public void CreateGenSettings()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.PluginGeneratorGuid))
                {
                    this.GeneratorSettings = string.Empty;
                    return;
                }
                if (gen == null && cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
                    gen = cfg.DicActiveAppProjectGenerators[this.Guid];
                ChangeSettingsObject();
                //this.NodesSettings = gen?.Generator?.GetAppGenerationSettingsVmFromJson(this.GeneratorSettings);
                HideProperties(gen);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void ChangeSettingsObject()
        {
            if (gen == null)
            {
                this.DynamicMainSettings = null;
                this.DynamicMainConnStrSettings = null;
                this.DynamicNodeDefaultSettings = null;
            }
            else
            {
                if (gen is IvPluginDbConnStringGenerator)
                {
                    this.DynamicMainConnStrSettings = (gen as IvPluginDbConnStringGenerator).ConnectionStringToVM(this.ConnStr);
                    var genDb = (gen as IvPluginDbConnStringGenerator).DbGenerator;
                    if (genDb != null)
                    {
                        this.DynamicMainSettings = (genDb as IvPluginGenerator).GetAppGenerationSettingsVmFromJson(this.GeneratorSettings);
                        var nd = new NodeSettings();
                        var res = nd.Run(this);
                        this.DynamicNodeDefaultSettings = res;
                        nd.Run(this.cfg.Model);
                    }
                }
                else
                {
                    this.DynamicMainSettings = gen.GetAppGenerationSettingsVmFromJson(this.GeneratorSettings);
                    var nd = new NodeSettings();
                    var res = nd.Run(this);
                    this.DynamicNodeDefaultSettings = res;
                    nd.Run(this.cfg.Model);
                }
            }
        }
        private string prevRelativePathToGenFolder = string.Empty;
        private string prevGenFileName = string.Empty;

        partial void OnPluginGuidChanging(ref string to)
        {
            cfg.DicActiveAppProjectGenerators.TryRemove(this.Guid);
            RemovePluginsGroupSettings();
            cfg.Model.DicGenNodeSettings.TryRemove(this.Guid);
        }
        partial void OnPluginGuidChanged()
        {
            if (this.IsNotNotifying)
                return;
            this.PluginGeneratorGuid = "";
            UpdateListGenerators();
            if (cfg.IsInitialized)
            {
                var nv = new ModelVisitorNodeGenSettings();
                nv.NodeGenSettingsApplyAction(this.GetConfig(), (p) =>
                {
                    p.RemoveNodeAppGenSettings(this.Guid);
                });
            }
            //this.NotifyPropertyChanged(() => this.DynamicNodesSettings);
            if (!string.IsNullOrWhiteSpace(this.GenFileName))
                prevGenFileName = this.GenFileName;
            this.GenFileName = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.RelativePathToGenFolder))
                prevRelativePathToGenFolder = this.RelativePathToGenFolder;
            this.RelativePathToGenFolder = string.Empty;

            // adding new plugins group settings
            var sln = (AppSolution)this.Parent.Parent;
            if (!string.IsNullOrWhiteSpace(this.PluginGuid))
            {
                var plg = cfg.DicPlugins[this.PluginGuid];
                if ((plg.PluginGroupSolutionSettings != null) && !sln.DicPluginsGroupSettings.ContainsKey(plg.PluginGroupSolutionSettings.Guid))
                {
                    sln.DicPluginsGroupSettings[plg.PluginGroupSolutionSettings.Guid] = plg.PluginGroupSolutionSettings.GetPluginGroupSolutionSettingsVm(null);
                }
            }
        }
        partial void OnPluginGeneratorGuidChanging(ref string to)
        {
            if (this.IsNotNotifying)
                return;
            if (cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
                cfg.DicActiveAppProjectGenerators.Remove(this.Guid);
            var nv = new ModelVisitorNodeGenSettings();
            nv.NodeGenSettingsApplyAction(cfg, (p) =>
            {
                p.RemoveNodeAppGenSettings(this.Guid);
            });
            this.GeneratorSettings = string.Empty;
            this.DescriptionGenerator = string.Empty;
            this.DicGenNodeSettings.TryRemove(this.Guid);
            this.gen = null;
            cfg.Model.DicGenNodeSettings.TryRemove(this.Guid);
        }
        partial void OnPluginGeneratorGuidChanged()
        {
            IvPluginDbGenerator genDb;
            //IvPluginGenerator gen = null;
            if (this.IsNotNotifying)
                return;
            var nv = new ModelVisitorNodeGenSettings();
            if (string.IsNullOrWhiteSpace(this.PluginGeneratorGuid))
                return;
            INodeGenSettings ngs = (INodeGenSettings)this;
            DictionaryExt<string, IvPluginGeneratorNodeSettings> dic = null;
            if (!this.DicGenNodeSettings.ContainsKey(this.Guid))
            {
                dic = new DictionaryExt<string, IvPluginGeneratorNodeSettings>();
                this.DicGenNodeSettings[this.Guid] = dic;
            }
            else
            {
                dic = this.DicGenNodeSettings[this.Guid];
            }

            if (!cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
            {
                foreach (var ttt in cfg.GroupPlugins.ListPlugins)
                {
                    foreach (var tt in ttt.ListGenerators)
                    {
                        if (tt.Generator.Guid == this.PluginGeneratorGuid)
                        {
                            gen = tt.Generator;
                            List<IvPluginGeneratorNodeSettings> lst = null;
                            if (gen is IvPluginDbConnStringGenerator)
                            {
                                genDb = (gen as IvPluginDbConnStringGenerator).DbGenerator;
                                cfg.DicActiveAppProjectGenerators[this.Guid] = genDb;
                                lst = genDb.GetListNodeGenerationSettings();
                            }
                            else
                            {
                                cfg.DicActiveAppProjectGenerators[this.Guid] = gen;
                                lst = gen.GetListNodeGenerationSettings();
                            }
                            foreach (var t in lst)
                            {
                                if (DicGenNodeSettings.ContainsKey(t.Guid))
                                    continue;
                                PluginGeneratorNodeSettings gs = null;
                                foreach (var ts in ngs.ListNodeGeneratorsSettings)
                                {
                                    if (ts.NodeSettingsVmGuid == t.Guid)
                                    {
                                        gs = ts;
                                        break;
                                    }
                                }
                                if (gs == null)
                                {
                                    gs = new PluginGeneratorNodeSettings();
                                    gs.Name = this.Name;
                                    gs.NodeSettingsVmGuid = t.Guid;
                                    gs.AppProjectGeneratorGuid = this.Guid;
                                    ngs.ListNodeGeneratorsSettings.Add(gs);
                                }
                                gs.SettingsVm = t.GetAppGenerationNodeSettingsVm(gs.Settings, false);
                                if (!this.DicGenNodeSettings.ContainsKey(this.Guid))
                                {
                                    this.DicGenNodeSettings[this.Guid] = new DictionaryExt<string, IvPluginGeneratorNodeSettings>();
                                }
                                var dicS = this.DicGenNodeSettings[this.Guid];
                                dicS[gs.NodeSettingsVmGuid] = gs.SettingsVm;
                                // Set link for default settings for ConfigModel
                                cfg.Model.TrySetSettings(this.Guid, gs.NodeSettingsVmGuid, gs.SettingsVm);
                            }
                        }
                    }
                }
            }
            nv.NodeGenSettingsApplyAction(cfg, (p) =>
            {
                p.AddNodeAppGenSettings(this.Guid);
            });
            ChangeSettingsObject();
            this.DescriptionGenerator = gen.Description;
            if (gen is IvPluginDbConnStringGenerator)
            {
                this._GenFileName = "app-settings.json";
                this._RelativePathToGenFolder = string.Empty;
                //(gen as IvPluginDbConnStringGenerator).ConnectionString = "";
            }
            //else if (gen is IvPluginDbConnStringGenerator)
            //{
            //}
            else
            {
                this._GenFileName = prevGenFileName;
                this._RelativePathToGenFolder = prevRelativePathToGenFolder;
            }
            HideProperties(gen);
            this.NotifyPropertyChanged(this.IconName);
        }
        partial void OnIsGenerateSqlSqriptToUpdatePrevStableChanged()
        {
            HideProperties(gen);
        }
        public void UpdateListGenerators()
        {
            if (cfg.IsInitialized && !string.IsNullOrWhiteSpace(this.PluginGuid))
            {
                Plugin plg = (Plugin)cfg.DicNodes[this.PluginGuid];
                this.ListGenerators.Clear();
                this.ListGenerators.AddRange(plg.ListGenerators);
                //EditorPluginSelection.ListGenerators.Clear();
                //EditorPluginSelection.ListGenerators.AddRange(plg.ListGenerators);
                this.DescriptionPlugin = plg.Description;
            }
        }
        private void RemovePluginsGroupSettings()
        {
            var sln = (AppSolution)this.Parent.Parent;
            if (!string.IsNullOrWhiteSpace(this.PluginGuid))
            {
                var plg = cfg.DicPlugins[this.PluginGuid];
                if ((plg.PluginGroupSolutionSettings != null)
                && sln.DicPluginsGroupSettings.ContainsKey(plg.PluginGroupSolutionSettings.Guid))
                {
                    bool is_only = true;
                    foreach (var t in sln.ListAppProjects)
                    {
                        foreach (var tt in t.ListAppProjectGenerators)
                        {
                            if (tt.Guid == this.Guid)
                                continue;
                            if (tt.PluginGuid == this.PluginGuid)
                                is_only = false;
                        }
                    }
                    if (is_only)
                        sln.DicPluginsGroupSettings.Remove(plg.PluginGroupSolutionSettings.Guid);
                }
            }
        }
        private void HideProperties(IvPluginGenerator gen)
        {
            if (gen == null)
            {
                this.AutoGenerateProperties = false;
            }
            else if (gen is IvPluginDbConnStringGenerator)
            {
                this.AutoGenerateProperties = false;
                if (this.IsGenerateSqlSqriptToUpdatePrevStable)
                {
                    this.SetPropertyDefinitions(new string[] {
                        this.GetPropertyName(() => this.GenFileName),
                        this.GetPropertyName(() => this.ListGenerators),
                        this.GetPropertyName(() => this.ListInModels),
                        this.GetPropertyName(() => this.DynamicNodesSettings),
                        this.GetPropertyName(() => this.NameUi),
                    });
                }
                else
                {
                    this.SetPropertyDefinitions(new string[] {
                        this.GetPropertyName(() => this.GenScriptFileName),
                        this.GetPropertyName(() => this.ConnStrToPrevStable),
                        this.GetPropertyName(() => this.RelativePathToGenFolder),
                        this.GetPropertyName(() => this.GenFileName),
                        this.GetPropertyName(() => this.ListGenerators),
                        this.GetPropertyName(() => this.ListInModels),
                        this.GetPropertyName(() => this.DynamicNodesSettings),
                        this.GetPropertyName(() => this.NameUi),
                    });
                }
            }
            //else if (gen is IvPluginDbConnStringGenerator)
            //{
            //}
            else
            {
                this.AutoGenerateProperties = false;
                this.SetPropertyDefinitions(new string[] {
                    this.GetPropertyName(() => this.DynamicMainConnStrSettings),
                    this.GetPropertyName(() => this.ConnStr),
                    this.GetPropertyName(() => this.DynamicNodesSettings),
                    this.GetPropertyName(() => this.NameUi),
                    //this.GetPropertyName(() => this.IsPrivateConnStr),
                });
            }
        }
        public string GetGenerationFilePath()
        {
            var path = this.GetGenerationFolderPath();
            path = Path.Combine(path, this.GenFileName);
            return path;
        }
        public string GetGenerationFolderPath()
        {
            if (string.IsNullOrEmpty(cfg.CurrentCfgFolderPath))
                return "";
            AppProject prj = this.Parent as AppProject;
            var path = prj.GetProjectFolderPath();
            if (string.IsNullOrWhiteSpace(this.RelativePathToGenFolder))
                path = Path.Combine(path, this.RelativePathToGenFolder);
            return path;
        }
        partial void OnRelativePathToGenFolderChanging(ref string to)
        {
            if (this.IsNotNotifying || string.IsNullOrWhiteSpace(to))
                return;
            to = Path.GetFullPath(to);
        }
        partial void OnRelativePathToGenFolderChanged()
        {
            if (this.IsNotNotifying)
                return;
            if (string.IsNullOrEmpty(cfg.CurrentCfgFolderPath))
                throw new Exception("Config is not saved yet");
            var sln = this.Parent.Parent as AppSolution;
            if (sln.RelativeAppSolutionPath == null)
                throw new Exception("Solution path is not selected yet");
            var prj = this.Parent as AppProject;
            if (prj.RelativeAppProjectPath == null)
                throw new Exception("Project path is not selected yet");
            if (!string.IsNullOrWhiteSpace(this._RelativePathToGenFolder))
            {
                var path = prj.GetProjectFolderPath();
#if NET48
                this._RelativePathToGenFolder = vSharpStudio.common.Utils..GetRelativePath(path, this._RelativePathToGenFolder);
#else
                this._RelativePathToGenFolder = Path.GetRelativePath(path, this._RelativePathToGenFolder);
#endif
            }
        }
        public IvPluginDbConnStringGenerator GetDbConnStringGenerator()
        {
            return gen as IvPluginDbConnStringGenerator;
        }
        public IvPluginDbGenerator GetDbGenerator()
        {
            return (gen as IvPluginDbConnStringGenerator)?.DbGenerator;
        }

        #region Tree operations
        //public bool CanAddSubNode()
        //{
        //    if (this.PluginGeneratorGuid == null)
        //        return false;
        //    Config cfg = (Config)this.GetConfig();
        //    var gen = cfg.DicActiveAppProjectGenerators[this.Guid];
        //    if (gen.PluginGeneratorType == vPluginLayerTypeEnum.DbDesign)
        //    {
        //        var plugin = cfg.DicPlugins[this.PluginGuid];
        //        foreach (var t in plugin.ListGenerators)
        //        {
        //            if (t.PluginGeneratorType == vPluginLayerTypeEnum.DbConnection)
        //                return true;
        //        }
        //        //var pluginLst = cfg.DicPluginLists[vPluginLayerTypeEnum.DbDesign];
        //        //(gen as IvPluginDbGenerator).
        //    }
        //    return false;
        //}
        //public override void NodeRemove()
        //{
        //    this.PluginGeneratorGuid = "";
        //    (this.Parent as AppProject).ListAppProjectGenerators.Remove(this);
        //    this.Parent = null;
        //}
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as AppProject).ListAppProjectGenerators.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (AppProject)(this.Parent as AppProject).ListAppProjectGenerators.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            (this.Parent as AppProject).ListAppProjectGenerators.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as AppProject).ListAppProjectGenerators.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (AppProject)(this.Parent as AppProject).ListAppProjectGenerators.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            (this.Parent as AppProject).ListAppProjectGenerators.MoveDown(this);
            this.SetSelected(this);
        }

        public void NodeRemove(bool ask = true)
        {
            if (ask)
            {
                var res = MessageBox.Show("You are deleting generator. Continue?", "Warning", System.Windows.MessageBoxButton.OKCancel);
                if (res != System.Windows.MessageBoxResult.OK)
                    return;
            }
            this.PluginGeneratorGuid = "";
            this.PluginGuid = "";
            (this.Parent as AppProject).ListAppProjectGenerators.Remove(this);
            this.Parent = null;
        }

        public override ITreeConfigNode NodeAddClone()
        {
            var node = AppProjectGenerator.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            (this.Parent as AppProject).ListAppProjectGenerators.Add(node);
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new AppProjectGenerator(this.Parent);
            (this.Parent as AppProject).ListAppProjectGenerators.Add(node);
            this.GetUniqueName(AppProjectGenerator.DefaultName, node, (this.Parent as AppProject).ListAppProjectGenerators);
            this.SetSelected(node);
            return node;
        }

        #endregion Tree operations
        public void Remove()
        {
            var p = this.Parent as AppProject;
            p.ListAppProjectGenerators.Remove(this);
        }
    }
}
