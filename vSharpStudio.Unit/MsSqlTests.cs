﻿using System;
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

namespace vSharpStudio.Unit
{
    [TestClass]
    public class MsSqlTests
    {
        public MsSqlTests()
        {
            ViewModelBindable.isUnitTests = true;
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
        public void Db001CanCreateDb()
        {
            var vm = new MainPageVM(false);
            vm.Compose();
            var plugin = (from p in vm.Model.GroupPlugins.ListPlugins where p.VPlugin is MsSqlPlugin select p).First();
            var gen = (from p in plugin.ListGenerators where p.Generator is MsSqlDesignGenerator select p).First();

            var cvm = (MsSqlDesignGeneratorSettings)gen.Generator.GetSettingsMvvm(null);
            cvm.Name = "test";
            var json = cvm.Settings;
            var cvm2 = (MsSqlDesignGeneratorSettings)gen.Generator.GetSettingsMvvm(json);
            Assert.IsTrue(cvm.Name == cvm2.Name);
            var sql = cvm.GenerateCode();
            Assert.AreEqual("", sql);
        }

        [TestMethod]
        public void Db002CanCreateCatalogStructure()
        {
            Assert.IsTrue(false);
        }
        [TestMethod]
        public void Db002CanMigrateCatalogStructure()
        {
            Assert.IsTrue(false);
        }
        [TestMethod]
        public void Db002CanExportImport()
        {
            Assert.IsTrue(false);
        }
    }
}
