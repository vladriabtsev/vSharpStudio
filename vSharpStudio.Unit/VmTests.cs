using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.ViewModels;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.Unit
{
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
            ViewModelBindable.isUnitTests = true;
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
            var cfg = new Config();
            cfg.Name = "test1";
            cfg.DbSettings.DbSchema = "schema1";
            cfg.BeginEdit();
            cfg.Name = "test2";
            cfg.DbSettings.DbSchema = "schema2";
            cfg.CancelEdit();
            Assert.IsTrue(cfg.Name == "test1");
            Assert.IsTrue(cfg.DbSettings.DbSchema == "schema1");
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
            var cfg = new Config();
            Catalog vm = cfg.Model.GroupCatalogs.AddCatalog("test1");
            vm.BeginEdit();
            vm.Name = "test2";
            vm.CancelEdit();
            Assert.IsTrue(vm.Name == "test1");
        }

        [TestMethod]
        public void Editable014CanCancelSecondLevelCollection()
        {
            var cfg = new Config();
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
            var cfg = new Config();
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
            Config.ConfigValidator.Reset();
            var cfg = new Config();
            Assert.IsTrue(cfg.ValidationCollection != null);
            Assert.IsTrue(cfg.ValidationCollection.Count == 0);
        }

        [TestMethod]
        public void Validation002_ValidationCollectionContainsValidationMessagesFromSubNodesForSelectedNode()
        {
            Config.ConfigValidator.Reset();
            Catalog.CatalogValidator.Reset();
            var cfg = new Config();
            cfg.SolutionPath = @"..\..\..\..\";

            var c = cfg.Model.GroupCatalogs.AddCatalog("test");
            Assert.IsTrue(c.Parent == cfg.Model.GroupCatalogs);

            string mes1 = "test error message";
            string mes2 = "test warning message";
            string mes22 = "test warning2 message";
            string mes3 = "test info message";

            Catalog.CatalogValidator.Validator.RuleFor(x => x).Null().WithMessage(mes22).WithSeverity(Severity.Warning).WithState(x => SeverityWeight.VeryHigh);
            Catalog.CatalogValidator.Validator.RuleFor(x => x).Null().WithMessage(mes1).WithSeverity(Severity.Error).WithState(x => SeverityWeight.VeryLow);
            Catalog.CatalogValidator.Validator.RuleFor(x => x).Null().WithMessage(mes3).WithSeverity(Severity.Info).WithState(x => SeverityWeight.VeryHigh);
            Catalog.CatalogValidator.Validator.RuleFor(x => x).Null().WithMessage(mes2).WithSeverity(Severity.Warning).WithState(x => SeverityWeight.VeryLow);

            cfg.Validate();

            cfg.ValidateSubTreeFromNode(c, _logger); // .ConfigureAwait(continueOnCapturedContext: false);

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

            cfg.ValidateSubTreeFromNode(cfg, _logger); // .ConfigureAwait(continueOnCapturedContext: false);
            Assert.IsTrue(cfg.ValidationCollection.Count == 4);
        }

        [TestMethod]
        public void Validation007_Propagation()
        {
            Config.ConfigValidator.Reset();
            var cfg = new Config();
            cfg.SolutionPath = @"..\..\..\..\";

            string mes1 = "test error message";
            string mes2 = "test error message2";

            cfg.Model.GroupConstants.NodeAddNewSubNode();
            Constant.ConstantValidator.Validator.RuleFor(x => x).Null().WithMessage(mes1).WithSeverity(Severity.Error).WithState(x => SeverityWeight.Normal);
            cfg.ValidateSubTreeFromNode(cfg.Model.GroupConstants);
            Assert.IsTrue(cfg.Model.GroupConstants[0].ValidationCollection.Count == 1);
            Assert.IsTrue(cfg.Model.GroupConstants[0].CountErrors == 1);
            Assert.IsTrue(cfg.Model.GroupConstants.ValidationCollection.Count == 1);
            Assert.IsTrue(cfg.Model.GroupConstants.CountErrors == 1);
            Assert.IsTrue(cfg.ValidationCollection.Count == 0);
            Assert.IsTrue(cfg.CountErrors == 1);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);

            cfg.Model.GroupEnumerations.NodeAddNewSubNode();
            Enumeration.EnumerationValidator.Validator.RuleFor(x => x).Null().WithMessage(mes2).WithSeverity(Severity.Error).WithState(x => SeverityWeight.Low);
            cfg.ValidateSubTreeFromNode(cfg.Model.GroupEnumerations);
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].ValidationCollection.Count == 1);
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].CountErrors == 1);
            Assert.IsTrue(cfg.Model.GroupEnumerations.ValidationCollection.Count == 1);
            Assert.IsTrue(cfg.Model.GroupEnumerations.CountErrors == 1);
            Assert.IsTrue(cfg.ValidationCollection.Count == 0);
            Assert.IsTrue(cfg.CountErrors == 2);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);

            cfg.ValidateSubTreeFromNode(cfg);
            Assert.IsTrue(cfg.ValidationCollection.Count == 2);
            Assert.IsTrue(cfg.CountErrors == 2);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
        }
        #endregion Validatable

        #region Property unique position for Protobuf
        [TestMethod]
        public void Property001_Position()
        {
            var cfg = new Config();
            cfg.Model.GroupCatalogs.NodeAddNewSubNode();
            cfg.Model.GroupCatalogs[0].GroupProperties.NodeAddNewSubNode();
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[0].Position == 2);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties.LastGenPosition == 2);
            cfg.Model.GroupCatalogs[0].GroupProperties.NodeAddNewSubNode();
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[1].Position == 3);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties.LastGenPosition == 3);
            cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeRemove();
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[0].Position == 3);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties.LastGenPosition == 3);
            cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeAddNew();
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[1].Position == 4);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties.LastGenPosition == 4);
        }
        #endregion Property unique position for Protobuf
    }
}
