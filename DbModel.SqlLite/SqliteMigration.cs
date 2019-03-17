using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Sqlite.Scaffolding.Internal;
using vSharpStudio.vm;
using vSharpStudio.vm.Migration;
using vSharpStudio.vm.ViewModels;

namespace DbModel.Sqlite
{
    public class SqliteMigration : IMigration
    {
        Config _config = null;

        public SqliteMigration(Config config)
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

            //var mf = new SqliteDatabaseModelFactory(new Logger());
            //var databaseModel = mf.Create(connectionString, new List<string>(), new List<string>() { this._config.DbSchema });

            return res;
        }
    }
}
