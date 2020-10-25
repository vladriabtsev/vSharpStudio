using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vSharpStudio.ViewModels;
using vSharpStudio.common;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using vSharpStudio.vm.ViewModels;
using System.Threading.Tasks;

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
        [TestMethod]
        public void Plugin003CanLoadPlugin()
        {
            _logger.LogTrace("Start test".CallerInfo());
            var vm = new MainPageVM(false);
            vm.OnFormLoaded();
            vm.Compose();
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
            vm.Compose();
            var pluginNode = (from p in vm.Config.GroupPlugins.ListPlugins where p.VPlugin is vPlugin.Sample.SamplePlugin select p).Single();
            var genDb = (IvPluginDbGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
            var genDbAccess = (IvPluginGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbAccess select p).Single().Generator;
            vm.CommandConfigSaveAs.Execute(@"..\..\..\..\TestApps\test1.vcfg");

            var sln = (AppSolution)vm.Config.GroupAppSolutions.NodeAddNewSubNode();
            sln.RelativeAppSolutionPath = @"..\..\..\..\TestApps\Solution.sln";

            var prj = (AppProject)sln.NodeAddNewSubNode();
            prj.RelativeAppProjectPath = @"..\..\..\..\TestApps\ConsoleApp1\ConsoleApp1.csproj";

            var gen = (AppProjectGenerator)prj.NodeAddNewSubNode();
            gen.RelativePathToGenFolder = @"..\..\..\..\TestApps\ConsoleApp1\Generated";
            gen.GenFileName = "test_file.cs";
            gen.PluginGuid = pluginNode.Guid;
            gen.PluginGeneratorGuid = genDbAccess.Guid;

            var ptst = new vPlugin.Sample.GeneratorDbAccessSettings();
            Assert.IsFalse(ptst.IsAccessParam2.HasValue);
            Assert.IsNull(ptst.AccessParam4);
            Assert.IsFalse(ptst.IsAccessParam1);
            Assert.AreEqual(string.Empty, ptst.AccessParam3);

            var prms = (vPlugin.Sample.GeneratorDbAccessSettings)gen.DynamicMainSettings;
            prms.IsAccessParam1 = true;
            prms.IsAccessParam2 = false;
            prms.AccessParam3 = "test";

            Assert.AreEqual(1, vm.Config.DicActiveAppProjectGenerators.Count);
            Assert.AreEqual(1, vm.Config.Model.GroupCommon.ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(1, vm.Config.Model.GroupConstants.ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(1, vm.Config.Model.GroupEnumerations.ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(1, vm.Config.Model.GroupCatalogs.ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(1, vm.Config.Model.GroupDocuments.ListNodeGeneratorsSettings.Count);
            //Assert.AreEqual(1, vm.Config.Model.GroupJournals.ListNodeGeneratorsSettings.Count);

            vm.CommandConfigSave.Execute(null);

            var vm2 = new MainPageVM(true);
            vm2.OnFormLoaded();
            vm2.Compose();
            Assert.AreEqual(1, vm2.Config.GroupAppSolutions.Count());
            Assert.AreEqual(sln.RelativeAppSolutionPath, vm2.Config.GroupAppSolutions[0].RelativeAppSolutionPath);
            Assert.AreEqual(1, vm2.Config.GroupAppSolutions[0].ListAppProjects.Count());
            Assert.AreEqual(1, vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators.Count());
            var gen2 = vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0];
            Assert.AreEqual(gen.RelativePathToGenFolder, gen2.RelativePathToGenFolder);
            Assert.AreEqual(gen.GenFileName, gen2.GenFileName);
            Assert.AreEqual(gen.PluginGuid, gen2.PluginGuid);
            Assert.AreEqual(gen.PluginGeneratorGuid, gen2.PluginGeneratorGuid);
            vm2.Config.SelectedNode = gen2;
            //Assert.IsNotNull(gen2.DynamicNodesSettings);
            var prms2 = (vPlugin.Sample.GeneratorDbAccessSettings)gen2.DynamicMainSettings;
            Assert.AreEqual(prms.IsAccessParam1, prms2.IsAccessParam1);
            Assert.AreEqual(prms.IsAccessParam2, prms2.IsAccessParam2);
            Assert.AreEqual(prms.AccessParam3, prms2.AccessParam3);
            _logger.LogTrace("End test".CallerInfo());
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
            vm.Compose();
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
            gen.RelativePathToGenFolder = @"..\..\..\..\TestApps\ConsoleApp1\Generated";
            gen.GenFileName = "test_file.cs";
            gen.PluginGuid = pluginNode.Guid;
            gen.PluginGeneratorGuid = genDbAccess.Guid;
            gen.Name = "AppGenName";
            gen.NameUi = "App Gen Name";
            Assert.AreEqual(1, vm.Config.DicActiveAppProjectGenerators.Count);
            Assert.AreEqual(1, vm.Config.Model.GroupConstants.ListNodeGeneratorsSettings.Count);
            Assert.AreEqual(1, vm.Config.Model.GroupCatalogs.ListNodeGeneratorsSettings.Count);

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


            var main = (vPlugin.Sample.GeneratorDbAccessSettings)gen.DynamicMainSettings;
            main.IsAccessParam1 = true;
            main.IsAccessParam2 = false;

            var ngs = (vPlugin.Sample.GeneratorDbAccessNodeSettings)gen.GetSettings(gen.Guid, vPlugin.Sample.GeneratorDbAccessNodeSettings.GuidStatic);
            var nds = (vPlugin.Sample.GeneratorDbAccessNodeSettings)vm.Config.Model.GetSettings(gen.Guid, vPlugin.Sample.GeneratorDbAccessNodeSettings.GuidStatic);

            // on model we have link to AppProjectGenerator settings
            Assert.AreEqual(nds.IsParam1, ngs.IsParam1);
            nds.IsParam1 = true;
            Assert.AreEqual(nds.IsParam1, ngs.IsParam1);

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
            vm2.Compose();

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

            main = (vPlugin.Sample.GeneratorDbAccessSettings)(vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0].DynamicMainSettings);
            Assert.AreEqual(true, main.IsAccessParam1);
            Assert.AreEqual(false, main.IsAccessParam2);
            nds = (vPlugin.Sample.GeneratorDbAccessNodeSettings)vm.Config.Model.GetSettings(gen.Guid, vPlugin.Sample.GeneratorDbAccessNodeSettings.GuidStatic);
            Assert.AreEqual(true, nds.IsParam1);

            Assert.IsFalse(vm.Config.Model.GroupCatalogs[0].GroupProperties.IsIncluded(gen.Guid, nds.Guid));

            //// if new app progect generator is added, new setting are attached to all appropriate nodes
            //var gen0 = vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0];
            //Assert.AreEqual(1, vm2.Config.DicActiveAppProjectGenerators.Count);
            //vm2.Config.GroupAppSolutions[0].ListAppProjects[0].NodeAddNewSubNode();
            //var gen2 = (from p in vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators where p.Guid != gen0.Guid select p).Single();
            //gen2.RelativePathToGenFolder = @"..\..\..\..\TestApps\ConsoleApp1\Generated";
            //gen2.GenFileName = "test_file.cs";
            //gen2.PluginGuid = pluginNode.Guid;
            //// Expect attached settings for Property and Catalog.Form
            //gen2.PluginGeneratorGuid = genDbAccess.Guid;
            //Assert.AreEqual(2, vm2.Config.DicActiveAppProjectGenerators.Count);
            //Assert.AreEqual(2, vm2.Config.Model.GroupEnumerations[0].ListNodeGeneratorsSettings.Count);
            //Assert.AreEqual(2, vm2.Config.Model.GroupConstants[0].ListNodeGeneratorsSettings.Count);
            //Assert.AreEqual(2, vm2.Config.Model.GroupCatalogs[0].ListNodeGeneratorsSettings.Count);
            //Assert.AreEqual(2, vm2.Config.Model.GroupCatalogs[0].GroupProperties[0].ListNodeGeneratorsSettings.Count);
            //Assert.AreEqual(2, vm2.Config.Model.GroupCatalogs[0].GroupForms[0].ListNodeGeneratorsSettings.Count);
            //Assert.AreEqual(2, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].ListNodeGeneratorsSettings.Count);
            //Assert.AreEqual(2, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties[0].ListNodeGeneratorsSettings.Count);

            //// if app progect generator is removed, attached seetings are removed from appropriate nodes as well
            //gen2.NodeRemove(false);
            //Assert.AreEqual(1, vm2.Config.DicActiveAppProjectGenerators.Count);
            //Assert.AreEqual(1, vm2.Config.Model.GroupEnumerations[0].ListNodeGeneratorsSettings.Count);
            //Assert.AreEqual(1, vm2.Config.Model.GroupConstants[0].ListNodeGeneratorsSettings.Count);
            //Assert.AreEqual(1, vm2.Config.Model.GroupCatalogs[0].ListNodeGeneratorsSettings.Count);
            //Assert.AreEqual(1, vm2.Config.Model.GroupCatalogs[0].GroupProperties[0].ListNodeGeneratorsSettings.Count);
            //Assert.AreEqual(1, vm2.Config.Model.GroupCatalogs[0].GroupForms[0].ListNodeGeneratorsSettings.Count);
            //Assert.AreEqual(1, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].ListNodeGeneratorsSettings.Count);
            //Assert.AreEqual(1, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties[0].ListNodeGeneratorsSettings.Count);
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
            vm.Compose();
            var pluginNode = (from p in vm.Config.GroupPlugins.ListPlugins where p.VPlugin is vPlugin.Sample.SamplePlugin select p).Single();
            var genDb = (IvPluginDbGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
            var genDbAccess = (IvPluginGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbAccess select p).Single().Generator;

            var sln = (AppSolution)vm.Config.GroupAppSolutions.NodeAddNewSubNode();
            sln.RelativeAppSolutionPath = @"..\..\..\..\TestApps\Solution.sln";
            Assert.IsNull(sln.DynamicMainSettings);

            var prj = (AppProject)sln.NodeAddNewSubNode();
            prj.RelativeAppProjectPath = @"..\..\..\..\TestApps\ConsoleApp1\ConsoleApp1.csproj";

            var gen = (AppProjectGenerator)prj.NodeAddNewSubNode();
            gen.RelativePathToGenFolder = @"..\..\..\..\TestApps\ConsoleApp1\Generated";
            gen.GenFileName = "test_file.cs";
            gen.PluginGuid = pluginNode.Guid;
            gen.PluginGeneratorGuid = genDbAccess.Guid;
            gen.Name = "AppGenName";
            gen.NameUi = "App Gen Name";

            Assert.IsTrue(vm.Config.DicPlugins.ContainsKey(pluginNode.Guid));
            var plgn = vm.Config.DicPlugins[pluginNode.Guid];
            Assert.IsNotNull(plgn);
            Assert.IsTrue(sln.DicPluginsGroupSettings.ContainsKey(plgn.PluginGroupSolutionSettings.Guid));
            Assert.IsNotNull(sln.DynamicMainSettings);

            var set = (vPlugin.Sample.PluginsGroupSettings)sln.DicPluginsGroupSettings[plgn.PluginGroupSolutionSettings.Guid];
            set.IsGroupParam1 = true;

            vm.CommandConfigSave.Execute(null);

            // 2. When generator is removed, appropriate solution settings has to be removed if it is a last plugin in group
            gen.PluginGuid = null;
            Assert.IsTrue(vm.Config.DicPlugins.ContainsKey(pluginNode.Guid));
            Assert.IsFalse(sln.DicPluginsGroupSettings.ContainsKey(plgn.PluginGroupSolutionSettings.Guid));
            Assert.IsNull(sln.DynamicMainSettings);

            // 3. When new generator is added and it is new group plugin, than appropriate solution settings has to be added in solution
            gen.PluginGuid = pluginNode.Guid;
            Assert.IsTrue(vm.Config.DicPlugins.ContainsKey(pluginNode.Guid));
            plgn = vm.Config.DicPlugins[pluginNode.Guid];
            Assert.IsNotNull(plgn);
            Assert.IsTrue(sln.DicPluginsGroupSettings.ContainsKey(plgn.PluginGroupSolutionSettings.Guid));
            Assert.IsNotNull(sln.DynamicMainSettings);

            // 2. When generator is removed, appropriate solution settings has to be removed if it is a last plugin in group
            gen.NodeRemove(false);
            Assert.IsTrue(vm.Config.DicPlugins.ContainsKey(pluginNode.Guid));
            Assert.IsFalse(sln.DicPluginsGroupSettings.ContainsKey(plgn.PluginGroupSolutionSettings.Guid));
            Assert.IsNull(sln.DynamicMainSettings);


            // 1. When Config is loaded: init group plugin settings on all solution nodes
            var vm2 = new MainPageVM(true);
            vm2.OnFormLoaded();
            vm2.Compose();

            Assert.IsTrue(vm2.Config.DicPlugins.ContainsKey(pluginNode.Guid));
            plgn = vm2.Config.DicPlugins[pluginNode.Guid];
            Assert.IsNotNull(plgn);
            sln = vm2.Config.GroupAppSolutions[0];
            Assert.IsTrue(sln.DicPluginsGroupSettings.ContainsKey(plgn.PluginGroupSolutionSettings.Guid));
            Assert.IsNotNull(sln.DynamicMainSettings);

            // 4. When saving Config: convert all solutions groups settings to string representations
            set = (vPlugin.Sample.PluginsGroupSettings)sln.DicPluginsGroupSettings[plgn.PluginGroupSolutionSettings.Guid];
            Assert.IsTrue(set.IsGroupParam1);
            _logger.LogTrace("End test".CallerInfo());
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
            vm.Config.Name = "test1";
            var c1 = vm.Config.Model.GroupConstants.NodeAddNewSubNode();
            var c2 = vm.Config.Model.GroupConstants.NodeAddNewSubNode();
            vm.CommandConfigSaveAs.Execute(cfgPath);

            vm.Compose();
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

            var prms = (vPlugin.Sample.GeneratorDbAccessSettings)gen.DynamicMainSettings;
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
            TestTransformation tt = new TestTransformation();
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
    }
}
