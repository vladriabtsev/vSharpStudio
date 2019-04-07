using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        void IMigration.CreateDatabase()
        {
            throw new NotImplementedException();
        }

        Task IMigration.CreateDatabaseAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        void IMigration.DropDatabase()
        {
            throw new NotImplementedException();
        }

        Task IMigration.DropDatabaseAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        bool IMigration.IsDatabaseExists()
        {
            throw new NotImplementedException();
        }

        Task<bool> IMigration.IsDatabaseExistsAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //List<EntityObjectProblem> IMigration.GetUpdateDbProblems()
        //{
        //    var res = new List<EntityObjectProblem>();
        //    IVisitorConfig source = new ConfigModelBuilderVisitor();
        //    source.Visit(this._config);


        //    return res;
        //}
        //DatabaseModel IMigration.GetDatabaseModel()
        //{
        //    DatabaseModel res = null;
        //    // https://github.com/mysql/mysql-connector-net
        //    // https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql
        //    var mf = new MySqlDatabaseModelFactory(ApplicationLogging.LoggerFactory);
        //    res = mf.Create(this._config.ConnectionString, new List<string>(), new List<string>() { this._config.DbSchema });
        //    return res;
        //}
        //void IMigration.InitMigration()
        //{
        //    throw new NotImplementedException();
        //}
        bool IMigration.IsDatabaseServiceOn()
        {
            throw new NotImplementedException();
        }

        Task<bool> IMigration.IsDatabaseServiceOnAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
