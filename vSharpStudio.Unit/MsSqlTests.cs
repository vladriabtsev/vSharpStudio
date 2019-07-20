using System;
using System.Linq;
using vSharpStudio.vm.ViewModels;
using ViewModelBase;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vSharpStudio.ViewModels;
using vPlugin.DbModel.MsSql;
using vSharpStudio.common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using System.Data.SqlClient;
using Dapper;

namespace vSharpStudio.Unit
{
    [TestClass]
    public class MsSqlTests
    {
        private Microsoft.Extensions.Logging.ILogger _logger;
        public MsSqlTests()
        {
            ViewModelBindable.isUnitTests = true;

            InitLogging(this);

            _logger = ApplicationLogging.CreateLogger<MsSqlTests>();
        }

        internal static void InitLogging(object type)
        {
            if (ApplicationLogging.LogerProvider == null)
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                    //.WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                    .CreateLogger().ForContext(type.GetType());
                var serviceCollection = new ServiceCollection();
                var lp = serviceCollection.AddLogging(loggingBuilder =>
                {
                    //loggingBuilder.AddFilter((p) => { return p >= LogLevel.Trace; });
                    //loggingBuilder.AddConsole((o) => { o.IncludeScopes = true; });
                    loggingBuilder.AddSerilog();
                    //loggingBuilder.AddConfiguration(new )
                    //loggingBuilder.AddDebug();
                }).BuildServiceProvider().GetRequiredService<ILoggerProvider>();
                ApplicationLogging.LogerProvider = lp;
            }
        }

        //#region Config
        [TestMethod]
        public void Plugin001CanLoadPlugin()
        {
            var vm = new MainPageVM(false);
            vm.Compose();
            Assert.IsTrue(vm.ListDbDesignPlugins.Count > 0);
            Assert.IsTrue(vm.Model.GroupPlugins.ListPlugins.Count > 0);
            var lstPlugins = (from p in vm.ListDbDesignPlugins where p is MsSqlDesignGenerator select p).ToList();
            Assert.IsTrue(lstPlugins.Count == 1);
            var lstPlugins2 = (from p in vm.Model.GroupPlugins.ListPlugins where p.VPlugin is MsSqlPlugin select p).ToList();
            Assert.IsTrue(lstPlugins2.Count == 1);
            Assert.IsTrue(lstPlugins2[0].ListGenerators.Count == 2);
            _logger.LogInformation("kuku");
        }
        [TestMethod]
        public void Plugin002CanWorkWithConnections()
        {
            var vm = new MainPageVM(false);
            vm.Compose();
            var plugin = (from p in vm.Model.GroupPlugins.ListPlugins where p.VPlugin is MsSqlPlugin select p).First();
            var connGen = (from p in plugin.ListGenerators where p.Generator is ConnectionGenerator select p).First();
            var cvm = (MsSqlConnectionSettings)connGen.Generator.GetSettingsMvvm(null);
            cvm.Name = "test";
            var json = cvm.Settings;
            var cvm2 = (MsSqlConnectionSettings)connGen.Generator.GetSettingsMvvm(json);
            Assert.IsTrue(cvm.Name == cvm2.Name);

            cvm.DataSource = "mydbsource";
            var connstring = cvm.GenerateCode();
            Assert.AreEqual("Data Source=mydbsource", connstring);
        }
        //#endregion Config
        [TestMethod]
        public void Plugin003CanWorkWithDbGenerator()
        {
            var vm = new MainPageVM(false);
            vm.Compose();
            var plugin = (from p in vm.Model.GroupPlugins.ListPlugins where p.VPlugin is MsSqlPlugin select p).First();

            var gen = (from p in plugin.ListGenerators where p.Generator is MsSqlDesignGenerator select p).First();
            var gvm = (MsSqlDesignGeneratorSettings)gen.Generator.GetSettingsMvvm(null);
            gvm.Name = "test";
            var json = gvm.Settings;
            var cvm2 = (MsSqlDesignGeneratorSettings)gen.Generator.GetSettingsMvvm(json);
            Assert.IsTrue(gvm.Name == cvm2.Name);
            //var sql = gvm.GenerateCode();
            //Assert.AreEqual("", sql);
        }

        private void EnsureDeletedTestDb(string connectionString)
        {
            IServiceProvider service_provider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();
            using (var context = new vDbContext(connectionString, service_provider,
                new DbContextOptionsBuilder().UseSqlServer(connectionString).Options))
            {
                context.Database.EnsureDeleted();
            }
        }

        [TestMethod]
        public void Db001_CreateEmptyDbWithSpecialTable()
        {
            Execute((vm) =>
            {
            }, (conn) =>
            {
                var tables = conn.Query("SELECT * FROM [INFORMATION_SCHEMA].[TABLES]");
                Assert.AreEqual(0, tables.Count());
            });
        }
        [TestMethod]
        public void Db002_ConstantsAndSimpleDataTypes()
        {
            Execute((vm) =>
            {
                Enumeration en = null;
                Constant c = null;

                en = new Enumeration() { DataTypeEnum = EnumEnumerationType.BYTE_VALUE };
                vm.Model.GroupEnumerations.AddEnumeration(en);
                c = new Constant() { Name = "tinyint_enum", DataType = new DataType() { DataTypeEnum = EnumDataType.ENUMERATION, ObjectGuid = en.Guid, IsNullable = false } };
                vm.Model.GroupConstants.AddConstant(c);
                en = new Enumeration() { DataTypeEnum = EnumEnumerationType.SHORT_VALUE };
                vm.Model.GroupEnumerations.AddEnumeration(en);
                c = new Constant() { Name = "smallint_enum", DataType = new DataType() { DataTypeEnum = EnumDataType.ENUMERATION, ObjectGuid = en.Guid, IsNullable = false } };
                vm.Model.GroupConstants.AddConstant(c);
                en = new Enumeration() { DataTypeEnum = EnumEnumerationType.INTEGER_VALUE };
                vm.Model.GroupEnumerations.AddEnumeration(en);
                c = new Constant() { Name = "int_enum", DataType = new DataType() { DataTypeEnum = EnumDataType.ENUMERATION, ObjectGuid = en.Guid, IsNullable = false } };
                vm.Model.GroupConstants.AddConstant(c);
                en = new Enumeration() { DataTypeEnum = EnumEnumerationType.STRING_VALUE };
                vm.Model.GroupEnumerations.AddEnumeration(en);
                c = new Constant() { Name = "nvarchar_enum", DataType = new DataType() { DataTypeEnum = EnumDataType.ENUMERATION, ObjectGuid = en.Guid, IsNullable = false } };
                vm.Model.GroupConstants.AddConstant(c);
                en = new Enumeration() { };
                vm.Model.GroupEnumerations.AddEnumeration(en);
                c = new Constant() { Name = "int_enum_def", DataType = new DataType() { DataTypeEnum = EnumDataType.ENUMERATION, ObjectGuid = en.Guid, IsNullable = false } };
                vm.Model.GroupConstants.AddConstant(c);

                c = new Constant() { Name = "datetime", DataType = new DataType() { DataTypeEnum = EnumDataType.DATE, IsNullable = false } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "datetime2", DataType = new DataType() { DataTypeEnum = EnumDataType.DATETIME, IsNullable = false } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "datetime3", DataType = new DataType() { DataTypeEnum = EnumDataType.TIME, IsNullable = false } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "bit", DataType = new DataType() { DataTypeEnum = EnumDataType.BOOL, IsNullable = false } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "tinyint", DataType = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL, IsPositive = true, Length = 2, IsNullable = false } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "smallint", DataType = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL, IsPositive = true, Length = 4, IsNullable = false } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "int", DataType = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL, IsPositive = true, Length = 9, IsNullable = false } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "bigint", DataType = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL, IsPositive = true, Length = 18, IsNullable = false } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "decimal", DataType = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL, IsPositive = true, Length = 28, IsNullable = false } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "decimal2", DataType = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL, IsPositive = true, Length = 15, Accuracy = 2, IsNullable = false } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "nvarchar", DataType = new DataType() { DataTypeEnum = EnumDataType.STRING, IsNullable = false } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "nvarchar5", DataType = new DataType() { DataTypeEnum = EnumDataType.STRING, Length = 5, IsNullable = false } };
                vm.Model.GroupConstants.AddConstant(c);

                en = new Enumeration() { DataTypeEnum = EnumEnumerationType.BYTE_VALUE };
                vm.Model.GroupEnumerations.AddEnumeration(en);
                c = new Constant() { Name = "tinyintn_enum", DataType = new DataType() { DataTypeEnum = EnumDataType.ENUMERATION, ObjectGuid = en.Guid, IsNullable = true } };
                vm.Model.GroupConstants.AddConstant(c);
                en = new Enumeration() { DataTypeEnum = EnumEnumerationType.SHORT_VALUE };
                vm.Model.GroupEnumerations.AddEnumeration(en);
                c = new Constant() { Name = "smallintn_enum", DataType = new DataType() { DataTypeEnum = EnumDataType.ENUMERATION, ObjectGuid = en.Guid, IsNullable = true } };
                vm.Model.GroupConstants.AddConstant(c);
                en = new Enumeration() { DataTypeEnum = EnumEnumerationType.INTEGER_VALUE };
                vm.Model.GroupEnumerations.AddEnumeration(en);
                c = new Constant() { Name = "intn_enum", DataType = new DataType() { DataTypeEnum = EnumDataType.ENUMERATION, ObjectGuid = en.Guid, IsNullable = true } };
                vm.Model.GroupConstants.AddConstant(c);
                en = new Enumeration() { DataTypeEnum = EnumEnumerationType.STRING_VALUE };
                vm.Model.GroupEnumerations.AddEnumeration(en);
                c = new Constant() { Name = "nvarcharn_enum", DataType = new DataType() { DataTypeEnum = EnumDataType.ENUMERATION, ObjectGuid = en.Guid, IsNullable = true } };
                vm.Model.GroupConstants.AddConstant(c);
                en = new Enumeration() { };
                vm.Model.GroupEnumerations.AddEnumeration(en);
                c = new Constant() { Name = "intn_enum_def", DataType = new DataType() { DataTypeEnum = EnumDataType.ENUMERATION, ObjectGuid = en.Guid, IsNullable = true } };
                vm.Model.GroupConstants.AddConstant(c);

                c = new Constant() { Name = "datetimen", DataType = new DataType() { DataTypeEnum = EnumDataType.DATE, IsNullable = true } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "datetimen2", DataType = new DataType() { DataTypeEnum = EnumDataType.DATETIME, IsNullable = true } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "datetimen3", DataType = new DataType() { DataTypeEnum = EnumDataType.TIME, IsNullable = true } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "bitn", DataType = new DataType() { DataTypeEnum = EnumDataType.BOOL, IsNullable = true } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "tinyintn", DataType = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL, IsPositive = true, Length = 2, IsNullable = true } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "smallintn", DataType = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL, IsPositive = true, Length = 4, IsNullable = true } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "intn", DataType = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL, IsPositive = true, Length = 9, IsNullable = true } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "bigintn", DataType = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL, IsPositive = true, Length = 18, IsNullable = true } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "decimaln", DataType = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL, IsPositive = true, Length = 28, IsNullable = true } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "decimaln2", DataType = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL, IsPositive = true, Length = 15, Accuracy = 2, IsNullable = true } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "nvarcharn", DataType = new DataType() { DataTypeEnum = EnumDataType.STRING, IsNullable = true } };
                vm.Model.GroupConstants.AddConstant(c);
                c = new Constant() { Name = "nvarcharn5", DataType = new DataType() { DataTypeEnum = EnumDataType.STRING, Length = 5, IsNullable = true } };
                vm.Model.GroupConstants.AddConstant(c);
                vm.CommandConfigSave.Execute(null);
            }, (conn) =>
            {
                var tables = conn.Query("SELECT * FROM [INFORMATION_SCHEMA].[TABLES]");
                Assert.AreEqual(1, tables.Count());
                var fields = conn.Query("SELECT * FROM [INFORMATION_SCHEMA].[COLUMNS] WHERE [TABLE_NAME]='Constants' ");
                foreach (var t in fields)
                {
                    switch (t.COLUMN_NAME)
                    {
                        case "Id":
                            Assert.AreEqual("int", t.DATA_TYPE);
                            Assert.AreEqual("NO", t.IS_NULLABLE);
                            break;
                        case "tinyintn_enum":
                        case "tinyint_enum":
                        case "smallintn_enum":
                        case "smallint_enum":
                        case "intn_enum":
                        case "int_enum":
                        case "nvarcharn_enum":
                        case "nvarchar_enum":
                        case "intn_enum_def":
                        case "int_enum_def":
                            Assert.IsTrue(t.COLUMN_NAME.StartsWith(t.DATA_TYPE));
                            if (t.IS_NULLABLE == "YES")
                                Assert.IsTrue(t.COLUMN_NAME.Substring(t.DATA_TYPE.Length, 1) == "n");
                            else
                                Assert.IsFalse(t.COLUMN_NAME.Substring(t.DATA_TYPE.Length, 1) == "n");
                            break;
                        case "datetime":
                        case "datetime2":
                        case "datetime3":
                            Assert.AreEqual("datetime2", t.DATA_TYPE);
                            Assert.AreEqual("NO", t.IS_NULLABLE);
                            break;
                        case "bit":
                            Assert.AreEqual("bit", t.DATA_TYPE);
                            Assert.AreEqual("NO", t.IS_NULLABLE);
                            break;
                        case "tinyint":
                        case "smallint":
                        case "int":
                        case "bigint":
                            Assert.AreEqual(t.COLUMN_NAME, t.DATA_TYPE);
                            Assert.AreEqual("NO", t.IS_NULLABLE);
                            break;
                        case "decimal":
                            Assert.AreEqual("decimal", t.DATA_TYPE);
                            Assert.AreEqual("NO", t.IS_NULLABLE);
                            Assert.AreEqual(28, t.NUMERIC_PRECISION);
                            Assert.AreEqual(0, t.NUMERIC_SCALE);
                            break;
                        case "decimal2":
                            Assert.AreEqual("decimal", t.DATA_TYPE);
                            Assert.AreEqual("NO", t.IS_NULLABLE);
                            Assert.AreEqual(15, t.NUMERIC_PRECISION);
                            Assert.AreEqual(2, t.NUMERIC_SCALE);
                            break;
                        case "nvarchar":
                            Assert.AreEqual("nvarchar", t.DATA_TYPE);
                            Assert.AreEqual("NO", t.IS_NULLABLE);
                            Assert.AreEqual(-1, t.CHARACTER_MAXIMUM_LENGTH);
                            break;
                        case "nvarchar5":
                            Assert.AreEqual("nvarchar", t.DATA_TYPE);
                            Assert.AreEqual("NO", t.IS_NULLABLE);
                            Assert.AreEqual(5, t.CHARACTER_MAXIMUM_LENGTH);
                            break;

                        case "datetimen":
                        case "datetimen2":
                        case "datetimen3":
                            Assert.AreEqual("datetime2", t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            break;
                        case "bitn":
                            Assert.AreEqual("bit", t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            break;
                        case "tinyintn":
                        case "smallintn":
                        case "intn":
                        case "bigintn":
                            Assert.AreEqual(t.COLUMN_NAME.Substring(0, t.COLUMN_NAME.Length - 1), t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            break;
                        case "decimaln":
                            Assert.AreEqual("decimal", t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            Assert.AreEqual(28, t.NUMERIC_PRECISION);
                            Assert.AreEqual(0, t.NUMERIC_SCALE);
                            break;
                        case "decimaln2":
                            Assert.AreEqual("decimal", t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            Assert.AreEqual(15, t.NUMERIC_PRECISION);
                            Assert.AreEqual(2, t.NUMERIC_SCALE);
                            break;
                        case "nvarcharn":
                            Assert.AreEqual("nvarchar", t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            Assert.AreEqual(-1, t.CHARACTER_MAXIMUM_LENGTH);
                            break;
                        case "nvarcharn5":
                            Assert.AreEqual("nvarchar", t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            Assert.AreEqual(5, t.CHARACTER_MAXIMUM_LENGTH);
                            break;
                        default:
                            throw new Exception();
                    }
                }
            });
        }
        [TestMethod]
        public void Db004_CatalogsAndCatalogDataTypes()
        {
            Execute((vm) =>
            {
                vm.Model.PrimaryKeyType = EnumPrimaryKeyType.LONG;
                vm.Model.Name = "tst";
                Catalog ctlg = null;
                Property p = null;
                ctlg = new Catalog() { Name = "parent" };
                p = new Property() { Name = "datetime2", DataType = new DataType() { DataTypeEnum = EnumDataType.DATETIME } };
                ctlg.GroupProperties.AddProperty(p);

                var tab = new PropertiesTab() { Name = "Sub" };
                p = new Property() { Name = "int", DataType = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL } };
                tab.GroupProperties.AddProperty(p);
                ctlg.GroupPropertiesTabs.AddPropertiesTab(tab);

                var tab2 = new PropertiesTab() { Name = "Sub2" };
                p = new Property() { Name = "nvarchar", DataType = new DataType() { DataTypeEnum = EnumDataType.STRING } };
                tab2.GroupProperties.AddProperty(p);
                tab.GroupPropertiesTabs.AddPropertiesTab(tab2);

                vm.Model.GroupCatalogs.AddCatalog(ctlg);

                string parentguid = ctlg.Guid;
                ctlg = new Catalog() { Name = "child" };
                p = new Property() { Name = "parent", DataType = new DataType() { DataTypeEnum = EnumDataType.CATALOG, ObjectGuid = parentguid } };
                ctlg.GroupProperties.AddProperty(p);
                vm.Model.GroupCatalogs.AddCatalog(ctlg);

                Constant c = null;
                c = new Constant() { Name = "child", DataType = new DataType() { DataTypeEnum = EnumDataType.CATALOG, ObjectGuid = ctlg.Guid } };
                vm.Model.GroupConstants.AddConstant(c);

                vm.CommandConfigSave.Execute(null);
            }, (conn) =>
            {
                var tables = conn.Query("SELECT * FROM [INFORMATION_SCHEMA].[TABLES]");
                Assert.AreEqual(5, tables.Count());
                var fields = conn.Query("SELECT * FROM [INFORMATION_SCHEMA].[COLUMNS] WHERE [TABLE_NAME]='Parent' ");
                Assert.AreEqual(2, fields.Count());
                foreach (var t in fields)
                {
                    Assert.AreEqual("tst", t.TABLE_SCHEMA);
                    switch (t.COLUMN_NAME)
                    {
                        case "Id":
                            Assert.AreEqual("bigint", t.DATA_TYPE);
                            Assert.AreEqual("NO", t.IS_NULLABLE);
                            break;
                        case "datetime2":
                            Assert.AreEqual("datetime2", t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            break;
                        default:
                            throw new Exception();
                    }
                }
                fields = conn.Query("SELECT * FROM [INFORMATION_SCHEMA].[COLUMNS] WHERE [TABLE_NAME]='Child' ");
                Assert.AreEqual(2, fields.Count());
                foreach (var t in fields)
                {
                    Assert.AreEqual("tst", t.TABLE_SCHEMA);
                    switch (t.COLUMN_NAME)
                    {
                        case "Id":
                            Assert.AreEqual("bigint", t.DATA_TYPE);
                            Assert.AreEqual("NO", t.IS_NULLABLE);
                            break;
                        case "parent" + "Id":
                            Assert.AreEqual("bigint", t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            break;
                        default:
                            throw new Exception();
                    }
                }
                fields = conn.Query("SELECT * FROM [INFORMATION_SCHEMA].[COLUMNS] WHERE [TABLE_NAME]='ParentSub' ");
                Assert.AreEqual(3, fields.Count());
                foreach (var t in fields)
                {
                    Assert.AreEqual("tst", t.TABLE_SCHEMA);
                    switch (t.COLUMN_NAME)
                    {
                        case "Id":
                            Assert.AreEqual("bigint", t.DATA_TYPE);
                            Assert.AreEqual("NO", t.IS_NULLABLE);
                            break;
                        case "int":
                            Assert.AreEqual("int", t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            break;
                        case "parent" + "Id":
                            Assert.AreEqual("bigint", t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            break;
                        default:
                            throw new Exception();
                    }
                }
                fields = conn.Query("SELECT * FROM [INFORMATION_SCHEMA].[COLUMNS] WHERE [TABLE_NAME]='ParentSubSub2' ");
                Assert.AreEqual(3, fields.Count());
                foreach (var t in fields)
                {
                    Assert.AreEqual("tst", t.TABLE_SCHEMA);
                    switch (t.COLUMN_NAME)
                    {
                        case "Id":
                            Assert.AreEqual("bigint", t.DATA_TYPE);
                            Assert.AreEqual("NO", t.IS_NULLABLE);
                            break;
                        case "nvarchar":
                            Assert.AreEqual("nvarchar", t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            break;
                        case "parentSub" + "Id":
                            Assert.AreEqual("bigint", t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            break;
                        default:
                            throw new Exception();
                    }
                }
                string fks_query = @"SELECT pt.name as ParentName, cc.name as ChildFieldName , pk.name ParentKeyFieldName
FROM sys.foreign_key_columns fk
INNER JOIN sys.columns cc ON fk.parent_column_id = cc.[column_id] AND fk.parent_object_id = cc.[object_id]
INNER JOIN sys.tables ct ON ct.[object_id] = fk.parent_object_id
INNER JOIN sys.tables pt ON pt.[object_id] = fk.[referenced_object_id]
INNER JOIN sys.columns pk ON fk.referenced_column_id = pk.[column_id] AND fk.[referenced_object_id] = pk.[object_id]
WHERE  ct.name=@table";
                var fks = conn.Query(fks_query, "table".ToDicSql("child")).ToList();
                Assert.AreEqual(1, fks.Count());
                Assert.AreEqual("parent", fks[0].ParentName);
                Assert.AreEqual("parentId", fks[0].ChildFieldName);
                Assert.AreEqual("Id", fks[0].ParentKeyFieldName);

                fks = conn.Query(fks_query, "table".ToDicSql("Constants")).ToList();
                Assert.AreEqual(1, fks.Count());
                Assert.AreEqual("child", fks[0].ParentName);
                Assert.AreEqual("childId", fks[0].ChildFieldName);
                Assert.AreEqual("Id", fks[0].ParentKeyFieldName);

                fks = conn.Query(fks_query, "table".ToDicSql("ParentSub")).ToList();
                Assert.AreEqual(1, fks.Count());
                Assert.AreEqual("parent", fks[0].ParentName);
                Assert.AreEqual("parentId", fks[0].ChildFieldName);
                Assert.AreEqual("Id", fks[0].ParentKeyFieldName);

                fks = conn.Query(fks_query, "table".ToDicSql("ParentSubSub2")).ToList();
                Assert.AreEqual(1, fks.Count());
                Assert.AreEqual("parentSub", fks[0].ParentName);
                Assert.AreEqual("parentSubId", fks[0].ChildFieldName);
                Assert.AreEqual("Id", fks[0].ParentKeyFieldName);

                fks = conn.Query(fks_query, "table".ToDicSql("parent")).ToList();
                Assert.AreEqual(0, fks.Count());
            });
        }
        [TestMethod]
        public void Db005_DocumensAndDocumentDataTypes()
        {
            Execute((vm) =>
            {
                vm.Model.PrimaryKeyType = EnumPrimaryKeyType.LONG;
                vm.Model.Name = "tst";
                Document doc = null;
                Property p = null;
                doc = new Document() { Name = "doc" };
                p = new Property() { Name = "datetime2", DataType = new DataType() { DataTypeEnum = EnumDataType.DATETIME } };
                doc.GroupProperties.AddProperty(p);

                var tab = new PropertiesTab() { Name = "Sub" };
                p = new Property() { Name = "int", DataType = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL } };
                tab.GroupProperties.AddProperty(p);
                doc.GroupPropertiesTabs.AddPropertiesTab(tab);

                var tab2 = new PropertiesTab() { Name = "Sub2" };
                p = new Property() { Name = "nvarchar", DataType = new DataType() { DataTypeEnum = EnumDataType.STRING } };
                tab2.GroupProperties.AddProperty(p);
                tab.GroupPropertiesTabs.AddPropertiesTab(tab2);

                vm.Model.GroupDocuments.GroupListDocuments.AddDocument(doc);

                p = new Property() { Name = "int_shared", DataType = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL } };
                vm.Model.GroupDocuments.GroupSharedProperties.AddProperty(p);

                Constant c = null;
                c = new Constant() { Name = "int_doc", DataType = new DataType() { DataTypeEnum = EnumDataType.DOCUMENT, ObjectGuid = doc.Guid } };
                vm.Model.GroupConstants.AddConstant(c);

                vm.CommandConfigSave.Execute(null);
            }, (conn) =>
            {
                var tables = conn.Query("SELECT * FROM [INFORMATION_SCHEMA].[TABLES]");
                Assert.AreEqual(4, tables.Count());
                var fields = conn.Query("SELECT * FROM [INFORMATION_SCHEMA].[COLUMNS] WHERE [TABLE_NAME]='doc' ");
                Assert.AreEqual(3, fields.Count());
                foreach (var t in fields)
                {
                    Assert.AreEqual("tst", t.TABLE_SCHEMA);
                    switch (t.COLUMN_NAME)
                    {
                        case "Id":
                            Assert.AreEqual("bigint", t.DATA_TYPE);
                            Assert.AreEqual("NO", t.IS_NULLABLE);
                            break;
                        case "datetime2":
                            Assert.AreEqual("datetime2", t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            break;
                        case "int_shared":
                            Assert.AreEqual("int", t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            break;
                        default:
                            throw new Exception();
                    }
                }
                fields = conn.Query("SELECT * FROM [INFORMATION_SCHEMA].[COLUMNS] WHERE [TABLE_NAME]='docSub' ");
                Assert.AreEqual(3, fields.Count());
                foreach (var t in fields)
                {
                    Assert.AreEqual("tst", t.TABLE_SCHEMA);
                    switch (t.COLUMN_NAME)
                    {
                        case "Id":
                            Assert.AreEqual("bigint", t.DATA_TYPE);
                            Assert.AreEqual("NO", t.IS_NULLABLE);
                            break;
                        case "doc" + "Id":
                            Assert.AreEqual("bigint", t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            break;
                        case "int":
                            Assert.AreEqual("int", t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            break;
                        default:
                            throw new Exception();
                    }
                }
                fields = conn.Query("SELECT * FROM [INFORMATION_SCHEMA].[COLUMNS] WHERE [TABLE_NAME]='docSubSub2' ");
                Assert.AreEqual(3, fields.Count());
                foreach (var t in fields)
                {
                    Assert.AreEqual("tst", t.TABLE_SCHEMA);
                    switch (t.COLUMN_NAME)
                    {
                        case "Id":
                            Assert.AreEqual("bigint", t.DATA_TYPE);
                            Assert.AreEqual("NO", t.IS_NULLABLE);
                            break;
                        case "nvarchar":
                            Assert.AreEqual("nvarchar", t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            Assert.AreEqual(10, t.CHARACTER_MAXIMUM_LENGTH);
                            break;
                        case "docSub" + "Id":
                            Assert.AreEqual("bigint", t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            break;
                        default:
                            throw new Exception();
                    }
                }
                fields = conn.Query("SELECT * FROM [INFORMATION_SCHEMA].[COLUMNS] WHERE [TABLE_NAME]='Constants' ");
                Assert.AreEqual(2, fields.Count());
                foreach (var t in fields)
                {
                    Assert.AreEqual("tst", t.TABLE_SCHEMA);
                    switch (t.COLUMN_NAME)
                    {
                        case "Id":
                            Assert.AreEqual("bigint", t.DATA_TYPE);
                            Assert.AreEqual("NO", t.IS_NULLABLE);
                            break;
                        case "int_doc" + "Id":
                            Assert.AreEqual("bigint", t.DATA_TYPE);
                            Assert.AreEqual("YES", t.IS_NULLABLE);
                            break;
                        default:
                            throw new Exception();
                    }
                }
                string fks_query = @"SELECT pt.name as ParentName, cc.name as ChildFieldName , pk.name ParentKeyFieldName
FROM sys.foreign_key_columns fk
INNER JOIN sys.columns cc ON fk.parent_column_id = cc.[column_id] AND fk.parent_object_id = cc.[object_id]
INNER JOIN sys.tables ct ON ct.[object_id] = fk.parent_object_id
INNER JOIN sys.tables pt ON pt.[object_id] = fk.[referenced_object_id]
INNER JOIN sys.columns pk ON fk.referenced_column_id = pk.[column_id] AND fk.[referenced_object_id] = pk.[object_id]
WHERE  ct.name=@table";
                var fks = conn.Query(fks_query, "table".ToDicSql("docSub")).ToList();
                Assert.AreEqual(1, fks.Count());
                Assert.AreEqual("doc", fks[0].ParentName);
                Assert.AreEqual("docId", fks[0].ChildFieldName);
                Assert.AreEqual("Id", fks[0].ParentKeyFieldName);

                fks = conn.Query(fks_query, "table".ToDicSql("docSubSub2")).ToList();
                Assert.AreEqual(1, fks.Count());
                Assert.AreEqual("docSub", fks[0].ParentName);
                Assert.AreEqual("docSubId", fks[0].ChildFieldName);
                Assert.AreEqual("Id", fks[0].ParentKeyFieldName);

                fks = conn.Query(fks_query, "table".ToDicSql("Constants")).ToList();
                Assert.AreEqual(1, fks.Count());
                Assert.AreEqual("doc", fks[0].ParentName);
                Assert.AreEqual("int_docId", fks[0].ChildFieldName);
                Assert.AreEqual("Id", fks[0].ParentKeyFieldName);

                fks = conn.Query(fks_query, "table".ToDicSql("doc")).ToList();
                Assert.AreEqual(0, fks.Count());
            });
        }

        private void Execute(Action<MainPageVM> onInitModel, Action<SqlConnection> onDbUpdated)
        {
            var vm = new MainPageVM(false);
            vm.Compose();
            var plugin = (from p in vm.Model.GroupPlugins.ListPlugins where p.VPlugin is MsSqlPlugin select p).First();
            var connGen = (from p in plugin.ListGenerators where p.Generator is ConnectionGenerator select p).First();
            var cvm = (MsSqlConnectionSettings)connGen.Generator.GetSettingsMvvm(null);
            cvm.Name = "test";
            cvm.DataSource = "DESKTOP-DAD1C";
            cvm.InitialCatalog = "unit_tests";
            //cvm.Authentication = SqlAuthenticationMethod.ActiveDirectoryIntegrated;
            cvm.IntegratedSecurity = true;
            var connstring = cvm.GenerateCode();

            var gen = (from p in plugin.ListGenerators where p.Generator is MsSqlDesignGenerator select p).First();
            var gvm = (MsSqlDesignGeneratorSettings)gen.Generator.GetSettingsMvvm(null);
            gvm.Name = "test";

            onInitModel(vm);

            MigrationOperation[] operations = null;

            var diff = vm.GetDiffModel();
            //EnsureDeletedTestDb(connstring);
            var dgen = (MsSqlDesignGenerator)gen.Generator;

            EnsureDeletedTestDb(connstring);

            dgen.UpdateToModel(connstring, operations, diff, () => { return true; },
                (ex) =>
                {
                    throw ex;
                });

            using (var connection = new SqlConnection(connstring))
            {
                onDbUpdated(connection);
            }
        }

        private MainPageVM CreateModel1()
        {
            var vm = new MainPageVM(false);

            //vm.Model.GroupCatalogs.

            return vm;
        }

        [TestMethod]
        public void MigrationIsNotRemovingIndexes()
        {
            Assert.IsTrue(false);
        }
        [TestMethod]
        public void CanExportImport()
        {
            Assert.IsTrue(false);
        }
    }
}
