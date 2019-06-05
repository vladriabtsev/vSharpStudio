using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Proto.Config.Connection;
using vSharpStudio.common;
using vSharpStudio.vm.ViewModels;

namespace vPlugin.DbModel.MsSql
{
    public class ConnectionGenerator : IvPluginGenerator
    {
        public ConnectionGenerator()
        {
            this.Guid = new Guid("85B2000B-6B1A-49B8-A977-01C1FF21649D");
            this.Name = "Connections";
            this.DefaultSettingsName = "Connection";
            this.Description = "Connection string generator";
            this.PluginGeneratorType = vPluginGeneratorTypeEnum.DbConnection;
        }
        static DiagnosticSource MsSqlMigratorDiagnostic = new DiagnosticListener("vPlugin.MsSqlMigrator");
        public ILogger Logger;
        public Guid Guid { get; protected set; }
        public string Name { get; protected set; }
        public string DefaultSettingsName { get; protected set; }
        public string Description { get; protected set; }
        public vPluginGeneratorTypeEnum PluginGeneratorType { get; }
        public IvPluginGeneratorSettingsVM GetSettingsMvvm(string settings)
        {
            if (settings == null)
                return new MsSqlConnectionSettings();
            proto_ms_sql_connection_settings proto = proto_ms_sql_connection_settings.Parser.ParseJson(settings);
            MsSqlConnectionSettings res = MsSqlConnectionSettings.ConvertToVM(proto);
            return res;
        }
        public string ConnectionString { get { return _ConnectionString; } set { _ConnectionString = value; } }
        private string _ConnectionString;
    }
}
