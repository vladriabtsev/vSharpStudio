using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using vSharpStudio.vm.Migration;

namespace vSharpStudio.Migration
{
    public class DbShemaReader
    {
        public const string PROVIDERNAMESQL = "System.Data.SqlClient";
        public const string PROVIDERNAMESQLITE = "Microsoft.Data.Sqlite";
        private IDatabaseModelFactoryExt _modelFactory;
        private string _pathToProjectWithConnectionString;
        private string _connectionStringName;
        //TODO choose DB provider automatically
        public DbShemaReader(string pathToProjectWithConnectionString, string connectionStringName)
        {
            //Check.NotNull(pathToProjectWithConnectionString, nameof(pathToProjectWithConnectionString));
            //Check.NotNull(connectionStringName, nameof(connectionStringName));

            this._pathToProjectWithConnectionString = pathToProjectWithConnectionString;
            this._connectionStringName = connectionStringName;
        }
        string GetConnectionString(ref string connectionStringName, out string providerName)
        {
            providerName = null;

            string result = "";
            ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
            string configPath = this._pathToProjectWithConnectionString + @"\App.config";
            if (File.Exists(configPath))
            {
                configFile.ExeConfigFilename = configPath;
            }
            else
            {
                configPath = this._pathToProjectWithConnectionString + @"\Web.config";
                if (File.Exists(configPath))
                {
                    configFile.ExeConfigFilename = configPath;
                }
            }
            if (string.IsNullOrEmpty(configFile.ExeConfigFilename))
                throw new ArgumentNullException("The project does not contain App.config or Web.config file.");


            var config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
            var connSection = config.ConnectionStrings;

            //if the connectionString is empty - which is the defauls
            //look for count-1 - this is the last connection string
            //and takes into account AppServices and LocalSqlServer
            if (string.IsNullOrEmpty(connectionStringName))
            {
                if (connSection.ConnectionStrings.Count > 1)
                {
                    connectionStringName = connSection.ConnectionStrings[connSection.ConnectionStrings.Count - 1].Name;
                    result = connSection.ConnectionStrings[connSection.ConnectionStrings.Count - 1].ConnectionString;
                    providerName = connSection.ConnectionStrings[connSection.ConnectionStrings.Count - 1].ProviderName;
                }
            }
            else
            {
                try
                {
                    result = connSection.ConnectionStrings[connectionStringName].ConnectionString;
                    providerName = connSection.ConnectionStrings[connectionStringName].ProviderName;
                }
                catch
                {
                    result = "There is no connection string name called '" + connectionStringName + "'";
                }
            }

            //	if (String.IsNullOrEmpty(providerName))
            //		providerName="System.Data.SqlClient";

            return result;
        }

        string _connectionString = "";
        string _providerName = "";
        void InitConnectionString()
        {
            if (String.IsNullOrEmpty(_connectionString))
            {
                _connectionString = GetConnectionString(ref this._connectionStringName, out _providerName);
                // https://www.connectionstrings.com/sqlconnection/
                if (_connectionString.Contains("|DataDirectory|"))
                {
                    //have to replace it
                    string dataFilePath = this._pathToProjectWithConnectionString + "\\App_Data\\";
                    _connectionString = _connectionString.Replace("|DataDirectory|", dataFilePath);
                }
            }
        }
        public string ConnectionString
        {
            get
            {
                InitConnectionString();
                return _connectionString;
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
    }
}
