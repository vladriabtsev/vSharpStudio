using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using vSharpStudio.std;
using vSharpStudio.vm;
using vSharpStudio.vm.Migration;
using vSharpStudio.vm.ViewModels;

namespace DbModel.MySql
{
    public class MySqlMigration : IMigration
    {
        public static ILogger Logger = ApplicationLogging.CreateLogger<MySqlMigration>();
        Config _config = null;
        public MySqlMigration(Config config)
        {
            this._config = config;
        }

        public void InitMigration()
        {
            throw new NotImplementedException();
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


            return res;
        }
        DatabaseModel IMigration.GetDatabaseModel()
        {
            DatabaseModel res = null;
            // https://github.com/mysql/mysql-connector-net
            // https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql
            var mf = new MySqlDatabaseModelFactory(ApplicationLogging.LoggerFactory);
            res = mf.Create(this._config.ConnectionString, new List<string>(), new List<string>() { this._config.DbSchema });
            return res;
        }

        void IMigration.InitMigration()
        {
            throw new NotImplementedException();
        }

        bool IMigration.DatabaseExist()
        {
            throw new NotImplementedException();
        }

        bool IMigration.CreateDatabase()
        {
            throw new NotImplementedException();
        }

        void IMigration.UpdateDb()
        {
            throw new NotImplementedException();
        }
    }
}
