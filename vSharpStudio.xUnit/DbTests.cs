using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using vSharpStudio.vm.Migration;
using vSharpStudio.vm.ViewModels;
using Xunit;
using Xunit.Abstractions;
using static Proto.Config.proto_data_type.Types;

namespace vSharpStudio.xUnit
{
    public class DbTests
    {
        public DbTests(ITestOutputHelper output)
        {
            ILoggerFactory loggerFactory = std.ApplicationLogging.LoggerFactory;
            loggerFactory.AddProvider(new DebugLoggerProvider());
            ILogger logger = loggerFactory.CreateLogger<DbTests>();
            logger.LogInformation("Start tests");
        }

        //[Theory]
        //[InlineData("DummyMsSql")]
        ////[InlineData("Sqlite")]
        //public void Db001IsDatabaseServiceOff(string conName)
        //{
        //    IMigration cfg = new ConfigRoot(Directory.GetCurrentDirectory() + @"\..\..\..", conName);
        //    (cfg as ConfigRoot).InitMigration();
        //    Assert.False(cfg.IsDatabaseServiceOn());
        //}
        //[Theory]
        //[InlineData("MsSql")]
        ////[InlineData("Sqlite")]
        //public void Db001IsDatabaseServiceOn(string conName)
        //{
        //    IMigration cfg = new ConfigRoot(Directory.GetCurrentDirectory() + @"\..\..\..", conName);
        //    (cfg as ConfigRoot).InitMigration();
        //    //cfg.GetUpdateDbProblems();
        //    Assert.True(false);
        //}
        //[Theory]
        //[InlineData("MsSql")]
        ////[InlineData("Sqlite")]
        //public void Db005CanRecognizeDbAbsence(string conName)
        //{
        //    IMigration cfg = new ConfigRoot(Directory.GetCurrentDirectory()+ @"\..\..\..", conName);
        //    (cfg as ConfigRoot).InitMigration();
        //    //cfg.GetUpdateDbProblems();
        //    Assert.True(false);
        //}
        //[Theory]
        //[InlineData("MsSql")]
        ////[InlineData("Sqlite")]
        //public void Db002CanCreateCatalog(string conName)
        //{
        //    var cfg = new ConfigRoot(Directory.GetCurrentDirectory() + @"\..\..\..", conName);
        //    cfg.InitMigration();
        //    var c = new Catalog("Test", new List<Property>() {
        //        new Property("pdouble0", EnumDataType.Numerical, 10, 0),
        //        new Property("pdouble", EnumDataType.Numerical),
        //        new Property("penum", EnumDataType.Enum),
        //        new Property("pstring", EnumDataType.String),
        //        new Property("pbool", EnumDataType.Bool)
        //    });
        //    //TODO for Numerical test all cases: int, long, uint, ulong, ...
        //    cfg.Catalogs.ListCatalogs.Add(c);

        //    //var res = cfg.GetUpdateDbProblems();
        //    //Assert.True(res.Count == 0);

        //    //cfg.UpdateDb();
        //    //c.Properties.ListProperties[0].DataType.Length = 3;
        //    //res = cfg.GetUpdateDbProblems();
        //    //Assert.True(res.Count == 1);

        //    Assert.True(false);
        //}
        private void Test(string createSql, IEnumerable<string> tables, IEnumerable<string> schemas, Action<DatabaseModel> asserter, string cleanupSql)
        {
            //Fixture.TestStore.ExecuteNonQuery(createSql);

            //try
            //{
            //    var databaseModelFactory = new SqlServerDatabaseModelFactory(
            //        new DiagnosticsLogger<DbLoggerCategory.Scaffolding>(
            //            Fixture.ListLoggerFactory,
            //            new LoggingOptions(),
            //            new DiagnosticListener("Fake")));

            //    var databaseModel = databaseModelFactory.Create(Fixture.TestStore.ConnectionString, tables, schemas);
            //    Assert.NotNull(databaseModel);
            //    asserter(databaseModel);
            //}
            //finally
            //{
            //    if (!string.IsNullOrEmpty(cleanupSql))
            //    {
            //        Fixture.TestStore.ExecuteNonQuery(cleanupSql);
            //    }
            //}
        }
    }
}
