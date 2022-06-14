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
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.IsTrue(vm.pconfig_history == null);
        }

        [TestMethod]
        public void Main002CanSaveConfigAndCreateVersions()
        {
            // empty config
            this.remove_config();
            var vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.IsTrue(vm.pconfig_history == null);

            // create object and save
            var gr = vm.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            gr.NodeAddNewSubNode();
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
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.IsTrue(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants.Count == 1);
            Assert.IsTrue(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].Name == cnst.Name);
            Assert.IsTrue(ct <= vm.Config.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Config.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.IsTrue(vm.Config.Version == 0);
            Assert.IsTrue(vm.pconfig_history != null);
            Assert.IsTrue(vm.pconfig_history.CurrentConfig != null);
            Assert.IsTrue(vm.pconfig_history.PrevStableConfig == null);

            vm.CommandConfigCurrentUpdate.Execute(null);
            // create stable version
            vm.CommandConfigCreateStableVersion.Execute(null);
            vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.IsTrue(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants.Count == 1);
            Assert.IsTrue(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].Name == cnst.Name);
            Assert.IsTrue(ct <= vm.Config.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Config.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.IsTrue(vm.Config.Version == 1);
            Assert.IsTrue(vm.pconfig_history != null);
            Assert.IsTrue(vm.pconfig_history.CurrentConfig != null);
            Assert.IsTrue(vm.pconfig_history.PrevStableConfig != null);
            Assert.IsTrue(vm.pconfig_history.PrevStableConfig.Version == 0);
            // migration code is created?
            // Assert.IsTrue(false);

            // create next stable version
            vm.CommandConfigCreateStableVersion.Execute(null);
            vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.IsTrue(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants.Count == 1);
            Assert.IsTrue(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].Name == cnst.Name);
            Assert.IsTrue(ct <= vm.Config.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Config.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.IsTrue(vm.Config.Version == 2);
            Assert.IsTrue(vm.pconfig_history != null);
            Assert.IsTrue(vm.pconfig_history.CurrentConfig != null);
            Assert.IsTrue(vm.pconfig_history.PrevStableConfig != null);
            Assert.IsTrue(vm.pconfig_history.PrevStableConfig.Version == 1);
            // old migration code is kept?
            // Assert.IsTrue(false);
        }
        [TestMethod]
        public void Main003CanSaveConfigInSelectedSolutionFolderAndReloadFromThisFolderByDefault()
        {
            this.remove_config();
            var vm = new MainPageVM(false);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.AreEqual(0, vm.UserSettings.ListOpenConfigHistory.Count);
            vm.Config.Name = "test1";
            vm.CommandConfigSaveAs.Execute(@"..\..\..\TestApps\config.vcfg");

            vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            // Load from previous save
            Assert.AreEqual(1, vm.UserSettings.ListOpenConfigHistory.Count);
            Assert.AreEqual("test1", vm.Config.Name);
            vm.Config.Name = "test2";
            vm.CommandConfigSaveAs.Execute(@"..\..\..\TestApps\config2.vcfg");

            vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
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
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.AreEqual(0, vm.UserSettings.ListOpenConfigHistory.Count);
            vm.Config.Name = "test1";
            var gr = vm.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            var c1 = gr.AddConstant("c1");
            vm.CommandConfigSaveAs.Execute(@"..\..\..\TestApps\config.vcfg");

            vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            gr = vm.Config.Model.GroupConstantGroups.ListConstantGroups[0];
            // Load from previous save
            Assert.AreEqual(1, vm.UserSettings.ListOpenConfigHistory.Count);
            Assert.AreEqual("test1", vm.Config.Name);
            Assert.AreEqual(1, gr.ListConstants.Count());
            Assert.AreEqual("c1", gr.ListConstants[0].Name);
            Assert.IsTrue(vm.Config.DicNodes.ContainsKey(c1.Guid));
        }
        [TestMethod]
        public void Main005IsChangedAndIsTreeChanged()
        {
            this.remove_config();
            var vm = new MainPageVM(false);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            vm.Config.Name = "test1";
            Assert.AreEqual(0, vm.UserSettings.ListOpenConfigHistory.Count);
            Assert.IsTrue(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.IsChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.IsHasChanged);

            vm.CommandConfigSaveAs.Execute(@"..\..\..\TestApps\config.vcfg");
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.IsChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.IsHasChanged);

            var gr = vm.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            gr.NodeAddNewSubNode();
            Assert.IsTrue(gr.ListConstants[0].IsChanged);
            Assert.IsFalse(gr.ListConstants[0].IsHasChanged);
            Assert.IsTrue(gr.IsChanged);
            Assert.IsTrue(gr.IsHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.IsChanged);
            Assert.IsTrue(vm.Config.Model.GroupConstantGroups.IsHasChanged);
            Assert.IsTrue(vm.Config.Model.IsHasChanged);
            Assert.IsTrue(vm.Config.Model.IsChanged);
            Assert.IsTrue(vm.Config.IsHasChanged);
            Assert.IsFalse(vm.Config.IsChanged);

            vm.CommandConfigSave.Execute(null);
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.IsChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.IsHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].IsChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].IsHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].IsChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].IsHasChanged);

            vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.IsChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.IsHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].IsChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].IsHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].IsChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].IsHasChanged);

            vm.Config.Model.PKeyName = "kuku";
            Assert.IsTrue(vm.Config.Model.IsChanged);
            Assert.IsFalse(vm.Config.Model.IsHasChanged);
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsTrue(vm.Config.IsHasChanged);

            //TODO add test propogation of IsChanged for different setting
        }
        [TestMethod]
        public void Main006CatalogSpecialFields()
        {
            var vm = new MainPageVM(false);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            vm.Config.Name = "test1";
            var gr = vm.Config.Model.GroupCatalogs;

            // Simple catalog
            var c = gr.AddCatalog("test");
            var lst = c.GetAllProperties(true);
            Assert.AreEqual(3, lst.Count);
            Assert.AreEqual(vm.Config.Model.PKeyName, lst[0].Name);
            Assert.AreEqual(vm.Config.Model.RecordVersionFieldName, lst[1].Name);
            Assert.AreEqual(vm.Config.Model.GroupCatalogs.PropertyNameName, lst[2].Name);

            // Tree catalog
            c.UseTree = true;
            lst = c.GetAllProperties(true);
            Assert.AreEqual(5, lst.Count);
            Assert.AreEqual(vm.Config.Model.PKeyName, lst[0].Name);
            Assert.AreEqual("RefTreeParent", lst[1].Name);
            Assert.AreEqual(vm.Config.Model.GroupCatalogs.PropertyIsOpenName, lst[2].Name);
            Assert.AreEqual(vm.Config.Model.RecordVersionFieldName, lst[3].Name);
            Assert.AreEqual(vm.Config.Model.GroupCatalogs.PropertyNameName, lst[4].Name);

            // Tree catalog
            c.UseTree = true;
            c.UseFolderTypeExplicitly = true;
            lst = c.GetAllProperties(true);
            Assert.AreEqual(6, lst.Count);
            Assert.AreEqual(vm.Config.Model.PKeyName, lst[0].Name);
            Assert.AreEqual("RefTreeParent", lst[1].Name);
            Assert.AreEqual(vm.Config.Model.GroupCatalogs.PropertyIsOpenName, lst[2].Name);
            Assert.AreEqual(vm.Config.Model.GroupCatalogs.PropertyIsFolderName, lst[3].Name);
            Assert.AreEqual(vm.Config.Model.RecordVersionFieldName, lst[4].Name);
            Assert.AreEqual(vm.Config.Model.GroupCatalogs.PropertyNameName, lst[5].Name);

            // Tree catalog with separate properties for tree
            c.UseTree = true;
            c.UseFolderTypeExplicitly = true;
            c.UseSeparateTreeForFolders = true;
            lst = c.GetAllProperties(true);
            Assert.AreEqual(4, lst.Count);
            Assert.AreEqual(vm.Config.Model.PKeyName, lst[0].Name);
            Assert.AreEqual("RefCtlgtestFolder", lst[1].Name);
            Assert.AreEqual(vm.Config.Model.RecordVersionFieldName, lst[2].Name);
            Assert.AreEqual(vm.Config.Model.GroupCatalogs.PropertyNameName, lst[3].Name);
            lst = c.GetAllFolderProperties(true);
            Assert.AreEqual(6, lst.Count);
            Assert.AreEqual(vm.Config.Model.PKeyName, lst[0].Name);
            Assert.AreEqual("RefTreeParent", lst[1].Name);
            Assert.AreEqual(vm.Config.Model.GroupCatalogs.PropertyIsOpenName, lst[2].Name);
            Assert.AreEqual(vm.Config.Model.GroupCatalogs.PropertyIsFolderName, lst[3].Name);
            Assert.AreEqual(vm.Config.Model.RecordVersionFieldName, lst[4].Name);
            Assert.AreEqual(vm.Config.Model.GroupCatalogs.PropertyNameName, lst[5].Name);
        }
        [TestMethod]
        public void Main009_Diff()
        {
            // new config, not saved yet
            this.remove_config();
            var vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            vm.Config.Name = "main";
            var gr = vm.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            var c1 = gr.AddConstant("c1");
            var c2 = gr.AddConstant("c2");
            var c3 = gr.AddConstant("c3");

            var cfgDiff = vm.Config;

            var c1Diff = (from p in gr.ListConstants where p.Name == "c1" select p).Single();
            Assert.AreEqual(c1Diff, cfgDiff.DicNodes[c1.Guid]);
        }
        //[TestMethod]
        //public void Main010_DiffList()
        //{
        //    // new config, not saved yet
        //    this.remove_config();
        //    var vm = new MainPageVM(true);
        //    vm.OnFormLoaded();
        //    vm.Compose();
        //    vm.Config.Name = "main";
        //    var c1 = vm.Config.Model.GroupConstants.AddConstant("c1");
        //    var c2 = vm.Config.Model.GroupConstants.AddConstant("c2");
        //    var c3 = vm.Config.Model.GroupConstants.AddConstant("c3");

        //    var cfgDiff = vm.Config;
        //    var c1Diff = (Constant)cfgDiff.DicNodes[c1.Guid];
        //    Assert.IsTrue(c1Diff.IsNew());
        //    Assert.IsFalse(c1Diff.IsDeleted());
        //    Assert.IsFalse(c1Diff.IsDeprecated());
        //    Assert.IsFalse(c1Diff.IsRenamed());
        //    // current changes are saved
        //    // no stable version (prev is null)
        //    vm.CommandConfigSaveAs.Execute(@".\kuku.vcfg");
        //    vm = new MainPageVM(true);
        //    vm.OnFormLoaded();
        //    vm.Compose();
        //    c1 = (Constant)(vm.Config.DicNodes[c1.Guid]);
        //    c2 = (Constant)(vm.Config.DicNodes[c2.Guid]);
        //    cfgDiff = vm.Config;
        //    c1Diff = (Constant)cfgDiff.DicNodes[c1.Guid];
        //    Assert.IsTrue(c1Diff.IsNew());
        //    Assert.IsFalse(c1Diff.IsDeleted());
        //    Assert.IsFalse(c1Diff.IsDeprecated());
        //    Assert.IsFalse(c1Diff.IsRenamed());

        //    vm.CommandConfigCurrentUpdate.Execute(null);

        //    // first stable version (prev not null, but oldest is null)
        //    vm.CommandConfigCreateStableVersion.Execute(null);
        //    c1Diff = (Constant)cfgDiff.DicNodes[c1.Guid];
        //    Assert.IsFalse(c1Diff.IsNew());
        //    Assert.IsFalse(c1Diff.IsDeleted());
        //    Assert.IsFalse(c1Diff.IsDeprecated());
        //    Assert.IsFalse(c1Diff.IsRenamed());

        //    ((Constant)vm.Config.DicNodes[c1.Guid]).Name = "c11";
        //    vm.Config.Model.GroupConstants.ListConstants.Remove((Constant)vm.Config.DicNodes[c2.Guid]);

        //    Assert.AreEqual(2, vm.Config.Model.GroupConstants.Count());
        //    Assert.AreEqual(3, cfgDiff.Model.GroupConstants.Count());
        //    c1Diff = (Constant)cfgDiff.DicNodes[c1.Guid];
        //    Assert.IsFalse(c1Diff.IsNew());
        //    Assert.IsFalse(c1Diff.IsDeleted());
        //    Assert.IsFalse(c1Diff.IsDeprecated());
        //    Assert.IsTrue(c1Diff.IsRenamed());

        //    var c2Diff = (Constant)cfgDiff.DicNodes[c2.Guid];
        //    Assert.IsFalse(c2Diff.IsNew());
        //    Assert.IsFalse(c2Diff.IsDeleted());
        //    Assert.IsTrue(c2Diff.IsDeprecated());
        //    Assert.IsFalse(c2Diff.IsRenamed());

        //    vm.CommandConfigSave.Execute(null);
        //    vm = new MainPageVM(true);
        //    vm.OnFormLoaded();
        //    vm.Compose();
        //    c1 = (Constant)(vm.Config.DicNodes[c1.Guid]);
        //    c2 = (Constant)(vm.Config.DicNodes[c2.Guid]);
        //    c1Diff = (Constant)cfgDiff.DicNodes[c1.Guid];
        //    Assert.IsFalse(c1Diff.IsNew());
        //    Assert.IsFalse(c1Diff.IsDeleted());
        //    Assert.IsFalse(c1Diff.IsDeprecated());
        //    Assert.IsTrue(c1Diff.IsRenamed());
        //}
        [TestMethod]
        public void Main011_Diff_Constants()
        {
            // empty config
            this.remove_config();
            var vm = new MainPageVM(true);
            var cfg = vm.Config;
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            var gr = vm.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            Assert.IsFalse(gr.IsHasNew);
            Assert.IsFalse(gr.IsHasMarkedForDeletion);

            // create object and save
            var c1 = gr.AddConstant("c1");
            Assert.IsTrue(c1.IsNew);
            Assert.IsTrue(gr.IsHasNew);
            Assert.IsFalse(gr.IsHasMarkedForDeletion);

            vm.CommandConfigSaveAs.Execute(@".\kuku.vcfg");
            Assert.AreEqual(1, gr.ListConstants.Count());
            Assert.IsTrue(c1.IsNew);
            Assert.IsTrue(c1.IsNewNode());
            Assert.IsTrue(gr.IsHasNew);
            Assert.IsFalse(gr.IsHasMarkedForDeletion);

            // c1-new -> new
            vm.CommandConfigCurrentUpdate.Execute(null);
            Assert.AreEqual(1, gr.ListConstants.Count());
            Assert.IsTrue(c1.IsNew);
            Assert.IsTrue(gr.IsHasNew);
            Assert.IsFalse(gr.IsHasMarkedForDeletion);

            // c1-new -> not new, not del  
            vm.CommandConfigCreateStableVersion.Execute(null);
            // prev c1 not new, not del
            Assert.AreEqual(1, gr.ListConstants.Count());
            Assert.IsFalse(c1.IsNew);
            Assert.IsFalse(gr.IsHasNew);
            Assert.IsFalse(gr.IsHasMarkedForDeletion);

            Assert.IsFalse(c1.IsDeprecated());
            c1.IsMarkedForDeletion = true; // deprecate
            Assert.IsTrue(c1.IsDeprecated());
            var c2 = gr.AddConstant("c2");
            c2.IsMarkedForDeletion = true; // delete
            var c3 = gr.AddConstant("c3");
            c3.DataType.Length = 101;
            Assert.IsTrue(c1.IsMarkedForDeletion);
            Assert.IsTrue(c1.IsDeprecated());
            Assert.AreEqual(3, gr.ListConstants.Count());
            Assert.IsTrue(gr.IsHasNew);
            Assert.IsTrue(gr.IsHasMarkedForDeletion);

            // c1- not new, del -> not new, del 
            // c2- new, del -> removed
            // c3- new -> new
            vm.CommandConfigCurrentUpdate.Execute(null);
            // prev c1 not new, not del
            Assert.AreEqual(2, gr.ListConstants.Count());
            Assert.IsTrue(c1.IsMarkedForDeletion);
            Assert.IsFalse(c1.IsNew);
            Assert.IsTrue(c1.IsDeprecated());
            Assert.IsFalse(c3.IsMarkedForDeletion);
            Assert.IsTrue(c3.IsNew);
            Assert.IsTrue(gr.IsHasNew);
            Assert.IsTrue(gr.IsHasMarkedForDeletion);

            // c1- not new, del -> not new, del => previous not new, del
            // c3- new -> not new
            vm.CommandConfigCreateStableVersion.Execute(null);
            // prev c1 not new, del
            Assert.AreEqual(2, gr.ListConstants.Count());
            Assert.IsFalse(c1.IsNew);
            Assert.IsTrue(c1.IsMarkedForDeletion);
            Assert.AreEqual(c3, gr.ListConstants[1]);
            Assert.IsFalse(c3.IsMarkedForDeletion);
            Assert.IsFalse(c3.IsNew);
            Assert.IsFalse(gr.IsHasNew);
            Assert.IsTrue(gr.IsHasMarkedForDeletion);
            Assert.IsTrue(c1.IsDeleted());

            // c1- not new, del -> removed
            // c3- new -> new
            vm.CommandConfigCreateStableVersion.Execute(null);
            Assert.AreEqual(1, gr.ListConstants.Count());
            Assert.AreEqual(c3, gr.ListConstants[0]);
            Assert.IsFalse(c3.IsMarkedForDeletion);
            Assert.IsFalse(c3.IsNew);
            Assert.IsFalse(gr.IsHasNew);
            Assert.IsFalse(gr.IsHasMarkedForDeletion);
        }
        [TestMethod]
        public void Main012_Diff_Enumerations()
        {
            // empty config
            this.remove_config();
            var vm = new MainPageVM(true);
            var cfg = vm.Config;
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.IsFalse(cfg.Model.GroupEnumerations.IsHasNew);
            Assert.IsFalse(cfg.Model.GroupEnumerations.IsHasMarkedForDeletion);

            // create object and save
            var c1 = cfg.Model.GroupEnumerations.AddEnumeration("c1", EnumEnumerationType.BYTE_VALUE);
            Assert.IsTrue(c1.IsNew);
            Assert.IsTrue(cfg.Model.GroupEnumerations.IsHasNew);
            Assert.IsFalse(cfg.Model.GroupEnumerations.IsHasMarkedForDeletion);

            vm.CommandConfigSaveAs.Execute(@".\kuku.vcfg");
            Assert.AreEqual(1, cfg.Model.GroupEnumerations.ListEnumerations.Count());
            Assert.IsTrue(c1.IsNew);
            Assert.IsTrue(c1.IsNewNode());
            Assert.IsTrue(cfg.Model.GroupEnumerations.IsHasNew);
            Assert.IsFalse(cfg.Model.GroupEnumerations.IsHasMarkedForDeletion);

            // c1-new -> new
            vm.CommandConfigCurrentUpdate.Execute(null);
            Assert.AreEqual(1, cfg.Model.GroupEnumerations.ListEnumerations.Count());
            Assert.IsTrue(c1.IsNew);
            Assert.IsTrue(cfg.Model.GroupEnumerations.IsHasNew);
            Assert.IsFalse(cfg.Model.GroupEnumerations.IsHasMarkedForDeletion);

            // c1-new -> not new, not del  
            vm.CommandConfigCreateStableVersion.Execute(null);
            // prev c1 not new, not del
            Assert.AreEqual(1, cfg.Model.GroupEnumerations.ListEnumerations.Count());
            Assert.IsFalse(c1.IsNew);
            Assert.IsFalse(cfg.Model.GroupEnumerations.IsHasNew);
            Assert.IsFalse(cfg.Model.GroupEnumerations.IsHasMarkedForDeletion);

            Assert.IsFalse(c1.IsDeprecated());
            c1.IsMarkedForDeletion = true; // deprecate
            Assert.IsTrue(c1.IsDeprecated());
            var c2 = cfg.Model.GroupEnumerations.AddEnumeration("c2", EnumEnumerationType.BYTE_VALUE);
            c2.IsMarkedForDeletion = true; // delete
            var c3 = cfg.Model.GroupEnumerations.AddEnumeration("c3", EnumEnumerationType.BYTE_VALUE);
            Assert.IsTrue(c1.IsMarkedForDeletion);
            Assert.IsTrue(c1.IsDeprecated());
            Assert.AreEqual(3, cfg.Model.GroupEnumerations.ListEnumerations.Count());
            Assert.IsTrue(cfg.Model.GroupEnumerations.IsHasNew);
            Assert.IsTrue(cfg.Model.GroupEnumerations.IsHasMarkedForDeletion);

            // c1- not new, del -> not new, del 
            // c2- new, del -> removed
            // c3- new -> new
            vm.CommandConfigCurrentUpdate.Execute(null);
            // prev c1 not new, not del
            Assert.AreEqual(2, cfg.Model.GroupEnumerations.ListEnumerations.Count());
            Assert.IsTrue(c1.IsMarkedForDeletion);
            Assert.IsFalse(c1.IsNew);
            Assert.IsTrue(c1.IsDeprecated());
            Assert.IsFalse(c3.IsMarkedForDeletion);
            Assert.IsTrue(c3.IsNew);
            Assert.IsTrue(cfg.Model.GroupEnumerations.IsHasNew);
            Assert.IsTrue(cfg.Model.GroupEnumerations.IsHasMarkedForDeletion);

            // c1- not new, del -> not new, del => previous not new, del
            // c3- new -> not new
            vm.CommandConfigCreateStableVersion.Execute(null);
            // prev c1 not new, del
            Assert.AreEqual(2, cfg.Model.GroupEnumerations.ListEnumerations.Count());
            Assert.IsFalse(c1.IsNew);
            Assert.IsTrue(c1.IsMarkedForDeletion);
            Assert.AreEqual(c3, cfg.Model.GroupEnumerations.ListEnumerations[1]);
            Assert.IsFalse(c3.IsMarkedForDeletion);
            Assert.IsFalse(c3.IsNew);
            Assert.IsFalse(cfg.Model.GroupEnumerations.IsHasNew);
            Assert.IsTrue(cfg.Model.GroupEnumerations.IsHasMarkedForDeletion);
            Assert.IsTrue(c1.IsDeleted());

            // c1- not new, del -> removed
            // c3- new -> new
            vm.CommandConfigCreateStableVersion.Execute(null);
            Assert.AreEqual(1, cfg.Model.GroupEnumerations.ListEnumerations.Count());
            Assert.AreEqual(c3, cfg.Model.GroupEnumerations.ListEnumerations[0]);
            Assert.IsFalse(c3.IsMarkedForDeletion);
            Assert.IsFalse(c3.IsNew);
            Assert.IsFalse(cfg.Model.GroupEnumerations.IsHasNew);
            Assert.IsFalse(cfg.Model.GroupEnumerations.IsHasMarkedForDeletion);
        }
        [TestMethod]
        public void Main014_HasMarkedAndNewPropagation()
        {
            // initial
            var vm = new MainPageVM(false);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsFalse(vm.Config.Model.IsHasNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasNew);

            #region constant
            var gr = vm.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            var cn1 = gr.AddConstant("c1");
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasNew);

            #region new
            gr.ListConstants.Remove(cn1);
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            cn1 = gr.AddConstant("c1");
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
            gr.ListConstants.Clear();
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

            var vis = new ModelVisitorBase();
            vis.Run(vm.Config, (v, n) =>
            {
                if (n is IEditableNode)
                {
                    var p = n as IEditableNode;
                    p.IsNew = false;
                }
            });
            Assert.IsFalse(vm.Config.Model.IsHasNew);

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
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
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
            p1.IsDefault = true;
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

            vm.CommandConfigSave.Execute(null);
            vm.CommandConfigCurrentUpdate.Execute(null);

            vm.CommandConfigCreateStableVersion.Execute(null);
            Debug.Assert(vm.Config == vm.Config.Model.Parent);
            Debug.Assert(vm.Config.Model.GroupEnumerations == vm.Config.Model.GroupEnumerations[0].Parent);
            // expect IsHasMarkedForDeletion and IsHasNew will be false
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
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
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.IsTrue(vm.Config.GroupConfigLinks.Count() == 0);
            var gr = vm.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            vm.CommandConfigSaveAs.Execute(pathExt + MainPageVM.DEFAULT_CFG_FILEName);


            // base config
            var vmb = new MainPageVM(false);
            vmb.OnFormLoaded();
            vmb.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            vmb.Config.Name = "ext";
            var grb = vmb.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            var c2 = grb.AddConstant("c2");
            vmb.CommandConfigSaveAs.Execute(pathExt + "kuku.vcfg");

            // create object and save
            var bcfg = vm.Config.GroupConfigLinks.AddBaseConfig("base", pathExt + "kuku.vcfg");
            var c1 = gr.AddConstant("c1");
            vm.CommandConfigSave.Execute(null);

            vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            gr = vm.Config.Model.GroupConstantGroups.ListConstantGroups[0];
            Assert.AreEqual(1, gr.ListConstants.Count);
            Assert.IsTrue(gr.Count() == 1);
            Assert.IsTrue(gr.ListConstants[0].Name == "c1");
            Assert.IsTrue(vm.Config.GroupConfigLinks.Count() == 1);
            Assert.IsTrue(vm.Config.GroupConfigLinks[0].ConfigBase.Model.GroupConstantGroups.ListConstantGroups.Count() == 1);
            Assert.IsTrue(vm.Config.GroupConfigLinks[0].ConfigBase.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants.Count() == 1);
            Assert.IsTrue(vm.Config.GroupConfigLinks[0].ConfigBase.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].Name == "c2");
            Assert.IsTrue(vm.Config.GroupConfigLinks[0].ConfigBase.Name == "ext");
            Assert.IsTrue(vm.Config.GroupConfigLinks[0].Name == "ext");
        }

        [TestMethod]
        public void Main082_BaseConfigDiff()
        {
            // empty config
            this.remove_config();
            var vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            vm.Config.Name = "main";
            var gr = vm.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            var c3 = gr.AddConstant("c3");
            Assert.IsTrue(vm.Config.GroupConfigLinks.Count() == 0);

            // base config
            var vmb = new MainPageVM(false);
            vmb.OnFormLoaded();
            vmb.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            vmb.Config.Name = "ext";
            var grb = vm.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            var c2 = grb.AddConstant("c2");
            if (!Directory.Exists(pathExt))
            {
                Directory.CreateDirectory(pathExt);
            }
            vmb.CommandConfigSaveAs.Execute(pathExt + "kuku.vcfg");

            vm.CommandConfigSaveAs.Execute(pathExt + MainPageVM.DEFAULT_CFG_FILEName);

            // create object and save
            var bcfg = vm.Config.GroupConfigLinks.AddBaseConfig("base", pathExt + "kuku.vcfg");
            vm.Config.GroupConfigLinks.AddBaseConfig(bcfg);
            var c1 = vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].AddConstant("c1");
            vm.CommandConfigSave.Execute(null);

            vm = new MainPageVM(true);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
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
        [TestMethod]
        public void Main091_ValidationRequirements()
        {
            var vm = new MainPageVM(false);
            var m = vm.Config.Model;
            var gc = m.GroupCatalogs;
            var c = gc.AddCatalog("Simple");

            var p = c.GroupProperties.AddPropertyChar("char_notnullable");
            #region char
            p.RangeValuesRequirementStr = "'c'";
            var v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(0, v.ListBoundaries.Count);
            Assert.AreEqual(1, v.ListValues.Count);
            Assert.AreEqual("'c'", v.ListValues[0]);

            p.RangeValuesRequirementStr = " 'c'";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(0, v.ListBoundaries.Count);
            Assert.AreEqual(1, v.ListValues.Count);
            Assert.AreEqual("'c'", v.ListValues[0]);

            p.RangeValuesRequirementStr = "'c' ";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(0, v.ListBoundaries.Count);
            Assert.AreEqual(1, v.ListValues.Count);
            Assert.AreEqual("'c'", v.ListValues[0]);

            p.RangeValuesRequirementStr = "'c';'b'";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(0, v.ListBoundaries.Count);
            Assert.AreEqual(2, v.ListValues.Count);
            Assert.AreEqual("'c'", v.ListValues[0]);
            Assert.AreEqual("'b'", v.ListValues[1]);

            p.RangeValuesRequirementStr = " 'c' ; 'b' ";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(0, v.ListBoundaries.Count);
            Assert.AreEqual(2, v.ListValues.Count);
            Assert.AreEqual("'c'", v.ListValues[0]);
            Assert.AreEqual("'b'", v.ListValues[1]);

            p.RangeValuesRequirementStr = "#'c'";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(1, v.ListBoundaries.Count);
            Assert.AreEqual(0, v.ListValues.Count);
            Assert.AreEqual(null, v.ListBoundaries[0].BoundaryMin);
            Assert.AreEqual("'c'", v.ListBoundaries[0].BoundaryMax);

            p.RangeValuesRequirementStr = "'c'#";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(1, v.ListBoundaries.Count);
            Assert.AreEqual(0, v.ListValues.Count);
            Assert.AreEqual("'c'", v.ListBoundaries[0].BoundaryMin);
            Assert.AreEqual(null, v.ListBoundaries[0].BoundaryMax);

            p.RangeValuesRequirementStr = "'a'#'c'";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(1, v.ListBoundaries.Count);
            Assert.AreEqual(0, v.ListValues.Count);
            Assert.AreEqual("'a'", v.ListBoundaries[0].BoundaryMin);
            Assert.AreEqual("'c'", v.ListBoundaries[0].BoundaryMax);

            p.RangeValuesRequirementStr = "#'0';'a'#'c';'b';'d'#";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(3, v.ListBoundaries.Count);
            Assert.AreEqual(1, v.ListValues.Count);
            Assert.AreEqual("'b'", v.ListValues[0]);



            p.RangeValuesRequirementStr = "''";
            v = p.RangeValuesRequirements;
            Assert.IsTrue(v.IsHasErrors);
            Assert.IsTrue(v.ListErrors[0].Contains("Can't"));
            Assert.IsTrue(v.ListErrors[0].Contains("''"));

            p.RangeValuesRequirementStr = "\"c\"";
            v = p.RangeValuesRequirements;
            Assert.IsTrue(v.IsHasErrors);
            Assert.AreEqual(1, v.ListErrors.Count);
            Assert.IsTrue(v.ListErrors[0].Contains("Can't"));
            Assert.IsTrue(v.ListErrors[0].Contains("\"c\""));

            p.RangeValuesRequirementStr = "  ";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);

            p.RangeValuesRequirementStr = "";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);

            p.RangeValuesRequirementStr = null;
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            #endregion char

            p = c.GroupProperties.AddPropertyString("str_unlimited", 0);
            #region string requirements validation
            p.RangeValuesRequirementStr = "\"\"";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(0, v.ListBoundaries.Count);
            Assert.AreEqual(1, v.ListValues.Count);
            Assert.AreEqual("\"\"", v.ListValues[0]);

            p.RangeValuesRequirementStr = "\"c\"";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(0, v.ListBoundaries.Count);
            Assert.AreEqual(1, v.ListValues.Count);
            Assert.AreEqual("\"c\"", v.ListValues[0]);

            p.RangeValuesRequirementStr = " \"c\"";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(0, v.ListBoundaries.Count);
            Assert.AreEqual(1, v.ListValues.Count);
            Assert.AreEqual("\"c\"", v.ListValues[0]);

            p.RangeValuesRequirementStr = "\"c\" ";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(0, v.ListBoundaries.Count);
            Assert.AreEqual(1, v.ListValues.Count);
            Assert.AreEqual("\"c\"", v.ListValues[0]);

            p.RangeValuesRequirementStr = "\"c\";\"b\"";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(0, v.ListBoundaries.Count);
            Assert.AreEqual(2, v.ListValues.Count);
            Assert.AreEqual("\"c\"", v.ListValues[0]);
            Assert.AreEqual("\"b\"", v.ListValues[1]);

            p.RangeValuesRequirementStr = " \"c\" ; \"b\" ";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(0, v.ListBoundaries.Count);
            Assert.AreEqual(2, v.ListValues.Count);
            Assert.AreEqual("\"c\"", v.ListValues[0]);
            Assert.AreEqual("\"b\"", v.ListValues[1]);



            p.RangeValuesRequirementStr = "'c'";
            v = p.RangeValuesRequirements;
            Assert.IsTrue(v.IsHasErrors);
            Assert.AreEqual(1, v.ListErrors.Count);
            Assert.IsTrue(v.ListErrors[0].Contains("Can't"));
            Assert.IsTrue(v.ListErrors[0].Contains("'c'"));

            p.RangeValuesRequirementStr = "#\"c\"";
            v = p.RangeValuesRequirements;
            Assert.IsTrue(v.IsHasErrors);
            Assert.AreEqual(1, v.ListErrors.Count);
            Assert.IsTrue(v.ListErrors[0].Contains("Can't"));
            Assert.IsTrue(v.ListErrors[0].Contains("#\"c\""));

            p.RangeValuesRequirementStr = "\"c\"#";
            v = p.RangeValuesRequirements;
            Assert.IsTrue(v.IsHasErrors);
            Assert.AreEqual(1, v.ListErrors.Count);
            Assert.IsTrue(v.ListErrors[0].Contains("Can't"));
            Assert.IsTrue(v.ListErrors[0].Contains("\"c\"#"));

            p.RangeValuesRequirementStr = "\"a\"#\"c\"";
            v = p.RangeValuesRequirements;
            Assert.IsTrue(v.IsHasErrors);
            Assert.AreEqual(1, v.ListErrors.Count);
            Assert.IsTrue(v.ListErrors[0].Contains("Can't"));
            Assert.IsTrue(v.ListErrors[0].Contains("\"a\"#\"c\""));

            p.RangeValuesRequirementStr = "";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            #endregion string requirements validation

            #region date
            #endregion date

            p = c.GroupProperties.AddPropertyNumerical("intmax", 9, 0);
            #region int requirements validation
            p.RangeValuesRequirementStr = "1";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(0, v.ListBoundaries.Count);
            Assert.AreEqual(1, v.ListValues.Count);
            Assert.AreEqual("1", v.ListValues[0]);

            p.RangeValuesRequirementStr = "#2";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(1, v.ListBoundaries.Count);
            Assert.AreEqual(0, v.ListValues.Count);
            Assert.AreEqual(null, v.ListBoundaries[0].BoundaryMin);
            Assert.AreEqual("2", v.ListBoundaries[0].BoundaryMax);

            p.RangeValuesRequirementStr = "2#";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(1, v.ListBoundaries.Count);
            Assert.AreEqual(0, v.ListValues.Count);
            Assert.AreEqual("2", v.ListBoundaries[0].BoundaryMin);
            Assert.AreEqual(null, v.ListBoundaries[0].BoundaryMax);

            p.RangeValuesRequirementStr = "3#7";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(1, v.ListBoundaries.Count);
            Assert.AreEqual(0, v.ListValues.Count);
            Assert.AreEqual("3", v.ListBoundaries[0].BoundaryMin);
            Assert.AreEqual("7", v.ListBoundaries[0].BoundaryMax);

            p.RangeValuesRequirementStr = "-7#-3";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(1, v.ListBoundaries.Count);
            Assert.AreEqual(0, v.ListValues.Count);
            Assert.AreEqual("-7", v.ListBoundaries[0].BoundaryMin);
            Assert.AreEqual("-3", v.ListBoundaries[0].BoundaryMax);

            p.RangeValuesRequirementStr = "#2;5#7;9;11#";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.AreEqual(3, v.ListBoundaries.Count);
            Assert.AreEqual(1, v.ListValues.Count);

            p.RangeValuesRequirementStr = "5#2";
            v = p.RangeValuesRequirements;
            Assert.IsTrue(v.IsHasErrors);
            Assert.AreEqual(1, v.ListErrors.Count);
            Assert.IsTrue(v.ListErrors[0].Contains("greater"));
            Assert.IsTrue(v.ListErrors[0].Contains("5#2"));


            p.RangeValuesRequirementStr = "bbb";
            v = p.RangeValuesRequirements;
            Assert.IsTrue(v.IsHasErrors);
            Assert.AreEqual(1, v.ListErrors.Count);
            Assert.IsTrue(v.ListErrors[0].Contains("Can't"));
            Assert.IsTrue(v.ListErrors[0].Contains("bbb"));

            p.RangeValuesRequirementStr = "";
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            #endregion int requirements validation
        }
    }
}
