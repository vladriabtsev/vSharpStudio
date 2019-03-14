using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Google.Protobuf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Scaffolding.Internal;
using vSharpStudio.vm.Migration;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Config
    {
        partial void OnInit()
        {
            this.Guid = System.Guid.NewGuid().ToString();
        }
        public Config(string configJson) : base(ConfigValidator.Validator)
        {
            this._dto = Proto.Config.proto_config.Parser.ParseJson(configJson);
            this.initFromDto();
        }
        public string ExportToJson()
        {
            var res = JsonFormatter.Default.Format(this._dto);
            return res;
        }

        public List<EntityObjectProblem> GetUpdateSqlDbProblems(string connectionString)
        {
            var res = new List<EntityObjectProblem>();

            //var databaseModelFactory = new SqlServerDatabaseModelFactory(
            //    new DiagnosticsLogger<DbLoggerCategory.Scaffolding>(
            //        Fixture.ListLoggerFactory,
            //        new LoggingOptions(),
            //        new DiagnosticListener("Fake")));

            //var databaseModel = databaseModelFactory.Create(Fixture.TestStore.ConnectionString, new List<string>(), new List<string>() { "v" });

            return res;
        }
    }
}
