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
        public void Plugin002WorkWithGlobalSettings()
        {
            var vm = new MainPageVM(false);
            vm.Compose();
            var pluginNode = (from p in vm.Config.GroupPlugins.ListPlugins where p.VPlugin is vPlugin.Sample.SamplePlugin select p).Single();
            var genDb = (IvPluginDbGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;
            var genDbAccess = (IvPluginGenerator)(from p in pluginNode.ListGenerators where p.Generator is vPlugin.Sample.GeneratorDbSchema select p).Single().Generator;

            string solDir = Directory.GetCurrentDirectory() + "TestSolution";
            if (Directory.Exists(solDir))
                Directory.Delete(solDir, true);
            vm.Config.GroupAppSolutions.NodeAddNewSubNode();
            vm.Config.GroupAppSolutions[0].RelativeAppSolutionPath = solDir;
            //vm.Config.GroupAppSolutions[0].IPluginsGroup1.
            vm.Config.GroupAppSolutions[0].NodeAddNewSubNode();
            vm.Config.GroupAppSolutions[0].ListAppProjects[0].RelativeAppProjectPath = "Project1";
            vm.Config.GroupAppSolutions[0].ListAppProjects[0].NodeAddNewSubNode();
            vm.Config.GroupAppSolutions[0].ListAppProjects[0].ListAppProjectGenerators[0].RelativePathToGeneratedFile="Generated\\test_file.cs";
        }
    }
}
