using System;
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
            Assert.IsTrue(vm.ListDbTypes.Count > 0);
            Assert.IsTrue(vm.Model.GroupSettings.Db.ListDbConnections.Count == 0);
        }
        [TestMethod]
        public void Plugin002CanLoad()
        {
            var vm = new MainPageVM();
            vm.Compose();
        }
        [TestMethod]
        public void Config002CanSaveAndRestore()
        {
            var cfg = new Config();
            cfg.GroupConstants.NodeAddNewSubNode();
            string json = cfg.ExportToJson();
            Assert.IsTrue(json.Length > 0);
            var cfg2 = new Config(json);
            Assert.IsTrue(cfg2.GroupConstants.Children.Count == 1);
            Assert.IsTrue(cfg2.GroupConstants.Children[0].Name == typeof(Constant).Name + 1);
        }
        [TestMethod]
        public void Config003CanSaveAndRestoreSortingValue()
        {
            var cfg = new Config();
            cfg.GroupConstants.NodeAddNewSubNode();
            cfg.GroupConstants.NodeAddNewSubNode();
            cfg.GroupConstants.Children[1].NodeMoveUp();
            string json = cfg.ExportToJson();
            Assert.IsTrue(json.Length > 0);
            var cfg2 = new Config(json);
            Assert.IsTrue(cfg2.GroupConstants.Children.Count == 2);
            Assert.IsTrue(cfg2.GroupConstants.Children[0].Name == typeof(Constant).Name + 2);
        }
        //#endregion Config

    }
}
