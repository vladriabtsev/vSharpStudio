#define Async
#define nPARRALEL
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Diagnostics;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Google.Protobuf;
using GuiLabs.Undo;
//using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.std;
using vSharpStudio.Views;
using vSharpStudio.vm.ViewModels;
using Xceed.Wpf.Toolkit;

namespace vSharpStudio.ViewModels
{
    //TODO 2020-08-07 Version. Faild to get working
    // https://github.com/GitTools/GitVersion
    //TODO 2020-08-07 init Plugin tree node after clicking on emptyConfig
    //TODO 2020-08-07 Model node setting contains generator not selected in AppProjectGenerator
    public class MainPageVM : VmValidatableWithSeverity<MainPageVM, MainPageVMValidator>, IPartImportsSatisfiedNotification
    {
        public static MainPageVM Create(bool isLoadConfig, string? pluginsFolderPath = null, string? configFile = null)
        {
            MainPageVM vm = new MainPageVM(isLoadConfig);
            vm.Compose(pluginsFolderPath);
            vm.OnFormLoaded();
            return vm;
        }
        private ILogger? _logger;
        public Xceed.Wpf.Toolkit.PropertyGrid.PropertyGrid? propertyGrid;
        public ValidationListForSelectedNode? validationListForSelectedNode;
        public MainPageVM() : base(MainPageVMValidator.Validator)
        {
            _logger = Logger.CreateLogger<MainPageVM>();
            //_Config = new Config();
        }
        //public MainPageVM(ILogger<MainPageVM> logger) : this()
        //{
        //    _logger = logger;
        //}
        bool isLoadConfig;
        string? configFile;
        //public MainPageVM(bool isLoadConfig, Action<MainPageVM, IEnumerable<Lazy<IvPlugin, IDictionary<string, object>>>> onImportsSatisfied = null, string configFile = null)
        public MainPageVM(bool isLoadConfig, string? configFile = null) : this()
        {
            _logger?.LogDebug("Created with isLoadConfig={isLoadConfig}, configFile='{configFile}'".CallerInfo(),
                isLoadConfig, configFile);
            //this.onImportsSatisfied = onImportsSatisfied;
            this.isLoadConfig = isLoadConfig;
            this.configFile = configFile;
            //this.Config = new Config();
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                // Catalog c = new Catalog();
                // this.Model = new ConfigRoot();
                // this.Model.Catalogs.ListCatalogs.Add(c);
                return;
            }

            #region Git Version
            //var assemblyName = assembly.GetName().Name;
            //var gitVersionInformationType = assembly.GetType("GitVersionInformation");
            //var fields = gitVersionInformationType.GetFields();

            //foreach (var field in fields)
            //{
            //    Trace.WriteLine(string.Format("{0}: {1}", field.Name, field.GetValue(null)));
            //}

            //// The GitVersionInformation class generated from a F# project exposes properties
            //var properties = gitVersionInformationType.GetProperties();

            //foreach (var property in properties)
            //{
            //    Trace.WriteLine(string.Format("{0}: {1}", property.Name, property.GetGetMethod(true).Invoke(null, null)));
            //}
            #endregion Git Version
        }
        public void OnFormLoaded()
        {
            this.IsBusy = true;
            _logger?.LogDebug("*** Application is starting. ***".CallerInfo());
            if (File.Exists(USER_SETTINGS_FILE_PATH))
            {
                var user_settings = File.ReadAllBytes(USER_SETTINGS_FILE_PATH);
                var us = Proto.Config.proto_user_settings.Parser.ParseFrom(user_settings);
                this.UserSettings = UserSettings.ConvertToVM(us, new UserSettings());
                if (this.UserSettings.ListOpenConfigHistory.Count > 0 &&
                    !string.IsNullOrWhiteSpace(this.UserSettings.ListOpenConfigHistory[0].ConfigPath) &&
                    File.Exists(this.UserSettings.ListOpenConfigHistory[0].ConfigPath))
                    this.CurrentCfgFilePath = this.UserSettings.ListOpenConfigHistory[0].ConfigPath;
                else
                    this.UserSettings = new UserSettings();
            }
            else
            {
                this.UserSettings = new UserSettings();
            }
            this.UserSettings.OnOpenRecentConfig = p =>
            {
                Debug.Assert(this.Config != null);
                if (this.Config.IsHasChanged)
                {
#if DEBUG
                    if (!VmBindable.isUnitTests)
                    {
#endif
                        var res = MessageBox.Show("Changes will be lost. Continue?", "Warning", System.Windows.MessageBoxButton.OKCancel);
                        if (res != System.Windows.MessageBoxResult.OK)
                            return;
#if DEBUG
                    }
#endif
                }
                this.Config = this.LoadConfig(p.ConfigPath, string.Empty, true);
            };
            if (isLoadConfig)
            {
                if (configFile != null)
                {
                    _logger?.LogDebug("Load Configuration from file {ConfigFile}".CallerInfo(), configFile);
                    this.Config = this.LoadConfig(configFile, string.Empty, true);
                }
                else if (!string.IsNullOrEmpty(this.CurrentCfgFilePath) && File.Exists(this.CurrentCfgFilePath))
                {
                    _logger?.LogDebug("Load Configuration from standard file {ConfigFile}".CallerInfo(), CurrentCfgFilePath);
                    this.Config = this.LoadConfig(this.CurrentCfgFilePath, string.Empty, true);
                }
                else
                {
                    _logger?.LogDebug("Using empty Configuration".CallerInfo());
                }
            }
            else
            {
                _logger?.LogDebug("Using empty Configuration".CallerInfo());
            }
            this.IsBusy = false;
        }
        private Config LoadConfig(string file_path, string indent, bool isRoot = false)
        {
            if (!File.Exists(file_path))
            {
                var ex = new ArgumentException("Configuration data are not found in the file: " + file_path);
                _logger?.LogCritical(ex, "".CallerInfo());
                throw ex;
            }
            var protoarr = File.ReadAllBytes(file_path);
            this.pconfig_history = Proto.Config.proto_config_short_history.Parser.ParseFrom(protoarr);
            _logger?.LogDebug("ConvertToVM Main Config".CallerInfo());
            var config = Config.ConvertToVM(this.pconfig_history.CurrentConfig, new Config());
            var currFolder = Path.GetDirectoryName(this.CurrentCfgFilePath);
            config.CurrentCfgFolderPath = currFolder ?? String.Empty;
            config.PrevCurrentConfig = Config.ConvertToVM(this.pconfig_history.CurrentConfig, new Config());
            if (isRoot)
            {
                if (this.pconfig_history.PrevStableConfig != null)
                {
                    _logger?.LogDebug("ConvertToVM Prev Config".CallerInfo());
                    config.PrevStableConfig = Config.ConvertToVM(this.pconfig_history.PrevStableConfig, new Config());
                }
            }
            string ind2 = indent + "   ";
            foreach (var t in config.GroupConfigLinks.ListBaseConfigLinks.ToList())
            {
                _logger?.LogDebug("Load Base Config {Name} from {Path}".CallerInfo(), t.Name, t.RelativeConfigFilePath);
                t.ConfigBase = this.LoadConfig(Path.Combine(config.CurrentCfgFolderPath, t.RelativeConfigFilePath), ind2);
                t.Name = t.ConfigBase.Name;
            }
            this.CurrentCfgFilePath = file_path;
            config.IsInitialized = false;

            InitConfig(config);
            if (isRoot)
            {
                this.Config = config;
                this.VisibilityAndMessageInstructions();
            }
            if (config.PrevStableConfig != null)
                InitConfig((Config)config.PrevStableConfig);
            if (config.PrevCurrentConfig != null)
                InitConfig((Config)config.PrevCurrentConfig);
            return config;
        }
        public Proto.Config.proto_config_short_history? pconfig_history { get; private set; }
        public UserSettings? UserSettings { get; private set; }
        public static readonly string USER_SETTINGS_FILE_PATH = @".\.vSharpStudio.settings";
        public static readonly string DEFAULT_CFG_FILEName = "vSharpStudio.vcfg";
        public static string? CurrentCfgFolderPath { get; private set; }
        public string? CurrentCfgFilePath
        {
            get { return this._CurrentCfgFilePath; }
            private set
            {
                var path = value;
                if (string.IsNullOrWhiteSpace(path))
                {
                    this._CurrentCfgFilePath = path;
                }
                else
                {
                    if (!Path.GetFileName(path).ToLower().EndsWith(".vcfg"))
                        path = Path.Combine(path, DEFAULT_CFG_FILEName);
                    this._CurrentCfgFilePath = Path.GetFullPath(path);
                    if (this.Config != null)
                        this.Config.CurrentCfgFolderPath = Path.GetDirectoryName(this._CurrentCfgFilePath) + "\\";
                }
                this.NotifyPropertyChanged();
            }
        }
        private string? _CurrentCfgFilePath;
        public static Config? ConfigInstance;

        #region Plugins

        // https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-4.0/ff603380(v=vs.100) // debug
        // https://www.codeproject.com/Articles/376033/From-Zero-to-Proficient-with-MEF
        // https://docs.microsoft.com/en-us/dotnet/framework/mef/
        [ImportMany(typeof(IvPlugin))]
        public IEnumerable<Lazy<IvPlugin, IDictionary<string, object>>>? _plugins = null;
        public void OnImportsSatisfied()
        {
            Debug.Assert(this._plugins != null);
            _logger?.LogDebug("Loaded {Count} plugins".CallerInfo(), this._plugins.Count());
            if (this.onPluginsLoaded != null)
                this.onPluginsLoaded();
        }
        public void InitConfig(Config? cfg)
        {
            if (cfg == null)
                return;
            try
            {
                cfg.IsInitialized = true;
                // Restore plugins
                List<PluginRow> lstGens = new List<PluginRow>();
                //cfg.DicGroupSettingGenerators = new Dictionary<string, IvPluginGenerator>();
                cfg.DicPlugins = new Dictionary<string, IvPlugin>();
                // Generators
                cfg.DicGenerators = new DictionaryExt<string, IvPluginGenerator>(100, false, true,
                        (ki, v) => { }, (kr, v) => { }, () => { }
                );
                Debug.Assert(this._plugins != null);
                foreach (var t in this._plugins)
                {
                    Debug.Assert(!cfg.DicPlugins.ContainsKey(t.Value.Guid));
                    cfg.DicPlugins[t.Value.Guid] = t.Value;
                    //var groupSettings = t.Value.GetPluginGroupSolutionSettingsVmFromJson(null);
                    //if (!cfg.DicGroupSettings.ContainsKey(groupSettings.Guid))
                    //    cfg.DicGroupSettings[groupSettings.Guid] = t.Value;
                    Plugin? p = null;
                    bool is_found = false;
                    // attaching plugin
                    foreach (var tt in cfg.GroupPlugins.ListPlugins)
                    {
                        if (tt.Guid == t.Value.Guid) // && (string.IsNullOrWhiteSpace(tt.Version) || tt.Version == t.Value.Version))
                        {
                            var ttp = (Plugin)tt;
                            ttp.SetVPlugin(t.Value);
                            ttp.Name = t.Value.Name;
                            ttp.Description = t.Value.Description;
                            ttp.Version = t.Value.Version;
                            p = ttp;
                            is_found = true;
                            break;
                        }
                    }
                    if (!is_found)
                    {
                        p = new Plugin(cfg.GroupPlugins, t.Value);
                        cfg.GroupPlugins.ListPlugins.Add(p);
                    }
                    Debug.Assert(p != null);
                    // attaching plugin generators
                    foreach (var tt in t.Value.ListGenerators)
                    {
                        //if (!cfg.DicGroupSettingGenerators.ContainsKey(tt.GroupGeneratorsGuid))
                        //{
                        //    cfg.DicGroupSettingGenerators[tt.GroupGeneratorsGuid] = tt;
                        //}
                        Debug.Assert(!cfg.DicGenerators.ContainsKey(tt.Guid));
                        cfg.DicGenerators[tt.Guid] = tt;
                        PluginGenerator? pg = null;
                        is_found = false;
                        foreach (var ttt in p.ListGenerators)
                        {
                            if (ttt.Guid == tt.Guid)
                            {
                                ttt.SetGenerator(tt);
                                ttt.Name = tt.Name;
                                ttt.Description = tt.Description;
                                pg = ttt;
                                is_found = true;
                                break;
                            }
                        }
                        if (!is_found)
                        {
                            pg = new PluginGenerator(p, tt);
                            p.ListGenerators.Add(pg);
                        }
                        lstGens.Add(new PluginRow()
                        {
                            GeneratorType = tt.PluginGeneratorType,
                            Plugin = p,
                            PluginGenerator = pg
                        });
                    }
                    // remove not existing generators
                    for (int iii = p.ListGenerators.Count - 1; iii >= 0; iii--)
                    {
                        if (p.ListGenerators[iii].Generator == null)
                        {
                            p.ListGenerators.RemoveAt(iii);
                        }
                    }
                }
                var dic = new Dictionary<vPluginLayerTypeEnum, List<PluginRow>>();
                foreach (var t in Enum.GetValues(typeof(vPluginLayerTypeEnum)))
                {
                    vPluginLayerTypeEnum gt = (vPluginLayerTypeEnum)t;
                    List<PluginRow> lst = new List<PluginRow>();
                    foreach (var tt in lstGens)
                    {
                        if (tt.GeneratorType == gt)
                        {
                            lst.Add(tt);
                        }
                    }
                    dic[gt] = lst;
                }
                if (cfg.DicPluginLists != null)
                {
                    cfg.DicPluginLists.Clear();
                }
                cfg.DicPluginLists = dic;
                //foreach (var t in lstGens)
                //{
                //    cfg.DicGenerators[t.PluginGenerator.Guid] = t.PluginGenerator.Generator;
                //}
                cfg.RefillDicGenerators();
                // Restore dictionary of all current nodes
                var nvb = new ModelVisitorBase();
                nvb.Run(cfg, null, null, null, (p, n) =>
                {
                    cfg._DicNodes[n.Guid] = n;
                });
                // Restore Node Settings VM for all nodes, which are supporting INodeGenSettings
                var nv = new ModelVisitorNodeGenSettings();
                nv.NodeGenSettingsApplyAction(cfg, (p) =>
                {
                    p.RestoreNodeAppGenSettingsVm();
                });
                // Create Settings VM for all project generators
                foreach (var t in cfg.GroupAppSolutions.ListAppSolutions)
                {
                    foreach (var tt in t.ListAppProjects)
                    {
                        foreach (var ttt in tt.ListAppProjectGenerators)
                        {
                            ttt.UpdateListGenerators();
                            ttt.RestoreSettings();
                            if (ttt.PluginGenerator != null)
                                cfg._DicActiveAppProjectGenerators[ttt.Guid] = ttt.PluginGenerator;
                        }
                        // group plugins settings
                        tt.RestoreGroupSettings();
                    }
                    // group plugins settings
                    t.RestoreGroupSettings();
                }
            }
            catch (Exception ex)
            {
                _logger?.LogCritical(ex, "".CallerInfo());
                throw;
            }
        }
        public List<IvPluginDbGenerator>? ListDbDesignPlugins
        {
            get
            {
                return this._ListDbDesignPlugins;
            }
            set
            {
                this._ListDbDesignPlugins = value;
                this.NotifyPropertyChanged();
            }
        }
        public List<IvPluginDbGenerator>? _ListDbDesignPlugins;
        public PluginRow? SelectedDbDesignPlugin
        {
            get
            {
                return this._SelectedDbDesignPlugin;
            }
            set
            {
                this._SelectedDbDesignPlugin = value;
                this.NotifyPropertyChanged();
            }
        }
        private PluginRow? _SelectedDbDesignPlugin;
        public INotifyPropertyChanged? SelectedDbDesignPluginSettings
        {
            get
            {
                return this._SelectedDbDesignPluginSettings;
            }
            set
            {
                this._SelectedDbDesignPluginSettings = value;
                this.NotifyPropertyChanged();
            }
        }
        private INotifyPropertyChanged? _SelectedDbDesignPluginSettings;
        private void AgregateCatalogs(string dir, string search, AggregateCatalog catalog, bool isPluginsFolder=false)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                return;
            }
            var dirs = Directory.GetDirectories(dir);
            if (isPluginsFolder)
            {
                if (dirs.Count() == 0)
                {
#if DEBUG
                    if (!VmBindable.isUnitTests)
#endif
                        MessageBox.Show($"No plugin's folders are found in folder:\n{dir}", "Warning", System.Windows.MessageBoxButton.OK);
                }
            }
            foreach (var t in dirs)
            {
                try
                {
                    DirectoryCatalog dirCatalog = new DirectoryCatalog(t, search);
                    if (dirCatalog.Parts.Count() > 0)
                    {
                        catalog.Catalogs.Add(dirCatalog);
                    }
                    this.AgregateCatalogs(t, search, catalog);
                }
                catch (Exception ex)
                {
#if DEBUG
                    if (!VmBindable.isUnitTests)
#endif
                        MessageBox.Show($"Can't load generator plugin from folder:\n{t}\nException: {ex.Message}", "Error", System.Windows.MessageBoxButton.OK);
                }
            }
        }
        private Action? onPluginsLoaded = null;
        internal void Compose(string? pluginsFolderPath = null)
        {
            try
            {
                string folder = (pluginsFolderPath == null ? Directory.GetCurrentDirectory() : pluginsFolderPath) + "\\Plugins";
                _logger?.LogDebug("Loading plugins from folder: {folder}".CallerInfo(), folder);
                AggregateCatalog catalog = new AggregateCatalog();
                this.AgregateCatalogs(folder, "vPlugin*.dll", catalog, true);
                CompositionContainer container = new CompositionContainer(catalog, CompositionOptions.DisableSilentRejection);
                container.SatisfyImportsOnce(this);
                //foreach (var sln in this.Config.GroupAppSolutions.ListAppSolutions)
                //{
                //    foreach (var prj in sln.ListAppProjects)
                //    {
                //        foreach (var pg in prj.ListAppProjectGenerators)
                //        {
                //            var set = pg.DynamicGeneratorSettings;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                _logger?.LogCritical(ex, "".CallerInfo());
                throw;
            }
        }

        #endregion Plugins

        public Config Config
        {
            get
            {
                return this._Config;
            }
            set
            {
                this._Config = value;
                MainPageVM.ConfigInstance = value;
                Debug.Assert(this._Config != null);
                this.NotifyPropertyChanged();
                this.ValidateProperty();
                this._Config.CurrentCfgFolderPath = Path.GetDirectoryName(this._CurrentCfgFilePath) ?? String.Empty;
                this._Config.OnSelectedNodeChanged = () =>
                {
                    this.CommandAddNew.RaiseCanExecuteChanged();
                    this.CommandAddNewChild.RaiseCanExecuteChanged();
                    this.CommandAddClone.RaiseCanExecuteChanged();
                    this.CommandMoveDown.RaiseCanExecuteChanged();
                    this.CommandMoveUp.RaiseCanExecuteChanged();
                    this.CommandDelete.RaiseCanExecuteChanged();
                    this.CommandSelectionLeft.RaiseCanExecuteChanged();
                    this.CommandSelectionRight.RaiseCanExecuteChanged();
                    this.CommandSelectionDown.RaiseCanExecuteChanged();
                    this.CommandSelectionUp.RaiseCanExecuteChanged();
                    if (this._Config.SelectedNode != null && IsValidateSubTreeWhenModelChanged)
                        this._Config.ValidateSubTreeFromNode(this._Config.SelectedNode);
                };
            }
        }
        public bool IsValidateSubTreeWhenModelChanged = true;
        private Config _Config;

        #region Main
        public vCommand CommandNewConfig
        {
            get
            {
                return this._CommandNewConfig ?? (this._CommandNewConfig = vCommand.Create(
                    (o) => { this.NewConfig(); this.SaveAs((string)o); },
                    (o) => { return this.Config == null || !string.IsNullOrEmpty(this.Config.CurrentCfgFolderPath); }));
            }
        }
        private vCommand? _CommandNewConfig;
        internal void NewConfig()
        {
            if (this.Config != null && this.Config.IsHasChanged)
            {
#if DEBUG
                if (!VmBindable.isUnitTests)
                {
#endif
                    var res = MessageBox.Show("Changes will be lost. Continue?", "Warning", System.Windows.MessageBoxButton.OKCancel);
                    if (res != System.Windows.MessageBoxResult.OK)
                        return;
#if DEBUG
                }
#endif
            }
            this.CurrentCfgFilePath = null;
            this.Config = new Config();
            this.InitConfig(this.Config);
            this.VisibilityAndMessageInstructions();
        }
        public vCommand CommandOpenConfig
        {
            get
            {
                return this._CommandOpenConfig ?? (this._CommandOpenConfig = vCommand.Create(
                    (o) => { this.OpenConfig(); },
                    (o) => { return true; }));
            }
        }
        private vCommand? _CommandOpenConfig;
        internal void OpenConfig()
        {
            if (this.Config != null && this.Config.IsHasChanged)
            {
#if DEBUG
                if (!VmBindable.isUnitTests)
                {
#endif
                    var res = MessageBox.Show("Changes will be lost. Continue?", "Warning", System.Windows.MessageBoxButton.OKCancel);
                    if (res != System.Windows.MessageBoxResult.OK)
                        return;
#if DEBUG
                }
#endif
            }
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = ""; // Default file name
            dlg.DefaultExt = ".vcfg"; // Default file extension
            dlg.Filter = "Any file (.*)|*.*"; // Filter files by extension
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                this.Config = this.LoadConfig(dlg.FileName, string.Empty, true);
            }
        }
        //TODO saving is not appropriate operation because loosing information about deleted objects (DB has to be updated)
        public vCommand CommandConfigSave
        {
            get
            {
                return this._CommandConfigSave ?? (this._CommandConfigSave = vCommand.Create(
                    (o) => { this.Save(); },
                    (o) => { return this.Config != null && this.CurrentCfgFilePath != null; }));
            }
        }
        private vCommand? _CommandConfigSave;
        internal void SavePrepare()
        {
            this.Config.PluginSettingsToModel();
            this.Config.LastUpdated = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);
            var proto = Config.ConvertToProto(this._Config);
            if (this.pconfig_history == null)
            {
                this.pconfig_history = new Proto.Config.proto_config_short_history();
            }

            this.pconfig_history.CurrentConfig = proto;
        }
        internal void Save()
        {
            //TODO save and clean private ConnStr
            this.SavePrepare();
            Utils.TryCall(() =>
            {
                Debug.Assert(this.UserSettings != null);
                Debug.Assert(this.CurrentCfgFilePath != null);
                var folder = Path.GetDirectoryName(this.CurrentCfgFilePath);
                Debug.Assert(folder != null);
                Directory.CreateDirectory(folder);
                File.WriteAllBytes(this.CurrentCfgFilePath, this.pconfig_history.ToByteArray());
                UpdateUserSettingsSaveConfigs();
                ResetIsChangedBeforeSave();
                File.WriteAllBytes(USER_SETTINGS_FILE_PATH, UserSettings.ConvertToProto(this.UserSettings).ToByteArray());
            }, "Can't save configuration. File path: '" + CurrentCfgFilePath + "'");
            //TODO restore private ConnStr
            this.ConnectionStringSettingsSave();
#if DEBUG
            //var json = JsonFormatter.Default.Format(this.pconfig_history);
            //File.WriteAllText(this.CurrentCfgFilePath + ".json", json);
            //CompareSaved(json);
#endif
        }
        public vCommand CommandConfigSaveAs
        {
            get
            {
                return this._CommandConfigSaveAs ?? (this._CommandConfigSaveAs = vCommand.Create(
                    (o) => { this.SaveAs((string)o); },
                    (o) => { return this.Config != null; }));
            }
        }
        private vCommand? _CommandConfigSaveAs;
        internal void SaveAs(string? filePath = null)
        {
            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.Filter = "vConfig files (*.vcfg)|*.vcfg|All files (*.*)|*.*";
            openFileDialog.CheckFileExists = false;
            openFileDialog.CheckPathExists = true;
            if (!string.IsNullOrEmpty(this._FilePathSaveAs))
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(this._FilePathSaveAs);
            }
            // https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=netframework-4.8
            if (filePath != null || openFileDialog.ShowDialog() == true)
            {
                this.FilePathSaveAs = filePath == null ? openFileDialog.FileName : Path.GetFullPath(filePath);
                this.SavePrepare();
                Utils.TryCall(
                    () =>
                    {
                        this.CurrentCfgFilePath = this.FilePathSaveAs;
                        Debug.Assert(this.UserSettings != null);
                        Debug.Assert(this.CurrentCfgFilePath != null);
                        var folder = Path.GetDirectoryName(this.CurrentCfgFilePath);
                        Debug.Assert(folder != null);
                        Directory.CreateDirectory(folder);
                        File.WriteAllBytes(this.CurrentCfgFilePath, this.pconfig_history.ToByteArray());
                        UpdateUserSettingsSaveConfigs();
                        ResetIsChangedBeforeSave();
                        File.WriteAllBytes(USER_SETTINGS_FILE_PATH, UserSettings.ConvertToProto(this.UserSettings).ToByteArray());
                        this.VisibilityAndMessageInstructions();
                    }, "Can't save configuration. File path: '" + this.FilePathSaveAs + "'");

                // var json = JsonFormatter.Default.Format(Config.ConvertToProto(_Model));
                // File.WriteAllText(FilePathSaveAs, json);
#if DEBUG
                // CompareSaved(json);
#endif
            }
            this.CommandConfigCurrentUpdate.RaiseCanExecuteChanged();
        }
        private void ResetIsChangedBeforeSave()
        {
            var vis = new ModelVisitorBase();
            vis.Run(this.Config, null, null, null, (v, n) =>
            {
                n.IsChanged = false;
            });
            if (this.Config.IsHasChanged)
            {
                var sss = this.Config.IsHasChangedPath;
                //TODO fix it
                //Debug.Assert(!this.Config.IsHasChanged);
            }
            Debug.Assert(!this.Config.IsChanged);
        }
        private void UpdateUserSettingsSaveConfigs()
        {
            Debug.Assert(this.UserSettings != null);
            Debug.Assert(this.CurrentCfgFilePath != null);
            if (this.UserSettings.ListOpenConfigHistory.Count > 0)
            {
                // same file as last
                if (this.UserSettings.ListOpenConfigHistory[0].ConfigPath == this.CurrentCfgFilePath)
                    this.UserSettings.ListOpenConfigHistory[0].OpenedLastTimeOn = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);
                else
                {
                    // remove previous
                    for (int i = this.UserSettings.ListOpenConfigHistory.Count - 1; i > 0; i--)
                    {
                        if (this.UserSettings.ListOpenConfigHistory[i].ConfigPath == this.CurrentCfgFilePath)
                            this.UserSettings.ListOpenConfigHistory.RemoveAt(i);
                    }
                    var us = new UserSettingsOpenedConfig()
                    {
                        OpenedLastTimeOn = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow),
                        ConfigPath = this.CurrentCfgFilePath
                    };
                    // insert as first
                    this.UserSettings.ListOpenConfigHistory.Insert(0, us);
                }
            }
            else // first file
            {
                var us = new UserSettingsOpenedConfig()
                {
                    OpenedLastTimeOn = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow),
                    ConfigPath = this.CurrentCfgFilePath
                };
                this.UserSettings.ListOpenConfigHistory.Add(us);
            }
        }
        private void CompareSaved(string json)
        {
            var model = new Config();
            var pconfig_history = Proto.Config.proto_config_short_history.Parser.WithDiscardUnknownFields(true).ParseJson(json);
            Config.ConvertToVM(pconfig_history.CurrentConfig, model);
            // https://github.com/GregFinzer/Compare-Net-Objects
            KellermanSoftware.CompareNetObjects.CompareLogic compareLogic = new KellermanSoftware.CompareNetObjects.CompareLogic();
            compareLogic.Config.IgnoreProperty<Config>(x => x.DicNodes);
            compareLogic.Config.IgnoreProperty<Config>(x => x.DicGenerators);
            KellermanSoftware.CompareNetObjects.ComparisonResult result = compareLogic.Compare(this.Config, model);
            if (!result.AreEqual)
            {
                Console.WriteLine(result.DifferencesString);
                throw new Exception();
            }
        }
        public string? FilePathSaveAs
        {
            get
            {
                return this._FilePathSaveAs;
            }
            set
            {
                this._FilePathSaveAs = value;
                this.NotifyPropertyChanged();
                this.SaveToolTip = _saveBaseToolTip + " as " + this._FilePathSaveAs;
            }
        }
        private string? _FilePathSaveAs;
        public string SaveToolTip
        {
            get
            {
                return this._SaveToolTip;
            }
            set
            {
                this._SaveToolTip = value;
                this.NotifyPropertyChanged();
            }
        }
        private const string _saveBaseToolTip = "Ctrl-S - save config";
        private string _SaveToolTip = _saveBaseToolTip;
        public ProgressVM? ProgressVM
        {
            get
            {
                if (_ProgressVM == null)
                    _ProgressVM = new ProgressVM();
                return _ProgressVM;
            }
        }
        private ProgressVM? _ProgressVM;
        private CancellationTokenSource? cancellationTokenSource;
        public vCommand CommandConfigCurrentUpdate
        {
            get
            {
                if (this._CommandConfigCurrentUpdate == null)
                {
#if Async
                    this._CommandConfigCurrentUpdate = vCommand.CreateAsync(
                        async (o) =>
#else
                    this._CommandConfigCurrentUpdate = vCommand.Create(
                        (o) =>
#endif
                        {
                            Debug.Assert(this.ProgressVM != null);
                            this.ProgressVM.Start("Update Current Version Generated Projects", 0, "", 0);
                            TestTransformation? tst = o as TestTransformation;
                            bool isException = false;
                            try
                            {
                                //this.Config.PluginSettingsToModel();
                                this.cancellationTokenSource = new CancellationTokenSource();
                                CancellationToken cancellationToken = this.cancellationTokenSource.Token;
#if Async
                                var ex = await this.UpdateCurrentVersionAsync(cancellationToken, (p) => { this.ProgressVM.From(p); }, o);
#else
                                this.UpdateCurrentVersion(cancellationToken, (p) => { this.ProgressVM.From(p); }, o);
#endif
                                this.cancellationTokenSource = null;
                                if (ex != null)
                                    throw ex;
                            }
                            catch (CancellationException)
                            {
                                isException = true;
                            }
                            catch (Exception ex)
                            {
                                isException = true;
                                this.ProgressVM.Exception = ex;
                                if (tst == null)
#if DEBUG
                                    if (!VmBindable.isUnitTests)
#endif
                                        MessageBox.Show(this.ProgressVM.Exception.ToString(), "Error");
                            }
                            if (!isException)
                            {
                                //await this.CommandConfigSave.ExecuteAsync(null);
                                this.CommandConfigSave.Execute(null);
                            }
                            this.ProgressVM.End();
                        }, (o) => { return this.cancellationTokenSource == null && this.Config != null && this.Config.IsNeedCurrentUpdate && this.CurrentCfgFilePath != null; }
                    );
                }
                return this._CommandConfigCurrentUpdate;
            }
        }
        private vCommand? _CommandConfigCurrentUpdate;
#if PARRALEL
        public async Task GenerateCodeAsync(CancellationToken cancellationToken, IConfig diffConfig, bool isCurrentUpdate, bool isDeleteDb = false)
#else
        public void GenerateCode(CancellationToken cancellationToken, IConfig diffConfig, bool isCurrentUpdate, bool isDeleteDb = false)
#endif
        {
            //var dicGroupGuids = new Dictionary<string, string?>();
            foreach (var ts in this.Config.GroupAppSolutions.ListAppSolutions)
            {
                foreach (var tp in ts.ListAppProjects)
                {
                    foreach (var tpg in tp.ListAppProjectGenerators)
                    {
                        Debug.Assert(tpg.PluginGenerator != null);
                        Debug.Assert(tpg.ListGenerators != null);
                        //dicGroupGuids[tpg.PluginGenerator.GroupGeneratorsGuid] = null;
                        foreach (var tg in tpg.ListGenerators)
                        {
                            if (tg.Generator != null)
                            {
                                tg.Generator.Init();
                            }
                        }
                    }
                }
            }
            //IProperty.Config = diffConfig;
            var nvb = new ModelVisitorBase();
            nvb.Run(diffConfig, null, null, null, (p, n) => { if (n is Property pp) pp.Tag = null; });
            foreach (var ts in this.Config.GroupAppSolutions.ListAppSolutions)
            {
                if (ts.IsMarkedForDeletion)
                    continue;
                if (cancellationToken.IsCancellationRequested)
                    throw new CancellationException();
                foreach (var tp in ts.ListAppProjects)
                {
                    if (tp.IsMarkedForDeletion)
                        continue;
                    // app settings path, 
                    var dicAppSettings = new Dictionary<string, StringBuilder>();
                    foreach (var tpg in tp.ListAppProjectGenerators)
                    {
                        if (tpg.IsMarkedForDeletion)
                            continue;
                        Debug.Assert(tpg.ListGenerators != null);
                        foreach (var tg in tpg.ListGenerators)
                        {
                            if (tg.Guid != tpg.PluginGeneratorGuid)
                                continue;
#if PARRALEL
                                await Task.Run(() =>
#endif
                            {
                                if (tg.Generator != null)
                                {
                                    string? code = null;
                                    switch (tg.Generator.PluginGeneratorType)
                                    {
                                        case vPluginLayerTypeEnum.DbDesign:
                                            if (!(tg.Generator is IvPluginDbGenerator))
                                                throw new Exception("Generator type vPluginLayerTypeEnum.DbDesign has to have interface: " + typeof(IvPluginDbGenerator).Name);
                                            Debug.Assert(this.Config.CurrentCfgFolderPath != null);
                                            string outFileConn = CommonUtils.GetOuputFilePath(this.Config.CurrentCfgFolderPath, ts, tp, tpg, tpg.GenFileName);
                                            bool first = false;
                                            StringBuilder? sb = null;
                                            if (!dicAppSettings.ContainsKey(outFileConn))
                                            {
                                                first = true;
                                                sb = new StringBuilder();
                                                dicAppSettings[outFileConn] = sb;
                                                sb.AppendLine("{");
                                                sb.AppendLine("\t\"db_conns\": {");
                                            }
                                            else
                                                sb = dicAppSettings[outFileConn];
                                            if (!first)
                                                sb.AppendLine(",");
                                            sb.Append("\t\t\"");
                                            sb.Append(tpg.Name);
                                            sb.AppendLine("\": {");
                                            sb.Append("\t\t\t\"provider\": \"");
                                            Debug.Assert(tpg.PluginDbGenerator != null);
                                            sb.Append(tpg.PluginDbGenerator.ProviderName);
                                            sb.AppendLine("\",");
                                            sb.Append("\t\t\t\"connection_string\": \"");
                                            Debug.Assert(tpg.DynamicMainConnStrSettings != null);
                                            var cnstr = tpg.DynamicMainConnStrSettings.GenerateCode(this.Config, ts, tp, tpg);
                                            sb.Append(cnstr);
                                            sb.AppendLine("\"");
                                            sb.Append("\t\t}");
                                            code = sb.ToString();
                                            if (isDeleteDb)
                                            {
                                                tpg.PluginDbGenerator.EnsureDbDeleted(tpg.ConnStr);
                                            }
                                            tpg.PluginDbGenerator.UpdateToModel(tpg.ConnStr, diffConfig, ts, tp, tpg.Guid, EnumDbUpdateLevels.TryKeepAll, false);
                                            if (isCurrentUpdate)
                                            {
                                                if (tpg.IsGenerateSqlSqriptToUpdatePrevStable)
                                                {
                                                    //TODO generate Stable DB update SQL script
                                                    var sql = tpg.PluginDbGenerator.UpdateToModel(tpg.ConnStr, diffConfig, ts, tp, tpg.Guid, EnumDbUpdateLevels.TryKeepAll, true);
                                                    string outSqlFile = CommonUtils.GetOuputFilePath(this.Config.CurrentCfgFolderPath, ts, tp, tpg, tpg.GenScriptFileName);
                                                    // tg.GetRelativeToConfigDiskPath()
                                                    //Directory.CreateDirectory(Path.GetDirectoryName(this.CurrentCfgFilePath));
                                                    byte[] sqlBytes = Encoding.UTF8.GetBytes(code);
                                                    File.WriteAllBytes(outSqlFile, sqlBytes);
                                                }
                                            }
                                            else
                                            {
                                                //TODO create copy of DEV DB into Stable DB. Same name with version suffix
                                                //genConn.DbGenerator
                                            }
                                            break;
                                        default:
                                            if (!isCurrentUpdate)
#if PARRALEL
                                                    return;
#else
                                                continue;
#endif
                                            if (!(tg.Generator is IvPluginGenerator))
                                                throw new Exception("Default generator has to have interface: " + typeof(IvPluginGenerator).Name);
                                            Debug.Assert(tpg.DynamicGeneratorSettings != null);
                                            code = tpg.DynamicGeneratorSettings.GenerateCode(this.Config, ts, tp, tpg);
                                            //code = (tg.Generator as IvPluginGenerator) .GetAppGenerationSettingsVmFromJson(null).GenerateCode(this.Config);
                                            break;
                                    }
                                    if (!string.IsNullOrWhiteSpace(code))
                                    {
                                        Debug.Assert(this.Config.CurrentCfgFolderPath != null);
                                        string outFile = CommonUtils.GetOuputFilePath(this.Config.CurrentCfgFolderPath, ts, tp, tpg, tpg.GenFileName);
                                        // tg.GetRelativeToConfigDiskPath()
                                        //Directory.CreateDirectory(Path.GetDirectoryName(this.CurrentCfgFilePath));
                                        byte[] bytes = Encoding.UTF8.GetBytes(code);
                                        File.WriteAllBytes(outFile, bytes);
                                    }
                                }
                            }
#if PARRALEL
                            );
#endif
                        }
                    }
                    if (isCurrentUpdate)
                    {
                        foreach (var t in dicAppSettings)
                        {
                            var sb = t.Value;
                            sb.AppendLine("");
                            sb.AppendLine("\t}");
                            sb.AppendLine("}");
                            byte[] bytes = Encoding.UTF8.GetBytes(sb.ToString());
                            File.WriteAllBytes(t.Key, bytes);
                        }
                    }
                }
            }
        }
        // https://docs.microsoft.com/en-us/archive/msdn-magazine/2013/march/async-await-best-practices-in-asynchronous-programming
#if Async
        private async Task<Exception?> UpdateCurrentVersionAsync(CancellationToken cancellationToken, Action<ProgressVM> onProgress, object? parm = null, bool askWarning = true)
#else
        private void UpdateCurrentVersion(CancellationToken cancellationToken, Action<ProgressVM> onProgress, object parm = null, bool askWarning = true)
#endif
        {
            Exception? resEx = null;
            ProgressVM progress = new ProgressVM();
            progress.Progress = 0;
            TestTransformation? tst = parm as TestTransformation;
            try
            {
                GuiLabs.Undo.ActionManager am = new GuiLabs.Undo.ActionManager();
                var dicRenamed = new Dictionary<string, string?>();
                var mvr = new ModelVisitorNodeReferencesBase();
                mvr.Run(this.Config, null, null, (m, n) =>
                {
                    if (!dicRenamed.ContainsKey(n.Guid) && n.IsRenamed(false))
                    {
                        dicRenamed[n.Guid] = null;
                    }
                });
                #region Remove New which are Marked for Deletion
                var lst = new List<string>();
                // delete from current model
                var vis1 = new ModelVisitorBase();
                vis1.Run(this.Config, null, null, null, (v, n) =>
                {
                    if (n is IEditableNode p)
                    {
                        if (p.IsMarkedForDeletion && p.IsNew)
                        {
                            lst.Add(n.Guid);
                        }
                    }
                });
                foreach (var tguid in lst)
                {
                    var n = this.Config.DicNodes[tguid];
                    var p = (IEditableNode)n;
                    p.Remove();
                }
                #endregion Remove New which are Marked for Deletion
                using (Transaction.Create(am))
                {
                    // I. Model validation (no need for UNDO)
                    #region
                    progress.SubName = "Model validation";
                    Debug.Assert(this._Config != null);
                    //#if Async
                    //                    await this._Config.ValidateSubTreeFromNodeAsync(this._Config);
                    //#else
                    this._Config.ValidateSubTreeFromNode(this._Config);
                    //#endif
                    if (this._Config.CountErrors > 0)
                        throw new Exception($"There are {this._Config.CountErrors} errors in configuration. Fix errors and try again.");
                    if (tst == null && this._Config.CountWarnings > 0)
                    {
#if DEBUG
                        if (!VmBindable.isUnitTests)
                        {
#endif
                            var res = MessageBox.Show("There are warnings in the config model. Continue?", "Warning", System.Windows.MessageBoxButton.OKCancel);
                            if (res != System.Windows.MessageBoxResult.OK)
                                return resEx;
#if DEBUG
                        }
#endif
                    }

                    // unit test
                    if (tst != null && tst.IsThrowExceptionOnConfigValidated)
                        throw new Exception(nameof(tst.IsThrowExceptionOnConfigValidated));
                    progress.Progress = 5;
                    progress.SubProgress = 100;
                    onProgress(progress);
                    #endregion

                    // II. Rename analysis
                    #region
                    bool isNeedRenames = false;
                    foreach (var ts in this.Config.GroupAppSolutions.ListAppSolutions)
                    {
                        foreach (var tp in ts.ListAppProjects)
                        {
                            foreach (var tg in tp.ListAppProjectGenerators)
                            {
                                if (cancellationToken.IsCancellationRequested)
                                    throw new CancellationException();
                                Debug.Assert(this._Config.DicGenerators != null);
                                var gg = this._Config.DicGenerators[tg.PluginGeneratorGuid];
                                if (!(gg is IvPluginCodeGenerator))
                                    continue;
                                var generator = (IvPluginCodeGenerator)gg;
                                List<PreRenameData> lstRenames = generator.GetListPreRename(this.Config, dicRenamed);
                                if (lstRenames.Count == 0)
                                    continue;
                                isNeedRenames = true;
                                break;
                            }
                            if (isNeedRenames)
                                break;
                        }
                        if (isNeedRenames)
                            break;
                    }
                    #endregion

                    // III. Build all solutions. Exception if not compilible (no need for UNDO)
                    #region
                    if (isNeedRenames)
                    {
                        progress.SubName = "Check current code compilation";
                        onProgress(progress);

                        int i = 0;
                        foreach (var ts in this.Config.GroupAppSolutions.ListAppSolutions)
                        {
                            if (cancellationToken.IsCancellationRequested)
                                throw new CancellationException();
                            i++;

#if Async
                            await CompileUtils.CompileAsync(_logger, ts.GetCombinedPath(ts.RelativeAppSolutionPath), cancellationToken);
#else
                            CompileUtils.Compile(_logger, ts.GetCombinedPath(ts.RelativeAppSolutionPath), cancellationToken);
#endif

                            progress.SubProgress = 100 * i / this.Config.GroupAppSolutions.ListAppSolutions.Count;
                            onProgress(progress);
                        }
                    }
                    // unit test
                    if (tst != null && tst.IsThrowExceptionOnBuildValidated)
                        throw new Exception(nameof(tst.IsThrowExceptionOnBuildValidated));
                    #endregion

                    // IV. Rename objects and properties by solution (code can be not compilible after that) (need UNDO from zip code backup)
                    #region
                    foreach (var ts in this.Config.GroupAppSolutions.ListAppSolutions)
                    {
                        foreach (var tp in ts.ListAppProjects)
                        {
                            foreach (var tg in tp.ListAppProjectGenerators)
                            {
                                if (cancellationToken.IsCancellationRequested)
                                    throw new CancellationException();
                                Debug.Assert(this._Config.DicGenerators != null);
                                var gg = this._Config.DicGenerators[tg.PluginGeneratorGuid];
                                if (!(gg is IvPluginCodeGenerator))
                                    continue;
                                var generator = (IvPluginCodeGenerator)gg;
                                List<PreRenameData> lstRenames = generator.GetListPreRename(this.Config, dicRenamed);
                                if (lstRenames.Count == 0)
                                    continue;
                                await CompileUtils.RenameAsync(_logger, ts.GetCombinedPath(ts.RelativeAppSolutionPath),
                                    ts.GetCombinedPath(tp.RelativeAppProjectPath), lstRenames, cancellationToken);
                            }
                        }
                    }
                    // unit test
                    if (tst != null && tst.IsThrowExceptionOnRenamed)
                        throw new Exception(nameof(tst.IsThrowExceptionOnRenamed));
                    #endregion

                    // V. Apply new DB schema to currentDB (no need for UNDO ???) Move into VI step
                    #region
                    //foreach (var ts in this.Config.GroupAppSolutions.ListAppSolutions)
                    //{
                    //    if (cancellationToken.IsCancellationRequested)
                    //        throw new CancellationException();
                    //    foreach (var tp in ts.ListAppProjects)
                    //    {
                    //        // generate db
                    //    }
                    //}
                    // unit test
                    if (tst != null && tst.IsThrowExceptionOnDbMigrated)
                        throw new Exception(nameof(tst.IsThrowExceptionOnDbMigrated));
                    #endregion

                    // VI. Generate code (no need for UNDO)
                    #region
#if PARRALEL
                    await this.GenerateCodeAsync(cancellationToken, this.Config, true);
#else
                    this.GenerateCode(cancellationToken, this.Config, true);
#endif
                    this.Config.SetIsNeedCurrentUpdate(false);
                    var vis = new ModelVisitorRemoveMarkedIfNewObjects();
                    vis.Run(this.Config, null, null, null);
                    this.Save();
                    // unit test
                    if (tst != null && tst.IsThrowExceptionOnCodeGenerated)
                        throw new Exception();
                    #endregion

                    // VII. Update history CurrentConfig (need UNDO)
                    #region
#if Async
#else
#endif
                    var update_history = new CallMethodAction(
                      () =>
                      {
                          this.Save();
                          var proto = Config.ConvertToProto(this.Config);
                          this.Config.PrevCurrentConfig = Config.ConvertToVM(proto, new Config());
                          this.InitConfig((Config)this.Config.PrevCurrentConfig);
                          // unit test
                          if (tst != null && tst.IsThrowExceptionOnConfigUpdated)
                              throw new Exception();
                      },
                      () =>
                      {
                      });
#if Async
#else
#endif
                    am.Execute(update_history);
                    #endregion

                    // VIII. Generate Update SQL for previous stable DB
                    //TODO Generate Update SQL for previous stable DB
                }
            }
            catch (Exception ex)
            {
                resEx = ex;
            }
            finally
            {
                //TODO roll back if Exception
            }
            return resEx;
        }
        public vCommand CommandConfigCreateStableVersion
        {
            get
            {
                return this._CommandConfigCreateStableVersion ?? (this._CommandConfigCreateStableVersion = vCommand.Create(
                (o) => { this.CreateStableVersion(); },
                (o) => { return this.Config != null; }));
            }
        }
        private vCommand? _CommandConfigCreateStableVersion;
        private void CreateStableVersion()
        {
            if (this.pconfig_history == null)
            {
                var ex = new NotSupportedException();
                _logger?.LogCritical(ex, "".CallerInfo());
                throw ex;
            }
            if (this.Config.IsHasChanged)
            {
                string mes = "Can't create stable version when Config has changes";
                var ex = new NotSupportedException(mes);
                _logger?.LogCritical(ex, mes.CallerInfo());
                throw ex;
            }
            if (this.Config.IsNeedCurrentUpdate)
            {
                string mes = "Can't create stable version without CURRENT UPDATE";
                var ex = new NotSupportedException(mes);
                _logger?.LogCritical(ex, mes.CallerInfo());
                throw ex;
            }

            this.CommandConfigSave.Execute(null);

            var vis = new ModelVisitorBase();
            if (this.Config.PrevStableConfig != null)
            {
                #region Remove Deleted (was Deprecated)
                var lst = new List<string>();
                // delete from current model
                vis.Run(this.Config, null, null, null, (v, n) =>
                {
                    if (n is IEditableNode p)
                    {
                        if (p.IsMarkedForDeletion)
                        {
                            if (n.IsDeleted())
                            {
                                lst.Add(n.Guid);
                            }
                        }
                    }
                });
                foreach (var tguid in lst)
                {
                    var n = this.Config.DicNodes[tguid];
                    var p = (IEditableNode)n;
                    p.Remove();
                }
                #endregion Remove Deprecated
            }
            // todo check if model has DB connected changes. Return if not.
            // todo create migration code
            var proto = Config.ConvertToProto(this.Config);
            this.pconfig_history.CurrentConfig = proto;
            this.pconfig_history.PrevStableConfig = this.pconfig_history.CurrentConfig.Clone();
            this.Config.PrevStableConfig = Config.ConvertToVM(proto, new Config());
            this.InitConfig((Config)this.Config.PrevStableConfig);
            this.Config.PrevCurrentConfig = Config.ConvertToVM(proto, new Config());
            this.InitConfig((Config)this.Config.PrevCurrentConfig);
            this.pconfig_history.CurrentConfig.Version++;
            vis.Run(this.Config, null, null, null, (v, n) =>
            {
                if (n is IEditableNode p)
                {
                    p.IsNew = false;
                    p.IsChanged = false;
                }
            });
            this.Config.SetIsNeedCurrentUpdate(false);
            Utils.TryCall(
                () =>
                {
                    Debug.Assert(!string.IsNullOrWhiteSpace(CurrentCfgFilePath));
                    File.WriteAllBytes(CurrentCfgFilePath, this.pconfig_history.ToByteArray());
                }, "Can't save configuration. File path: '" + CurrentCfgFilePath + "'");
        }

        #endregion Main

        #region ConfigTree
        private void VisibilityAndMessageInstructions()
        {
            this.VisibilityConfig = System.Windows.Visibility.Visible;
            if (string.IsNullOrEmpty(this.Config.CurrentCfgFolderPath))
            {
                this.MessageInstructions = "Before start editing config, empty config has to be saved";
            }
            else
            {
                this.MessageInstructions = null;
            }
        }
        public string? MessageInstructions
        {
            set
            {
                _MessageInstructions = value;
                this.NotifyPropertyChanged();
            }
            get { return _MessageInstructions; }
        }
        private string? _MessageInstructions;
        public System.Windows.Visibility VisibilityConfig
        {
            set
            {
                _VisibilityConfig = value;
                this.NotifyPropertyChanged();
            }
            get { return _VisibilityConfig; }
        }
        private System.Windows.Visibility _VisibilityConfig = System.Windows.Visibility.Hidden;
        public vCommand CommandAddNew
        {
            get
            {
                return this._CommandAddNew ?? (this._CommandAddNew = vCommand.Create(
                (o) => { Utils.TryCall(() => { Debug.Assert(this.Config.SelectedNode != null); this.Config.SelectedNode.NodeAddNew(); }, "Add new node command"); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanAddNew(); }));
            }
        }
        private vCommand? _CommandAddNew;
        public vCommand CommandAddNewChild
        {
            get
            {
                return this._CommandAddNewChild ?? (this._CommandAddNewChild = vCommand.Create(
                (o) => { Utils.TryCall(() => { Debug.Assert(this.Config.SelectedNode != null); this.Config.SelectedNode.NodeAddNewSubNode(); }, "Add new sub node command"); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanAddNewSubNode(); }));
            }
        }
        private vCommand? _CommandAddNewChild;
        public vCommand CommandAddClone
        {
            get
            {
                return this._CommandAddClone ?? (this._CommandAddClone = vCommand.Create(
                (o) => { Debug.Assert(this.Config.SelectedNode != null); this.Config.SelectedNode.NodeAddClone(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanAddClone(); }));
            }
        }
        private vCommand? _CommandAddClone;
        public vCommand CommandMoveDown
        {
            get
            {
                return this._CommandMoveDown ?? (this._CommandMoveDown = vCommand.Create(
                (o) => { Debug.Assert(this.Config.SelectedNode != null); this.Config.SelectedNode.NodeMoveDown(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanMoveDown(); }));
            }
        }
        private vCommand? _CommandMoveDown;
        public vCommand CommandMoveUp
        {
            get
            {
                return this._CommandMoveUp ?? (this._CommandMoveUp = vCommand.Create(
                (o) => { Debug.Assert(this.Config.SelectedNode != null); this.Config.SelectedNode.NodeMoveUp(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanMoveUp(); }));
            }
        }
        private vCommand? _CommandMoveUp;
        public vCommand CommandDelete
        {
            get
            {
                return this._CommandDelete ?? (this._CommandDelete = vCommand.Create(
                (o) => { Debug.Assert(this.Config.SelectedNode != null); this.Config.SelectedNode.NodeMarkForDeletion(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanMarkForDeletion(); }));
            }
        }
        private vCommand? _CommandDelete;
        public vCommand CommandSelectionLeft
        {
            get
            {
                return this._CommandSelectionLeft ?? (this._CommandSelectionLeft = vCommand.Create(
                (o) => { Debug.Assert(this.Config.SelectedNode != null); this.Config.SelectedNode.NodeLeft(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanLeft(); }));
            }
        }
        private vCommand? _CommandSelectionLeft;
        public vCommand CommandSelectionRight
        {
            get
            {
                return this._CommandSelectionRight ?? (this._CommandSelectionRight = vCommand.Create(
                (o) => { Debug.Assert(this.Config.SelectedNode != null); this.Config.SelectedNode.NodeRight(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanRight(); }));
            }
        }
        private vCommand? _CommandSelectionRight;
        public vCommand CommandSelectionDown
        {
            get
            {
                return this._CommandSelectionDown ?? (this._CommandSelectionDown = vCommand.Create(
                (o) => { Debug.Assert(this.Config.SelectedNode != null); this.Config.SelectedNode.NodeDown(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanDown(); }));
            }
        }
        private vCommand? _CommandSelectionDown;
        public vCommand CommandSelectionUp
        {
            get
            {
                return this._CommandSelectionUp ?? (this._CommandSelectionUp = vCommand.Create(
                (o) => { Debug.Assert(this.Config.SelectedNode != null); this.Config.SelectedNode.NodeUp(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanUp(); }));
            }
        }
        private vCommand? _CommandSelectionUp;
        #endregion ConfigTree

        public vCommand CommandFromErrorToSelection
        {
            get
            {
                return this._CommandFromErrorToSelection ?? (this._CommandFromErrorToSelection = vCommand.Create(
                (o) =>
                {
                    if (o == null)
                    {
                        return;
                    }
                    this.Config.SelectedNode = (ITreeConfigNode)((ValidationMessage)o).Model;
                },
                (o) => { return this.Config != null; }));
            }
        }

        private vCommand? _CommandFromErrorToSelection;

        #region ConnectionString

        // https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configurationmanager?f1url=https%3A%2F%2Fmsdn.microsoft.com%2Fquery%2Fdev16.query%3FappId%3DDev16IDEF1%26l%3DEN-US%26k%3Dk(System.Configuration.ConfigurationManager);k(TargetFrameworkMoniker-.NETFramework,Version%3Dv4.7.2);k(DevLang-csharp)%26rd%3Dtrue&view=netframework-4.8
        // https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configuration?view=netframework-4.8
        internal ConnectionStringSettingsCollection? ConnectionStringSettings = null;

        private void ConnectionStringSettingsSave()
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var conns = configFile.ConnectionStrings.ConnectionStrings;
            conns.Clear();
            if (this.ConnectionStringSettings != null)
            {
                for (int i = 0; i < this.ConnectionStringSettings.Count; i++)
                {
                    conns.Add(this.ConnectionStringSettings[i]);
                }
            }

            // var settings = configFile.AppSettings.Settings;
            // if (settings[key] == null)
            // {
            //    settings.Add(key, value);
            // }
            // else
            // {
            //    settings[key].Value = value;
            // }
            Utils.TryCall(
                () =>
            {
                configFile.Save(ConfigurationSaveMode.Modified);
            }, "Error writing app settings");

            // ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }

        // string GetConnectionString(ref string connectionStringName, out string providerName)
        // {
        //    providerName = null;

        // string result = "";
        //    ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
        //    string configPath = this.PathToProjectWithConnectionString + @"\App.config";
        //    if (File.Exists(configPath))
        //    {
        //        configFile.ExeConfigFilename = configPath;
        //    }
        //    else
        //    {
        //        configPath = this.PathToProjectWithConnectionString + @"\Web.config";
        //        if (File.Exists(configPath))
        //        {
        //            configFile.ExeConfigFilename = configPath;
        //        }
        //    }
        //    if (string.IsNullOrEmpty(configFile.ExeConfigFilename))
        //        throw new ArgumentNullException("The project does not contain App.config or Web.config file.");
        // var config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
        //    var connSection = config.ConnectionStrings;

        // //if the connectionString is empty - which is the defauls
        //    //look for count-1 - this is the last connection string
        //    //and takes into account AppServices and LocalSqlServer
        //    if (string.IsNullOrEmpty(connectionStringName))
        //    {
        //        if (connSection.ConnectionStrings.Count > 1)
        //        {
        //            connectionStringName = connSection.ConnectionStrings[connSection.ConnectionStrings.Count - 1].Name;
        //            result = connSection.ConnectionStrings[connSection.ConnectionStrings.Count - 1].ConnectionString;
        //            providerName = connSection.ConnectionStrings[connSection.ConnectionStrings.Count - 1].ProviderName;
        //        }
        //    }
        //    else
        //    {
        //        try
        //        {
        //            result = connSection.ConnectionStrings[connectionStringName].ConnectionString;
        //            providerName = connSection.ConnectionStrings[connectionStringName].ProviderName;
        //        }
        //        catch
        //        {
        //            result = "There is no connection string name called '" + connectionStringName + "'";
        //        }
        //    }

        // //if (String.IsNullOrEmpty(providerName))
        //    //providerName="System.Data.SqlClient";

        // return result;
        // }
        // string GetConnectionString(out string providerName)
        // {
        //    providerName = null;

        // string result = "";
        //    ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
        //    string configPath = this.PathToProjectWithConnectionString + @"\App.config";
        //    if (File.Exists(configPath))
        //    {
        //        configFile.ExeConfigFilename = configPath;
        //    }
        //    else
        //    {
        //        configPath = this.PathToProjectWithConnectionString + @"\Web.config";
        //        if (File.Exists(configPath))
        //        {
        //            configFile.ExeConfigFilename = configPath;
        //        }
        //    }
        //    if (string.IsNullOrEmpty(configFile.ExeConfigFilename))
        //        throw new ArgumentNullException("The project does not contain App.config or Web.config file.");
        // var config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
        //    var connSection = config.ConnectionStrings;

        // try
        //    {
        //        result = connSection.ConnectionStrings[this.SelectedDbDesignPlugin.Name + "Admin"].ConnectionString;
        //        providerName = connSection.ConnectionStrings[this.SelectedDbDesignPlugin.Name + "Admin"].ProviderName;
        //    }
        //    catch
        //    {
        //        result = "There is no connection string name called '" + this.SelectedDbDesignPlugin.Name + "Admin" + "'";
        //    }

        // //if (String.IsNullOrEmpty(providerName))
        //    //providerName="System.Data.SqlClient";

        // return result;
        // }
        // public string PathToProjectWithConnectionString
        // {
        //    set
        //    {
        //        if (_PathToProjectWithConnectionString != value)
        //        {
        //            _PathToProjectWithConnectionString = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //    get { return _PathToProjectWithConnectionString; }
        // }
        // private string _PathToProjectWithConnectionString = "";
        // public string ConnectionString
        // {
        //    get { return _ConnectionString; }
        //    set
        //    {
        //        _ConnectionString = value;
        //        NotifyPropertyChanged();
        //    }
        // }
        // private string _ConnectionString;
        // void InitConnectionString()
        // {
        //    this.ConnectionString = GetConnectionString(out _providerName);
        //    // https://www.connectionstrings.com/sqlconnection/
        //    if (this.ConnectionString != null && this.ConnectionString.Contains("|DataDirectory|"))
        //    {
        //        //have to replace it
        //        string dataFilePath = this.PathToProjectWithConnectionString + "\\App_Data\\";
        //        this.ConnectionString = this.ConnectionString.Replace("|DataDirectory|", dataFilePath);
        //    }
        // }
        // public string ProviderName
        // {
        //    get
        //    {
        //        InitConnectionString();
        //        return _providerName;
        //    }
        // }
        // string _providerName = "";
        // public const string PROVIDERName_SQL = "System.Data.SqlClient";
        // public const string PROVIDERName_SQLITE = "Microsoft.Data.Sqlite";
        // public const string PROVIDERName_MYSQL = "MySql.Data";
        // public const string PROVIDERName_NPGSQL = "Npgsql";
        #endregion

        #region Utils
        public static string GetvSharpStudioPluginsPath()
        {
            string? path = null;
            var currPath = Directory.GetCurrentDirectory();
            var bin = currPath.Substring(currPath.IndexOf(@"\bin"));
            var s = @"\vSharpStudio";
            var vss = currPath.Substring(0, currPath.IndexOf(s));
            path = vss + s + s + bin;
            return path;
        }
        #endregion Utils
    }
}
