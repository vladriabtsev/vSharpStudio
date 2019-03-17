using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.SqlServer.Scaffolding.Internal;
using vSharpStudio.vm;
using vSharpStudio.vm.Migration;
using vSharpStudio.vm.ViewModels;

namespace DbModel.MsSql
{
    public class SqlServerMigration : IMigration
    {
        Config _config = null;
        public SqlServerMigration(Config config)
        {
            this._config = config;
        }

        public void UpdateDb()
        {
            throw new NotImplementedException();
        }

        List<EntityObjectProblem> IMigration.GetUpdateDbProblems()
        {
            var res = new List<EntityObjectProblem>();
            IVisitorConfig source = new ConfigModelBuilderVisitor();
            source.Visit(this._config);

            var mf = new SqlServerDatabaseModelFactory(new Logger());
            var databaseModel = mf.Create(this._config.ConnectionString, new List<string>(), new List<string>() { this._config.DbSchema });

            return res;
        }
    }
}
