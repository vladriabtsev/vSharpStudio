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
        public void Plugin001CanLoad()
        {
            var vm = new MainPageVM(false);
            vm.Compose();
            Assert.IsTrue(vm.ListDbDesignPlugins.Count > 0);
            Assert.IsTrue(vm.Model.GroupPlugins.ListPlugins.Count > 0);
            var lstPlugins = (from p in vm.ListDbDesignPlugins where p is MsSqlMigrator select p).ToList();
            Assert.IsTrue(lstPlugins.Count == 1);
            var lstPlugins2 = (from p in vm.Model.GroupPlugins.ListPlugins where p.VPlugin is MsSqlPlugin select p).ToList();
            Assert.IsTrue(lstPlugins2.Count == 1);
        }
        [TestMethod]
        public void Plugin002CanGetSettingsVM()
        {
            var vm = new MainPageVM(false);
            vm.Compose();
            vm.SelectedDbDesignPlugin = (from p in vm.ListDbDesignPlugins where p.Name == "MsSql" select p).First();
            Assert.IsTrue(vm.SelectedDbDesignPlugin != null);
            Assert.IsTrue(vm.SelectedDbDesignPlugin is MsSqlMigrator);
            //Assert.IsTrue(vm.list.SelectedDbDesignPlugin != null);
            Assert.IsTrue(vm.SelectedDbDesignPlugin is ConnMsSql);
        }
        //#endregion Config

    }
}
