using System;
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
        }
        private void remove_config()
        {
            if (File.Exists(MainPageVM.CFG_PATH))
                File.Delete(MainPageVM.CFG_PATH);
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
            vm.Model.GroupConstants.NodeAddNewSubNode();
            var cnst = (Constant)vm.Model.SelectedNode;
            var ct = DateTime.UtcNow;
            vm.CommandConfigSave.Execute(null);
            Assert.IsTrue(vm.Model.LastUpdated != null);
            Assert.IsTrue(ct <= vm.Model.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Model.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.IsTrue(vm.Model.Version == 0);

            // reload
            vm = new MainPageVM(true);
            Assert.IsTrue(vm.Model.GroupConstants.ListConstants.Count == 1);
            Assert.IsTrue(vm.Model.GroupConstants.ListConstants[0].Name == cnst.Name);
            Assert.IsTrue(ct <= vm.Model.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Model.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.IsTrue(vm.Model.Version == 0);
            Assert.IsTrue(vm.pconfig_history != null);
            Assert.IsTrue(vm.pconfig_history.CurrentConfig != null);
            Assert.IsTrue(vm.pconfig_history.PrevStableConfig == null);
            Assert.IsTrue(vm.pconfig_history.OldStableConfig == null);

            // create stable version
            vm.CommandConfigCreateStableVersion.Execute(null);
            vm = new MainPageVM(true);
            Assert.IsTrue(vm.Model.GroupConstants.ListConstants.Count == 1);
            Assert.IsTrue(vm.Model.GroupConstants.ListConstants[0].Name == cnst.Name);
            Assert.IsTrue(ct <= vm.Model.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Model.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.IsTrue(vm.Model.Version == 1);
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
            Assert.IsTrue(vm.Model.GroupConstants.ListConstants.Count == 1);
            Assert.IsTrue(vm.Model.GroupConstants.ListConstants[0].Name == cnst.Name);
            Assert.IsTrue(ct <= vm.Model.LastUpdated.ToDateTime());
            Assert.IsTrue(vm.Model.LastUpdated.ToDateTime() <= DateTime.UtcNow);
            Assert.IsTrue(vm.Model.Version == 2);
            Assert.IsTrue(vm.pconfig_history != null);
            Assert.IsTrue(vm.pconfig_history.CurrentConfig != null);
            Assert.IsTrue(vm.pconfig_history.PrevStableConfig != null);
            Assert.IsTrue(vm.pconfig_history.PrevStableConfig.Version == 1);
            Assert.IsTrue(vm.pconfig_history.OldStableConfig != null);
            Assert.IsTrue(vm.pconfig_history.OldStableConfig.Version == 0);
            // old migration code is kept?
            //Assert.IsTrue(false);
        }
    }
}
