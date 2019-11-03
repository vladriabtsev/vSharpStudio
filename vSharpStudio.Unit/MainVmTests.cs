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
            MsSqlTests.InitLogging(this);
        }
        private void remove_config()
        {
            if (File.Exists(MainPageVM.CFG_FILE_PATH))
                File.Delete(MainPageVM.CFG_FILE_PATH);
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
            vm.Config.Model.GroupConstants.NodeAddNewSubNode();
            var cnst = (Constant)vm.Config.SelectedNode;
            var ct = DateTime.UtcNow;
            vm.CommandConfigSave.Execute(null);
            Assert.IsTrue(vm.Config.LastUpdated != null);
            Assert.IsTrue(ct <= vm.Config.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Config.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.IsTrue(vm.Config.Version == 0);

            // reload
            vm = new MainPageVM(true);
            Assert.IsTrue(vm.Config.Model.GroupConstants.ListConstants.Count == 1);
            Assert.IsTrue(vm.Config.Model.GroupConstants.ListConstants[0].Name == cnst.Name);
            Assert.IsTrue(ct <= vm.Config.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Config.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.IsTrue(vm.Config.Version == 0);
            Assert.IsTrue(vm.pconfig_history != null);
            Assert.IsTrue(vm.pconfig_history.CurrentConfig != null);
            Assert.IsTrue(vm.pconfig_history.PrevStableConfig == null);
            Assert.IsTrue(vm.pconfig_history.OldStableConfig == null);

            // create stable version
            vm.CommandConfigCreateStableVersion.Execute(null);
            vm = new MainPageVM(true);
            Assert.IsTrue(vm.Config.Model.GroupConstants.ListConstants.Count == 1);
            Assert.IsTrue(vm.Config.Model.GroupConstants.ListConstants[0].Name == cnst.Name);
            Assert.IsTrue(ct <= vm.Config.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Config.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.IsTrue(vm.Config.Version == 1);
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
            Assert.IsTrue(vm.Config.Model.GroupConstants.ListConstants.Count == 1);
            Assert.IsTrue(vm.Config.Model.GroupConstants.ListConstants[0].Name == cnst.Name);
            Assert.IsTrue(ct <= vm.Config.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Config.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.IsTrue(vm.Config.Version == 2);
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
        public void Main003_DiffList()
        {
            // new config, not saved yet
            remove_config();
            var vm = new MainPageVM(true);
            vm.Config.Name = "main";
            var c1 = vm.Config.Model.GroupConstants.AddConstant("c1");
            var c2 = vm.Config.Model.GroupConstants.AddConstant("c2");
            var c3 = vm.Config.Model.GroupConstants.AddConstant("c3");
            var diffc = vm.GetDiffListConfigs();

            Assert.IsTrue(vm.Config.Model.GroupConstants.ListConstants[0].IsNew());

            Assert.IsTrue(vm.Config.Model.GroupConstants[0].IsNew());
            Assert.IsFalse(vm.Config.Model.GroupConstants[0].IsDeleted());
            Assert.IsFalse(vm.Config.Model.GroupConstants[0].IsDeprecated());
            Assert.IsFalse(vm.Config.Model.GroupConstants[0].IsRenamed());

            // current changes are saved
            // no stable version (prev is null)
            vm.CommandConfigSave.Execute(null);
            vm = new MainPageVM(true);
            diffc = vm.GetDiffListConfigs();
            Assert.IsTrue(vm.Config.Model.GroupConstants[0].IsNew());
            Assert.IsFalse(vm.Config.Model.GroupConstants[0].IsDeleted());
            Assert.IsFalse(vm.Config.Model.GroupConstants[0].IsDeprecated());
            Assert.IsFalse(vm.Config.Model.GroupConstants[0].IsRenamed());

            // first stable version (prev not null, but oldest is null)
            vm.CommandConfigCreateStableVersion.Execute(null);
            diffc = vm.GetDiffListConfigs();
            Assert.IsFalse(vm.Config.Model.GroupConstants[0].IsNew());
            Assert.IsFalse(vm.Config.Model.GroupConstants[0].IsDeleted());
            Assert.IsFalse(vm.Config.Model.GroupConstants[0].IsDeprecated());
            Assert.IsFalse(vm.Config.Model.GroupConstants[0].IsRenamed());
            
            vm.Config.Model.GroupConstants[0].Name = "c11";
            vm.Config.Model.GroupConstants.ListConstants.RemoveAt(1);
            diffc = vm.GetDiffListConfigs();

            Assert.IsTrue(vm.Config.Model.GroupConstants.Count() == 2);
            Assert.IsFalse(vm.Config.Model.GroupConstants[0].IsNew());
            Assert.IsFalse(vm.Config.Model.GroupConstants[0].IsDeleted());
            Assert.IsFalse(vm.Config.Model.GroupConstants[0].IsDeprecated());
            Assert.IsTrue(vm.Config.Model.GroupConstants[0].IsRenamed());

            Assert.IsFalse(vm.Config.Model.GroupConstants[1].IsNew());
            Assert.IsFalse(vm.Config.Model.GroupConstants[1].IsDeleted());
            Assert.IsFalse(vm.Config.Model.GroupConstants[1].IsDeprecated());
            Assert.IsFalse(vm.Config.Model.GroupConstants[1].IsRenamed());

            vm = new MainPageVM(true);
            diffc = vm.GetDiffListConfigs();
            Assert.IsFalse(vm.Config.Model.GroupConstants[0].IsNew());
            Assert.IsFalse(vm.Config.Model.GroupConstants[0].IsDeleted());
            Assert.IsFalse(vm.Config.Model.GroupConstants[0].IsDeprecated());
            Assert.IsFalse(vm.Config.Model.GroupConstants[0].IsRenamed());

        }
        [TestMethod]
        public void Main011_Diff_Constants()
        {
            // empty config
            remove_config();
            var vm = new MainPageVM(true);

            // create object and save
            var c1 = vm.Config.Model.GroupConstants.AddConstant("c1");
            var c2 = vm.Config.Model.GroupConstants.AddConstant("c2");
            new Constant(vm.Config.Model.GroupConstants) { Name = "c3" };
            var c3 = vm.Config.Model.GroupConstants.AddConstant("c3");
            c3.DataType.Length = 101;
            vm.CommandConfigSave.Execute(null);

            var diff = vm.GetDiffModel();
            Assert.IsTrue(diff.DiffMainConfig.DiffConfigModel.Constants.ListAll.Count == 3);
            Assert.IsTrue(c1.IsNew());
            Assert.IsTrue(c2.IsNew());
            Assert.IsTrue(c3.IsNew());

            // create stable version (first prev version)
            vm.CommandConfigCreateStableVersion.Execute(null);
            c1.Name = "c1r";
            c3.DataType.Length = 100;
            var c4 = vm.Config.Model.GroupConstants.AddConstant("c4");
            vm.Config.Model.GroupConstants.Remove(c2);
            diff = vm.GetDiffModel();
            Assert.IsTrue(diff.DiffMainConfig.DiffConfigModel.Constants.ListAll.Count == 4);

            Assert.IsTrue(!c1.IsNew());
            Assert.IsTrue(c1.GetDiffDataType() == null);
            Assert.IsTrue(c1.IsRenamed());
            var cc2 = (from p in diff.DiffMainConfig.DiffConfigModel.Constants.ListAll where p.Name == "c2" select p).Single();
            Assert.IsTrue(cc2.IsDeprecated());
            Assert.IsTrue(!c3.IsNew());
            Assert.IsTrue(c3.IsCanLooseData());
            Assert.IsTrue(c3.GetDiffDataType() != null);
            Assert.IsTrue(c4.IsNew());

            // create next stable version (first oldest version, second prev version)
            vm.CommandConfigCreateStableVersion.Execute(null);
            diff = vm.GetDiffModel();
            Assert.IsTrue(diff.DiffMainConfig.DiffConfigModel.Constants.ListAll.Count == 4);
            Assert.IsTrue(!c1.IsRenamed());
            Assert.IsTrue(c1.GetDiffDataType() == null);
            cc2 = (from p in diff.DiffMainConfig.DiffConfigModel.Constants.ListAll where p.Name == "c2" select p).Single();
            Assert.IsTrue(!cc2.IsDeprecated());
            Assert.IsTrue(cc2.IsDeleted());
            Assert.IsTrue(c3.GetDiffDataType() == null);
            Assert.IsTrue(!c4.IsNew());

            vm.CommandConfigCreateStableVersion.Execute(null);
            diff = vm.GetDiffModel();
            Assert.IsTrue(diff.DiffMainConfig.DiffConfigModel.Constants.ListAll.Count == 3); // deleted is removed
        }
        [TestMethod]
        public void Main012_Diff_Enumerations()
        {
            // empty config
            remove_config();
            var vm = new MainPageVM(true);

            // create object and save
            var c1 = vm.Config.Model.GroupEnumerations.AddEnumeration("c1", EnumEnumerationType.BYTE_VALUE);
            c1.AddEnumerationPair("e1", "123");
            c1.AddEnumerationPair("e2", "124");
            c1.AddEnumerationPair("e3", "125");
            var c2 = vm.Config.Model.GroupEnumerations.AddEnumeration("c2", EnumEnumerationType.INTEGER_VALUE);
            var c3 = vm.Config.Model.GroupEnumerations.AddEnumeration("c3", EnumEnumerationType.INTEGER_VALUE);
            vm.CommandConfigSave.Execute(null);

            var diff = vm.GetDiffModel();
            Assert.IsTrue(diff.DiffMainConfig.DiffConfigModel.Enumerations.ListAll.Count == 3);
            Assert.IsTrue(c1.IsNew());
            Assert.IsTrue(c1.ListEnumerationPairs[0].IsNew());
            Assert.IsTrue(c1.ListEnumerationPairs[1].IsNew());
            Assert.IsTrue(c1.ListEnumerationPairs[2].IsNew());
            Assert.IsTrue(c2.IsNew());
            Assert.IsTrue(c3.IsNew());

            // create stable version (first prev version)
            vm.CommandConfigCreateStableVersion.Execute(null);
            c1.Name = "c1r";
            c3.DataTypeEnum = EnumEnumerationType.BYTE_VALUE;
            var c4 = vm.Config.Model.GroupEnumerations.AddEnumeration("c4", EnumEnumerationType.INTEGER_VALUE);
            vm.Config.Model.GroupEnumerations.Remove(c2);
            diff = vm.GetDiffModel();
            Assert.IsTrue(diff.DiffMainConfig.DiffConfigModel.Enumerations.ListAll.Count == 4);

            Assert.IsTrue(!c1.IsNew());
            Assert.IsTrue(c1.GetDiffEnumerationType() == null);
            Assert.IsTrue(c1.IsRenamed());
            var cc2 = (from p in diff.DiffMainConfig.DiffConfigModel.Enumerations.ListAll where p.Name == "c2" select p).Single();
            Assert.IsTrue(cc2.IsDeprecated());
            Assert.IsTrue(!c3.IsNew());
            Assert.IsTrue(c3.GetDiffEnumerationType() != null);
            Assert.IsTrue(c4.IsNew());

            // create next stable version (first oldest version, second prev version)
            vm.CommandConfigCreateStableVersion.Execute(null);
            diff = vm.GetDiffModel();
            Assert.IsTrue(diff.DiffMainConfig.DiffConfigModel.Enumerations.ListAll.Count == 4);
            Assert.IsTrue(!c1.IsRenamed());
            Assert.IsTrue(c1.GetDiffEnumerationType() == null);
            cc2 = (from p in diff.DiffMainConfig.DiffConfigModel.Enumerations.ListAll where p.Name == "c2" select p).Single();
            Assert.IsTrue(!cc2.IsDeprecated());
            Assert.IsTrue(cc2.IsDeleted());
            Assert.IsTrue(c3.GetDiffEnumerationType() == null);
            Assert.IsTrue(!c4.IsNew());

            vm.CommandConfigCreateStableVersion.Execute(null);
            diff = vm.GetDiffModel();
            Assert.IsTrue(diff.DiffMainConfig.DiffConfigModel.Enumerations.ListAll.Count == 3); // deleted is removed
        }
        [TestMethod]
        public void Main077_Diff_Model()
        {
            // empty config
            remove_config();
            var vm = new MainPageVM(true);
            var diff = vm.GetDiffModel();
        }
        [TestMethod]
        public void Main081_BaseConfigLoading()
        {
            // empty config
            remove_config();
            var vm = new MainPageVM(true);
            Assert.IsTrue(vm.Config.GroupConfigLinks.Count() == 0);

            // base config
            var vmb = new MainPageVM(false);
            vmb.Config.Name = "ext";
            var c2 = vmb.Config.Model.GroupConstants.AddConstant("c2");
            var path = @".\extcfg\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            vmb.SaveConfigAsForTests(path + MainPageVM.CFG_FILE_NAME);

            // create object and save
            var bcfg = new BaseConfigLink(vm.Config.GroupConfigLinks) { RelativeConfigFilePath = path };
            vm.Config.GroupConfigLinks.AddBaseConfig(bcfg);
            var c1 = vm.Config.Model.GroupConstants.AddConstant("c1");
            vm.CommandConfigSave.Execute(null);

            vm = new MainPageVM(true);
            Assert.IsTrue(vm.Config.Model.GroupConstants.Count() == 1);
            Assert.IsTrue(vm.Config.Model.GroupConstants[0].Name == "c1");
            Assert.IsTrue(vm.Config.GroupConfigLinks.Count() == 1);
            Assert.IsTrue(vm.Config.GroupConfigLinks[0].Config.Model.GroupConstants.ListConstants.Count() == 1);
            Assert.IsTrue(vm.Config.GroupConfigLinks[0].Config.Model.GroupConstants[0].Name == "c2");
            Assert.IsTrue(vm.Config.GroupConfigLinks[0].Config.Name == "ext");
            Assert.IsTrue(vm.Config.GroupConfigLinks[0].Name == "ext");
        }
        [TestMethod]
        public void Main082_BaseConfigDiff()
        {
            // empty config
            remove_config();
            var vm = new MainPageVM(true);
            vm.Config.Name = "main";
            var c3 = vm.Config.Model.GroupConstants.AddConstant("c3");
            Assert.IsTrue(vm.Config.GroupConfigLinks.Count() == 0);

            // base config
            var vmb = new MainPageVM(false);
            vmb.Config.Name = "ext";
            var c2 = vmb.Config.Model.GroupConstants.AddConstant("c2");
            var path = @".\extcfg\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            vmb.SaveConfigAsForTests(path + MainPageVM.CFG_FILE_NAME);

            // create object and save
            var bcfg = new BaseConfigLink(vm.Config.GroupConfigLinks) { RelativeConfigFilePath = path };
            vm.Config.GroupConfigLinks.AddBaseConfig(bcfg);
            var c1 = vm.Config.Model.GroupConstants.AddConstant("c1");
            vm.CommandConfigSave.Execute(null);

            vm = new MainPageVM(true);
            var diffc = vm.GetDiffListConfigs();
            Assert.IsTrue(diffc.Config.Name == "main");
            Assert.IsTrue(diffc.ListAll.Count == 2);
            Assert.IsTrue(diffc.ListSubConfigs.Count == 1);
            Assert.IsTrue(diffc.ListSubConfigs[0].Name == "ext");
            Assert.IsTrue(diffc.Config.IsNew());
            Assert.IsFalse(diffc.Config.IsDeleted());
            Assert.IsFalse(diffc.Config.IsDeprecated());
            Assert.IsFalse(diffc.Config.IsRenamed());
        }
    }
}
