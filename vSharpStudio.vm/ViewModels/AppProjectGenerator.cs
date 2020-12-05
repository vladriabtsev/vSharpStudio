﻿using System;
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
    public partial class AppProjectGenerator : ICanRemoveNode, IEditableNode, IEditableNodeGroup
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
        [BrowsableAttribute(false)]
        public IvPlugin Plugin { get { return this.plugin; } }
        private IvPlugin plugin;
        [BrowsableAttribute(false)]
        private IvPluginGenerator PluginGenerator
        {
            get { return _PluginGenerator; }
            set
            {
                _PluginGenerator = value;
            }
        }
        private IvPluginGenerator _PluginGenerator = null;
        [BrowsableAttribute(false)]
        public IvPluginDbGenerator PluginDbGenerator { get; set; }
        partial void OnConnStrChanged()
        {
            if (this.IsConnectString() ?? false)
            {
                (_PluginGenerator as IvPluginDbConnStringGenerator).ConnectionStringToVM(this.ConnStr);
            }
        }
        partial void OnInit()
        {
            this._RelativePathToGenFolder = @"Generated\";
            this.ListGenerators = new SortedObservableCollection<PluginGenerator>();
            cfg = (Config)this.GetConfig();
            HideProperties();
        }
        protected override void OnInitFromDto()
        {
            //base.OnInitFromDto();
            cfg = (Config)this.GetConfig();
            HideProperties();
        }
        public bool? IsConnectString()
        {
            if (_PluginGenerator == null)
                return null;
            return _PluginGenerator is IvPluginDbConnStringGenerator;
        }
        [Browsable(false)]
        new public string IconName
        {
            get
            {
                if (_PluginGenerator == null && cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
                    PluginGenerator = cfg.DicActiveAppProjectGenerators[this.Guid];
                if (_PluginGenerator == null)
                {
                    return null;
                }
                else if (_PluginGenerator is IvPluginDbConnStringGenerator)
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
        public IvPluginGeneratorSettings DynamicMainConnStrSettings
        {
            get
            {
                if (_DynamicMainConnStrSettings == null && cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
                {
                    if (_PluginGenerator is IvPluginDbGenerator)
                    {
                        var db_gen = _PluginGenerator as IvPluginDbGenerator;
                        this._DynamicMainConnStrSettings = db_gen.GetConnectionStringMvvm(db_gen.ProviderName, this.ConnStr);
                    }
                    else
                    {
                        this._DynamicMainConnStrSettings = null;
                    }
                    if (this._DynamicMainConnStrSettings != null)
                    {
                        this.NotifyPropertyChanged();
                        this.ValidateProperty();
                    }
                }
                return this._DynamicMainConnStrSettings;
            }
            set
            {
                this._DynamicMainConnStrSettings = value;
                this.NotifyPropertyChanged();
            }
        }
        private IvPluginGeneratorSettings _DynamicMainConnStrSettings;
        [PropertyOrderAttribute(11)]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [DisplayName("Generator")]
        [Description("General generator settings. Config model nodes can contain additional settings if generator supports node settings")]
        public IvPluginGeneratorSettings DynamicGeneratorSettings
        {
            get
            {
                if (_DynamicGeneratorSettings == null && cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
                {
                    if (_PluginGenerator is IvPluginDbConnStringGenerator)
                    {
                        if (this.PluginDbGenerator == null)
                        {
                            this._DynamicGeneratorSettings = null;
                        }
                        else
                        {
                            this._DynamicGeneratorSettings = (this.PluginDbGenerator as IvPluginGenerator).GetAppGenerationSettingsVmFromJson(this.GeneratorSettings);
                        }
                    }
                    else
                    {
                        this._DynamicGeneratorSettings = _PluginGenerator.GetAppGenerationSettingsVmFromJson(this.GeneratorSettings);
                    }
                    if (this._DynamicGeneratorSettings != null)
                    {
                        this.NotifyPropertyChanged();
                        this.ValidateProperty();
                    }
                }
                return this._DynamicGeneratorSettings;
            }
            set
            {
                this._DynamicGeneratorSettings = value;
                this.NotifyPropertyChanged();
            }
        }
        private IvPluginGeneratorSettings _DynamicGeneratorSettings;

        [PropertyOrderAttribute(12)]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [DisplayName("Model")]
        [Description("Model node settings for generator")]
        public object DynamicModelNodeSettings
        {
            get
            {
                if (_DynamicModelNodeSettings == null)
                {
                    var nd = new NodeSettings();
                    this._DynamicModelNodeSettings = nd.Run(this);
                    if (this._DynamicModelNodeSettings != null)
                    {
                        this.NotifyPropertyChanged();
                        this.ValidateProperty();
                    }
                }
                return this._DynamicModelNodeSettings;
            }
            set
            {
                this._DynamicModelNodeSettings = value;
                this.NotifyPropertyChanged();
            }
        }
        private object _DynamicModelNodeSettings;

        public IvPluginGeneratorSettings GetGeneratorSettings()
        {
            if (_PluginGenerator is IvPluginDbConnStringGenerator)
            {
                var genConnStr = (_PluginGenerator as IvPluginDbConnStringGenerator).DbGenerator;
            }
            return null;
        }
        public IvPluginGroupSolutionSettings GetPluginGroupSettings(string guidGroupSettings)
        {
            var sln = (AppSolution)(this.Parent.Parent);
            var settings = sln.DicPluginsGroupSettings[guidGroupSettings];
            return settings;
        }
        public IvPluginGeneratorNodeSettings GetDefaultNodeSettings(string guidNodeSettings)
        {
            var cfg = this.GetConfig();
            return cfg.Model.GetSettings(this.Guid, guidNodeSettings);
        }
        private string prevRelativePathToGenFolder = string.Empty;
        private string prevGenFileName = string.Empty;
        partial void OnPluginGuidChanging(ref string to)
        {
            cfg.DicActiveAppProjectGenerators.TryRemove(this.Guid);
            if (!string.IsNullOrEmpty(this.PluginGuid))
            {
                var sln = (AppSolution)this.Parent.Parent;
                if (!string.IsNullOrWhiteSpace(this.PluginGuid))
                {
                    var plg = cfg.DicPlugins[this.PluginGuid];
                    var groupSettings = plg.GetPluginGroupSolutionSettingsVmFromJson(null);
                    if (groupSettings != null)
                    {
                        bool is_only = true;
                        foreach (var t in sln.ListAppProjects)
                        {
                            foreach (var tt in t.ListAppProjectGenerators)
                            {
                                if (tt.Guid == this.Guid)
                                    continue;
                                if (tt.PluginGroupSettingsGuid == this.PluginGroupSettingsGuid)
                                    is_only = false;
                            }
                        }
                        if (is_only)
                            sln.DicPluginsGroupSettings.Remove(this.PluginGroupSettingsGuid);
                    }
                }
            }
            cfg.Model.DicGenNodeSettings.TryRemove(this.Guid);
        }
        partial void OnPluginGuidChanged()
        {
            if (this.IsNotNotifying)
                return;
            this.PluginGeneratorGuid = string.Empty;
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
                this.plugin = cfg.DicPlugins[this.PluginGuid];
                var groupSettings = this.plugin.GetPluginGroupSolutionSettingsVmFromJson(null);
                if (groupSettings != null)
                {
                    this.PluginGroupSettingsGuid = groupSettings.Guid;
                    if (!sln.DicPluginsGroupSettings.ContainsKey(groupSettings.Guid))
                    {
                        sln.DicPluginsGroupSettings[groupSettings.Guid] = groupSettings;
                    }
                }
            }
            HideProperties();
            sln.DynamicPluginGroupSettings = null;
            this.DynamicGeneratorSettings = null;
            this.DynamicMainConnStrSettings = null;
            this.DynamicModelNodeSettings = null;
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
            //this.DicGenNodeSettings.TryRemove(this.Guid);
            this._PluginGenerator = null;
            cfg.Model.DicGenNodeSettings.TryRemove(this.Guid);
            this.DynamicGeneratorSettings = null;
            this.DynamicMainConnStrSettings = null;
            this.DynamicModelNodeSettings = null;
        }
        partial void OnPluginGeneratorGuidChanged()
        {
            //IvPluginGenerator gen = null;
            if (this.IsNotNotifying)
                return;
            if (string.IsNullOrWhiteSpace(this.PluginGeneratorGuid))
                return;
            var nv = new ModelVisitorNodeGenSettings();
            //INodeGenSettings ngs = (INodeGenSettings)this;
            //DictionaryExt<string, IvPluginGeneratorNodeSettings> dic = null;
            //if (!this.DicGenNodeSettings.ContainsKey(this.Guid))
            //{
            //    dic = new DictionaryExt<string, IvPluginGeneratorNodeSettings>();
            //    this.DicGenNodeSettings[this.Guid] = dic;
            //}
            //else
            //{
            //    dic = this.DicGenNodeSettings[this.Guid];
            //}

            this.RestoreSettings();
            //if (!cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
            //{
            //}
            nv.NodeGenSettingsApplyAction(cfg, (p) =>
            {
                p.AddNodeAppGenSettings(this.Guid);
            });
            this.DescriptionGenerator = _PluginGenerator.Description;
            if (this.PluginDbGenerator != null)
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
            HideProperties();
            this.NotifyPropertyChanged(() => this.IconName);
            this.NotifyPropertyChanged(() => this.DynamicModelNodeSettings);
            this.NotifyPropertyChanged(() => this.DynamicGeneratorSettings);
            this.NotifyPropertyChanged(() => this.DynamicMainConnStrSettings);
        }
        partial void OnIsGenerateSqlSqriptToUpdatePrevStableChanged()
        {
            HideProperties();
        }
        public void RestoreSettings()
        {
            foreach (var ttt in cfg.GroupPlugins.ListPlugins)
            {
                foreach (var tt in ttt.ListGenerators)
                {
                    if (tt.Generator == null)
                        continue;
                    if (tt.Generator.Guid == this.PluginGeneratorGuid)
                    {
                        var groupSettins = ttt.VPlugin.GetPluginGroupSolutionSettingsVmFromJson(null);
                        if (groupSettins == null)
                        {
                            this.PluginGroupSettingsGuid = string.Empty;
                        }
                        else
                        {
                            this.PluginGroupSettingsGuid = groupSettins.Guid;
                        }
                        this.PluginGenerator = tt.Generator;
                        //List<IvPluginGeneratorNodeSettings> lst = null;
                        if (_PluginGenerator is IvPluginDbConnStringGenerator)
                        {
                            this.PluginDbGenerator = (_PluginGenerator as IvPluginDbConnStringGenerator).DbGenerator;
                            cfg.DicActiveAppProjectGenerators[this.Guid] = this.PluginDbGenerator;
                            //lst = genDb.GetListNodeGenerationSettings();
                            this.PluginDbGenerator.ProviderName = (_PluginGenerator as IvPluginDbConnStringGenerator).ProviderName;
                        }
                        else
                        {
                            cfg.DicActiveAppProjectGenerators[this.Guid] = this.PluginGenerator;
                            //lst = gen.GetListNodeGenerationSettings();
                        }
                        //foreach (var t in lst)
                        //{
                        //    //if (DicGenNodeSettings.ContainsKey(t.Guid))
                        //    //    continue;
                        //    PluginGeneratorNodeSettings gs = null;
                        //    foreach (var ts in ngs.ListNodeGeneratorsSettings)
                        //    {
                        //        if (ts.NodeSettingsVmGuid == t.Guid)
                        //        {
                        //            gs = ts;
                        //            break;
                        //        }
                        //    }
                        //    if (gs == null)
                        //    {
                        //        gs = new PluginGeneratorNodeSettings(this);
                        //        gs.Name = this.Name;
                        //        gs.NodeSettingsVmGuid = t.Guid;
                        //        gs.AppProjectGeneratorGuid = this.Guid;
                        //        ngs.ListNodeGeneratorsSettings.Add(gs);
                        //    }
                        //    gs.SettingsVm = t.GetAppGenerationNodeSettingsVm(gs.Settings, false);
                        //    //if (!this.DicGenNodeSettings.ContainsKey(this.Guid))
                        //    //{
                        //    //    this.DicGenNodeSettings[this.Guid] = new DictionaryExt<string, IvPluginGeneratorNodeSettings>();
                        //    //}
                        //    //var dicS = this.DicGenNodeSettings[this.Guid];
                        //    //dicS[gs.NodeSettingsVmGuid] = gs.SettingsVm;

                        //    // Set link for default settings for ConfigModel
                        //    cfg.Model.TrySetSettings(this.Guid, gs.NodeSettingsVmGuid, gs.SettingsVm);
                        //}
                    }
                }
            }
            if (cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
                this.PluginGenerator = cfg.DicActiveAppProjectGenerators[this.Guid];
            else
                this.PluginGenerator = null;
            if (this.IsConnectString() ?? false)
            {
                (_PluginGenerator as IvPluginDbConnStringGenerator).OnConnectionStringChanged =
                    (connStr) =>
                    {
                        this._ConnStr = connStr;
                        this.NotifyPropertyChanged(() => this.ConnStr);
                    };
            }
        }
        public void SaveSettings()
        {
            if (_PluginGenerator is IvPluginDbConnStringGenerator)
            {
                if (this.DynamicMainConnStrSettings != null)
                    this.ConnStr = this.DynamicMainConnStrSettings.SettingsAsJson;
                else
                    this.ConnStr = string.Empty;
            }
            if (this.DynamicGeneratorSettings != null)
                this.GeneratorSettings = this.DynamicGeneratorSettings.SettingsAsJson;
            else
                this.GeneratorSettings = string.Empty;
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
        public IvPluginGroupSolutionSettings GetPluginsGroupSettings()
        {
            if (!string.IsNullOrEmpty(this.PluginGroupSettingsGuid))
            {
                var sln = (AppSolution)this.Parent.Parent;
                return sln.DicPluginsGroupSettings[this.PluginGroupSettingsGuid];
            }
            return null;
        }
        private void HideProperties()
        {
            if (this.PluginGenerator == null)
            {
                this.AutoGenerateProperties = false;
                this.SetPropertyDefinitions(new string[] {
                        this.GetPropertyName(() => this.GenScriptFileName),
                        this.GetPropertyName(() => this.ConnStrToPrevStable),
                        this.GetPropertyName(() => this.ConnStr),
                        this.GetPropertyName(() => this.RelativePathToGenFolder),
                        this.GetPropertyName(() => this.GenFileName),
                        this.GetPropertyName(() => this.ListGenerators),
                        this.GetPropertyName(() => this.ListInModels),
                        this.GetPropertyName(() => this.DynamicGeneratorSettings),
                        this.GetPropertyName(() => this.DynamicMainConnStrSettings),
                        this.GetPropertyName(() => this.DynamicModelNodeSettings),
                        this.GetPropertyName(() => this.NameUi),
                        this.GetPropertyName(() => this.IsGenerateSqlSqriptToUpdatePrevStable),
                    });
            }
            //else if (gen is IvPluginDbConnStringGenerator)
            //{
            //    this.AutoGenerateProperties = false;
            //    if (this.IsGenerateSqlSqriptToUpdatePrevStable)
            //    {
            //        this.SetPropertyDefinitions(new string[] {
            //            this.GetPropertyName(() => this.GenFileName),
            //            this.GetPropertyName(() => this.ListGenerators),
            //            this.GetPropertyName(() => this.ListInModels),
            //            //this.GetPropertyName(() => this.DynamicNodesSettings),
            //            this.GetPropertyName(() => this.NameUi),
            //        });
            //    }
            //    else
            //    {
            //        this.SetPropertyDefinitions(new string[] {
            //            this.GetPropertyName(() => this.GenScriptFileName),
            //            this.GetPropertyName(() => this.ConnStrToPrevStable),
            //            this.GetPropertyName(() => this.RelativePathToGenFolder),
            //            this.GetPropertyName(() => this.GenFileName),
            //            this.GetPropertyName(() => this.ListGenerators),
            //            this.GetPropertyName(() => this.ListInModels),
            //            //this.GetPropertyName(() => this.DynamicNodesSettings),
            //            this.GetPropertyName(() => this.NameUi),
            //        });
            //    }
            //}
            else if (this.PluginDbGenerator != null)
            {
                if (this.IsGenerateSqlSqriptToUpdatePrevStable)
                {
                    this.SetPropertyDefinitions(new string[] {
                        this.GetPropertyName(() => this.GenFileName),
                        this.GetPropertyName(() => this.ListGenerators),
                        this.GetPropertyName(() => this.ListInModels),
                        this.GetPropertyName(() => this.RelativePathToGenFolder),
                        this.GetPropertyName(() => this.NameUi),
                    });
                }
                else
                {
                    this.SetPropertyDefinitions(new string[] {
                        this.GetPropertyName(() => this.GenScriptFileName),
                        this.GetPropertyName(() => this.GenFileName),
                        this.GetPropertyName(() => this.ListGenerators),
                        this.GetPropertyName(() => this.ListInModels),
                        this.GetPropertyName(() => this.RelativePathToGenFolder),
                        this.GetPropertyName(() => this.NameUi),
                    });
                }
            }
            else
            {
                this.AutoGenerateProperties = false;
                this.SetPropertyDefinitions(new string[] {
                    this.GetPropertyName(() => this.DynamicMainConnStrSettings),
                    this.GetPropertyName(() => this.ConnStr),
                    this.GetPropertyName(() => this.NameUi),
                    this.GetPropertyName(() => this.ListInModels),
                    this.GetPropertyName(() => this.IsGenerateSqlSqriptToUpdatePrevStable),
                    this.GetPropertyName(() => this.GenScriptFileName),
                    this.GetPropertyName(() => this.ConnStrToPrevStable),
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
            return _PluginGenerator as IvPluginDbConnStringGenerator;
        }
        public IvPluginDbGenerator GetDbGenerator()
        {
            return (_PluginGenerator as IvPluginDbConnStringGenerator)?.DbGenerator;
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
            this.PluginGeneratorGuid = string.Empty;
            this.PluginGuid = string.Empty;
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
