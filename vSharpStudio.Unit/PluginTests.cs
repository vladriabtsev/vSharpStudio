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
            vm.Config.Model.GroupCatalogs.NodeAddNewSubNode();
            var cat = vm.Config.Model.GroupCatalogs[0];
            cat.GroupProperties.NodeAddNewSubNode();
            var prop = cat.GroupProperties[0];
            Assert.IsTrue(ConfigObjectSubBase<Property, Property.PropertyValidator>.SearchInModelPathByPattern(prop.ModelPath, "Property"));
            Assert.IsTrue(ConfigObjectSubBase<Property, Property.PropertyValidator>.SearchInModelPathByPattern(prop.ModelPath, "Test;Property"));
            Assert.IsTrue(ConfigObjectSubBase<Property, Property.PropertyValidator>.SearchInModelPathByPattern(prop.ModelPath, "Catalog.*.Property"));

            Assert.IsFalse(ConfigObjectSubBase<Property, Property.PropertyValidator>.SearchInModelPathByPattern(prop.ModelPath, "Catalog"));
            Assert.IsFalse(ConfigObjectSubBase<Property, Property.PropertyValidator>.SearchInModelPathByPattern(prop.ModelPath, "Catalog.*.Test"));
            Assert.IsFalse(ConfigObjectSubBase<Property, Property.PropertyValidator>.SearchInModelPathByPattern(prop.ModelPath, "Test.*.Property"));
        }
        [TestMethod]
        public void Plugin003CanLoadPlugin()
        {
            _logger.LogTrace("Start test".CallerInfo());
            var vm = new MainPageVM(false);
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
            vm.Compose();
            var pluginNode = (from p in vm.Config.GroupPlugins.ListPlugins where p.VPlugin is vPlugin.Sample.SamplePlugin select p).Single();
            var genDb = (IvPluginDbGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
            var genDbAccess = (IvPluginGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbAccess select p).Single().Generator;

            string solDir = Directory.GetCurrentDirectory() + "TestSolution";
            if (Directory.Exists(solDir))
                Directory.Delete(solDir, true);
            Directory.CreateDirectory(solDir);
            vm.Config.GroupAppSolutions.NodeAddNewSubNode();
            vm.Config.GroupAppSolutions[0].RelativeAppSolutionPath = solDir;
            vm.Config.GroupAppSolutions[0].NodeAddNewSubNode();
            string prjFolder = "Project1";
            vm.Config.GroupAppSolutions[0].ListAppProjects[0].RelativeAppProjectPath = prjFolder;
            Directory.CreateDirectory(solDir + "\\" + prjFolder);
            vm.Config.GroupAppSolutions[0].ListAppProjects[0].NodeAddNewSubNode();
            var gen = vm.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0];
            string genFolder = "Generated";
            gen.RelativePathToGeneratedFile = genFolder + "\\test_file.cs";
            gen.PluginGuid = pluginNode.Guid;
            gen.PluginGeneratorGuid = genDbAccess.Guid;
            var prms = (vPlugin.Sample.GeneratorDbAccessSettings)gen.Settings;
            prms.IsAccessParam1 = true;
            prms.IsAccessParam2 = false;
            prms.AccessParam3 = "test";
            vm.CommandConfigSave.Execute(null);

            var vm2 = new MainPageVM(true);
            vm2.Compose();
            Assert.AreEqual(1, vm2.Config.GroupAppSolutions.Count());
            Assert.AreEqual(solDir, vm2.Config.GroupAppSolutions[0].RelativeAppSolutionPath);
            Assert.AreEqual(1, vm2.Config.GroupAppSolutions[0].ListAppProjects.Count());
            Assert.AreEqual(1, vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators.Count());
            var gen2 = vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0];
            Assert.AreEqual(gen.RelativePathToGeneratedFile, gen2.RelativePathToGeneratedFile);
            Assert.AreEqual(gen.PluginGuid, gen2.PluginGuid);
            Assert.AreEqual(gen.PluginGeneratorGuid, gen2.PluginGeneratorGuid);
            Assert.IsNotNull(gen2.Settings);
            var prms2 = (vPlugin.Sample.GeneratorDbAccessSettings)gen2.Settings;
            Assert.AreEqual(prms.IsAccessParam1, prms2.IsAccessParam1);
            Assert.AreEqual(prms.IsAccessParam2, prms2.IsAccessParam2);
            Assert.AreEqual(prms.AccessParam3, prms2.AccessParam3);
            _logger.LogTrace("End test".CallerInfo());
        }
        [TestMethod]
        public void Plugin005WorkWithNodeGeneratorSettings()
        {
            _logger.LogTrace("Start test".CallerInfo());
            var vm = new MainPageVM(false);
            vm.Compose();
            var pluginNode = (from p in vm.Config.GroupPlugins.ListPlugins where p.VPlugin is vPlugin.Sample.SamplePlugin select p).Single();
            var genDb = (IvPluginDbGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
            var genDbAccess = (IvPluginGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbAccess select p).Single().Generator;

            string solDir = Directory.GetCurrentDirectory() + "TestSolution";
            if (Directory.Exists(solDir))
                Directory.Delete(solDir, true);
            Directory.CreateDirectory(solDir);
            vm.Config.GroupAppSolutions.NodeAddNewSubNode();
            vm.Config.GroupAppSolutions[0].RelativeAppSolutionPath = solDir;
            vm.Config.GroupAppSolutions[0].NodeAddNewSubNode();
            string prjFolder = "Project1";
            vm.Config.GroupAppSolutions[0].ListAppProjects[0].RelativeAppProjectPath = prjFolder;
            Directory.CreateDirectory(solDir + "\\" + prjFolder);
            Assert.AreEqual(0, vm.Config.DicAppGenerators.Count);
            vm.Config.GroupAppSolutions[0].ListAppProjects[0].NodeAddNewSubNode();
            var appGen = vm.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0];
            appGen.Name = "AppGenName";
            appGen.NameUi = "App Gen Name";
            string genFolder = "Generated";
            appGen.RelativePathToGeneratedFile = genFolder + "\\test_file.cs";
            appGen.PluginGuid = pluginNode.Guid;
            // Expect attached settings for Property and Catalog.Form
            appGen.PluginGeneratorGuid = genDbAccess.Guid;
            Assert.AreEqual(1, vm.Config.DicAppGenerators.Count);

            vm.Config.Model.GroupEnumerations.NodeAddNewSubNode();
            Assert.AreEqual(0, vm.Config.Model.GroupEnumerations[0].ListGeneratorsSettings.Count);
            vm.Config.Model.GroupConstants.NodeAddNewSubNode();
            Assert.AreEqual(0, vm.Config.Model.GroupConstants[0].ListGeneratorsSettings.Count);
            vm.Config.Model.GroupCatalogs.NodeAddNewSubNode();
            Assert.AreEqual(0, vm.Config.Model.GroupCatalogs[0].ListGeneratorsSettings.Count);

            // if new node is added, settings are attached to new node
            vm.Config.Model.GroupCatalogs[0].GroupProperties.NodeAddNewSubNode();
            Assert.AreEqual(1, vm.Config.Model.GroupCatalogs[0].GroupProperties[0].ListGeneratorsSettings.Count);
            var stt = vm.Config.Model.GroupCatalogs[0].GroupProperties[0].ListGeneratorsSettings[0];
            Assert.AreEqual(appGen.Guid, stt.AppGeneratorGuid);
            foreach (var t in genDbAccess.GetListNodeGenerationSettings())
            {
                if (t.SearchPathInModel == "Property")
                    Assert.AreEqual(t.Guid, stt.NodeSettingsVmGuid);
            }
            Assert.AreEqual(appGen.Name, stt.Name);
            Assert.AreEqual(appGen.NameUi, stt.NameUi);
            vm.Config.Model.GroupCatalogs[0].GroupForms.NodeAddNewSubNode();
            Assert.AreEqual(1, vm.Config.Model.GroupCatalogs[0].GroupForms[0].ListGeneratorsSettings.Count);

            vm.Config.Model.GroupDocuments.GroupListDocuments.NodeAddNewSubNode();
            Assert.AreEqual(0, vm.Config.Model.GroupDocuments.GroupListDocuments[0].ListGeneratorsSettings.Count);

            // if new node is added, settings are attached to new node
            vm.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties.NodeAddNewSubNode();
            Assert.AreEqual(1, vm.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties[0].ListGeneratorsSettings.Count);

            vm.CommandConfigSave.Execute(null);
            Assert.AreEqual(1, vm.Config.DicAppGenerators.Count);

            var vm2 = new MainPageVM(true);
            vm2.Compose();

            Assert.AreEqual(1, vm2.Config.DicAppGenerators.Count);
            // node seeting are attached for appropriate nodes for appropriate setting
            Assert.AreEqual(0, vm2.Config.Model.GroupEnumerations[0].ListGeneratorsSettings.Count);
            Assert.AreEqual(0, vm2.Config.Model.GroupConstants[0].ListGeneratorsSettings.Count);
            Assert.AreEqual(0, vm2.Config.Model.GroupCatalogs[0].ListGeneratorsSettings.Count);
            Assert.AreEqual(1, vm2.Config.Model.GroupCatalogs[0].GroupProperties[0].ListGeneratorsSettings.Count);
            Assert.AreEqual(1, vm2.Config.Model.GroupCatalogs[0].GroupForms[0].ListGeneratorsSettings.Count);
            Assert.AreEqual(0, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].ListGeneratorsSettings.Count);
            Assert.AreEqual(1, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties[0].ListGeneratorsSettings.Count);


            // if new app progect generator is added, new setting are attached to all appropriate nodes
            var gen0 = vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0];
            Assert.AreEqual(1, vm2.Config.DicAppGenerators.Count);
            vm2.Config.GroupAppSolutions[0].ListAppProjects[0].NodeAddNewSubNode();
            appGen = (from p in vm2.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators where p.Guid != gen0.Guid select p).Single();
            appGen.RelativePathToGeneratedFile = genFolder + "\\test_file2.cs";
            appGen.PluginGuid = pluginNode.Guid;
            // Expect attached settings for Property and Catalog.Form
            appGen.PluginGeneratorGuid = genDbAccess.Guid;
            Assert.AreEqual(2, vm2.Config.DicAppGenerators.Count);
            Assert.AreEqual(0, vm2.Config.Model.GroupEnumerations[0].ListGeneratorsSettings.Count);
            Assert.AreEqual(0, vm2.Config.Model.GroupConstants[0].ListGeneratorsSettings.Count);
            Assert.AreEqual(0, vm2.Config.Model.GroupCatalogs[0].ListGeneratorsSettings.Count);
            Assert.AreEqual(2, vm2.Config.Model.GroupCatalogs[0].GroupProperties[0].ListGeneratorsSettings.Count);
            Assert.AreEqual(2, vm2.Config.Model.GroupCatalogs[0].GroupForms[0].ListGeneratorsSettings.Count);
            Assert.AreEqual(0, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].ListGeneratorsSettings.Count);
            Assert.AreEqual(2, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties[0].ListGeneratorsSettings.Count);

            // if app progect generator is removed, attached seetings are removed from appropriate nodes as well
            appGen.NodeRemove();
            Assert.AreEqual(1, vm2.Config.DicAppGenerators.Count);
            Assert.AreEqual(0, vm2.Config.Model.GroupEnumerations[0].ListGeneratorsSettings.Count);
            Assert.AreEqual(0, vm2.Config.Model.GroupConstants[0].ListGeneratorsSettings.Count);
            Assert.AreEqual(0, vm2.Config.Model.GroupCatalogs[0].ListGeneratorsSettings.Count);
            Assert.AreEqual(1, vm2.Config.Model.GroupCatalogs[0].GroupProperties[0].ListGeneratorsSettings.Count);
            Assert.AreEqual(1, vm2.Config.Model.GroupCatalogs[0].GroupForms[0].ListGeneratorsSettings.Count);
            Assert.AreEqual(0, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].ListGeneratorsSettings.Count);
            Assert.AreEqual(1, vm2.Config.Model.GroupDocuments.GroupListDocuments[0].GroupProperties[0].ListGeneratorsSettings.Count);
            _logger.LogTrace("End test".CallerInfo());
        }
    }
}
