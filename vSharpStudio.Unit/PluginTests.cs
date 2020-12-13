using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vSharpStudio.common;
using vSharpStudio.ViewModels;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.Unit
{
    [TestClass]
    public class PluginTests
    {
        static PluginTests()
        {
            LoggerInit.Init();
        }
        private static ILogger _logger;
        public PluginTests()
        {
            if (_logger == null)
                //_logger = Logger.ServiceProvider.GetRequiredService<ILogger<PluginTests>>();
                _logger = Logger.CreateLogger<PluginTests>();
        }

        [TestMethod]
        public void Plugin001SearchInPath()
        {
            var vm = new MainPageVM(false);
            vm.OnFormLoaded();
            vm.Config.Model.GroupCatalogs.NodeAddNewSubNode();
            var cat = vm.Config.Model.GroupCatalogs[0];
            cat.GroupProperties.NodeAddNewSubNode();
            var prop = cat.GroupProperties[0];
            Assert.IsTrue(ConfigObjectVmGenSettings<Property, PropertyValidator>.SearchInModelPathByPattern(prop.ModelPath, "Property"));
            Assert.IsTrue(ConfigObjectVmGenSettings<Property, PropertyValidator>.SearchInModelPathByPattern(prop.ModelPath, "Test;Property"));
            Assert.IsTrue(ConfigObjectVmGenSettings<Property, PropertyValidator>.SearchInModelPathByPattern(prop.ModelPath, "Catalog.*.Property"));
            Assert.IsTrue(ConfigObjectVmGenSettings<Property, PropertyValidator>.SearchInModelPathByPattern(cat.GroupProperties.ModelPath, "!Property"));
            Assert.IsTrue(ConfigObjectVmGenSettings<Property, PropertyValidator>.SearchInModelPathByPattern(prop.ModelPath, "!Test.*.Property"));

            Assert.IsFalse(ConfigObjectVmGenSettings<Property, PropertyValidator>.SearchInModelPathByPattern(prop.ModelPath, "Catalog"));
            Assert.IsFalse(ConfigObjectVmGenSettings<Property, PropertyValidator>.SearchInModelPathByPattern(prop.ModelPath, "Catalog.*.Test"));
            Assert.IsFalse(ConfigObjectVmGenSettings<Property, PropertyValidator>.SearchInModelPathByPattern(prop.ModelPath, "Test.*.Property"));
            Assert.IsFalse(ConfigObjectVmGenSettings<Property, PropertyValidator>.SearchInModelPathByPattern(prop.ModelPath, "!Property"));
            Assert.IsFalse(ConfigObjectVmGenSettings<Property, PropertyValidator>.SearchInModelPathByPattern(prop.ModelPath, "!Catalog.*.Property"));
        }
        private string pluginsFolderPath = "";
        [TestMethod]
        public void Plugin003CanLoadPlugin()
        {
            _logger.LogTrace("Start test".CallerInfo());
            var vm = new MainPageVM(false);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            vm.CommandConfigSaveAs.Execute(@".\");

            Assert.IsTrue(vm.Config.GroupPlugins.ListPlugins.Count > 0);
            var pluginNode = (from p in vm.Config.GroupPlugins.ListPlugins where p.VPlugin is vPlugin.Sample.SamplePlugin select p).Single();
            Assert.IsNotNull(pluginNode);
            Assert.IsTrue(pluginNode.ListGenerators.Count == 2); ;
            var genDb = (IvPluginDbGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
            Assert.IsNotNull(genDb);
            var genDbAccess = (IvPluginGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
            Assert.IsNotNull(genDbAccess);
            _logger.LogTrace("End test".CallerInfo());
        }
        [TestMethod]
        public void Plugin004WorkWithAppGeneratorSettings()
        {
            _logger.LogTrace("Start test".CallerInfo());
            var vm = new MainPageVM(false);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            vm.CommandConfigSaveAs.Execute(@".\");

            var pluginNode = (from p in vm.Config.GroupPlugins.ListPlugins where p.VPlugin is vPlugin.Sample.SamplePlugin select p).Single();
            var genDb = (IvPluginDbGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
            var genDbAccess = (IvPluginGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbAccess select p).Single().Generator;
            vm.CommandConfigSaveAs.Execute(@"..\..\..\..\TestApps\OldProject\test1.vcfg");

            var sln = (AppSolution)vm.Config.GroupAppSolutions.NodeAddNewSubNode();
            sln.RelativeAppSolutionPath = @"..\..\..\..\TestApps\OldProject\Solution.sln";

            var prj = (AppProject)sln.NodeAddNewSubNode();
            prj.RelativeAppProjectPath = @"..\..\..\..\TestApps\OldProject\ConsoleApp1\ConsoleApp1.csproj";
            prj.Namespace = "ns";

            var gen = (AppProjectGenerator)prj.NodeAddNewSubNode();
            gen.RelativePathToGenFolder = @"..\..\..\..\TestApps\OldProject\ConsoleApp1\Generated";
            gen.GenFileName = "test_file.cs";
            gen.PluginGuid = pluginNode.Guid;
            gen.PluginGeneratorGuid = genDbAccess.Guid;

            var prms = (vPlugin.Sample.GeneratorDbAccessSettings)gen.DynamicGeneratorSettings;
            prms.IsAccessParam1 = true;
            prms.IsAccessParam2 = false;
            prms.AccessParam3 = "kuku3";
            prms.AccessParam4 = "kuku4";

            var prmsGroup = (vPlugin.Sample.PluginsGroupSettings)gen.GetPluginsGroupSettings();
            prmsGroup.IsGroupParam1 = true;

            Assert.AreEqual(1, vm.Config.DicActiveAppProjectGenerators.Count);
            Assert.AreEqual(1, vm.Config.Model.GroupCommon.ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(1, vm.Config.Model.GroupConstants.ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(1, vm.Config.Model.GroupEnumerations.ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(1, vm.Config.Model.GroupCatalogs.ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(1, vm.Config.Model.GroupDocuments.ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(2, gen.ListGenerators.Count);
            Assert.IsNotNull(gen.DynamicGeneratorSettings);
            Assert.AreEqual(typeof(vPlugin.Sample.GeneratorDbAccessSettings).Name, gen.DynamicGeneratorSettings.GetType().Name);
            Assert.IsNotNull(vm.Config.Model.DynamicNodesSettings);
            //Assert.IsNotNull(vm.Config.Model.DynamicNodeDefaultSettings);
            //Assert.AreEqual(typeof(vPlugin.Sample.GeneratorDbAccessNodeSettings).Name, vm.Config.Model.DynamicNodesSettings.GetType().Name);

            vm.CommandConfigSave.Execute(null);

            var vm2 = new MainPageVM(true);
            vm2.OnFormLoaded();
            vm2.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            Assert.AreEqual(1, vm2.Config.GroupAppSolutions.Count());
            Assert.AreEqual(sln.RelativeAppSolutionPath, vm2.Config.GroupAppSolutions[0].RelativeAppSolutionPath);
            Assert.AreEqual(1, vm2.Config.GroupAppSolutions[0].ListAppProjects.Count());
            Assert.AreEqual(1, vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators.Count());
            var gen2 = vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0];
            Assert.AreEqual(gen.RelativePathToGenFolder, gen2.RelativePathToGenFolder);
            Assert.AreEqual(gen.GenFileName, gen2.GenFileName);
            Assert.AreEqual(gen.PluginGuid, gen2.PluginGuid);
            Assert.AreEqual(gen.PluginGeneratorGuid, gen2.PluginGeneratorGuid);
            Assert.AreEqual(2, gen2.ListGenerators.Count);
            Assert.IsNotNull(gen2.DynamicGeneratorSettings);
            Assert.AreEqual(typeof(vPlugin.Sample.GeneratorDbAccessSettings).Name, gen2.DynamicGeneratorSettings.GetType().Name);
            Assert.IsNotNull(vm2.Config.Model.DynamicNodesSettings);
            vm2.Config.SelectedNode = gen2;
            //Assert.IsNotNull(gen2.DynamicNodesSettings);
            var prms2 = (vPlugin.Sample.GeneratorDbAccessSettings)gen2.DynamicGeneratorSettings;
            Assert.AreEqual(prms.IsAccessParam1, prms2.IsAccessParam1);
            Assert.AreEqual(prms.IsAccessParam2, prms2.IsAccessParam2);
            Assert.AreEqual(prms.AccessParam3, prms2.AccessParam3);
            Assert.AreEqual(prms.AccessParam4, prms2.AccessParam4);
            var prmsGroup2 = (vPlugin.Sample.PluginsGroupSettings)gen2.GetPluginGroupSettings(vPlugin.Sample.PluginsGroupSettings.GuidStatic);
            Assert.IsTrue(prmsGroup2.IsGroupParam1);
            #region DicDiffResult
            var diffActiveAppProjectGenerators = DicDiffResult<string, IvPluginGenerator>.DicDiff(vm.Config.DicActiveAppProjectGenerators, vm2.Config.DicActiveAppProjectGenerators);
            Assert.AreEqual(0, diffActiveAppProjectGenerators.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffActiveAppProjectGenerators.Dic2ButNotInDic1.Count);
            var diffGenerators = DicDiffResult<string, IvPluginGenerator>.DicDiff(vm.Config.DicGenerators, vm2.Config.DicGenerators);
            Assert.AreEqual(0, diffGenerators.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffGenerators.Dic2ButNotInDic1.Count);
            var diffPlugins = DicDiffResult<string, IvPlugin>.DicDiff(vm.Config.DicPlugins, vm2.Config.DicPlugins);
            Assert.AreEqual(0, diffPlugins.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffPlugins.Dic2ButNotInDic1.Count);
            var diffPluginLists = DicDiffResult<vPluginLayerTypeEnum, List<PluginRow>>.DicDiff(vm.Config.DicPluginLists, vm2.Config.DicPluginLists);
            Assert.AreEqual(0, diffPluginLists.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffPluginLists.Dic2ButNotInDic1.Count);
            var diffNodes = DicDiffResult<string, ITreeConfigNode>.DicDiff(vm.Config.DicNodes, vm2.Config.DicNodes);
            Assert.AreEqual(0, diffNodes.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffNodes.Dic2ButNotInDic1.Count);
            #endregion DicDiffResult

            vm2.Config.ValidateSubTreeFromNode(vm.Config);
            Assert.IsTrue(vm2.Config.CountErrors == 0);
            vm2.Config.DebugTag = "stop";
            vm2.CommandConfigCurrentUpdate.Execute(new TestTransformation());

            #region DicDiffResult
            diffActiveAppProjectGenerators = DicDiffResult<string, IvPluginGenerator>.DicDiff(vm2.Config.DicActiveAppProjectGenerators, vm2.Config.PrevCurrentConfig.DicActiveAppProjectGenerators);
            Assert.AreEqual(0, diffActiveAppProjectGenerators.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffActiveAppProjectGenerators.Dic2ButNotInDic1.Count);
            diffGenerators = DicDiffResult<string, IvPluginGenerator>.DicDiff(vm2.Config.DicGenerators, (vm2.Config.PrevCurrentConfig as Config).DicGenerators);
            Assert.AreEqual(0, diffGenerators.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffGenerators.Dic2ButNotInDic1.Count);
            diffPlugins = DicDiffResult<string, IvPlugin>.DicDiff(vm2.Config.DicPlugins, (vm2.Config.PrevCurrentConfig as Config).DicPlugins);
            Assert.AreEqual(0, diffPlugins.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffPlugins.Dic2ButNotInDic1.Count);
            diffPluginLists = DicDiffResult<vPluginLayerTypeEnum, List<PluginRow>>.DicDiff(vm2.Config.DicPluginLists, (vm2.Config.PrevCurrentConfig as Config).DicPluginLists);
            Assert.AreEqual(0, diffPluginLists.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffPluginLists.Dic2ButNotInDic1.Count);
            diffNodes = DicDiffResult<string, ITreeConfigNode>.DicDiff(vm2.Config.DicNodes, vm2.Config.PrevCurrentConfig.DicNodes);
            //Assert.AreEqual(0, diffNodes.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffNodes.Dic2ButNotInDic1.Count);
            #endregion DicDiffResult
            gen2 = (AppProjectGenerator)vm2.Config.PrevCurrentConfig.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0];
            prms2 = (vPlugin.Sample.GeneratorDbAccessSettings)gen2.DynamicGeneratorSettings;
            Assert.AreEqual(prms.IsAccessParam1, prms2.IsAccessParam1);
            Assert.AreEqual(prms.IsAccessParam2, prms2.IsAccessParam2);
            Assert.AreEqual(prms.AccessParam3, prms2.AccessParam3);
            Assert.AreEqual(prms.AccessParam4, prms2.AccessParam4);

            vm2.Config.ValidateSubTreeFromNode(vm.Config);
            Assert.IsTrue(vm2.Config.CountErrors == 0);
            vm2.CommandConfigCreateStableVersion.Execute(new TestTransformation());

            #region DicDiffResult
            diffActiveAppProjectGenerators = DicDiffResult<string, IvPluginGenerator>.DicDiff(vm2.Config.DicActiveAppProjectGenerators, vm2.Config.PrevCurrentConfig.DicActiveAppProjectGenerators);
            Assert.AreEqual(0, diffActiveAppProjectGenerators.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffActiveAppProjectGenerators.Dic2ButNotInDic1.Count);
            diffGenerators = DicDiffResult<string, IvPluginGenerator>.DicDiff(vm2.Config.DicGenerators, (vm2.Config.PrevCurrentConfig as Config).DicGenerators);
            Assert.AreEqual(0, diffGenerators.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffGenerators.Dic2ButNotInDic1.Count);
            diffPlugins = DicDiffResult<string, IvPlugin>.DicDiff(vm2.Config.DicPlugins, (vm2.Config.PrevCurrentConfig as Config).DicPlugins);
            Assert.AreEqual(0, diffPlugins.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffPlugins.Dic2ButNotInDic1.Count);
            diffPluginLists = DicDiffResult<vPluginLayerTypeEnum, List<PluginRow>>.DicDiff(vm2.Config.DicPluginLists, (vm2.Config.PrevCurrentConfig as Config).DicPluginLists);
            Assert.AreEqual(0, diffPluginLists.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffPluginLists.Dic2ButNotInDic1.Count);
            diffNodes = DicDiffResult<string, ITreeConfigNode>.DicDiff(vm2.Config.DicNodes, vm2.Config.PrevCurrentConfig.DicNodes);
            //Assert.AreEqual(0, diffNodes.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffNodes.Dic2ButNotInDic1.Count);
            #endregion DicDiffResult
            gen2 = (AppProjectGenerator)vm2.Config.PrevStableConfig.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0];
            prms2 = (vPlugin.Sample.GeneratorDbAccessSettings)gen2.DynamicGeneratorSettings;
            Assert.AreEqual(prms.IsAccessParam1, prms2.IsAccessParam1);
            Assert.AreEqual(prms.IsAccessParam2, prms2.IsAccessParam2);
            Assert.AreEqual(prms.AccessParam3, prms2.AccessParam3);
            Assert.AreEqual(prms.AccessParam4, prms2.AccessParam4);
        }
        [TestMethod]
        public void Plugin005WorkWithNodeGeneratorSettings()
        {
            // GeneratorDbAccessNodeCatalogFormSettings "Catalog.*.Form"
            // GeneratorDbAccessNodePropertySettings    "Property"

            // Settings workflow:
            // 1. When Config is loaded: init all generators settings VMs on all model nodes
            // 2. When model node is added: init all generators settings VMs on this node
            // 3. When new generator is selected: old generator has to be removed from all model nodes, 
            //     and new generator settings has to be added for all model nodes
            // 4. When saving Config: convert all model nodes generators settings to string representations
            _logger.LogTrace("Start test".CallerInfo());
            var vm = new MainPageVM(false);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            vm.CommandConfigSaveAs.Execute(@".\");

            var pluginNode = (from p in vm.Config.GroupPlugins.ListPlugins where p.VPlugin is vPlugin.Sample.SamplePlugin select p).Single();
            var genDb = (IvPluginDbGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
            var genDbAccess = (IvPluginGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbAccess select p).Single().Generator;

            var sln = (AppSolution)vm.Config.GroupAppSolutions.NodeAddNewSubNode();
            sln.RelativeAppSolutionPath = @"..\..\..\..\TestApps\OldProject\Solution.sln";

            var prj = (AppProject)sln.NodeAddNewSubNode();
            prj.RelativeAppProjectPath = @"..\..\..\..\TestApps\OldProject\ConsoleApp1\ConsoleApp1.csproj";
            prj.Namespace = "testns";
            //Assert.AreEqual(0, vm.Config.DicAppGenerators.Count);

            var gen = (AppProjectGenerator)prj.NodeAddNewSubNode();
            gen.RelativePathToGenFolder = @"..\..\..\..\TestApps\OldProject\ConsoleApp1\Generated";
            gen.GenFileName = "test_file.cs";
            gen.PluginGuid = pluginNode.Guid;
            gen.PluginGeneratorGuid = genDbAccess.Guid;
            gen.Name = "AppGenName";
            gen.NameUi = "App Gen Name";

            // 3. When new generator is selected: old generator has to be removed from all model nodes, 
            //     and new generator settings has to be added for all model nodes
            Assert.AreEqual(1, vm.Config.DicActiveAppProjectGenerators.Count);
            Assert.AreEqual(1, vm.Config.Model.GroupConstants.ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(1, vm.Config.Model.GroupCatalogs.ListNodeGeneratorsSettings.Count);
            gen.NodeRemove(false);
            Assert.AreEqual(1, vm.Config.GroupAppSolutions[0].ListAppProjects.Count);
            Assert.AreEqual(0, vm.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators.Count);
            Assert.AreEqual(0, vm.Config.DicActiveAppProjectGenerators.Count);
            Assert.AreEqual(0, vm.Config.Model.GroupConstants.ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(0, vm.Config.Model.GroupCatalogs.ListNodeGeneratorsSettings.Count);
            gen = (AppProjectGenerator)prj.NodeAddNewSubNode();
            gen.RelativePathToGenFolder = @"..\..\..\..\TestApps\OldProject\ConsoleApp1\Generated";
            gen.GenFileName = "test_file.cs";
            gen.PluginGuid = pluginNode.Guid;
            gen.PluginGeneratorGuid = genDbAccess.Guid;
            gen.Name = "AppGenName";
            gen.NameUi = "App Gen Name";
            Assert.AreEqual(1, vm.Config.DicActiveAppProjectGenerators.Count);
            Assert.AreEqual(1, vm.Config.Model.GroupConstants.ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(1, vm.Config.Model.GroupCatalogs.ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(2, gen.ListGenerators.Count);

            // 2. When model node is added: init all generators settings VMs on this node
            Assert.AreEqual(1, vm.Config.Model.GroupEnumerations.ListNodeGeneratorsSettings.Count);
            vm.Config.Model.GroupEnumerations.NodeAddNewSubNode();
            Assert.AreEqual(1, vm.Config.Model.GroupEnumerations[0].ListNodeGeneratorsSettings.Count);
            vm.Config.Model.GroupConstants.NodeAddNewSubNode();
            Assert.AreEqual(1, vm.Config.Model.GroupConstants[0].ListNodeGeneratorsSettings.Count);
            vm.Config.Model.GroupCatalogs.NodeAddNewSubNode();
            Assert.AreEqual(1, vm.Config.Model.GroupCatalogs[0].ListNodeGeneratorsSettings.Count);
            vm.Config.Model.GroupCatalogs[0].GroupProperties.NodeAddNewSubNode();
            Assert.AreEqual(2, vm.Config.Model.GroupCatalogs[0].GroupProperties[0].ListNodeGeneratorsSettings.Count);
            vm.Config.Model.GroupCatalogs[0].GroupForms.NodeAddNewSubNode();
            Assert.AreEqual(2, vm.Config.Model.GroupCatalogs[0].GroupForms[0].ListNodeGeneratorsSettings.Count);
            vm.Config.Model.GroupDocuments.GroupListDocuments.NodeAddNewSubNode();
            Assert.AreEqual(1, vm.Config.Model.GroupDocuments.GroupListDocuments[0].ListNodeGeneratorsSettings.Count);
            vm.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties.NodeAddNewSubNode();
            Assert.AreEqual(2, vm.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties[0].ListNodeGeneratorsSettings.Count);


            var main = (vPlugin.Sample.GeneratorDbAccessSettings)gen.DynamicGeneratorSettings;
            main.IsAccessParam1 = true;
            main.IsAccessParam2 = false;

            //var ngs = (vPlugin.Sample.GeneratorDbAccessNodeSettings)gen.DynamicModelNodeSettings;
            var nds = (vPlugin.Sample.GeneratorDbAccessNodeSettings)vm.Config.Model.GetSettings(gen.Guid, vPlugin.Sample.GeneratorDbAccessNodeSettings.GuidStatic);

            // on model we have link to AppProjectGenerator settings
            //Assert.AreEqual(nds.IsParam1, ngs.IsParam1);
            nds.IsParam1 = true;
            //Assert.AreEqual(nds.IsParam1, ngs.IsParam1);

            Assert.IsTrue(vm.Config.Model.GroupCatalogs[0].GroupProperties.IsIncluded(gen.Guid, vPlugin.Sample.GeneratorDbAccessNodeSettings.GuidStatic));
            nds.IsIncluded = false;
            Assert.IsFalse(vm.Config.Model.GroupCatalogs[0].GroupProperties.IsIncluded(gen.Guid, vPlugin.Sample.GeneratorDbAccessNodeSettings.GuidStatic));

            //foreach (var t in genDbAccess.GetListNodeGenerationSettings())
            //{
            //    if (t.SearchPathInModel == "Property")
            //        Assert.AreEqual(t.Guid, stt.NodeSettingsVmGuid);
            //}
            //Assert.AreEqual(gen.Name, stt.Name);
            //Assert.AreEqual(gen.NameUi, stt.NameUi);

            // 4. When saving Config: convert all model nodes generators settings to string representations
            //Assert.AreEqual("", vm.Config.Model.GroupConstants.ListGeneratorsSettings[0].Settings);
            vm.CommandConfigSave.Execute(null);
            //Assert.AreNotEqual("", vm.Config.Model.GroupConstants.ListGeneratorsSettings[0].Settings);
            Assert.AreEqual(1, vm.Config.DicActiveAppProjectGenerators.Count);

            // 1. When Config is loaded: init all generators settings VMs on all model nodes
            var vm2 = new MainPageVM(true);
            vm2.OnFormLoaded();
            vm2.Compose(MainPageVM.GetvSharpStudioPluginsPath());

            Assert.AreEqual(1, vm2.Config.DicActiveAppProjectGenerators.Count);
            Assert.AreEqual(1, vm2.Config.Model.GroupEnumerations[0].ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(1, vm2.Config.Model.GroupConstants[0].ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(1, vm2.Config.Model.GroupCatalogs[0].ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(2, vm2.Config.Model.GroupCatalogs[0].GroupProperties[0].ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(2, vm2.Config.Model.GroupCatalogs[0].GroupForms[0].ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(1, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(2, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties[0].ListNodeGeneratorsSettings.Count);

            var cfgDiff = vm2.Config;
            Assert.AreEqual(1, cfgDiff.DicActiveAppProjectGenerators.Count);
            Assert.AreEqual(1, cfgDiff.Model.GroupConstants.ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(1, cfgDiff.Model.GroupCatalogs.ListNodeGeneratorsSettings.Count);

            main = (vPlugin.Sample.GeneratorDbAccessSettings)(vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0].DynamicGeneratorSettings);
            Assert.AreEqual(true, main.IsAccessParam1);
            Assert.AreEqual(false, main.IsAccessParam2);
            nds = (vPlugin.Sample.GeneratorDbAccessNodeSettings)vm.Config.Model.GetSettings(gen.Guid, vPlugin.Sample.GeneratorDbAccessNodeSettings.GuidStatic);
            Assert.AreEqual(true, nds.IsParam1);
            Assert.IsFalse(vm.Config.Model.GroupCatalogs[0].GroupProperties.IsIncluded(gen.Guid, nds.Guid));
            Assert.IsFalse(vm2.Config.Model.GroupCatalogs[0].GroupProperties.IsIncluded(gen.Guid, nds.Guid));

            vm2.CommandConfigCurrentUpdate.Execute(new TestTransformation());
            main = (vPlugin.Sample.GeneratorDbAccessSettings)(vm2.Config.PrevCurrentConfig.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0].DynamicGeneratorSettings);
            Assert.AreEqual(true, main.IsAccessParam1);
            Assert.AreEqual(false, main.IsAccessParam2);
            nds = (vPlugin.Sample.GeneratorDbAccessNodeSettings)vm2.Config.PrevCurrentConfig.Model.GetSettings(gen.Guid, vPlugin.Sample.GeneratorDbAccessNodeSettings.GuidStatic);
            Assert.AreEqual(true, nds.IsParam1);
            Assert.IsFalse(vm.Config.Model.GroupCatalogs[0].GroupProperties.IsIncluded(gen.Guid, nds.Guid));

            vm2.CommandConfigCreateStableVersion.Execute(new TestTransformation());
            Assert.IsFalse((vm2.Config.PrevStableConfig.Model.GroupCatalogs[0].GroupProperties as IGetNodeSetting).IsIncluded(gen.Guid, nds.Guid));
            main = (vPlugin.Sample.GeneratorDbAccessSettings)(vm2.Config.PrevStableConfig.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0].DynamicGeneratorSettings);
            Assert.AreEqual(true, main.IsAccessParam1);
            Assert.AreEqual(false, main.IsAccessParam2);
            nds = (vPlugin.Sample.GeneratorDbAccessNodeSettings)vm2.Config.PrevStableConfig.Model.GetSettings(gen.Guid, vPlugin.Sample.GeneratorDbAccessNodeSettings.GuidStatic);
            Assert.AreEqual(true, nds.IsParam1);
            Assert.IsFalse(vm.Config.Model.GroupCatalogs[0].GroupProperties.IsIncluded(gen.Guid, nds.Guid));


            _logger.LogTrace("End test".CallerInfo());
        }
        [TestMethod]
        public void Plugin006WorkWithPluginsGroupSettings()
        {
            // Settings workflow:
            // 1. When Config is loaded: init group plugin settings on all solution nodes
            // 2. When generator is removed, appropriate solution settings has to be removed if it is a last plugin in group
            // 3. When new generator is added and it is new group plugin, than appropriate solution settings has to be added in solution
            // 4. When saving Config: convert all solutions groups settings to string representations
            _logger.LogTrace("Start test".CallerInfo());
            var vm = new MainPageVM(false);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            vm.CommandConfigSaveAs.Execute(@".\");

            var pluginNode = (from p in vm.Config.GroupPlugins.ListPlugins where p.VPlugin is vPlugin.Sample.SamplePlugin select p).Single();
            var genDb = (IvPluginDbGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
            var genDbAccess = (IvPluginGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbAccess select p).Single().Generator;

            var sln = (AppSolution)vm.Config.GroupAppSolutions.NodeAddNewSubNode();
            sln.RelativeAppSolutionPath = @"..\..\..\..\TestApps\OldProject\Solution.sln";
            Assert.IsNull(sln.DynamicPluginGroupSettings);

            var prj = (AppProject)sln.NodeAddNewSubNode();
            prj.RelativeAppProjectPath = @"..\..\..\..\TestApps\OldProject\ConsoleApp1\ConsoleApp1.csproj";

            var gen = (AppProjectGenerator)prj.NodeAddNewSubNode();
            gen.RelativePathToGenFolder = @"..\..\..\..\TestApps\OldProject\ConsoleApp1\Generated";
            gen.GenFileName = "test_file.cs";
            gen.PluginGuid = pluginNode.Guid;
            gen.PluginGeneratorGuid = genDbAccess.Guid;
            gen.Name = "AppGenName";
            gen.NameUi = "App Gen Name";

            Assert.IsTrue(vm.Config.DicPlugins.ContainsKey(pluginNode.Guid));
            var plgn = vm.Config.DicPlugins[pluginNode.Guid];
            Assert.IsNotNull(plgn);
            Assert.IsTrue(sln.DicPluginsGroupSettings.ContainsKey(gen.PluginGroupSettingsGuid));
            Assert.IsNotNull(sln.DynamicPluginGroupSettings);

            var set = (vPlugin.Sample.PluginsGroupSettings)sln.DicPluginsGroupSettings[gen.PluginGroupSettingsGuid];
            set.IsGroupParam1 = true;

            vm.CommandConfigSave.Execute(null);

            // 2. When generator is removed, appropriate solution settings has to be removed if it is a last plugin in group
            gen.PluginGuid = null;
            Assert.IsTrue(vm.Config.DicPlugins.ContainsKey(pluginNode.Guid));
            Assert.IsFalse(sln.DicPluginsGroupSettings.ContainsKey(gen.PluginGroupSettingsGuid));
            Assert.IsNull(sln.DynamicPluginGroupSettings);

            // 3. When new generator is added and it is new group plugin, than appropriate solution settings has to be added in solution
            gen.PluginGuid = pluginNode.Guid;
            Assert.IsTrue(vm.Config.DicPlugins.ContainsKey(pluginNode.Guid));
            plgn = vm.Config.DicPlugins[pluginNode.Guid];
            Assert.IsNotNull(plgn);
            Assert.IsTrue(sln.DicPluginsGroupSettings.ContainsKey(gen.PluginGroupSettingsGuid));
            Assert.IsNotNull(sln.DynamicPluginGroupSettings);

            // 2. When generator is removed, appropriate solution settings has to be removed if it is a last plugin in group
            gen.NodeRemove(false);
            Assert.IsTrue(vm.Config.DicPlugins.ContainsKey(pluginNode.Guid));
            Assert.IsFalse(sln.DicPluginsGroupSettings.ContainsKey(gen.PluginGroupSettingsGuid));
            Assert.IsNull(sln.DynamicPluginGroupSettings);


            // 1. When Config is loaded: init group plugin settings on all solution nodes
            var vm2 = new MainPageVM(true);
            vm2.OnFormLoaded();
            vm2.Compose(MainPageVM.GetvSharpStudioPluginsPath());

            Assert.IsTrue(vm2.Config.DicPlugins.ContainsKey(pluginNode.Guid));
            plgn = vm2.Config.DicPlugins[pluginNode.Guid];
            Assert.IsNotNull(plgn);
            sln = vm2.Config.GroupAppSolutions[0];
            Assert.IsTrue(sln.DicPluginsGroupSettings.ContainsKey(gen.PluginGroupSettingsGuid));
            Assert.IsNotNull(sln.DynamicPluginGroupSettings);

            // 4. When saving Config: convert all solutions groups settings to string representations
            set = (vPlugin.Sample.PluginsGroupSettings)sln.DicPluginsGroupSettings[gen.PluginGroupSettingsGuid];
            Assert.IsTrue(set.IsGroupParam1);
            _logger.LogTrace("End test".CallerInfo());
        }
        [TestMethod]
        public void Plugin007WorkWithNodeGeneratorSettingsTwoProjects()
        {
            // GeneratorDbAccessNodeCatalogFormSettings "Catalog.*.Form"
            // GeneratorDbAccessNodePropertySettings    "Property"

            // Settings workflow:
            // 1. When Config is loaded: init all generators settings VMs on all model nodes
            // 2. When model node is added: init all generators settings VMs on this node
            // 3. When new generator is selected: old generator has to be removed from all model nodes, 
            //     and new generator settings has to be added for all model nodes
            // 4. When saving Config: convert all model nodes generators settings to string representations
            _logger.LogTrace("Start test".CallerInfo());
            var vm = new MainPageVM(false);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            vm.CommandConfigSaveAs.Execute(@".\");

            var pluginNode = (from p in vm.Config.GroupPlugins.ListPlugins where p.VPlugin is vPlugin.Sample.SamplePlugin select p).Single();
            var genDb = (IvPluginDbGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
            var genDbAccess = (IvPluginGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbAccess select p).Single().Generator;

            var sln = (AppSolution)vm.Config.GroupAppSolutions.NodeAddNewSubNode();
            sln.RelativeAppSolutionPath = @"..\..\..\..\TestApps\OldProject\Solution.sln";

            var prj = (AppProject)sln.NodeAddNewSubNode();
            prj.RelativeAppProjectPath = @"..\..\..\..\TestApps\OldProject\ConsoleApp1\ConsoleApp1.csproj";
            Assert.AreEqual(0, vm.Config.DicActiveAppProjectGenerators.Count);
            var gen = (AppProjectGenerator)prj.NodeAddNewSubNode();
            gen.RelativePathToGenFolder = @"..\..\..\..\TestApps\OldProject\ConsoleApp1\Generated";
            gen.GenFileName = "test_file.cs";
            gen.PluginGuid = pluginNode.Guid;
            gen.PluginGeneratorGuid = genDbAccess.Guid;
            gen.Name = "AppGenName";
            gen.NameUi = "App Gen Name";
            Assert.AreEqual(1, vm.Config.DicActiveAppProjectGenerators.Count);
            var main = (vPlugin.Sample.GeneratorDbAccessSettings)gen.DynamicGeneratorSettings;
            main.IsAccessParam1 = true;
            main.IsAccessParam2 = false;

            var gen2 = (AppProjectGenerator)prj.NodeAddNewSubNode();
            gen2.RelativePathToGenFolder = @"..\..\..\..\TestApps\OldProject\ConsoleApp1\Generated";
            gen2.GenFileName = "test_file2.cs";
            gen2.PluginGuid = pluginNode.Guid;
            gen2.PluginGeneratorGuid = genDbAccess.Guid;
            gen2.Name = "AppGenName2";
            gen2.NameUi = "App Gen Name2";
            Assert.AreEqual(2, vm.Config.DicActiveAppProjectGenerators.Count);
            var main2 = (vPlugin.Sample.GeneratorDbAccessSettings)gen2.DynamicGeneratorSettings;
            main2.IsAccessParam1 = false;
            main2.IsAccessParam2 = true;

            // main setting for generator are different for dofferent generators
            Assert.AreNotEqual(main.IsAccessParam1, main2.IsAccessParam1);
            Assert.AreNotEqual(main.IsAccessParam2, main2.IsAccessParam2);

            // node setting for generator are different for dofferent generators
            var nds = (vPlugin.Sample.GeneratorDbAccessNodeSettings)vm.Config.Model.GetSettings(gen.Guid, vPlugin.Sample.GeneratorDbAccessNodeSettings.GuidStatic);
            nds.IsParam1 = true;
            var nds2 = (vPlugin.Sample.GeneratorDbAccessNodeSettings)vm.Config.Model.GetSettings(gen2.Guid, vPlugin.Sample.GeneratorDbAccessNodeSettings.GuidStatic);
            nds2.IsParam1 = false;
            Assert.AreNotEqual(nds.IsParam1, nds2.IsParam1);

            vm.CommandConfigSave.Execute(null);

            // 1. When Config is loaded: init all generators settings VMs on all model nodes
            var vm2 = new MainPageVM(true);
            vm2.OnFormLoaded();
            vm2.Compose(MainPageVM.GetvSharpStudioPluginsPath());

            sln = vm2.Config.GroupAppSolutions.ListAppSolutions[0];
            prj = sln.ListAppProjects[0];
            gen = prj.ListAppProjectGenerators[0];
            gen2 = prj.ListAppProjectGenerators[1];
            main = (vPlugin.Sample.GeneratorDbAccessSettings)gen.DynamicGeneratorSettings;
            main2 = (vPlugin.Sample.GeneratorDbAccessSettings)gen2.DynamicGeneratorSettings;

            // main setting for generator are different for dofferent generators
            Assert.AreNotEqual(main.IsAccessParam1, main2.IsAccessParam1);
            Assert.AreNotEqual(main.IsAccessParam2, main2.IsAccessParam2);

            // node setting for generator are different for dofferent generators
            nds = (vPlugin.Sample.GeneratorDbAccessNodeSettings)vm.Config.Model.GetSettings(gen.Guid, vPlugin.Sample.GeneratorDbAccessNodeSettings.GuidStatic);
            nds2 = (vPlugin.Sample.GeneratorDbAccessNodeSettings)vm.Config.Model.GetSettings(gen2.Guid, vPlugin.Sample.GeneratorDbAccessNodeSettings.GuidStatic);
            Assert.AreNotEqual(nds.IsParam1, nds2.IsParam1);

            _logger.LogTrace("End test".CallerInfo());
        }
        [TestMethod]
        public void Plugin008WorkWithConnStringSettings()
        {
            _logger.LogTrace("Start test".CallerInfo());
            var vm = new MainPageVM(false);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            vm.CommandConfigSaveAs.Execute(@".\");

            var pluginNode = (from p in vm.Config.GroupPlugins.ListPlugins where p.VPlugin is vPlugin.Sample.SamplePlugin select p).Single();
            var genDb = (IvPluginDbGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
            vm.CommandConfigSaveAs.Execute(@"..\..\..\..\TestApps\OldProject\test1.vcfg");

            var sln = (AppSolution)vm.Config.GroupAppSolutions.NodeAddNewSubNode();
            sln.RelativeAppSolutionPath = @"..\..\..\..\TestApps\OldProject\Solution.sln";

            var prj = (AppProject)sln.NodeAddNewSubNode();
            prj.RelativeAppProjectPath = @"..\..\..\..\TestApps\OldProject\ConsoleApp1\ConsoleApp1.csproj";
            prj.Namespace = "ns";

            var gen = (AppProjectGenerator)prj.NodeAddNewSubNode();
            Assert.AreEqual("", gen.ConnStr);

            gen.RelativePathToGenFolder = @"..\..\..\..\TestApps\OldProject\ConsoleApp1\Generated";
            gen.GenFileName = "test_file.cs";
            gen.PluginGuid = pluginNode.Guid;
            gen.PluginGeneratorGuid = genDb.Guid;

            var connSettings = (vPlugin.Sample.DbConnectionStringSettings)gen.DynamicMainConnStrSettings;
            Assert.IsNotNull(connSettings);
            Assert.AreEqual("", gen.ConnStr);
            connSettings.StringSettings = "test_value";
            Assert.AreEqual("test_value", gen.ConnStr);
            gen.ConnStr = "test_value2";
            connSettings = (vPlugin.Sample.DbConnectionStringSettings)gen.DynamicMainConnStrSettings;
            Assert.AreEqual("test_value2", connSettings.StringSettings);

            vm.CommandConfigSave.Execute(null);

            var vm2 = new MainPageVM(true);
            vm2.OnFormLoaded();
            vm2.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            var gen2 = vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0];
            var connSettings2 = (vPlugin.Sample.DbConnectionStringSettings)gen2.DynamicMainConnStrSettings;
            Assert.IsNotNull(connSettings2);
            Assert.AreEqual("test_value2", gen2.ConnStr);
        }
        [TestMethod]
        [DataRow(@"OldProject\")]
        //[DataRow(@"SdkProject\")]
        public async Task Plugin012CanProduceCodeFileCompileTrgetProjectsAndUnitTestThem(string prType)
        {
            var slnFolder = @"..\..\..\..\TestApps\" + prType;
            var slnPath = slnFolder + "Solution.sln";
            var cfgPath = slnFolder + "test1.vcfg";
            var prjFolder = slnFolder + @"ConsoleApp1\";
            var prjPath = prjFolder + "ConsoleApp1.csproj";
            var genFolder = prjFolder + @"Generated\";
            var genFile = "test_file.cs";

            var vm = new MainPageVM(false);
            vm.OnFormLoaded();
            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());

            vm.Config.Name = "test1";
            var c1 = vm.Config.Model.GroupConstants.NodeAddNewSubNode();
            var c2 = vm.Config.Model.GroupConstants.NodeAddNewSubNode();
            vm.CommandConfigSaveAs.Execute(cfgPath);

            vm.Compose(MainPageVM.GetvSharpStudioPluginsPath());
            var pluginNode = (from p in vm.Config.GroupPlugins.ListPlugins where p.VPlugin is vPlugin.Sample.SamplePlugin select p).Single();
            var genDb = (IvPluginDbGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
            var genDbAccess = (IvPluginGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbAccess select p).Single().Generator;

            var sln = (AppSolution)vm.Config.GroupAppSolutions.NodeAddNewSubNode();
            sln.RelativeAppSolutionPath = slnPath;
            Assert.AreEqual("Solution.sln", sln.RelativeAppSolutionPath);

            var prj = (AppProject)sln.NodeAddNewSubNode();
            prj.Namespace = "Testnamespace";
            prj.RelativeAppProjectPath = prjPath;
            Assert.AreEqual(@"ConsoleApp1\ConsoleApp1.csproj", prj.RelativeAppProjectPath);

            var gen = (AppProjectGenerator)prj.NodeAddNewSubNode();
            gen.PluginGuid = pluginNode.Guid;
            gen.PluginGeneratorGuid = genDbAccess.Guid;
            gen.RelativePathToGenFolder = genFolder;
            Assert.AreEqual(@"Generated\", gen.RelativePathToGenFolder);
            gen.GenFileName = genFile;

            string fpath = prj.GetCombinedPath(gen.GetGenerationFilePath());
            File.WriteAllText(fpath, "namespace Test {}");

            sln.Validate();
            prj.Validate();
            gen.Validate();
            Assert.AreEqual(0, sln.ValidationCollection.Count);
            Assert.AreEqual(0, prj.ValidationCollection.Count);
            Assert.AreEqual(0, gen.ValidationCollection.Count);

            var prms = (vPlugin.Sample.GeneratorDbAccessSettings)gen.DynamicGeneratorSettings;
            prms.IsAccessParam1 = true;
            prms.IsAccessParam2 = false;
            prms.AccessParam3 = "test";
            vm.CommandConfigSave.Execute(null);

            var genFilePath = genFolder + genFile;
            if (File.Exists(genFilePath))
                File.Delete(genFilePath);

            // Can recognize not valid Config, SolutionPath is empty
            #region not valid Config
            // valid
            vm.Config.ValidateSubTreeFromNode(vm.Config);
            Assert.IsTrue(vm.Config.CountErrors == 0);
            // not valid
            sln.RelativeAppSolutionPath = null;
            vm.Config.ValidateSubTreeFromNode();
            Assert.IsTrue(vm.Config.CountErrors > 0);
            //// valid
            //sln.RelativeAppSolutionPath = slnPath;
            //vm.Config.ValidateSubTreeFromNode();
            //Assert.IsTrue(vm.Config.CountErrors == 0);

            TestTransformation tt = new TestTransformation();
            tt.IsThrowExceptionOnBuildValidated = true;
            await vm.CommandConfigCurrentUpdate.ExecuteAsync(tt);
            Assert.IsTrue(vm.ProgressVM.Exception != null);
            Assert.IsTrue(vm.Config.CountErrors > 0);
            #endregion not valid Config

            #region valid Config
            sln.RelativeAppSolutionPath = slnPath;
            tt = new TestTransformation();
            tt.IsThrowExceptionOnConfigValidated = true;
            await vm.CommandConfigCurrentUpdate.ExecuteAsync(tt);
            Assert.IsTrue(vm.ProgressVM.Exception != null);
            Assert.IsTrue(vm.ProgressVM.Exception.Message == nameof(tt.IsThrowExceptionOnConfigValidated));
            Assert.IsTrue(vm.Config.CountErrors == 0);
            #endregion valid Config

            #region compilable code
            tt = new TestTransformation();
            await vm.CommandConfigCurrentUpdate.ExecuteAsync(tt);
            Assert.IsTrue(vm.ProgressVM.Exception == null);
            #endregion compilable code

            // Can recognize exception before rename
            #region not compilable code
            tt = new TestTransformation();
            tt.IsThrowExceptionOnBuildValidated = true;
            await vm.CommandConfigCurrentUpdate.ExecuteAsync(tt);
            Assert.IsTrue(vm.ProgressVM.Exception != null);
            Assert.IsTrue(vm.ProgressVM.Exception.Message == nameof(tt.IsThrowExceptionOnBuildValidated));
            #endregion not compilable code

            // Exclude compilation process if there are no renames
            #region not compilable code
            tt = new TestTransformation();
            File.WriteAllText(fpath, "wrong c# code");
            await vm.CommandConfigCurrentUpdate.ExecuteAsync(tt);
            Assert.IsTrue(vm.ProgressVM.Exception == null);
            #endregion not compilable code

            // Include compilation process if there are renames
            #region not compilable code
            tt = new TestTransformation();
            tt.IsThrowExceptionOnBuildValidated = true;
            await vm.CommandConfigCurrentUpdate.ExecuteAsync(tt);
            Assert.IsTrue(vm.ProgressVM.Exception != null);
            Assert.IsTrue(vm.ProgressVM.Exception.Message == nameof(tt.IsThrowExceptionOnBuildValidated));
            #endregion not compilable code


            // Can rename

            // Can roll back if rename is fault
            //Assert.IsTrue(File.Exists(genFilePath));

            // Can generate code
            //#region generate valid code
            //if (File.Exists(genFilePath))
            //    File.Delete(genFilePath);
            //prms.IsGenerateNotValidCode = false;
            ////vm.CommandConfigCurrentUpdate.ExecuteWithException(null);
            //vm.CommandConfigCurrentUpdate.Execute(null);

            //Assert.IsTrue(File.Exists(genFolder + genFile));
            //#endregion generate valid code

            // Can recognize errors when compiling generated code

            // Can compile generated code

            // Can run unit test for generated code

            #region generate not valid code
            #endregion generate not valid code

            //Assert.IsTrue(false);
        }

        public void DicDiffDebug(Config cfg, Config anotherCfg)
        {
            var diffActiveAppProjectGenerators = DicDiffResult<string, IvPluginGenerator>.DicDiff(cfg.DicActiveAppProjectGenerators, anotherCfg.DicActiveAppProjectGenerators);
            Assert.AreEqual(0, diffActiveAppProjectGenerators.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffActiveAppProjectGenerators.Dic2ButNotInDic1.Count);
            var diffGenerators = DicDiffResult<string, IvPluginGenerator>.DicDiff(cfg.DicGenerators, anotherCfg.DicGenerators);
            Assert.AreEqual(0, diffGenerators.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffGenerators.Dic2ButNotInDic1.Count);
            var diffNodes = DicDiffResult<string, ITreeConfigNode>.DicDiff(cfg.DicNodes, anotherCfg.DicNodes);
            Assert.AreEqual(0, diffNodes.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffNodes.Dic2ButNotInDic1.Count);
            var diffPlugins = DicDiffResult<string, IvPlugin>.DicDiff(cfg.DicPlugins, anotherCfg.DicPlugins);
            Assert.AreEqual(0, diffPlugins.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffPlugins.Dic2ButNotInDic1.Count);
            var diffPluginLists = DicDiffResult<vPluginLayerTypeEnum, List<PluginRow>>.DicDiff(cfg.DicPluginLists, anotherCfg.DicPluginLists);
            Assert.AreEqual(0, diffPluginLists.Dic1ButNotInDic2.Count);
            Assert.AreEqual(0, diffPluginLists.Dic2ButNotInDic1.Count);
        }
    }
}
