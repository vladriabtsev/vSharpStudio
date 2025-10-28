using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLogging;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModelBase;
using vPlugin.Sample;
using vSharpStudio.common;
using vSharpStudio.ViewModels;
using vSharpStudio.vm.ViewModels;
using static System.Net.Mime.MediaTypeNames;

//TODO test work with json to load config (now config loading is not changing current)
//TODO with encoding string, for proto we need UTF8

namespace vSharpStudio.Unit
{
    [TestClass]
    public class MainVmTests
    {
        private readonly ILogger _logger;
        string pathExt = @".\extcfg\";

        static MainVmTests()
        {
        }
        public MainVmTests()
        {
            //AppLogger.IsFullFilePath = true;
            AppLogger.LogLevel = LogLevel.Trace;
            AppLogger.UseDebug = true;
            _logger = AppLogger.CreateLogger(nameof(MainVmTests));
            VmBindable.isUnitTests = true;
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

        private void CheckNewConfig(MainPageVM vm, bool isWithPlugins, bool isWasSaved = false)
        {
            if (!isWasSaved)
                Assert.IsNull(vm.pconfig_history);
            Assert.IsTrue(vm.Config.IsNew);
            if (isWithPlugins)
            {
                Assert.IsTrue(vm.Config.IsHasChanged);
            }
            else
            {
                Assert.IsFalse(vm.Config.IsHasChanged);
            }

            Assert.IsFalse(vm.Config.GroupAppSolutions.IsNew);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsChanged);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsChangedOrHasChanged);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasChanged);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsChanged);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsChangedOrHasChanged);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasChanged);
            foreach (var plugin in vm.Config.GroupPlugins.ListPlugins)
            {
                Assert.IsTrue(plugin.IsNew);
                Assert.IsFalse(plugin.IsHasNew);
                Assert.IsTrue(plugin.IsChanged);
                Assert.IsFalse(plugin.IsHasChanged);
            }
            Assert.IsFalse(vm.Config.GroupPlugins.IsNew);
            Assert.IsFalse(vm.Config.GroupPlugins.IsChanged);
            if (isWithPlugins)
            {
                Assert.IsTrue(vm.Config.GroupPlugins.IsChangedOrHasChanged);
                Assert.IsTrue(vm.Config.GroupPlugins.IsHasChanged);
            }
            else
            {
                Assert.IsFalse(vm.Config.GroupPlugins.IsChangedOrHasChanged);
                Assert.IsFalse(vm.Config.GroupPlugins.IsHasChanged);
            }

            Assert.IsFalse(vm.Config.Model.IsNew);
            Assert.IsFalse(vm.Config.Model.IsChanged);
            Assert.IsFalse(vm.Config.Model.IsChangedOrHasChanged);
            Assert.IsFalse(vm.Config.Model.IsHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupCatalogs.IsNew);
            Assert.IsFalse(vm.Config.Model.GroupCatalogs.IsChanged);
            Assert.IsFalse(vm.Config.Model.GroupCatalogs.IsChangedOrHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupCatalogs.IsHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupCommon.IsNew);
            Assert.IsFalse(vm.Config.Model.GroupCommon.IsChanged);
            Assert.IsFalse(vm.Config.Model.GroupCommon.IsChangedOrHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupCommon.IsHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.IsNew);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.IsChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.IsChangedOrHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.IsHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupDocuments.IsNew);
            Assert.IsFalse(vm.Config.Model.GroupDocuments.IsChanged);
            Assert.IsFalse(vm.Config.Model.GroupDocuments.IsChangedOrHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupDocuments.IsHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupEnumerations.IsNew);
            Assert.IsFalse(vm.Config.Model.GroupEnumerations.IsChanged);
            Assert.IsFalse(vm.Config.Model.GroupEnumerations.IsChangedOrHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupEnumerations.IsHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupRelations.IsNew);
            Assert.IsFalse(vm.Config.Model.GroupRelations.IsChanged);
            Assert.IsFalse(vm.Config.Model.GroupRelations.IsChangedOrHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupRelations.IsHasChanged);

            Assert.IsFalse(vm.BtnAddClone.CanExecute());
            Assert.IsFalse(vm.BtnAddNew.CanExecute());
            //Assert.IsFalse(vm.BtnAddNewChild.CanExecute());
            Assert.IsFalse(vm.BtnConfigCreateStableVersionAsync.CanExecute());
            Assert.IsFalse(vm.BtnConfigCurrentUpdateAsync.CanExecute());
            Assert.IsFalse(vm.BtnConfigSave.CanExecute());
            Assert.IsTrue(vm.BtnConfigSaveAs.CanExecute(null));
            Assert.IsFalse(vm.BtnDelete.CanExecute());
            Assert.IsTrue(vm.CommandFromErrorToSelection.CanExecute(null));
            Assert.IsFalse(vm.BtnMoveDown.CanExecute());
            Assert.IsFalse(vm.BtnMoveUp.CanExecute());
            Assert.IsFalse(vm.BtnSelectionDown.CanExecute());
            Assert.IsFalse(vm.BtnSelectionLeft.CanExecute());
            Assert.IsFalse(vm.BtnSelectionRight.CanExecute());
            Assert.IsFalse(vm.BtnSelectionUp.CanExecute());
            Assert.IsFalse(vm.BtnNewConfig.CanExecute());
            Assert.IsTrue(vm.BtnOpenConfig.CanExecute(null));
        }
        [TestMethod]
        public async Task Main001_IsNew_IsChanged_IsHasChanged()
        {
            this.remove_config();
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            CheckNewConfig(vm, false);
            vm.BtnNewConfig.Execute(); // not saved yet
            CheckNewConfig(vm, true);

            vm.BtnConfigSaveAs.Execute(@"..\..\..\TestApps\config.vcfg");
            Assert.IsFalse(vm.Config.IsNew);
            Assert.IsTrue(vm.Config.IsHasNew); // all plugins are new for new configuration
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);
            Assert.IsTrue(vm.Config.GroupPlugins.IsHasNew);
            foreach (var plugin in vm.Config.GroupPlugins.ListPlugins)
            {
                Assert.IsTrue(plugin.IsNew); // stil new because stable version is not created yet
                Assert.IsFalse(plugin.IsHasNew);
                Assert.IsFalse(plugin.IsChanged);
                Assert.IsFalse(plugin.IsHasChanged);
            }

            // Update current config
            await vm.BtnConfigCurrentUpdateAsync.ExecuteAsync();
            Assert.IsTrue(vm.Config.IsHasNew);
            Assert.IsTrue(vm.Config.GroupPlugins.IsHasNew);
            foreach (var plugin in vm.Config.GroupPlugins.ListPlugins)
            {
                Assert.IsTrue(plugin.IsNew); // stil new because stable version is not created yet
            }

            // Update current config
            await vm.BtnConfigCreateStableVersionAsync.ExecuteAsync();
            Assert.IsFalse(vm.Config.IsHasNew);
            Assert.IsFalse(vm.Config.GroupPlugins.IsHasNew);
            foreach (var plugin in vm.Config.GroupPlugins.ListPlugins)
            {
                Assert.IsFalse(plugin.IsNew); // stil new because stable version is not created yet
            }

            // Modifying Config
            vm.Config.Name = "test1";
            Assert.IsFalse(vm.Config.IsNew);
            Assert.IsFalse(vm.Config.IsHasNew);
            Assert.IsTrue(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);
            vm.BtnConfigSave.Execute();
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);

            // Modifying Config.GroupAppSolutions
            vm.Config.GroupAppSolutions.Description = "test1";
            Assert.IsTrue(vm.Config.GroupAppSolutions.IsChanged);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasChanged);
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsTrue(vm.Config.IsHasChanged);
            vm.BtnConfigSave.Execute();
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsChanged);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasChanged);
            Assert.IsFalse(vm.Config.IsHasNew);
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);
            Assert.IsEmpty(vm.Config.GroupAppSolutions.ListAppSolutions);
            vm.Config.GroupAppSolutions.NodeAddNewSubNode();
            Assert.IsTrue(vm.Config.IsHasNew);
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsTrue(vm.Config.IsHasChanged);
            Assert.IsTrue(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsChanged);
            Assert.IsTrue(vm.Config.GroupAppSolutions.IsHasChanged);
            Assert.HasCount(1, vm.Config.GroupAppSolutions.ListAppSolutions);
            Assert.IsTrue(vm.Config.GroupAppSolutions[0].IsNew);
            Assert.IsTrue(vm.Config.GroupAppSolutions[0].IsChanged);

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

            vm.BtnConfigSave.Execute();
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.IsChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.IsHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].IsChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].IsHasChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].IsChanged);
            Assert.IsFalse(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].IsHasChanged);

            vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
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
            Assert.IsTrue(vm.BtnConfigSave.CanExecute());
            Assert.IsTrue(vm.BtnConfigSaveAs.CanExecute(null));

            vm.BtnConfigSave.Execute();
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsChanged);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasChanged);
            Assert.IsTrue(vm.BtnConfigSave.CanExecute());
            Assert.IsTrue(vm.BtnConfigSaveAs.CanExecute(null));

            var sln = (AppSolution)vm.Config.GroupAppSolutions.NodeAddNewSubNode();
            Assert.IsTrue(vm.Config.IsHasChanged);
            vm.BtnConfigSave.Execute();
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);

            sln.RelativeAppSolutionPath = @"..\..\..\..\TestApps\OldProject\Solution.sln";
            Assert.IsTrue(vm.Config.IsHasChanged);
            Assert.IsTrue(vm.BtnConfigSave.CanExecute());
            Assert.IsTrue(vm.BtnConfigSaveAs.CanExecute(null));
            vm.BtnConfigSave.Execute();
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);

            var prj = (AppProject)sln.NodeAddNewSubNode();
            Assert.IsTrue(vm.Config.IsHasChanged);
            vm.BtnConfigSave.Execute();
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);

            prj.RelativeAppProjectPath = @"..\..\..\..\TestApps\OldProject\ConsoleApp1\ConsoleApp1.csproj";
            Assert.IsTrue(vm.Config.IsHasChanged);
            Assert.IsTrue(vm.BtnConfigSave.CanExecute());
            Assert.IsTrue(vm.BtnConfigSaveAs.CanExecute(null));
            vm.BtnConfigSave.Execute();
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);

            var gen = (AppProjectGenerator)prj.NodeAddNewSubNode();
            Assert.IsTrue(vm.Config.IsHasChanged);
            vm.BtnConfigSave.Execute();
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);

            gen.RelativePathToGenFolder = @"..\..\..\..\TestApps\OldProject\ConsoleApp1\Generated";
            Assert.IsTrue(vm.Config.IsHasChanged);
            Assert.IsTrue(vm.BtnConfigSave.CanExecute());
            Assert.IsTrue(vm.BtnConfigSaveAs.CanExecute(null));
            vm.BtnConfigSave.Execute();
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);

            var pluginNode = (from p in vm.Config.GroupPlugins.ListPlugins where p.VPlugin is vPlugin.Sample.SamplePlugin select p).Single();
            var genDb = (IvPluginDbGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
            var genDbAccess = (IvPluginGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbAccess select p).Single().Generator;
            gen.GenFileName = "test_file.cs";
            gen.PluginGuid = pluginNode.Guid;
            gen.PluginGeneratorGuid = genDbAccess.Guid;
            Assert.IsTrue(vm.Config.IsHasChanged);
            Assert.IsTrue(vm.BtnConfigSave.CanExecute());
            Assert.IsTrue(vm.BtnConfigSaveAs.CanExecute(null));
            vm.BtnConfigSave.Execute();
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);

            //AppSolution plugin settings
            var slnSet = (PluginsGroupSolutionSettings)sln.DicPluginsGroupSettings[PluginsGroupSolutionSettings.GuidStatic];
            slnSet.IsGroupParam1 = !slnSet.IsGroupParam1;
            Assert.IsTrue(vm.Config.IsHasChanged);
            Assert.IsTrue(vm.BtnConfigSave.CanExecute());
            Assert.IsTrue(vm.BtnConfigSaveAs.CanExecute(null));
            vm.BtnConfigSave.Execute();
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);

            //AppProject plugin settings
            var prjSet = (PluginsGroupProjectSettings)prj.DicPluginsGroupSettings[PluginsGroupProjectSettings.GuidStatic];
            prjSet.IsGroupProjectParam1 = !prjSet.IsGroupProjectParam1;
            Assert.IsTrue(vm.Config.IsHasChanged);
            Assert.IsTrue(vm.BtnConfigSave.CanExecute());
            Assert.IsTrue(vm.BtnConfigSaveAs.CanExecute(null));
            vm.BtnConfigSave.Execute();
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);

            //AppProjectGenerator plugin settings
            var genSet = (GeneratorDbAccessSettings)gen.DynamicGeneratorSettings;
            prjSet.IsGroupProjectParam1 = !prjSet.IsGroupProjectParam1;
            Assert.IsTrue(vm.Config.IsHasChanged);
            Assert.IsTrue(vm.BtnConfigSave.CanExecute());
            Assert.IsTrue(vm.BtnConfigSaveAs.CanExecute(null));
            vm.BtnConfigSave.Execute();
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);

            //AppProjectGenerator plugin node settings
            var genModelSet = (GeneratorDbAccessNodeSettings)vm.Config.Model.GroupConstantGroups.DicGenNodeSettings[gen.Guid];
            genModelSet.IsCatalogFormParam1 = !genModelSet.IsCatalogFormParam1;
            Assert.IsTrue(vm.Config.IsHasChanged);
            Assert.IsTrue(vm.BtnConfigSave.CanExecute());
            Assert.IsTrue(vm.BtnConfigSaveAs.CanExecute(null));
            vm.BtnConfigSave.Execute();
            Assert.IsFalse(vm.Config.IsChanged);
            Assert.IsFalse(vm.Config.IsHasChanged);

            vm.BtnNewConfig.Execute();
            CheckNewConfig(vm, true, true);
        }
        [TestMethod]
        public async Task Main002CanSaveConfigAndCreateVersions()
        {
            // empty config
            this.remove_config();
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.IsTrue(vm.Config.IsNew);
            Assert.IsNull(vm.pconfig_history);

            vm.BtnNewConfig.Execute();
            Assert.IsNotNull(vm.Config);
            Assert.IsTrue(vm.Config.IsNeedCurrentUpdate);

            // create object and save
            var gr = vm.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            gr.NodeAddNewSubNode();
            var cnst = (Constant)vm.Config.SelectedNode;
            var ct = DateTime.UtcNow;
            Assert.IsTrue(vm.Config.IsNeedCurrentUpdate);
            vm.BtnConfigSaveAs.Execute(@".\kuku.vcfg");
            Assert.IsTrue(vm.Config.LastUpdated != null);
            Assert.IsTrue(ct <= vm.Config.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Config.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.AreEqual(0, vm.Config.Version);

            Assert.IsTrue(vm.Config.IsNeedCurrentUpdate);
            gr.NodeAddNewSubNode();
            Assert.IsTrue(vm.Config.IsNeedCurrentUpdate);

            //Assert.IsTrue(vm.BtnAddClone.CanExecute());
            //Assert.IsTrue(vm.BtnAddNew.CanExecute());
            //Assert.IsFalse(vm.BtnAddNewChild.CanExecute());
            //Assert.IsTrue(vm.BtnConfigCreateStableVersionAsync.CanExecute());
            //Assert.IsTrue(vm.BtnConfigCurrentUpdateAsync.CanExecute(null));
            //Assert.IsTrue(vm.BtnConfigSave.CanExecute());
            //Assert.IsTrue(vm.BtnConfigSaveAs.CanExecute(null));
            //Assert.IsTrue(vm.BtnDelete.CanExecute());
            //Assert.IsTrue(vm.CommandFromErrorToSelection.CanExecute(null));
            //Assert.IsFalse(vm.BtnMoveDown.CanExecute());
            //Assert.IsFalse(vm.BtnMoveUp.CanExecute());
            //Assert.IsTrue(vm.BtnNewConfig.IsEnabled);
            //Assert.IsTrue(vm.BtnOpenConfig.CanExecute(null));
            //Assert.IsFalse(vm.BtnSelectionDown.CanExecute());
            //Assert.IsTrue(vm.BtnSelectionLeft.CanExecute());
            //Assert.IsFalse(vm.BtnSelectionRight.CanExecute());
            //Assert.IsFalse(vm.BtnSelectionUp.CanExecute());

            // reload
            vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.HasCount(1, vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants);
            Assert.AreEqual(cnst.Name, vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].Name);
            Assert.IsTrue(ct <= vm.Config.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Config.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.AreEqual(0, vm.Config.Version);
            Assert.IsNotNull(vm.pconfig_history);
            Assert.IsNotNull(vm.pconfig_history.CurrentConfig);
            Assert.IsNull(vm.pconfig_history.PrevStableConfig);
            Assert.IsTrue(vm.Config.IsNeedCurrentUpdate);

            vm.Config.Model.GroupConstantGroups.NodeAddNewSubNode();
            Assert.IsTrue(vm.Config.IsNeedCurrentUpdate);

            await vm.BtnConfigCurrentUpdateAsync.ExecuteAsync();
            Assert.IsFalse(vm.Config.IsNeedCurrentUpdate);
            // create stable version
            await vm.BtnConfigCreateStableVersionAsync.ExecuteAsync();
            Assert.IsFalse(vm.Config.IsNeedCurrentUpdate);
            vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.IsFalse(vm.Config.IsNeedCurrentUpdate);
            Assert.HasCount(1, vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants);
            Assert.AreEqual(cnst.Name, vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].Name);
            Assert.IsTrue(ct <= vm.Config.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Config.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.AreEqual(1, vm.Config.Version);
            Assert.IsNotNull(vm.pconfig_history);
            Assert.IsNotNull(vm.pconfig_history.CurrentConfig);
            Assert.IsNotNull(vm.pconfig_history.PrevStableConfig);
            Assert.AreEqual(0, vm.pconfig_history.PrevStableConfig.Version);
            // migration code is created?
            // Assert.IsTrue(false);

            // create next stable version
            await vm.BtnConfigCreateStableVersionAsync.ExecuteAsync();
            Assert.IsFalse(vm.Config.IsNeedCurrentUpdate);
            vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.HasCount(1, vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants);
            Assert.AreEqual(cnst.Name, vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].Name);
            Assert.IsTrue(ct <= vm.Config.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Config.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.AreEqual(2, vm.Config.Version);
            Assert.IsNotNull(vm.pconfig_history);
            Assert.IsNotNull(vm.pconfig_history.CurrentConfig);
            Assert.IsNotNull(vm.pconfig_history.PrevStableConfig);
            Assert.AreEqual(1, vm.pconfig_history.PrevStableConfig.Version);
            // old migration code is kept?
            // Assert.IsTrue(false);
        }
        [TestMethod]
        public void Main003CanSaveConfigInSelectedSolutionFolderAndReloadFromThisFolderByDefault()
        {
            this.remove_config();
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.IsEmpty(vm.UserSettings.ListOpenConfigHistory);

            vm.BtnNewConfig.Execute();
            Assert.IsEmpty(vm.UserSettings.ListOpenConfigHistory);

            vm.Config.Name = "test1";
            vm.BtnConfigSaveAs.Execute(@"..\..\..\TestApps\config.vcfg");
            Assert.HasCount(1, vm.UserSettings.ListOpenConfigHistory);

            vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            // Load from previous save
            Assert.HasCount(1, vm.UserSettings.ListOpenConfigHistory);
            Assert.AreEqual("test1", vm.Config.Name);
            vm.Config.Name = "test2";
            vm.BtnConfigSaveAs.Execute(@"..\..\..\TestApps\config2.vcfg");

            vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            // Load from previous save
            Assert.HasCount(2, vm.UserSettings.ListOpenConfigHistory);
            Assert.AreEqual("test2", vm.Config.Name);
        }
        [TestMethod]
        public void Main004CanSaveConfigAndReload()
        {
            this.remove_config();
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            vm.BtnNewConfig.Execute();
            vm.Config.Name = "test1";
            var gr = vm.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            var c1 = gr.AddConstant("c1");
            vm.BtnConfigSaveAs.Execute(@"..\..\..\TestApps\config.vcfg");

            vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            gr = vm.Config.Model.GroupConstantGroups.ListConstantGroups[0];
            // Load from previous save
            Assert.HasCount(1, vm.UserSettings.ListOpenConfigHistory);
            Assert.AreEqual("test1", vm.Config.Name);
            Assert.HasCount(1, gr.ListConstants);
            Assert.AreEqual("c1", gr.ListConstants[0].Name);
            Assert.IsTrue(vm.Config.DicNodes.ContainsKey(c1.Guid));
        }
        [TestMethod]
        public void Main006CatalogSpecialFields()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            vm.BtnNewConfig.Execute();
            vm.Config.Name = "test1";
            var gr = vm.Config.Model.GroupCatalogs;

            // Simple catalog
            var c = gr.AddCatalog("test");
            var lst = c.GetAllProperties(true);
            Assert.HasCount(4, lst);
            Assert.AreEqual(vm.Config.Model.PKeyName, lst[0].Name);
            Assert.AreEqual(vm.Config.Model.RecordVersionFieldName, lst[1].Name);
            Assert.AreEqual(vm.Config.Model.GroupCatalogs.PropertyCodeName, lst[2].Name);
            Assert.AreEqual(vm.Config.Model.GroupCatalogs.PropertyNameName, lst[3].Name);

            //// Tree catalog
            //c.UseTree = true;
            //lst = c.GetAllProperties(true);
            //Assert.AreEqual(6, lst.Count);
            //Assert.AreEqual(vm.Config.Model.PKeyName, lst[0].Name);
            //Assert.AreEqual("RefTreeParent", lst[1].Name);
            //Assert.AreEqual(vm.Config.Model.RecordVersionFieldName, lst[3].Name);
            //Assert.AreEqual(vm.Config.Model.PropertyCodeName, lst[4].Name);
            //Assert.AreEqual(vm.Config.Model.PropertyNameName, lst[5].Name);

            // Tree catalog
            c.UseTree = true;
            lst = c.GetAllProperties(true);
            Assert.HasCount(6, lst);
            Assert.AreEqual(vm.Config.Model.PKeyName, lst[0].Name);
            Assert.AreEqual("RefTreeParent", lst[1].Name);
            Assert.AreEqual(vm.Config.Model.GroupCatalogs.PropertyIsFolderName, lst[2].Name);
            Assert.AreEqual(vm.Config.Model.RecordVersionFieldName, lst[3].Name);
            Assert.AreEqual(vm.Config.Model.GroupCatalogs.PropertyCodeName, lst[4].Name);
            Assert.AreEqual(vm.Config.Model.GroupCatalogs.PropertyNameName, lst[5].Name);

            // Tree catalog with separate properties for tree
            c.UseTree = true;
            c.UseSeparateTreeForFolders = true;
            lst = c.GetAllProperties(true);
            Assert.HasCount(5, lst);
            Assert.AreEqual(vm.Config.Model.PKeyName, lst[0].Name);
            Assert.AreEqual("RefParent", lst[1].Name);
            Assert.AreEqual(vm.Config.Model.RecordVersionFieldName, lst[2].Name);
            Assert.AreEqual(vm.Config.Model.GroupCatalogs.PropertyCodeName, lst[3].Name);
            Assert.AreEqual(vm.Config.Model.GroupCatalogs.PropertyNameName, lst[4].Name);
            lst = c.GetAllFolderProperties(true);
            Assert.HasCount(5, lst);
            Assert.AreEqual(vm.Config.Model.PKeyName, lst[0].Name);
            Assert.AreEqual("RefTreeParent", lst[1].Name);
            Assert.AreEqual(vm.Config.Model.RecordVersionFieldName, lst[2].Name);
            Assert.AreEqual(vm.Config.Model.GroupCatalogs.PropertyCodeName, lst[3].Name);
            Assert.AreEqual(vm.Config.Model.GroupCatalogs.PropertyNameName, lst[4].Name);
        }
        [TestMethod]
        [Ignore("Not implemented yet")]
        public void Main008Backups()
        {
            //// empty config
            //this.remove_config();
            //var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            //Assert.IsTrue(vm.Config.IsNew);
            //Assert.IsTrue(vm.pconfig_history == null);

            //vm.BtnNewConfig.Execute(@".\kuku.vcfg");
            //Assert.IsTrue(vm.Config != null);
            //Assert.IsFalse(vm.Config.IsNeedCurrentUpdate);

            //// create object and save
            //var gr = vm.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            //gr.NodeAddNewSubNode();
            //var cnst = (Constant)vm.Config.SelectedNode;
            //var ct = DateTime.UtcNow;
            //Assert.IsTrue(vm.Config.IsNeedCurrentUpdate);
            //vm.BtnConfigSaveAs.Execute(@".\kuku.vcfg");
            //Assert.IsTrue(vm.Config.LastUpdated != null);
            //Assert.IsTrue(ct <= vm.Config.LastUpdated.ToDateTime());
            //Assert.IsTrue(vm.Config.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            //Assert.IsTrue(vm.Config.Version == 0);
            //Assert.IsTrue(vm.Config.IsNeedCurrentUpdate);


            //// can save config and DB backups
            //vm.BtnConfigSaveAs.Execute(@".\kuku.vbak");
            //var vmPrev = vm;

            //// can restore config and DB backups, folder ? and DB bases names ?
            //vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            //vm.BtnOpenConfig.Execute(@".\kuku.vbak");

            //Assert.IsTrue(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants.Count == 1);
            //Assert.IsTrue(vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].Name == cnst.Name);
            //Assert.IsTrue(ct <= vm.Config.LastUpdated.ToDateTime());
            //Assert.IsTrue(vm.Config.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            //Assert.IsTrue(vm.Config.Version == 0);
            //Assert.IsTrue(vm.pconfig_history != null);
            //Assert.IsTrue(vm.pconfig_history.CurrentConfig != null);
            //Assert.IsTrue(vm.pconfig_history.PrevStableConfig == null);
            //Assert.IsTrue(vm.Config.IsNeedCurrentUpdate);
        }
        [TestMethod]
        public void Main009_Diff()
        {
            // new config, not saved yet
            this.remove_config();
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            vm.BtnNewConfig.Execute();
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
        public async Task Main011_Diff_Constants()
        {
            // empty config
            this.remove_config();
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            vm.BtnNewConfig.Execute();
            var cfg = vm.Config;
            var gr = vm.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            Assert.IsFalse(gr.IsHasNew);
            Assert.IsFalse(gr.IsHasMarkedForDeletion);

            // create constant object and save
            var c1 = gr.AddConstant("c1");
            Assert.IsTrue(c1.IsNew);
            Assert.IsTrue(gr.IsHasNew);
            Assert.IsFalse(gr.IsHasMarkedForDeletion);
            // create enumeration constant object
            var en1 = cfg.Model.GroupEnumerations.AddEnumeration("en1", EnumEnumerationType.BYTE_VALUE);
            Assert.IsTrue(en1.IsNew);
            Assert.IsTrue(cfg.Model.GroupEnumerations.IsHasNew);
            Assert.IsFalse(cfg.Model.GroupEnumerations.IsHasMarkedForDeletion);
            var c2 = gr.AddConstantEnumeration("c2", en1);
            Assert.IsTrue(c2.IsNew);
            Assert.IsTrue(gr.IsHasNew);
            Assert.IsFalse(gr.IsHasMarkedForDeletion);
            var cat = cfg.Model.GroupCatalogs.AddCatalog("cat");
            var c3 = gr.AddConstantTypeRefCatalog("c3", cat);
            Assert.IsTrue(c2.IsNew);
            Assert.IsTrue(gr.IsHasNew);
            Assert.IsFalse(gr.IsHasMarkedForDeletion);
            vm.BtnConfigSaveAs.Execute(@".\kuku.vcfg");
            Assert.IsFalse(vm.Config.IsHasChanged);
            Assert.HasCount(1, cfg.Model.GroupEnumerations.ListEnumerations);
            Assert.HasCount(3, gr.ListConstants);
            Assert.IsTrue(c1.IsNew);
            Assert.IsTrue(c1.IsNewNode());
            Assert.IsTrue(gr.IsHasNew);
            Assert.IsFalse(gr.IsHasMarkedForDeletion);

            await vm.BtnConfigValidateAsync.ExecuteAsync();
            Assert.AreEqual(0, vm.Config.CountErrors);
            // can't update when enumeration is marked for deletion, but constant is not marked
            en1.IsMarkedForDeletion = true;
            await vm.BtnConfigValidateAsync.ExecuteAsync();
            Assert.AreEqual(1, vm.Config.CountErrors);
            c2.IsMarkedForDeletion = true;
            await vm.BtnConfigValidateAsync.ExecuteAsync();
            Assert.AreEqual(0, vm.Config.CountErrors);
            // can't update when catalog is marked for deletion, but constant is not marked
            cat.IsMarkedForDeletion = true;
            await vm.BtnConfigValidateAsync.ExecuteAsync();
            Assert.AreEqual(1, vm.Config.CountErrors);
            c3.IsMarkedForDeletion = true;
            await vm.BtnConfigValidateAsync.ExecuteAsync();
            Assert.AreEqual(0, vm.Config.CountErrors);

            // c1-new -> new
            Assert.IsTrue(vm.Config.IsNeedCurrentUpdate);
            vm.Config.SelectedNode = vm.Config;
            Assert.AreEqual(0, vm.Config.CountErrors);
            await vm.BtnConfigCurrentUpdateAsync.ExecuteAsync();
            Assert.IsFalse(vm.Config.IsHasChanged);
            Assert.IsFalse(vm.Config.IsNeedCurrentUpdate);
            Assert.IsEmpty(cfg.Model.GroupEnumerations.ListEnumerations);
            Assert.HasCount(1, gr.ListConstants); // second constant is deleted as it is new and enumeration marked for deletion
            Assert.IsTrue(c1.IsNew);
            Assert.IsTrue(gr.IsHasNew);
            Assert.IsFalse(gr.IsHasMarkedForDeletion);

            // c1-new -> not new, not del  
            Assert.IsFalse(vm.Config.IsHasChanged);
            await vm.BtnConfigCreateStableVersionAsync.ExecuteAsync();
            Assert.IsFalse(vm.Config.IsNeedCurrentUpdate);
            Assert.IsFalse(vm.Config.IsHasChanged);
            // prev c1 not new, not del
            Assert.HasCount(1, gr.ListConstants);
            Assert.IsFalse(c1.IsNew);
            Assert.IsFalse(gr.IsHasNew);
            Assert.IsFalse(gr.IsHasMarkedForDeletion);

            Assert.IsFalse(c1.IsMarkedForDeletion);
            Assert.IsFalse(c1.IsDeprecated());
            vm.Config.SelectedNode = c1;
            vm.BtnDelete.Execute();
            Assert.IsFalse(c1.IsNew);
            Assert.IsTrue(c1.IsMarkedForDeletion);
            Assert.IsTrue(c1.IsDeprecated());
            c2 = gr.AddConstant("c2");
            vm.Config.SelectedNode = c2;
            vm.BtnDelete.Execute();
            Assert.IsTrue(c2.IsNew);
            Assert.IsTrue(c2.IsMarkedForDeletion);
            Assert.IsFalse(c2.IsDeprecated());
            c3 = gr.AddConstant("c3");
            c3.DataType.Length = 101;
            Assert.HasCount(3, gr.ListConstants);
            Assert.IsTrue(gr.IsHasNew);
            Assert.IsTrue(gr.IsHasMarkedForDeletion);

            // c1- not new, del -> not new, del 
            // c2- new, del -> removed
            // c3- new -> new
            await vm.BtnConfigCurrentUpdateAsync.ExecuteAsync();
            Assert.IsFalse(vm.Config.IsHasChanged);
            // prev c1 not new, not del
            Assert.HasCount(2, gr.ListConstants);
            Assert.IsTrue(c1.IsMarkedForDeletion);
            Assert.IsFalse(c1.IsNew);
            Assert.IsTrue(c1.IsDeprecated());
            Assert.IsFalse(c3.IsMarkedForDeletion);
            Assert.IsTrue(c3.IsNew);
            Assert.IsTrue(gr.IsHasNew);
            Assert.IsTrue(gr.IsHasMarkedForDeletion);

            // c1- not new, del -> not new, del => previous not new, del
            // c3- new -> not new
            await vm.BtnConfigCreateStableVersionAsync.ExecuteAsync();
            Assert.IsFalse(vm.Config.IsNeedCurrentUpdate);
            Assert.IsFalse(vm.Config.IsHasChanged);
            // prev c1 not new, del
            Assert.HasCount(2, gr.ListConstants);
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
            await vm.BtnConfigCreateStableVersionAsync.ExecuteAsync();
            Assert.IsFalse(vm.Config.IsNeedCurrentUpdate);
            Assert.IsFalse(vm.Config.IsHasChanged);
            Assert.HasCount(1, gr.ListConstants);
            Assert.AreEqual(c3, gr.ListConstants[0]);
            Assert.IsFalse(c3.IsMarkedForDeletion);
            Assert.IsFalse(c3.IsNew);
            Assert.IsFalse(gr.IsHasNew);
            Assert.IsFalse(gr.IsHasMarkedForDeletion);
        }
        [TestMethod]
        public async Task Main012_Diff_Enumerations()
        {
            // empty config
            this.remove_config();
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            vm.BtnNewConfig.Execute();
            var cfg = vm.Config;
            Assert.IsFalse(cfg.Model.GroupEnumerations.IsHasNew);
            Assert.IsFalse(cfg.Model.GroupEnumerations.IsHasMarkedForDeletion);

            // create object and save
            var c1 = cfg.Model.GroupEnumerations.AddEnumeration("c1", EnumEnumerationType.BYTE_VALUE);
            Assert.IsTrue(c1.IsNew);
            Assert.IsTrue(cfg.Model.GroupEnumerations.IsHasNew);
            Assert.IsFalse(cfg.Model.GroupEnumerations.IsHasMarkedForDeletion);

            vm.BtnConfigSaveAs.Execute(@".\kuku.vcfg");
            Assert.HasCount(1, cfg.Model.GroupEnumerations.ListEnumerations);
            Assert.IsTrue(c1.IsNew);
            Assert.IsTrue(c1.IsNewNode());
            Assert.IsTrue(cfg.Model.GroupEnumerations.IsHasNew);
            Assert.IsFalse(cfg.Model.GroupEnumerations.IsHasMarkedForDeletion);

            // c1-new -> new
            await vm.BtnConfigCurrentUpdateAsync.ExecuteAsync();
            Assert.HasCount(1, cfg.Model.GroupEnumerations.ListEnumerations);
            Assert.IsTrue(c1.IsNew);
            Assert.IsTrue(cfg.Model.GroupEnumerations.IsHasNew);
            Assert.IsFalse(cfg.Model.GroupEnumerations.IsHasMarkedForDeletion);

            // c1-new -> not new, not del  
            await vm.BtnConfigCreateStableVersionAsync.ExecuteAsync();
            Assert.IsFalse(vm.Config.IsNeedCurrentUpdate);
            // prev c1 not new, not del
            Assert.HasCount(1, cfg.Model.GroupEnumerations.ListEnumerations);
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
            Assert.HasCount(3, cfg.Model.GroupEnumerations.ListEnumerations);
            Assert.IsTrue(cfg.Model.GroupEnumerations.IsHasNew);
            Assert.IsTrue(cfg.Model.GroupEnumerations.IsHasMarkedForDeletion);

            // c1- not new, del -> not new, del 
            // c2- new, del -> removed
            // c3- new -> new
            await vm.BtnConfigCurrentUpdateAsync.ExecuteAsync();
            // prev c1 not new, not del
            Assert.HasCount(2, cfg.Model.GroupEnumerations.ListEnumerations);
            Assert.IsTrue(c1.IsMarkedForDeletion);
            Assert.IsFalse(c1.IsNew);
            Assert.IsTrue(c1.IsDeprecated());
            Assert.IsFalse(c3.IsMarkedForDeletion);
            Assert.IsTrue(c3.IsNew);
            Assert.IsTrue(cfg.Model.GroupEnumerations.IsHasNew);
            Assert.IsTrue(cfg.Model.GroupEnumerations.IsHasMarkedForDeletion);

            // c1- not new, del -> not new, del => previous not new, del
            // c3- new -> not new
            await vm.BtnConfigCreateStableVersionAsync.ExecuteAsync();
            Assert.IsFalse(vm.Config.IsNeedCurrentUpdate);
            // prev c1 not new, del
            Assert.HasCount(2, cfg.Model.GroupEnumerations.ListEnumerations);
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
            await vm.BtnConfigCreateStableVersionAsync.ExecuteAsync();
            Assert.IsFalse(vm.Config.IsNeedCurrentUpdate);
            Assert.HasCount(1, cfg.Model.GroupEnumerations.ListEnumerations);
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
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            vm.BtnNewConfig.Execute();
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsFalse(vm.Config.Model.IsHasNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasNew);

            var m = vm.Config.Model;

            #region constant
            var gr = vm.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            Assert.IsTrue(gr.IsNew);
            Assert.IsFalse(gr.IsHasNew);
            var cn1 = gr.AddConstant("c1");
            Assert.IsTrue(cn1.IsNew);
            Assert.IsFalse(cn1.IsHasNew);
            Assert.IsTrue(gr.IsHasNew);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasNew);

            #region new
            //gr.ListConstants.Remove(cn1);
            cn1.IsNew = false;
            Assert.IsFalse(gr.IsHasNew);

            cn1.IsNew = true;
            Assert.IsTrue(gr.IsHasNew);
            #endregion new

            #region deletion

            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            cn1.IsMarkedForDeletion = true;
            Assert.IsTrue(m.GroupConstantGroups.IsHasMarkedForDeletion);
            Assert.IsTrue(m.GroupConstantGroups.IsHasNew);
            Assert.IsTrue(m.IsHasMarkedForDeletion);
            Assert.IsTrue(m.IsHasNew);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasNew);

            cn1.IsMarkedForDeletion = false;
            Assert.IsFalse(m.GroupConstantGroups.IsHasMarkedForDeletion);
            Assert.IsFalse(m.IsHasMarkedForDeletion);

            Assert.IsTrue(gr.IsHasNew);
            gr.ListConstants.Clear();
            Assert.IsFalse(gr.IsHasNew);
            Assert.IsTrue(m.GroupConstantGroups.IsHasNew);
            Assert.IsTrue(m.IsHasNew);

            m.GroupConstantGroups.ListConstantGroups.Clear();
            Assert.IsFalse(m.GroupConstantGroups.IsHasMarkedForDeletion);
            Assert.IsFalse(m.IsHasMarkedForDeletion);
            Assert.IsFalse(m.GroupConstantGroups.IsHasNew);
            Assert.IsFalse(m.IsHasNew);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);

            #endregion deletion

            #endregion constant

            #region enumeration
            var en1 = vm.Config.Model.GroupEnumerations.AddEnumeration("c1", EnumEnumerationType.BYTE_VALUE);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasNew);

            #region new
            vm.Config.Model.GroupEnumerations.ListEnumerations.Remove(en1);
            Assert.AreEqual(0, vm.Config.Model.GroupEnumerations.ListEnumerations.Count);
            Assert.IsFalse(vm.Config.Model.GroupEnumerations.IsHasNew);
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            en1 = vm.Config.Model.GroupEnumerations.AddEnumeration("c1", EnumEnumerationType.BYTE_VALUE);
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            en1.IsNew = false;
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            var p1 = en1.AddEnumerationPair("e1", "123");
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            p1.IsNew = false;
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            p1.IsNew = true;
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            en1.IsNew = true;
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            #endregion new

            #region deletion
            en1.IsMarkedForDeletion = true;
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasMarkedForDeletion);
            Assert.IsFalse(vm.Config.GroupAppSolutions.IsHasNew);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.IsFalse(vm.Config.GroupConfigLinks.IsHasNew);

            en1.IsMarkedForDeletion = false;
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);

            p1 = en1.AddEnumerationPair("e1", "123");
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);

            p1.IsMarkedForDeletion = true;
            Assert.IsTrue(en1.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasMarkedForDeletion);

            p1.IsMarkedForDeletion = false;
            Assert.IsFalse(en1.IsHasMarkedForDeletion);
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

            var pt2 = c2.AddDetails("kuku2");
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

            pt2 = c2.AddDetails("kuku");
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

            pt2 = d1.AddDetails("kuku2");
            Assert.IsTrue(vm.Config.Model.IsHasNew);

            pt2.IsNew = false;
            Assert.IsFalse(vm.Config.Model.IsHasNew);

            var vis = new ModelVisitorBase();
            vis.RunFromRoot(vm.Config, null, null, null, (v, n) =>
            {
                if (n is IEditableNode)
                {
                    var p = n as IEditableNode;
                    //p.IsNew = false;
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

            pt2 = d1.AddDetails("kuku");
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
        public async Task Main015_Delete_New_Enumerations()
        {
            // empty config
            this.remove_config();
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            vm.BtnNewConfig.Execute();
            vm.BtnConfigSaveAs.Execute(@".\kuku.vcfg");
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

            vm.BtnConfigSave.Execute();
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
            Assert.HasCount(3, vm.Config.Model.GroupEnumerations.ListEnumerations);

            vm.BtnConfigSave.Execute();
            // expect IsHasMarkedForDeletion and IsHasNew will not changed
            Assert.IsTrue(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.HasCount(3, vm.Config.Model.GroupEnumerations.ListEnumerations);

            Debug.Assert(vm.Config.Model.GroupEnumerations == vm.Config.Model.GroupEnumerations[0].Parent);
            Debug.Assert(vm.Config == vm.Config.Model.Parent);
            await vm.BtnConfigCurrentUpdateAsync.ExecuteAsync();
            Debug.Assert(vm.Config == vm.Config.Model.Parent);
            Debug.Assert(vm.Config.Model.GroupEnumerations == vm.Config.Model.GroupEnumerations[0].Parent);
            // expect new objects (IsNew) with IsMarkedForDeletion will be deleted in DB and model
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.HasCount(2, vm.Config.Model.GroupEnumerations.ListEnumerations);

            c3 = vm.Config.Model.GroupEnumerations.AddEnumeration("c3", EnumEnumerationType.INTEGER_VALUE);
            // mark for deletion
            c3.IsMarkedForDeletion = true;
            c2.IsMarkedForDeletion = true;
            Assert.IsTrue(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            Assert.HasCount(3, vm.Config.Model.GroupEnumerations.ListEnumerations);
            Debug.Assert(vm.Config.Model.GroupEnumerations == vm.Config.Model.GroupEnumerations[0].Parent);
            Debug.Assert(vm.Config == vm.Config.Model.Parent);

            vm.BtnConfigSave.Execute();
            await vm.BtnConfigCurrentUpdateAsync.ExecuteAsync();

            await vm.BtnConfigCreateStableVersionAsync.ExecuteAsync();
            Assert.IsFalse(vm.Config.IsNeedCurrentUpdate);
            Debug.Assert(vm.Config == vm.Config.Model.Parent);
            Debug.Assert(vm.Config.Model.GroupEnumerations == vm.Config.Model.GroupEnumerations[0].Parent);
            // expect IsHasMarkedForDeletion and IsHasNew will be false
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.HasCount(1, vm.Config.Model.GroupEnumerations.ListEnumerations);

            c3 = vm.Config.Model.GroupEnumerations.AddEnumeration("c3", EnumEnumerationType.INTEGER_VALUE);
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
            await vm.BtnConfigCurrentUpdateAsync.ExecuteAsync();
            Assert.IsFalse(vm.Config.Model.IsHasMarkedForDeletion);
            Assert.IsTrue(vm.Config.Model.IsHasNew);
        }
        [TestMethod]
        public void Main016_Diff_WorkWithAppGeneratorSettings()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            vm.BtnNewConfig.Execute();
            var pluginNode = (from p in vm.Config.GroupPlugins.ListPlugins where p.VPlugin is vPlugin.Sample.SamplePlugin select p).Single();
            var genDb = (IvPluginDbGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
            var genDbAccess = (IvPluginGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbAccess select p).Single().Generator;
            vm.BtnConfigSaveAs.Execute(@"..\..\..\..\TestApps\test1.vcfg");

            var sln = (AppSolution)vm.Config.GroupAppSolutions.NodeAddNewSubNode();
            sln.RelativeAppSolutionPath = @"..\..\..\..\TestApps\Solution.sln";

            var prj = (AppProject)sln.NodeAddNewSubNode();
            prj.RelativeAppProjectPath = @"..\..\..\..\TestApps\ConsoleApp1\ConsoleApp1.csproj";

            var gen = (AppProjectGenerator)prj.NodeAddNewSubNode();
            gen.RelativePathToGenFolder = @"..\..\..\..\TestApps\ConsoleApp1\Generated";
            gen.GenFileName = "test_file.cs";
            gen.PluginGuid = pluginNode.Guid;
            gen.PluginGeneratorGuid = genDbAccess.Guid;

            var ptst = new vPlugin.Sample.GeneratorDbAccessSettings(gen);
            Assert.IsFalse(ptst.IsAccessParam2.HasValue);
            Assert.IsNull(ptst.AccessParam4);
            Assert.IsFalse(ptst.IsAccessParam1);
            Assert.AreEqual(string.Empty, ptst.AccessParam3);

            var prms = (vPlugin.Sample.GeneratorDbAccessSettings)gen.DynamicGeneratorSettings;
            prms.IsAccessParam1 = true;
            prms.IsAccessParam2 = false;
            prms.AccessParam3 = "test";

            Assert.HasCount(1, vm.Config.DicActiveAppProjectGenerators);
            Assert.IsEmpty(vm.Config.Model.GroupCommon.ListNodeGeneratorsSettings);
            Assert.HasCount(1, vm.Config.Model.GroupConstantGroups.ListNodeGeneratorsSettings);
            foreach (var t in vm.Config.Model.GroupConstantGroups.ListConstantGroups)
            {
                Assert.HasCount(1, t.ListNodeGeneratorsSettings);
            }
            Assert.HasCount(1, vm.Config.Model.GroupEnumerations.ListNodeGeneratorsSettings);
            Assert.IsEmpty(vm.Config.Model.GroupCatalogs.ListNodeGeneratorsSettings);
            Assert.IsEmpty(vm.Config.Model.GroupDocuments.ListNodeGeneratorsSettings);
            //Assert.AreEqual(1, vm.Config.Model.GroupJournals.ListNodeGeneratorsSettings.Count);

            vm.BtnConfigSave.Execute();

            var vm2 = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.AreEqual(1, vm2.Config.GroupAppSolutions.Count());
            Assert.AreEqual(sln.RelativeAppSolutionPath, vm2.Config.GroupAppSolutions[0].RelativeAppSolutionPath);
            Assert.HasCount(1, vm2.Config.GroupAppSolutions[0].ListAppProjects);
            Assert.HasCount(1, vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators);
            var gen2 = vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0];
            Assert.AreEqual(gen.RelativePathToGenFolder, gen2.RelativePathToGenFolder);
            Assert.AreEqual(gen.GenFileName, gen2.GenFileName);
            Assert.AreEqual(gen.PluginGuid, gen2.PluginGuid);
            Assert.AreEqual(gen.PluginGeneratorGuid, gen2.PluginGeneratorGuid);
            vm2.Config.SelectedNode = gen2;
            //Assert.IsNotNull(gen2.DynamicNodesSettings);
            var prms2 = (vPlugin.Sample.GeneratorDbAccessSettings)gen2.DynamicGeneratorSettings;
            Assert.AreEqual(prms.IsAccessParam1, prms2.IsAccessParam1);
            Assert.AreEqual(prms.IsAccessParam2, prms2.IsAccessParam2);
            Assert.AreEqual(prms.AccessParam3, prms2.AccessParam3);
        }
        [TestMethod]
        public void Main017_Diff_WorkWithNodeGeneratorSettings()
        {
            // GeneratorDbAccessNodeCatalogFormSettings "Catalog.*.Form"
            // GeneratorDbAccessNodePropertySettings    "Property"

            // Settings workflow:
            // 1. When Config is loaded: init all generators settings VMs on all model nodes supported by generator
            // 2. When model node is added: init all generators settings VMs on this node
            // 3. When new generator is selected: old generator has to be removed from all model nodes, 
            //     and new generator settings has to be added for all model nodes
            // 4. When saving Config: convert all model nodes generators settings to string representations
            _logger.LogTrace("Start test");
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            //vm.BtnNewConfig.Execute(@".\kuku.vcfg");
            vm.BtnNewConfig.Execute();
            vm.BtnConfigSaveAs.Execute(@".\kuku.vcfg");

            var pluginNode = (from p in vm.Config.GroupPlugins.ListPlugins where p.VPlugin is vPlugin.Sample.SamplePlugin select p).Single();
            var genDb = (IvPluginDbGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
            var genDbAccess = (IvPluginGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbAccess select p).Single().Generator;

            var sln = (AppSolution)vm.Config.GroupAppSolutions.NodeAddNewSubNode();
            sln.RelativeAppSolutionPath = @"..\..\..\..\TestApps\Solution.sln";

            var prj = (AppProject)sln.NodeAddNewSubNode();
            prj.RelativeAppProjectPath = @"..\..\..\..\TestApps\ConsoleApp1\ConsoleApp1.csproj";
            //Assert.AreEqual(0, vm.Config.DicAppGenerators.Count);

            var gen = (AppProjectGenerator)prj.NodeAddNewSubNode();
            gen.RelativePathToGenFolder = @"..\..\..\..\TestApps\ConsoleApp1\Generated";
            gen.GenFileName = "test_file.cs";
            gen.PluginGuid = pluginNode.Guid;
            gen.PluginGeneratorGuid = genDbAccess.Guid;
            gen.Name = "AppGenName";
            gen.NameUi = "App Gen Name";

            // 3. When new generator is selected: old generator has to be removed from all model nodes, 
            //     and new generator settings has to be added for all model nodes
            Assert.HasCount(1, vm.Config.DicActiveAppProjectGenerators);
            Assert.HasCount(1, vm.Config.Model.GroupConstantGroups.ListNodeGeneratorsSettings);
            foreach (var t in vm.Config.Model.GroupConstantGroups.ListConstantGroups)
            {
                Assert.HasCount(1, t.ListNodeGeneratorsSettings);
            }
            Assert.IsEmpty(vm.Config.Model.GroupCatalogs.ListNodeGeneratorsSettings);
            gen.NodeRemove(false);
            Assert.HasCount(1, vm.Config.GroupAppSolutions[0].ListAppProjects);
            Assert.IsEmpty(vm.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators);
            Assert.IsEmpty(vm.Config.DicActiveAppProjectGenerators);
            Assert.IsEmpty(vm.Config.Model.GroupConstantGroups.ListNodeGeneratorsSettings);
            foreach (var t in vm.Config.Model.GroupConstantGroups.ListConstantGroups)
            {
                Assert.IsEmpty(t.ListNodeGeneratorsSettings);
            }
            Assert.IsEmpty(vm.Config.Model.GroupCatalogs.ListNodeGeneratorsSettings);
            gen = (AppProjectGenerator)prj.NodeAddNewSubNode();
            gen.RelativePathToGenFolder = @"..\..\..\..\TestApps\ConsoleApp1\Generated";
            gen.GenFileName = "test_file.cs";
            gen.PluginGuid = pluginNode.Guid;
            gen.PluginGeneratorGuid = genDbAccess.Guid;
            gen.Name = "AppGenName";
            gen.NameUi = "App Gen Name";
            Assert.HasCount(1, vm.Config.DicActiveAppProjectGenerators);
            Assert.HasCount(1, vm.Config.Model.GroupConstantGroups.ListNodeGeneratorsSettings);
            foreach (var t in vm.Config.Model.GroupConstantGroups.ListConstantGroups)
            {
                Assert.HasCount(1, t.ListNodeGeneratorsSettings);
            }
            Assert.IsEmpty(vm.Config.Model.GroupCatalogs.ListNodeGeneratorsSettings);

            // 2. When model node is added: init all generators settings VMs on this node
            Assert.HasCount(1, vm.Config.Model.GroupEnumerations.ListNodeGeneratorsSettings);
            vm.Config.Model.GroupEnumerations.NodeAddNewSubNode();
            Assert.HasCount(1, vm.Config.Model.GroupEnumerations[0].ListNodeGeneratorsSettings);
            Assert.HasCount(1, vm.Config.Model.GroupConstantGroups.ListNodeGeneratorsSettings);
            foreach (var t in vm.Config.Model.GroupConstantGroups.ListConstantGroups)
            {
                Assert.HasCount(1, t.ListNodeGeneratorsSettings);
            }
            vm.Config.Model.GroupCatalogs.NodeAddNewSubNode();
            Assert.HasCount(1, vm.Config.Model.GroupCatalogs[0].ListNodeGeneratorsSettings);
            vm.Config.Model.GroupCatalogs[0].GroupProperties.NodeAddNewSubNode();
            Assert.HasCount(1, vm.Config.Model.GroupCatalogs[0].GroupProperties[0].ListNodeGeneratorsSettings);
            vm.Config.Model.GroupCatalogs[0].GroupForms.NodeAddNewSubNode();
            Assert.HasCount(1, vm.Config.Model.GroupCatalogs[0].GroupForms[0].ListNodeGeneratorsSettings);
            vm.Config.Model.GroupDocuments.GroupListDocuments.NodeAddNewSubNode();
            Assert.HasCount(1, vm.Config.Model.GroupDocuments.GroupListDocuments[0].ListNodeGeneratorsSettings);
            vm.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties.NodeAddNewSubNode();
            Assert.HasCount(1, vm.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties[0].ListNodeGeneratorsSettings);


            var main = (vPlugin.Sample.GeneratorDbAccessSettings)gen.DynamicGeneratorSettings;
            main.IsAccessParam1 = true;
            main.IsAccessParam2 = false;

            var ngs = (vPlugin.Sample.GeneratorDbAccessNodeSettings)gen.DynamicModelNodeSettings;
            var nds = (vPlugin.Sample.GeneratorDbAccessNodeSettings)vm.Config.Model.GetSettings(gen.Guid);

            // on model we have link to AppProjectGenerator settings
            Assert.AreEqual(nds.IsParam1, ngs.IsParam1);
            nds.IsParam1 = true;
            Assert.AreEqual(nds.IsParam1, ngs.IsParam1);

            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].GroupProperties.IsIncluded(gen.Guid));
            nds.IsIncluded = false;
            Assert.IsFalse(vm.Config.Model.GroupCatalogs[0].GroupProperties.IsIncluded(gen.Guid));

            //foreach (var t in genDbAccess.GetListNodeGenerationSettings())
            //{
            //    if (t.SearchPathInModel == "Property")
            //        Assert.AreEqual(t.Guid, stt.NodeSettingsVmGuid);
            //}
            //Assert.AreEqual(gen.Name, stt.Name);
            //Assert.AreEqual(gen.NameUi, stt.NameUi);

            // 4. When saving Config: convert all model nodes generators settings to string representations
            //Assert.AreEqual("", vm.Config.Model.GroupConstants.ListGeneratorsSettings[0].Settings);
            vm.BtnConfigSave.Execute();
            //Assert.AreNotEqual("", vm.Config.Model.GroupConstants.ListGeneratorsSettings[0].Settings);
            Assert.HasCount(1, vm.Config.DicActiveAppProjectGenerators);

            // 1. When Config is loaded: init all generators settings VMs on all model nodes
            var vm2 = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.HasCount(1, vm2.Config.DicActiveAppProjectGenerators);
            Assert.HasCount(1, vm2.Config.Model.GroupEnumerations[0].ListNodeGeneratorsSettings);
            Assert.HasCount(1, vm2.Config.Model.GroupConstantGroups.ListNodeGeneratorsSettings);
            foreach (var t in vm2.Config.Model.GroupConstantGroups.ListConstantGroups)
            {
                Assert.HasCount(1, t.ListNodeGeneratorsSettings);
            }
            Assert.HasCount(1, vm2.Config.Model.GroupCatalogs[0].ListNodeGeneratorsSettings);
            Assert.HasCount(1, vm2.Config.Model.GroupCatalogs[0].GroupProperties[0].ListNodeGeneratorsSettings);
            Assert.HasCount(1, vm2.Config.Model.GroupCatalogs[0].GroupForms[0].ListNodeGeneratorsSettings);
            Assert.HasCount(1, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].ListNodeGeneratorsSettings);
            Assert.HasCount(1, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties[0].ListNodeGeneratorsSettings);

            main = (vPlugin.Sample.GeneratorDbAccessSettings)(vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0].DynamicGeneratorSettings);
            Assert.IsTrue(main.IsAccessParam1);
            Assert.IsFalse(main.IsAccessParam2);
            nds = (vPlugin.Sample.GeneratorDbAccessNodeSettings)vm.Config.Model.GetSettings(gen.Guid);
            Assert.IsTrue(nds.IsParam1);

            Assert.IsFalse(vm.Config.Model.GroupCatalogs[0].GroupProperties.IsIncluded(gen.Guid));

            // if new app progect generator is added, new setting are attached to all appropriate nodes
            var gen0 = vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0];
            Assert.HasCount(1, vm2.Config.DicActiveAppProjectGenerators);
            vm2.Config.GroupAppSolutions[0].ListAppProjects[0].NodeAddNewSubNode();
            var gen2 = (from p in vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators where p.Guid != gen0.Guid select p).Single();
            gen2.RelativePathToGenFolder = @"..\..\..\..\TestApps\ConsoleApp1\Generated";
            gen2.GenFileName = "test_file.cs";
            gen2.PluginGuid = pluginNode.Guid;
            // Expect attached settings for Property and Catalog.Form
            gen2.PluginGeneratorGuid = genDbAccess.Guid;
            Assert.HasCount(2, vm2.Config.DicActiveAppProjectGenerators);
            Assert.HasCount(2, vm2.Config.Model.GroupEnumerations[0].ListNodeGeneratorsSettings);
            Assert.HasCount(2, vm2.Config.Model.GroupConstantGroups.ListNodeGeneratorsSettings);
            //Assert.AreEqual(2, vm2.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListNodeGeneratorsSettings.Count);
            //Assert.AreEqual(2, vm2.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].ListNodeGeneratorsSettings.Count);
            Assert.HasCount(2, vm2.Config.Model.GroupCatalogs[0].ListNodeGeneratorsSettings);
            Assert.HasCount(2, vm2.Config.Model.GroupCatalogs[0].GroupProperties[0].ListNodeGeneratorsSettings);
            Assert.HasCount(2, vm2.Config.Model.GroupCatalogs[0].GroupForms[0].ListNodeGeneratorsSettings);
            Assert.HasCount(2, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].ListNodeGeneratorsSettings);
            Assert.HasCount(2, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties[0].ListNodeGeneratorsSettings);

            // if app progect generator is removed, attached seetings are removed from appropriate nodes as well
            gen2.NodeRemove(false);
            Assert.HasCount(1, vm2.Config.DicActiveAppProjectGenerators);
            Assert.HasCount(1, vm2.Config.Model.GroupEnumerations[0].ListNodeGeneratorsSettings);
            Assert.HasCount(1, vm2.Config.Model.GroupConstantGroups.ListNodeGeneratorsSettings);
            //Assert.AreEqual(1, vm2.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListNodeGeneratorsSettings.Count);
            //Assert.AreEqual(1, vm2.Config.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].ListNodeGeneratorsSettings.Count);
            Assert.HasCount(1, vm2.Config.Model.GroupCatalogs[0].ListNodeGeneratorsSettings);
            Assert.HasCount(1, vm2.Config.Model.GroupCatalogs[0].GroupProperties[0].ListNodeGeneratorsSettings);
            Assert.HasCount(1, vm2.Config.Model.GroupCatalogs[0].GroupForms[0].ListNodeGeneratorsSettings);
            Assert.HasCount(1, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].ListNodeGeneratorsSettings);
            Assert.HasCount(1, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties[0].ListNodeGeneratorsSettings);
            _logger.LogTrace("End test");
        }
        [TestMethod]
        public void Main018_Diff_WorkWithPluginsGroupSettings()
        {
            // Settings workflow:
            // 1. When Config is loaded: init plugin generators settings on all solution and project nodes
            // 2. When generator is removed, appropriate project/solution settings are still kept in case user will use them again. Such setting will be removed when configuration is restored.
            // 3. When new generator is added and it is having new project/solution settings, than appropriate project/solution settings has to be added in project/solution
            // 4. When saving Config: Only for active generators projects/solutions settings will be converted to string representations and saved.
            _logger.LogTrace("Start test");
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            //vm.BtnNewConfig.Execute(@".\kuku.vcfg");
            vm.BtnNewConfig.Execute();
            vm.BtnConfigSaveAs.Execute(@".\kuku.vcfg");
            var pluginNode = (from p in vm.Config.GroupPlugins.ListPlugins where p.VPlugin is vPlugin.Sample.SamplePlugin select p).Single();
            var genDb = (IvPluginDbGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
            var genDbAccess = (IvPluginGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbAccess select p).Single().Generator;

            var sln = (AppSolution)vm.Config.GroupAppSolutions.NodeAddNewSubNode();
            sln.RelativeAppSolutionPath = @"..\..\..\..\TestApps\Solution.sln";
            Assert.IsNull(sln.DynamicPluginGroupSettings);

            var prj = (AppProject)sln.NodeAddNewSubNode();
            prj.RelativeAppProjectPath = @"..\..\..\..\TestApps\ConsoleApp1\ConsoleApp1.csproj";

            var gen = (AppProjectGenerator)prj.NodeAddNewSubNode();
            gen.RelativePathToGenFolder = @"..\..\..\..\TestApps\ConsoleApp1\Generated";
            gen.GenFileName = "test_file.cs";
            Assert.AreEqual(string.Empty, gen.PluginGuid);
            Assert.AreEqual(string.Empty, gen.PluginGeneratorGuid);
            Assert.IsNull(gen.PluginGenerator);
            gen.PluginGuid = pluginNode.Guid;
            gen.PluginGeneratorGuid = genDbAccess.Guid;
            Assert.AreNotEqual(string.Empty, gen.PluginGuid);
            Assert.AreNotEqual(string.Empty, gen.PluginGeneratorGuid);
            Assert.IsNotNull(gen.PluginGenerator);
            gen.Name = "AppGenName";
            gen.NameUi = "App Gen Name";

            Assert.IsTrue(vm.Config.DicPlugins.ContainsKey(pluginNode.Guid));
            var plgn = vm.Config.DicPlugins[pluginNode.Guid];
            Assert.IsNotNull(plgn);
            Assert.IsTrue(sln.DicPluginsGroupSettings.ContainsKey(vPlugin.Sample.PluginsGroupSolutionSettings.GuidStatic));
            Assert.IsNotNull(sln.DynamicPluginGroupSettings);

            var set = (vPlugin.Sample.PluginsGroupSolutionSettings)sln.DicPluginsGroupSettings[vPlugin.Sample.PluginsGroupSolutionSettings.GuidStatic];
            set.IsGroupParam1 = true;

            vm.BtnConfigSaveAs.Execute(@".\kuku.vcfg");

            // 2. When generator is removed, appropriate project/solution settings are still kept in case user will use them again. Such setting will be removed when configuration is restored.
            gen.PluginGuid = string.Empty;
            Assert.IsTrue(vm.Config.DicPlugins.ContainsKey(pluginNode.Guid));
            Assert.IsTrue(sln.DicPluginsGroupSettings.ContainsKey(vPlugin.Sample.PluginsGroupSolutionSettings.GuidStatic));
            Assert.IsNotNull(sln.DynamicPluginGroupSettings);

            // 3. When new generator is added and it is having new project/solution settings, than appropriate project/solution settings has to be added in project/solution
            gen.PluginGuid = pluginNode.Guid;
            Assert.IsTrue(vm.Config.DicPlugins.ContainsKey(pluginNode.Guid));
            plgn = vm.Config.DicPlugins[pluginNode.Guid];
            Assert.IsNotNull(plgn);
            Assert.IsTrue(sln.DicPluginsGroupSettings.ContainsKey(vPlugin.Sample.PluginsGroupSolutionSettings.GuidStatic));
            Assert.IsNotNull(sln.DynamicPluginGroupSettings);

            gen.PluginGeneratorGuid = genDbAccess.Guid;
            Assert.IsTrue(sln.DicPluginsGroupSettings.ContainsKey(vPlugin.Sample.PluginsGroupSolutionSettings.GuidStatic));
            Assert.IsNotNull(sln.DynamicPluginGroupSettings);

            // 2. When generator is removed, appropriate project/solution settings are still kept in case user will use them again. Such setting will be removed when configuration is restored.
            gen.NodeRemove(false);
            Assert.IsTrue(vm.Config.DicPlugins.ContainsKey(pluginNode.Guid));
            Assert.IsTrue(sln.DicPluginsGroupSettings.ContainsKey(vPlugin.Sample.PluginsGroupSolutionSettings.GuidStatic));
            Assert.IsNotNull(sln.DynamicPluginGroupSettings);


            // 1. When Config is loaded: init plugin generators settings on all solution and project nodes
            var vm2 = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.IsTrue(vm2.Config.DicPlugins.ContainsKey(pluginNode.Guid));
            plgn = vm2.Config.DicPlugins[pluginNode.Guid];
            Assert.IsNotNull(plgn);
            sln = vm2.Config.GroupAppSolutions[0];
            Assert.IsTrue(sln.DicPluginsGroupSettings.ContainsKey(vPlugin.Sample.PluginsGroupSolutionSettings.GuidStatic));
            Assert.IsNotNull(sln.DynamicPluginGroupSettings);

            // 4. When saving Config: convert all projects/solutions settings to string representations
            set = (vPlugin.Sample.PluginsGroupSolutionSettings)sln.DicPluginsGroupSettings[vPlugin.Sample.PluginsGroupSolutionSettings.GuidStatic];
            Assert.IsTrue(set.IsGroupParam1);
            _logger.LogTrace("End test");
        }
        [TestMethod]
        public void Main081_BaseConfigLoading()
        {
            // empty config
            this.remove_config();
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            //vm.BtnNewConfig. Execute(@".\kuku.vcfg");
            vm.BtnNewConfig.Execute();
            Assert.AreEqual(0, vm.Config.GroupConfigLinks.Count());
            var gr = vm.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            vm.BtnConfigSaveAs.Execute(pathExt + MainPageVM.DEFAULT_CFG_FILEName);


            // base config
            var vmb = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            //vmb.BtnNewConfig.Execute(pathExt + "kuku.vcfg");
            vmb.BtnNewConfig.Execute();
            vmb.Config.Name = "ext";
            var grb = vmb.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            var c2 = grb.AddConstant("c2");
            vmb.BtnConfigSaveAs.Execute(pathExt + "kuku.vcfg");

            // create object and save
            var bcfg = vm.Config.GroupConfigLinks.AddBaseConfig("base", pathExt + "kuku.vcfg");
            var c1 = gr.AddConstant("c1");
            vm.BtnConfigSave.Execute();

            vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            gr = vm.Config.Model.GroupConstantGroups.ListConstantGroups[0];
            Assert.HasCount(1, gr.ListConstants);
            Assert.AreEqual(1, gr.Count());
            Assert.AreEqual("c1", gr.ListConstants[0].Name);
            Assert.AreEqual(1, vm.Config.GroupConfigLinks.Count());
            Assert.HasCount(1, vm.Config.GroupConfigLinks[0].ConfigBase.Model.GroupConstantGroups.ListConstantGroups);
            Assert.HasCount(1, vm.Config.GroupConfigLinks[0].ConfigBase.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants);
            Assert.AreEqual("c2", vm.Config.GroupConfigLinks[0].ConfigBase.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].Name);
            Assert.AreEqual("ext", vm.Config.GroupConfigLinks[0].ConfigBase.Name);
            Assert.AreEqual("ext", vm.Config.GroupConfigLinks[0].Name);
        }
        [TestMethod]
        public void Main082_BaseConfigDiff()
        {
            // empty config
            this.remove_config();
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            //vm.BtnNewConfig.Execute(@".\kuku.vcfg");
            vm.BtnNewConfig.Execute();
            vm.Config.Name = "main";
            var gr = vm.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            var c3 = gr.AddConstant("c3");
            Assert.AreEqual(0, vm.Config.GroupConfigLinks.Count());

            // base config
            var vmb = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            //vmb.BtnNewConfig.Execute(pathExt + "kuku.vcfg");
            vmb.BtnNewConfig.Execute();
            vmb.Config.Name = "ext";
            var grb = vm.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            var c2 = grb.AddConstant("c2");
            if (!Directory.Exists(pathExt))
            {
                Directory.CreateDirectory(pathExt);
            }
            vmb.BtnConfigSaveAs.Execute(pathExt + "kuku.vcfg");

            vm.BtnConfigSaveAs.Execute(pathExt + MainPageVM.DEFAULT_CFG_FILEName);

            // create object and save
            var bcfg = vm.Config.GroupConfigLinks.AddBaseConfig("base", pathExt + "kuku.vcfg");
            var c1 = vm.Config.Model.GroupConstantGroups.ListConstantGroups[0].AddConstant("c1");
            vm.BtnConfigSave.Execute();

            vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
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
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            //vm.BtnNewConfig.Execute(@".\kuku.vcfg");
            vm.BtnNewConfig.Execute();
            var m = vm.Config.Model;
            var gc = m.GroupCatalogs;
            var c = gc.AddCatalog("Simple");

            var p = c.GroupProperties.AddPropertyChar("char_notnullable", true);
            #region char
            p.RangeValuesRequirementStr = "'c'";
            p.Validate();
            var v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.IsEmpty(v.ListBoundaries);
            Assert.HasCount(1, v.ListValues);
            Assert.AreEqual("'c'", v.ListValues[0]);

            p.RangeValuesRequirementStr = " 'c'";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.IsEmpty(v.ListBoundaries);
            Assert.HasCount(1, v.ListValues);
            Assert.AreEqual("'c'", v.ListValues[0]);

            p.RangeValuesRequirementStr = "'c' ";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.IsEmpty(v.ListBoundaries);
            Assert.HasCount(1, v.ListValues);
            Assert.AreEqual("'c'", v.ListValues[0]);

            p.RangeValuesRequirementStr = "'c';'b'";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.IsEmpty(v.ListBoundaries);
            Assert.HasCount(2, v.ListValues);
            Assert.AreEqual("'c'", v.ListValues[0]);
            Assert.AreEqual("'b'", v.ListValues[1]);

            p.RangeValuesRequirementStr = " 'c' ; 'b' ";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.IsEmpty(v.ListBoundaries);
            Assert.HasCount(2, v.ListValues);
            Assert.AreEqual("'c'", v.ListValues[0]);
            Assert.AreEqual("'b'", v.ListValues[1]);

            p.RangeValuesRequirementStr = "#'c'";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.HasCount(1, v.ListBoundaries);
            Assert.IsEmpty(v.ListValues);
            Assert.IsNull(v.ListBoundaries[0].BoundaryMin);
            Assert.AreEqual("'c'", v.ListBoundaries[0].BoundaryMax);

            p.RangeValuesRequirementStr = "'c'#";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.HasCount(1, v.ListBoundaries);
            Assert.IsEmpty(v.ListValues);
            Assert.AreEqual("'c'", v.ListBoundaries[0].BoundaryMin);
            Assert.IsNull(v.ListBoundaries[0].BoundaryMax);

            p.RangeValuesRequirementStr = "'a'#'c'";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.HasCount(1, v.ListBoundaries);
            Assert.IsEmpty(v.ListValues);
            Assert.AreEqual("'a'", v.ListBoundaries[0].BoundaryMin);
            Assert.AreEqual("'c'", v.ListBoundaries[0].BoundaryMax);

            p.RangeValuesRequirementStr = "#'0';'a'#'c';'b';'d'#";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.HasCount(3, v.ListBoundaries);
            Assert.HasCount(1, v.ListValues);
            Assert.AreEqual("'b'", v.ListValues[0]);



            p.RangeValuesRequirementStr = "''";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsTrue(v.IsHasErrors);
            Assert.Contains("Can't", v.ListErrors[0]);
            Assert.Contains("''", v.ListErrors[0]);

            p.RangeValuesRequirementStr = "\"c\"";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsTrue(v.IsHasErrors);
            Assert.HasCount(1, v.ListErrors);
            Assert.Contains("Can't", v.ListErrors[0]);
            Assert.Contains("\"c\"", v.ListErrors[0]);

            p.RangeValuesRequirementStr = "  ";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);

            p.RangeValuesRequirementStr = "";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);

            p.RangeValuesRequirementStr = null;
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            #endregion char

            p = c.GroupProperties.AddPropertyString("str_unlimited", false, 0);

            #region string requirements validation
            p.RangeValuesRequirementStr = "\"\"";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.IsEmpty(v.ListBoundaries);
            Assert.HasCount(1, v.ListValues);
            Assert.AreEqual("\"\"", v.ListValues[0]);

            p.RangeValuesRequirementStr = "\"c\"";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.IsEmpty(v.ListBoundaries);
            Assert.HasCount(1, v.ListValues);
            Assert.AreEqual("\"c\"", v.ListValues[0]);

            p.RangeValuesRequirementStr = " \"c\"";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.IsEmpty(v.ListBoundaries);
            Assert.HasCount(1, v.ListValues);
            Assert.AreEqual("\"c\"", v.ListValues[0]);

            p.RangeValuesRequirementStr = "\"c\" ";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.IsEmpty(v.ListBoundaries);
            Assert.HasCount(1, v.ListValues);
            Assert.AreEqual("\"c\"", v.ListValues[0]);

            p.RangeValuesRequirementStr = "\"c\";\"b\"";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.IsEmpty(v.ListBoundaries);
            Assert.HasCount(2, v.ListValues);
            Assert.AreEqual("\"c\"", v.ListValues[0]);
            Assert.AreEqual("\"b\"", v.ListValues[1]);

            p.RangeValuesRequirementStr = " \"c\" ; \"b\" ";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.IsEmpty(v.ListBoundaries);
            Assert.HasCount(2, v.ListValues);
            Assert.AreEqual("\"c\"", v.ListValues[0]);
            Assert.AreEqual("\"b\"", v.ListValues[1]);

            p.RangeValuesRequirementStr = "'c'";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsTrue(v.IsHasErrors);
            Assert.HasCount(1, v.ListErrors);
            Assert.Contains("Can't", v.ListErrors[0]);
            Assert.Contains("'c'", v.ListErrors[0]);

            p.RangeValuesRequirementStr = "#\"c\"";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsTrue(v.IsHasErrors);
            Assert.HasCount(1, v.ListErrors);
            Assert.Contains("Can't", v.ListErrors[0]);
            Assert.Contains("#\"c\"", v.ListErrors[0]);

            p.RangeValuesRequirementStr = "\"c\"#";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsTrue(v.IsHasErrors);
            Assert.HasCount(1, v.ListErrors);
            Assert.Contains("Can't", v.ListErrors[0]);
            Assert.Contains("\"c\"#", v.ListErrors[0]);

            p.RangeValuesRequirementStr = "\"a\"#\"c\"";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsTrue(v.IsHasErrors);
            Assert.HasCount(1, v.ListErrors);
            Assert.Contains("Can't", v.ListErrors[0]);
            Assert.Contains("\"a\"#\"c\"", v.ListErrors[0]);

            p.RangeValuesRequirementStr = "";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            #endregion string requirements validation

            #region date
            #endregion date

            p = c.GroupProperties.AddPropertyNumerical("intmax", 9, 0);

            #region int requirements validation
            p.RangeValuesRequirementStr = "1";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.IsEmpty(v.ListBoundaries);
            Assert.HasCount(1, v.ListValues);
            Assert.AreEqual("1", v.ListValues[0]);

            p.RangeValuesRequirementStr = "#2";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.HasCount(1, v.ListBoundaries);
            Assert.IsEmpty(v.ListValues);
            Assert.IsNull(v.ListBoundaries[0].BoundaryMin);
            Assert.AreEqual("2", v.ListBoundaries[0].BoundaryMax);

            p.RangeValuesRequirementStr = "2#";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.HasCount(1, v.ListBoundaries);
            Assert.IsEmpty(v.ListValues);
            Assert.AreEqual("2", v.ListBoundaries[0].BoundaryMin);
            Assert.IsNull(v.ListBoundaries[0].BoundaryMax);

            p.RangeValuesRequirementStr = "3#7";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.HasCount(1, v.ListBoundaries);
            Assert.IsEmpty(v.ListValues);
            Assert.AreEqual("3", v.ListBoundaries[0].BoundaryMin);
            Assert.AreEqual("7", v.ListBoundaries[0].BoundaryMax);

            p.RangeValuesRequirementStr = "-7#-3";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.HasCount(1, v.ListBoundaries);
            Assert.IsEmpty(v.ListValues);
            Assert.AreEqual("-7", v.ListBoundaries[0].BoundaryMin);
            Assert.AreEqual("-3", v.ListBoundaries[0].BoundaryMax);

            p.RangeValuesRequirementStr = "#2;5#7;9;11#";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            Assert.HasCount(3, v.ListBoundaries);
            Assert.HasCount(1, v.ListValues);

            p.RangeValuesRequirementStr = "5#2";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsTrue(v.IsHasErrors);
            Assert.HasCount(1, v.ListErrors);
            Assert.Contains("greater", v.ListErrors[0]);
            Assert.Contains("5#2", v.ListErrors[0]);

            p.RangeValuesRequirementStr = "bbb";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsTrue(v.IsHasErrors);
            Assert.HasCount(1, v.ListErrors);
            Assert.Contains("Can't", v.ListErrors[0]);
            Assert.Contains("bbb", v.ListErrors[0]);

            p.RangeValuesRequirementStr = "";
            p.Validate();
            v = p.RangeValuesRequirements;
            Assert.IsFalse(v.IsHasErrors);
            #endregion int requirements validation
        }
        [TestMethod]
        public async Task Main092ModelPath()
        {
            // empty config
            this.remove_config();
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            vm.BtnNewConfig.Execute();
            var cfg = vm.Config;
            Assert.AreEqual("", cfg.ModelPath);
            Assert.AreEqual(null, cfg.ToolTipText);
            Assert.AreEqual("BaseConfigs", cfg.GroupConfigLinks.ModelPath);
            Assert.AreEqual("Links to Base Configs", cfg.GroupConfigLinks.ToolTipText);
            Assert.AreEqual("Plugins", cfg.GroupPlugins.ModelPath);
            Assert.AreEqual("Installed Plugins", cfg.GroupPlugins.ToolTipText);
            Assert.AreEqual("M", cfg.Model.ModelPath);
            Assert.AreEqual("Model of application for generation in the 'Applications' group", cfg.Model.ToolTipText);
            Assert.AreEqual("M.Common", cfg.Model.GroupCommon.ModelPath);
            Assert.AreEqual("M.Common.Roles", cfg.Model.GroupCommon.GroupRoles.ModelPath);

            Assert.AreEqual("M.Enumerations", cfg.Model.GroupEnumerations.ModelPath);
            var en1 = cfg.Model.GroupEnumerations.AddEnumeration("en1", EnumEnumerationType.BYTE_VALUE);
            Assert.AreEqual("M.Enumerations.en1", en1.ModelPath);

            Assert.AreEqual("M.Constants", cfg.Model.GroupConstantGroups.ModelPath);
            var gr = vm.Config.Model.GroupConstantGroups.AddGroupConstants("Gr");
            Assert.AreEqual("M.Constants.Gr", gr.ModelPath);
            var c1 = gr.AddConstant("c1");
            Assert.AreEqual("M.Constants.Gr.c1", c1.ModelPath);

            Assert.AreEqual("M.Catalogs", cfg.Model.GroupCatalogs.ModelPath);
            var cat = cfg.Model.GroupCatalogs.AddCatalog("cat");
            Assert.AreEqual("M.Catalogs.cat", cat.ModelPath);
            Assert.AreEqual("M.Catalogs.cat.Properties", cat.GroupProperties.ModelPath);
            cat.AddProperty("p1");
            Assert.AreEqual("M.Catalogs.cat.Properties.p1", cat.GroupProperties.ListProperties[0].ModelPath);

            Assert.AreEqual("M.Relations", cfg.Model.GroupRelations.ModelPath);
            Assert.AreEqual("M.Relations.ManyToMany", cfg.Model.GroupRelations.GroupListManyToManyRelations.ModelPath);
            var cat2 = cfg.Model.GroupCatalogs.AddCatalog("cat2");
            cfg.Model.GroupRelations.GroupListManyToManyRelations.AddRelation("cat-to-cat2", cat, cat2, false);
            Assert.AreEqual("M.Relations.ManyToMany.cat-to-cat2", cfg.Model.GroupRelations.GroupListManyToManyRelations.ListRelations[0].ModelPath);
            Assert.AreEqual("M.Relations.OneToOne", cfg.Model.GroupRelations.GroupListOneToOneRelations.ModelPath);
            var cat3 = cfg.Model.GroupCatalogs.AddCatalog("cat3");
            cfg.Model.GroupRelations.GroupListOneToOneRelations.AddRelation("cat-to-cat3", cat, cat3, false);
            Assert.AreEqual("M.Relations.OneToOne.cat-to-cat3", cfg.Model.GroupRelations.GroupListOneToOneRelations.ListRelations[0].ModelPath);

            Assert.AreEqual("M.Documents", cfg.Model.GroupDocuments.ModelPath);
            Assert.AreEqual("M.Documents.Documents", cfg.Model.GroupDocuments.GroupListDocuments.ModelPath);
            var d1 = cfg.Model.GroupDocuments.AddDocument("d1");
            Assert.AreEqual("M.Documents.Documents.d1", d1.ModelPath);
            Assert.AreEqual("M.Documents.Documents.d1.Properties", d1.GroupProperties.ModelPath);
            var p2 = d1.GroupProperties.AddProperty("p2");
            Assert.AreEqual("M.Documents.Documents.d1.Properties.p2", p2.ModelPath);


            Assert.AreEqual("M.Documents.Journals", cfg.Model.GroupDocuments.GroupJournals.ModelPath);
            Assert.AreEqual("M.Documents.Sequences", cfg.Model.GroupDocuments.GroupListSequences.ModelPath);
            Assert.AreEqual("M.Documents.Registers", cfg.Model.GroupDocuments.GroupRegisters.ModelPath);
            Assert.AreEqual("M.Documents.Timeline", cfg.Model.GroupDocuments.DocumentTimeline.ModelPath);
            //Assert.AreEqual("M.Documents.Timeline", cfg.Model.GroupDocuments.DocumentTimeline.ListProperties.ModelPath);
            Assert.AreEqual("Applications", cfg.GroupAppSolutions.ModelPath);

            //vm.BtnConfigSaveAs.Execute(@".\kuku.vcfg");
        }

        #region Roles
        [TestMethod]
        public void Main101_RolesTests()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            //vm.BtnNewConfig.Execute(@".\kuku.vcfg");
            vm.BtnNewConfig.Execute();
            var m = vm.Config.Model;

            // Catalog
            var gc = m.GroupCatalogs;
            var c = gc.AddCatalog("Simple");
            Assert.IsEmpty(c.dicCatalogAccess);
            var det = c.AddDetails("det1");
            var pdet = det.AddPropertyString("pdet", 5);
            Assert.IsEmpty(pdet.dicPropertyAccess);
            Assert.IsEmpty(det.GroupProperties.dicPropertyAccess);
            Assert.IsEmpty(det.dicDetailAccess);
            var role = m.GroupCommon.GroupRoles.AddRole("role1");
            Assert.HasCount(1, pdet.dicPropertyAccess);
            Assert.HasCount(1, det.GroupProperties.dicPropertyAccess);
            Assert.HasCount(1, det.dicDetailAccess);

            Assert.HasCount(1, c.dicCatalogAccess);
            Assert.AreEqual(EnumCatalogDetailAccess.C_MARK_DEL, c.GetRoleCatalogAccess(role));
            Assert.AreEqual(EnumCatalogDetailAccess.C_MARK_DEL, gc.GetRoleCatalogAccess(role));
            var p = c.GroupProperties.AddPropertyChar("char_notnullable");
            var pf = c.Folder.GroupProperties.AddPropertyChar("pfolder");
            Assert.HasCount(1, p.dicPropertyAccess);
            Assert.AreEqual(EnumCatalogDetailAccess.C_MARK_DEL, c.GetRoleCatalogAccess(role));
            Assert.AreEqual(EnumPrintAccess.PR_PRINT, c.GetRoleCatalogPrint(role));
            Assert.AreEqual(EnumPropertyAccess.P_EDIT, p.GetRolePropertyAccess(role));

            // Constant
            var gtg = m.GroupConstantGroups;
            var ctg = gtg.AddGroupConstants("settings1");
            var ct = ctg.AddConstantString("const1");
            Assert.HasCount(1, ct.dicConstantAccess);
            Assert.HasCount(1, ctg.dicConstantAccess);
            Assert.AreEqual(EnumConstantAccess.CN_EDIT, ct.GetRoleConstantAccess(role));
            Assert.AreEqual(EnumPrintAccess.PR_PRINT, ct.GetRoleConstantPrint(role));

            // Document
            var gd = m.GroupDocuments;
            var d = gd.AddDocument("Doc1");
            var gld = d.ParentGroupListDocuments;
            Assert.HasCount(1, d.dicDocumentAccess);
            Assert.AreEqual(EnumDocumentAccess.D_UNPOST, d.GetRoleDocumentAccess(role));
            Assert.AreEqual(EnumDocumentAccess.D_UNPOST, gd.GetRoleDocumentAccess(role));
            var pd = d.GroupProperties.AddPropertyChar("char_notnullable");
            Assert.HasCount(1, pd.dicPropertyAccess);
            Assert.AreEqual(EnumPropertyAccess.P_EDIT, pd.GetRolePropertyAccess(role));

            // Timeline
            var pTimeline = gd.DocumentTimeline.AddPropertyString("shared", 5);

            // Constant
            foreach (var tpr in Enum.GetValues<EnumPrintAccess>())
            {
                var enPrint = (EnumPrintAccess)tpr;
                foreach (var tpa in Enum.GetValues<EnumConstantAccess>())
                {
                    var enConstAccess = (EnumConstantAccess)tpa;
                    ct.SetRoleAccess(role, enConstAccess, enPrint);
                    switch (enConstAccess)
                    {
                        case EnumConstantAccess.CN_BY_PARENT:
                            // Group constants
                            TestConstantsGroup(role, gtg, ctg, ct, enPrint);
                            break;
                        default:
                            if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                                Assert.AreEqual(enPrint, ct.GetRoleConstantPrint(role));
                            Assert.AreEqual(enConstAccess, ct.GetRoleConstantAccess(role));
                            break;
                    }
                }
            }

            // Catalog property
            foreach (var tpr in Enum.GetValues<EnumPrintAccess>())
            {
                var enPrint = (EnumPrintAccess)tpr;
                foreach (var tpa in Enum.GetValues<EnumPropertyAccess>())
                {
                    var enPropAccess = (EnumPropertyAccess)tpa;
                    p.SetRoleAccess(role, enPropAccess, enPrint);
                    switch (enPropAccess)
                    {
                        case EnumPropertyAccess.P_BY_PARENT:
                            // Catalog
                            TestCatalog(role, c, p, enPrint);
                            break;
                        default:
                            if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                                Assert.AreEqual(enPrint, p.GetRolePropertyPrint(role));
                            Assert.AreEqual(enPropAccess, p.GetRolePropertyAccess(role));
                            break;
                    }
                }
            }

            // Catalog detail property
            foreach (var tpr in Enum.GetValues<EnumPrintAccess>())
            {
                var enPrint = (EnumPrintAccess)tpr;
                foreach (var tpa in Enum.GetValues<EnumPropertyAccess>())
                {
                    var enPropAccess = (EnumPropertyAccess)tpa;
                    pdet.SetRoleAccess(role, enPropAccess, enPrint);
                    switch (enPropAccess)
                    {
                        case EnumPropertyAccess.P_BY_PARENT:
                            // Catalog
                            TestDetail(role, c, det, pdet, enPrint);
                            break;
                        default:
                            if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                                Assert.AreEqual(enPrint, pdet.GetRolePropertyPrint(role));
                            Assert.AreEqual(enPropAccess, pdet.GetRolePropertyAccess(role));
                            break;
                    }
                }
            }

            // Catalog folder property
            foreach (var tpr in Enum.GetValues<EnumPrintAccess>())
            {
                var enPrint = (EnumPrintAccess)tpr;
                foreach (var tpa in Enum.GetValues<EnumPropertyAccess>())
                {
                    var enPropAccess = (EnumPropertyAccess)tpa;
                    pf.SetRoleAccess(role, enPropAccess, enPrint);
                    switch (enPropAccess)
                    {
                        case EnumPropertyAccess.P_BY_PARENT:
                            // Catalog
                            TestGroupListProperties(role, c.Folder, pf, enPrint);
                            break;
                        default:
                            if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                                Assert.AreEqual(enPrint, pf.GetRolePropertyPrint(role));
                            Assert.AreEqual(enPropAccess, pf.GetRolePropertyAccess(role));
                            break;
                    }
                }
            }

            // Document property
            foreach (var tpr in Enum.GetValues<EnumPrintAccess>())
            {
                var enPrint = (EnumPrintAccess)tpr;
                foreach (var tpa in Enum.GetValues<EnumPropertyAccess>())
                {
                    var enPropAccess = (EnumPropertyAccess)tpa;
                    pd.SetRoleAccess(role, enPropAccess, enPrint);
                    switch (enPropAccess)
                    {
                        case EnumPropertyAccess.P_BY_PARENT:
                            // Catalog
                            TestDocument(role, gd, gld, d, pd, enPrint);
                            break;
                        default:
                            if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                                Assert.AreEqual(enPrint, pd.GetRolePropertyPrint(role));
                            Assert.AreEqual(enPropAccess, pd.GetRolePropertyAccess(role));
                            break;
                    }
                }
            }

            // Timeline property
            foreach (var tpr in Enum.GetValues<EnumPrintAccess>())
            {
                var enPrint = (EnumPrintAccess)tpr;
                foreach (var tpa in Enum.GetValues<EnumPropertyAccess>())
                {
                    var enPropAccess = (EnumPropertyAccess)tpa;
                    pTimeline.SetRoleAccess(role, enPropAccess, enPrint);
                    switch (enPropAccess)
                    {
                        case EnumPropertyAccess.P_BY_PARENT:
                            // List properties
                            TestGroupListProperties(role, gd, pTimeline, enPrint);
                            break;
                        default:
                            if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                                Assert.AreEqual(enPrint, pTimeline.GetRolePropertyPrint(role));
                            Assert.AreEqual(enPropAccess, pTimeline.GetRolePropertyAccess(role));
                            break;
                    }
                }
            }
        }
        private static void TestConstantsGroup(Role role, GroupConstantGroups gctg, GroupListConstants ctg, Constant ct, EnumPrintAccess enPrint)
        {
            foreach (var tca in Enum.GetValues<EnumConstantAccess>())
            {
                var enCnstAccess = (EnumConstantAccess)tca;
                ctg.SetRoleAccess(role, enCnstAccess, enPrint);
                switch (enCnstAccess)
                {
                    case EnumConstantAccess.CN_BY_PARENT:
                        // Group of constants groups
                        TestGroupConstantsGroup(role, gctg, ctg, ct, enPrint);
                        break;
                    default:
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, ctg.GetRoleConstantPrint(role));
                        Assert.AreEqual(enCnstAccess, ctg.GetRoleConstantAccess(role));
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, ct.GetRoleConstantPrint(role));
                        switch (enCnstAccess)
                        {
                            case EnumConstantAccess.CN_BY_PARENT:
                                foreach (var tca_gc in Enum.GetValues<EnumConstantAccess>())
                                {
                                    var enGCnstAccess = (EnumConstantAccess)tca_gc;
                                    ctg.SetRoleAccess(role, enGCnstAccess, enPrint);
                                    switch (enGCnstAccess)
                                    {
                                        case EnumConstantAccess.CN_BY_PARENT:
                                            //Assert.AreNotEqual(EnumCatalogDetailAccess.C_BY_PARENT, enGCnstAccess, "Default value is not set");
                                            break;
                                        default:
                                            if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                                                Assert.AreEqual(enPrint, ctg.GetRoleConstantPrint(role));
                                            Assert.AreEqual(enGCnstAccess, ctg.GetRoleConstantAccess(role));
                                            if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                                                Assert.AreEqual(enPrint, ct.GetRoleConstantPrint(role));
                                            switch (enGCnstAccess)
                                            {
                                                case EnumConstantAccess.CN_BY_PARENT:
                                                    Assert.AreNotEqual(EnumConstantAccess.CN_BY_PARENT, enGCnstAccess, "Default value is not set");
                                                    break;
                                                case EnumConstantAccess.CN_HIDE:
                                                    Assert.AreEqual(EnumConstantAccess.CN_HIDE, ct.GetRoleConstantAccess(role));
                                                    break;
                                                case EnumConstantAccess.CN_VIEW:
                                                    Assert.AreEqual(EnumConstantAccess.CN_VIEW, ct.GetRoleConstantAccess(role));
                                                    break;
                                                case EnumConstantAccess.CN_EDIT:
                                                    Assert.AreEqual(EnumConstantAccess.CN_EDIT, ct.GetRoleConstantAccess(role));
                                                    break;
                                                default:
                                                    throw new NotImplementedException();
                                            }
                                            break;
                                    }
                                }
                                break;
                            case EnumConstantAccess.CN_HIDE:
                                Assert.AreEqual(EnumConstantAccess.CN_HIDE, ct.GetRoleConstantAccess(role));
                                break;
                            case EnumConstantAccess.CN_VIEW:
                                Assert.AreEqual(EnumConstantAccess.CN_VIEW, ct.GetRoleConstantAccess(role));
                                break;
                            case EnumConstantAccess.CN_EDIT:
                                Assert.AreEqual(EnumConstantAccess.CN_EDIT, ct.GetRoleConstantAccess(role));
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                        break;
                }
            }
        }
        private static void TestGroupConstantsGroup(Role role, GroupConstantGroups gctg, GroupListConstants ctg, Constant ct, EnumPrintAccess enPrint)
        {
            foreach (var tca in Enum.GetValues<EnumConstantAccess>())
            {
                var enCnstAccess = (EnumConstantAccess)tca;
                role.DefaultConstantEditAccessSettings = enCnstAccess;
                role.DefaultConstantPrintAccessSettings = enPrint;
                switch (enCnstAccess)
                {
                    case EnumConstantAccess.CN_BY_PARENT:
                        // Group of constants groups
                        break;
                    default:
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, gctg.GetRoleConstantPrint(role));
                        Assert.AreEqual(enCnstAccess, gctg.GetRoleConstantAccess(role));
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, ctg.GetRoleConstantPrint(role));
                        Assert.AreEqual(enCnstAccess, ctg.GetRoleConstantAccess(role));
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, ct.GetRoleConstantPrint(role));
                        switch (enCnstAccess)
                        {
                            //case EnumConstantAccess.CN_BY_PARENT:
                            //    foreach (var tca_gc in Enum.GetValues(typeof(EnumConstantAccess)))
                            //    {
                            //        var enGCnstAccess = (EnumConstantAccess)tca_gc;
                            //        ctg.SetRoleAccess(role, enGCnstAccess, enPrint);
                            //        switch (enGCnstAccess)
                            //        {
                            //            case EnumConstantAccess.CN_BY_PARENT:
                            //                //Assert.AreNotEqual(EnumCatalogDetailAccess.C_BY_PARENT, enGCnstAccess, "Default value is not set");
                            //                break;
                            //            default:
                            //                if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            //                    Assert.AreEqual(enPrint, ctg.GetRoleConstantPrint(role));
                            //                Assert.AreEqual(enGCnstAccess, ctg.GetRoleConstantAccess(role));
                            //                if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            //                    Assert.AreEqual(enPrint, ct.GetRoleConstantPrint(role));
                            //                switch (enGCnstAccess)
                            //                {
                            //                    case EnumConstantAccess.CN_BY_PARENT:
                            //                        Assert.AreNotEqual(EnumConstantAccess.CN_BY_PARENT, enGCnstAccess, "Default value is not set");
                            //                        break;
                            //                    case EnumConstantAccess.CN_HIDE:
                            //                        Assert.AreEqual(EnumConstantAccess.CN_HIDE, ct.GetRoleConstantAccess(role));
                            //                        break;
                            //                    case EnumConstantAccess.CN_VIEW:
                            //                        Assert.AreEqual(EnumConstantAccess.CN_VIEW, ct.GetRoleConstantAccess(role));
                            //                        break;
                            //                    case EnumConstantAccess.CN_EDIT:
                            //                        Assert.AreEqual(EnumConstantAccess.CN_EDIT, ct.GetRoleConstantAccess(role));
                            //                        break;
                            //                    default:
                            //                        throw new NotImplementedException();
                            //                }
                            //                break;
                            //        }
                            //    }
                            //    break;
                            case EnumConstantAccess.CN_HIDE:
                                Assert.AreEqual(EnumConstantAccess.CN_HIDE, ct.GetRoleConstantAccess(role));
                                break;
                            case EnumConstantAccess.CN_VIEW:
                                Assert.AreEqual(EnumConstantAccess.CN_VIEW, ct.GetRoleConstantAccess(role));
                                break;
                            case EnumConstantAccess.CN_EDIT:
                                Assert.AreEqual(EnumConstantAccess.CN_EDIT, ct.GetRoleConstantAccess(role));
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                        break;
                }
            }
        }
        private static void TestProperty(Role role, Property p, EnumPropertyAccess enPropAccess, EnumPrintAccess enPrint)
        {
            if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                Assert.AreEqual(enPrint, p.GetRolePropertyPrint(role));
            switch (enPropAccess)
            {
                case EnumPropertyAccess.P_HIDE:
                    Assert.AreEqual(EnumPropertyAccess.P_HIDE, p.GetRolePropertyAccess(role));
                    break;
                case EnumPropertyAccess.P_VIEW:
                    Assert.AreEqual(EnumPropertyAccess.P_VIEW, p.GetRolePropertyAccess(role));
                    break;
                case EnumPropertyAccess.P_EDIT:
                    Assert.AreEqual(EnumPropertyAccess.P_EDIT, p.GetRolePropertyAccess(role));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        private static void TestGroupListProperties(Role role, GroupDocuments gd, Property p, EnumPrintAccess enPrint)
        {
            var glp = p.ParentListPropertiesI;
            foreach (var tca in Enum.GetValues<EnumPropertyAccess>())
            {
                var enPropAccess = (EnumPropertyAccess)tca;
                glp.SetRoleAccess(role, enPropAccess, enPrint);
                switch (enPropAccess)
                {
                    case EnumPropertyAccess.P_BY_PARENT:
                        TestGroupDocumentsShared(role, gd, glp, p, enPrint);
                        break;
                    default:
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, glp.GetRolePropertyPrint(role));
                        Assert.AreEqual(enPropAccess, glp.GetRolePropertyAccess(role));
                        TestProperty(role, p, enPropAccess, enPrint);
                        break;
                }
            }
        }
        private static void TestGroupListProperties(Role role, CatalogFolder cf, Property p, EnumPrintAccess enPrint)
        {
            var glp = p.ParentGroupListProperties;
            foreach (var tca in Enum.GetValues<EnumPropertyAccess>())
            {
                var enPropAccess = (EnumPropertyAccess)tca;
                glp.SetRoleAccess(role, enPropAccess, enPrint);
                switch (enPropAccess)
                {
                    case EnumPropertyAccess.P_BY_PARENT:
                        TestCatalogFolder(role, cf, p, enPrint);
                        break;
                    default:
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, glp.GetRolePropertyPrint(role));
                        Assert.AreEqual(enPropAccess, glp.GetRolePropertyAccess(role));
                        TestProperty(role, p, enPropAccess, enPrint);
                        break;
                }
            }
        }
        private static void TestCatalogFolder(Role role, CatalogFolder cf, Property p, EnumPrintAccess enPrint)
        {
            var c = cf.ParentCatalog;
            var gc = c.ParentGroupListCatalogs;
            foreach (var tca in Enum.GetValues<EnumCatalogDetailAccess>())
            {
                var enCatAccess = (EnumCatalogDetailAccess)tca;
                c.SetRoleAccess(role, enCatAccess, enPrint);
                switch (enCatAccess)
                {
                    case EnumCatalogDetailAccess.C_BY_PARENT:
                        TestGroupCatalog(role, gc, c, p, enPrint);
                        break;
                    default:
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, c.GetRoleCatalogPrint(role));
                        Assert.AreEqual(enCatAccess, c.GetRoleCatalogAccess(role));
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, p.GetRolePropertyPrint(role));
                        switch (enCatAccess)
                        {
                            case EnumCatalogDetailAccess.C_BY_PARENT:
                                foreach (var tca_gc in Enum.GetValues<EnumCatalogDetailAccess>())
                                {
                                    var enGCatAccess = (EnumCatalogDetailAccess)tca_gc;
                                    role.DefaultCatalogEditAccessSettings = enGCatAccess;
                                    role.DefaultCatalogPrintAccessSettings = enPrint;
                                    switch (enGCatAccess)
                                    {
                                        case EnumCatalogDetailAccess.C_BY_PARENT:
                                            Assert.AreNotEqual(EnumCatalogDetailAccess.C_BY_PARENT, enGCatAccess, "Default value is not set");
                                            break;
                                        default:
                                            if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                                                Assert.AreEqual(enPrint, gc.GetRoleCatalogPrint(role));
                                            Assert.AreEqual(enGCatAccess, gc.GetRoleCatalogAccess(role));
                                            if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                                                Assert.AreEqual(enPrint, p.GetRolePropertyPrint(role));
                                            TestProperty(role, p, enGCatAccess);
                                            break;
                                    }
                                }
                                break;
                            default:
                                TestProperty(role, p, enCatAccess);
                                break;
                        }
                        break;
                }
            }
        }
        private static void TestCatalog(Role role, Catalog c, Property p, EnumPrintAccess enPrint)
        {
            var gc = c.ParentGroupListCatalogs;
            foreach (var tca in Enum.GetValues<EnumCatalogDetailAccess>())
            {
                var enCatAccess = (EnumCatalogDetailAccess)tca;
                c.SetRoleAccess(role, enCatAccess, enPrint);
                switch (enCatAccess)
                {
                    case EnumCatalogDetailAccess.C_BY_PARENT:
                        TestGroupCatalog(role, gc, c, p, enPrint);
                        break;
                    default:
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, c.GetRoleCatalogPrint(role));
                        Assert.AreEqual(enCatAccess, c.GetRoleCatalogAccess(role));
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, p.GetRolePropertyPrint(role));
                        switch (enCatAccess)
                        {
                            case EnumCatalogDetailAccess.C_BY_PARENT:
                                foreach (var tca_gc in Enum.GetValues<EnumCatalogDetailAccess>())
                                {
                                    var enGCatAccess = (EnumCatalogDetailAccess)tca_gc;
                                    role.DefaultCatalogEditAccessSettings = enGCatAccess;
                                    role.DefaultCatalogPrintAccessSettings = enPrint;
                                    switch (enGCatAccess)
                                    {
                                        case EnumCatalogDetailAccess.C_BY_PARENT:
                                            Assert.AreNotEqual(EnumCatalogDetailAccess.C_BY_PARENT, enGCatAccess, "Default value is not set");
                                            break;
                                        default:
                                            if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                                                Assert.AreEqual(enPrint, gc.GetRoleCatalogPrint(role));
                                            Assert.AreEqual(enGCatAccess, gc.GetRoleCatalogAccess(role));
                                            if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                                                Assert.AreEqual(enPrint, p.GetRolePropertyPrint(role));
                                            TestProperty(role, p, enGCatAccess);
                                            break;
                                    }
                                }
                                break;
                            default:
                                TestProperty(role, p, enCatAccess);
                                break;
                        }
                        break;
                }
            }
        }
        private static void TestDetail(Role role, Catalog c, Detail dt, Property p, EnumPrintAccess enPrint)
        {
            var gc = c.ParentGroupListCatalogs;
            foreach (var tca in Enum.GetValues<EnumCatalogDetailAccess>())
            {
                var enCatAccess = (EnumCatalogDetailAccess)tca;
                dt.SetRoleAccess(role, enCatAccess, enPrint);
                switch (enCatAccess)
                {
                    case EnumCatalogDetailAccess.C_BY_PARENT:
                        TestCatalog(role, c, p, enPrint);
                        break;
                    default:
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, dt.GetRoleDetailPrint(role));
                        Assert.AreEqual(enCatAccess, dt.GetRoleDetailAccess(role));
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, p.GetRolePropertyPrint(role));
                        switch (enCatAccess)
                        {
                            case EnumCatalogDetailAccess.C_BY_PARENT:
                                foreach (var tca_gc in Enum.GetValues<EnumCatalogDetailAccess>())
                                {
                                    var enGCatAccess = (EnumCatalogDetailAccess)tca_gc;
                                    role.DefaultCatalogEditAccessSettings = enGCatAccess;
                                    role.DefaultCatalogPrintAccessSettings = enPrint;
                                    switch (enGCatAccess)
                                    {
                                        case EnumCatalogDetailAccess.C_BY_PARENT:
                                            Assert.AreNotEqual(EnumCatalogDetailAccess.C_BY_PARENT, enGCatAccess, "Default value is not set");
                                            break;
                                        default:
                                            if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                                                Assert.AreEqual(enPrint, gc.GetRoleCatalogPrint(role));
                                            Assert.AreEqual(enGCatAccess, gc.GetRoleCatalogAccess(role));
                                            if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                                                Assert.AreEqual(enPrint, p.GetRolePropertyPrint(role));
                                            TestProperty(role, p, enGCatAccess);
                                            break;
                                    }
                                }
                                break;
                            default:
                                TestProperty(role, p, enCatAccess);
                                break;
                        }
                        break;
                }
            }
        }
        private static void TestProperty(Role role, Property p, EnumCatalogDetailAccess enGCatAccess)
        {
            switch (enGCatAccess)
            {
                case EnumCatalogDetailAccess.C_BY_PARENT:
                    Assert.AreNotEqual(EnumCatalogDetailAccess.C_BY_PARENT, enGCatAccess, "Default value is not set");
                    break;
                case EnumCatalogDetailAccess.C_HIDE:
                    Assert.AreEqual(EnumPropertyAccess.P_HIDE, p.GetRolePropertyAccess(role));
                    break;
                case EnumCatalogDetailAccess.C_VIEW:
                    Assert.AreEqual(EnumPropertyAccess.P_VIEW, p.GetRolePropertyAccess(role));
                    break;
                case EnumCatalogDetailAccess.C_EDIT_ITEMS:
                case EnumCatalogDetailAccess.C_EDIT_FOLDERS:
                case EnumCatalogDetailAccess.C_MARK_DEL:
                    Assert.AreEqual(EnumPropertyAccess.P_EDIT, p.GetRolePropertyAccess(role));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        private static void TestGroupCatalog(Role role, GroupListCatalogs gc, Catalog c, Property p, EnumPrintAccess enPrint)
        {
            foreach (var tca in Enum.GetValues<EnumCatalogDetailAccess>())
            {
                var enGrCatAccess = (EnumCatalogDetailAccess)tca;
                role.DefaultCatalogEditAccessSettings = enGrCatAccess;
                role.DefaultCatalogPrintAccessSettings = enPrint;
                switch (enGrCatAccess)
                {
                    case EnumCatalogDetailAccess.C_BY_PARENT:
                        break;
                    default:
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, gc.GetRoleCatalogPrint(role));
                        Assert.AreEqual(enGrCatAccess, gc.GetRoleCatalogAccess(role));
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, c.GetRoleCatalogPrint(role));
                        Assert.AreEqual(enGrCatAccess, c.GetRoleCatalogAccess(role));
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, p.GetRolePropertyPrint(role));
                        TestProperty(role, p, enGrCatAccess);
                        break;
                }
            }
        }
        private static void TestDocument(Role role, GroupDocuments gd, GroupListDocuments gld, Document d, Property p, EnumPrintAccess enPrint)
        {
            foreach (var tca in Enum.GetValues<EnumDocumentAccess>())
            {
                var enDocAccess = (EnumDocumentAccess)tca;
                d.SetRoleAccess(role, enDocAccess, enPrint);
                switch (enDocAccess)
                {
                    case EnumDocumentAccess.D_BY_PARENT:
                        TestGroupListDocuments(role, gd, gld, d, p, enPrint);
                        break;
                    default:
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, d.GetRoleDocumentPrint(role));
                        Assert.AreEqual(enDocAccess, d.GetRoleDocumentAccess(role));
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, p.GetRolePropertyPrint(role));
                        TestProperty(role, p, enDocAccess);
                        break;
                }
            }
        }
        private static void TestGroupListDocuments(Role role, GroupDocuments gd, GroupListDocuments gld, Document d, Property p, EnumPrintAccess enPrint)
        {
            foreach (var tca in Enum.GetValues<EnumDocumentAccess>())
            {
                var enGrDocAccess = (EnumDocumentAccess)tca;
                gld.SetRoleAccess(role, enGrDocAccess, enPrint);
                switch (enGrDocAccess)
                {
                    case EnumDocumentAccess.D_BY_PARENT:
                        TestGroupDocuments(role, gd, gld, d, p, enPrint);
                        break;
                    default:
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, gld.GetRoleDocumentPrint(role));
                        Assert.AreEqual(enGrDocAccess, gld.GetRoleDocumentAccess(role));
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, d.GetRoleDocumentPrint(role));
                        Assert.AreEqual(enGrDocAccess, d.GetRoleDocumentAccess(role));
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, p.GetRolePropertyPrint(role));
                        TestProperty(role, p, enGrDocAccess);
                        break;
                }
            }
        }
        private static void TestProperty(Role role, Property p, EnumDocumentAccess enGrDocAccess)
        {
            switch (enGrDocAccess)
            {
                case EnumDocumentAccess.D_HIDE:
                    Assert.AreEqual(EnumPropertyAccess.P_HIDE, p.GetRolePropertyAccess(role));
                    break;
                case EnumDocumentAccess.D_VIEW:
                    Assert.AreEqual(EnumPropertyAccess.P_VIEW, p.GetRolePropertyAccess(role));
                    break;
                case EnumDocumentAccess.D_EDIT:
                case EnumDocumentAccess.D_POST:
                case EnumDocumentAccess.D_UNPOST:
                case EnumDocumentAccess.D_MARK_DEL:
                    Assert.AreEqual(EnumPropertyAccess.P_EDIT, p.GetRolePropertyAccess(role));
                    break;
                case EnumDocumentAccess.D_VIEW_POST_DATA:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        private static void TestGroupDocuments(Role role, GroupDocuments gd, GroupListDocuments gld, Document d, Property p, EnumPrintAccess enPrint)
        {
            foreach (var tca in Enum.GetValues<EnumDocumentAccess>())
            {
                var enGrDocAccess = (EnumDocumentAccess)tca;
                role.DefaultDocumentEditAccessSettings = enGrDocAccess;
                role.DefaultDocumentPrintAccessSettings = enPrint;
                switch (enGrDocAccess)
                {
                    case EnumDocumentAccess.D_BY_PARENT:
                        break;
                    default:
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, gd.GetRoleDocumentPrint(role));
                        Assert.AreEqual(enGrDocAccess, gd.GetRoleDocumentAccess(role));
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, gld.GetRoleDocumentPrint(role));
                        Assert.AreEqual(enGrDocAccess, gld.GetRoleDocumentAccess(role));
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, d.GetRoleDocumentPrint(role));
                        Assert.AreEqual(enGrDocAccess, d.GetRoleDocumentAccess(role));
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, p.GetRolePropertyPrint(role));
                        TestProperty(role, p, enGrDocAccess);
                        break;
                }
            }
        }
        private static void TestGroupDocumentsShared(Role role, GroupDocuments gd, IListProperties glp, Property p, EnumPrintAccess enPrint)
        {
            foreach (var tca in Enum.GetValues<EnumPropertyAccess>())
            {
                var enPropAccess = (EnumDocumentAccess)tca;
                role.DefaultDocumentEditAccessSettings = enPropAccess;
                role.DefaultDocumentPrintAccessSettings = enPrint;
                switch (enPropAccess)
                {
                    case EnumDocumentAccess.D_BY_PARENT:
                        break;
                    default:
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, gd.GetRoleDocumentPrint(role));
                        Assert.AreEqual(enPropAccess, gd.GetRoleDocumentAccess(role));
                        if (enPrint != EnumPrintAccess.PR_BY_PARENT)
                            Assert.AreEqual(enPrint, p.GetRolePropertyPrint(role));
                        TestProperty(role, p, enPropAccess);
                        break;
                }
            }
        }
        #endregion Roles
    }
}
