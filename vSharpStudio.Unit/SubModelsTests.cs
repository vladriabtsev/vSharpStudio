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
            m.GroupCatalogs.Add(new Catalog() { Name = "c1" });
            var sms = vm.Config.GroupSubModels;
            sms.ListModels.Add(new Model() { Name = "sm1" });
            vm.SaveConfigAsForTests(cfgPath);

            vm = new MainPageVM(true, null, cfgPath);
            Assert.IsTrue(vm.Config.GroupSubModels.ListModels.Count == 1);
            Assert.IsTrue(vm.Config.GroupSubModels.ListModels[0].Name == "sm1");
        }
        [TestMethod]
        public void S02DefaultAllIncludeInModels()
        {
            string cfgPath = "test01.cfg";
            remove_config();
            var vm = new MainPageVM(false);
            var cm = vm.Config.Model;
            cm.GroupCatalogs.Add(new Catalog() { Name = "c1" });
            var sms = vm.Config.GroupSubModels;
            var m = new Model() { Name = "sm1" };
            sms.ListModels.Add(m);
            vm.Config.GroupSubModels[0].EnumDefaultInclusion = EnumIncludeDefaultPolicy.INCLUDE;
            vm.SaveConfigAsForTests(cfgPath);
            Assert.IsTrue(m.CheckIsIncluded(vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);

            vm = new MainPageVM(true, null, cfgPath);
            Assert.IsTrue(vm.Config.GroupSubModels.ListModels.Count == 1);
            Assert.IsTrue(vm.Config.GroupSubModels.ListModels[0].Name == "sm1");
            m = vm.Config.GroupSubModels[0];
            Assert.IsTrue(m.CheckIsIncluded(vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);
        }
        [TestMethod]
        public void S03DefaultAllExcludeInModels()
        {
            string cfgPath = "test01.cfg";
            remove_config();
            var vm = new MainPageVM(false);
            var cm = vm.Config.Model;
            cm.GroupCatalogs.Add(new Catalog() { Name = "c1" });
            var sms = vm.Config.GroupSubModels;
            var m = new Model() { Name = "sm1" };
            sms.ListModels.Add(m);
            vm.Config.GroupSubModels[0].EnumDefaultInclusion = EnumIncludeDefaultPolicy.EXCLUDE;
            vm.SaveConfigAsForTests(cfgPath);
            Assert.IsFalse(m.CheckIsIncluded(vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsFalse(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);

            vm = new MainPageVM(true, null, cfgPath);
            Assert.IsTrue(vm.Config.GroupSubModels.ListModels.Count == 1);
            Assert.IsTrue(vm.Config.GroupSubModels.ListModels[0].Name == "sm1");
            m = vm.Config.GroupSubModels[0];
            Assert.IsFalse(m.CheckIsIncluded(vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsFalse(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);
        }
        [TestMethod]
        public void S04DefaultAllIncludeInModelsButOneExclude()
        {
            string cfgPath = "test01.cfg";
            remove_config();
            var vm = new MainPageVM(false);
            var cat = (Catalog)vm.Config.Model.GroupCatalogs.NodeAddNewSubNode();
            var pr1 = cat.GroupProperties.NodeAddNewSubNode();
            var pr2 = cat.GroupProperties.NodeAddNewSubNode();
            var sms = vm.Config.GroupSubModels;
            var m = new Model() { Name = "sm1" };
            sms.ListModels.Add(m);
            m.IsSelectedChanged(pr1.Guid, null, false);
            vm.Config.GroupSubModels[0].EnumDefaultInclusion = EnumIncludeDefaultPolicy.INCLUDE;
            vm.SaveConfigAsForTests(cfgPath);
            Assert.IsTrue(m.CheckIsIncluded(vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);
            Assert.IsFalse(pr1.CheckIsIncluded(m) ?? false);
            Assert.IsTrue(pr2.CheckIsIncluded(m) ?? false);

            vm = new MainPageVM(true, null, cfgPath);
            Assert.IsTrue(vm.Config.GroupSubModels.ListModels.Count == 1);
            Assert.IsTrue(vm.Config.GroupSubModels.ListModels[0].Name == "sm1");
            m = vm.Config.GroupSubModels[0];
            Assert.IsTrue(m.CheckIsIncluded(vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);
            pr1 = vm.Config.Model.GroupCatalogs[0].GroupProperties[0];
            pr2 = vm.Config.Model.GroupCatalogs[0].GroupProperties[1];
            Assert.IsFalse(pr1.CheckIsIncluded(m) ?? false);
            Assert.IsTrue(pr2.CheckIsIncluded(m) ?? false);
        }
        [TestMethod]
        public void S05DefaultChangeNothingChanged()
        {
            string cfgPath = "test01.cfg";
            remove_config();
            var vm = new MainPageVM(false);
            var cat = (Catalog)vm.Config.Model.GroupCatalogs.NodeAddNewSubNode();
            var pr1 = cat.GroupProperties.NodeAddNewSubNode();
            var pr2 = cat.GroupProperties.NodeAddNewSubNode();
            var sms = vm.Config.GroupSubModels;
            var m = new Model() { Name = "sm1" };
            sms.ListModels.Add(m);
            m.IsSelectedChanged(pr1.Guid, null, false);
            vm.Config.GroupSubModels[0].EnumDefaultInclusion = EnumIncludeDefaultPolicy.INCLUDE;
            vm.SaveConfigAsForTests(cfgPath);
            Assert.IsTrue(m.CheckIsIncluded(vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);
            Assert.IsFalse(pr1.CheckIsIncluded(m) ?? false);
            Assert.IsTrue(pr2.CheckIsIncluded(m) ?? false);
            vm.Config.GroupSubModels[0].EnumDefaultInclusion = EnumIncludeDefaultPolicy.EXCLUDE;
            Assert.IsTrue(m.CheckIsIncluded(vm.Config.Model.GroupCatalogs[0]) ?? false);
            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].CheckIsIncluded(m) ?? false);
            Assert.IsFalse(pr1.CheckIsIncluded(m) ?? false);
            Assert.IsTrue(pr2.CheckIsIncluded(m) ?? false);
        }
    }
}
