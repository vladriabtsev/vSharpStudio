using System;
using System.Linq;
using vSharpStudio.vm.ViewModels;
using ViewModelBase;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vSharpStudio.ViewModels;

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
            var vm = new MainPageVM();
            vm.Compose();
            Assert.IsTrue(vm.ListDbDesignPlugins.Count > 0);
            Assert.IsTrue(vm.Model.GroupSettings.Db.ListDbConnections.Count == 0);
        }
        [TestMethod]
        public void Plugin002CanGetSettingsVM()
        {
            var vm = new MainPageVM();
            vm.Compose();
            vm.SelectedDbDesignPlugin = (from p in vm.ListDbDesignPlugins where p.Name == "MsSql" select p).First();
            Assert.IsTrue(vm.SelectedDbDesignPluginSettings != null);
            Assert.IsTrue(vm.SelectedDbDesignPluginSettings is ConnMsSql);
        }
        //#endregion Config

    }
}
