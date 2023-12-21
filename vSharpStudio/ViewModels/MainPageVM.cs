#define Async
#define nPARRALEL
//#if RELEASE && !PARRALEL
#if RELEASE && PARRALEL
Not tested yet
//Release version is expecting #define PARRALEL in previous lines of code
#endif
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Xml.Linq;
using AsyncAwaitBestPractices;
using CommunityToolkit.Diagnostics;
using Google.Protobuf;
using GuiLabs.Undo;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.Controls;
using vSharpStudio.std;
using vSharpStudio.Views;
using vSharpStudio.vm.ViewModels;
using Xceed.Wpf.Toolkit;

namespace vSharpStudio.ViewModels
{
    //TODO 2020-08-07 Version. Faild to get working
    // https://github.com/GitTools/GitVersion
    public class MainPageVM : VmValidatableWithSeverity<MainPageVM, MainPageVMValidator>, IPartImportsSatisfiedNotification
    {
        public static bool NotSaveUserSettings = false;
        public static MainPageVM Create(bool isLoadConfig, string? pluginsFolderPath = null, string? configFile = null)
        {
            // IsChanged in all classes is changed only if configuration objects properties are changed. Auto generated in code.
            VmBindable.IsModifyIsChangedExplicitly = true;
            MainPageVM vm = new MainPageVM(isLoadConfig, configFile);
            vm.Compose(pluginsFolderPath);
            vm.OnFormLoaded();
            if (!isLoadConfig)
            {
                vm._Config = new Config(true);
            }
            return vm;
        }
        private readonly ILogger? _logger;
        public Xceed.Wpf.Toolkit.PropertyGrid.PropertyGrid? propertyGrid;
        public ValidationListForSelectedNode? validationListForSelectedNode;
        public MainPageVM() : base(MainPageVMValidator.Validator)
        {
            VmBindable.IsChangedNotificationDelay = 200;
            _logger = Logger.CreateLogger<MainPageVM>();
            EditorGenSettingsDialog.GenSettingsDialogAction = (node, guid) =>
            {
                Debug.Assert(MainPageVM._mainPage != null);
                var vm = new GenSettingsVm(node, guid);
                var maxMargin = 10;
                var ctnr = MainPageVM._mainPage._windowContainer;
                MainPageVM._mainPage._modalNodeSettingsWindow.Top = maxMargin;
                MainPageVM._mainPage._modalNodeSettingsWindow.Height = ctnr.ActualHeight - 2 * maxMargin;
                var collPropertyGridWidth = GenSettingsVm.PropertyGridWidthStatic;
                MainPageVM._mainPage._modalNodeSettingsWindow.Width = Math.Min(ctnr.ActualWidth - 2 * maxMargin,
                    Math.Max(collPropertyGridWidth, vm.NotEmptyColumns * collPropertyGridWidth + (vm.NotEmptyColumns - 1) * 3)) + 6;
                MainPageVM._mainPage._modalNodeSettingsWindow.Left = (ctnr.ActualWidth - MainPageVM._mainPage._modalNodeSettingsWindow.Width) / 2;
                MainPageVM._mainPage._modalNodeSettingsWindow.Caption = $"Node settings for '{node.Name}' by generator '{this.Config.DicNodes[guid].Name}'";
                MainPageVM._mainPage._GenSettings.ViewVm = vm;
                MainPageVM._mainPage._modalNodeSettingsWindow.WindowState = WindowState.Open;
            };
            EditorPropertyGridDialog.PropertyGridDialogAction = (val) =>
            {
                Debug.Assert(MainPageVM._mainPage != null);
                var maxMargin = 10;
                var ctnr = MainPageVM._mainPage._windowContainer;
                MainPageVM._mainPage._modalBaseSettingsEditWindow.Top = maxMargin;
                MainPageVM._mainPage._modalBaseSettingsEditWindow.Height = ctnr.ActualHeight - 2 * maxMargin;
                MainPageVM._mainPage._modalBaseSettingsEditWindow.Width = Math.Min(ctnr.ActualWidth - 2 * maxMargin, 400);
                MainPageVM._mainPage._modalBaseSettingsEditWindow.Left = (ctnr.ActualWidth - MainPageVM._mainPage._modalBaseSettingsEditWindow.Width) / 2;
                //MainPageVM._mainPage._modalNodeSettingsWindow.Caption = $"Node settings for '{node.Name}' by generator '{this.Config.DicNodes[guid].Name}'";
                //MainPageVM._mainPage._modalBaseSettingsEditWindow.Caption = $"Settings";
                MainPageVM._mainPage._modalBaseSettingsEditWindow.DataContext = val;
                MainPageVM._mainPage._modalBaseSettingsEditWindow.WindowState = WindowState.Open;
            };

            //_Config = new Config();
        }
        //public MainPageVM(ILogger<MainPageVM> logger) : this()
        //{
        //    _logger = logger;
        //}
        private readonly bool isLoadConfig;
        private readonly string? configFile;
        //public MainPageVM(bool isLoadConfig, Action<MainPageVM, IEnumerable<Lazy<IvPlugin, IDictionary<string, object>>>> onImportsSatisfied = null, string configFile = null)
        internal static MainPage? _mainPage = null;
        public MainPageVM(bool isLoadConfig, string? configFile = null, MainPage? mainPage = null) : this()
        {
            _logger?.LogDebug("Created with isLoadConfig={isLoadConfig}, configFile='{configFile}'".CallerInfo(),
                isLoadConfig, configFile);
            MainPageVM._mainPage = mainPage;
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
            Debug.Assert(this.ProgressVM != null);
            this.ProgressVM.ProgressStart("Configuration Loading");
            _logger?.LogDebug("*** Application is starting. ***".CallerInfo());
            if (File.Exists(USER_SETTINGS_FILE_PATH))
            {
                var user_settings = File.ReadAllBytes(USER_SETTINGS_FILE_PATH);
                var us = Proto.Config.proto_user_settings.Parser.WithDiscardUnknownFields(true).ParseFrom(user_settings);
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
                        var res = Xceed.Wpf.Toolkit.MessageBox.Show("Changes will be lost. Continue?", "Warning", System.Windows.MessageBoxButton.OKCancel);
                        if (res != System.Windows.MessageBoxResult.OK)
                            return;
#if DEBUG
                    }
#endif
                }
                this.LoadConfig(p.ConfigPath, string.Empty, true);
            };
            if (isLoadConfig)
            {
                if (configFile != null)
                {
                    _logger?.LogDebug("Load Configuration from file {ConfigFile}".CallerInfo(), configFile);
                    this.LoadConfig(configFile, string.Empty, true);
                }
                else if (!string.IsNullOrEmpty(this.CurrentCfgFilePath) && File.Exists(this.CurrentCfgFilePath))
                {
                    _logger?.LogDebug("Load Configuration from standard file {ConfigFile}".CallerInfo(), this.CurrentCfgFilePath);
                    this.LoadConfig(this.CurrentCfgFilePath, string.Empty, true);
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
            this.ProgressVM.ProgressClose();
        }
        private const string emptyStr = "";
        private Config? LoadConfig(string file_path, string indent, bool isRoot = false)
        {
            if (!File.Exists(file_path) || Path.GetExtension(file_path) != ".vcfg")
            {
                var ex = new ArgumentException("Configuration data are not found in the file: " + file_path);
                _logger?.LogCritical(ex, emptyStr.CallerInfo());
                return null;
            }
            var protoarr = File.ReadAllBytes(file_path);
            this.pconfig_history = Proto.Config.proto_config_short_history.Parser.WithDiscardUnknownFields(true).ParseFrom(protoarr);
            _logger?.LogDebug("ConvertToVM Main Config".CallerInfo());
            var config = Config.ConvertToVM(this.pconfig_history.CurrentConfig, new Config(false));
            var currFolder = Path.GetDirectoryName(this.CurrentCfgFilePath);
            config.CurrentCfgFolderPath = currFolder ?? String.Empty;
            config.PrevCurrentConfig = Config.ConvertToVM(this.pconfig_history.CurrentConfig, new Config(false));
            if (isRoot)
            {
                if (this.pconfig_history.PrevStableConfig != null)
                {
                    _logger?.LogDebug("ConvertToVM Prev Config".CallerInfo());
                    config.PrevStableConfig = Config.ConvertToVM(this.pconfig_history.PrevStableConfig, new Config(false));
                }
                this.CurrentCfgFilePath = file_path;
            }
            string ind2 = indent + "   ";
            foreach (var t in config.GroupConfigLinks.ListBaseConfigLinks.ToList())
            {
                _logger?.LogDebug("Load Base Config {Name} from {Path}".CallerInfo(), t.Name, t.RelativeConfigFilePath);
                t.ConfigBase = this.LoadConfig(Path.Combine(config.CurrentCfgFolderPath, t.RelativeConfigFilePath), ind2);
                Debug.Assert(t.ConfigBase != null);
                t.Name = t.ConfigBase.Name;
            }
            config.IsInitialized = false;

            InitConfig(config);
            if (isRoot)
            {
                this.Config = config;
                this.VisibilityAndMessageInstructions();
                this.Config.RestoreIsHas();
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
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.CurrentCfgFilePathTitle));
            }
        }
        private string? _CurrentCfgFilePath;
        public string CurrentCfgFilePathTitle
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this._CurrentCfgFilePath))
                {
                    return "Configuration file is not selected yet. Create new one or open existing";
                }
                return this._CurrentCfgFilePath;
            }
        }
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
            this.onPluginsLoaded?.Invoke();
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
                cfg.DicPluginLists?.Clear();
                cfg.DicPluginLists = dic;
                //foreach (var t in lstGens)
                //{
                //    cfg.DicGenerators[t.PluginGenerator.Guid] = t.PluginGenerator.Generator;
                //}
                cfg.RefillDicGenerators();
                // Restore dictionary of all current nodes
                var nvb = new ModelVisitorBase();
                nvb.RunFromRoot(cfg, null, null, null, (p, n) =>
                {
                    cfg._DicNodes[n.Guid] = n;
                    if (n is IRoleAccess ra)
                        ra.InitRoles();
                });
                // Restore Node Settings VM for all nodes, which are supporting INodeGenSettings
                var nv = new ModelVisitorNodeGenSettings();
                nv.NodeGenSettingsApplyAction(cfg, (p) =>
                {
                    p.RestoreNodeAppGenSettingsVm();
                });
                // Plugin group model
                cfg.Model.RestorePluginGroupsModels();
                nvb.RunFromRoot(cfg, null, null, null, (p, n) =>
                {
                    n.OnConfigInitialized();
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
                _logger?.LogCritical(ex, emptyStr.CallerInfo());
                throw;
            }
#if DEBUG
            this.CheckShortIdUniqueness(cfg);
#endif
        }
        public List<IvPluginDbGenerator>? ListDbDesignPlugins
        {
            get
            {
                return this._ListDbDesignPlugins;
            }
            set
            {
                SetProperty(ref this._ListDbDesignPlugins, value);
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
                SetProperty(ref this._SelectedDbDesignPlugin, value);
            }
        }
        private PluginRow? _SelectedDbDesignPlugin;
        //public IOnPropertyChanged? SelectedDbDesignPluginSettings
        //{
        //    get
        //    {
        //        return this._SelectedDbDesignPluginSettings;
        //    }
        //    set
        //    {
        //        SetProperty(ref this._SelectedDbDesignPluginSettings, value);
        //    }
        //}
        //private IOnPropertyChanged? _SelectedDbDesignPluginSettings;
        private void AgregateCatalogs(string dir, string search, AggregateCatalog catalog, bool isPluginsFolder = false)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                return;
            }
            var dirs = Directory.GetDirectories(dir);
            if (isPluginsFolder)
            {
                if (dirs.Length == 0)
                {
#if DEBUG
                    if (!VmBindable.isUnitTests)
#endif
                        Xceed.Wpf.Toolkit.MessageBox.Show($"No plugin's folders are found in folder:\n{dir}", "Warning", System.Windows.MessageBoxButton.OK);
                }
            }
            foreach (var t in dirs)
            {
                try
                {
                    DirectoryCatalog dirCatalog = new DirectoryCatalog(t, search);
                    if (dirCatalog.Parts.Any())
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
                        Xceed.Wpf.Toolkit.MessageBox.Show($"Can't load generator plugin from folder:\n{t}\nException: {ex.Message}", "Error", System.Windows.MessageBoxButton.OK);
                }
            }
        }
        private readonly Action? onPluginsLoaded = null;
        internal void Compose(string? pluginsFolderPath = null)
        {
            try
            {
                string folder = (pluginsFolderPath ?? Directory.GetCurrentDirectory()) + "\\Plugins";
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
                _logger?.LogCritical(ex, emptyStr.CallerInfo());
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
                SetProperty(ref this._Config, value);
                MainPageVM.ConfigInstance = value;
                Debug.Assert(this._Config != null);
                this.ValidateProperty();
                this._Config.CurrentCfgFolderPath = Path.GetDirectoryName(this._CurrentCfgFilePath) ?? String.Empty;
                this.Config.OnSelectedNodeChanging = (oldValue, newValue) =>
                {
                    if (newValue != null)
                        ((VmBindable)newValue).OnOpeningEditor();
                };
                this._Config.OnSelectedNodeChanged = () =>
                {
                    this.BtnAddNew.Command.NotifyCanExecuteChanged();
                    //this.BtnAddNewChild.Command.NotifyCanExecuteChanged();
                    this.BtnAddClone.Command.NotifyCanExecuteChanged();
                    this.BtnMoveDown.Command.NotifyCanExecuteChanged();
                    this.BtnMoveUp.Command.NotifyCanExecuteChanged();
                    this.BtnDelete.Command.NotifyCanExecuteChanged();
                    this.BtnSelectionLeft.Command.NotifyCanExecuteChanged();
                    this.BtnSelectionRight.Command.NotifyCanExecuteChanged();
                    this.BtnSelectionDown.Command.NotifyCanExecuteChanged();
                    this.BtnSelectionUp.Command.NotifyCanExecuteChanged();
                    this.BtnConfigValidateAsync.Command.NotifyCanExecuteChanged();
                    this.BtnConfigCurrentUpdateAsync.Command.NotifyCanExecuteChanged();
                    this.BtnConfigCreateStableVersionAsync.Command.NotifyCanExecuteChanged();
                };
            }
        }
        private Config _Config;

        #region Main
        public vButtonVM BtnNewConfig
        {
            get
            {
                return this._BtnNewConfig ??= new vButtonVM(
                    () =>
                    {

                        this.NewConfig();
                        this.BtnConfigSave.Command.NotifyCanExecuteChanged();
                        this.BtnConfigSaveAs.Command.NotifyCanExecuteChanged();
                        this.BtnConfigCurrentUpdateAsync.Command.NotifyCanExecuteChanged();
                    },
                    () => { return this.Config == null || !string.IsNullOrEmpty(this.Config.CurrentCfgFolderPath); });
            }
        }
        private vButtonVM? _BtnNewConfig;
        internal void NewConfig()
        {
            if (this.Config != null && this.Config.IsHasChanged)
            {
#if DEBUG
                if (!VmBindable.isUnitTests)
                {
#endif
                    var res = Xceed.Wpf.Toolkit.MessageBox.Show("Changes will be lost. Continue?", "Warning", System.Windows.MessageBoxButton.OKCancel);
                    if (res != System.Windows.MessageBoxResult.OK)
                        return;
#if DEBUG
                }
#endif
            }
            this.CurrentCfgFilePath = null;
            this.Config = new Config(true);
            this.InitConfig(this.Config);
            this.VisibilityAndMessageInstructions();
        }
        public vButtonVM<string> BtnOpenConfig
        {
            get
            {
                return this._BtnOpenConfig ??= new vButtonVM<string>(
                    (o) =>
                    {
                        this.OpenConfig(o);
                        this.BtnConfigSave.Command.NotifyCanExecuteChanged();
                        this.BtnConfigSaveAs.Command.NotifyCanExecuteChanged();
                        this.BtnConfigCurrentUpdateAsync.Command.NotifyCanExecuteChanged();
                    },
                    (o) => { return true; });
            }
        }
        private vButtonVM<string>? _BtnOpenConfig;
        internal void OpenConfig(string? filePath)
        {
            if (this.Config != null && this.Config.IsHasChanged)
            {
#if DEBUG
                if (!VmBindable.isUnitTests)
                {
#endif
                    var res = Xceed.Wpf.Toolkit.MessageBox.Show("Changes will be lost. Continue?", "Warning", System.Windows.MessageBoxButton.OKCancel);
                    if (res != System.Windows.MessageBoxResult.OK)
                        return;
#if DEBUG
                }
#endif
            }
            if (filePath != null)
            {
                if (File.Exists(filePath))
                    this.LoadConfig(filePath, string.Empty, true);
                else
                    Xceed.Wpf.Toolkit.MessageBox.Show("Selected file is not found", "Warning", System.Windows.MessageBoxButton.OK);
                return;
            }
            else
            {
                var dlg = new Microsoft.Win32.OpenFileDialog
                {
                    FileName = emptyStr, // Default file name
                    DefaultExt = ".vcfg", // Default file extension
                    Filter = "Any file (.vcfg)|*.vcfg" // Filter files by extension
                };
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    this.LoadConfig(dlg.FileName, string.Empty, true);
                }
            }
        }
        //TODO saving is not appropriate operation because loosing information about deleted objects (DB has to be updated)
        public vButtonVM BtnConfigSave
        {
            get
            {
                return this._BtnConfigSave ??= new vButtonVM(
                    () => { this.Save(); },
                    () => { return this.Config != null && this.CurrentCfgFilePath != null; });
            }
        }
        private vButtonVM? _BtnConfigSave;
#if DEBUG
        private void CheckShortIdUniqueness(Config cfg)
        {
            var hash = new HashSet<int>();
            // Constant groups
            foreach (var t in cfg.Model.GroupConstantGroups.ListConstantGroups)
            {
                if (hash.Contains(t.ShortId))
                    throw new NotSupportedException();
                if (t.ShortId == 0)
                    throw new NotSupportedException();
                hash.Add(t.ShortId);
            }
            // Constants
            hash.Clear();
            foreach (var tt in cfg.Model.GroupConstantGroups.ListConstantGroups)
            {
                foreach (var t in tt.ListConstants)
                {
                    if (hash.Contains(t.ShortId))
                        throw new NotSupportedException();
                    if (t.ShortId == 0)
                        throw new NotSupportedException();
                    hash.Add(t.ShortId);
                }
            }
            // Catalogs
            hash.Clear();
            foreach (var t in cfg.Model.GroupCatalogs.ListCatalogs)
            {
                if (hash.Contains(t.ShortId))
                    throw new NotSupportedException();
                if (t.ShortId == 0)
                    throw new NotSupportedException();
                hash.Add(t.ShortId);
            }
            // Documents
            hash.Clear();
            foreach (var t in cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments)
            {
                if (hash.Contains(t.ShortId))
                    throw new NotSupportedException();
                if (t.ShortId == 0)
                    throw new NotSupportedException();
                hash.Add(t.ShortId);
            }
            // Registers
            hash.Clear();
            foreach (var t in cfg.Model.GroupRegisters.ListRegisters)
            {
                if (hash.Contains(t.ShortId))
                    throw new NotSupportedException();
                if (t.ShortId == 0)
                    throw new NotSupportedException();
                hash.Add(t.ShortId);
            }
            // Details
            hash.Clear();
            foreach (var tt in cfg.Model.GroupCatalogs.ListCatalogs)
            {
                foreach (var t in tt.GroupDetails.ListDetails)
                {
                    if (hash.Contains(t.ShortId))
                        throw new NotSupportedException();
                    if (t.ShortId == 0)
                        throw new NotSupportedException();
                    hash.Add(t.ShortId);
                }
            }
            foreach (var tt in cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments)
            {
                foreach (var t in tt.GroupDetails.ListDetails)
                {
                    if (hash.Contains(t.ShortId))
                        throw new NotSupportedException();
                    if (t.ShortId == 0)
                        throw new NotSupportedException();
                    hash.Add(t.ShortId);
                }
            }
            // finish
            hash.Clear();
        }
#endif
        internal void SavePrepare()
        {
#if DEBUG
            this.CheckShortIdUniqueness(this.Config);
#endif
            this.Config.PluginSettingsToModel();
            this.Config.SetLastUpdated(DateTime.UtcNow);
            var proto = Config.ConvertToProto(this._Config);
            this.pconfig_history ??= new Proto.Config.proto_config_short_history();

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
#if DEBUG
                //var json = JsonFormatter.Default.Format(this.pconfig_history);
                JsonFormatter formatter = new JsonFormatter(JsonFormatter.Settings.Default.WithIndentation());
                var json = formatter.Format(this.pconfig_history);
                File.WriteAllText(this.CurrentCfgFilePath + ".json", json);
#endif
                this.UpdateUserSettingsSaveConfigs();
                this.ResetIsChangedBeforeSave();
                if (!MainPageVM.NotSaveUserSettings)
                    File.WriteAllBytes(USER_SETTINGS_FILE_PATH, UserSettings.ConvertToProto(this.UserSettings).ToByteArray());
#if DEBUG
                //var json = JsonFormatter.Default.Format(this.pconfig_history);
                //File.WriteAllText(this.CurrentCfgFilePath + ".json", json);
                //CompareSaved(json);
#endif
            }, "Can't save configuration. File path: '" + CurrentCfgFilePath + "'");
            //TODO restore private ConnStr
            this.ConnectionStringSettingsSave();
            this.Config.SetIsNew(false);
        }
        public vButtonVM<string> BtnConfigSaveAs
        {
            get
            {
                return this._BtnConfigSaveAs ??= new vButtonVM<string>(
                    (o) => { Debug.Assert(o != null); this.SaveAs(o); },
                    (o) => { return this.Config != null; });
            }
        }
        private vButtonVM<string>? _BtnConfigSaveAs;
        internal void SaveAs(string filePath)
        {
            if (VmBindable.isUnitTests)
            {
                Debug.Assert(!string.IsNullOrEmpty(filePath));
            }
            string fExt = "";
            System.Windows.Forms.SaveFileDialog? openFileDialog = null;
            if (string.IsNullOrEmpty(filePath)) // explicit filePath from tests
            {
                // https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=netframework-4.8
                openFileDialog = new()
                {
                    Filter = "vConfig files (*.vcfg)|*.vcfg",
                    CheckFileExists = false,
                    CheckPathExists = true
                };
                Debug.Assert(openFileDialog != null);
                if (!string.IsNullOrEmpty(this._FilePathSaveAs))
                {
                    openFileDialog.InitialDirectory = Path.GetDirectoryName(this._FilePathSaveAs);
                }
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                filePath = openFileDialog.FileName;
            }
            fExt = Path.GetExtension(filePath);
            switch (fExt)
            {
                case ".vcfg":
                    this.FilePathSaveAs = openFileDialog != null ? openFileDialog.FileName : Path.GetFullPath(filePath);
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
#if DEBUG
                            //var json = JsonFormatter.Default.Format(this.pconfig_history);
                            JsonFormatter formatter = new JsonFormatter(JsonFormatter.Settings.Default.WithIndentation());
                            var json = formatter.Format(this.pconfig_history);
                            File.WriteAllText(this.CurrentCfgFilePath + ".json", json);
#endif
                            this.UpdateUserSettingsSaveConfigs();
                            this.ResetIsChangedBeforeSave();
                            if (!MainPageVM.NotSaveUserSettings)
                                File.WriteAllBytes(USER_SETTINGS_FILE_PATH, UserSettings.ConvertToProto(this.UserSettings).ToByteArray());
                            this.VisibilityAndMessageInstructions();
                            // var json = JsonFormatter.Default.Format(Config.ConvertToProto(_Model));
                            // File.WriteAllText(FilePathSaveAs, json);
#if DEBUG
                            // CompareSaved(json);
#endif
                        }, "Can't save configuration. File path: '" + this.FilePathSaveAs + "'");
                    break;
                default:
                    throw new Exception($"Unsupported file extention type: {fExt}");
            }
            this.Config.SetIsNew(false);
            this.BtnConfigCurrentUpdateAsync.Command.NotifyCanExecuteChanged();
        }
        private void ResetIsChangedBeforeSave()
        {
            var vis = new ModelVisitorBase();
            IEditableObjectExt.IsTraceChanges = false;
            vis.RunFromRoot(this.Config, null, null, null, (v, n) =>
            {
                if (n is IEditableNodeGroup pp)
                {
                    pp.IsHasChanged = false;
                }
                n.IsChanged = false;
            });
            IEditableObjectExt.IsTraceChanges = true;
            if (this.Config.IsHasChanged)
            {
                var sss = this.Config.IsHasChangedPath;
                Debug.Assert(!this.Config.IsHasChanged);
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
                    while (this.UserSettings.ListOpenConfigHistory.Count > 10)
                    {
                        this.UserSettings.ListOpenConfigHistory.RemoveAt(this.UserSettings.ListOpenConfigHistory.Count - 1);
                    }
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
            var model = new Config(false);
            var pconfig_history = CommonUtils.ParseJson<Proto.Config.proto_config_short_history>(json, true);
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
                SetProperty(ref this._FilePathSaveAs, value);
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
                SetProperty(ref this._SaveToolTip, value);
            }
        }
        private const string _saveBaseToolTip = "Ctrl-S - save config";
        private string _SaveToolTip = _saveBaseToolTip;

        #region Backup
        //        public vButtonVM<string> BtnRestore
        //        {
        //            get
        //            {
        //                return this._BtnRestore ??= new vButtonVM<string>(
        //                    (o) => { this.Restore(o); },
        //                    (o) => { return this.Config != null; });
        //            }
        //        }
        //        private vButtonVM<string>? _BtnRestore;
        //        internal void Restore(string? filePath = null)
        //        {
        //            new NotImplementedException();
        //        }
        //        public vButtonVM<string> BtnBackupAs
        //        {
        //            get
        //            {
        //                return this._BtnBackupAs ??= new vButtonVM<string>(
        //                    (o) => { this.BackupAs(o); },
        //                    (o) => { return this.Config != null; });
        //            }
        //        }
        //        private vButtonVM<string>? _BtnBackupAs;
        //        internal void BackupAs(string? filePath = null)
        //        {
        //            string fExt = "";
        //            System.Windows.Forms.SaveFileDialog? openFileDialog = null;
        //            if (filePath == null) // explicite filePath from tests
        //            {
        //                // https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=netframework-4.8
        //                openFileDialog = new()
        //                {
        //                    Filter = "Solution backup files (*.vbak)|*.vbak",
        //                    CheckFileExists = false,
        //                    CheckPathExists = true
        //                };
        //                Debug.Assert(openFileDialog != null);
        //                if (!string.IsNullOrEmpty(this._FilePathSaveAs))
        //                {
        //                    openFileDialog.InitialDirectory = Path.GetDirectoryName(this._FilePathSaveAs);
        //                }
        //                if (openFileDialog.ShowDialog() != DialogResult.OK)
        //                    return;
        //                filePath = openFileDialog.FileName;
        //            }
        //            fExt = Path.GetExtension(filePath);
        //            switch (fExt)
        //            {
        //                case ".vbak": // solution backup (config and DBs)


        //                    new NotImplementedException();

        //                    this.FilePathSaveAs = openFileDialog != null ? openFileDialog.FileName : Path.GetFullPath(filePath);
        //                    this.SavePrepare();
        //                    Utils.TryCall(
        //                        () =>
        //                        {
        //                            this.CurrentCfgFilePath = this.FilePathSaveAs;
        //                            Debug.Assert(this.UserSettings != null);
        //                            Debug.Assert(this.CurrentCfgFilePath != null);
        //                            var folder = Path.GetDirectoryName(this.CurrentCfgFilePath);
        //                            Debug.Assert(folder != null);
        //                            Directory.CreateDirectory(folder);
        //                            File.WriteAllBytes(this.CurrentCfgFilePath, this.pconfig_history.ToByteArray());
        //#if DEBUG
        //                            //var json = JsonFormatter.Default.Format(this.pconfig_history);
        //                            JsonFormatter formatter = new JsonFormatter(JsonFormatter.Settings.Default.WithIndentation());
        //                            var json = formatter.Format(this.pconfig_history);
        //                            File.WriteAllText(this.CurrentCfgFilePath + ".json", json);
        //#endif
        //                            UpdateUserSettingsSaveConfigs();
        //                            ResetIsChangedBeforeSave();
        //                            File.WriteAllBytes(USER_SETTINGS_FILE_PATH, UserSettings.ConvertToProto(this.UserSettings).ToByteArray());
        //                            this.VisibilityAndMessageInstructions();
        //                            // var json = JsonFormatter.Default.Format(Config.ConvertToProto(_Model));
        //                            // File.WriteAllText(FilePathSaveAs, json);
        //#if DEBUG
        //                            // CompareSaved(json);
        //#endif
        //                        }, "Can't save configuration. File path: '" + this.FilePathSaveAs + "'");
        //                    break;
        //                default:
        //                    throw new Exception($"Unsupported file extention type: {fExt}");
        //            }
        //            this.BtnConfigCurrentUpdateAsync.Command.NotifyCanExecuteChanged();
        //        }
        #endregion Backup

        public ProgressVM ProgressVM
        {
            get
            {
                _ProgressVM ??= new ProgressVM();
                return _ProgressVM;
            }
        }
        private ProgressVM? _ProgressVM;
        private CancellationTokenSource? cancellationTokenSource;
        public vButtonVmAsync<TestTransformation?> BtnConfigValidateAsync
        {
            get
            {
                this._BtnConfigValidateAsync ??= new vButtonVmAsync<TestTransformation?>(
                        async (o) =>
                        {
                            Debug.Assert(this.ProgressVM != null);
                            try
                            {
                                this.cancellationTokenSource = new CancellationTokenSource();
                                var cancellationToken = this.cancellationTokenSource.Token;
                                this.ProgressVM.ProgressStart("Configuration Validation", 0, null, 0, cancellationToken);
                                // https://learn.microsoft.com/en-us/archive/msdn-magazine/2014/april/mvvm-multithreading-and-dispatching-in-mvvm-applications
                                // https://softwareengineering.stackexchange.com/questions/347970/multithreaded-c-mvvm-application-architecture
                                if (VmBindable.isUnitTests)
                                    await this._Config.ValidateSubTreeFromNodeAsync(this._Config, null, cancellationToken, this._logger);
                                else
                                    await Task.Run(() =>
                                    {
                                        return this._Config.ValidateSubTreeFromNodeAsync(this._Config, this.ProgressVM, cancellationToken, this._logger);
                                    });
                                //this._Config.ValidateSubTreeFromNodeAsync(this._Config, cancellationToken, this._logger).SafeFireAndForget(onException: ex => Trace.WriteLine(ex));
                            }
                            catch (CancellationException)
                            {
                            }
                            catch (Exception ex)
                            {
                                this.ProgressVM.Exception = ex;
                                if (o == null)
#if DEBUG
                                    if (VmBindable.isUnitTests)
                                        throw;
                                    else
#endif
                                        Xceed.Wpf.Toolkit.MessageBox.Show(this.ProgressVM.Exception.ToString(), "Error");
                            }
                            finally
                            {
                                this.cancellationTokenSource = null;
                                this.ProgressVM.ProgressClose();
                            }
                        }, (o) =>
                        {
                            if (this.cancellationTokenSource != null)
                            {
                                return false;
                            }
                            if (this.Config == null)
                            {
                                return false;
                            }
                            //this._BtnConfigCurrentUpdate!.ToolTipText = "kuku";
                            return true;
                        }
                    );
                return this._BtnConfigValidateAsync;
            }
        }
        private vButtonVmAsync<TestTransformation?>? _BtnConfigValidateAsync;
        private CancellationTokenSource? cancellationSourceForValidatingSubTreeFromNode = null;
        public async Task ValidateSelectedNodeAsync()
        {
            if (this.Config.SelectedNode != null)
            {
                if (this.cancellationSourceForValidatingSubTreeFromNode != null)
                {
                    this.cancellationSourceForValidatingSubTreeFromNode.Cancel();
                    this._logger?.LogInformation("=== Cancellation request ===");
                }
                this.cancellationSourceForValidatingSubTreeFromNode = new CancellationTokenSource();
                var token = this.cancellationSourceForValidatingSubTreeFromNode.Token;
                await Task.Run(() =>
                {
                    return this.Config.ValidateSubTreeFromNodeAsync(this.Config.SelectedNode, null, token, this._logger);
                });
            }
        }
        public vButtonVmAsync<TestTransformation?> BtnConfigCurrentUpdateAsync
        {
            get
            {
                this._BtnConfigCurrentUpdate ??= new vButtonVmAsync<TestTransformation?>(
                        async (o) =>
                        {
                            Debug.Assert(this.ProgressVM != null);
                            try
                            {
                                await this.BtnConfigValidateAsync.ExecuteAsync(o);
                                if (this._Config.CountErrors > 0)
                                {
#if DEBUG
                                    if (!VmBindable.isUnitTests)
                                    {
#endif
                                        var res = Xceed.Wpf.Toolkit.MessageBox.Show($"There are {this._Config.CountErrors} errors in configuration. First error is\n\n{this._Config.FindValidationMessage()?.Message}\n\nFix errors and try again.",
                                            "Error", System.Windows.MessageBoxButton.OK);
                                        this.cancellationTokenSource = null;
                                        this.ProgressVM.ProgressClose();
                                        return;
#if DEBUG
                                    }
                                    else
                                    {
                                        var sb = new StringBuilder();
                                        sb.AppendLine();
                                        foreach (var t in this._Config.ValidationCollection)
                                        {
                                            if (t.Severity == FluentValidation.Severity.Error)
                                                sb.AppendLine(t.Message);
                                        }
                                        throw new Exception($"There are {this._Config.CountErrors} config errors.{sb}");
                                    }
#endif
                                }
                                #region
#if DEBUG
                                if (!VmBindable.isUnitTests)
                                {
#endif
                                    if (this._Config.CountWarnings > 0)
                                    {
                                        var res = Xceed.Wpf.Toolkit.MessageBox.Show("There are warnings in the config model. Continue?", "Warning", System.Windows.MessageBoxButton.OKCancel);
                                        if (res != System.Windows.MessageBoxResult.OK)
                                            return;
                                    }
#if DEBUG
                                }
#endif
                                #endregion


                                this.cancellationTokenSource = new CancellationTokenSource();
                                CancellationToken cancellationToken = this.cancellationTokenSource.Token;

                                this.ProgressVM.ProgressStart("Updating Code/DB to Current Version of Configuration", 0, emptyStr, 0, cancellationToken);
                                if (VmBindable.isUnitTests)
                                    await this.UpdateCurrentVersionAsync(cancellationToken, o);
                                else
                                    await Task.Run(() =>
                                    {
                                        return this.UpdateCurrentVersionAsync(cancellationToken, o);
                                    });
                                this.ResetIsChangedBeforeSave();
                            }
                            catch (CancellationException)
                            {
                            }
                            catch (Exception ex)
                            {
                                this.ProgressVM.Exception = ex;
#if DEBUG
                                if (VmBindable.isUnitTests)
                                    throw;
                                else
#endif
                                    Xceed.Wpf.Toolkit.MessageBox.Show(this.ProgressVM.Exception.ToString(), "Error");
                            }
                            finally
                            {
                                //if (!isException)
                                //{
                                //    //await this.CommandConfigSave.ExecuteAsync(null);
                                //    this.BtnConfigSave.Command.Execute(null);
                                //}
                                this.cancellationTokenSource = null;
                                this.ProgressVM.ProgressClose();
                            }
                        }, (o) =>
                        {
                            if (this.cancellationTokenSource != null)
                            {
                                return false;
                            }
                            if (this.Config == null)
                            {
                                return false;
                            }
                            //if (!this.Config.IsNeedCurrentUpdate)
                            //{
                            //    return false;
                            //}
                            if (string.IsNullOrWhiteSpace(this.CurrentCfgFilePath))
                            {
                                return false;
                            }
                            return true;
                        }
                    );
                return this._BtnConfigCurrentUpdate;
            }
        }
        private vButtonVmAsync<TestTransformation?>? _BtnConfigCurrentUpdate;
#if PARRALEL
        public async Task GenerateCodeAsync(CancellationToken cancellationToken, IConfig diffConfig, bool isCurrentUpdate, bool isDeleteDb = false)
#else
        public void GenerateCode(CancellationToken cancellationToken, IConfig diffConfig, bool isCurrentUpdate, bool isDeleteDb = false)
#endif
        {
            var nGens = 0;
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
                                nGens++;
                            }
                        }
                    }
                }
            }
            //IProperty.Config = diffConfig;

            //var nvb = new ModelVisitorBase();
            //nvb.Run(diffConfig, null, null, null, (p, n) => { if (n is Property pp) pp.Tag = null; });
            int i = 0;
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
                                    this.ProgressVM?.ProgressUpdateSubTask($"Project '{ts.Name}'-'{tp.Name}'-'{tpg.Name}'", 100 * i / nGens);
                                    i++;
                                    string? code = null;
                                    switch (tg.Generator.PluginGeneratorType)
                                    {
                                        case vPluginLayerTypeEnum.DbDesign:
                                            if (tg.Generator is not IvPluginDbGenerator)
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
                                            if (tg.Generator is not IvPluginGenerator)
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
                            sb.AppendLine(emptyStr);
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
        private async Task UpdateCurrentVersionAsync(CancellationToken cancellationToken, object? parm = null, bool askWarning = true)
        {
            TestTransformation? tst = parm as TestTransformation;
            try
            {
                int iProgressSteps = 5;
                int iProgressStep = 1;
                GuiLabs.Undo.ActionManager am = new GuiLabs.Undo.ActionManager();
                var dicRenamed = new Dictionary<string, string?>();
                var mvr = new ModelVisitorBase();
                // Collect renamed objects
                mvr.RunFromRoot(this.Config, null, null, null, (m, n) =>
                {
                    if (!dicRenamed.ContainsKey(n.Guid) && n.IsRenamed(false))
                    {
                        dicRenamed[n.Guid] = null;
                    }
                });

                // I. Remove new marked for deletion nodes
                #region
                this.ProgressVM?.ProgressUpdate($"{iProgressStep}/{iProgressSteps}. Removing new marked for deletion nodes", 0);
                var lst = new List<string>();
                // delete from current model
                var vis1 = new ModelVisitorBase();
                vis1.RunFromRoot(this.Config, null, null, null, (v, n) =>
                {
                    if (n is IEditableNode p)
                    {
                        if (p.IsMarkedForDeletion && n is ICanAddNode pn && pn.IsNew)
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
                #endregion

                using (Transaction.Create(am))
                {
                    // II. Rename analysis
                    #region
                    this.ProgressVM?.ProgressUpdate($"{iProgressStep}. Finding objects for renaming", iProgressStep * 100 / iProgressSteps);
                    iProgressStep++;
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
                                if (gg is not IvPluginCodeGenerator)
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
                        this.ProgressVM?.ProgressUpdate($"{iProgressStep}. Compiling current code", iProgressStep * 100 / iProgressSteps);
                        iProgressStep++;

                        int ii = 0;
                        foreach (var ts in this.Config.GroupAppSolutions.ListAppSolutions)
                        {
                            if (cancellationToken.IsCancellationRequested)
                                throw new CancellationException();
                            this.ProgressVM?.ProgressUpdateSubTask($"Compiling solution '{ts.Name}'", 100 * ii / this.Config.GroupAppSolutions.ListAppSolutions.Count);
                            ii++;

                            await CompileUtils.CompileAsync(_logger, ts.GetCombinedPath(ts.RelativeAppSolutionPath), cancellationToken);
                            //CompileUtils.Compile(_logger, ts.GetCombinedPath(ts.RelativeAppSolutionPath), cancellationToken);

                            //TODO result of compilation
                        }
                    }
                    else
                    {
                        iProgressStep++;
                    }
                    // unit test
                    if (tst != null && tst.IsThrowExceptionOnBuildValidated)
                        throw new Exception(nameof(tst.IsThrowExceptionOnBuildValidated));
                    #endregion

                    // IV. Rename objects and properties by solution (code can be not compilible after that) (need UNDO from zip code backup)
                    #region
                    this.ProgressVM?.ProgressUpdate($"{iProgressStep}. Renaming objects in code", iProgressStep * 100 / iProgressSteps);
                    iProgressStep++;
                    var nProjects = 0;
                    foreach (var ts in this.Config.GroupAppSolutions.ListAppSolutions)
                    {
                        foreach (var tp in ts.ListAppProjects)
                        {
                            nProjects++;
                        }
                    }
                    int i = 0;
                    foreach (var ts in this.Config.GroupAppSolutions.ListAppSolutions)
                    {
                        foreach (var tp in ts.ListAppProjects)
                        {
                            this.ProgressVM?.ProgressUpdateSubTask($"Project '{ts.Name}'-'{tp.Name}'", 100 * i / nProjects);
                            i++;
                            foreach (var tg in tp.ListAppProjectGenerators)
                            {
                                if (cancellationToken.IsCancellationRequested)
                                    throw new CancellationException();
                                Debug.Assert(this._Config.DicGenerators != null);
                                var gg = this._Config.DicGenerators[tg.PluginGeneratorGuid];
                                if (gg is not IvPluginCodeGenerator)
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

                    // V. Generate code (no need for UNDO)
                    #region
                    this.ProgressVM?.ProgressUpdate($"{iProgressStep}. Generating code/DB", iProgressStep * 100 / iProgressSteps);
                    iProgressStep++;
#if PARRALEL
                    await this.GenerateCodeAsync(cancellationToken, this.Config, true);
#else
                    this.GenerateCode(cancellationToken, this.Config, true);
#endif
                    var vis = new ModelVisitorRemoveMarkedIfNewObjects(this.Config);
                    vis.DeleteNewMarkedForDeletion();
                    //this.Config.SetIsNeedCurrentUpdate(false);
                    //this.Save();
                    // unit test
                    if (tst != null && tst.IsThrowExceptionOnCodeGenerated)
                        throw new Exception();
                    #endregion

                    // VI. Update history CurrentConfig (need UNDO)
                    #region
#if Async
#else
#endif
                    var update_history = new CallMethodAction(
                      () =>
                      {
                          //this.Save();
                          var proto = Config.ConvertToProto(this.Config);
                          this.Config.PrevCurrentConfig = Config.ConvertToVM(proto, new Config(false));
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

                    // VII. Generate Update SQL for previous stable DB
                    //TODO Generate Update SQL for previous stable DB

                    this.Save();
                    this.Config.SetIsNeedCurrentUpdate(false);
                }
            }
            //catch (Exception ex)
            //{
            //    resEx = ex;
            //}
            finally
            {
                //TODO roll back if Exception
            }
        }
        public vButtonVmAsync<TestTransformation?> BtnConfigCreateStableVersionAsync
        {
            get
            {
                return this._BtnConfigCreateStableVersionAsync ??= new vButtonVmAsync<TestTransformation?>(
                    (t) =>
                    {
                        this.ProgressVM?.ProgressStart("Creating Version for Deployment");
                        try
                        {
                            this.CreateStableVersion(t);
                        }
                        finally
                        {
                            this.ProgressVM?.ProgressClose();
                        }
                        return Task.CompletedTask;
                    },
                    (t) => { return this.Config != null && !string.IsNullOrWhiteSpace(this.Config.CurrentCfgFolderPath); });
            }
        }
        private vButtonVmAsync<TestTransformation?>? _BtnConfigCreateStableVersionAsync;
        //public vButtonVmAsync<TestTransformation?> BtnConfigCreateStableVersionAsync
        //{
        //    get
        //    {
        //        return this._BtnConfigCreateStableVersionAsync ?? (this._BtnConfigCreateStableVersionAsync = new vButtonVmAsync<TestTransformation?>(
        //            (t) =>
        //            {
        //                return Task.Run(() =>
        //                {
        //                    this.IsBusy = true;
        //                    try
        //                    {
        //                        this.CreateStableVersion(t);
        //                    }
        //                    finally
        //                    {
        //                        this.IsBusy = false;
        //                    }
        //                });
        //            },
        //            (t) => { return this.Config != null; }));
        //    }
        //}
        //private vButtonVmAsync<TestTransformation?>? _BtnConfigCreateStableVersionAsync;
        private void CreateStableVersion(TestTransformation? tst)
        {
            if (this.pconfig_history == null)
            {
                var ex = new NotSupportedException();
                _logger?.LogCritical(ex, emptyStr.CallerInfo());
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

            this.BtnConfigSave.Command.Execute(null);

            var vis = new ModelVisitorBase();
            if (this.Config.PrevStableConfig != null)
            {
                #region Remove Deleted (was Deprecated)
                var lst = new List<string>();
                // delete from current model
                vis.RunFromRoot(this.Config, null, null, null, (v, n) =>
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
            this.Config.PrevStableConfig = Config.ConvertToVM(proto, new Config(false));
            this.InitConfig((Config)this.Config.PrevStableConfig);
            this.Config.PrevCurrentConfig = Config.ConvertToVM(proto, new Config(false));
            this.InitConfig((Config)this.Config.PrevCurrentConfig);
            this.pconfig_history.CurrentConfig.Version++;
            IEditableObjectExt.IsTraceChanges = false;
            vis.RunToRoot(this.Config, null, null, null, (v, n) =>
            {
                if (n is ICanAddNode p)
                {
                    p.IsNew = false;
                }
                if (n is IEditableNodeGroup pp)
                {
                    pp.IsHasNew = false;
                }
            });
            IEditableObjectExt.IsTraceChanges = true;
            //this.Config.SetIsNeedCurrentUpdate(false);
            Utils.TryCall(
                () =>
                {
                    Debug.Assert(!string.IsNullOrWhiteSpace(CurrentCfgFilePath));
                    File.WriteAllBytes(CurrentCfgFilePath, this.pconfig_history.ToByteArray());
                }, "Can't save configuration. File path: '" + CurrentCfgFilePath + "'");
            this.ResetIsChangedBeforeSave();
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
                SetProperty(ref this._MessageInstructions, value);
            }
            get { return _MessageInstructions; }
        }
        private string? _MessageInstructions;
        public System.Windows.Visibility VisibilityConfig
        {
            set
            {
                SetProperty(ref this._VisibilityConfig, value);
            }
            get { return _VisibilityConfig; }
        }
        private System.Windows.Visibility _VisibilityConfig = System.Windows.Visibility.Hidden;
        public vButtonVM BtnAddNew
        {
            get
            {
                return this._BtnAddNew ??= new vButtonVM(
                () =>
                {
                    Utils.TryCall(() =>
                    {
                        Debug.Assert(this.Config.SelectedNode != null);
                        if (this.Config.SelectedNode.NodeCanAddNewSubNode())
                        {
                            this.Config.SelectedNode.NodeAddNewSubNode();
                        }
                        else
                        {
                            if (this.Config.SelectedNode.NodeCanAddNew())
                                this.Config.SelectedNode.NodeAddNew();
                        }
                    }, "Add new node command");
                },
                () => { return this.Config != null && this.Config.SelectedNode != null && (this.Config.SelectedNode.NodeCanAddNew() || this.Config.SelectedNode.NodeCanAddNewSubNode()); });
            }
        }
        private vButtonVM? _BtnAddNew;
        //public vButtonVM BtnAddNewChild
        //{
        //    get
        //    {
        //        return this._BtnAddNewChild ??= new vButtonVM(
        //        () => { Utils.TryCall(() => { Debug.Assert(this.Config.SelectedNode != null); this.Config.SelectedNode.NodeAddNewSubNode(); }, "Add new sub node command"); },
        //        () => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanAddNewSubNode(); });
        //    }
        //}
        //private vButtonVM? _BtnAddNewChild;
        public vButtonVM BtnAddClone
        {
            get
            {
                return this._BtnAddClone ??= new vButtonVM(
                () => { Debug.Assert(this.Config.SelectedNode != null); this.Config.SelectedNode.NodeAddClone(); },
                () => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanAddClone(); });
            }
        }
        private vButtonVM? _BtnAddClone;
        public vButtonVM BtnMoveDown
        {
            get
            {
                return this._BtnMoveDown ??= new vButtonVM(
                () => { Debug.Assert(this.Config.SelectedNode != null); this.Config.SelectedNode.NodeMoveDown(); },
                () => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanMoveDown(); });
            }
        }
        private vButtonVM? _BtnMoveDown;
        public vButtonVM BtnMoveUp
        {
            get
            {
                return this._BtnMoveUp ??= new vButtonVM(
                () => { Debug.Assert(this.Config.SelectedNode != null); this.Config.SelectedNode.NodeMoveUp(); },
                () => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanMoveUp(); });
            }
        }
        private vButtonVM? _BtnMoveUp;
        public vButtonVM BtnDelete
        {
            get
            {
                return this._BtnDelete ??= new vButtonVM(
                () => { Debug.Assert(this.Config.SelectedNode != null); this.Config.SelectedNode.NodeMarkForDeletion(); },
                () => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanMarkForDeletion(); });
            }
        }
        private vButtonVM? _BtnDelete;
        public vButtonVM BtnSelectionLeft
        {
            get
            {
                return this._BtnSelectionLeft ??= new vButtonVM(
                () => { Debug.Assert(this.Config.SelectedNode != null); this.Config.SelectedNode.NodeLeft(); },
                () => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanLeft(); });
            }
        }
        private vButtonVM? _BtnSelectionLeft;
        public vButtonVM BtnSelectionRight
        {
            get
            {
                return this._BtnSelectionRight ??= new vButtonVM(
                () => { Debug.Assert(this.Config.SelectedNode != null); this.Config.SelectedNode.NodeRight(); },
                () => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanRight(); });
            }
        }
        private vButtonVM? _BtnSelectionRight;
        public vButtonVM BtnSelectionDown
        {
            get
            {
                return this._BtnSelectionDown ??= new vButtonVM(
                () => { Debug.Assert(this.Config.SelectedNode != null); this.Config.SelectedNode.NodeDown(); },
                () => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanDown(); });
            }
        }
        private vButtonVM? _BtnSelectionDown;
        public vButtonVM BtnSelectionUp
        {
            get
            {
                return this._BtnSelectionUp ??= new vButtonVM(
                () => { Debug.Assert(this.Config.SelectedNode != null); this.Config.SelectedNode.NodeUp(); },
                () => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanUp(); });
            }
        }
        private vButtonVM? _BtnSelectionUp;
        #endregion ConfigTree

        public vCommand CommandFromErrorToSelection
        {
            get
            {
                return this._CommandFromErrorToSelection ??= vCommand.Create(
                (o) =>
                {
                    if (o == null)
                    {
                        return;
                    }
                    this.Config.SelectedNode = (ITreeConfigNode?)((ValidationMessage?)o)?.Model;
                },
                (o) => { return this.Config != null; });
            }
        }
        private vCommand? _CommandFromErrorToSelection;

        #region ConnectionString

        // https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configurationmanager?f1url=https%3A%2F%2Fmsdn.microsoft.com%2Fquery%2Fdev16.query%3FappId%3DDev16IDEF1%26l%3DEN-US%26k%3Dk(System.Configuration.ConfigurationManager);k(TargetFrameworkMoniker-.NETFramework,Version%3Dv4.7.2);k(DevLang-csharp)%26rd%3Dtrue&view=netframework-4.8
        // https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configuration?view=netframework-4.8
        internal ConnectionStringSettingsCollection? ConnectionStringSettings = null;

        private void ConnectionStringSettingsSave()
        {
            if (MainPageVM.NotSaveUserSettings)
                return;
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
        //            OnPropertyChanged();
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
        //        OnPropertyChanged();
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
            var currPath = Directory.GetCurrentDirectory();
            var vspro = currPath[..currPath.IndexOf(@"\bin")];
            var bin = currPath[currPath.IndexOf(@"\bin")..];
            var s = @"\vSharpStudio.pro";
            var indxPro = currPath.IndexOf(s);
            string dir = "";
            if (indxPro > 0)
            {
                dir = currPath[..currPath.IndexOf(s)];
                dir = dir + s;
            }
            var res = $"{dir}\\submodules\\vSharpStudio\\vSharpStudio{bin}";
            return res;
        }
        #endregion Utils
    }
}
