using System;
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
        public ILogger Logger;
        public MainPageVM() : base(MainPageVMValidator.Validator)
        {
        }
        public MainPageVM(bool isLoadConfig, Action<MainPageVM, IEnumerable<Lazy<IvPlugin, IDictionary<string, object>>>> onImportsSatisfied = null, string configFile = null) : base(MainPageVMValidator.Validator)
        {
            this.onImportsSatisfied = onImportsSatisfied;
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                //Catalog c = new Catalog();
                //this.Model = new ConfigRoot();
                //this.Model.Catalogs.ListCatalogs.Add(c);
                return;
            }

            if (App.ServiceCollection == null)
            {
                ILoggerFactory loggerFactory = std.ApplicationLogging.LoggerFactory;
                App.ServiceCollection = new ServiceCollection();
                App.ServiceCollection.Add(ServiceDescriptor.Singleton<ILoggerFactory>(loggerFactory));
            }
            var Services = App.ServiceCollection.BuildServiceProvider();
            Logger = Services.GetRequiredService<ILoggerFactory>().CreateLogger<MainPageVM>();
            Logger.LogInformation("Application is starting.");

            if (isLoadConfig)
            {
                if (configFile != null)
                {
                    this.Config = LoadConfig(configFile, "");
                }
                else if (File.Exists(CFG_FILE_PATH))
                {
                    this.Config = LoadConfig(CFG_FILE_PATH, "");
                }
                else
                {
                    Logger.LogInformation("Creating empty Configuration");
                    this.Config = new Config();
                }
            }
            else
            {
                Logger.LogInformation("Creating empty Configuration");
                this.Config = new Config();
            }
            //this.PathToProjectWithConnectionString = Directory.GetCurrentDirectory();
            //this.Model.OnProviderSelectionChanged = (provider) =>
            //{
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
            //};
            //this.Model.OnProviderSelectionChanged(null);
        }

        private Config LoadConfig(string file_path, string indent)
        {
            Config.IsLoading = true;
            if (!File.Exists(file_path))
                throw new ArgumentException("Configuration data are not found in the file: " + file_path);
            Logger.LogInformation(indent + "Configuration data are found in the file: " + file_path);
            var protoarr = File.ReadAllBytes(file_path);
            pconfig_history = Proto.Config.proto_config_short_history.Parser.ParseFrom(protoarr);
            var cfg = Config.ConvertToVM(pconfig_history.CurrentConfig, new Config());
            string ind2 = indent + "   ";
            foreach (var t in cfg.GroupConfigs.ListBaseConfigs.ToList())
            {
                t.Config = LoadConfig(t.RelativeConfigFilePath + CFG_FILE_NAME, ind2);
                t.Name = t.Config.Name;
            }
            //string json = File.ReadAllText(CFG_PATH);
            //this.Model = new Config(json);
            Config.IsLoading = false;
            return cfg;
        }
        public Proto.Config.proto_config_short_history pconfig_history { get; private set; }
        public static readonly string CFG_FILE_PATH = @".\current.vcfg";
        public static readonly string CFG_FILE_NAME = "current.vcfg";
        public DiffModel GetDiffModel()
        {
            DiffModel res = new DiffModel(
                pconfig_history?.OldStableConfig == null ? null : Config.ConvertToVM(pconfig_history.OldStableConfig, new Config()),
                pconfig_history?.PrevStableConfig == null ? null : Config.ConvertToVM(pconfig_history.PrevStableConfig, new Config()),
                this.Config
            );
            return res;
        }
        //internal void OnSelectedItemChanged(object oldValue, object newValue)
        //{
        //    this.Model.SelectedNode = (ITreeConfigNode)newValue;
        //}
        public static Config ConfigInstance;

        #region Plugins

        // https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-4.0/ff603380(v=vs.100) // debug
        // https://www.codeproject.com/Articles/376033/From-Zero-to-Proficient-with-MEF
        // https://docs.microsoft.com/en-us/dotnet/framework/mef/
        [ImportMany(typeof(IvPlugin))]
        IEnumerable<Lazy<IvPlugin, IDictionary<string, object>>> _plugins;
        Action<MainPageVM, IEnumerable<Lazy<IvPlugin, IDictionary<string, object>>>> onImportsSatisfied = null;
        public void OnImportsSatisfied()
        {
            Logger.LogInformation("Loaded " + _plugins.Count() + " plugins");
            if (onImportsSatisfied != null)
                onImportsSatisfied(this, _plugins);
            List<PluginRow> lstGens = new List<PluginRow>();
            //List<IvPluginDbGenerator> lstDbs = new List<IvPluginDbGenerator>();
            foreach (var t in _plugins)
            {
                Plugin p = null;
                bool is_found = false;
                // attaching plugin
                foreach (var tt in this.Config.GroupPlugins.ListPlugins)
                {
                    if (tt.Guid == t.Value.Guid && (string.IsNullOrWhiteSpace(tt.Version) || tt.Version == t.Value.Version))
                    {
                        tt.SetVPlugin(t.Value);
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
                                Utils.TryCall(() =>
                                {
                                    tttt.GeneratorSettings = File.ReadAllText(tttt.FilePath);
                                }, "Private connection settins was not loaded. Plugin: '" + p.Name + "' Generator: '" + ttt.Name + "' Connection settings name: '" + tttt.Name + "' File path: '" + tttt.FilePath + "'");
                            }
                            else
                            {
                                Utils.TryCall(() =>
                                {
                                    tttt.SetVM(ttt.Generator.GetSettingsMvvm(tttt.GeneratorSettings));
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
                        lst.Add(tt);
                }
                dic[gt] = lst;
            }
            if (this.DicPlugins != null)
                this.DicPlugins.Clear();
            this.DicPlugins = dic;
            this.Config.DicPlugins = dic;
        }
        public Dictionary<vPluginLayerTypeEnum, List<PluginRow>> DicPlugins
        {
            get { return _DicPlugins; }
            set
            {
                _DicPlugins = value;
                NotifyPropertyChanged();
            }
        }
        public Dictionary<vPluginLayerTypeEnum, List<PluginRow>> _DicPlugins;
        public List<IvPluginDbGenerator> ListDbDesignPlugins
        {
            get { return _ListDbDesignPlugins; }
            set
            {
                _ListDbDesignPlugins = value;
                NotifyPropertyChanged();
            }
        }
        public List<IvPluginDbGenerator> _ListDbDesignPlugins;
        public PluginRow SelectedDbDesignPlugin
        {
            get { return _SelectedDbDesignPlugin; }
            set
            {
                _SelectedDbDesignPlugin = value;
                NotifyPropertyChanged();
                //this.Model.GroupSettings.
                //var propvm = _SelectedDbDesignPlugin.GetSettingsMvvm()
            }
        }
        private PluginRow _SelectedDbDesignPlugin;
        public INotifyPropertyChanged SelectedDbDesignPluginSettings
        {
            get { return _SelectedDbDesignPluginSettings; }
            set
            {
                _SelectedDbDesignPluginSettings = value;
                NotifyPropertyChanged();
                //InitConnectionString();
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
                    catalog.Catalogs.Add(dirCatalog);
                AgregateCatalogs(t, search, catalog);
            }
        }
        public void Compose()
        {
            Logger.LogInformation("Loading plugins");
            AggregateCatalog catalog = new AggregateCatalog();
            AgregateCatalogs(Directory.GetCurrentDirectory() + "\\Plugins", "vPlugin*.dll", catalog);
            CompositionContainer container = new CompositionContainer(catalog);
            container.SatisfyImportsOnce(this);
        }

        #endregion Plugins

        public Config Config
        {
            set
            {
                _Config = value;
                MainPageVM.ConfigInstance = value;
                NotifyPropertyChanged();
                ValidateProperty();
                _Config.OnSelectedNodeChanged = () =>
                {
                    CommandAddNew.RaiseCanExecuteChanged();
                    CommandAddNewChild.RaiseCanExecuteChanged();
                    CommandAddClone.RaiseCanExecuteChanged();
                    CommandMoveDown.RaiseCanExecuteChanged();
                    CommandMoveUp.RaiseCanExecuteChanged();
                    CommandDelete.RaiseCanExecuteChanged();
                    CommandSelectionLeft.RaiseCanExecuteChanged();
                    CommandSelectionRight.RaiseCanExecuteChanged();
                    CommandSelectionDown.RaiseCanExecuteChanged();
                    CommandSelectionUp.RaiseCanExecuteChanged();
                    _Config.ValidateSubTreeFromNode(_Config.SelectedNode);
                };
            }
            get { return _Config; }
        }
        private Config _Config;

        public Microsoft.EntityFrameworkCore.Metadata.IMutableModel GetEfModel()
        {
            Migration.ConfigToModelVisitor visitor = new Migration.ConfigToModelVisitor();
            this.Config.AcceptConfigNodeVisitor(visitor);
            return visitor.Result;
        }

        #region Main

        public vCommand CommandConfigSave
        {
            get
            {
                return _CommandConfigSave ?? (_CommandConfigSave = vCommand.Create(
                    (o) => { this.Save(); },
                    (o) => { return this.Config != null; }));
            }
        }
        private vCommand _CommandConfigSave;
        private void PluginSettingsToModel()
        {
            foreach (var t in _Config.GroupPlugins.ListPlugins)
            {
                foreach (var tt in t.ListGenerators)
                {
                    foreach (var ttt in tt.ListSettings)
                    {
                        Utils.TryCall(() =>
                        {
                            ttt.GeneratorSettings = ttt.VM.Settings;
                        }, "Can't get PROTO settings from Plugin: '" + t.Name + "' Generator: '" + tt.Name + "' Settings: '" + ttt.Name + "'");
                        if (ttt.IsPrivate)
                        {
                            Utils.TryCall(() =>
                            {
                                File.WriteAllText(ttt.FilePath, ttt.GeneratorSettings);
                            }, "Private connection settins was not saved. Plugin: '" + t.Name + "' Generator: '" + tt.Name + "' Settings: '" + ttt.Name + "' File path: '" + ttt.FilePath + "'");
                            ttt.GeneratorSettings = "";
                        }
                    }
                }
            }
        }
        internal void SavePrepare()
        {
            _Config.LastUpdated = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);
            var proto = Config.ConvertToProto(_Config);
            if (this.pconfig_history == null)
                this.pconfig_history = new Proto.Config.proto_config_short_history();
            this.pconfig_history.CurrentConfig = proto;
        }
        public void SaveConfigAsForTests(string file_path)
        {
            PluginSettingsToModel();
            SavePrepare();
            File.WriteAllBytes(file_path, this.pconfig_history.ToByteArray());
        }
        internal void Save()
        {
            PluginSettingsToModel();
            SavePrepare();
            Utils.TryCall(() =>
            {
                File.WriteAllBytes(CFG_FILE_PATH, this.pconfig_history.ToByteArray());
            }, "Can't save configuration. File path: '" + CFG_FILE_PATH + "'");
            this.ConnectionStringSettingsSave();
            //var json = JsonFormatter.Default.Format(proto);
            //File.WriteAllText(CFG_PATH, json);
#if DEBUG
            //CompareSaved(json);
#endif
        }
        public vCommand CommandConfigSaveAs
        {
            get
            {
                return _CommandConfigSaveAs ?? (_CommandConfigSaveAs = vCommand.Create(
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
            //openFileDialog.InitialDirectory = @"c:\temp\";
            //openFileDialog.Multiselect = true;
            if (!string.IsNullOrEmpty(_FilePathSaveAs))
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(_FilePathSaveAs);
            }
            if (openFileDialog.ShowDialog() == true)
            {
                FilePathSaveAs = openFileDialog.FileName;
                PluginSettingsToModel();
                SavePrepare();
                Utils.TryCall(() =>
                {
                    File.WriteAllBytes(FilePathSaveAs, this.pconfig_history.ToByteArray());
                }, "Can't save configuration. File path: '" + FilePathSaveAs + "'");
                //var json = JsonFormatter.Default.Format(Config.ConvertToProto(_Model));
                //File.WriteAllText(FilePathSaveAs, json);
#if DEBUG
                //CompareSaved(json);
#endif
            }
        }
        private void CompareSaved(string json)
        {
            return;

            //KellermanSoftware.CompareNetObjects.CompareLogic compareLogic = new KellermanSoftware.CompareNetObjects.CompareLogic();
            //var model = new Config(json);
            //KellermanSoftware.CompareNetObjects.ComparisonResult result = compareLogic.Compare(this.Config as Config, model as Config);
            //if (!result.AreEqual)
            //{
            //    Console.WriteLine(result.DifferencesString);
            //    throw new Exception();
            //}
        }

        public string FilePathSaveAs
        {
            get { return _FilePathSaveAs; }
            set
            {
                _FilePathSaveAs = value;
                NotifyPropertyChanged();
                SaveToolTip = _saveBaseToolTip + " as " + _FilePathSaveAs;
            }
        }
        private string _FilePathSaveAs;
        public string SaveToolTip
        {
            get { return _SaveToolTip; }
            set
            {
                _SaveToolTip = value;
                NotifyPropertyChanged();
            }
        }
        private string _SaveToolTip = _saveBaseToolTip;
        private const string _saveBaseToolTip = "Ctrl-S - save config";
        public vCommand CommandConfigCreateStableVersion
        {
            get
            {
                return _CommandConfigCreateStableVersion ?? (_CommandConfigCreateStableVersion = vCommand.Create(
                (o) => { this.CreateStableVersion(); },
                (o) => { return this.Config != null; }));
            }
        }
        private vCommand _CommandConfigCreateStableVersion;

        private void CreateStableVersion()
        {
            if (pconfig_history == null)
                return;
            PluginSettingsToModel();
            //todo check if model has DB connected changes. Return if not.

            //todo create migration code

            _Config.LastUpdated = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);
            var proto = Config.ConvertToProto(_Config);
            pconfig_history.CurrentConfig = proto;
            if (pconfig_history.PrevStableConfig != null)
            {
                pconfig_history.OldStableConfig = pconfig_history.PrevStableConfig.Clone();
            }
            pconfig_history.PrevStableConfig = pconfig_history.CurrentConfig.Clone();
            pconfig_history.CurrentConfig.Version++;
            Utils.TryCall(() =>
            {
                File.WriteAllBytes(CFG_FILE_PATH, pconfig_history.ToByteArray());
            }, "Can't save configuration. File path: '" + CFG_FILE_PATH + "'");
        }

        #endregion Main

        #region ConfigTree

        public vCommand CommandAddNew
        {
            get
            {
                return _CommandAddNew ?? (_CommandAddNew = vCommand.Create(
                (o) => { Utils.TryCall(() => { this.Config.SelectedNode.NodeAddNew(); }, "Add new node command"); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanAddNew(); }));
            }
        }
        private vCommand _CommandAddNew;
        public vCommand CommandAddNewChild
        {
            get
            {
                return _CommandAddNewChild ?? (_CommandAddNewChild = vCommand.Create(
                (o) => { Utils.TryCall(() => { this.Config.SelectedNode.NodeAddNewSubNode(); }, "Add new sub node command"); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanAddNewSubNode(); }));
            }
        }
        private vCommand _CommandAddNewChild;
        public vCommand CommandAddClone
        {
            get
            {
                return _CommandAddClone ?? (_CommandAddClone = vCommand.Create(
                (o) => { this.Config.SelectedNode.NodeAddClone(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanAddClone(); }));
            }
        }
        private vCommand _CommandAddClone;
        public vCommand CommandMoveDown
        {
            get
            {
                return _CommandMoveDown ?? (_CommandMoveDown = vCommand.Create(
                (o) => { this.Config.SelectedNode.NodeMoveDown(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanMoveDown(); }));
            }
        }
        private vCommand _CommandMoveDown;
        public vCommand CommandMoveUp
        {
            get
            {
                return _CommandMoveUp ?? (_CommandMoveUp = vCommand.Create(
                (o) => { this.Config.SelectedNode.NodeMoveUp(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanMoveUp(); }));
            }
        }
        private vCommand _CommandMoveUp;
        public vCommand CommandDelete
        {
            get
            {
                return _CommandDelete ?? (_CommandDelete = vCommand.Create(
                (o) => { this.Config.SelectedNode.NodeRemove(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanRemove(); }));
            }
        }
        private vCommand _CommandDelete;
        public vCommand CommandSelectionLeft
        {
            get
            {
                return _CommandSelectionLeft ?? (_CommandSelectionLeft = vCommand.Create(
                (o) => { this.Config.SelectedNode.NodeLeft(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanLeft(); }));
            }
        }
        private vCommand _CommandSelectionLeft;
        public vCommand CommandSelectionRight
        {
            get
            {
                return _CommandSelectionRight ?? (_CommandSelectionRight = vCommand.Create(
                (o) => { this.Config.SelectedNode.NodeRight(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanRight(); }));
            }
        }
        private vCommand _CommandSelectionRight;
        public vCommand CommandSelectionDown
        {
            get
            {
                return _CommandSelectionDown ?? (_CommandSelectionDown = vCommand.Create(
                (o) => { this.Config.SelectedNode.NodeDown(); },
                (o) => { return this.Config != null && this.Config.SelectedNode != null && this.Config.SelectedNode.NodeCanDown(); }));
            }
        }
        private vCommand _CommandSelectionDown;
        public vCommand CommandSelectionUp
        {
            get
            {
                return _CommandSelectionUp ?? (_CommandSelectionUp = vCommand.Create(
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
                return _CommandFromErrorToSelection ?? (_CommandFromErrorToSelection = vCommand.Create(
                (o) => { if (o == null) return; this.Config.SelectedNode = (ITreeConfigNode)(o as ValidationMessage).Model; },
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
            //var settings = configFile.AppSettings.Settings;
            //if (settings[key] == null)
            //{
            //    settings.Add(key, value);
            //}
            //else
            //{
            //    settings[key].Value = value;
            //}
            Utils.TryCall(() =>
            {
                configFile.Save(ConfigurationSaveMode.Modified);
            }, "Error writing app settings");
            //ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }

        //string GetConnectionString(ref string connectionStringName, out string providerName)
        //{
        //    providerName = null;

        //    string result = "";
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


        //    var config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
        //    var connSection = config.ConnectionStrings;

        //    //if the connectionString is empty - which is the defauls
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

        //    //	if (String.IsNullOrEmpty(providerName))
        //    //		providerName="System.Data.SqlClient";

        //    return result;
        //}
        //string GetConnectionString(out string providerName)
        //{
        //    providerName = null;

        //    string result = "";
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


        //    var config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
        //    var connSection = config.ConnectionStrings;

        //    try
        //    {
        //        result = connSection.ConnectionStrings[this.SelectedDbDesignPlugin.Name + "Admin"].ConnectionString;
        //        providerName = connSection.ConnectionStrings[this.SelectedDbDesignPlugin.Name + "Admin"].ProviderName;
        //    }
        //    catch
        //    {
        //        result = "There is no connection string name called '" + this.SelectedDbDesignPlugin.Name + "Admin" + "'";
        //    }

        //    //	if (String.IsNullOrEmpty(providerName))
        //    //		providerName="System.Data.SqlClient";

        //    return result;
        //}
        //public string PathToProjectWithConnectionString
        //{
        //    set
        //    {
        //        if (_PathToProjectWithConnectionString != value)
        //        {
        //            _PathToProjectWithConnectionString = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //    get { return _PathToProjectWithConnectionString; }
        //}
        //private string _PathToProjectWithConnectionString = "";
        //public string ConnectionString
        //{
        //    get { return _ConnectionString; }
        //    set
        //    {
        //        _ConnectionString = value;
        //        NotifyPropertyChanged();
        //    }
        //}
        //private string _ConnectionString;
        //void InitConnectionString()
        //{
        //    this.ConnectionString = GetConnectionString(out _providerName);
        //    // https://www.connectionstrings.com/sqlconnection/
        //    if (this.ConnectionString != null && this.ConnectionString.Contains("|DataDirectory|"))
        //    {
        //        //have to replace it
        //        string dataFilePath = this.PathToProjectWithConnectionString + "\\App_Data\\";
        //        this.ConnectionString = this.ConnectionString.Replace("|DataDirectory|", dataFilePath);
        //    }
        //}
        //public string ProviderName
        //{
        //    get
        //    {
        //        InitConnectionString();
        //        return _providerName;
        //    }
        //}
        //string _providerName = "";
        //public const string PROVIDER_NAME_SQL = "System.Data.SqlClient";
        //public const string PROVIDER_NAME_SQLITE = "Microsoft.Data.Sqlite";
        //public const string PROVIDER_NAME_MYSQL = "MySql.Data";
        //public const string PROVIDER_NAME_NPGSQL = "Npgsql";
        #endregion
    }
}
