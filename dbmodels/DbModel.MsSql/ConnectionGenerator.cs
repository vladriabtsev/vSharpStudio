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
    public class ConnectionGenerator : IvPluginCodeGenerator
    {
        public ConnectionGenerator()
        {
            this.Guid = new Guid("85B2000B-6B1A-49B8-A977-01C1FF21649D");
            this.Name = "Connections";
            this.DefaultSettingsName = "Connection";
            this.Description = "Connection string generator";
            this.PluginType = vPluginTypeEnum.DbConnection;
        }
        static DiagnosticSource MsSqlMigratorDiagnostic = new DiagnosticListener("vPlugin.MsSqlMigrator");
        public ILogger Logger;
        public Guid Guid { get; protected set; }
        public string Name { get; protected set; }
        public string DefaultSettingsName { get; protected set; }
        public string Description { get; protected set; }
        public vPluginTypeEnum PluginType { get; }
        public IvPluginSettingsVM GetSettingsMvvm(string settings)
        {
            if (settings == null)
                return new ConnMsSql();
            proto_conn_ms_sql proto = proto_conn_ms_sql.Parser.ParseJson(settings);
            ConnMsSql res = ConnMsSql.ConvertToVM(proto);
            return res;
        }
        public string ConnectionString { get { return _ConnectionString; } set { _ConnectionString = value; } }
        private string _ConnectionString;
    }
}
