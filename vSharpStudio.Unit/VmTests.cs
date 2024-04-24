using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Threading;
using ApplicationLogging;
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
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
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
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
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
            var mvm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
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
            var mvm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
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
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
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
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
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

            await cfg.ValidateSubTreeFromNodeAsync(c, null, token);

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
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            var cfg = vm.Config;
            cfg.CurrentCfgFolderPath = @".\";
            //cfg.SolutionPath = @"..\..\..\..\";

            var sol1 = cfg.GroupAppSolutions.AddAppSolution("sol1", "./");
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
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
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
            var mvm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            //mvm.BtnNewConfig.Execute(@".\kuku.vcfg");
            mvm.BtnNewConfig.Execute();

            var cfg = mvm.Config;
            int catPos = 21;
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
            var mvm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            //mvm.BtnNewConfig.Execute(@".\kuku.vcfg");
            mvm.BtnNewConfig.Execute();

            var cfg = mvm.Config;
            uint pos = 20;
            var reg = (Register)cfg.Model.GroupDocuments.GroupRegisters.NodeAddNewSubNode();

            var dim = (IRegisterDimension)reg.GroupRegisterDimensions.NodeAddNewSubNode();
            pos++;
            Assert.AreEqual(pos, reg.GroupProperties.LastGenPosition);
            Assert.AreEqual(pos, dim.Position);

            var cat = cfg.Model.GroupCatalogs.AddCatalog("test_cat");
            dim = reg.AddDimension("test_dim", cat);
            pos++;
            Assert.AreEqual(pos, reg.GroupProperties.LastGenPosition);
            Assert.AreEqual(pos, dim.Position);

            dim = (IRegisterDimension)reg.GroupRegisterDimensions.NodeAddNewSubNode();
            pos++;
            Assert.AreEqual(pos, reg.GroupProperties.LastGenPosition);
            Assert.AreEqual(pos, dim.Position);

            var prop = (IProperty)reg.GroupProperties.NodeAddNewSubNode();
            pos++;
            Assert.AreEqual(pos, reg.GroupProperties.LastGenPosition);
            Assert.AreEqual(pos, prop.Position);

            dim = (IRegisterDimension)reg.GroupRegisterDimensions.NodeAddNewSubNode();
            pos++;
            Assert.AreEqual(pos, reg.GroupProperties.LastGenPosition);
            Assert.AreEqual(pos, dim.Position);

            prop = (IProperty)reg.AddAttachedProperty("test_prop");
            pos++;
            Assert.AreEqual(pos, reg.GroupProperties.LastGenPosition);
            Assert.AreEqual(pos, prop.Position);
        }
        #endregion Unique position for Protobuf

        [TestMethod]
        async public System.Threading.Tasks.Task Register002_Validation()
        {
            var cancellation = new CancellationTokenSource();
            var token = cancellation.Token;

            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            vm.BtnNewConfig.Execute();

            var cfg = vm.Config;

            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.IsTrue(cfg.CountErrors == 0);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.ValidationCollection.Count == 0);

            // Can work with register without catalogs and docs
            var reg1 = cfg.Model.GroupDocuments.GroupRegisters.AddRegister("turnover", EnumRegisterType.TURNOVER);
            reg1.TableTurnoverPropertyMoneyAccumulatorLength = 20;
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.CountErrors == 1);
            //cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Dimensions are not selected."));
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. List of Document types for Register is empty"));

            // Remove one error by adding document for register
            var doc1 = cfg.Model.GroupDocuments.AddDocument("doc1");
            var seq = cfg.Model.GroupDocuments.GroupListSequences.NodeAddNewSubNode();
            doc1.SequenceGuid = seq.Guid;
            reg1.SelectedDoc = doc1;
            reg1.ListObjectDocRefs.Add(new ComplexRef("", doc1.Guid));
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.CountErrors == 1);
            //cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Dimensions are not selected."));
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. There are no any mappings for 'doc1' document."));

            // Dimension without selected catalog will produce another error
            var dim1 = (RegisterDimension)reg1.AddDimension("cat_dimension1");
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.CountErrors == 2);
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. There are no any mappings for 'doc1' document."));
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Catalog type is not selected for register dimension"));

            // Set catalog type for dimension
            var cat1 = cfg.Model.GroupCatalogs.AddCatalog("cat1");
            dim1.DimensionCatalogGuid = cat1.Guid;
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.CountErrors == 1);
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. There are no any mappings for 'doc1' document."));

            // Map Money Accumulator to property with low accuracy
            var p_num28_5 = doc1.AddPropertyNumerical("num28_5", 28, 5);
            reg1.MappingRegPropertyAdd(doc1.Guid, reg1.TableTurnoverPropertyMoneyAccumulatorGuid, p_num28_5.Guid);
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.CountInfos == 2);
            Assert.IsTrue(cfg.CountErrors == 2);
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Dimension 'cat_dimension1' is not mapped to 'doc1' document property."));
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Accumulator property 'AccumulatedQty' is not mapped to 'doc1' document property."));
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Accumulator property 'AccumulatedMoney' has length less than length 'num28_5' property of 'doc1' document."));
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Accumulator property 'AccumulatedMoney' has accuracy less than accuracy 'num28_5' property of 'doc1' document."));

            // Map Money Accumulator
            reg1.MappingRegPropertyRemove(doc1.Guid, reg1.TableTurnoverPropertyMoneyAccumulatorGuid);
            var p_num10_2 = doc1.AddPropertyNumerical("num10_2", 10, 2);
            reg1.MappingRegPropertyAdd(doc1.Guid, reg1.TableTurnoverPropertyMoneyAccumulatorGuid, p_num10_2.Guid);
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.CountErrors == 2);
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Dimension 'cat_dimension1' is not mapped to 'doc1' document property."));
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Accumulator property 'AccumulatedQty' is not mapped to 'doc1' document property."));

            // Map dimension
            var det1 = doc1.AddDetails("det1");
            var p_det1_cat1 = det1.AddPropertyCatalog("cat1", cat1.Guid, true);
            reg1.MappingRegPropertyAdd(doc1.Guid, dim1.Guid, p_det1_cat1.Guid);
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.CountErrors == 2);
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Accumulator property 'AccumulatedQty' is not mapped to 'doc1' document property."));
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Accumulator property 'AccumulatedMoney' not mapped on a same record as a deepest dimension 'cat_dimension1' of 'doc1' document."));

            // Map Money Accumulator on a same record as deepest dimension
            var pd_num10_2 = det1.AddPropertyNumerical("num10_2", 10, 2);
            reg1.MappingRegPropertyRemove(doc1.Guid, reg1.TableTurnoverPropertyMoneyAccumulatorGuid);
            reg1.MappingRegPropertyAdd(doc1.Guid, reg1.TableTurnoverPropertyMoneyAccumulatorGuid, pd_num10_2.Guid);
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.CountErrors == 1);
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Accumulator property 'AccumulatedQty' is not mapped to 'doc1' document property."));

            // Map Qty Accumulator on a same record as deepest dimension
            var pd_num10_4 = det1.AddPropertyNumerical("num10_4", 10, 4);
            reg1.MappingRegPropertyAdd(doc1.Guid, reg1.TableTurnoverPropertyQtyAccumulatorGuid, pd_num10_4.Guid);
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.CountErrors == 0);

            // Add dimension
            var dim2 = (RegisterDimension)reg1.AddDimension("cat_dimension2");
            reg1.MappingRegPropertyAdd(doc1.Guid, reg1.TableTurnoverPropertyQtyAccumulatorGuid, pd_num10_4.Guid);
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.CountErrors == 2);
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Dimension 'cat_dimension2' is not mapped to 'doc1' document property."));
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Catalog type is not selected for register dimension."));

            // Change dimension type to same as first dimension type
            dim2.DimensionCatalogGuid = cat1.Guid;
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.CountErrors == 3);
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Dimension 'cat_dimension2' is not mapped to 'doc1' document property."));
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover' dimension 'cat_dimension1'. Selected catalog type for register dimension is already used for 'cat_dimension2' dimension."));
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover' dimension 'cat_dimension2'. Selected catalog type for register dimension is already used for 'cat_dimension1' dimension."));

            // Change dimension type to another catalog
            var cat2 = cfg.Model.GroupCatalogs.AddCatalog("cat2");
            dim2.DimensionCatalogGuid = cat2.Guid;
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.CountErrors == 1);
            cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Dimension 'cat_dimension2' is not mapped to 'doc1' document property."));

            // Map dimension 2
            var p_doc1_cat2 = doc1.AddPropertyCatalog("cat2", cat2.Guid, true);
            reg1.MappingRegPropertyAdd(doc1.Guid, dim2.Guid, p_doc1_cat2.Guid);
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.CountErrors == 0);

            //var s_qty5_2 = cfg.Model.GroupDocuments.AddSharedPropertyNumerical("qty", 5, 2);

            // 1. Can find doc shared property to map register dimention 

            // 2. Can find doc property to map register dimention 

            // 3. Can find doc numerical property to map register property.
            // Length of doc property has to be less or equal than numerical register property length.
            // Accuracy of doc property has to be less or equal than numerical register property accuracy.

            // 4. Can find doc string property to map register property.
            // Length of doc property has to be less or equal than register property length.

            // 5. Can find doc string property to map register attached property.
            // Length of doc property has to be less or equal than register property length.

            // 6. Can find doc catalog property to map register attached property.

            //await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
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
        [Ignore]
        [TestMethod]
        public void PropertyUniqueGuidForCatalogs()
        {
            var mvm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            //mvm.BtnNewConfig.Execute(@".\kuku.vcfg");
            mvm.BtnNewConfig.Execute();
            var cfg = mvm.Config;

            // Catalogs
            cfg.Model.GroupCatalogs.UseCodeProperty = true;
            var c1 = (Catalog)cfg.Model.GroupCatalogs.NodeAddNewSubNode();
            var c2 = (Catalog)cfg.Model.GroupCatalogs.NodeAddNewSubNode();
            var lst = new List<IProperty>();
            var p1 = cfg.Model.GroupCatalogs[0].GetCodeProperty(lst);
            var p2 = cfg.Model.GroupCatalogs[1].GetCodeProperty(lst);
            Assert.AreNotEqual(p1.Guid, p2.Guid);

            // Self tree catalogs
            c1.UseTree = true;
            c2.UseTree = true;
            lst = c1.GetAllProperties(false).ToList();
            p1 = lst.Single(t => t.Name == cfg.Model.GroupCatalogs.PropertyCodeName);
            var p1h = lst.Single(t => t.Name == c1.PropertyRefSelf.Name);
            lst = c2.GetAllProperties(false).ToList();
            p2 = lst.Single(t => t.Name == cfg.Model.GroupCatalogs.PropertyCodeName);
            var p2h = lst.Single(t => t.Name == c2.PropertyRefSelf.Name);
            Assert.AreNotEqual(p1.Guid, p2.Guid);
            Assert.AreNotEqual(p1h.Guid, p2h.Guid);

            // Separate tree folder catalogs
            c1.UseSeparateTreeForFolders = true;
            c2.UseSeparateTreeForFolders = true;
            lst = c1.GetAllFolderProperties(false).ToList();
            p1 = lst.Single(t => t.Name == cfg.Model.GroupCatalogs.PropertyCodeName);
            p1h = lst.Single(t => t.Name == c1.PropertyRefSelf.Name);
            lst = c2.GetAllFolderProperties(false).ToList();
            p2 = lst.Single(t => t.Name == cfg.Model.GroupCatalogs.PropertyCodeName);
            p2h = lst.Single(t => t.Name == c2.PropertyRefSelf.Name);
            Assert.AreNotEqual(p1.Guid, p2.Guid);
            Assert.AreNotEqual(p1h.Guid, p2h.Guid);

            lst = c1.GetAllProperties(false).ToList();
            p1 = lst.Single(t => t.Name == cfg.Model.GroupCatalogs.PropertyCodeName);
            p1h = lst.Single(t => t.Name == c1.PropertyRefFolder.Name);
            lst = c2.GetAllProperties(false).ToList();
            p2 = lst.Single(t => t.Name == cfg.Model.GroupCatalogs.PropertyCodeName);
            p2h = lst.Single(t => t.Name == c2.PropertyRefFolder.Name);
            Assert.AreNotEqual(p1.Guid, p2.Guid);
            Assert.AreNotEqual(p1h.Guid, p2h.Guid);

            // Catalog tabs
            var t1 = (Detail)c1.GroupDetails.NodeAddNewSubNode();
            lst.Clear();
            t1.GetSpecialProperties(lst, false);
            p1h = lst.Single(t => t.Name == t1.PropertyRefParent.Name);
            var t2 = (Detail)c2.GroupDetails.NodeAddNewSubNode();
            lst.Clear();
            t2.GetSpecialProperties(lst, false);
            p2h = lst.Single(t => t.Name == t2.PropertyRefParent.Name);
            Assert.AreNotEqual(p1h.Guid, p2h.Guid);

            // Catalog folder tabs
            t1 = (Detail)c1.Folder.GroupDetails.NodeAddNewSubNode();
            lst.Clear();
            t1.GetSpecialProperties(lst, false);
            p1h = lst.Single(t => t.Name == t1.PropertyRefParent.Name);
            t2 = (Detail)c2.Folder.GroupDetails.NodeAddNewSubNode();
            lst.Clear();
            t2.GetSpecialProperties(lst, false);
            p2h = lst.Single(t => t.Name == t2.PropertyRefParent.Name);
            Assert.AreNotEqual(p1h.Guid, p2h.Guid);
        }
        [Ignore]
        [TestMethod]
        public void PropertyUniqueGuidForDocuments()
        {
            var mvm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            //mvm.BtnNewConfig.Execute(@".\kuku.vcfg");
            mvm.BtnNewConfig.Execute();
            var cfg = mvm.Config;

            // Documents
            var s1 = (DocumentEnumeratorSequence)cfg.Model.GroupDocuments.GroupListSequences.NodeAddNewSubNode();
            var d1 = (Document)cfg.Model.GroupDocuments.GroupListDocuments.NodeAddNewSubNode();
            d1.SequenceGuid = s1.Guid;
            var s2 = (DocumentEnumeratorSequence)cfg.Model.GroupDocuments.GroupListSequences.NodeAddNewSubNode();
            var d2 = (Document)cfg.Model.GroupDocuments.GroupListDocuments.NodeAddNewSubNode();
            d2.SequenceGuid = s2.Guid;
            var lst =cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments[0].GetPropertiesForUI(false).ToList();
            var p1 = lst.Single(t => t.Name == cfg.Model.GroupDocuments.PropertyDocNumberName);
            var p1h = lst.Single(t => t.Name == cfg.Model.GroupDocuments.PropertyDocNumberName + "UniqueScopeHelper");
            lst = cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments[1].GetPropertiesForUI(false).ToList();
            var p2 = lst.Single(t => t.Name == cfg.Model.GroupDocuments.PropertyDocNumberName);
            var p2h = lst.Single(t => t.Name == cfg.Model.GroupDocuments.PropertyDocNumberName + "UniqueScopeHelper");
            Assert.AreNotEqual(p1.Guid, p2.Guid);
            Assert.AreNotEqual(p1h.Guid, p2h.Guid);

            // Document tabs
            var t1 = (Detail)d1.GroupDetails.NodeAddNewSubNode();
            lst.Clear();
            t1.GetSpecialProperties(lst, false);
            p1h = lst.Single(t => t.Name == t1.PropertyRefParent.Name);
            var t2 = (Detail)d2.GroupDetails.NodeAddNewSubNode();
            lst.Clear();
            t2.GetSpecialProperties(lst, false);
            p2h = lst.Single(t => t.Name == t2.PropertyRefParent.Name);
            Assert.AreNotEqual(p1h.Guid, p2h.Guid);
        }
    }
}
