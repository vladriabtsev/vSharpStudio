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
using Microsoft.Extensions.Logging;
using System.Threading;

namespace vSharpStudio.Unit
{
    [TestClass]
    public class MainVmTests
    {
        string pathExt = @".\extcfg\";

        static MainVmTests()
        {
            LoggerInit.Init();
        }
        private static Microsoft.Extensions.Logging.ILogger _logger;
        public MainVmTests()
        {
            VmBindable.isUnitTests = true;
            if (_logger == null)
                //_logger = Logger.ServiceProvider.GetRequiredService<ILogger<PluginTests>>();
                _logger = Logger.CreateLogger<PluginTests>();
        }

        private void remove_config()
        {
            if (File.Exists(MainPageVM.USER_SETTINGS_FILE_PATH))
            {
                File.Delete(MainPageVM.USER_SETTINGS_FILE_PATH);
            }
            if (Directory.Exists(pathExt))
            {
                Directory.Delete(pathExt, true);
            }
        }

        [TestMethod]
        public void Main001StartWithEmptyConfig()
        {
            this.remove_config();
            var vm = new MainPageVM(true);
            vm.OnFormLoaded();
            Assert.IsTrue(vm.pconfig_history == null);
        }

        [TestMethod]
        public void Main002CanSaveConfigAndCreateVersions()
        {
            // empty config
            this.remove_config();
            var vm = new MainPageVM(true);
            vm.OnFormLoaded();
            Assert.IsTrue(vm.pconfig_history == null);

            // create object and save
            vm.Config.Model.GroupConstants.NodeAddNewSubNode();
            var cnst = (Constant)vm.Config.SelectedNode;
            var ct = DateTime.UtcNow;
            vm.CommandConfigSaveAs.Execute(@".\kuku.vcfg");
            Assert.IsTrue(vm.Config.LastUpdated != null);
            Assert.IsTrue(ct <= vm.Config.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Config.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.IsTrue(vm.Config.Version == 0);

            // reload
            vm = new MainPageVM(true);
            vm.OnFormLoaded();
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
            vm.OnFormLoaded();
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
            vm.OnFormLoaded();
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
        public void Main003CanSaveConfigInSelectedSolutionFolderAndReloadFromThisFolderByDefault()
        {
            this.remove_config();
            var vm = new MainPageVM(false);
            vm.OnFormLoaded();
            Assert.AreEqual(0, vm.UserSettings.ListOpenConfigHistory.Count);
            vm.Config.Name = "test1";
            vm.CommandConfigSaveAs.Execute(@"..\..\..\TestApps\config.vcfg");

            vm = new MainPageVM(true);
            vm.OnFormLoaded();
            // Load from previous save
            Assert.AreEqual(1, vm.UserSettings.ListOpenConfigHistory.Count);
            Assert.AreEqual("test1", vm.Config.Name);
            vm.Config.Name = "test2";
            vm.CommandConfigSaveAs.Execute(@"..\..\..\TestApps\config2.vcfg");

            vm = new MainPageVM(true);
            vm.OnFormLoaded();
            // Load from previous save
            Assert.AreEqual(2, vm.UserSettings.ListOpenConfigHistory.Count);
            Assert.AreEqual("test2", vm.Config.Name);
        }
        [TestMethod]
        public void Main004CanSaveConfigAndReload()
        {
            this.remove_config();
            var vm = new MainPageVM(false);
            vm.OnFormLoaded();
            Assert.AreEqual(0, vm.UserSettings.ListOpenConfigHistory.Count);
            vm.Config.Name = "test1";
            var c1 = vm.Config.Model.GroupConstants.AddConstant("c1");
            vm.CommandConfigSaveAs.Execute(@"..\..\..\TestApps\config.vcfg");

            vm = new MainPageVM(true);
            vm.OnFormLoaded();
            // Load from previous save
            Assert.AreEqual(1, vm.UserSettings.ListOpenConfigHistory.Count);
            Assert.AreEqual("test1", vm.Config.Name);
            Assert.AreEqual(1, vm.Config.Model.GroupConstants.Count());
            Assert.AreEqual("c1", vm.Config.Model.GroupConstants[0].Name);
            Assert.IsTrue(vm.Config.DicNodes.ContainsKey(c1.Guid));
        }
        [TestMethod]
        public void Main005IsChangedAndIsTreeChanged()
        {
            this.remove_config();
            var vm = new MainPageVM(false);
            vm.OnFormLoaded();
            vm.Config.Name = "test1";
            Assert.AreEqual(0, vm.UserSettings.ListOpenConfigHistory.Count);
            Assert.IsTrue(vm.Config.IsChanged == true);
            Assert.IsTrue(vm.Config.IsSubTreeChanged == true);
            Assert.IsTrue(vm.Config.Model.GroupConstants.IsChanged == true);
            Assert.IsTrue(vm.Config.Model.GroupConstants.IsSubTreeChanged == false);

            vm.CommandConfigSaveAs.Execute(@"..\..\..\TestApps\config.vcfg");
            Assert.IsTrue(vm.Config.IsChanged == false);
            Assert.IsTrue(vm.Config.IsSubTreeChanged == false);
            Assert.IsTrue(vm.Config.Model.GroupConstants.IsChanged == false);
            Assert.IsTrue(vm.Config.Model.GroupConstants.IsSubTreeChanged == false);

            vm.Config.Model.GroupConstants.NodeAddNewSubNode();
            Assert.IsTrue(vm.Config.IsChanged == false);
            Assert.IsTrue(vm.Config.IsSubTreeChanged == true);
            Assert.IsTrue(vm.Config.Model.GroupConstants.IsChanged == true);
            Assert.IsTrue(vm.Config.Model.GroupConstants.IsSubTreeChanged == true);
            Assert.IsTrue(vm.Config.Model.GroupConstants[0].IsChanged == true);
            Assert.IsTrue(vm.Config.Model.GroupConstants[0].IsSubTreeChanged == false);

            vm.CommandConfigSave.Execute(null);
            Assert.IsTrue(vm.Config.IsChanged == false);
            Assert.IsTrue(vm.Config.IsSubTreeChanged == false);
            Assert.IsTrue(vm.Config.Model.GroupConstants.IsChanged == false);
            Assert.IsTrue(vm.Config.Model.GroupConstants.IsSubTreeChanged == false);
            Assert.IsTrue(vm.Config.Model.GroupConstants[0].IsChanged == false);
            Assert.IsTrue(vm.Config.Model.GroupConstants[0].IsSubTreeChanged == false);

            vm = new MainPageVM(true);
            vm.OnFormLoaded();
            Assert.IsTrue(vm.Config.IsChanged == false);
            Assert.IsTrue(vm.Config.IsSubTreeChanged == false);
            Assert.IsTrue(vm.Config.Model.GroupConstants.IsChanged == false);
            Assert.IsTrue(vm.Config.Model.GroupConstants.IsSubTreeChanged == false);
        }
        [TestMethod]
        public void Main009_Diff()
        {
            // new config, not saved yet
            this.remove_config();
            var vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose();
            vm.Config.Name = "main";
            var c1 = vm.Config.Model.GroupConstants.AddConstant("c1");
            var c2 = vm.Config.Model.GroupConstants.AddConstant("c2");
            var c3 = vm.Config.Model.GroupConstants.AddConstant("c3");

            var mod = new ModelVisitorForAnnotation();
            var cfgDiff = mod.GetDiffAnnotatedConfig(vm.Config, vm.Config.PrevStableConfig, vm.Config.OldStableConfig); // Recalculate annotation

            var c1Diff = (from p in cfgDiff.Model.GroupConstants.ListConstants where p.Name == "c1" select p).Single();
            Assert.AreEqual(c1Diff, cfgDiff.DicNodes[c1.Guid]);

            //var c1Diff = (Constant)cfgDiff.DicNodes[c1.Guid];
            //Assert.AreNotEqual(c1, (Constant)cfgDiff.DicNodes[c1.Guid]);
            //Assert.IsTrue(c1Diff.IsNew());
            //Assert.IsFalse(c1Diff.IsDeleted());
            //Assert.IsFalse(c1Diff.IsDeprecated());
            //Assert.IsFalse(c1Diff.IsRenamed());
        }
        [TestMethod]
        public void Main010_DiffList()
        {
            // new config, not saved yet
            this.remove_config();
            var vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose();
            vm.Config.Name = "main";
            var c1 = vm.Config.Model.GroupConstants.AddConstant("c1");
            var c2 = vm.Config.Model.GroupConstants.AddConstant("c2");
            var c3 = vm.Config.Model.GroupConstants.AddConstant("c3");

            var mod = new ModelVisitorForAnnotation();
            var cfgDiff = mod.GetDiffAnnotatedConfig(vm.Config, vm.Config.PrevStableConfig, vm.Config.OldStableConfig); // Recalculate annotation
            var c1Diff = (Constant)cfgDiff.DicNodes[c1.Guid];
            Assert.AreNotEqual(c1, (Constant)cfgDiff.DicNodes[c1.Guid]);
            Assert.IsTrue(c1Diff.IsNew());
            Assert.IsFalse(c1Diff.IsDeleted());
            Assert.IsFalse(c1Diff.IsDeprecated());
            Assert.IsFalse(c1Diff.IsRenamed());
            // current changes are saved
            // no stable version (prev is null)
            vm.CommandConfigSaveAs.Execute(@".\kuku.vcfg");
            vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose();
            c1 = (Constant)(vm.Config.DicNodes[c1.Guid]);
            c2 = (Constant)(vm.Config.DicNodes[c2.Guid]);
            mod = new ModelVisitorForAnnotation();
            cfgDiff = mod.GetDiffAnnotatedConfig(vm.Config, vm.Config.PrevStableConfig, vm.Config.OldStableConfig); // Recalculate annotation
            c1Diff = (Constant)cfgDiff.DicNodes[c1.Guid];
            Assert.IsTrue(c1Diff.IsNew());
            Assert.IsFalse(c1Diff.IsDeleted());
            Assert.IsFalse(c1Diff.IsDeprecated());
            Assert.IsFalse(c1Diff.IsRenamed());

            // first stable version (prev not null, but oldest is null)
            vm.CommandConfigCreateStableVersion.Execute(null);
            cfgDiff = mod.GetDiffAnnotatedConfig(vm.Config, vm.Config.PrevStableConfig, vm.Config.OldStableConfig); // Recalculate annotation
            c1Diff = (Constant)cfgDiff.DicNodes[c1.Guid];
            Assert.IsFalse(c1Diff.IsNew());
            Assert.IsFalse(c1Diff.IsDeleted());
            Assert.IsFalse(c1Diff.IsDeprecated());
            Assert.IsFalse(c1Diff.IsRenamed());

            ((Constant)vm.Config.DicNodes[c1.Guid]).Name = "c11";
            vm.Config.Model.GroupConstants.ListConstants.Remove((Constant)vm.Config.DicNodes[c2.Guid]);

            Assert.AreEqual(2, vm.Config.Model.GroupConstants.Count());
            cfgDiff = mod.GetDiffAnnotatedConfig(vm.Config, vm.Config.PrevStableConfig, vm.Config.OldStableConfig); // Recalculate annotation
            Assert.AreEqual(3, cfgDiff.Model.GroupConstants.Count());
            c1Diff = (Constant)cfgDiff.DicNodes[c1.Guid];
            Assert.IsFalse(c1Diff.IsNew());
            Assert.IsFalse(c1Diff.IsDeleted());
            Assert.IsFalse(c1Diff.IsDeprecated());
            Assert.IsTrue(c1Diff.IsRenamed());

            var c2Diff = (Constant)cfgDiff.DicNodes[c2.Guid];
            Assert.IsFalse(c2Diff.IsNew());
            Assert.IsFalse(c2Diff.IsDeleted());
            Assert.IsTrue(c2Diff.IsDeprecated());
            Assert.IsFalse(c2Diff.IsRenamed());

            vm.CommandConfigSave.Execute(null);
            vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose();
            c1 = (Constant)(vm.Config.DicNodes[c1.Guid]);
            c2 = (Constant)(vm.Config.DicNodes[c2.Guid]);
            mod = new ModelVisitorForAnnotation();
            cfgDiff = mod.GetDiffAnnotatedConfig(vm.Config, vm.Config.PrevStableConfig, vm.Config.OldStableConfig); // Recalculate annotation
            c1Diff = (Constant)cfgDiff.DicNodes[c1.Guid];
            Assert.IsFalse(c1Diff.IsNew());
            Assert.IsFalse(c1Diff.IsDeleted());
            Assert.IsFalse(c1Diff.IsDeprecated());
            Assert.IsTrue(c1Diff.IsRenamed());
        }
        [TestMethod]
        public void Main011_Diff_Constants()
        {
            // empty config
            this.remove_config();
            var vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose();
            // create object and save
            var c1 = vm.Config.Model.GroupConstants.AddConstant("c1");
            var c2 = vm.Config.Model.GroupConstants.AddConstant("c2");
            //new Constant(vm.Config.Model.GroupConstants) { Name = "c3" };
            var c3 = vm.Config.Model.GroupConstants.AddConstant("c3");
            c3.DataType.Length = 101;
            vm.CommandConfigSaveAs.Execute(@".\kuku.vcfg");

            var mod = new ModelVisitorForAnnotation();
            var cfgDiff = mod.GetDiffAnnotatedConfig(vm.Config, vm.Config.PrevStableConfig, vm.Config.OldStableConfig); // Recalculate annotation
            Assert.IsTrue(cfgDiff.Model.GroupConstants.ListConstants.Count() == 3);
            var c1Diff = (Constant)cfgDiff.DicNodes[c1.Guid];
            Assert.IsTrue(c1Diff.IsNew());
            var c2Diff = (Constant)cfgDiff.DicNodes[c1.Guid];
            Assert.IsTrue(c2Diff.IsNew());
            var c3Diff = (Constant)cfgDiff.DicNodes[c1.Guid];
            Assert.IsTrue(c3Diff.IsNew());

            // create stable version (first prev version)
            vm.CommandConfigCreateStableVersion.Execute(null);
            c1.Name = "c1r";
            c3.DataType.Length = 100;
            var c4 = vm.Config.Model.GroupConstants.AddConstant("c4");
            vm.Config.Model.GroupConstants.Remove(c2);

            cfgDiff = mod.GetDiffAnnotatedConfig(vm.Config, vm.Config.PrevStableConfig, vm.Config.OldStableConfig); // Recalculate annotation
            Assert.AreEqual(4, cfgDiff.Model.GroupConstants.ListConstants.Count());
            c1Diff = (Constant)cfgDiff.DicNodes[c1.Guid];
            Assert.IsTrue(!c1Diff.IsNew());
            Assert.IsTrue(c1Diff.IsRenamed());
            var cc2 = (from p in cfgDiff.Model.GroupConstants.ListConstants where p.Name == "c2" select p).Single();
            Assert.IsTrue(cc2.IsDeprecated());
            c3Diff = (Constant)cfgDiff.DicNodes[c3.Guid];
            Assert.IsTrue(!c3Diff.IsNew());
            Assert.IsTrue(c3Diff.IsCanLooseData());
            var c4Diff = (Constant)cfgDiff.DicNodes[c4.Guid];
            Assert.IsTrue(c4Diff.IsNew());

            // create next stable version (first oldest version, second prev version)
            vm.CommandConfigCreateStableVersion.Execute(null);
            cfgDiff = mod.GetDiffAnnotatedConfig(vm.Config, vm.Config.PrevStableConfig, vm.Config.OldStableConfig); // Recalculate annotation
            Assert.AreEqual(4, cfgDiff.Model.GroupConstants.ListConstants.Count());
            c1Diff = (Constant)cfgDiff.DicNodes[c1.Guid];
            Assert.IsTrue(!c1Diff.IsRenamed());
            cc2 = (from p in cfgDiff.Model.GroupConstants.ListConstants where p.Name == "c2" select p).Single();
            Assert.IsTrue(!cc2.IsDeprecated());
            Assert.IsTrue(cc2.IsDeleted());
            c4Diff = (Constant)cfgDiff.DicNodes[c4.Guid];
            Assert.IsTrue(!c4Diff.IsNew());

            vm.CommandConfigCreateStableVersion.Execute(null);
            cfgDiff = mod.GetDiffAnnotatedConfig(vm.Config, vm.Config.PrevStableConfig, vm.Config.OldStableConfig); // Recalculate annotation
            Assert.AreEqual(3, cfgDiff.Model.GroupConstants.ListConstants.Count()); // deleted is removed
        }
        [TestMethod]
        public void Main012_Diff_Enumerations()
        {
            // empty config
            this.remove_config();
            var vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose();
            vm.CommandConfigSaveAs.Execute(@".\kuku.vcfg");

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

            var mod = new ModelVisitorForAnnotation();
            var cfgDiff = mod.GetDiffAnnotatedConfig(vm.Config, vm.Config.PrevStableConfig, vm.Config.OldStableConfig); // Recalculate annotation

            Assert.IsTrue(cfgDiff.Model.GroupEnumerations.ListEnumerations.Count() == 3);
            var c1Diff = (Enumeration)cfgDiff.DicNodes[c1.Guid];
            Assert.IsTrue(c1Diff.IsNew());
            var p1Diff = (EnumerationPair)cfgDiff.DicNodes[p1.Guid];
            Assert.IsTrue(p1Diff.IsNew());
            var p2Diff = (EnumerationPair)cfgDiff.DicNodes[p2.Guid];
            Assert.IsTrue(p2Diff.IsNew());
            var p3Diff = (EnumerationPair)cfgDiff.DicNodes[p3.Guid];
            Assert.IsTrue(p3Diff.IsNew());
            var c2Diff = (Enumeration)cfgDiff.DicNodes[c2.Guid];
            Assert.IsTrue(c2Diff.IsNew());
            var c3Diff = (Enumeration)cfgDiff.DicNodes[c3.Guid];
            Assert.IsTrue(c3Diff.IsNew());

            vm.CommandConfigSave.Execute(null);
            cfgDiff = mod.GetDiffAnnotatedConfig(vm.Config, vm.Config.PrevStableConfig, vm.Config.OldStableConfig); // Recalculate annotation
            Assert.IsTrue(cfgDiff.DicNodes.ContainsKey(c1.Guid));
            Assert.IsTrue(cfgDiff.DicNodes.ContainsKey(c2.Guid));
            Assert.IsTrue(cfgDiff.DicNodes.ContainsKey(c3.Guid));
            Assert.IsTrue(cfgDiff.Model.GroupEnumerations.ListEnumerations.Count() == 3);
            c1Diff = (Enumeration)cfgDiff.DicNodes[c1.Guid];
            Assert.IsTrue(c1Diff.IsNew());
            p1Diff = (EnumerationPair)cfgDiff.DicNodes[p1.Guid];
            Assert.IsTrue(p1Diff.IsNew());
            p2Diff = (EnumerationPair)cfgDiff.DicNodes[p2.Guid];
            Assert.IsTrue(p2Diff.IsNew());
            p3Diff = (EnumerationPair)cfgDiff.DicNodes[p3.Guid];
            Assert.IsTrue(p3Diff.IsNew());
            c2Diff = (Enumeration)cfgDiff.DicNodes[c2.Guid];
            Assert.IsTrue(c2Diff.IsNew());
            c3Diff = (Enumeration)cfgDiff.DicNodes[c3.Guid];
            Assert.IsTrue(c3Diff.IsNew());

            // create stable version (first prev version)
            vm.CommandConfigCreateStableVersion.Execute(null);
            c1.Name = "c1r";
            c3.DataTypeEnum = EnumEnumerationType.BYTE_VALUE;
            var c4 = vm.Config.Model.GroupEnumerations.AddEnumeration("c4", EnumEnumerationType.INTEGER_VALUE);
            vm.Config.Model.GroupEnumerations.Remove(c2);

            cfgDiff = mod.GetDiffAnnotatedConfig(vm.Config, vm.Config.PrevStableConfig, vm.Config.OldStableConfig); // Recalculate annotation
            Assert.IsTrue(cfgDiff.DicNodes.ContainsKey(c1.Guid));
            Assert.IsFalse(!cfgDiff.DicNodes.ContainsKey(c2.Guid));
            Assert.IsTrue(cfgDiff.DicNodes.ContainsKey(c3.Guid));
            Assert.IsTrue(cfgDiff.DicNodes.ContainsKey(c4.Guid));
            Assert.IsTrue(cfgDiff.Model.GroupEnumerations.ListEnumerations.Count() == 4);
            c1Diff = (Enumeration)cfgDiff.DicNodes[c1.Guid];
            Assert.IsFalse(c1Diff.IsNew());
            Assert.IsTrue(c1Diff.IsRenamed());
            var cc2 = (from p in cfgDiff.Model.GroupEnumerations.ListEnumerations where p.Name == "c2" select p).Single();
            Assert.IsTrue(cc2.IsDeprecated());
            c3Diff = (Enumeration)cfgDiff.DicNodes[c3.Guid];
            Assert.IsFalse(c3Diff.IsNew());
            var c4Diff = (Enumeration)cfgDiff.DicNodes[c4.Guid];
            Assert.IsTrue(c4Diff.IsNew());

            // create next stable version (first oldest version, second prev version)
            vm.CommandConfigCreateStableVersion.Execute(null);
            cfgDiff = mod.GetDiffAnnotatedConfig(vm.Config, vm.Config.PrevStableConfig, vm.Config.OldStableConfig); // Recalculate annotation
            Assert.IsTrue(cfgDiff.Model.GroupEnumerations.ListEnumerations.Count() == 4);
            c1Diff = (Enumeration)cfgDiff.DicNodes[c1.Guid];
            Assert.IsFalse(c1Diff.IsRenamed());
            cc2 = (from p in cfgDiff.Model.GroupEnumerations.ListEnumerations where p.Name == "c2" select p).Single();
            Assert.IsTrue(!cc2.IsDeprecated());
            Assert.IsTrue(cc2.IsDeleted());
            c4Diff = (Enumeration)cfgDiff.DicNodes[c4.Guid];
            Assert.IsFalse(c4Diff.IsNew());

            vm.CommandConfigCreateStableVersion.Execute(null);
            cfgDiff = mod.GetDiffAnnotatedConfig(vm.Config, vm.Config.PrevStableConfig, vm.Config.OldStableConfig); // Recalculate annotation
            Assert.IsTrue(cfgDiff.Model.GroupEnumerations.ListEnumerations.Count() == 3); // deleted is removed
        }
        [TestMethod]
        public void Main014_HasMarkedAndNewPropagation()
        {
            // initial
            var vm = new MainPageVM(false);
            vm.OnFormLoaded();
            vm.Compose();
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsFalse(vm.Config.Model.IsHasNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasNew);

            #region constant
            var cn1 = vm.Config.Model.GroupConstants.AddConstant("c1");
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasNew);

            #region new
            vm.Config.Model.GroupConstants.ListConstants.Remove(cn1);
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            cn1 = vm.Config.Model.GroupConstants.AddConstant("c1");
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            cn1.IsNew = false;
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            cn1.IsNew = true;
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            #endregion new

            #region deletion
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            cn1.IsMarkedForDeletion = true;
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasNew);

            cn1.IsMarkedForDeletion = false;
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);

            #endregion deletion
            vm.Config.Model.GroupConstants.ListConstants.Clear();
            #endregion constant

            #region enumeration
            var c1 = vm.Config.Model.GroupEnumerations.AddEnumeration("c1", EnumEnumerationType.BYTE_VALUE);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasNew);

            #region new
            vm.Config.Model.GroupEnumerations.ListEnumerations.Remove(c1);
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            c1 = vm.Config.Model.GroupEnumerations.AddEnumeration("c1", EnumEnumerationType.BYTE_VALUE);
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            c1.IsNew = false;
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            var p1 = c1.AddEnumerationPair("e1", "123");
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            p1.IsNew = false;
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            p1.IsNew = true;
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            c1.IsNew = true;
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            #endregion new

            #region deletion
            c1.IsMarkedForDeletion = true;
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasNew);

            c1.IsMarkedForDeletion = false;
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);

            p1 = c1.AddEnumerationPair("e1", "123");
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);

            p1.IsMarkedForDeletion = true;
            Assert.IsTrue(c1.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasMarkedForDeletion);

            p1.IsMarkedForDeletion = false;
            Assert.IsFalse(c1.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            #endregion deletion
            vm.Config.Model.GroupEnumerations.ListEnumerations.Clear();
            #endregion enumeration

            #region catalog
            var c2 = vm.Config.Model.GroupCatalogs.AddCatalog();
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasNew);

            #region new
            vm.Config.Model.GroupCatalogs.ListCatalogs.Remove(c2);
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            c2 = vm.Config.Model.GroupCatalogs.AddCatalog();
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            c2.IsNew = false;
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            var p2 = c2.AddProperty("p2");
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            p2.IsNew = false;
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            p2.IsNew = true;
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            var pt2 = c2.AddPropertiesTab("kuku2");
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            pt2.IsNew = false;
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            p2.IsNew = false;
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            pt2.IsNew = true;
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            var pt2p2 = pt2.AddProperty("pt2p2");
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            pt2.IsNew = false;
            pt2p2.IsNew = false;
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            pt2p2.IsNew = true;
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            c2.IsNew = true;
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            #endregion new

            #region deletion
            c2.IsMarkedForDeletion = true;
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasNew);

            c2.IsMarkedForDeletion = false;
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);

            pt2 = c2.AddPropertiesTab("kuku");
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);

            pt2.IsMarkedForDeletion = true;
            Assert.IsTrue(c2.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasMarkedForDeletion);

            pt2.IsMarkedForDeletion = false;
            Assert.IsFalse(c2.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            #endregion deletion
            vm.Config.Model.GroupCatalogs.ListCatalogs.Clear();
            #endregion catalog

            #region document
            var d1 = vm.Config.Model.GroupDocuments.AddDocument("d1");
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasNew);

            #region new
            vm.Config.Model.GroupDocuments.GroupListDocuments.Remove(d1);
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            d1 = vm.Config.Model.GroupDocuments.AddDocument("d1");
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            d1.IsNew = false;
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            var dp1 = d1.AddProperty("dp1");
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            dp1.IsNew = false;
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            //dp1.IsNew = true;
            //Assert.IsTrue(vm.Config.Model.IsHasNew);

            pt2 = d1.AddPropertiesTab("kuku2");
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            pt2.IsNew = false;
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            vm.Config.Model.RemoveMarkedForDeletionAndNewFlags();
            pt2p2 = pt2.AddProperty("pt2p2");
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            pt2p2.IsNew = false;
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            pt2p2.IsNew = true;
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            d1.IsNew = true;
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            #endregion new

            #region deletion
            d1.IsMarkedForDeletion = true;
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasNew);

            d1.IsMarkedForDeletion = false;
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);

            pt2 = d1.AddPropertiesTab("kuku");
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);

            pt2.IsMarkedForDeletion = true;
            Assert.IsTrue(d1.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasMarkedForDeletion);

            pt2.IsMarkedForDeletion = false;
            Assert.IsFalse(d1.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            #endregion deletion
            vm.Config.Model.GroupDocuments.GroupListDocuments.ListDocuments.Clear();
            #endregion document
        }
        [TestMethod]
        public void Main015_Delete_New_Enumerations()
        {
            // empty config
            this.remove_config();
            var vm = new MainPageVM(false);
            vm.OnFormLoaded();
            vm.Compose();
            vm.CommandConfigSaveAs.Execute(@".\kuku.vcfg");
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsFalse(vm.Config.Model.IsHasNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasNew);

            // create object and save
            var c1 = vm.Config.Model.GroupEnumerations.AddEnumeration("c1", EnumEnumerationType.BYTE_VALUE);
            var p1 = c1.AddEnumerationPair("e1", "123");
            var p2 = c1.AddEnumerationPair("e2", "124");
            var p3 = c1.AddEnumerationPair("e3", "125");
            var c2 = vm.Config.Model.GroupEnumerations.AddEnumeration("c2", EnumEnumerationType.INTEGER_VALUE);
            var c3 = vm.Config.Model.GroupEnumerations.AddEnumeration("c3", EnumEnumerationType.INTEGER_VALUE);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasNew);

            vm.CommandConfigSave.Execute(null);
            // expect IsHasMarkedForDeletion and IsHasNew will not changed
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasNew);

            // mark for deletion
            c3.IsMarkedForDeletion = true;
            Assert.IsTrue(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.AreEqual(3, vm.Config.Model.GroupEnumerations.ListEnumerations.Count);

            vm.CommandConfigSave.Execute(null);
            // expect IsHasMarkedForDeletion and IsHasNew will not changed
            Assert.IsTrue(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.AreEqual(3, vm.Config.Model.GroupEnumerations.ListEnumerations.Count);

            Debug.Assert(vm.Config.Model.GroupEnumerations == vm.Config.Model.GroupEnumerations[0].Parent);
            Debug.Assert(vm.Config == vm.Config.Model.Parent);
            vm.CommandConfigCurrentUpdate.Execute(null);
            Debug.Assert(vm.Config == vm.Config.Model.Parent);
            Debug.Assert(vm.Config.Model.GroupEnumerations == vm.Config.Model.GroupEnumerations[0].Parent);
            // expect new objects (IsNew) with IsMarkedForDeletion will be deleted in DB and model
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.AreEqual(2, vm.Config.Model.GroupEnumerations.ListEnumerations.Count);

            c3 = vm.Config.Model.GroupEnumerations.AddEnumeration("c3", EnumEnumerationType.INTEGER_VALUE);
            // mark for deletion
            c3.IsMarkedForDeletion = true;
            c2.IsMarkedForDeletion = true;
            Assert.IsTrue(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.AreEqual(3, vm.Config.Model.GroupEnumerations.ListEnumerations.Count);
            Debug.Assert(vm.Config.Model.GroupEnumerations == vm.Config.Model.GroupEnumerations[0].Parent);
            Debug.Assert(vm.Config == vm.Config.Model.Parent);
            vm.CommandConfigCreateStableVersion.Execute(null);
            Debug.Assert(vm.Config == vm.Config.Model.Parent);
            Debug.Assert(vm.Config.Model.GroupEnumerations == vm.Config.Model.GroupEnumerations[0].Parent);
            // expect IsHasMarkedForDeletion and IsHasNew will be false
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.Model.IsHasNew);
            Assert.AreEqual(1, vm.Config.Model.GroupEnumerations.ListEnumerations.Count);

            c3 = vm.Config.Model.GroupEnumerations.AddEnumeration("c3", EnumEnumerationType.INTEGER_VALUE);
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            vm.CommandConfigCurrentUpdate.Execute(null);
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
        }
        //[TestMethod]
        //public void Main013_Diff_WorkWithAppGeneratorSettings()
        //{
        //    var vm = new MainPageVM(false);
        //    vm.OnFormLoaded();
        //    vm.Compose();
        //    var pluginNode = (from p in vm.Config.GroupPlugins.ListPlugins where p.VPlugin is vPlugin.Sample.SamplePlugin select p).Single();
        //    var genDb = (IvPluginDbGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
        //    var genDbAccess = (IvPluginGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbAccess select p).Single().Generator;
        //    vm.CommandConfigSaveAs.Execute(@"..\..\..\..\TestApps\test1.vcfg");

        //    var sln = (AppSolution)vm.Config.GroupAppSolutions.NodeAddNewSubNode();
        //    sln.RelativeAppSolutionPath = @"..\..\..\..\TestApps\Solution.sln";

        //    var prj = (AppProject)sln.NodeAddNewSubNode();
        //    prj.RelativeAppProjectPath = @"..\..\..\..\TestApps\ConsoleApp1\ConsoleApp1.csproj";

        //    var gen = (AppProjectGenerator)prj.NodeAddNewSubNode();
        //    gen.RelativePathToGenFolder = @"..\..\..\..\TestApps\ConsoleApp1\Generated";
        //    gen.GenFileName = "test_file.cs";
        //    gen.PluginGuid = pluginNode.Guid;
        //    gen.PluginGeneratorGuid = genDbAccess.Guid;

        //    var ptst = new vPlugin.Sample.GeneratorDbAccessSettings();
        //    Assert.IsFalse(ptst.IsAccessParam2.HasValue);
        //    Assert.IsNull(ptst.AccessParam4);
        //    Assert.IsFalse(ptst.IsAccessParam1);
        //    Assert.AreEqual(string.Empty, ptst.AccessParam3);

        //    var prms = (vPlugin.Sample.GeneratorDbAccessSettings)gen.DynamicMainSettings;
        //    prms.IsAccessParam1 = true;
        //    prms.IsAccessParam2 = false;
        //    prms.AccessParam3 = "test";

        //    Assert.AreEqual(1, vm.Config.DicActiveAppProjectGenerators.Count);
        //    Assert.AreEqual(1, vm.Config.Model.GroupCommon.ListNodeGeneratorsSettings.Count);
        //    Assert.AreEqual(1, vm.Config.Model.GroupConstants.ListNodeGeneratorsSettings.Count);
        //    Assert.AreEqual(1, vm.Config.Model.GroupEnumerations.ListNodeGeneratorsSettings.Count);
        //    Assert.AreEqual(1, vm.Config.Model.GroupCatalogs.ListNodeGeneratorsSettings.Count);
        //    Assert.AreEqual(1, vm.Config.Model.GroupDocuments.ListNodeGeneratorsSettings.Count);
        //    //Assert.AreEqual(1, vm.Config.Model.GroupJournals.ListNodeGeneratorsSettings.Count);

        //    vm.CommandConfigSave.Execute(null);

        //    var vm2 = new MainPageVM(true);
        //    vm2.OnFormLoaded();
        //    vm2.Compose();
        //    Assert.AreEqual(1, vm2.Config.GroupAppSolutions.Count());
        //    Assert.AreEqual(sln.RelativeAppSolutionPath, vm2.Config.GroupAppSolutions[0].RelativeAppSolutionPath);
        //    Assert.AreEqual(1, vm2.Config.GroupAppSolutions[0].ListAppProjects.Count());
        //    Assert.AreEqual(1, vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators.Count());
        //    var gen2 = vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0];
        //    Assert.AreEqual(gen.RelativePathToGenFolder, gen2.RelativePathToGenFolder);
        //    Assert.AreEqual(gen.GenFileName, gen2.GenFileName);
        //    Assert.AreEqual(gen.PluginGuid, gen2.PluginGuid);
        //    Assert.AreEqual(gen.PluginGeneratorGuid, gen2.PluginGeneratorGuid);
        //    vm2.Config.SelectedNode = gen2;
        //    //Assert.IsNotNull(gen2.DynamicNodesSettings);
        //    var prms2 = (vPlugin.Sample.GeneratorDbAccessSettings)gen2.DynamicMainSettings;
        //    Assert.AreEqual(prms.IsAccessParam1, prms2.IsAccessParam1);
        //    Assert.AreEqual(prms.IsAccessParam2, prms2.IsAccessParam2);
        //    Assert.AreEqual(prms.AccessParam3, prms2.AccessParam3);

        //    Assert.IsFalse(true);
        //}
        //[TestMethod]
        //public void Main014_Diff_WorkWithNodeGeneratorSettings()
        //{
        //    // GeneratorDbAccessNodeCatalogFormSettings "Catalog.*.Form"
        //    // GeneratorDbAccessNodePropertySettings    "Property"

        //    // Settings workflow:
        //    // 1. When Config is loaded: init all generators settings VMs on all model nodes
        //    // 2. When model node is added: init all generators settings VMs on this node
        //    // 3. When new generator is selected: old generator has to be removed from all model nodes, 
        //    //     and new generator settings has to be added for all model nodes
        //    // 4. When saving Config: convert all model nodes generators settings to string representations
        //    _logger.LogTrace("Start test".CallerInfo());
        //    var vm = new MainPageVM(false);
        //    vm.OnFormLoaded();
        //    vm.Compose();
        //    var pluginNode = (from p in vm.Config.GroupPlugins.ListPlugins where p.VPlugin is vPlugin.Sample.SamplePlugin select p).Single();
        //    var genDb = (IvPluginDbGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
        //    var genDbAccess = (IvPluginGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbAccess select p).Single().Generator;

        //    var sln = (AppSolution)vm.Config.GroupAppSolutions.NodeAddNewSubNode();
        //    sln.RelativeAppSolutionPath = @"..\..\..\..\TestApps\Solution.sln";

        //    var prj = (AppProject)sln.NodeAddNewSubNode();
        //    prj.RelativeAppProjectPath = @"..\..\..\..\TestApps\ConsoleApp1\ConsoleApp1.csproj";
        //    //Assert.AreEqual(0, vm.Config.DicAppGenerators.Count);

        //    var gen = (AppProjectGenerator)prj.NodeAddNewSubNode();
        //    gen.RelativePathToGenFolder = @"..\..\..\..\TestApps\ConsoleApp1\Generated";
        //    gen.GenFileName = "test_file.cs";
        //    gen.PluginGuid = pluginNode.Guid;
        //    gen.PluginGeneratorGuid = genDbAccess.Guid;
        //    gen.Name = "AppGenName";
        //    gen.NameUi = "App Gen Name";

        //    // 3. When new generator is selected: old generator has to be removed from all model nodes, 
        //    //     and new generator settings has to be added for all model nodes
        //    Assert.AreEqual(1, vm.Config.DicActiveAppProjectGenerators.Count);
        //    Assert.AreEqual(1, vm.Config.Model.GroupConstants.ListNodeGeneratorsSettings.Count);
        //    Assert.AreEqual(1, vm.Config.Model.GroupCatalogs.ListNodeGeneratorsSettings.Count);
        //    gen.NodeRemove(false);
        //    Assert.AreEqual(1, vm.Config.GroupAppSolutions[0].ListAppProjects.Count);
        //    Assert.AreEqual(0, vm.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators.Count);
        //    Assert.AreEqual(0, vm.Config.DicActiveAppProjectGenerators.Count);
        //    Assert.AreEqual(0, vm.Config.Model.GroupConstants.ListNodeGeneratorsSettings.Count);
        //    Assert.AreEqual(0, vm.Config.Model.GroupCatalogs.ListNodeGeneratorsSettings.Count);
        //    gen = (AppProjectGenerator)prj.NodeAddNewSubNode();
        //    gen.RelativePathToGenFolder = @"..\..\..\..\TestApps\ConsoleApp1\Generated";
        //    gen.GenFileName = "test_file.cs";
        //    gen.PluginGuid = pluginNode.Guid;
        //    gen.PluginGeneratorGuid = genDbAccess.Guid;
        //    gen.Name = "AppGenName";
        //    gen.NameUi = "App Gen Name";
        //    Assert.AreEqual(1, vm.Config.DicActiveAppProjectGenerators.Count);
        //    Assert.AreEqual(1, vm.Config.Model.GroupConstants.ListNodeGeneratorsSettings.Count);
        //    Assert.AreEqual(1, vm.Config.Model.GroupCatalogs.ListNodeGeneratorsSettings.Count);

        //    // 2. When model node is added: init all generators settings VMs on this node
        //    Assert.AreEqual(1, vm.Config.Model.GroupEnumerations.ListNodeGeneratorsSettings.Count);
        //    vm.Config.Model.GroupEnumerations.NodeAddNewSubNode();
        //    Assert.AreEqual(1, vm.Config.Model.GroupEnumerations[0].ListNodeGeneratorsSettings.Count);
        //    vm.Config.Model.GroupConstants.NodeAddNewSubNode();
        //    Assert.AreEqual(1, vm.Config.Model.GroupConstants[0].ListNodeGeneratorsSettings.Count);
        //    vm.Config.Model.GroupCatalogs.NodeAddNewSubNode();
        //    Assert.AreEqual(1, vm.Config.Model.GroupCatalogs[0].ListNodeGeneratorsSettings.Count);
        //    vm.Config.Model.GroupCatalogs[0].GroupProperties.NodeAddNewSubNode();
        //    Assert.AreEqual(2, vm.Config.Model.GroupCatalogs[0].GroupProperties[0].ListNodeGeneratorsSettings.Count);
        //    vm.Config.Model.GroupCatalogs[0].GroupForms.NodeAddNewSubNode();
        //    Assert.AreEqual(2, vm.Config.Model.GroupCatalogs[0].GroupForms[0].ListNodeGeneratorsSettings.Count);
        //    vm.Config.Model.GroupDocuments.GroupListDocuments.NodeAddNewSubNode();
        //    Assert.AreEqual(1, vm.Config.Model.GroupDocuments.GroupListDocuments[0].ListNodeGeneratorsSettings.Count);
        //    vm.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties.NodeAddNewSubNode();
        //    Assert.AreEqual(2, vm.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties[0].ListNodeGeneratorsSettings.Count);


        //    var main = (vPlugin.Sample.GeneratorDbAccessSettings)gen.DynamicMainSettings;
        //    main.IsAccessParam1 = true;
        //    main.IsAccessParam2 = false;

        //    var ngs = (vPlugin.Sample.GeneratorDbAccessNodeSettings)gen.GetSettings(gen.Guid, vPlugin.Sample.GeneratorDbAccessNodeSettings.GuidStatic);
        //    var nds = (vPlugin.Sample.GeneratorDbAccessNodeSettings)vm.Config.Model.GetSettings(gen.Guid, vPlugin.Sample.GeneratorDbAccessNodeSettings.GuidStatic);

        //    // on model we have link to AppProjectGenerator settings
        //    Assert.AreEqual(nds.IsParam1, ngs.IsParam1);
        //    nds.IsParam1 = true;
        //    Assert.AreEqual(nds.IsParam1, ngs.IsParam1);

        //    Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].GroupProperties.IsIncluded(gen.Guid, vPlugin.Sample.GeneratorDbAccessNodeSettings.GuidStatic));
        //    nds.IsIncluded = false;
        //    Assert.IsFalse(vm.Config.Model.GroupCatalogs[0].GroupProperties.IsIncluded(gen.Guid, vPlugin.Sample.GeneratorDbAccessNodeSettings.GuidStatic));

        //    //foreach (var t in genDbAccess.GetListNodeGenerationSettings())
        //    //{
        //    //    if (t.SearchPathInModel == "Property")
        //    //        Assert.AreEqual(t.Guid, stt.NodeSettingsVmGuid);
        //    //}
        //    //Assert.AreEqual(gen.Name, stt.Name);
        //    //Assert.AreEqual(gen.NameUi, stt.NameUi);

        //    // 4. When saving Config: convert all model nodes generators settings to string representations
        //    //Assert.AreEqual("", vm.Config.Model.GroupConstants.ListGeneratorsSettings[0].Settings);
        //    vm.CommandConfigSave.Execute(null);
        //    //Assert.AreNotEqual("", vm.Config.Model.GroupConstants.ListGeneratorsSettings[0].Settings);
        //    Assert.AreEqual(1, vm.Config.DicActiveAppProjectGenerators.Count);

        //    // 1. When Config is loaded: init all generators settings VMs on all model nodes
        //    var vm2 = new MainPageVM(true);
        //    vm2.OnFormLoaded();
        //    vm2.Compose();

        //    Assert.AreEqual(1, vm2.Config.DicActiveAppProjectGenerators.Count);
        //    Assert.AreEqual(1, vm2.Config.Model.GroupEnumerations[0].ListNodeGeneratorsSettings.Count);
        //    Assert.AreEqual(1, vm2.Config.Model.GroupConstants[0].ListNodeGeneratorsSettings.Count);
        //    Assert.AreEqual(1, vm2.Config.Model.GroupCatalogs[0].ListNodeGeneratorsSettings.Count);
        //    Assert.AreEqual(2, vm2.Config.Model.GroupCatalogs[0].GroupProperties[0].ListNodeGeneratorsSettings.Count);
        //    Assert.AreEqual(2, vm2.Config.Model.GroupCatalogs[0].GroupForms[0].ListNodeGeneratorsSettings.Count);
        //    Assert.AreEqual(1, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].ListNodeGeneratorsSettings.Count);
        //    Assert.AreEqual(2, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties[0].ListNodeGeneratorsSettings.Count);

        //    main = (vPlugin.Sample.GeneratorDbAccessSettings)(vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0].DynamicMainSettings);
        //    Assert.AreEqual(true, main.IsAccessParam1);
        //    Assert.AreEqual(false, main.IsAccessParam2);
        //    nds = (vPlugin.Sample.GeneratorDbAccessNodeSettings)vm.Config.Model.GetSettings(gen.Guid, vPlugin.Sample.GeneratorDbAccessNodeSettings.GuidStatic);
        //    Assert.AreEqual(true, nds.IsParam1);

        //    Assert.IsFalse(vm.Config.Model.GroupCatalogs[0].GroupProperties.IsIncluded(gen.Guid, nds.Guid));

        //    //// if new app progect generator is added, new setting are attached to all appropriate nodes
        //    //var gen0 = vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0];
        //    //Assert.AreEqual(1, vm2.Config.DicActiveAppProjectGenerators.Count);
        //    //vm2.Config.GroupAppSolutions[0].ListAppProjects[0].NodeAddNewSubNode();
        //    //var gen2 = (from p in vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators where p.Guid != gen0.Guid select p).Single();
        //    //gen2.RelativePathToGenFolder = @"..\..\..\..\TestApps\ConsoleApp1\Generated";
        //    //gen2.GenFileName = "test_file.cs";
        //    //gen2.PluginGuid = pluginNode.Guid;
        //    //// Expect attached settings for Property and Catalog.Form
        //    //gen2.PluginGeneratorGuid = genDbAccess.Guid;
        //    //Assert.AreEqual(2, vm2.Config.DicActiveAppProjectGenerators.Count);
        //    //Assert.AreEqual(2, vm2.Config.Model.GroupEnumerations[0].ListNodeGeneratorsSettings.Count);
        //    //Assert.AreEqual(2, vm2.Config.Model.GroupConstants[0].ListNodeGeneratorsSettings.Count);
        //    //Assert.AreEqual(2, vm2.Config.Model.GroupCatalogs[0].ListNodeGeneratorsSettings.Count);
        //    //Assert.AreEqual(2, vm2.Config.Model.GroupCatalogs[0].GroupProperties[0].ListNodeGeneratorsSettings.Count);
        //    //Assert.AreEqual(2, vm2.Config.Model.GroupCatalogs[0].GroupForms[0].ListNodeGeneratorsSettings.Count);
        //    //Assert.AreEqual(2, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].ListNodeGeneratorsSettings.Count);
        //    //Assert.AreEqual(2, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties[0].ListNodeGeneratorsSettings.Count);

        //    //// if app progect generator is removed, attached seetings are removed from appropriate nodes as well
        //    //gen2.NodeRemove(false);
        //    //Assert.AreEqual(1, vm2.Config.DicActiveAppProjectGenerators.Count);
        //    //Assert.AreEqual(1, vm2.Config.Model.GroupEnumerations[0].ListNodeGeneratorsSettings.Count);
        //    //Assert.AreEqual(1, vm2.Config.Model.GroupConstants[0].ListNodeGeneratorsSettings.Count);
        //    //Assert.AreEqual(1, vm2.Config.Model.GroupCatalogs[0].ListNodeGeneratorsSettings.Count);
        //    //Assert.AreEqual(1, vm2.Config.Model.GroupCatalogs[0].GroupProperties[0].ListNodeGeneratorsSettings.Count);
        //    //Assert.AreEqual(1, vm2.Config.Model.GroupCatalogs[0].GroupForms[0].ListNodeGeneratorsSettings.Count);
        //    //Assert.AreEqual(1, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].ListNodeGeneratorsSettings.Count);
        //    //Assert.AreEqual(1, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties[0].ListNodeGeneratorsSettings.Count);
        //    _logger.LogTrace("End test".CallerInfo());
        //    Assert.IsFalse(true);
        //}
        //[TestMethod]
        //public void Main015_Diff_WorkWithPluginsGroupSettings()
        //{
        //    // Settings workflow:
        //    // 1. When Config is loaded: init group plugin settings on all solution nodes
        //    // 2. When generator is removed, appropriate solution settings has to be removed if it is a last plugin in group
        //    // 3. When new generator is added and it is new group plugin, than appropriate solution settings has to be added in solution
        //    // 4. When saving Config: convert all solutions groups settings to string representations
        //    _logger.LogTrace("Start test".CallerInfo());
        //    var vm = new MainPageVM(false);
        //    vm.OnFormLoaded();
        //    vm.Compose();
        //    var pluginNode = (from p in vm.Config.GroupPlugins.ListPlugins where p.VPlugin is vPlugin.Sample.SamplePlugin select p).Single();
        //    var genDb = (IvPluginDbGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
        //    var genDbAccess = (IvPluginGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbAccess select p).Single().Generator;

        //    var sln = (AppSolution)vm.Config.GroupAppSolutions.NodeAddNewSubNode();
        //    sln.RelativeAppSolutionPath = @"..\..\..\..\TestApps\Solution.sln";
        //    Assert.IsNull(sln.DynamicMainSettings);

        //    var prj = (AppProject)sln.NodeAddNewSubNode();
        //    prj.RelativeAppProjectPath = @"..\..\..\..\TestApps\ConsoleApp1\ConsoleApp1.csproj";

        //    var gen = (AppProjectGenerator)prj.NodeAddNewSubNode();
        //    gen.RelativePathToGenFolder = @"..\..\..\..\TestApps\ConsoleApp1\Generated";
        //    gen.GenFileName = "test_file.cs";
        //    gen.PluginGuid = pluginNode.Guid;
        //    gen.PluginGeneratorGuid = genDbAccess.Guid;
        //    gen.Name = "AppGenName";
        //    gen.NameUi = "App Gen Name";

        //    Assert.IsTrue(vm.Config.DicPlugins.ContainsKey(pluginNode.Guid));
        //    var plgn = vm.Config.DicPlugins[pluginNode.Guid];
        //    Assert.IsNotNull(plgn);
        //    Assert.IsTrue(sln.DicPluginsGroupSettings.ContainsKey(plgn.PluginGroupSolutionSettings.Guid));
        //    Assert.IsNotNull(sln.DynamicMainSettings);

        //    var set = (vPlugin.Sample.PluginsGroupSettings)sln.DicPluginsGroupSettings[plgn.PluginGroupSolutionSettings.Guid];
        //    set.IsGroupParam1 = true;

        //    vm.CommandConfigSave.Execute(null);

        //    // 2. When generator is removed, appropriate solution settings has to be removed if it is a last plugin in group
        //    gen.PluginGuid = null;
        //    Assert.IsTrue(vm.Config.DicPlugins.ContainsKey(pluginNode.Guid));
        //    Assert.IsFalse(sln.DicPluginsGroupSettings.ContainsKey(plgn.PluginGroupSolutionSettings.Guid));
        //    Assert.IsNull(sln.DynamicMainSettings);

        //    // 3. When new generator is added and it is new group plugin, than appropriate solution settings has to be added in solution
        //    gen.PluginGuid = pluginNode.Guid;
        //    Assert.IsTrue(vm.Config.DicPlugins.ContainsKey(pluginNode.Guid));
        //    plgn = vm.Config.DicPlugins[pluginNode.Guid];
        //    Assert.IsNotNull(plgn);
        //    Assert.IsTrue(sln.DicPluginsGroupSettings.ContainsKey(plgn.PluginGroupSolutionSettings.Guid));
        //    Assert.IsNotNull(sln.DynamicMainSettings);

        //    // 2. When generator is removed, appropriate solution settings has to be removed if it is a last plugin in group
        //    gen.NodeRemove(false);
        //    Assert.IsTrue(vm.Config.DicPlugins.ContainsKey(pluginNode.Guid));
        //    Assert.IsFalse(sln.DicPluginsGroupSettings.ContainsKey(plgn.PluginGroupSolutionSettings.Guid));
        //    Assert.IsNull(sln.DynamicMainSettings);


        //    // 1. When Config is loaded: init group plugin settings on all solution nodes
        //    var vm2 = new MainPageVM(true);
        //    vm2.OnFormLoaded();
        //    vm2.Compose();

        //    Assert.IsTrue(vm2.Config.DicPlugins.ContainsKey(pluginNode.Guid));
        //    plgn = vm2.Config.DicPlugins[pluginNode.Guid];
        //    Assert.IsNotNull(plgn);
        //    sln = vm2.Config.GroupAppSolutions[0];
        //    Assert.IsTrue(sln.DicPluginsGroupSettings.ContainsKey(plgn.PluginGroupSolutionSettings.Guid));
        //    Assert.IsNotNull(sln.DynamicMainSettings);

        //    // 4. When saving Config: convert all solutions groups settings to string representations
        //    set = (vPlugin.Sample.PluginsGroupSettings)sln.DicPluginsGroupSettings[plgn.PluginGroupSolutionSettings.Guid];
        //    Assert.IsTrue(set.IsGroupParam1);
        //    _logger.LogTrace("End test".CallerInfo());
        //    Assert.IsFalse(true);
        //}

        [TestMethod]
        public void Main081_BaseConfigLoading()
        {
            // empty config
            this.remove_config();
            var vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose();
            Assert.IsTrue(vm.Config.GroupConfigLinks.Count() == 0);

            // base config
            var vmb = new MainPageVM(false);
            vmb.OnFormLoaded();
            vm.Compose();
            vmb.Config.Name = "ext";
            var c2 = vmb.Config.Model.GroupConstants.AddConstant("c2");
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}
            vmb.CommandConfigSaveAs.Execute(pathExt + "kuku.vcfg");

            vm.CommandConfigSaveAs.Execute(pathExt + MainPageVM.DEFAULT_CFG_FILE_NAME);

            // create object and save
            var bcfg = vm.Config.GroupConfigLinks.AddBaseConfig("base", pathExt + "kuku.vcfg");
            var c1 = vm.Config.Model.GroupConstants.AddConstant("c1");
            vm.CommandConfigSave.Execute(null);

            vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose();
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
            vm.OnFormLoaded();
            vm.Compose();
            vm.Config.Name = "main";
            var c3 = vm.Config.Model.GroupConstants.AddConstant("c3");
            Assert.IsTrue(vm.Config.GroupConfigLinks.Count() == 0);

            // base config
            var vmb = new MainPageVM(false);
            vmb.OnFormLoaded();
            vm.Compose();
            vmb.Config.Name = "ext";
            var c2 = vmb.Config.Model.GroupConstants.AddConstant("c2");
            if (!Directory.Exists(pathExt))
            {
                Directory.CreateDirectory(pathExt);
            }
            vmb.CommandConfigSaveAs.Execute(pathExt + "kuku.vcfg");

            vm.CommandConfigSaveAs.Execute(pathExt + MainPageVM.DEFAULT_CFG_FILE_NAME);

            // create object and save
            var bcfg = vm.Config.GroupConfigLinks.AddBaseConfig("base", pathExt + "kuku.vcfg");
            vm.Config.GroupConfigLinks.AddBaseConfig(bcfg);
            var c1 = vm.Config.Model.GroupConstants.AddConstant("c1");
            vm.CommandConfigSave.Execute(null);

            vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose();
            //TODO diff test implementation
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
