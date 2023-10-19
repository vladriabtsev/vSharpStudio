using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.ViewModels;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.Unit
{
    //TODO Save buttons always enabled
    [TestClass]
    public class VmTests
    {
        static VmTests()
        {
            LoggerInit.Init();
        }
        private static Microsoft.Extensions.Logging.ILogger _logger;
        // public VmTests(ITestOutputHelper output)
        public VmTests()
        {
            //UIDispatcher.Initialize();

            VmBindable.isUnitTests = true;
            // ILoggerFactory loggerFactory = std.ApplicationLogging.LoggerFactory;
            // loggerFactory.AddProvider(new DebugLoggerProvider());
            // _logger = loggerFactory.CreateLogger<VmTests>();
            // _logger.LogInformation("======================  Start VmTests tests ===============================");
            if (_logger == null)
                //_logger = Logger.ServiceProvider.GetRequiredService<ILogger<PluginTests>>();
                _logger = Logger.CreateLogger<PluginTests>();
        }

        #region Editable

        [TestMethod]
        public void Editable011CanCancelDifferentLevelSimpleProperty()
        {
            var vm = MainPageVM.Create(false, MainPageVM.GetvSharpStudioPluginsPath());
            var cfg = vm.Config;
            cfg.Name = "test1";
            cfg.GroupAppSolutions.Name = "kuku1";
            //cfg.DbSettings.DbSchema = "schema1";
            cfg.BeginEdit();
            cfg.Name = "test2";
            cfg.GroupAppSolutions.Name = "kuku2";
            //cfg.DbSettings.DbSchema = "schema2";
            cfg.CancelEdit();
            Assert.IsTrue(cfg.Name == "test1");
            Assert.IsTrue(cfg.GroupAppSolutions.Name == "kuku1");
            //Assert.IsTrue(cfg.DbSettings.DbSchema == "schema1");
        }

        // [TestMethod]
        // public void Editable012CanCancelSameLevelNullable()
        // {
        //    Catalog vm = new Catalog
        //    {
        //    };
        //    var cfg = new Config();
        //    cfg.GroupCatalogs.Add(vm);
        //    vm.DbIdGenerator.IsPrimaryKeyClustered = true;
        //    vm.BeginEdit();
        //    vm.DbIdGenerator.IsPrimaryKeyClustered = false;
        //    vm.CancelEdit();
        //    Assert.IsTrue(vm.DbIdGenerator.IsPrimaryKeyClustered ?? false);
        // }
        [TestMethod]
        public void Editable013CanCancelSecondLevelSimpleProperty()
        {
            var vm = MainPageVM.Create(false, MainPageVM.GetvSharpStudioPluginsPath());
            var cfg = vm.Config;
            Catalog cat_vm = cfg.Model.GroupCatalogs.AddCatalog("test1");
            cat_vm.BeginEdit();
            cat_vm.Name = "test2";
            cat_vm.CancelEdit();
            Assert.IsTrue(cat_vm.Name == "test1");
        }

        [TestMethod]
        public void Editable014CanCancelSecondLevelCollection()
        {
            var mvm = MainPageVM.Create(false, MainPageVM.GetvSharpStudioPluginsPath());
            mvm.BtnNewConfig.Execute();
            mvm.BtnConfigSaveAs.Execute(@".\test.vcfg");

            var cfg = mvm.Config;
            Catalog vm = cfg.Model.GroupCatalogs.AddCatalog("test");
            var prop = vm.GroupProperties.AddProperty("test1");
            vm.BeginEdit();
            vm.GroupProperties[0].Name = "test2";
            vm.CancelEdit();
            Assert.IsTrue(vm.GroupProperties[0].Name == "test1");
            vm.BeginEdit();
            prop = vm.GroupProperties.AddProperty("test3");
            Assert.IsTrue(vm.GroupProperties.Count() == 2);
            vm.CancelEdit();
            Assert.IsTrue(vm.GroupProperties.Count() == 1);
            Assert.IsTrue(vm.GroupProperties[0].Name == "test1");
        }

        [TestMethod]
        public void Editable021CanCancelCatalogPropertiy()
        {
            var mvm = MainPageVM.Create(false, MainPageVM.GetvSharpStudioPluginsPath());
            //mvm.BtnNewConfig.Execute(@".\kuku.vcfg");
            mvm.BtnNewConfig.Execute();

            var cfg = mvm.Config;
            Catalog vm = cfg.Model.GroupCatalogs.AddCatalog();
            vm.BeginEdit();
            vm.GroupProperties.AddProperty("pdouble0", EnumDataType.NUMERICAL, 10, 0);
            vm.CancelEdit();
            Assert.IsTrue(vm.GroupProperties.Count() == 0);
            vm.GroupProperties.AddProperty("pdouble0", EnumDataType.NUMERICAL, 10, 0);
            vm.BeginEdit();
            vm.GroupProperties[0].DataType.DataTypeEnum = EnumDataType.STRING;
            vm.CancelEdit();
            Assert.IsTrue(vm.GroupProperties.Count() == 1);
            Assert.IsTrue(vm.GroupProperties[0].DataType.DataTypeEnum == EnumDataType.NUMERICAL);
            vm.BeginEdit();
            vm.GroupProperties.ListProperties.Clear();
            vm.CancelEdit();
            Assert.IsTrue(vm.GroupProperties.Count() == 1);
            Assert.IsTrue(vm.GroupProperties[0].DataType.DataTypeEnum == EnumDataType.NUMERICAL);
        }
        #endregion Editable

        #region Validatable
        [TestMethod]
        public void Validation001_ValidationCollectionEmptyAfterInit()
        {
            ConfigValidator.Reset();
            var vm = MainPageVM.Create(false, MainPageVM.GetvSharpStudioPluginsPath());
            var cfg = vm.Config;
            Assert.IsTrue(cfg.ValidationCollection != null);
            Assert.IsTrue(cfg.ValidationCollection.Count == 0);
        }

        [TestMethod]
        async public Task Validation002_CatalogValidationCollectionContainsValidationMessagesFromSubNodesForSelectedNode()
        {
            var cancellation = new CancellationTokenSource();
            var token = cancellation.Token;

            ConfigValidator.Reset();
            CatalogValidator.Reset();
            var vm = MainPageVM.Create(false, MainPageVM.GetvSharpStudioPluginsPath());
            var cfg = vm.Config;
            //cfg.SolutionPath = @"..\..\..\..\";

            var c = cfg.Model.GroupCatalogs.AddCatalog("test");
            Assert.IsTrue(c.Parent == cfg.Model.GroupCatalogs);

            string mes1 = "test error message";
            string mes2 = "test warning message";
            string mes22 = "test warning2 message";
            string mes3 = "test info message";

            CatalogValidator.Validator.RuleFor(x => x).Null().WithMessage(mes22).WithSeverity(Severity.Warning).WithState(x => SeverityWeight.VeryHigh);
            CatalogValidator.Validator.RuleFor(x => x).Null().WithMessage(mes1).WithSeverity(Severity.Error).WithState(x => SeverityWeight.VeryLow);
            CatalogValidator.Validator.RuleFor(x => x).Null().WithMessage(mes3).WithSeverity(Severity.Info).WithState(x => SeverityWeight.VeryHigh);
            CatalogValidator.Validator.RuleFor(x => x).Null().WithMessage(mes2).WithSeverity(Severity.Warning).WithState(x => SeverityWeight.VeryLow);

            cfg.Validate();

            await cfg.ValidateSubTreeFromNodeAsync(c, null, token, _logger);

            Assert.IsTrue(cfg.ValidationCollection.Count == 0);
            Assert.IsTrue(c.ValidationCollection.Count == 4);
            var p = c.ValidationCollection[0];
            Assert.IsTrue(p.Severity == FluentValidation.Severity.Error);
            Assert.IsTrue(p.Message == mes1);
            Assert.IsTrue(p.Model == c);
            p = c.ValidationCollection[1];
            Assert.IsTrue(p.Severity == FluentValidation.Severity.Warning);
            Assert.IsTrue(p.Message == mes22);
            Assert.IsTrue(p.Model == c);
            p = c.ValidationCollection[2];
            Assert.IsTrue(p.Severity == FluentValidation.Severity.Warning);
            Assert.IsTrue(p.Message == mes2);
            Assert.IsTrue(p.Model == c);
            p = c.ValidationCollection[3];
            Assert.IsTrue(p.Severity == FluentValidation.Severity.Info);
            Assert.IsTrue(p.Message == mes3);
            Assert.IsTrue(p.Model == c);

            Assert.AreEqual(1, c.CountErrors);
            Assert.AreEqual(2, c.CountWarnings);
            Assert.AreEqual(1, c.CountInfos);

            Assert.AreEqual(1, cfg.Model.GroupCatalogs.CountErrors);
            Assert.AreEqual(2, cfg.Model.GroupCatalogs.CountWarnings);
            Assert.AreEqual(1, cfg.Model.GroupCatalogs.CountInfos);

            Assert.AreEqual(1, cfg.CountErrors);
            Assert.AreEqual(2, cfg.CountWarnings);
            Assert.AreEqual(1, cfg.CountInfos);

            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.IsTrue(cfg.ValidationCollection.Count == 4);
            //await cfg.ValidateSubTreeFromNodeAsync(cfg, token);
            //Assert.IsTrue(cfg.ValidationCollection.Count == 4);
        }

        [TestMethod]
        async public Task Validation003_AppProjectGeneratorValidationCollectionContainsValidationMessagesFromSubNodesForSelectedNode()
        {
            var cancellation = new CancellationTokenSource();
            var token = cancellation.Token;

            ConfigValidator.Reset();
            CatalogValidator.Reset();
            var vm = MainPageVM.Create(false, MainPageVM.GetvSharpStudioPluginsPath());
            var cfg = vm.Config;
            cfg.CurrentCfgFolderPath = @".\";
            //cfg.SolutionPath = @"..\..\..\..\";

            var sol1 = cfg.GroupAppSolutions.AddAppSolution("sol1","./");
            var prj1 = sol1.AddAppProject("prj1", "./");
            var gen1 = prj1.AddGenerator("gen1", string.Empty, string.Empty, "");

            string mes1 = "test error message";
            string mes2 = "test warning message";
            string mes22 = "test warning2 message";
            string mes3 = "test info message";

            //AppProjectGeneratorValidator.Validator.RuleFor(x => x).Null().WithMessage(mes22).WithSeverity(Severity.Warning).WithState(x => SeverityWeight.VeryHigh);
            //AppProjectGeneratorValidator.Validator.RuleFor(x => x).Null().WithMessage(mes1).WithSeverity(Severity.Error).WithState(x => SeverityWeight.VeryLow);
            //AppProjectGeneratorValidator.Validator.RuleFor(x => x).Null().WithMessage(mes3).WithSeverity(Severity.Info).WithState(x => SeverityWeight.VeryHigh);
            //AppProjectGeneratorValidator.Validator.RuleFor(x => x).Null().WithMessage(mes2).WithSeverity(Severity.Warning).WithState(x => SeverityWeight.VeryLow);

            cfg.Validate();

            await cfg.ValidateSubTreeFromNodeAsync(sol1, null, token);
            Assert.IsTrue(cfg.ValidationCollection.Count == 0);
            Assert.IsTrue(sol1.ValidationCollection.Count == 7);
            //var p = sol1.ValidationCollection[0];
            //Assert.IsTrue(p.Severity == FluentValidation.Severity.Error);
            //Assert.IsTrue(p.Message == mes1);
            //Assert.IsTrue(p.Model == sol1);
            //p = sol1.ValidationCollection[1];
            //Assert.IsTrue(p.Severity == FluentValidation.Severity.Warning);
            //Assert.IsTrue(p.Message == mes22);
            //Assert.IsTrue(p.Model == sol1);
            //p = sol1.ValidationCollection[2];
            //Assert.IsTrue(p.Severity == FluentValidation.Severity.Warning);
            //Assert.IsTrue(p.Message == mes2);
            //Assert.IsTrue(p.Model == sol1);
            //p = sol1.ValidationCollection[3];
            //Assert.IsTrue(p.Severity == FluentValidation.Severity.Info);
            //Assert.IsTrue(p.Message == mes3);
            //Assert.IsTrue(p.Model == sol1);

            //Assert.AreEqual(1, sol1.CountErrors);
            //Assert.AreEqual(2, sol1.CountWarnings);
            //Assert.AreEqual(1, sol1.CountInfos);

            //Assert.AreEqual(1, cfg.Model.GroupCatalogs.CountErrors);
            //Assert.AreEqual(2, cfg.Model.GroupCatalogs.CountWarnings);
            //Assert.AreEqual(1, cfg.Model.GroupCatalogs.CountInfos);

            //Assert.AreEqual(1, cfg.CountErrors);
            //Assert.AreEqual(2, cfg.CountWarnings);
            //Assert.AreEqual(1, cfg.CountInfos);

            //cfg.ValidateSubTreeFromNode(cfg, _logger); // .ConfigureAwait(continueOnCapturedContext: false);
            //Assert.IsTrue(cfg.ValidationCollection.Count == 4);
        }
        [TestMethod]
        async public Task Validation007_Propagation()
        {
            var cancellation = new CancellationTokenSource();
            var token = cancellation.Token;

            ConfigValidator.Reset();
            var vm = MainPageVM.Create(false, MainPageVM.GetvSharpStudioPluginsPath());
            var cfg = vm.Config;
            //cfg.SolutionPath = @"..\..\..\..\";

            string mes1 = "test error message";
            string mes2 = "test error message2";

            var gr = cfg.Model.GroupConstantGroups.AddGroupConstants("Gr");
            gr.NodeAddNewSubNode();
            ConstantValidator.Validator.RuleFor(x => x).Null().WithMessage(mes1).WithSeverity(Severity.Error).WithState(x => SeverityWeight.Normal);

            await cfg.ValidateSubTreeFromNodeAsync(cfg.Model.GroupConstantGroups, null, token);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].ValidationCollection.Count == 1);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].CountErrors == 1);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].CountErrors == 1);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ValidationCollection.Count == 1);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.CountErrors == 1);
            Assert.IsTrue(cfg.ValidationCollection.Count == 0);
            Assert.IsTrue(cfg.CountErrors == 1);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);

            cfg.Model.GroupEnumerations.NodeAddNewSubNode();
            EnumerationValidator.Validator.RuleFor(x => x).Null().WithMessage(mes2).WithSeverity(Severity.Error).WithState(x => SeverityWeight.Low);

            await cfg.ValidateSubTreeFromNodeAsync(cfg.Model.GroupEnumerations, null, token);
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].ValidationCollection.Count == 1);
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].CountErrors == 1);
            Assert.IsTrue(cfg.Model.GroupEnumerations.ValidationCollection.Count == 1);
            Assert.IsTrue(cfg.Model.GroupEnumerations.CountErrors == 1);
            Assert.IsTrue(cfg.ValidationCollection.Count == 0);
            Assert.IsTrue(cfg.CountErrors == 2);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);

            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.IsTrue(cfg.ValidationCollection.Count == 2);
            Assert.IsTrue(cfg.CountErrors == 2);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
        }
        #endregion Validatable

        #region Unique position for Protobuf
        [TestMethod]
        public void Property001_Position()
        {
            var mvm = MainPageVM.Create(false, MainPageVM.GetvSharpStudioPluginsPath());
            //mvm.BtnNewConfig.Execute(@".\kuku.vcfg");
            mvm.BtnNewConfig.Execute();

            var cfg = mvm.Config;
            int catPos = 14;
            cfg.Model.GroupCatalogs.NodeAddNewSubNode();
            cfg.Model.GroupCatalogs[0].GroupProperties.NodeAddNewSubNode();
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[0].Position == catPos);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties.LastGenPosition == catPos);
            cfg.Model.GroupCatalogs[0].GroupProperties.NodeAddNewSubNode();
            catPos++;
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[1].Position == catPos);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties.LastGenPosition == catPos);
            cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeRemove();
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[0].Position == catPos);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties.LastGenPosition == catPos);
            cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeAddNew();
            catPos++;
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[1].Position == catPos);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties.LastGenPosition == catPos);
        }
        [TestMethod]
        public void Register001_Property_Position()
        {
            var mvm = MainPageVM.Create(false, MainPageVM.GetvSharpStudioPluginsPath());
            //mvm.BtnNewConfig.Execute(@".\kuku.vcfg");
            mvm.BtnNewConfig.Execute();

            var cfg = mvm.Config;
            uint pos = 20;
            var reg = (IRegister)cfg.Model.GroupListRegisters.NodeAddNewSubNode();
            Assert.AreEqual(pos, reg.LastGenPosition);

            var dim = (IRegisterDimension)reg.GroupRegisterDimensions.NodeAddNewSubNode();
            pos++;
            Assert.AreEqual(pos, reg.LastGenPosition);
            Assert.AreEqual(pos, dim.Position);

            var cat = cfg.Model.GroupCatalogs.AddCatalog("test_cat");
            dim = reg.AddDimension("test_dim", cat);
            pos++;
            Assert.AreEqual(pos, reg.LastGenPosition);
            Assert.AreEqual(pos, dim.Position);

            dim = (IRegisterDimension)reg.GroupRegisterDimensions.NodeAddNewSubNode();
            pos++;
            Assert.AreEqual(pos, reg.LastGenPosition);
            Assert.AreEqual(pos, dim.Position);

            var prop = (IProperty)reg.GroupAttachedProperties.NodeAddNewSubNode();
            pos++;
            Assert.AreEqual(pos, reg.LastGenPosition);
            Assert.AreEqual(pos, prop.Position);

            dim = (IRegisterDimension)reg.GroupRegisterDimensions.NodeAddNewSubNode();
            pos++;
            Assert.AreEqual(pos, reg.LastGenPosition);
            Assert.AreEqual(pos, dim.Position);

            prop = (IProperty)reg.AddAttachedProperty("test_prop");
            pos++;
            Assert.AreEqual(pos, reg.LastGenPosition);
            Assert.AreEqual(pos, prop.Position);
        }
        #endregion Unique position for Protobuf

        [TestMethod]
        public void Register002_Validation()
        {
            var mvm = MainPageVM.Create(false, MainPageVM.GetvSharpStudioPluginsPath());
            //mvm.BtnNewConfig.Execute(@".\kuku.vcfg");
            mvm.BtnNewConfig.Execute();

            var cfg = mvm.Config;
            var reg = (IRegister)cfg.Model.GroupListRegisters.NodeAddNewSubNode();
            reg.Validate();
            Assert.AreEqual(2, reg.ValidationCollection.Count);
            reg.ValidationCollection.Single(m => m.Message.StartsWith("Register dimentions are not selected"));
            reg.ValidationCollection.Single(m => m.Message.StartsWith("List of Document types for Register is empty"));

            var dim = (IRegisterDimension)reg.GroupRegisterDimensions.NodeAddNewSubNode();
            Assert.AreEqual(1, reg.GroupRegisterDimensions.ListDimensions.Count);
            reg.Validate();
            Assert.AreEqual(1, reg.ValidationCollection.Count);
            reg.ValidationCollection.Single(m => m.Message.StartsWith("List of Document types for Register is empty"));

            mvm.BtnConfigSaveAs.Execute(@".\test.vcfg");
            mvm = MainPageVM.Create(true, MainPageVM.GetvSharpStudioPluginsPath());
            Assert.AreEqual(1, mvm.Config.Model.GroupListRegisters.ListRegisters.Count);
            Assert.AreEqual(1, mvm.Config.Model.GroupListRegisters.ListRegisters[0].GroupRegisterDimensions.ListDimensions.Count);
            reg = mvm.Config.Model.GroupListRegisters.ListRegisters[0];
            reg.Validate();
            Assert.AreEqual(1, reg.ValidationCollection.Count);
            reg.ValidationCollection.Single(m => m.Message.StartsWith("List of Document types for Register is empty"));
        }

        //#region OnAdded in parent
        //[TestMethod]
        //public void OnAdded001_NotifyObjectWasAddedToParentCollection()
        //{
        //    var cfg = new Config();
        //    cfg.Model.GroupCatalogs.NodeAddNewSubNode();
        //    cfg.Model.GroupCatalogs[0].GroupProperties.NodeAddNewSubNode();
        //    Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[0].Position == 2);
        //    Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties.LastGenPosition == 2);
        //    cfg.Model.GroupCatalogs[0].GroupProperties.NodeAddNewSubNode();
        //    Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[1].Position == 3);
        //    Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties.LastGenPosition == 3);
        //    cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeRemove();
        //    Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[0].Position == 3);
        //    Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties.LastGenPosition == 3);
        //    cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeAddNew();
        //    Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[1].Position == 4);
        //    Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties.LastGenPosition == 4);
        //}
        //#endregion OnAdded in parent
        [TestMethod]
        public void OnUIThread_001_Position()
        {
            UIDispatcher.Invoke(() =>
            {
            });
        }
    }
}
