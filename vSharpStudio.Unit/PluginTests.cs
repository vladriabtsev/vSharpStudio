using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vSharpStudio.ViewModels;
using vSharpStudio.common;
using System.IO;

namespace vSharpStudio.Unit
{
    [TestClass]
    public class PluginTests
    {
        [TestMethod]
        public void Plugin001CanLoadPlugin()
        {
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
        }
        [TestMethod]
        public void Plugin002WorkWithGeneratorSettings()
        {
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
        }
    }
}
