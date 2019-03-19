using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using vSharpStudio.vm.ViewModels;
using Xunit;
using static Proto.Config.proto_data_type.Types;

namespace vSharpStudio.xUnit
{
    public class MsSqlTests
    {
        [Fact]
        public void MsSql000GuidInit()
        {
            //IDatabaseComparer
            var cfg = new Config();
            cfg.ConnectionStringName = "MsSql";
            cfg.PathToProjectWithConnectionString = Directory.GetCurrentDirectory() + @"..\..\app.config";
//            var v = new MsSqlServerSchemaReader();
            //MsSqlModel v = new MsSqlModel();
            Assert.True(false);
        }
        [Fact]
        public void MsSql001CanCreateCatalogWithDifferentSimpleDataTypes()
        {
            //var cfg = new ConfigRoot();
            //cfg.ConnectionStringName = "MsSql";
            //cfg.PathToProjectWithConnectionString = Directory.GetCurrentDirectory() + @"..\..\app.config";
            //var c = new Catalog("Test", new List<Property>() {
            //    new Property("pdouble0", EnumDataType.Numerical, 10, 0),
            //    new Property("pdouble", EnumDataType.Numerical),
            //    new Property("penum", EnumDataType.Enum),
            //    new Property("pstring", EnumDataType.String),
            //    new Property("pbool", EnumDataType.Bool)
            //});
            ////TODO for Numerical test all cases: int, long, uint, ulong, ...
            //cfg.Catalogs.ListCatalogs.Add(c);

            //var res = cfg.GetUpdateDbProblems();
            //Assert.True(res.Count == 0);

            //cfg.UpdateDb();
            //c.Properties.ListProperties[0].DataType.Length = 3;
            //res = cfg.GetUpdateDbProblems();
            //Assert.True(res.Count == 1);

            Assert.True(false);
        }
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
