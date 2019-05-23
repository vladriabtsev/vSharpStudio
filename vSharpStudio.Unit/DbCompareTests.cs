using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vSharpStudio.common;
using vSharpStudio.ViewModels;
using vSharpStudio.vm.ViewModels;
using static Proto.Config.proto_data_type.Types;

// https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest
// https://www.automatetheplanet.com/mstest-cheat-sheet/
namespace vSharpStudio.Unit
{
    [TestClass]
    public class DbCompareTests
    {
        public DbCompareTests()
        {
        }
        [TestMethod]
        [DataRow("MsSQL")]
        //[DataRow("Sqlite")]
        //[DataRow("NpgSQL")]
        //[DataRow("MySQL")]
        public void ConstantCanCreateBool(string connStringName)
        {
            Exec(connStringName, (cfg) =>
            {
                cfg.AdNew(new Constant("c1", EnumDataType.Bool));
                //m.AdNew(new Constant("c2", EnumDataType.Numerical, 2, 0, true));

            }, (m) =>
            {
                Assert.AreEqual("", m.DatabaseName);
                Assert.AreEqual("", m.DefaultSchema);
                Assert.AreEqual(1, m.Tables.Count);
                Assert.AreEqual("Constants", m.Tables[0].Name);
                Assert.AreEqual(0, m.Tables[0].Indexes.Count);
                Assert.AreEqual(0, m.Tables[0].UniqueConstraints.Count);
                Assert.AreEqual(0, m.Tables[0].ForeignKeys.Count);
                Assert.AreEqual(1, m.Tables[0].Columns.Count);

                Assert.AreEqual("c1", m.Tables[0].Columns[0].Name);
                Assert.AreEqual(null, m.Tables[0].Columns[0].ComputedColumnSql);
                Assert.AreEqual(null, m.Tables[0].Columns[0].DefaultValueSql);
                Assert.AreEqual(false, m.Tables[0].Columns[0].IsNullable);
                Assert.AreEqual("", m.Tables[0].Columns[0].StoreType);
                Assert.AreEqual(null, m.Tables[0].Columns[0].ValueGenerated);

                //Assert.AreEqual("c2", m.Tables[0].Columns[1].Name);
            });
        }

        void Exec(string connStringName, Action<Config> configCreation, Action<DatabaseModel> assert)
        {
            MainPageVM vm = new MainPageVM(false, (mvm, dbs) =>
            {
                foreach (var t in dbs)
                {
                    if (t.Value is IDbMigrator)
                    {
                        var tt = t.Value as IDbMigrator;
                        if (tt.Name == connStringName)
                        {
                            mvm.SelectedDbDesignPlugin = tt;
                        }
                    }
                }
                if (mvm.SelectedDbDesignPlugin == null)
                    throw new ArgumentException();

                var cfg = mvm.Model;
                configCreation(cfg);

                mvm.SelectedDbDesignPlugin.ConnectionString = mvm.ConnectionString;

                var model = mvm.GetEfModel();
                // Migrate DB
                mvm.SelectedDbDesignPlugin.UpdateToModel(model);

                var m = mvm.SelectedDbDesignPlugin.GetDbModel(new List<string>(), new List<string>());
                assert(m);
            });
        }
    }
}
