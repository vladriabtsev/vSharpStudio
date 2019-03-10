using System;
using System.IO;
using vSharpStudio.Migration;
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
            var v = new MsSqlServerSchemaReader();
            //MsSqlModel v = new MsSqlModel();
            Assert.True(false);
        }
        [Fact]
        public void MsSql001CanCreateCatalog()
        {
            var cfg = new Config();
            cfg.ConnectionStringName = "MsSql";
            cfg.PathToProjectWithConnectionString = Directory.GetCurrentDirectory() + @"..\..\app.config";
            var c = new Catalog("Test");
            //TODO for Numerical test all cases: int, long, uint, ulong, ...
            c.Properties.ListProperties.Add(new Property("pdouble0", EnumDataType.Numerical, 10, 0));
            c.Properties.ListProperties.Add(new Property("pdouble", EnumDataType.Numerical));
            c.Properties.ListProperties.Add(new Property("penum", EnumDataType.Enum));
            c.Properties.ListProperties.Add(new Property("pstring", EnumDataType.String));
            c.Properties.ListProperties.Add(new Property("pbool", EnumDataType.Bool));
            cfg.Catalogs.ListCatalogs.Add(c);

            //cfg.

            Assert.True(false);
        }
    }
}
