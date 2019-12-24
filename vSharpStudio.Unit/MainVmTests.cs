using System;
using System.Linq;
using vSharpStudio.vm.ViewModels;
using ViewModelBase;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vSharpStudio.ViewModels;
using vSharpStudio.common;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace vSharpStudio.Unit
{
    [TestClass]
    public class MainVmTests
    {
        public MainVmTests()
        {
            ViewModelBindable.isUnitTests = true;
            EditorVmTests.InitLogging(this);
        }

        private void remove_config()
        {
            if (File.Exists(MainPageVM.CFG_FILE_PATH))
            {
                File.Delete(MainPageVM.CFG_FILE_PATH);
            }
        }

        [TestMethod]
        public void Main001StartWithEmptyConfig()
        {
            this.remove_config();
            var vm = new MainPageVM(true);
            Assert.IsTrue(vm.pconfig_history == null);
        }

        [TestMethod]
        public void Main002CanSaveConfigAndCreateVersions()
        {
            // empty config
            this.remove_config();
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
            // Assert.IsTrue(false);

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
            // Assert.IsTrue(false);
        }

        [TestMethod]
        public void Main003_DiffList()
        {
            // new config, not saved yet
            this.remove_config();
            var vm = new MainPageVM(true);
            vm.Config.Name = "main";
            var c1 = vm.Config.Model.GroupConstants.AddConstant("c1");
            var c2 = vm.Config.Model.GroupConstants.AddConstant("c2");
            var c3 = vm.Config.Model.GroupConstants.AddConstant("c3");

            var lstAll = vm.Config.GetDiffConstants(); // Recalculate annotation
            Assert.IsTrue(vm.Config.GetDiffConstant(c1).IsNew());
            Assert.IsFalse(vm.Config.GetDiffConstant(c1).IsDeleted());
            Assert.IsFalse(vm.Config.GetDiffConstant(c1).IsDeprecated());
            Assert.IsFalse(vm.Config.GetDiffConstant(c1).IsRenamed());

            // current changes are saved
            // no stable version (prev is null)
            vm.CommandConfigSave.Execute(null);
            vm = new MainPageVM(true);
            lstAll = vm.Config.GetDiffConstants(); // Recalculate annotation
            Assert.IsTrue(vm.Config.GetDiffConstant(c1).IsNew());
            Assert.IsFalse(vm.Config.GetDiffConstant(c1).IsDeleted());
            Assert.IsFalse(vm.Config.GetDiffConstant(c1).IsDeprecated());
            Assert.IsFalse(vm.Config.GetDiffConstant(c1).IsRenamed());

            // first stable version (prev not null, but oldest is null)
            vm.CommandConfigCreateStableVersion.Execute(null);
            lstAll = vm.Config.GetDiffConstants(); // Recalculate annotation
            Assert.IsFalse(vm.Config.GetDiffConstant(c1).IsNew());
            Assert.IsFalse(vm.Config.GetDiffConstant(c1).IsDeleted());
            Assert.IsFalse(vm.Config.GetDiffConstant(c1).IsDeprecated());
            Assert.IsFalse(vm.Config.GetDiffConstant(c1).IsRenamed());

            vm.Config.Model.GroupConstants[0].Name = "c11";
            vm.Config.Model.GroupConstants.ListConstants.RemoveAt(1);

            Assert.IsTrue(vm.Config.Model.GroupConstants.Count() == 2);
            lstAll = vm.Config.GetDiffConstants(); // Recalculate annotation
            Assert.IsFalse(vm.Config.GetDiffConstant(c1).IsNew());
            Assert.IsFalse(vm.Config.GetDiffConstant(c1).IsDeleted());
            Assert.IsFalse(vm.Config.GetDiffConstant(c1).IsDeprecated());
            Assert.IsTrue(vm.Config.GetDiffConstant(c1).IsRenamed());

            Assert.IsFalse(vm.Config.GetDiffConstant(c2).IsNew());
            Assert.IsFalse(vm.Config.GetDiffConstant(c2).IsDeleted());
            Assert.IsFalse(vm.Config.GetDiffConstant(c2).IsDeprecated());
            Assert.IsFalse(vm.Config.GetDiffConstant(c2).IsRenamed());

            vm = new MainPageVM(true);
            lstAll = vm.Config.GetDiffConstants(); // Recalculate annotation
            Assert.IsFalse(vm.Config.GetDiffConstant(c1).IsNew());
            Assert.IsFalse(vm.Config.GetDiffConstant(c1).IsDeleted());
            Assert.IsFalse(vm.Config.GetDiffConstant(c1).IsDeprecated());
            Assert.IsFalse(vm.Config.GetDiffConstant(c1).IsRenamed());

        }

        [TestMethod]
        public void Main011_Diff_Constants()
        {
            // empty config
            this.remove_config();
            var vm = new MainPageVM(true);

            // create object and save
            var c1 = vm.Config.Model.GroupConstants.AddConstant("c1");
            var c2 = vm.Config.Model.GroupConstants.AddConstant("c2");
            new Constant(vm.Config.Model.GroupConstants) { Name = "c3" };
            var c3 = vm.Config.Model.GroupConstants.AddConstant("c3");
            c3.DataType.Length = 101;
            vm.CommandConfigSave.Execute(null);

            Assert.IsTrue(vm.Config.GetDiffConstants().Count == 3);
            Assert.IsTrue(c1.IsNew());
            Assert.IsTrue(c2.IsNew());
            Assert.IsTrue(c3.IsNew());

            // create stable version (first prev version)
            vm.CommandConfigCreateStableVersion.Execute(null);
            c1.Name = "c1r";
            c3.DataType.Length = 100;
            var c4 = vm.Config.Model.GroupConstants.AddConstant("c4");
            vm.Config.Model.GroupConstants.Remove(c2);
            Assert.IsTrue(vm.Config.GetDiffConstants().Count == 4);

            Assert.IsTrue(!c1.IsNew());
            Assert.IsTrue(c1.IsRenamed());
            var cc2 = (from p in vm.Config.GetDiffConstants() where p.Name == "c2" select p).Single();
            Assert.IsTrue(cc2.IsDeprecated());
            Assert.IsTrue(!c3.IsNew());
            Assert.IsTrue(c3.IsCanLooseData());
            Assert.IsTrue(c4.IsNew());

            // create next stable version (first oldest version, second prev version)
            vm.CommandConfigCreateStableVersion.Execute(null);
            Assert.IsTrue(vm.Config.GetDiffConstants().Count == 4);
            Assert.IsTrue(!c1.IsRenamed());
            cc2 = (from p in vm.Config.GetDiffConstants() where p.Name == "c2" select p).Single();
            Assert.IsTrue(!cc2.IsDeprecated());
            Assert.IsTrue(cc2.IsDeleted());
            Assert.IsTrue(!c4.IsNew());

            vm.CommandConfigCreateStableVersion.Execute(null);
            Assert.IsTrue(vm.Config.GetDiffConstants().Count == 3); // deleted is removed
        }

        [TestMethod]
        public void Main012_Diff_Enumerations()
        {
            // empty config
            this.remove_config();
            var vm = new MainPageVM(true);

            // create object and save
            var c1 = vm.Config.Model.GroupEnumerations.AddEnumeration("c1", EnumEnumerationType.BYTE_VALUE);
            Assert.IsTrue(vm.Config.DicNodes.ContainsKey(c1.Guid));
            var p1 = c1.AddEnumerationPair("e1", "123");
            var p2 = c1.AddEnumerationPair("e2", "124");
            var p3 = c1.AddEnumerationPair("e3", "125");
            var c2 = vm.Config.Model.GroupEnumerations.AddEnumeration("c2", EnumEnumerationType.INTEGER_VALUE);
            Assert.IsTrue(vm.Config.DicNodes.ContainsKey(c2.Guid));
            var c3 = vm.Config.Model.GroupEnumerations.AddEnumeration("c3", EnumEnumerationType.INTEGER_VALUE);
            Assert.IsTrue(vm.Config.DicNodes.ContainsKey(c3.Guid));

            Assert.IsTrue(vm.Config.GetDiffEnumerations().Count == 3);
            Assert.IsTrue(c1.IsNew());
            c1.GetDiffEnumerationPairs();
            Assert.IsTrue(vm.Config.GetDiffEnumerationPair(p1).IsNew());
            Assert.IsTrue(vm.Config.GetDiffEnumerationPair(p2).IsNew());
            Assert.IsTrue(vm.Config.GetDiffEnumerationPair(p3).IsNew());
            Assert.IsTrue(c2.IsNew());
            Assert.IsTrue(c3.IsNew());

            vm.CommandConfigSave.Execute(null);

            Assert.IsTrue(vm.Config.GetDiffEnumerations().Count == 3);
            Assert.IsTrue(c1.IsNew());
            Assert.IsTrue(vm.Config.GetDiffEnumerationPair(p1).IsNew());
            Assert.IsTrue(vm.Config.GetDiffEnumerationPair(p2).IsNew());
            Assert.IsTrue(vm.Config.GetDiffEnumerationPair(p3).IsNew());
            Assert.IsTrue(c2.IsNew());
            Assert.IsTrue(c3.IsNew());

            Assert.IsTrue(vm.Config.DicNodes.ContainsKey(c1.Guid));
            Assert.IsTrue(vm.Config.DicNodes.ContainsKey(c2.Guid));
            Assert.IsTrue(vm.Config.DicNodes.ContainsKey(c3.Guid));

            // create stable version (first prev version)
            vm.CommandConfigCreateStableVersion.Execute(null);
            c1.Name = "c1r";
            c3.DataTypeEnum = EnumEnumerationType.BYTE_VALUE;
            var c4 = vm.Config.Model.GroupEnumerations.AddEnumeration("c4", EnumEnumerationType.INTEGER_VALUE);
            vm.Config.Model.GroupEnumerations.Remove(c2);
            Assert.IsTrue(vm.Config.GetDiffEnumerations().Count == 4);

            Assert.IsTrue(!c1.IsNew());
            Assert.IsTrue(c1.IsRenamed());
            var cc2 = (from p in vm.Config.GetDiffEnumerations() where p.Name == "c2" select p).Single();
            Assert.IsTrue(cc2.IsDeprecated());
            Assert.IsTrue(!c3.IsNew());
            Assert.IsTrue(c4.IsNew());

            Assert.IsTrue(vm.Config.DicNodes.ContainsKey(c1.Guid));
            Assert.IsTrue(!vm.Config.DicNodes.ContainsKey(c2.Guid));
            Assert.IsTrue(vm.Config.DicNodes.ContainsKey(c3.Guid));
            Assert.IsTrue(vm.Config.DicNodes.ContainsKey(c4.Guid));

            // create next stable version (first oldest version, second prev version)
            vm.CommandConfigCreateStableVersion.Execute(null);
            Assert.IsTrue(vm.Config.GetDiffEnumerations().Count == 4);
            Assert.IsTrue(!c1.IsRenamed());
            cc2 = (from p in vm.Config.GetDiffEnumerations() where p.Name == "c2" select p).Single();
            Assert.IsTrue(!cc2.IsDeprecated());
            Assert.IsTrue(cc2.IsDeleted());
            Assert.IsTrue(!c4.IsNew());

            vm.CommandConfigCreateStableVersion.Execute(null);
            Assert.IsTrue(vm.Config.GetDiffEnumerations().Count == 3); // deleted is removed
        }

        [TestMethod]
        public void Main081_BaseConfigLoading()
        {
            // empty config
            this.remove_config();
            var vm = new MainPageVM(true);
            Assert.IsTrue(vm.Config.GroupConfigLinks.Count() == 0);

            // base config
            var vmb = new MainPageVM(false);
            vmb.Config.Name = "ext";
            var c2 = vmb.Config.Model.GroupConstants.AddConstant("c2");
            var path = @".\extcfg\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

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
            this.remove_config();
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
            {
                Directory.CreateDirectory(path);
            }

            vmb.SaveConfigAsForTests(path + MainPageVM.CFG_FILE_NAME);

            // create object and save
            var bcfg = new BaseConfigLink(vm.Config.GroupConfigLinks) { RelativeConfigFilePath = path };
            vm.Config.GroupConfigLinks.AddBaseConfig(bcfg);
            var c1 = vm.Config.Model.GroupConstants.AddConstant("c1");
            vm.CommandConfigSave.Execute(null);

            vm = new MainPageVM(true);
            // var diffc = vm.GetDiffListConfigs();
            // Assert.IsTrue(diffc.Config.Name == "main");
            // Assert.IsTrue(diffc.ListAll.Count == 2);
            // Assert.IsTrue(diffc.ListSubConfigs.Count == 1);
            // Assert.IsTrue(diffc.ListSubConfigs[0].Name == "ext");
            // Assert.IsTrue(diffc.Config.IsNew());
            // Assert.IsFalse(diffc.Config.IsDeleted());
            // Assert.IsFalse(diffc.Config.IsDeprecated());
            // Assert.IsFalse(diffc.Config.IsRenamed());
        }
    }
}
