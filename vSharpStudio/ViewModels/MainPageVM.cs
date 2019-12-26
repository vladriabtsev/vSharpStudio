﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Google.Protobuf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.std;
using vSharpStudio.Views;
using vSharpStudio.vm.ViewModels;
using Xceed.Wpf.Toolkit;

namespace vSharpStudio.ViewModels
{
    public class MainPageVM : ViewModelValidatableWithSeverity<MainPageVM, MainPageVMValidator>, IPartImportsSatisfiedNotification
    {
        private static ILogger _logger;

        public MainPageVM()
            : base(MainPageVMValidator.Validator)
        {
            _logger = Logger.CreateLogger<MainPageVM>();
        }

        public MainPageVM(bool isLoadConfig, Action<MainPageVM, IEnumerable<Lazy<IvPlugin, IDictionary<string, object>>>> onImportsSatisfied = null, string configFile = null)
            : this()
        {
            this.onImportsSatisfied = onImportsSatisfied;
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                // Catalog c = new Catalog();
                // this.Model = new ConfigRoot();
                // this.Model.Catalogs.ListCatalogs.Add(c);
                return;
            }

            //if (App.ServiceCollection == null)
            //{
            //    ILoggerFactory loggerFactory = std.ApplicationLogging.LoggerFactory;
            //    App.ServiceCollection = new ServiceCollection();
            //    App.ServiceCollection.Add(ServiceDescriptor.Singleton<ILoggerFactory>(loggerFactory));
            //}
            //var Services = App.ServiceCollection.BuildServiceProvider();
            //this.Logger = Services.GetRequiredService<ILoggerFactory>().CreateLogger<MainPageVM>();
            //this.Logger.LogInformation("Application is starting.");
            _logger.LogDebug("*** Application is starting. ***".CallerInfo());

            if (isLoadConfig)
            {
                if (configFile != null)
                {
                    _logger.LogDebug("Load Configuration from file {ConfigFile}".CallerInfo(), configFile);
                    this.Config = this.LoadConfig(configFile, string.Empty, true);
                }
                else if (File.Exists(CFG_FILE_PATH))
                {
                    _logger.LogDebug("Load Configuration from standard file {ConfigFile}".CallerInfo(), CFG_FILE_PATH);
                    this.Config = this.LoadConfig(CFG_FILE_PATH, string.Empty, true);
                }
                else
                {
                    _logger.LogDebug("Creating empty Configuration".CallerInfo());
                    this.Config = new Config();
                }
            }
            else
            {
                _logger.LogDebug("Creating empty Configuration".CallerInfo());
                this.Config = new Config();
            }

            // this.PathToProjectWithConnectionString = Directory.GetCurrentDirectory();
            // this.Model.OnProviderSelectionChanged = (provider) =>
            // {
            //    this.ConnectionStringSettings = ConfigurationManager.ConnectionStrings;
            //    this.Model.ListConnectionStringVMs.Clear();
            //    this.Model.ListDbProviders.Clear();
            //    for (int i = 0; i < this.ConnectionStringSettings.Count; i++)
            //    {
            //        string pr = this.ConnectionStringSettings[i].ProviderName;
            //        if (string.IsNullOrEmpty(pr))
            //        {
            //            bool isFound = false;
            //            foreach (var tt in this.Model.ListDbProviders)
            //            {
            //                if (tt == pr)
            //                {
            //                    isFound = true;
            //                    break;
            //                }
            //            }
            //            if (!isFound)
            //                this.Model.ListDbProviders.Add(pr);
            //        }
            //        if (provider != null)
            //        {
            //            if (provider == pr)
            //            {
            //                this.Model.ListConnectionStringVMs.Add(new ConnStringVM()
            //                {
            //                    Name = this.ConnectionStringSettings[i].Name,
            //                    ConnectionString = this.ConnectionStringSettings[i].ConnectionString,
            //                    Provider = pr
            //                });
            //            }
            //        }
            //        else
            //        {
            //            this.Model.ListConnectionStringVMs.Add(new ConnStringVM()
            //            {
            //                Name = this.ConnectionStringSettings[i].Name,
            //                ConnectionString = this.ConnectionStringSettings[i].ConnectionString,
            //                Provider = pr
            //            }); ;
            //        }
            //    }
            // };
            // this.Model.OnProviderSelectionChanged(null);
        }

        private Config LoadConfig(string file_path, string indent, bool isRoot = false)
        {
            Config.IsLoading = true;
            if (!File.Exists(file_path))
            {
                var ex = new ArgumentException("Configuration data are not found in the file: " + file_path);
                _logger.LogCritical(ex, "".CallerInfo());
                throw ex;
            }
            var protoarr = File.ReadAllBytes(file_path);
            this.pconfig_history = Proto.Config.proto_config_short_history.Parser.ParseFrom(protoarr);
            _logger.LogDebug("ConvertToVM Main Config".CallerInfo());
            var cfg = Config.ConvertToVM(this.pconfig_history.CurrentConfig, new Config());
            if (isRoot)
            {
                if (this.pconfig_history.PrevStableConfig != null)
                {
                    _logger.LogDebug("ConvertToVM Prev Config".CallerInfo());
                    cfg.PrevStableConfig = Config.ConvertToVM(this.pconfig_history.PrevStableConfig, new Config());
                }

                if (this.pconfig_history.OldStableConfig != null)
                {
                    _logger.LogDebug("ConvertToVM Old Config".CallerInfo());
                    cfg.OldStableConfig = Config.ConvertToVM(this.pconfig_history.OldStableConfig, new Config());
                }
            }
            string ind2 = indent + "   ";
            foreach (var t in cfg.GroupConfigLinks.ListBaseConfigLinks.ToList())
            {
                _logger.LogDebug("Load Base Config {Name} from {Path}".CallerInfo(), t.Name, t.RelativeConfigFilePath);
                t.Config = this.LoadConfig(t.RelativeConfigFilePath + CFG_FILE_NAME, ind2);
                t.Name = t.Config.Name;
            }

            // string json = File.ReadAllText(CFG_PATH);
            // this.Model = new Config(json);
            Config.IsLoading = false;
            return cfg;
        }

        public Proto.Config.proto_config_short_history pconfig_history { get; private set; }

        public static readonly string CFG_FILE_PATH = @".\current.vcfg";
        public static readonly string CFG_FILE_NAME = "current.vcfg";

        // public DiffModel GetDiffModel()
        // {
        //    DiffModel res = new DiffModel(
        //        pconfig_history?.OldStableConfig == null ? null : Config.ConvertToVM(pconfig_history.OldStableConfig, new Config()),
        //        pconfig_history?.PrevStableConfig == null ? null : Config.ConvertToVM(pconfig_history.PrevStableConfig, new Config()),
        //        this.Config
        //    );
        //    return res;
        // }
        // internal void OnSelectedItemChanged(object oldValue, object newValue)
        // {
        //    this.Model.SelectedNode = (ITreeConfigNode)newValue;
        // }
        public static Config ConfigInstance;

        #region Plugins

        // https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-4.0/ff603380(v=vs.100) // debug
        // https://www.codeproject.com/Articles/376033/From-Zero-to-Proficient-with-MEF
        // https://docs.microsoft.com/en-us/dotnet/framework/mef/
        [ImportMany(typeof(IvPlugin))]
        private IEnumerable<Lazy<IvPlugin, IDictionary<string, object>>> _plugins = null;
        private Action<MainPageVM, IEnumerable<Lazy<IvPlugin, IDictionary<string, object>>>> onImportsSatisfied = null;

        public void OnImportsSatisfied()
        {
            try
            {
                _logger.LogDebug("Loaded {Count} plugins".CallerInfo(), this._plugins.Count());
                if (this.onImportsSatisfied != null)
                {
                    this.onImportsSatisfied(this, this._plugins);
                }

                List<PluginRow> lstGens = new List<PluginRow>();

                // List<IvPluginDbGenerator> lstDbs = new List<IvPluginDbGenerator>();
                foreach (var t in this._plugins)
                {
                    Plugin p = null;
                    bool is_found = false;

                    // attaching plugin
                    foreach (var tt in this.Config.GroupPlugins.ListPlugins)
                    {
                        if (tt.Guid == t.Value.Guid) // && (string.IsNullOrWhiteSpace(tt.Version) || tt.Version == t.Value.Version))
                        {
                            tt.SetVPlugin(t.Value);
                            //tt.GroupGuid = t.Value.GroupGuid;
                            //tt.GroupVersion = t.Value.GroupVersion;
                            //tt.GroupInfo = t.Value.GroupInfo;
                            tt.Name = t.Value.Name;
                            tt.Description = t.Value.Description;
                            tt.Version = t.Value.Version;
                            p = tt;
                            is_found = true;
                            break;
                        }
                    }
                    if (!is_found)
                    {
                        p = new Plugin(this.Config.GroupPlugins, t.Value);
                        this.Config.GroupPlugins.ListPlugins.Add(p);
                    }

                    // attaching plugin generators
                    foreach (var tt in t.Value.ListGenerators)
                    {
                        PluginGenerator pg = null;
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
                    foreach (var ttt in p.ListGenerators)
                    {
                        foreach (var tttt in ttt.ListSettings)
                        {
                            if (ttt.Generator != null)
                            {
                                if (tttt.IsPrivate)
                                {
                                    Utils.TryCall(
                                        () =>
                                        {
                                            tttt.GeneratorSettings = File.ReadAllText(tttt.FilePath);
                                        }, "Private connection settins was not loaded. Plugin: '" + p.Name + "' Generator: '" + ttt.Name + "' Connection settings name: '" + tttt.Name + "' File path: '" + tttt.FilePath + "'");
                                }
                                else
                                {
                                    Utils.TryCall(
                                        () =>
                                    {
                                        tttt.SetVM(ttt.Generator.GetAppGenerationSettingsVmFromJson(tttt.GeneratorSettings));
                                    }, "Can't get MVVM settings model from Plugin: '" + p.Name + "' Generator: '" + ttt.Name + "'");
                                }
                            }
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
                if (this.DicPlugins != null)
                {
                    this.DicPlugins.Clear();
                }

                this.DicPlugins = dic;
                this.Config.DicPlugins = dic;

                // Generators
                this.Config.DicGenerators = new Dictionary<string, IvPluginGenerator>();
                foreach (var t in lstGens)
                {
                    this.Config.DicGenerators[t.PluginGenerator.Guid] = t.PluginGenerator.Generator;
                }

                // Create Settings VM for all project generators
                foreach (var t in this.Config.GroupAppSolutions.ListAppSolutions)
                {
                    foreach (var tt in t.ListAppProjects)
                    {
                        foreach (var ttt in tt.ListAppProjectGenerators)
                        {
                            ttt.CreateGenSettings();
                        }
                    }
                }
                this.Config.RefillDicGenerators();
                // Restore Node Settings VM for all nodes, which are supporting INodeGenSettings
                var nv = new NodeGenSettingsModelVisitor();
                nv.NodeGenSettingsApplyAction(this.Config, (p) =>
                {
                    p.RestoreNodeAppGenSettingsVm();
                });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "".CallerInfo());
                throw;
            }
        }

        public Dictionary<vPluginLayerTypeEnum, List<PluginRow>> DicPlugins
        {
            get
            {
                return this._DicPlugins;
            }

            set
            {
                this._DicPlugins = value;
                this.NotifyPropertyChanged();
            }
        }

        public Dictionary<vPluginLayerTypeEnum, List<PluginRow>> _DicPlugins;

        public List<IvPluginDbGenerator> ListDbDesignPlugins
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

        public List<IvPluginDbGenerator> _ListDbDesignPlugins;

        public PluginRow SelectedDbDesignPlugin
        {
            get
            {
                return this._SelectedDbDesignPlugin;
            }

            set
            {
                this._SelectedDbDesignPlugin = value;
                this.NotifyPropertyChanged();

                // this.Model.GroupSettings.
                // var propvm = _SelectedDbDesignPlugin.GetSettingsMvvm()
            }
        }

        private PluginRow _SelectedDbDesignPlugin;

        public INotifyPropertyChanged SelectedDbDesignPluginSettings
        {
            get
            {
                return this._SelectedDbDesignPluginSettings;
            }

            set
            {
                this._SelectedDbDesignPluginSettings = value;
                this.NotifyPropertyChanged();

                // InitConnectionString();
            }
        }

        private INotifyPropertyChanged _SelectedDbDesignPluginSettings;

        private void AgregateCatalogs(string dir, string search, AggregateCatalog catalog)
        {
            var dirs = Directory.GetDirectories(dir);
            foreach (var t in dirs)
            {
                DirectoryCatalog dirCatalog = new DirectoryCatalog(t, search);
                if (dirCatalog.Parts.Count() > 0)
                {
                    catalog.Catalogs.Add(dirCatalog);
                }

                this.AgregateCatalogs(t, search, catalog);
            }
        }

        public void Compose()
        {
            _logger.LogDebug("Loading plugins".CallerInfo());
            try
            {
                AggregateCatalog catalog = new AggregateCatalog();
                this.AgregateCatalogs(Directory.GetCurrentDirectory() + "\\Plugins", "vPlugin*.dll", catalog);
                CompositionContainer container = new CompositionContainer(catalog, CompositionOptions.DisableSilentRejection);
                container.SatisfyImportsOnce(this);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "".CallerInfo());
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
                this.NotifyPropertyChanged();
                this.ValidateProperty();
                this._Config.OnSelectedNodeChanging = (from, to) =>
                {
                    if (to is INodeGenSettings)
                    {
                        var t = to as INodeGenSettings;
                        t.CreatePropertyGridNodeGenSettings(t);
                    }
                    else
                    {
                        //TODO remove property without node settings from PropertyGrid
                    }
                };
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
                    this._Config.ValidateSubTreeFromNode(this._Config.SelectedNode);
                };
            }
        }

        private Config _Config;

        //public Microsoft.EntityFrameworkCore.Metadata.IMutableModel GetEfModel()
        //{
        //    Migration.ConfigToModelVisitor visitor = new Migration.ConfigToModelVisitor();
        //    this.Config.AcceptConfigNodeVisitor(visitor);
        //    return visitor.Result;
        //}

        #region Main

        public vCommand CommandConfigSave
        {
            get
            {
                return this._CommandConfigSave ?? (this._CommandConfigSave = vCommand.Create(
                    (o) => { this.Save(); },
                    (o) => { return this.Config != null; }));
            }
        }

        private vCommand _CommandConfigSave;

        private void PluginSettingsToModel()
        {
            foreach (var t in this._Config.GroupAppSolutions.ListAppSolutions)
            {
                foreach (var tt in t.ListAppProjects)
                {
                    foreach (var ttt in tt.ListAppProjectGenerators)
                    {
#if RELEASE
                        Utils.TryCall(
                            () =>
                            {
#endif
                        if (ttt.Settings != null && ttt.Settings is IvPluginGeneratorSettingsVM)
                            ttt.GeneratorSettings = (ttt.Settings as IvPluginGeneratorSettingsVM).SettingsAsJson;
#if RELEASE
                            }, "Can't get PROTO settings from Plugin");
#endif
                        //if (ttt.IsPrivate)
                        //{
                        //    Utils.TryCall(
                        //        () =>
                        //        {
                        //            File.WriteAllText(ttt.FilePath, ttt.GeneratorSettings);
                        //        }, "Private connection settins was not saved. Plugin: '" + t.Name + "' Generator: '" + tt.Name + "' Settings: '" + ttt.Name + "' File path: '" + ttt.FilePath + "'");
                        //    ttt.GeneratorSettings = string.Empty;
                        //}
                    }
                }
            }
            // Save Node Settings VM for all nodes, which are supporting INodeGenSettings
            var nv = new NodeGenSettingsModelVisitor();
            nv.NodeGenSettingsApplyAction(this.Config, (p) =>
            {
                p.SaveNodeAppGenSettings();
            });
            //foreach (var t in this._Config.GroupPlugins.ListPlugins)
            //{
            //    foreach (var tt in t.ListGenerators)
            //    {
            //        foreach (var ttt in tt.ListSettings)
            //        {
            //            Utils.TryCall(
            //                () =>
            //            {
            //                ttt.GeneratorSettings = ttt.VM.SettingsAsJson;
            //            }, "Can't get PROTO settings from Plugin: '" + t.Name + "' Generator: '" + tt.Name + "' Settings: '" + ttt.Name + "'");
            //            if (ttt.IsPrivate)
            //            {
            //                Utils.TryCall(
            //                    () =>
            //                {
            //                    File.WriteAllText(ttt.FilePath, ttt.GeneratorSettings);
            //                }, "Private connection settins was not saved. Plugin: '" + t.Name + "' Generator: '" + tt.Name + "' Settings: '" + ttt.Name + "' File path: '" + ttt.FilePath + "'");
            //                ttt.GeneratorSettings = string.Empty;
            //            }
            //        }
            //    }
            //}
        }

        internal void SavePrepare()
        {
            this._Config.LastUpdated = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);
            var proto = Config.ConvertToProto(this._Config);
            if (this.pconfig_history == null)
            {
                this.pconfig_history = new Proto.Config.proto_config_short_history();
            }

            this.pconfig_history.CurrentConfig = proto;
        }

        public void SaveConfigAsForTests(string file_path)
        {
            this.PluginSettingsToModel();
            this.SavePrepare();
            File.WriteAllBytes(file_path, this.pconfig_history.ToByteArray());
        }

        internal void Save()
        {
            this.PluginSettingsToModel();
            this.SavePrepare();
            Utils.TryCall(
                () =>
            {
                File.WriteAllBytes(CFG_FILE_PATH, this.pconfig_history.ToByteArray());
            }, "Can't save configuration. File path: '" + CFG_FILE_PATH + "'");
            this.ConnectionStringSettingsSave();

            // var json = JsonFormatter.Default.Format(proto);
            // File.WriteAllText(CFG_PATH, json);
#if DEBUG
            // CompareSaved(json);
#endif
        }

        public vCommand CommandConfigSaveAs
        {
            get
            {
                return this._CommandConfigSaveAs ?? (this._CommandConfigSaveAs = vCommand.Create(
                    (o) => { this.SaveAs(); },
                    (o) => { return this.Config != null; }));
            }
        }

        private vCommand _CommandConfigSaveAs;

        internal void SaveAs()
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=netframework-4.8
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "vConfig files (*.vcfg)|*.vcfg|All files (*.*)|*.*";

            // openFileDialog.InitialDirectory = @"c:\temp\";
            // openFileDialog.Multiselect = true;
            if (!string.IsNullOrEmpty(this._FilePathSaveAs))
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(this._FilePathSaveAs);
            }
            if (openFileDialog.ShowDialog() == true)
            {
                this.FilePathSaveAs = openFileDialog.FileName;
                this.PluginSettingsToModel();
                this.SavePrepare();
                Utils.TryCall(
                    () =>
                {
                    File.WriteAllBytes(this.FilePathSaveAs, this.pconfig_history.ToByteArray());
                }, "Can't save configuration. File path: '" + this.FilePathSaveAs + "'");

                // var json = JsonFormatter.Default.Format(Config.ConvertToProto(_Model));
                // File.WriteAllText(FilePathSaveAs, json);
#if DEBUG
                // CompareSaved(json);
#endif
            }
        }

        private void CompareSaved(string json)
        {
            return;

            // KellermanSoftware.CompareNetObjects.CompareLogic compareLogic = new KellermanSoftware.CompareNetObjects.CompareLogic();
            // var model = new Config(json);
            // KellermanSoftware.CompareNetObjects.ComparisonResult result = compareLogic.Compare(this.Config as Config, model as Config);
            // if (!result.AreEqual)
            // {
            //    Console.WriteLine(result.DifferencesString);
            //    throw new Exception();
            // }
        }

        public string FilePathSaveAs
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

        private string _FilePathSaveAs;

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

        public vCommand CommandConfigCreateStableVersion
        {
            get
            {
                return this._CommandConfigCreateStableVersion ?? (this._CommandConfigCreateStableVersion = vCommand.Create(
                (o) => { this.CreateStableVersion(); },
                (o) => { return this.Config != null; }));
            }
        }

        private vCommand _CommandConfigCreateStableVersion;

        private void CreateStableVersion()
        {
            if (this.pconfig_history == null)
            {
                var ex = new NotSupportedException();
                _logger.LogCritical(ex, "".CallerInfo());
                throw ex;
            }

            this.PluginSettingsToModel();

            // todo check if model has DB connected changes. Return if not.
            // todo create migration code
            this._Config.LastUpdated = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);
            var proto = Config.ConvertToProto(this._Config);
            this.pconfig_history.CurrentConfig = proto;
            if (this.pconfig_history.PrevStableConfig != null)
            {
                this.pconfig_history.OldStableConfig = this.pconfig_history.PrevStableConfig.Clone();
                this.Config.OldStableConfig = this.Config.PrevStableConfig;
            }
            this.pconfig_history.PrevStableConfig = this.pconfig_history.CurrentConfig.Clone();
            this.Config.PrevStableConfig = Config.ConvertToVM(this.pconfig_history.CurrentConfig, new Config());
            this.pconfig_history.CurrentConfig.Version++;
            Utils.TryCall(
                () =>
            {
                File.WriteAllBytes(CFG_FILE_PATH, this.pconfig_history.ToByteArray());
            }, "Can't save configuration. File path: '" + CFG_FILE_PATH + "'");
        }

        #endregion Main

        #region ConfigTree

        public vCommand CommandAddNew
        {
            get
            {
                return this._CommandAddNew ?? (this._CommandAddNew = vCommand.Create(
                (o) => { Utils.TryCall(() => { this.Config.SelectedNode.NodeAddNew(); }, "Add new node command"); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanAddNew(); }));
            }
        }

        private vCommand _CommandAddNew;

        public vCommand CommandAddNewChild
        {
            get
            {
                return this._CommandAddNewChild ?? (this._CommandAddNewChild = vCommand.Create(
                (o) => { Utils.TryCall(() => { this.Config.SelectedNode.NodeAddNewSubNode(); }, "Add new sub node command"); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanAddNewSubNode(); }));
            }
        }

        private vCommand _CommandAddNewChild;

        public vCommand CommandAddClone
        {
            get
            {
                return this._CommandAddClone ?? (this._CommandAddClone = vCommand.Create(
                (o) => { this.Config.SelectedNode.NodeAddClone(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanAddClone(); }));
            }
        }

        private vCommand _CommandAddClone;

        public vCommand CommandMoveDown
        {
            get
            {
                return this._CommandMoveDown ?? (this._CommandMoveDown = vCommand.Create(
                (o) => { this.Config.SelectedNode.NodeMoveDown(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanMoveDown(); }));
            }
        }

        private vCommand _CommandMoveDown;

        public vCommand CommandMoveUp
        {
            get
            {
                return this._CommandMoveUp ?? (this._CommandMoveUp = vCommand.Create(
                (o) => { this.Config.SelectedNode.NodeMoveUp(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanMoveUp(); }));
            }
        }

        private vCommand _CommandMoveUp;

        public vCommand CommandDelete
        {
            get
            {
                return this._CommandDelete ?? (this._CommandDelete = vCommand.Create(
                (o) => { this.Config.SelectedNode.NodeRemove(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanRemove(); }));
            }
        }

        private vCommand _CommandDelete;

        public vCommand CommandSelectionLeft
        {
            get
            {
                return this._CommandSelectionLeft ?? (this._CommandSelectionLeft = vCommand.Create(
                (o) => { this.Config.SelectedNode.NodeLeft(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanLeft(); }));
            }
        }

        private vCommand _CommandSelectionLeft;

        public vCommand CommandSelectionRight
        {
            get
            {
                return this._CommandSelectionRight ?? (this._CommandSelectionRight = vCommand.Create(
                (o) => { this.Config.SelectedNode.NodeRight(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanRight(); }));
            }
        }

        private vCommand _CommandSelectionRight;

        public vCommand CommandSelectionDown
        {
            get
            {
                return this._CommandSelectionDown ?? (this._CommandSelectionDown = vCommand.Create(
                (o) => { this.Config.SelectedNode.NodeDown(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanDown(); }));
            }
        }

        private vCommand _CommandSelectionDown;

        public vCommand CommandSelectionUp
        {
            get
            {
                return this._CommandSelectionUp ?? (this._CommandSelectionUp = vCommand.Create(
                (o) => { this.Config.SelectedNode.NodeUp(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanUp(); }));
            }
        }

        private vCommand _CommandSelectionUp;

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
                    this.Config.SelectedNode = (ITreeConfigNode)(o as ValidationMessage).Model;
                },
                (o) => { return this.Config != null; }));
            }
        }

        private vCommand _CommandFromErrorToSelection;

        #region ConnectionString

        // https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configurationmanager?f1url=https%3A%2F%2Fmsdn.microsoft.com%2Fquery%2Fdev16.query%3FappId%3DDev16IDEF1%26l%3DEN-US%26k%3Dk(System.Configuration.ConfigurationManager);k(TargetFrameworkMoniker-.NETFramework,Version%3Dv4.7.2);k(DevLang-csharp)%26rd%3Dtrue&view=netframework-4.8
        // https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configuration?view=netframework-4.8
        internal ConnectionStringSettingsCollection ConnectionStringSettings = null;

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
        // public const string PROVIDER_NAME_SQL = "System.Data.SqlClient";
        // public const string PROVIDER_NAME_SQLITE = "Microsoft.Data.Sqlite";
        // public const string PROVIDER_NAME_MYSQL = "MySql.Data";
        // public const string PROVIDER_NAME_NPGSQL = "Npgsql";
        #endregion
    }
}
