using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.SqlServer.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.Logging;
using vSharpStudio.std;
using vSharpStudio.vm;
using vSharpStudio.vm.Migration;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.Migration
{
    // https://github.com/aspnet/EntityFrameworkCore
    public class SqlServerMigration : IMigration
    {
        private static string _logger_category = typeof(SqlServerMigration).FullName;
        private static DiagnosticSource _logger = new DiagnosticListener(_logger_category);

        public static ILogger Logger = ApplicationLogging.CreateLogger<SqlServerMigration>();

        Config _config = null;
        public SqlServerMigration(Config config)
        {
            this._config = config;
            if (_logger.IsEnabled(_logger_category))
                _logger.Write("Created", null);

            DbContextOptions options = new DbContextOptionsBuilder()
                              .UseSqlServer(config.ConnectionString)
                              .Options;
            _dependencies = new RelationalConnectionDependencies(
                    options,
                    new DiagnosticsLogger<DbLoggerCategory.Database.Transaction>(
                        new LoggerFactory(),
                        new LoggingOptions(),
                        new DiagnosticListener("FakeDiagnosticListener")),
                    new DiagnosticsLogger<DbLoggerCategory.Database.Connection>(
                        new LoggerFactory(),
                        new LoggingOptions(),
                        new DiagnosticListener("FakeDiagnosticListener")),
                    new NamedConnectionStringResolver(options),
                    new RelationalTransactionFactory(new RelationalTransactionFactoryDependencies()));
        }
        private RelationalConnectionDependencies _dependencies = null;
        //DatabaseModel IMigration.GetDatabaseModel()
        //{
        //    DatabaseModel res = null;
        //    // https://joshuachini.com/2017/03/08/adding-diagnostics-in-entity-framework-core/
        //    // https://github.com/dotnet/corefx/blob/master/src/System.Diagnostics.DiagnosticSource/src/DiagnosticSourceUsersGuide.md
        //    var mf = new SqlServerDatabaseModelFactory(new DiagnosticLogger<Microsoft.EntityFrameworkCore.DbLoggerCategory.Scaffolding>());
        //    res = mf.Create(this._config.ConnectionString, new List<string>(), new List<string>() { this._config.DbSchema });
        //    return res;
        //}

        //void IMigration.InitMigration()
        //{
        //    throw new NotImplementedException();
        //}
        bool IMigration.IsDatabaseServiceOn()
        {
            using (var connection = new SqlServerConnection(_dependencies))
            {
                using (var master = connection.CreateMasterConnection())
                {
                    //Assert.Equal(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master", master.ConnectionString);
                    //Assert.Equal(60, master.CommandTimeout);
                }
            }

            //SqlServerConnection conn = new SqlServerConnection(CreateDependencies());
            //conn.
            //    Row
            //SqlServerDatabaseCreator cr = new SqlServerDatabaseCreator(CreateDependencies());
            throw new NotImplementedException();
        }
        Task<bool> IMigration.IsDatabaseServiceOnAsync(CancellationToken cancellationToken)
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
            //ClearAllPools();

            //using (var masterConnection = _connection.CreateMasterConnection())
            //{
            //    Dependencies.MigrationCommandExecutor
            //        .ExecuteNonQuery(CreateDropCommands(), masterConnection);
            //}
        }
        Task IMigration.DropDatabaseAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            //ClearAllPools();

            //using (var masterConnection = _connection.CreateMasterConnection())
            //{
            //    await Dependencies.MigrationCommandExecutor
            //        .ExecuteNonQueryAsync(CreateDropCommands(), masterConnection, cancellationToken);
            //}
        }
        //void IMigration.UpdateDb()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
