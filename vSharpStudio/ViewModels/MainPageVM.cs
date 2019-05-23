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
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.std;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.ViewModels
{
    public class MainPageVM : ViewModelValidatableWithSeverity<MainPageVM, MainPageVMValidator>, IPartImportsSatisfiedNotification
    {
        public static ILogger Logger = ApplicationLogging.CreateLogger<MainPageVM>();
        public MainPageVM(bool isLoadConfig = true, Action<MainPageVM, IEnumerable<Lazy<IvPlugin, IDictionary<string, object>>>> onImportsSatisfied = null) : base(MainPageVMValidator.Validator)
        {
            this.onImportsSatisfied = onImportsSatisfied;
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                //Catalog c = new Catalog();
                //this.Model = new ConfigRoot();
                //this.Model.Catalogs.ListCatalogs.Add(c);
                return;
            }
            if (isLoadConfig && File.Exists(CFG_PATH))
            {
                string json = File.ReadAllText(CFG_PATH);
                this.Model = new Config(json);
            }
            else
                this.Model = new Config();
            this.PathToProjectWithConnectionString = Directory.GetCurrentDirectory();
        }
        private const string CFG_PATH = @".\current.vcfg";
        //internal void OnSelectedItemChanged(object oldValue, object newValue)
        //{
        //    this.Model.SelectedNode = (ITreeConfigNode)newValue;
        //}
        public static Config ConfigInstance;

        // https://www.codeproject.com/Articles/376033/From-Zero-to-Proficient-with-MEF
        // https://docs.microsoft.com/en-us/dotnet/framework/mef/
        [ImportMany(typeof(IvPlugin))]
        IEnumerable<Lazy<IvPlugin, IDictionary<string, object>>> _plugins;
        Action<MainPageVM, IEnumerable<Lazy<IvPlugin, IDictionary<string, object>>>> onImportsSatisfied = null;
        public void OnImportsSatisfied()
        {
            if (onImportsSatisfied != null)
                onImportsSatisfied(this, _plugins);
            List<IDbMigrator> lstDbs = new List<IDbMigrator>();
            foreach (var t in _plugins)
            {
                if (t is IDbMigrator)
                    lstDbs.Add((IDbMigrator)t.Value);
            }
            this.ListDbTypes = lstDbs;
        }
        public List<IDbMigrator> ListDbTypes
        {
            get { return _ListDbTypes; }
            set
            {
                _ListDbTypes = value;
                NotifyPropertyChanged();
            }
        }
        public List<IDbMigrator> _ListDbTypes;
        public IDbMigrator SelectedDbType
        {
            get { return _SelectedDbType; }
            set
            {
                _SelectedDbType = value;
                NotifyPropertyChanged();
                //InitConnectionString();
            }
        }
        private IDbMigrator _SelectedDbType;

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
            AggregateCatalog catalog = new AggregateCatalog();
            AgregateCatalogs(Directory.GetCurrentDirectory() + "\\Plugins", "vPlugin*.dll", catalog);
            CompositionContainer container = new CompositionContainer(catalog);
            container.SatisfyImportsOnce(this);


            //AssemblyCatalog catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
            //DirectoryCatalog dirCatalog = new DirectoryCatalog("Plugins", "*.dll");
            //AssemblyCatalog assemblyCat = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
            //TypeCatalog catalog = new TypeCatalog((typeof(IvPlugin));
            //AggregateCatalog catalog = new AggregateCatalog(assemblyCat, dirCatalog);
            //CompositionContainer container = new CompositionContainer(dirCatalog);
            //container.SatisfyImportsOnce(this);
            //container.ComposeParts(this);

            //dirCatalog = new DirectoryCatalog(@"C:\Temp");
            //AssemblyCatalog assemblyCat = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
            //AggregateCatalog catalog = new AggregateCatalog(assemblyCat, dirCatalog);
            //CompositionContainer container = new CompositionContainer(catalog);
            //container.ComposeParts(this);
        }

        //List<IvPlugin> ListDbMigrators
        //{
        //    get { return _ListDbMigrators; }
        //    set
        //    {
        //        _ListDbMigrators = value;
        //        NotifyPropertyChanged();
        //    }
        //}
        //List<IvPlugin> _ListDbMigrators;
        public Config Model
        {
            set
            {
                _Model = value;
                MainPageVM.ConfigInstance = value;
                NotifyPropertyChanged();
                ValidateProperty();
                _Model.OnSelectedNodeChanged = () =>
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
                    _Model.ValidateSubTreeFromNode(_Model.SelectedNode);
                };
            }
            get { return _Model; }
        }
        private Config _Model;

        public Microsoft.EntityFrameworkCore.Metadata.IMutableModel GetEfModel()
        {
            Migration.ConfigToModelVisitor visitor = new Migration.ConfigToModelVisitor();
            this.Model.AcceptConfigNode(visitor);
            return visitor.Result;
        }

        #region Main

        public vCommand CommandConfigSave
        {
            get
            {
                return _CommandConfigSave ?? (_CommandConfigSave = vCommand.Create(
                (o) => { this.Save(); },
                (o) => { return this.Model != null; }));
            }
        }
        private vCommand _CommandConfigSave;
        internal void Save()
        {
            var json = JsonFormatter.Default.Format(Config.ConvertToProto(_Model));
            File.WriteAllText(CFG_PATH, json);
#if DEBUG
            CompareSaved(json);
#endif
        }
        public vCommand CommandConfigSaveAs
        {
            get
            {
                return _CommandConfigSaveAs ?? (_CommandConfigSaveAs = vCommand.Create(
                (o) => { this.SaveAs(); },
                (o) => { return this.Model != null; }));
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
                var json = JsonFormatter.Default.Format(Config.ConvertToProto(_Model));
                File.WriteAllText(FilePathSaveAs, json);
#if DEBUG
                CompareSaved(json);
#endif
            }
        }

        private void CompareSaved(string json)
        {
            return;

            KellermanSoftware.CompareNetObjects.CompareLogic compareLogic = new KellermanSoftware.CompareNetObjects.CompareLogic();
            var model = new Config(json);
            KellermanSoftware.CompareNetObjects.ComparisonResult result = compareLogic.Compare(this.Model as Config, model as Config);
            if (!result.AreEqual)
            {
                Console.WriteLine(result.DifferencesString);
                throw new Exception();
            }
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

        #endregion Main

        #region ConfigTree

        public vCommand CommandAddNew
        {
            get
            {
                return _CommandAddNew ?? (_CommandAddNew = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeAddNew(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanAddNew(); }));
            }
        }
        private vCommand _CommandAddNew;
        public vCommand CommandAddNewChild
        {
            get
            {
                return _CommandAddNewChild ?? (_CommandAddNewChild = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeAddNewSubNode(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanAddNewSubNode(); }));
            }
        }
        private vCommand _CommandAddNewChild;
        public vCommand CommandAddClone
        {
            get
            {
                return _CommandAddClone ?? (_CommandAddClone = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeAddClone(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanAddClone(); }));
            }
        }
        private vCommand _CommandAddClone;
        public vCommand CommandMoveDown
        {
            get
            {
                return _CommandMoveDown ?? (_CommandMoveDown = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeMoveDown(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanMoveDown(); }));
            }
        }
        private vCommand _CommandMoveDown;
        public vCommand CommandMoveUp
        {
            get
            {
                return _CommandMoveUp ?? (_CommandMoveUp = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeMoveUp(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanMoveUp(); }));
            }
        }
        private vCommand _CommandMoveUp;
        public vCommand CommandDelete
        {
            get
            {
                return _CommandDelete ?? (_CommandDelete = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeRemove(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanRemove(); }));
            }
        }
        private vCommand _CommandDelete;
        public vCommand CommandSelectionLeft
        {
            get
            {
                return _CommandSelectionLeft ?? (_CommandSelectionLeft = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeLeft(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanLeft(); }));
            }
        }
        private vCommand _CommandSelectionLeft;
        public vCommand CommandSelectionRight
        {
            get
            {
                return _CommandSelectionRight ?? (_CommandSelectionRight = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeRight(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanRight(); }));
            }
        }
        private vCommand _CommandSelectionRight;
        public vCommand CommandSelectionDown
        {
            get
            {
                return _CommandSelectionDown ?? (_CommandSelectionDown = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeDown(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanDown(); }));
            }
        }
        private vCommand _CommandSelectionDown;
        public vCommand CommandSelectionUp
        {
            get
            {
                return _CommandSelectionUp ?? (_CommandSelectionUp = vCommand.Create(
                (o) => { this.Model.SelectedNode.NodeUp(); },
                (o) => { return this.Model != null && this.Model.SelectedNode != null && this.Model.SelectedNode.NodeCanUp(); }));
            }
        }
        private vCommand _CommandSelectionUp;

        #endregion ConfigTree
        public vCommand CommandFromErrorToSelection
        {
            get
            {
                return _CommandFromErrorToSelection ?? (_CommandFromErrorToSelection = vCommand.Create(
                (o) => { if (o == null) return; this.Model.SelectedNode = (ITreeConfigNode)(o as ValidationMessage).Model; },
                (o) => { return this.Model != null; }));
            }
        }
        private vCommand _CommandFromErrorToSelection;

        #region ConnectionString
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
        string GetConnectionString(out string providerName)
        {
            providerName = null;

            string result = "";
            ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
            string configPath = this.PathToProjectWithConnectionString + @"\App.config";
            if (File.Exists(configPath))
            {
                configFile.ExeConfigFilename = configPath;
            }
            else
            {
                configPath = this.PathToProjectWithConnectionString + @"\Web.config";
                if (File.Exists(configPath))
                {
                    configFile.ExeConfigFilename = configPath;
                }
            }
            if (string.IsNullOrEmpty(configFile.ExeConfigFilename))
                throw new ArgumentNullException("The project does not contain App.config or Web.config file.");


            var config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
            var connSection = config.ConnectionStrings;

            try
            {
                result = connSection.ConnectionStrings[this.SelectedDbType.Name + "Admin"].ConnectionString;
                providerName = connSection.ConnectionStrings[this.SelectedDbType.Name + "Admin"].ProviderName;
            }
            catch
            {
                result = "There is no connection string name called '" + this.SelectedDbType.Name + "Admin" + "'";
            }

            //	if (String.IsNullOrEmpty(providerName))
            //		providerName="System.Data.SqlClient";

            return result;
        }
        public string PathToProjectWithConnectionString
        {
            set
            {
                if (_PathToProjectWithConnectionString != value)
                {
                    _PathToProjectWithConnectionString = value;
                    NotifyPropertyChanged();
                }
            }
            get { return _PathToProjectWithConnectionString; }
        }
        private string _PathToProjectWithConnectionString = "";
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set
            {
                _ConnectionString = value;
                NotifyPropertyChanged();
            }
        }
        private string _ConnectionString;
        void InitConnectionString()
        {
            this.ConnectionString = GetConnectionString(out _providerName);
            // https://www.connectionstrings.com/sqlconnection/
            if (this.ConnectionString != null && this.ConnectionString.Contains("|DataDirectory|"))
            {
                //have to replace it
                string dataFilePath = this.PathToProjectWithConnectionString + "\\App_Data\\";
                this.ConnectionString = this.ConnectionString.Replace("|DataDirectory|", dataFilePath);
            }
        }
        public string ProviderName
        {
            get
            {
                InitConnectionString();
                return _providerName;
            }
        }
        string _providerName = "";
        public const string PROVIDER_NAME_SQL = "System.Data.SqlClient";
        public const string PROVIDER_NAME_SQLITE = "Microsoft.Data.Sqlite";
        public const string PROVIDER_NAME_MYSQL = "MySql.Data";
        public const string PROVIDER_NAME_NPGSQL = "Npgsql";
        #endregion
    }
}
