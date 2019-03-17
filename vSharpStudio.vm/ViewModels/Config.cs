using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Google.Protobuf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using vSharpStudio.vm.Migration;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Config : IMigration
    {
        protected IMigration _migration = null;
        public string ConnectionString = null;
        partial void OnInit()
        {
            this.Guid = System.Guid.NewGuid().ToString();
            if (string.IsNullOrWhiteSpace(this.DbSchema))
                this.DbSchema = "v";
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
        public List<EntityObjectProblem> GetUpdateDbProblems()
        {
            return _migration.GetUpdateDbProblems();
        }

        public void UpdateDb()
        {
            throw new NotImplementedException();
        }
    }
}
