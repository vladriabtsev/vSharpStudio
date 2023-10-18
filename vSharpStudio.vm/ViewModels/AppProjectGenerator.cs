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
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class AppProjectGenerator : ICanRemoveNode, ICanAddNode, IEditableNode, IEditableNodeGroup, INodeDeletable
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Conn:{ConnStr} File:{GenFileName}";
        }
        private Config? cfg;
        [Browsable(false)]
        public AppProject ParentAppProject { get { Debug.Assert(this.Parent != null); return (AppProject)this.Parent; } }
        [Browsable(false)]
        public IAppProject ParentAppProjectI { get { Debug.Assert(this.Parent != null); return (IAppProject)this.Parent; } }
        //protected override string GetNodeIconName() { return "iconFolder"; }
        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return new ConfigNodesCollection<ITreeConfigNodeSortable>(this);
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentAppProject.ListAppProjectGenerators;
        }
        public override bool HasChildren()
        {
            return false;
        }
        #endregion ITree
        //[BrowsableAttribute(false)]
        //new public ConfigNodesCollection<AppProjectGenerator> Children { get { return this.ListAppProjectGenerators; } }
        [Browsable(false)]
        public IvPlugin? Plugin { get { return this.plugin; } }
        private IvPlugin? plugin;
        [Browsable(false)]
        public IvPluginGenerator? PluginGenerator
        {
            get { return this._PluginGenerator; }
            //get
            //{
            //if (string.IsNullOrWhiteSpace(this.PluginGeneratorGuid))
            //    return null;
            //Debug.Assert(cfg != null);
            //Debug.Assert(cfg.DicGenerators != null);
            //if (!cfg.DicGenerators.ContainsKey(this.PluginGeneratorGuid))
            //    return null;
            //var gntr = cfg.DicGenerators[this.PluginGeneratorGuid];
            //return gntr;
            //}
            set
            {
                if (this.SetProperty(ref this._PluginGenerator, value))
                {
                    this.PluginDbGenerator = this._PluginGenerator as IvPluginDbGenerator;
                }
            }
        }
        private IvPluginGenerator? _PluginGenerator;
        [Browsable(false)]
        public IvPluginDbGenerator? PluginDbGenerator
        {
            get { return this._PluginDbGenerator; }
            set { this.SetProperty(ref this._PluginGenerator, value); }
        }
        private IvPluginDbGenerator? _PluginDbGenerator;
        partial void OnConnStrChanged()
        {
            DebugExt.WriteLineWithStack();
            if (this.PluginDbGenerator != null)
            {
                DebugExt.WriteLine($"this.ConnStr='{this.ConnStr}'");
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
        public new string? IconName
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
        [Browsable(false)]
        // Used only to select generator in UI
        public SortedObservableCollection<PluginGenerator>? ListGenerators { get; private set; }
        [PropertyOrderAttribute(10)]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [DisplayName("Settings ConnStr")]
        [Description("DB connection string generator settings")]
        public IvPluginGeneratorSettings? DynamicMainConnStrSettings
        {
            get { return this._DynamicMainConnStrSettings; }
            set
            {
                if (this.SetProperty(ref this._DynamicMainConnStrSettings, value))
                {
                    Debug.Assert(this._DynamicMainConnStrSettings != null);
                    Debug.Assert(cfg != null);
                    this._ConnStr = this._DynamicMainConnStrSettings.GenerateCode(this.cfg, this.ParentAppProject.ParentAppSolution, this.ParentAppProject, this);
                    DebugExt.WriteLineWithStack($"this._ConnStr='{this._ConnStr}'");
                    this.NotifyPropertyChanged(nameof(this.ConnStr));
                }
            }
//            get
//            {
//                Debug.Assert(cfg != null);
//                DebugExt.WriteLineWithStack();
//                if (_DynamicMainConnStrSettings == null && cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
//                {
//                    if (this.PluginGenerator is IvPluginDbGenerator)
//                    {
//                        DebugExt.WriteLine($"Trying get connection string model from DB generator. this.ConnStr='{this.ConnStr}'");
//                        //if (this.PluginGenerator.Guid ==)
//                        var db_gen = this.PluginGenerator as IvPluginDbGenerator;
//                        try
//                        {
//                            this._DynamicMainConnStrSettings = db_gen?.GetConnectionStringMvvm(this, this.ConnStr);
//                        }
//                        catch (Exception ex)
//                        {
//#if DEBUG
//                            if (VmBindable.isUnitTests)
//                                throw;
//                            else
//#endif
//                                MessageBox.Show($"Can't create connection string VM.\nError: {ex.Message}\nChoose another provider!", "Error", System.Windows.MessageBoxButton.OKCancel);
//                            this._DynamicMainConnStrSettings = null;
//                            return this._DynamicMainConnStrSettings;
//                        }
//                        Debug.Assert(this._DynamicMainConnStrSettings != null);
//                        this._DynamicMainConnStrSettings.PropertyChanged += _DynamicMainConnStrSettings_PropertyChanged;
//                    }
//                    else
//                    {
//                        DebugExt.WriteLine("Resetting _DynamicMainConnStrSettings");
//                        this._DynamicMainConnStrSettings = null;
//                    }
//                    if (this._DynamicMainConnStrSettings != null)
//                    {
//                        this.NotifyPropertyChanged();
//                        this.ValidateProperty();
//                    }
//                }
//                return this._DynamicMainConnStrSettings;
//            }
//            set
//            {
//                DebugExt.WriteLine($"this._DynamicMainConnStrSettings='{value}'");
//                //SetProperty(ref this._DynamicMainConnStrSettings, value);
//                this._DynamicMainConnStrSettings = value;
//                this.NotifyPropertyChanged();
//            }
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
                Debug.Assert(cfg != null);
                DebugExt.WriteLineWithStack();
                if (_DynamicGeneratorSettings == null && cfg.DicActiveAppProjectGenerators.ContainsKey(this.Guid))
                {
                    Debug.Assert(this.PluginGenerator != null);
                    this._DynamicGeneratorSettings = this.PluginGenerator.GetAppGenerationSettingsVmFromJson(this, this.GeneratorSettings);
                    Debug.Assert(this._DynamicGeneratorSettings != null);
                    Debug.Assert(this._DynamicGeneratorSettings.ParentAppProjectGenerator != null);
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
                return this._DynamicGeneratorSettings!;
            }
            set
            {
                DebugExt.WriteLineWithStack();
                //SetProperty(ref this._DynamicGeneratorSettings, value);
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
                DebugExt.WriteLineWithStack();
                if (_DynamicModelNodeSettings == null)
                {
                    Debug.Assert(cfg != null);
                    DebugExt.WriteLine($"Getting setings for '{this.Guid}'");
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
                DebugExt.WriteLineWithStack();
                //SetProperty(ref this._DynamicModelNodeSettings, value);
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
            Debug.Assert(cfg != null);
            DebugExt.WriteLineWithStack();
            return this.cfg.Model.GetSettings(this.Guid);
        }
        private string prevRelativePathToGenFolder = string.Empty;
        private string prevGenFileName = string.Empty;

        private void RemoveGroupSettingsIfLast()
        {
            DebugExt.WriteLineWithStack();
            IvPluginGenerator? gnt = this.PluginDbGenerator;
            if (gnt == null)
                gnt = this.PluginGenerator;
            if (gnt == null)
                return;

            var sln = this.ParentAppProject.ParentAppSolution;
            bool is_only_in_sln = true;
            foreach (var t in sln.ListAppProjects)
            {
                bool is_only_in_prj = true;
                foreach (var tt in t.ListAppProjectGenerators)
                {
                    if (tt.Guid == this.Guid)
                        continue;
                    IvPluginGenerator? gn = tt.PluginDbGenerator;
                    if (gn == null)
                        gn = tt.PluginGenerator;
                    if (gn == null)
                        continue;
                    if (gn.SolutionParametersGuid == gnt.SolutionParametersGuid)
                    {
                        is_only_in_sln = false;
                    }
                    if (gn.ProjectParametersGuid == gnt.ProjectParametersGuid)
                    {
                        is_only_in_prj = false;
                    }
                }
                if (is_only_in_prj)
                {
                    if (t.DicPluginsGroupSettings.ContainsKey(gnt.ProjectParametersGuid))
                        t.DicPluginsGroupSettings.Remove(gnt.ProjectParametersGuid);
                }
            }
            if (is_only_in_sln)
            {
                if (sln.DicPluginsGroupSettings.ContainsKey(gnt.SolutionParametersGuid))
                    sln.DicPluginsGroupSettings.Remove(gnt.SolutionParametersGuid);
            }
        }

        partial void OnPluginGuidChanging(ref string to)
        {
            Debug.Assert(cfg != null);
            DebugExt.WriteLineWithStack($"to='{to}'");
            cfg._DicActiveAppProjectGenerators.TryRemove(this.Guid);
            if (!string.IsNullOrEmpty(this.PluginGuid))
            {
                this.PluginGeneratorGuid = String.Empty;
            }
            cfg.Model._DicGenNodeSettings.TryRemove(this.Guid);
            //this.RemoveGroupSettingsIfLast();
        }
        partial void OnPluginGuidChanged()
        {
            DebugExt.WriteLineWithStack();
            this.PluginGeneratorGuid = string.Empty;
            this.GenFileName = string.Empty;
            if (string.IsNullOrEmpty(this.PluginGuid))
            {
                this.plugin = null;
            }
            else
            {
                Debug.Assert(cfg != null);
                Debug.Assert(cfg.DicPlugins != null);
                this.plugin = cfg.DicPlugins[this.PluginGuid];
            }

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

            this.NotifyPropertyChanged(nameof(this.PropertyDefinitions));

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
            Debug.Assert(this.ListGenerators != null);
            if (this.ListGenerators.Count == 1)
            {
                this.PluginGeneratorGuid = this.ListGenerators[0].Guid;
            }
        }
        partial void OnPluginGeneratorGuidChanging(ref string to)
        {
            Debug.Assert(cfg != null);
            DebugExt.WriteLineWithStack($"call parameters to='{to}'");
            if (cfg._DicActiveAppProjectGenerators.ContainsKey(this.Guid))
                cfg._DicActiveAppProjectGenerators.Remove(this.Guid);
            var nv = new ModelVisitorNodeGenSettings();
            nv.NodeGenSettingsApplyAction(cfg, (p) =>
            {
                p.RemoveNodeAppGenSettings(this.Guid);
            });
            this.RemoveGroupSettingsIfLast();
            this.GeneratorSettings = string.Empty;
            this.DescriptionGenerator = string.Empty;
            //this.DicGenNodeSettings.TryRemove(this.Guid);
            //this.PluginGeneratorGuid = String.Empty;
            cfg.Model._DicGenNodeSettings.TryRemove(this.Guid);
            this.DynamicGeneratorSettings = null;
            this.DynamicMainConnStrSettings = null;
            this.DynamicModelNodeSettings = null;
        }
        partial void OnPluginGeneratorGuidChanged()
        {
            DebugExt.WriteLineWithStack();
            if (string.IsNullOrWhiteSpace(this.PluginGeneratorGuid))
                return;
            var nv = new ModelVisitorNodeGenSettings();
            Debug.Assert(this.PluginGenerator != null);
            Debug.Assert(cfg != null);
            cfg._DicActiveAppProjectGenerators[this.Guid] = this.PluginGenerator;
            this.RestoreSettings();
            (this.ParentAppProject as AppProject).RestoreGroupSettings(this.PluginGenerator);
            (this.ParentAppProject.ParentAppSolution as AppSolution).RestoreGroupSettings(this.PluginGenerator);
            nv.NodeGenSettingsApplyAction(cfg, (p) =>
            {
                p.AddNodeAppGenSettings(this.Guid);
            });
            if (this.PluginDbGenerator != null)
            {
                this._GenFileName = "app-settings.json";
                this._RelativePathToGenFolder = string.Empty;
                if (this.Plugin!.ListGenerators.Count == 1)
                    this.Name = this.Plugin!.Name;
                else
                    this.Name = $"{this.Plugin!.Name}-{this.PluginDbGenerator.Name}";
                this.DescriptionGenerator = this.PluginDbGenerator.Description;
            }
            else
            {
                if (this.PluginGenerator != null)
                {
                    if (this.Plugin!.ListGenerators.Count == 1)
                        this.Name = this.Plugin!.Name;
                    else
                        this.Name = $"{this.Plugin!.Name}-{this.PluginGenerator.Name}";
                    this.DescriptionGenerator = this.PluginGenerator.Description;
                }
                this._GenFileName = prevGenFileName;
                this._RelativePathToGenFolder = prevRelativePathToGenFolder;
            }
            this.NotifyPropertyChanged(nameof(this.PropertyDefinitions));
            this.NotifyPropertyChanged(nameof(this.IconName));
            this.NotifyPropertyChanged(nameof(this.DynamicModelNodeSettings));
            this.NotifyPropertyChanged(nameof(this.DynamicGeneratorSettings));
            this.NotifyPropertyChanged(nameof(this.DynamicMainConnStrSettings));
        }
        partial void OnIsGenerateSqlSqriptToUpdatePrevStableChanged()
        {
            this.NotifyPropertyChanged(nameof(this.PropertyDefinitions));
        }
        protected override void OnValidated(ValidationResult res)
        {
            DebugExt.WriteLineWithStack();
            if (this.PluginGeneratorSettings == null)
                return;
            var res2 = this.PluginGeneratorSettings.ValidateSettings();
            if (res2 != null)
            {
                foreach (var tt in res2.Errors)
                {
                    tt.PropertyName = nameof(this.DynamicGeneratorSettings);
                }
                res.Errors.AddRange(res2.Errors);
            }
        }
        IvPluginGeneratorSettings? PluginGeneratorSettings { get; set; }
        public void RestoreSettings()
        {
            DebugExt.WriteLineWithStack();
            if (this.PluginGenerator != null)
            {
                if (string.IsNullOrWhiteSpace(this.GeneratorSettings))
                {
                    this.PluginGeneratorSettings = this.PluginGenerator.GetAppGenerationSettingsVmFromJson(this, null);
                }
                else
                {
                    this.PluginGeneratorSettings = this.PluginGenerator.GetAppGenerationSettingsVmFromJson(this, this.GeneratorSettings);
                }
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
            this.NotifyPropertyChanged(nameof(this.PropertyDefinitions));
        }
        public void SaveSettings()
        {
            DebugExt.WriteLineWithStack();
            if (this.DynamicGeneratorSettings != null)
                this.GeneratorSettings = this.DynamicGeneratorSettings.SettingsAsJson;
            else
                this.GeneratorSettings = string.Empty;
        }
        public void UpdateListGenerators()
        {
            Debug.Assert(cfg != null);
            DebugExt.WriteLineWithStack();
            if (cfg.IsInitialized && !string.IsNullOrWhiteSpace(this.PluginGuid))
            {
                var plg = (Plugin)cfg.DicNodes[this.PluginGuid];
                Debug.Assert(this.ListGenerators != null);
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
            DebugExt.WriteLineWithStack();
            var lst = new List<string>();
            if (this.PluginGenerator == null)
            {
                lst.Add(nameof(this.GenScriptFileName));
                lst.Add(nameof(this.ConnStrToPrevStable));
                lst.Add(nameof(this.ConnStr));
                lst.Add(nameof(this.RelativePathToGenFolder));
                lst.Add(nameof(this.GenFileName));
                lst.Add(nameof(this.ListGenerators));
                lst.Add(nameof(this.ListInModels));
                lst.Add(nameof(this.DynamicGeneratorSettings));
                lst.Add(nameof(this.DynamicMainConnStrSettings));
                lst.Add(nameof(this.DynamicModelNodeSettings));
                lst.Add(nameof(this.NameUi));
                lst.Add(nameof(this.IsGenerateSqlSqriptToUpdatePrevStable));
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
                    lst.Add(nameof(this.GenFileName));
                    lst.Add(nameof(this.ListGenerators));
                    lst.Add(nameof(this.ListInModels));
                    lst.Add(nameof(this.RelativePathToGenFolder));
                    lst.Add(nameof(this.NameUi));
                }
                else
                {
                    lst.Add(nameof(this.GenScriptFileName));
                    lst.Add(nameof(this.GenFileName));
                    lst.Add(nameof(this.ListGenerators));
                    lst.Add(nameof(this.ListInModels));
                    lst.Add(nameof(this.RelativePathToGenFolder));
                    lst.Add(nameof(this.NameUi));
                }
            }
            else
            {
                lst.Add(nameof(this.DynamicMainConnStrSettings));
                lst.Add(nameof(this.ConnStr));
                lst.Add(nameof(this.NameUi));
                lst.Add(nameof(this.ListInModels));
                lst.Add(nameof(this.IsGenerateSqlSqriptToUpdatePrevStable));
                lst.Add(nameof(this.GenScriptFileName));
                lst.Add(nameof(this.ConnStrToPrevStable));
            }
            return lst.ToArray();
        }
        public string GetGenerationFilePath()
        {
            DebugExt.WriteLineWithStack();
            var path = this.GetGenerationFolderPath();
            path = Path.Combine(path, this.GenFileName);
            return path;
        }
        public string GetGenerationFolderPath()
        {
            Debug.Assert(cfg != null);
            DebugExt.WriteLineWithStack();
            if (string.IsNullOrEmpty(cfg.CurrentCfgFolderPath))
                return "";
            var path = this.ParentAppProject.GetProjectFolderPath();
            if (!string.IsNullOrWhiteSpace(this.RelativePathToGenFolder))
                path = Path.Combine(path, this.RelativePathToGenFolder);
            return path;
        }
        partial void OnRelativePathToGenFolderChanging(ref string to)
        {
            DebugExt.WriteLineWithStack();
            if (string.IsNullOrWhiteSpace(to))
                return;
            to = Path.GetFullPath(to);
        }
        partial void OnRelativePathToGenFolderChanged()
        {
            Debug.Assert(cfg != null);
            DebugExt.WriteLineWithStack();
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
            var prev = (AppProject?)this.ParentAppProject.ListAppProjectGenerators.GetPrev(this);
            if (prev == null)
                return;
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
            var next = (AppProject?)this.ParentAppProject.ListAppProjectGenerators.GetNext(this);
            if (next == null)
                return;
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
#if DEBUG
                if (!VmBindable.isUnitTests)
                {
#endif
                    var res = MessageBox.Show("You are deleting generator. Continue?", "Warning", System.Windows.MessageBoxButton.OKCancel);
                    if (res != System.Windows.MessageBoxResult.OK)
                        return;
#if DEBUG
                }
#endif
            }
            this.PluginGeneratorGuid = string.Empty;
            this.PluginGuid = string.Empty;
            this.ParentAppProject.ListAppProjectGenerators.Remove(this);
            this.Parent = null!;
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
            var node = new AppProjectGenerator(this.ParentAppProject);
            this.ParentAppProject.ListAppProjectGenerators.Add(node);
            this.GetUniqueName(Defaults.AppPrjGeneratorName, node, this.ParentAppProject.ListAppProjectGenerators);
            this.SetSelected(node);
            return node;
        }

        #endregion Tree operations
        public void Remove()
        {
            DebugExt.WriteLineWithStack();
            this.ParentAppProject.ListAppProjectGenerators.Remove(this);
        }
        public void Delete()
        {
            DebugExt.WriteLineWithStack();
            var res = MessageBox.Show("You are going remove generator. Continue?", "Warning", System.Windows.MessageBoxButton.OKCancel);
            if (res == System.Windows.MessageBoxResult.OK)
                this.Remove();
        }
    }
}
