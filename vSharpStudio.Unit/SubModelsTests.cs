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
        public void S01CanSaveSubmodels()
        {
            string cfgPath = "test01.cfg";
            remove_config();
            var vm = new MainPageVM(false);
            var m = vm.Config.Model;
            m.GroupCatalogs.Add(new Catalog() { Name="c1" });
            var sms = vm.Config.GroupSubModels;
            sms.ListSubModels.Add(new SubModel() { Name="sm1"});
            vm.SaveConfigAsForTests(cfgPath);
            
            
            //vm.re


            //var cfg = new Config();
            //cfg.Model.GroupCatalogs.Add(vm);
            //vm.GroupProperties.Add(prop);
            //vm.BeginEdit();
            //vm.GroupProperties[0].Name = "test2";
            //vm.CancelEdit();
            //Assert.IsTrue(vm.GroupProperties[0].Name == "test1");
            //vm.BeginEdit();
            //prop = new Property() { Name = "test3" };
            //vm.GroupProperties.Add(prop);
            //Assert.IsTrue(vm.GroupProperties.Count() == 2);
            //vm.CancelEdit();
            //Assert.IsTrue(vm.GroupProperties.Count() == 1);
            //Assert.IsTrue(vm.GroupProperties[0].Name == "test1");
        }
        [TestMethod]
        public void S02DefaultAllIncludeInSubmodels()
        {
            remove_config();
            var vm = new MainPageVM(true);
            Assert.IsTrue(false);
        }
        [TestMethod]
        public void S03DefaultAllExcludeInSubmodels()
        {
            remove_config();
            var vm = new MainPageVM(true);
            Assert.IsTrue(false);
        }
        [TestMethod]
        public void S04DefaultAllIncludeInSubmodelsButOneExclude()
        {
            remove_config();
            var vm = new MainPageVM(true);
            Assert.IsTrue(false);
        }
        [TestMethod]
        public void S05DefaultAllExcludeInSubmodelsButOneInclude()
        {
            remove_config();
            var vm = new MainPageVM(true);
            Assert.IsTrue(false);
        }
        [TestMethod]
        public void S06CanSaveSubmodelsWithDefaultForCatalogs()
        {
            remove_config();
            var vm = new MainPageVM(true);
            Assert.IsTrue(false);

            // but property group always by default is including?
        }
    }
}
