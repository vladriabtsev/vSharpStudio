using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Sqlite.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Sqlite.Storage.Internal;
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

        bool IMigration.IsDatabaseServiceOn()
        {
            throw new NotImplementedException();
        }

        Task<bool> IMigration.IsDatabaseServiceOnAsync(CancellationToken cancellationToken)
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
        //    // https://joshuachini.com/2017/03/08/adding-diagnostics-in-entity-framework-core/
        //    // https://github.com/dotnet/corefx/blob/master/src/System.Diagnostics.DiagnosticSource/src/DiagnosticSourceUsersGuide.md
        //    //var mf = new SqliteDatabaseModelFactory(new DiagnosticLogger<Microsoft.EntityFrameworkCore.DbLoggerCategory.Scaffolding>(), new SqliteTypeMappingSource());
        //    //res = mf.Create(this._config.ConnectionString, new List<string>(), new List<string>() { this._config.DbSchema });
        //    return res;
        //}
        //void IMigration.InitMigration()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
