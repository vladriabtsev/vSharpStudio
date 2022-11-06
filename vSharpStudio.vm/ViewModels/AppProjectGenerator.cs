using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Text;
using FluentValidation.Results;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("AppPrjGen:{Name,nq} Conn:{ConnStr,nq} File:{GenFileName,nq}")]
    public partial class AppProjectGenerator : ICanRemoveNode, IEditableNode, IEditableNodeGroup
    {
        private Config cfg;
        [BrowsableAttribute(false)]
        public AppProject ParentAppProject { get { Debug.Assert(this.Parent != null); return (AppProject)this.Parent; } }
        [BrowsableAttribute(false)]
        public IAppProject ParentAppProjectI { get { Debug.Assert(this.Parent != null); return (IAppProject)this.Parent; } }
        //protected override string GetNodeIconName() { return "iconFolder"; }
        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return new List<ITreeConfigNode>();
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            return this.ParentAppProject.ListAppProjectGenerators;
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
        public IvPluginGenerator? PluginGenerator
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.PluginGeneratorGuid))
                    return null;
                var gntr = cfg.DicGenerators[this.PluginGeneratorGuid];
                if (gntr == null)
                    throw new Exception($"Can't find plugin generator '{this.DescriptionGenerator}' with Guid={this.PluginGeneratorGuid} for application project generator '{this.Name}'");
                return gntr;
            }
        }
        [BrowsableAttribute(false)]
        public IvPluginDbGenerator? PluginDbGenerator
        {
            get
            {
                return this.PluginGenerator as IvPluginDbGenerator;
            }
        }
        partial void OnConnStrChanged()
        {
            if (this.IsNotifying && this.PluginDbGenerator != null)
            {
                this.DynamicMainConnStrSettings = this.PluginDbGenerator.GetConnectionStringMvvm(this, this.ConnStr);
            }
        }
        partial void OnCreated()
        {
            this._RelativePathToGenFolder = @"Generated\";
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this.cfg = this.ParentAppProject.ParentAppSolution.ParentGroupListAppSolutions.ParentConfig;
            this.ListGenerators = new SortedObservableCollection<PluginGenerator>();
            //this.ListGenerators.OnAddingAction = (t) =>
            //{
            //    t.IsNew = true;
            //};
            //this.ListGenerators.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            //this.ListGenerators.OnRemovedAction = (t) =>
            //{
            //    this.OnRemoveChild();
            //};
            //this.ListGenerators.OnClearedAction = () =>
            //{
            //    this.OnRemoveChild();
            //};
        }
        [Browsable(false)]
        new public string? IconName
        {
            get
            {
                //if (_PluginGenerator == null && cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
                //    PluginGenerator = cfg.DicActiveAppProjectGenerators[this.Guid];
                if (this.PluginGenerator == null)
                {
                    return null;
                }
                else if (this.PluginDbGenerator != null)
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
        // Used only to select generator in UI
        public SortedObservableCollection<PluginGenerator> ListGenerators { get; private set; }
        [PropertyOrderAttribute(10)]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [DisplayName("Settings ConnStr")]
        [Description("DB connection string generator settings")]
        public IvPluginGeneratorSettings? DynamicMainConnStrSettings
        {
            get
            {
                if (_DynamicMainConnStrSettings == null && cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
                {
                    if (this.PluginGenerator is IvPluginDbGenerator)
                    {
                        //if (this.PluginGenerator.Guid ==)
                        var db_gen = this.PluginGenerator as IvPluginDbGenerator;
                        try
                        {
                            this._DynamicMainConnStrSettings = db_gen?.GetConnectionStringMvvm(this, this.ConnStr);
                        }
                        catch (Exception ex)
                        {
                            var res = MessageBox.Show($"Can't create connection string VM.\nError: {ex.Message}\nChoose another provider!", "Error", System.Windows.MessageBoxButton.OKCancel);
                            this._DynamicMainConnStrSettings = null;
                            return this._DynamicMainConnStrSettings;
                        }
                        Debug.Assert(this._DynamicMainConnStrSettings != null);
                        this._DynamicMainConnStrSettings.PropertyChanged += _DynamicMainConnStrSettings_PropertyChanged;
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

        private void _DynamicMainConnStrSettings_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            Debug.Assert(this._DynamicMainConnStrSettings != null);
            this._ConnStr = this._DynamicMainConnStrSettings.GenerateCode(this.cfg, this.ParentAppProject.ParentAppSolution, this.ParentAppProject, this);
            this.NotifyPropertyChanged(() => this.ConnStr);
        }

        private IvPluginGeneratorSettings? _DynamicMainConnStrSettings;
        [PropertyOrderAttribute(11)]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [DisplayName("Generator")]
        [Description("General generator settings. Config model nodes can contain additional settings if generator supports node settings")]
        public IvPluginGeneratorSettings? DynamicGeneratorSettings
        {
            get
            {
                if (_DynamicGeneratorSettings == null && cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
                {
                    Debug.Assert(this.PluginGenerator != null);
                    this._DynamicGeneratorSettings = this.PluginGenerator.GetAppGenerationSettingsVmFromJson(this, this.GeneratorSettings);
                    Debug.Assert(this._DynamicGeneratorSettings.ParentAppProjectGenerator != null);
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
                return this._DynamicGeneratorSettings!;
            }
            set
            {
                this._DynamicGeneratorSettings = value;
                this.NotifyPropertyChanged();
            }
        }
        private IvPluginGeneratorSettings? _DynamicGeneratorSettings;

        [PropertyOrderAttribute(12)]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [DisplayName("Model")]
        [Description("Model node settings for generator")]
        public object? DynamicModelNodeSettings
        {
            get
            {
                if (_DynamicModelNodeSettings == null)
                {
                    this._DynamicModelNodeSettings = this.cfg.Model.GetSettings(this.Guid);
                    //var nd = new NodeSettings();
                    //this._DynamicModelNodeSettings = nd.Run(this);
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
        private object? _DynamicModelNodeSettings;
        //public IvPluginGroupSolutionSettings GetPluginGroupSettings(string guidGroupSettings)
        //{
        //    var sln = (AppSolution)(this.Parent.Parent);
        //    var settings = sln.DicPluginsGroupSettings[guidGroupSettings];
        //    return settings;
        //}
        public IvPluginGeneratorNodeSettings? GetDefaultNodeSettings()
        {
            return this.cfg.Model.GetSettings(this.Guid);
        }
        private string prevRelativePathToGenFolder = string.Empty;
        private string prevGenFileName = string.Empty;
        partial void OnPluginGuidChanging(ref string to)
        {
            cfg._DicActiveAppProjectGenerators.TryRemove(this.Guid);
            if (!string.IsNullOrEmpty(this.PluginGuid))
            {
                this.PluginGeneratorGuid = String.Empty;
            }
            cfg.Model._DicGenNodeSettings.TryRemove(this.Guid);
        }

        private void RemoveGroupSettingsIfLast()
        {
            var sln = this.ParentAppProject.ParentAppSolution;
            bool is_only_in_sln = true;
            foreach (var t in sln.ListAppProjects)
            {
                bool is_only_in_prj = true;
                foreach (var tt in t.ListAppProjectGenerators)
                {
                    if (tt.Guid == this.Guid)
                        continue;
                    if (tt.PluginGeneratorGroupGuid == this.PluginGeneratorGroupGuid)
                    {
                        is_only_in_prj = false;
                        is_only_in_sln = false;
                    }
                }
                if (is_only_in_prj)
                {
                    if (t.DicPluginsGroupSettings.ContainsKey(this.PluginGeneratorGroupGuid))
                        t.DicPluginsGroupSettings.Remove(this.PluginGeneratorGroupGuid);
                }
            }
            if (is_only_in_sln)
            {
                if (sln.DicPluginsGroupSettings.ContainsKey(this.PluginGeneratorGroupGuid))
                    sln.DicPluginsGroupSettings.Remove(this.PluginGeneratorGroupGuid);
            }
        }

        partial void OnPluginGuidChanged()
        {
            if (!this.IsNotifying)
                return;
            //this.RemoveGroupSettingsIfLast();
            this.PluginGeneratorGuid = string.Empty;
            this.GenFileName = string.Empty;
            UpdateListGenerators();
            //if (cfg.IsInitialized)
            //{
            //    var nv = new ModelVisitorNodeGenSettings();
            //    nv.NodeGenSettingsApplyAction(this.GetConfig(), (p) =>
            //    {
            //        p.RemoveNodeAppGenSettings(this.Guid);
            //    });
            //}
            //this.NotifyPropertyChanged(() => this.DynamicNodesSettings);
            if (!string.IsNullOrWhiteSpace(this.GenFileName))
                prevGenFileName = this.GenFileName;
            this.GenFileName = string.Empty;
            if (!string.IsNullOrWhiteSpace(this.RelativePathToGenFolder))
                prevRelativePathToGenFolder = this.RelativePathToGenFolder;
            this.RelativePathToGenFolder = string.Empty;

            this.NotifyPropertyChanged(() => this.PropertyDefinitions);

            var sln = this.ParentAppProject.ParentAppSolution;
            sln.DynamicPluginGroupSettings = null;
            foreach (var t in sln.ListAppProjects)
            {
                t.DynamicPluginGroupSettings = null;
            }
            this.DynamicGeneratorSettings = null;
            this.DynamicMainConnStrSettings = null;
            this.DynamicModelNodeSettings = null;
            // auto selection generator if it is only one
            if (this.ListGenerators.Count == 1)
            {
                this.PluginGeneratorGuid = this.ListGenerators[0].Guid;
            }
        }
        partial void OnPluginGeneratorGuidChanging(ref string to)
        {
            //if (cfg.IsInitialized)
            //{
            if (!this.IsNotifying)
                return;
            if (cfg._DicActiveAppProjectGenerators.ContainsKey(this.Guid))
                cfg._DicActiveAppProjectGenerators.Remove(this.Guid);
            var nv = new ModelVisitorNodeGenSettings();
            nv.NodeGenSettingsApplyAction(cfg, (p) =>
            {
                p.RemoveNodeAppGenSettings(this.Guid);
            });
            if (!string.IsNullOrWhiteSpace(this.PluginGeneratorGroupGuid))
            {
                this.RemoveGroupSettingsIfLast();
            }
            this.GeneratorSettings = string.Empty;
            this.DescriptionGenerator = string.Empty;
            //this.DicGenNodeSettings.TryRemove(this.Guid);
            //this.PluginGeneratorGuid = String.Empty;
            this.PluginGeneratorGroupGuid = String.Empty;
            cfg.Model._DicGenNodeSettings.TryRemove(this.Guid);
            this.DynamicGeneratorSettings = null;
            this.DynamicMainConnStrSettings = null;
            this.DynamicModelNodeSettings = null;
        }
        partial void OnPluginGeneratorGuidChanged()
        {
            if (!this.IsNotifying)
                return;
            if (string.IsNullOrWhiteSpace(this.PluginGeneratorGuid))
                return;
            var nv = new ModelVisitorNodeGenSettings();
            Debug.Assert(this.PluginGenerator != null);
            cfg._DicActiveAppProjectGenerators[this.Guid] = this.PluginGenerator;
            if (this.plugin == null)
                this.plugin = cfg.DicPlugins[this.PluginGuid];
            this.RestoreSettings();
            this.PluginGeneratorGroupGuid = this.PluginGenerator.GroupGeneratorsGuid;
            (this.ParentAppProject as AppProject).RestoreGroupSettings(this.PluginGenerator);
            (this.ParentAppProject.ParentAppSolution as AppSolution).RestoreGroupSettings(this.PluginGenerator);
            nv.NodeGenSettingsApplyAction(cfg, (p) =>
            {
                p.AddNodeAppGenSettings(this.Guid);
            });
            this.DescriptionGenerator = this.PluginGenerator.Description;
            if (this.PluginDbGenerator != null)
            {
                this._GenFileName = "app-settings.json";
                this._RelativePathToGenFolder = string.Empty;
            }
            else
            {
                this._GenFileName = prevGenFileName;
                this._RelativePathToGenFolder = prevRelativePathToGenFolder;
            }
            this.NotifyPropertyChanged(() => this.PropertyDefinitions);
            this.NotifyPropertyChanged(() => this.IconName);
            this.NotifyPropertyChanged(() => this.DynamicModelNodeSettings);
            this.NotifyPropertyChanged(() => this.DynamicGeneratorSettings);
            this.NotifyPropertyChanged(() => this.DynamicMainConnStrSettings);
        }
        partial void OnIsGenerateSqlSqriptToUpdatePrevStableChanged()
        {
            this.NotifyPropertyChanged(() => this.PropertyDefinitions);
        }
        protected override void OnValidated(ValidationResult res)
        {
            if (this.PluginGeneratorSettings == null)
                return;
            var res2 = this.PluginGeneratorSettings.ValidateSettings();
            foreach (var tt in res2.Errors)
            {
                tt.PropertyName = nameof(this.DynamicGeneratorSettings);
            }
            res.Errors.AddRange(res2.Errors);
        }
        IvPluginGeneratorSettings PluginGeneratorSettings { get; set; }
        public void RestoreSettings()
        {
            if (this.PluginGenerator != null)
            {
                if (!cfg.DicGroupSettingGenerators.ContainsKey(this.PluginGenerator.GroupGeneratorsGuid))
                    cfg.DicGroupSettingGenerators[this.PluginGenerator.GroupGeneratorsGuid] = this.PluginGenerator;
                if (string.IsNullOrWhiteSpace(this.GeneratorSettings))
                {
                    this.PluginGeneratorSettings = this.PluginGenerator.GetAppGenerationSettingsVmFromJson(this, null);
                }
                else
                {
                    this.PluginGeneratorSettings = this.PluginGenerator.GetAppGenerationSettingsVmFromJson(this, this.GeneratorSettings);
                }
                Debug.Assert(this.PluginGeneratorSettings.ParentAppProjectGenerator != null);
                this.DynamicGeneratorSettings = this.PluginGeneratorSettings;
            }
            //else if (this.PluginDbGenerator != null)
            //{
            //    IvPluginGeneratorSettings set = null;
            //    if (string.IsNullOrWhiteSpace(this.GeneratorSettings))
            //    {
            //        set = this.PluginDbGenerator.GetAppGenerationSettingsVmFromJson(this, null);
            //    }
            //    else
            //    {
            //        set = this.PluginDbGenerator.GetAppGenerationSettingsVmFromJson(this, this.GeneratorSettings);
            //    }
            //    set.Parent = this;
            //    this.DynamicGeneratorSettings = set;
            //}


            //// adding new plugins group settings
            //var sln = (AppSolution)this.Parent.Parent;
            //if (!string.IsNullOrWhiteSpace(this.PluginGuid))
            //{
            //    this.plugin = cfg.DicPlugins[this.PluginGuid];
            //    this.Name = this.plugin.Name;
            //    var groupSlnSettings = this.plugin.GetPluginGroupSolutionSettingsVmFromJson(null);
            //    Debug.Assert(groupSlnSettings != null);
            //    if (groupSlnSettings != null)
            //    {
            //        this.PluginGroupGuid = groupSlnSettings.Guid;
            //        if (!sln.DicPluginsGroupSettings.ContainsKey(this.PluginGenerator.GroupGuid))
            //        {
            //            sln.DicPluginsGroupSettings[this.PluginGroupSettingsGuid] = groupSlnSettings;
            //            foreach (var t in sln.ListAppProjects)
            //            {
            //                var groupPrjSettings = this.plugin.GetPluginGroupProjectSettingsVmFromJson(null);
            //                Debug.Assert(groupPrjSettings != null);
            //                t.DicPluginsGroupSettings[this.PluginGroupSettingsGuid] = groupPrjSettings;
            //            }
            //        }
            //    }
            //}
            //if (cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
            //{
            //    this.PluginGenerator = cfg.DicActiveAppProjectGenerators[this.Guid];
            //    this.PluginDbGenerator = this.PluginGenerator as IvPluginDbGenerator;
            //}
            //else
            //{
            //    this.PluginGenerator = null;
            //    this.PluginDbGenerator = null;
            //}
            this.NotifyPropertyChanged(() => this.PropertyDefinitions);
        }
        public void SaveSettings()
        {
            if (this.DynamicGeneratorSettings != null)
                this.GeneratorSettings = this.DynamicGeneratorSettings.SettingsAsJson;
            else
                this.GeneratorSettings = string.Empty;
        }
        public void UpdateListGenerators()
        {
            if (cfg.IsInitialized && !string.IsNullOrWhiteSpace(this.PluginGuid))
            {
                var plg = (Plugin)cfg.DicNodes[this.PluginGuid];
                this.ListGenerators.Clear();
                this.ListGenerators.AddRange(plg.ListGenerators);
                this.DescriptionPlugin = plg.Description;
            }
        }
        //public IvPluginGroupSolutionSettings GetPluginsGroupSettings()
        //{
        //    if (!string.IsNullOrEmpty(this.PluginGroupSettingsGuid))
        //    {
        //        var sln = (AppSolution)this.Parent.Parent;
        //        return sln.DicPluginsGroupSettings[this.PluginGroupSettingsGuid];
        //    }
        //    return null;
        //}
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            if (this.PluginGenerator == null)
            {
                lst.Add(this.GetPropertyName(() => this.GenScriptFileName));
                lst.Add(this.GetPropertyName(() => this.ConnStrToPrevStable));
                lst.Add(this.GetPropertyName(() => this.ConnStr));
                lst.Add(this.GetPropertyName(() => this.RelativePathToGenFolder));
                lst.Add(this.GetPropertyName(() => this.GenFileName));
                lst.Add(this.GetPropertyName(() => this.ListGenerators));
                lst.Add(this.GetPropertyName(() => this.ListInModels));
                lst.Add(this.GetPropertyName(() => this.DynamicGeneratorSettings));
                lst.Add(this.GetPropertyName(() => this.DynamicMainConnStrSettings));
                lst.Add(this.GetPropertyName(() => this.DynamicModelNodeSettings));
                lst.Add(this.GetPropertyName(() => this.NameUi));
                lst.Add(this.GetPropertyName(() => this.IsGenerateSqlSqriptToUpdatePrevStable));
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
                    lst.Add(this.GetPropertyName(() => this.GenFileName));
                    lst.Add(this.GetPropertyName(() => this.ListGenerators));
                    lst.Add(this.GetPropertyName(() => this.ListInModels));
                    lst.Add(this.GetPropertyName(() => this.RelativePathToGenFolder));
                    lst.Add(this.GetPropertyName(() => this.NameUi));
                }
                else
                {
                    lst.Add(this.GetPropertyName(() => this.GenScriptFileName));
                    lst.Add(this.GetPropertyName(() => this.GenFileName));
                    lst.Add(this.GetPropertyName(() => this.ListGenerators));
                    lst.Add(this.GetPropertyName(() => this.ListInModels));
                    lst.Add(this.GetPropertyName(() => this.RelativePathToGenFolder));
                    lst.Add(this.GetPropertyName(() => this.NameUi));
                }
            }
            else
            {
                lst.Add(this.GetPropertyName(() => this.DynamicMainConnStrSettings));
                lst.Add(this.GetPropertyName(() => this.ConnStr));
                lst.Add(this.GetPropertyName(() => this.NameUi));
                lst.Add(this.GetPropertyName(() => this.ListInModels));
                lst.Add(this.GetPropertyName(() => this.IsGenerateSqlSqriptToUpdatePrevStable));
                lst.Add(this.GetPropertyName(() => this.GenScriptFileName));
                lst.Add(this.GetPropertyName(() => this.ConnStrToPrevStable));
            }
            return lst.ToArray();
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
            var path = this.ParentAppProject.GetProjectFolderPath();
            if (string.IsNullOrWhiteSpace(this.RelativePathToGenFolder))
                path = Path.Combine(path, this.RelativePathToGenFolder);
            return path;
        }
        partial void OnRelativePathToGenFolderChanging(ref string to)
        {
            if (!this.IsNotifying || string.IsNullOrWhiteSpace(to))
                return;
            to = Path.GetFullPath(to);
        }
        partial void OnRelativePathToGenFolderChanged()
        {
            if (!this.IsNotifying)
                return;
            if (string.IsNullOrEmpty(cfg.CurrentCfgFolderPath))
                throw new Exception("Config is not saved yet");
            var prj = this.ParentAppProject;
            if (prj.RelativeAppProjectPath == null)
                throw new Exception("Project path is not selected yet");
            var sln = prj.ParentAppSolution;
            if (sln.RelativeAppSolutionPath == null)
                throw new Exception("Solution path is not selected yet");
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
                if (this.ParentAppProject.ListAppProjectGenerators.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (AppProject)this.ParentAppProject.ListAppProjectGenerators.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentAppProject.ListAppProjectGenerators.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentAppProject.ListAppProjectGenerators.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (AppProject)this.ParentAppProject.ListAppProjectGenerators.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentAppProject.ListAppProjectGenerators.MoveDown(this);
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
            this.ParentAppProject.ListAppProjectGenerators.Remove(this);
            this.Parent = null;
        }

        public override ITreeConfigNode NodeAddClone()
        {
            var node = AppProjectGenerator.Clone(this.ParentAppProject, this, true, true);
            node.Parent = this.Parent;
            this.ParentAppProject.ListAppProjectGenerators.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new AppProjectGenerator(this.Parent);
            this.ParentAppProject.ListAppProjectGenerators.Add(node);
            this.GetUniqueName(Defaults.AppPrjGeneratorName, node, this.ParentAppProject.ListAppProjectGenerators);
            this.SetSelected(node);
            return node;
        }

        #endregion Tree operations
        public void Remove()
        {
            this.ParentAppProject.ListAppProjectGenerators.Remove(this);
        }
    }
}
