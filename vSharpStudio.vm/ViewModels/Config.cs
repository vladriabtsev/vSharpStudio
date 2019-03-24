using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Google.Protobuf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.vm.Migration;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Config : EntityObjectBaseWithGuid<Config, Config.ConfigValidator>, IEntityObject, IMigration
    {
        protected IMigration _migration = null;
        public string ConnectionString = null;
        partial void OnInit()
        {
            this.Guid = System.Guid.NewGuid().ToString();
            if (string.IsNullOrWhiteSpace(this.DbSchema))
                this.DbSchema = "v";
        }
        public Config(string configJson, SortableObservableCollection<ValidationMessage> validationCollection = null) 
            : base(ConfigValidator.Validator, validationCollection)
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
        public virtual void InitMigration()
        {
            throw new NotImplementedException();
        }
        public bool IsDatabaseServiceOn()
        {
            return _migration.IsDatabaseServiceOn();
        }
        public bool IsDatabaseExist()
        {
            throw new NotImplementedException();
        }
        public bool CreateDatabase()
        {
            throw new NotImplementedException();
        }
        public DatabaseModel GetDatabaseModel()
        {
            return _migration.GetDatabaseModel();
        }
    }
}
