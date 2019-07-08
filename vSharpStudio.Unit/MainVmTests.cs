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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Metadata;
using System.IO;

namespace vSharpStudio.Unit
{
    [TestClass]
    public class MainVmTests
    {
        public MainVmTests()
        {
            ViewModelBindable.isUnitTests = true;
        }
        private void remove_config()
        {
            if (File.Exists(MainPageVM.CFG_PATH))
                File.Delete(MainPageVM.CFG_PATH);
        }
        [TestMethod]
        public void Main001StartWithEmptyConfig()
        {
            remove_config();
            var vm = new MainPageVM(true);
            Assert.IsTrue(vm.pconfig_history == null);
        }
        [TestMethod]
        public void Main002CanSaveConfigAndCreateVersions()
        {
            // empty config
            remove_config();
            var vm = new MainPageVM(true);
            Assert.IsTrue(vm.pconfig_history == null);

            // create object and save
            vm.Model.GroupConstants.NodeAddNewSubNode();
            var cnst = (Constant)vm.Model.SelectedNode;
            var ct = DateTime.UtcNow;
            vm.CommandConfigSave.Execute(null);
            Assert.IsTrue(vm.Model.LastUpdated != null);
            Assert.IsTrue(ct <= vm.Model.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Model.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.IsTrue(vm.Model.Version == 0);

            // reload
            vm = new MainPageVM(true);
            Assert.IsTrue(vm.Model.GroupConstants.ListConstants.Count == 1);
            Assert.IsTrue(vm.Model.GroupConstants.ListConstants[0].Name == cnst.Name);
            Assert.IsTrue(ct <= vm.Model.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Model.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.IsTrue(vm.Model.Version == 0);
            Assert.IsTrue(vm.pconfig_history != null);
            Assert.IsTrue(vm.pconfig_history.CurrentConfig != null);
            Assert.IsTrue(vm.pconfig_history.PrevStableConfig == null);
            Assert.IsTrue(vm.pconfig_history.OldStableConfig == null);

            // create stable version
            vm.CommandConfigCreateStableVersion.Execute(null);
            vm = new MainPageVM(true);
            Assert.IsTrue(vm.Model.GroupConstants.ListConstants.Count == 1);
            Assert.IsTrue(vm.Model.GroupConstants.ListConstants[0].Name == cnst.Name);
            Assert.IsTrue(ct <= vm.Model.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Model.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.IsTrue(vm.Model.Version == 1);
            Assert.IsTrue(vm.pconfig_history != null);
            Assert.IsTrue(vm.pconfig_history.CurrentConfig != null);
            Assert.IsTrue(vm.pconfig_history.PrevStableConfig != null);
            Assert.IsTrue(vm.pconfig_history.PrevStableConfig.Version == 0);
            Assert.IsTrue(vm.pconfig_history.OldStableConfig == null);
            // migration code is created?
            //Assert.IsTrue(false);

            // create next stable version
            vm.CommandConfigCreateStableVersion.Execute(null);
            vm = new MainPageVM(true);
            Assert.IsTrue(vm.Model.GroupConstants.ListConstants.Count == 1);
            Assert.IsTrue(vm.Model.GroupConstants.ListConstants[0].Name == cnst.Name);
            Assert.IsTrue(ct <= vm.Model.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Model.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.IsTrue(vm.Model.Version == 2);
            Assert.IsTrue(vm.pconfig_history != null);
            Assert.IsTrue(vm.pconfig_history.CurrentConfig != null);
            Assert.IsTrue(vm.pconfig_history.PrevStableConfig != null);
            Assert.IsTrue(vm.pconfig_history.PrevStableConfig.Version == 1);
            Assert.IsTrue(vm.pconfig_history.OldStableConfig != null);
            Assert.IsTrue(vm.pconfig_history.OldStableConfig.Version == 0);
            // old migration code is kept?
            //Assert.IsTrue(false);
        }
        [TestMethod]
        public void Main003_Diff_Constants()
        {
            // empty config
            remove_config();
            var vm = new MainPageVM(true);

            // create object and save
            var c1 = new Constant() { Name = "c1" };
            vm.Model.GroupConstants.AddConstant(c1);
            var c2 = new Constant() { Name = "c2" };
            vm.Model.GroupConstants.AddConstant(c2);
            var c3 = new Constant() { Name = "c3" };
            vm.Model.GroupConstants.AddConstant(c3);
            vm.CommandConfigSave.Execute(null);

            var diff = vm.GetDiffModel();
            Assert.IsTrue(diff.Constants.ListAll.Count == 3);
            Assert.IsTrue(c1.IsNew());
            Assert.IsTrue(c2.IsNew());
            Assert.IsTrue(c3.IsNew());

            // create stable version (first prev version)
            vm.CommandConfigCreateStableVersion.Execute(null);
            c1.Name = "c1r";
            c3.DataType.Length = 100;
            var c4 = new Constant() { Name = "c4" };
            vm.Model.GroupConstants.AddConstant(c4);
            vm.Model.GroupConstants.Remove(c2);
            diff = vm.GetDiffModel();
            Assert.IsTrue(diff.Constants.ListAll.Count==4);

            Assert.IsTrue(!c1.IsNew());
            Assert.IsTrue(!c1.IsDiffDataType());
            Assert.IsTrue(c1.IsRenamed());
            var cc2 = (from p in diff.Constants.ListAll where p.Name == "c2" select p).Single();
            Assert.IsTrue(cc2.IsDeprecated());
            Assert.IsTrue(!c3.IsNew());
            Assert.IsTrue(c3.IsCanLooseData());
            Assert.IsTrue(c3.IsDiffDataType());
            Assert.IsTrue(c4.IsNew());

            // create next stable version (first oldest version, second prev version)
            vm.CommandConfigCreateStableVersion.Execute(null);
            diff = vm.GetDiffModel();
            Assert.IsTrue(diff.Constants.ListAll.Count == 4);
            Assert.IsTrue(!c1.IsRenamed());
            Assert.IsTrue(!c1.IsDiffDataType());
            cc2 = (from p in diff.Constants.ListAll where p.Name == "c2" select p).Single();
            Assert.IsTrue(!cc2.IsDeprecated());
            Assert.IsTrue(cc2.IsDeleted());
            Assert.IsTrue(!c3.IsDiffDataType());
            Assert.IsTrue(!c4.IsNew());

            vm.CommandConfigCreateStableVersion.Execute(null);
            diff = vm.GetDiffModel();
            Assert.IsTrue(diff.Constants.ListAll.Count == 3); // deleted is removed
        }
        [TestMethod]
        public void Main004_Diff_Enumerations()
        {
            // empty config
            remove_config();
            var vm = new MainPageVM(true);

            // create object and save
            var c1 = new Enumeration() { Name = "c1", DataTypeEnum= EnumEnumerationType.BYTE_VALUE };
            vm.Model.GroupEnumerations.AddEnumeration(c1);
            c1.AddEnumerationPair(new EnumerationPair() { Name="e1", Value="123" });
            var c2 = new Enumeration() { Name = "c2" };
            vm.Model.GroupEnumerations.AddEnumeration(c2);
            var c3 = new Enumeration() { Name = "c3" };
            vm.Model.GroupEnumerations.AddEnumeration(c3);
            vm.CommandConfigSave.Execute(null);

            var diff = vm.GetDiffModel();
            Assert.IsTrue(diff.e.Constants.ListAll.Count == 3);
            Assert.IsTrue(c1.IsNew());
            Assert.IsTrue(c2.IsNew());
            Assert.IsTrue(c3.IsNew());

            // create stable version (first prev version)
            vm.CommandConfigCreateStableVersion.Execute(null);
            c1.Name = "c1r";
            c3.DataType.Length = 100;
            var c4 = new Constant() { Name = "c4" };
            vm.Model.GroupConstants.AddConstant(c4);
            vm.Model.GroupConstants.Remove(c2);
            diff = vm.GetDiffModel();
            Assert.IsTrue(diff.Constants.ListAll.Count == 4);

            Assert.IsTrue(!c1.IsNew());
            Assert.IsTrue(!c1.IsDiffDataType());
            Assert.IsTrue(c1.IsRenamed());
            var cc2 = (from p in diff.Constants.ListAll where p.Name == "c2" select p).Single();
            Assert.IsTrue(cc2.IsDeprecated());
            Assert.IsTrue(!c3.IsNew());
            Assert.IsTrue(c3.IsCanLooseData());
            Assert.IsTrue(c3.IsDiffDataType());
            Assert.IsTrue(c4.IsNew());

            // create next stable version (first oldest version, second prev version)
            vm.CommandConfigCreateStableVersion.Execute(null);
            diff = vm.GetDiffModel();
            Assert.IsTrue(diff.Constants.ListAll.Count == 4);
            Assert.IsTrue(!c1.IsRenamed());
            Assert.IsTrue(!c1.IsDiffDataType());
            cc2 = (from p in diff.Constants.ListAll where p.Name == "c2" select p).Single();
            Assert.IsTrue(!cc2.IsDeprecated());
            Assert.IsTrue(cc2.IsDeleted());
            Assert.IsTrue(!c3.IsDiffDataType());
            Assert.IsTrue(!c4.IsNew());

            vm.CommandConfigCreateStableVersion.Execute(null);
            diff = vm.GetDiffModel();
            Assert.IsTrue(diff.Constants.ListAll.Count == 3); // deleted is removed
        }
    }
}
