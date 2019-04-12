using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.Migration;
using vSharpStudio.std;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.ViewModels
{
    //TODO report based on FlowDocument https://github.com/rodrigovedovato/FlowDocumentReporting
    public class ConfigRoot : Config
    {
        public const string PROVIDER_NAME_SQL = "System.Data.SqlClient";
        public const string PROVIDER_NAME_SQLITE = "Microsoft.Data.Sqlite";
        public const string PROVIDER_NAME_MYSQL = "MySql.Data";
        public const string PROVIDER_NAME_NPGSQL = "Npgsql";
        public static ILogger Logger = ApplicationLogging.CreateLogger<ConfigRoot>();
        public ConfigRoot()
        {
            // https://msdn.microsoft.com/en-us/magazine/mt830355.aspx
            // https://msdn.microsoft.com/en-us/magazine/mt694089.aspx
            Logger.LogInformation("");
        }
        public ConfigRoot(string pathToProjectWithConnectionString, string connectionStringName)
        {
            this.PathToProjectWithConnectionString = pathToProjectWithConnectionString;
            this.ConnectionStringName = connectionStringName;
            //InitMigration();
        }
        public ConfigRoot(string configJson) : base(configJson)
        {
            //InitMigration();
        }
        public void InitMigration()
        {
            // https://docs.microsoft.com/en-us/ef/core/providers/
            // https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/obtaining-a-dbproviderfactory ???
            switch (this.ProviderName)
            {
                case ConfigRoot.PROVIDER_NAME_SQL:
                    _migration = new MsSqlServerMigration(this);
                    break;
#if DEBUG
                //case ConfigRoot.PROVIDER_NAME_SQLITE:
                //    _migration = new DbModel.Sqlite.SqliteMigration(this);
                //    break;
                //case ConfigRoot.PROVIDER_NAME_MYSQL:
                //    _migration = new DbModel.MySql.MySqlMigration(this);
                //    break;
                //case ConfigRoot.PROVIDER_NAME_NPGSQL:
                //    _migration = new DbModel.Postgre.NpgsqlMigration(this);
                //    break;
#endif
                default:
                    throw new ArgumentException("Unsupported ProviderName in connection string: " + this.ProviderName);
            }
        }

        #region Full Validation

        //async public Task ValidateSubTreeFromNode(IAccept node)
        //{
        //    if (cancellationSourceForValidatingFullConfig != null)
        //    {
        //        cancellationSourceForValidatingFullConfig.Cancel();
        //    }
        //    this.cancellationSourceForValidatingFullConfig = new CancellationTokenSource();
        //    var token = cancellationSourceForValidatingFullConfig.Token;
        //    // https://msdn.microsoft.com/en-us/magazine/jj991977.aspx
        //    // https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap
        //    // https://devblogs.microsoft.com/pfxteam/asynclazyt/
        //    // https://github.com/StephenCleary/AsyncEx
        //    // https://msdn.microsoft.com/en-us/magazine/dn818493.aspx
        //    try
        //    {
        //        var res = await ValidateSubTreeFromNode(node, token).ConfigureAwait(continueOnCapturedContext: false);
        //        if (!token.IsCancellationRequested)
        //            ListValidationMessages = res;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //Task<SortedObservableCollection<ValidationMessage>> ValidateSubTreeFromNode(IAccept node, CancellationToken cancellationToken = default)
        //{
        //    var task = new Task<SortedObservableCollection<ValidationMessage>>(() =>
        //    {
        //        var visitor = new TreeNodeValidatorVisitor(cancellationToken);
        //        node.Accept(visitor);
        //        return visitor.Result;
        //    });
        //    return task;
        //}

        private CancellationTokenSource cancellationSourceForValidatingFullConfig = null;
        public async Task ValidateSubTreeFromNodeAsync(ITreeConfigNode node)
        {
            // https://msdn.microsoft.com/en-us/magazine/jj991977.aspx
            // https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap
            // https://devblogs.microsoft.com/pfxteam/asynclazyt/
            // https://github.com/StephenCleary/AsyncEx
            // https://msdn.microsoft.com/en-us/magazine/dn818493.aspx
            await Task.Run(() =>
            {
                ValidateSubTreeFromNode(node);
            }).ConfigureAwait(false); // not keeping context because doing nothing after await
        }
        public void ValidateSubTreeFromNode(ITreeConfigNode node, ILogger logger = null)
        {
            if (cancellationSourceForValidatingFullConfig != null)
            {
                cancellationSourceForValidatingFullConfig.Cancel();
                logger.LogInformation("=== Cancellation request ===");
            }
            this.cancellationSourceForValidatingFullConfig = new CancellationTokenSource();
            var token = cancellationSourceForValidatingFullConfig.Token;

            var visitor = new TreeNodeValidatorVisitor(token, node, logger);
            this.Accept(visitor);
            if (!token.IsCancellationRequested)
            {
                // update for UI from another Thread (if from async version) (it is not only update, many others including CountErrors, CountWarnings ...
                this.ListAllValidationMessages = visitor.Result; 
            }
            else
            {
                logger.LogInformation("=== Cancelled ===");
            }
        }
        public SortedObservableCollection<ValidationMessage> ListAllValidationMessages
        {
            set
            {
                if (_ListAllValidationMessages != null)
                    _ListAllValidationMessages.Clear();
                _ListAllValidationMessages = value;
                NotifyPropertyChanged();
            }
            get { return _ListAllValidationMessages; }
        }
        private SortedObservableCollection<ValidationMessage> _ListAllValidationMessages = null;

        #endregion Full Validation

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

            return null;

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
                result = connSection.ConnectionStrings[this.ConnectionStringName].ConnectionString;
                providerName = connSection.ConnectionStrings[this.ConnectionStringName].ProviderName;
            }
            catch
            {
                result = "There is no connection string name called '" + this.ConnectionStringName + "'";
            }

            //	if (String.IsNullOrEmpty(providerName))
            //		providerName="System.Data.SqlClient";

            return result;
        }

        void InitConnectionString()
        {
            if (String.IsNullOrEmpty(this.ConnectionString))
            {
                this.ConnectionString = GetConnectionString(out _providerName);
                // https://www.connectionstrings.com/sqlconnection/
                if (this.ConnectionString.Contains("|DataDirectory|"))
                {
                    //have to replace it
                    string dataFilePath = this.PathToProjectWithConnectionString + "\\App_Data\\";
                    this.ConnectionString = this.ConnectionString.Replace("|DataDirectory|", dataFilePath);
                }
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
        #endregion
    }
}
