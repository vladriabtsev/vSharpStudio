using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.Migration;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.ViewModels
{
    //TODO report based on FlowDocument https://github.com/rodrigovedovato/FlowDocumentReporting
    public class ConfigRoot : Config
    {
        public ConfigRoot()
        {
            // https://docs.microsoft.com/en-us/ef/core/providers/
            switch (this.ProviderName)
            {
                case DbShemaReader.PROVIDERNAMESQL:
                    _migration = new DbModel.MsSql.SqlServerMigration(this);
                    break;
                case DbShemaReader.PROVIDERNAMESQLITE:
                    _migration = new DbModel.Sqlite.SqliteMigration(this);
                    break;
                default:
                    throw new ArgumentException("Unsupported ProviderName in connection string: " + this.ProviderName);
            }
        }
        public ConfigRoot(bool dummy, string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
        }
        public ConfigRoot(string configJson) : base(configJson) { }

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
