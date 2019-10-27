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
using System.IO;

namespace vSharpStudio.Unit
{
    [TestClass]
    public class SubModelsTests
    {
        public SubModelsTests()
        {
            ViewModelBindable.isUnitTests = true;
            MsSqlTests.InitLogging(this);
        }
        private void remove_config()
        {
            if (File.Exists(MainPageVM.CFG_FILE_PATH))
                File.Delete(MainPageVM.CFG_FILE_PATH);
        }
        [TestMethod]
        public void S01CanSaveModels()
        {
            string cfgPath = "test01.cfg";
            remove_config();
            var vm = new MainPageVM(false);
            var m = vm.Config.Model;
            m.GroupCatalogs.AddCatalog("c1");
            var sms = vm.Config.GroupModels;
            sms.AddModel("sm1");
            vm.SaveConfigAsForTests(cfgPath);

            vm = new MainPageVM(true, null, cfgPath);
            Assert.IsTrue(vm.Config.GroupModels.ListModels.Count == 1);
            Assert.IsTrue(vm.Config.GroupModels.ListModels[0].Name == "sm1");
        }
        [TestMethod]
        public void S02DefaultAllIncludeInModels()
        {
            string cfgPath = "test01.cfg";
            remove_config();
            var vm = new MainPageVM(false);
            var cm = vm.Config.Model;
            var sms = vm.Config.GroupModels;
            sms.NodeAddNewSubNode();
            var m = sms.ListModels[0];
            m.EnumDefaultInclusion = EnumIncludeDefaultPolicy.INCLUDE;
            cm.GroupCatalogs.NodeAddNewSubNode();
            vm.SaveConfigAsForTests(cfgPath);
            Assert.IsTrue(ModelExt.CheckIsIncluded(m, vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);

            vm = new MainPageVM(true, null, cfgPath);
            Assert.IsTrue(vm.Config.GroupModels.ListModels.Count == 1);
            m = vm.Config.GroupModels[0];
            Assert.IsTrue(ModelExt.CheckIsIncluded(m, vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);
        }
        [TestMethod]
        public void S03DefaultAllExcludeInModels()
        {
            string cfgPath = "test01.cfg";
            remove_config();
            var vm = new MainPageVM(false);
            var cm = vm.Config.Model;
            cm.GroupCatalogs.AddCatalog("c1");
            var sms = vm.Config.GroupModels;
            var m = sms.AddModel("sm1");
            vm.Config.GroupModels[0].EnumDefaultInclusion = EnumIncludeDefaultPolicy.EXCLUDE;

            vm.SaveConfigAsForTests(cfgPath);
            Assert.IsFalse(ModelExt.CheckIsIncluded(m, vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsFalse(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);

            vm = new MainPageVM(true, null, cfgPath);
            Assert.IsTrue(vm.Config.GroupModels.ListModels.Count == 1);
            Assert.IsTrue(vm.Config.GroupModels.ListModels[0].Name == "sm1");
            m = vm.Config.GroupModels[0];
            Assert.IsFalse(ModelExt.CheckIsIncluded(m, vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsFalse(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);
        }
        [TestMethod]
        public void S04DefaultAllIncludeInModels()
        {
            string cfgPath = "test01.cfg";
            remove_config();
            var vm = new MainPageVM(false);
            var cat = (Catalog)vm.Config.Model.GroupCatalogs.NodeAddNewSubNode();
            var pr1 = cat.GroupProperties.NodeAddNewSubNode();
            var pr2 = cat.GroupProperties.NodeAddNewSubNode();
            var sms = vm.Config.GroupModels;
            var m = sms.AddModel("sm1");
            vm.Config.GroupModels[0].EnumDefaultInclusion = EnumIncludeDefaultPolicy.INCLUDE;

            vm.SaveConfigAsForTests(cfgPath);
            Assert.IsTrue(ModelExt.CheckIsIncluded(m, vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);
            Assert.IsTrue(pr1.CheckIsIncluded(m) ?? false);
            Assert.IsTrue(pr2.CheckIsIncluded(m) ?? false);

            vm = new MainPageVM(true, null, cfgPath);
            Assert.IsTrue(vm.Config.GroupModels.ListModels.Count == 1);
            Assert.IsTrue(vm.Config.GroupModels.ListModels[0].Name == "sm1");
            m = vm.Config.GroupModels[0];
            Assert.IsTrue(ModelExt.CheckIsIncluded(m, vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);
            pr1 = vm.Config.Model.GroupCatalogs[0].GroupProperties[0];
            pr2 = vm.Config.Model.GroupCatalogs[0].GroupProperties[1];
            Assert.IsTrue(pr1.CheckIsIncluded(m) ?? false);
            Assert.IsTrue(pr2.CheckIsIncluded(m) ?? false);
        }
        [TestMethod]
        public void S05DefaultAllIncludeInModelsButOneExclude()
        {
            string cfgPath = "test01.cfg";
            remove_config();
            var vm = new MainPageVM(false);
            var cat = (Catalog)vm.Config.Model.GroupCatalogs.NodeAddNewSubNode();
            var pr1 = cat.GroupProperties.NodeAddNewSubNode();
            var pr2 = cat.GroupProperties.NodeAddNewSubNode();
            var sms = vm.Config.GroupModels;
            var m = sms.AddModel("sm1");
            vm.Config.GroupModels[0].EnumDefaultInclusion = EnumIncludeDefaultPolicy.INCLUDE;

            m.OnIsIncludedChanging(pr2, false);
            Assert.IsTrue(ModelExt.CheckIsIncluded(m, vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);
            Assert.IsTrue(pr1.CheckIsIncluded(m) ?? false);
            Assert.IsFalse(pr2.CheckIsIncluded(m) ?? false);

            vm.SaveConfigAsForTests(cfgPath);
            vm = new MainPageVM(true, null, cfgPath);
            Assert.IsTrue(vm.Config.GroupModels.ListModels.Count == 1);
            Assert.IsTrue(vm.Config.GroupModels.ListModels[0].Name == "sm1");
            m = vm.Config.GroupModels[0];
            Assert.IsTrue(ModelExt.CheckIsIncluded(m, vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);
            pr1 = vm.Config.Model.GroupCatalogs[0].GroupProperties[0];
            pr2 = vm.Config.Model.GroupCatalogs[0].GroupProperties[1];
            Assert.IsTrue(pr1.CheckIsIncluded(m) ?? false);
            Assert.IsFalse(pr2.CheckIsIncluded(m) ?? false);
        }
        [TestMethod]
        public void S06DefaultChangeNothingChanged()
        {
            string cfgPath = "test01.cfg";
            remove_config();
            var vm = new MainPageVM(false);
            var cat = (Catalog)vm.Config.Model.GroupCatalogs.NodeAddNewSubNode();
            var pr1 = cat.GroupProperties.NodeAddNewSubNode();
            var pr2 = cat.GroupProperties.NodeAddNewSubNode();
            var sms = vm.Config.GroupModels;
            var m = sms.AddModel("sm1");
            m.OnIsIncludedChanging(cat, true);
            m.OnIsIncludedChanging(pr1, false);

            Assert.IsTrue(ModelExt.CheckIsIncluded(m, vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);
            Assert.IsFalse(pr1.CheckIsIncluded(m) ?? false);
            Assert.IsTrue(pr2.CheckIsIncluded(m) ?? false);

            vm.Config.GroupModels[0].EnumDefaultInclusion = EnumIncludeDefaultPolicy.INCLUDE;
            Assert.IsTrue(ModelExt.CheckIsIncluded(m, vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);
            Assert.IsFalse(pr1.CheckIsIncluded(m) ?? false);
            Assert.IsTrue(pr2.CheckIsIncluded(m) ?? false);

            vm.Config.GroupModels[0].EnumDefaultInclusion = EnumIncludeDefaultPolicy.EXCLUDE;
            Assert.IsTrue(ModelExt.CheckIsIncluded(m, vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);
            Assert.IsFalse(pr1.CheckIsIncluded(m) ?? false);
            Assert.IsTrue(pr2.CheckIsIncluded(m) ?? false);

            vm.SaveConfigAsForTests(cfgPath);
            vm = new MainPageVM(true, null, cfgPath);
            Assert.IsTrue(ModelExt.CheckIsIncluded(m, vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);
            Assert.IsFalse(pr1.CheckIsIncluded(m) ?? false);
            Assert.IsTrue(pr2.CheckIsIncluded(m) ?? false);

            vm.Config.GroupModels[0].EnumDefaultInclusion = EnumIncludeDefaultPolicy.INCLUDE;
            vm.SaveConfigAsForTests(cfgPath);
            vm = new MainPageVM(true, null, cfgPath);
            Assert.IsTrue(ModelExt.CheckIsIncluded(m, vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);
            Assert.IsFalse(pr1.CheckIsIncluded(m) ?? false);
            Assert.IsTrue(pr2.CheckIsIncluded(m) ?? false);

            vm.Config.GroupModels[0].EnumDefaultInclusion = EnumIncludeDefaultPolicy.EXCLUDE;
            vm.SaveConfigAsForTests(cfgPath);
            vm = new MainPageVM(true, null, cfgPath);
            Assert.IsTrue(ModelExt.CheckIsIncluded(m, vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);
            Assert.IsFalse(pr1.CheckIsIncluded(m) ?? false);
            Assert.IsTrue(pr2.CheckIsIncluded(m) ?? false);
        }
        [TestMethod]
        public void S07AddModelRowsInListInModelsForNewTreeConfigNode()
        {
            var vm = new MainPageVM(false);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs.ListInModels.Count == 0);
            Assert.IsTrue(vm.Config.Model.GroupConstants.ListInModels.Count == 0);

            var m = vm.Config.Model;
            var sms = vm.Config.GroupModels;
            sms.AddModel("sm1");
            m.GroupCatalogs.AddCatalog("c1");
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].ListInModels.Count == 1);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].ListInModels[0].IsIncluded);

            Assert.IsTrue(vm.Config.Model.GroupCatalogs.ListInModels.Count == 1);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs.ListInModels[0].IsIncluded);

            Assert.IsTrue(vm.Config.Model.GroupConstants.ListInModels.Count == 1);
            Assert.IsFalse(vm.Config.Model.GroupConstants.ListInModels[0].IsIncluded);
        }
        [TestMethod]
        public void S11AddModelAddObjectsListInModels()
        {
            var vm = new MainPageVM(false);
            var m = vm.Config.Model;
            m.GroupCatalogs.AddCatalog("c1");
            var sms = vm.Config.GroupModels;
            sms.AddModel("sm1");
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].ListInModels.Count == 1);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].ListInModels[0].IsIncluded);

            Assert.IsTrue(vm.Config.Model.GroupCatalogs.ListInModels.Count == 1);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs.ListInModels[0].IsIncluded);

            Assert.IsTrue(vm.Config.Model.GroupConstants.ListInModels.Count == 1);
            Assert.IsFalse(vm.Config.Model.GroupConstants.ListInModels[0].IsIncluded);
        }
        [TestMethod]
        public void S12CanRemoveModel()
        {
            Assert.IsTrue(false);
        }
        [TestMethod]
        public void S13CanMoveModelInList()
        {
            Assert.IsTrue(false);
        }
        [TestMethod]
        public void S14CanChangeInclusion()
        {
            Assert.IsTrue(false);
        }
    }
}
