using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationLogging;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        private readonly ILogger _logger = AppLogger.CreateLogger(nameof(VmTests));
        static VmTests()
        {
        }
        // public VmTests(ITestOutputHelper output)
        public VmTests()
        {
            AppLogger.LogLevel = LogLevel.Trace;
            AppLogger.UseDebug = true;
            _logger = AppLogger.CreateLogger(nameof(VmTests));

            VmBindable.isUnitTests = true;
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
            Assert.AreEqual("test1", cfg.Name);
            Assert.AreEqual("kuku1", cfg.GroupAppSolutions.Name);
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
            Catalog cat_vm = cfg.Model.GroupCatalogs.GroupListCatalogs.AddCatalog("test1");
            cat_vm.BeginEdit();
            cat_vm.Name = "test2";
            cat_vm.CancelEdit();
            Assert.AreEqual("test1", cat_vm.Name);
        }

        [TestMethod]
        public void Editable014CanCancelSecondLevelCollection()
        {
            var mvm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            mvm.BtnNewConfig.Execute();
            mvm.BtnConfigSaveAs.Execute(@".\test.vcfg");

            var cfg = mvm.Config;
            Catalog vm = cfg.Model.GroupCatalogs.GroupListCatalogs.AddCatalog("test");
            var prop = vm.GroupProperties.AddProperty("test1");
            vm.BeginEdit();
            vm.GroupProperties[0].Name = "test2";
            vm.CancelEdit();
            Assert.AreEqual("test1", vm.GroupProperties[0].Name);
            vm.BeginEdit();
            prop = vm.GroupProperties.AddProperty("test3");
            Assert.AreEqual(2, vm.GroupProperties.Count());
            vm.CancelEdit();
            Assert.AreEqual(1, vm.GroupProperties.Count());
            Assert.AreEqual("test1", vm.GroupProperties[0].Name);
        }

        [TestMethod]
        public void Editable021CanCancelCatalogPropertiy()
        {
            var mvm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            //mvm.BtnNewConfig.Execute(@".\kuku.vcfg");
            mvm.BtnNewConfig.Execute();

            var cfg = mvm.Config;
            Catalog vm = cfg.Model.GroupCatalogs.GroupListCatalogs.AddCatalog();
            vm.BeginEdit();
            vm.GroupProperties.AddProperty("pdouble0", EnumDataType.NUMERICAL, 10, 0);
            vm.CancelEdit();
            Assert.AreEqual(0, vm.GroupProperties.Count());
            vm.GroupProperties.AddProperty("pdouble0", EnumDataType.NUMERICAL, 10, 0);
            vm.BeginEdit();
            vm.GroupProperties[0].DataType.DataTypeEnum = EnumDataType.STRING;
            vm.CancelEdit();
            Assert.AreEqual(1, vm.GroupProperties.Count());
            Assert.AreEqual(EnumDataType.NUMERICAL, vm.GroupProperties[0].DataType.DataTypeEnum);
            vm.BeginEdit();
            vm.GroupProperties.ListProperties.Clear();
            vm.CancelEdit();
            Assert.AreEqual(1, vm.GroupProperties.Count());
            Assert.AreEqual(EnumDataType.NUMERICAL, vm.GroupProperties[0].DataType.DataTypeEnum);
        }
        #endregion Editable

        #region Validatable
        [TestMethod]
        public void Validation001_ValidationCollectionEmptyAfterInit()
        {
            ConfigValidator.Reset();
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            var cfg = vm.Config;
            Assert.IsNotNull(cfg.ValidationCollection);
            Assert.IsEmpty(cfg.ValidationCollection);
        }

        [TestMethod]
        public async Task Validation002_CatalogValidationCollectionContainsValidationMessagesFromSubNodesForSelectedNode()
        {
            var cancellation = new CancellationTokenSource();
            var token = cancellation.Token;

            ConfigValidator.Reset();
            CatalogValidator.Reset();
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            var cfg = vm.Config;
            //cfg.SolutionPath = @"..\..\..\..\";

            var c = cfg.Model.GroupCatalogs.GroupListCatalogs.AddCatalog("test");
            Assert.AreEqual(cfg.Model.GroupCatalogs.GroupListCatalogs, c.Parent);

            string mes1 = "test error VeryLow";
            string mes2 = "test warning VeryLow";
            string mes22 = "test warning VeryHigh";
            string mes3 = "test info VeryHigh";

            CatalogValidator.Validator.RuleFor(x => x).Null().WithMessage(mes22).WithSeverity(Severity.Warning).WithState(x => SeverityWeight.VeryHigh);
            CatalogValidator.Validator.RuleFor(x => x).Null().WithMessage(mes1).WithSeverity(Severity.Error).WithState(x => SeverityWeight.VeryLow);
            CatalogValidator.Validator.RuleFor(x => x).Null().WithMessage(mes3).WithSeverity(Severity.Info).WithState(x => SeverityWeight.VeryHigh);
            CatalogValidator.Validator.RuleFor(x => x).Null().WithMessage(mes2).WithSeverity(Severity.Warning).WithState(x => SeverityWeight.VeryLow);

            cfg.Validate();

            await cfg.ValidateSubTreeFromNodeAsync(c, null, token);

            Assert.IsEmpty(cfg.ValidationCollection);
            Assert.HasCount(4, c.ValidationCollection);
            var p = c.ValidationCollection[0];
            Assert.AreEqual(FluentValidation.Severity.Error, p.Severity);
            Assert.AreEqual(mes1, p.Message);
            Assert.AreEqual(c, p.Model);
            p = c.ValidationCollection[1];
            Assert.AreEqual(FluentValidation.Severity.Warning, p.Severity);
            Assert.AreEqual(mes22, p.Message);
            Assert.AreEqual(c, p.Model);
            p = c.ValidationCollection[2];
            Assert.AreEqual(FluentValidation.Severity.Warning, p.Severity);
            Assert.AreEqual(mes2, p.Message);
            Assert.AreEqual(c, p.Model);
            p = c.ValidationCollection[3];
            Assert.AreEqual(FluentValidation.Severity.Info, p.Severity);
            Assert.AreEqual(mes3, p.Message);
            Assert.AreEqual(c, p.Model);

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
            Assert.HasCount(4, cfg.ValidationCollection);
            //await cfg.ValidateSubTreeFromNodeAsync(cfg, token);
            //Assert.IsTrue(cfg.ValidationCollection.Count == 4);
        }

        [TestMethod]
        public async Task Validation003_AppProjectGeneratorValidationCollectionContainsValidationMessagesFromSubNodesForSelectedNode()
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

            //string mes1 = "test error message";
            //string mes2 = "test warning message";
            //string mes22 = "test warning2 message";
            //string mes3 = "test info message";

            //AppProjectGeneratorValidator.Validator.RuleFor(x => x).Null().WithMessage(mes22).WithSeverity(Severity.Warning).WithState(x => SeverityWeight.VeryHigh);
            //AppProjectGeneratorValidator.Validator.RuleFor(x => x).Null().WithMessage(mes1).WithSeverity(Severity.Error).WithState(x => SeverityWeight.VeryLow);
            //AppProjectGeneratorValidator.Validator.RuleFor(x => x).Null().WithMessage(mes3).WithSeverity(Severity.Info).WithState(x => SeverityWeight.VeryHigh);
            //AppProjectGeneratorValidator.Validator.RuleFor(x => x).Null().WithMessage(mes2).WithSeverity(Severity.Warning).WithState(x => SeverityWeight.VeryLow);

            cfg.Validate();

            await cfg.ValidateSubTreeFromNodeAsync(sol1, null, token);
            Assert.IsEmpty(cfg.ValidationCollection);
            Assert.HasCount(7, sol1.ValidationCollection);
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
        public async Task Validation007_Propagation()
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
            Assert.HasCount(1, cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].ValidationCollection);
            Assert.AreEqual(1, cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].CountErrors);
            Assert.AreEqual(1, cfg.Model.GroupConstantGroups.ListConstantGroups[0].CountErrors);
            Assert.HasCount(1, cfg.Model.GroupConstantGroups.ValidationCollection);
            Assert.AreEqual(1, cfg.Model.GroupConstantGroups.CountErrors);
            Assert.IsEmpty(cfg.ValidationCollection);
            Assert.AreEqual(1, cfg.CountErrors);
            Assert.AreEqual(0, cfg.CountInfos);
            Assert.AreEqual(0, cfg.CountWarnings);

            cfg.Model.GroupEnumerations.NodeAddNewSubNode();
            EnumerationValidator.Validator.RuleFor(x => x).Null().WithMessage(mes2).WithSeverity(Severity.Error).WithState(x => SeverityWeight.Low);

            await cfg.ValidateSubTreeFromNodeAsync(cfg.Model.GroupEnumerations, null, token);
            Assert.HasCount(1, cfg.Model.GroupEnumerations[0].ValidationCollection);
            Assert.AreEqual(1, cfg.Model.GroupEnumerations[0].CountErrors);
            Assert.HasCount(1, cfg.Model.GroupEnumerations.ValidationCollection);
            Assert.AreEqual(1, cfg.Model.GroupEnumerations.CountErrors);
            Assert.IsEmpty(cfg.ValidationCollection);
            Assert.AreEqual(2, cfg.CountErrors);
            Assert.AreEqual(0, cfg.CountInfos);
            Assert.AreEqual(0, cfg.CountWarnings);

            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.HasCount(2, cfg.ValidationCollection);
            Assert.AreEqual(2, cfg.CountErrors);
            Assert.AreEqual(0, cfg.CountInfos);
            Assert.AreEqual(0, cfg.CountWarnings);
        }
        #endregion Validatable

        #region Unique position for Protobuf
        [TestMethod]
        public void Position_001_Catalog_Prpperty()
        {
            var mvm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            mvm.BtnNewConfig.Execute();
            var cfg = mvm.Config;

            uint pos_in_c1 = IProperty.PositionReservation;

            var cref1 = cfg.Model.GroupCatalogs.GroupListCatalogs.AddCatalog();
            var cref2 = cfg.Model.GroupCatalogs.GroupListCatalogs.AddCatalog();

            var c1 = cfg.Model.GroupCatalogs.GroupListCatalogs.AddCatalog();
            // for ID, record version, Code, Name
            pos_in_c1 += 4;
            Assert.AreEqual(pos_in_c1, c1.LastPosition);
            var c1p1 = c1.AddProperty("p1");
            Assert.AreEqual(++pos_in_c1, c1.LastPosition);
            Assert.AreEqual(pos_in_c1, c1p1.Position);
            c1p1.Remove();
            Assert.AreEqual(pos_in_c1, c1.LastPosition);
            var c1p2 = c1.AddProperty("p2");
            Assert.AreEqual(++pos_in_c1, c1.LastPosition);
            Assert.AreEqual(pos_in_c1, c1p2.Position);
            // Get positions for all special properties
            var lst = c1.GetAllProperties(true).ToList();
            Assert.AreEqual(pos_in_c1, c1.LastPosition);

            // Catalog property
            var c1pcat1 = c1.AddPropertyCatalog("cat1", cref1);
            // for cat1, cat1Descr, cat1RefId
            pos_in_c1 += 3;
            Assert.AreEqual(pos_in_c1, c1.LastPosition);
            Assert.AreEqual(pos_in_c1, c1pcat1.Position);
            Assert.AreEqual(1, c1pcat1.ListObjectRefs.Count);
            // Catalogs property
            var c1pcat1_cat2 = c1.AddPropertyCatalogs("cat1_cat2", cref1, cref2);
            // for cat1_cat2, cat1_cat2Descr, cat1_cat2Gd, cat1RefId, cat2RefId
            pos_in_c1 += 5;
            Assert.AreEqual(pos_in_c1, c1.LastPosition);
            Assert.AreEqual(pos_in_c1, c1pcat1_cat2.Position);
            Assert.AreEqual(2, c1pcat1_cat2.ListObjectRefs.Count);

            // Any catalog property

            // Document property

            // Document property

            // Any document property

            uint pos_in_c1d1 = IProperty.PositionReservation;
            var c1d1 = c1.AddDetails("detail1");
            Assert.AreEqual(++pos_in_c1, c1.LastPosition);
            Assert.AreEqual(pos_in_c1, c1d1.Position);
            // for ID, record version, RefParent
            pos_in_c1d1 += 3;
            Assert.AreEqual(pos_in_c1d1, c1d1.LastPosition);
            var c1d1p1 = c1d1.AddProperty("p1");
            Assert.AreEqual(++pos_in_c1d1, c1d1.LastPosition);
            Assert.AreEqual(pos_in_c1d1, c1d1p1.Position);

            uint pos_c1d1d1 = IProperty.PositionReservation;
            var c1d1d1 = c1d1.AddDetails("detail1");
            Assert.AreEqual(++pos_in_c1d1, c1d1.LastPosition);
            Assert.AreEqual(pos_in_c1d1, c1d1d1.Position);
            Assert.AreEqual(pos_in_c1d1, c1d1.LastPosition);
            var c1d1d1p1 = c1d1d1.AddProperty("p1");
            // for ID, record version, RefParent
            pos_c1d1d1 += 3;
            Assert.AreEqual(++pos_c1d1d1, c1d1d1.LastPosition);
            Assert.AreEqual(pos_c1d1d1, c1d1d1p1.Position);

            // Catalog property
            var c1d1pcat1 = c1d1.AddPropertyCatalog("cat1", cref1);
            // for cat1, cat1Descr, cat1RefId
            pos_in_c1d1 += 3;
            Assert.AreEqual(pos_in_c1d1, c1d1.LastPosition);
            Assert.AreEqual(pos_in_c1d1, c1d1pcat1.Position);
            Assert.AreEqual(1, c1d1pcat1.ListObjectRefs.Count);
            // Catalogs property
            var c1d1pcat1_cat2 = c1d1.AddPropertyCatalogs("cat1_cat2", cref1, cref2);
            // for cat1_cat2, cat1_cat2Descr, cat1_cat2Gd, cat1RefId, cat2RefId
            pos_in_c1d1 += 5;
            Assert.AreEqual(pos_in_c1d1, c1d1.LastPosition);
            Assert.AreEqual(pos_in_c1d1, c1d1pcat1_cat2.Position);
            Assert.AreEqual(2, c1d1pcat1_cat2.ListObjectRefs.Count);

            // Any catalog property

            // Document property

            // Document property

            // Any document property
        }
        [TestMethod]
        public void Position_002_Document_Prpperty()
        {
            var mvm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            mvm.BtnNewConfig.Execute();
            var cfg = mvm.Config;

            uint pos_in_d1 = IProperty.PositionReservation;

            var cref1 = cfg.Model.GroupCatalogs.GroupListCatalogs.AddCatalog();
            var cref2 = cfg.Model.GroupCatalogs.GroupListCatalogs.AddCatalog();

            var d1 = cfg.Model.GroupDocuments.GroupListDocuments.AddDocument("doc1");
            // for ID, record version
            pos_in_d1 += 2;
            Assert.AreEqual(pos_in_d1, d1.LastPosition);
            var d1p1 = d1.AddProperty("p1");
            Assert.AreEqual(++pos_in_d1, d1.LastPosition);
            Assert.AreEqual(pos_in_d1, d1p1.Position);

            d1p1.Remove();
            Assert.AreEqual(pos_in_d1, d1.LastPosition);
            var c1p2 = d1.AddProperty("p2");
            Assert.AreEqual(++pos_in_d1, d1.LastPosition);
            Assert.AreEqual(pos_in_d1, c1p2.Position);

            // Catalog property
            var d1pcat1 = d1.AddPropertyCatalog("cat1", cref1);
            // for cat1, cat1Descr, cat1RefId
            pos_in_d1 += 3;
            Assert.AreEqual(pos_in_d1, d1.LastPosition);
            Assert.AreEqual(pos_in_d1, d1pcat1.Position);
            Assert.AreEqual(1, d1pcat1.ListObjectRefs.Count);
            // Catalogs property
            var d1pcat1_cat2 = d1.AddPropertyCatalogs("cat1_cat2", cref1, cref2);
            // for cat1_cat2, cat1_cat2Descr, cat1_cat2Gd, cat1RefId, cat2RefId
            pos_in_d1 += 5;
            Assert.AreEqual(pos_in_d1, d1.LastPosition);
            Assert.AreEqual(pos_in_d1, d1pcat1_cat2.Position);
            Assert.AreEqual(2, d1pcat1_cat2.ListObjectRefs.Count);

            // Any catalog property

            // Document property

            // Document property

            // Any document property

            uint pos_in_d1d1 = IProperty.PositionReservation;

            var d1d1 = d1.AddDetails("detail1");
            Assert.AreEqual(++pos_in_d1, d1.LastPosition);
            Assert.AreEqual(pos_in_d1, d1d1.Position);
            // for ID, record version, RefParent
            pos_in_d1d1 += 3;
            Assert.AreEqual(pos_in_d1d1, d1d1.LastPosition);

            var d1d1p1 = d1d1.AddProperty("p1");
            Assert.AreEqual(++pos_in_d1d1, d1d1.LastPosition);
            Assert.AreEqual(pos_in_d1d1, d1d1p1.Position);

            // Catalog property
            var d1d1pcat1 = d1d1.AddPropertyCatalog("cat1", cref1);
            // for ID, record version, RefParent
            pos_in_d1d1 += 3;
            Assert.AreEqual(pos_in_d1d1, d1d1.LastPosition);
            Assert.AreEqual(pos_in_d1d1, d1d1pcat1.Position);
            Assert.AreEqual(1, d1d1pcat1.ListObjectRefs.Count);
            // Catalogs property
            var d1d1pcat1_cat2 = d1d1.AddPropertyCatalogs("cat1_cat2", cref1, cref2);
            // for cat1_cat2, cat1_cat2Descr, cat1_cat2Gd, cat1RefId, cat2RefId
            pos_in_d1d1 += 5;
            Assert.AreEqual(pos_in_d1d1, d1d1.LastPosition);
            Assert.AreEqual(pos_in_d1d1, d1d1pcat1_cat2.Position);
            Assert.AreEqual(2, d1d1pcat1_cat2.ListObjectRefs.Count);

            // Any catalog property

            // Document property

            // Document property

            // Any document property

            pos_in_d1 = IProperty.PositionReservation;
            var d2 = cfg.Model.GroupDocuments.GroupListDocuments.AddDocument("doc2");
            // for ID, record version
            pos_in_d1 += 2;
            Assert.AreEqual(pos_in_d1, d2.LastPosition);
            var c2p1 = d2.AddProperty("p1");
            Assert.AreEqual(++pos_in_d1, d2.LastPosition);
        }
        [TestMethod]
        public void Position_003_ManyToMany_Prpperty()
        {
            var mvm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            mvm.BtnNewConfig.Execute();
            var cfg = mvm.Config;

            uint pos_in_m2m = IProperty.PositionReservation;

            var cref1 = cfg.Model.GroupCatalogs.GroupListCatalogs.AddCatalog();
            var cref2 = cfg.Model.GroupCatalogs.GroupListCatalogs.AddCatalog();

            var m2m = cfg.Model.GroupCatalogs.GroupRelations.GroupListManyToManyRelations.AddRelation("testrel1", cref1, cref2, false);
            // for ID, record version, -- without history
            pos_in_m2m += 2;
            // for cref1, cref2, cref1Descr, cref2Descr, cref1RefId, cref2RefId
            pos_in_m2m += 6;
            Assert.AreEqual(pos_in_m2m, m2m.LastPosition);
        }
        [TestMethod]
        public void Position_004_Register_Property()
        {
            var mvm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            mvm.BtnNewConfig.Execute();
            var cfg = mvm.Config;

            uint pos = IProperty.PositionReservation;

            var r1 = cfg.Model.GroupDocuments.GroupRegisters.AddRegister();
            pos += 4; // ID, record version, two accumulation properties
            Assert.AreEqual(pos, r1.LastPosition);
            var r1p1 = r1.AddAttachedProperty("p1");
            Assert.AreEqual(++pos, r1.LastPosition);
            Assert.AreEqual(pos, r1p1.Position);

            r1p1.Remove();
            Assert.AreEqual(pos, r1.LastPosition);
            var r1p2 = r1.AddAttachedProperty("p2");
            Assert.AreEqual(++pos, r1.LastPosition);
            Assert.AreEqual(pos, r1p2.Position);

            var cat = cfg.Model.GroupCatalogs.GroupListCatalogs.AddCatalog("test_cat");
            var r1d1 = r1.AddDimension("d1", cat);
            // for test_cat, test_catDescr, test_catRefId
            pos += 3;
            Assert.AreEqual(pos, r1.LastPosition);
            Assert.AreEqual(pos, r1d1.Position);

            uint pos2 = IProperty.PositionReservation;
            var r2 = cfg.Model.GroupDocuments.GroupRegisters.AddRegister();
            pos2 += 4; // ID, record version, two accumulation properties
            Assert.AreEqual(pos2, r2.LastPosition);
            var r2p1 = r2.AddAttachedProperty("p1");
            Assert.AreEqual(++pos2, r2.LastPosition);
        }
        [TestMethod]
        public void Position_005_Constants()
        {
            var mvm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            mvm.BtnNewConfig.Execute();
            var cfg = mvm.Config;

            var cref1 = cfg.Model.GroupCatalogs.GroupListCatalogs.AddCatalog();
            var cref2 = cfg.Model.GroupCatalogs.GroupListCatalogs.AddCatalog();

            var gn1 = cfg.Model.GroupConstantGroups.AddGroupConstants("set1");
            uint pos_in_gn1 = IProperty.PositionReservation;
            pos_in_gn1 += 2; // ID, record version
            Assert.AreEqual(pos_in_gn1, gn1.LastPosition);

            var n1 = gn1.AddConstant("n1");
            Assert.AreEqual(++pos_in_gn1, gn1.LastPosition);
            Assert.AreEqual(pos_in_gn1, n1.Position);

            var n2 = gn1.AddConstantCatalog("test_cat", cref1); ;
            // for test_cat, test_catDescr, test_catRefId
            pos_in_gn1 += 3;
            Assert.AreEqual(pos_in_gn1, gn1.LastPosition);
            Assert.AreEqual(pos_in_gn1, n2.Position);

            var n3 = gn1.AddConstantCatalogs("test_cats", cref1, cref2); ;
            // for test_cat, test_catDescr, test_catRefId
            pos_in_gn1 += 5;
            Assert.AreEqual(pos_in_gn1, gn1.LastPosition);
            Assert.AreEqual(pos_in_gn1, n3.Position);
        }
        [TestMethod]
        public void Position_006_Timeline()
        {
            var mvm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            mvm.BtnNewConfig.Execute();
            var cfg = mvm.Config;

            var cref1 = cfg.Model.GroupCatalogs.GroupListCatalogs.AddCatalog();
            var cref2 = cfg.Model.GroupCatalogs.GroupListCatalogs.AddCatalog();

            var tm = cfg.Model.GroupDocuments.DocumentTimeline;
            uint pos_in_tm = IProperty.PositionReservation;
            pos_in_tm += 2; // ID, record version
            Assert.AreEqual(pos_in_tm, tm.LastPosition);

            var n1 = tm.AddProperty("p1");
            Assert.AreEqual(++pos_in_tm, tm.LastPosition);
            Assert.AreEqual(pos_in_tm, n1.Position);

            var n2 = tm.AddPropertyCatalog("test_cat", cref1); ;
            // for test_cat, test_catDescr, test_catRefId
            pos_in_tm += 3;
            Assert.AreEqual(pos_in_tm, tm.LastPosition);
            Assert.AreEqual(pos_in_tm, n2.Position);

            var n3 = tm.AddPropertyCatalogs("test_cats", cref1, cref2); ;
            // for test_cat, test_catDescr, test_catRefId
            pos_in_tm += 5;
            Assert.AreEqual(pos_in_tm, tm.LastPosition);
            Assert.AreEqual(pos_in_tm, n3.Position);
        }
        [TestMethod]
        public void Property002_Sorting()
        {
            var mvm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            mvm.BtnNewConfig.Execute();

            var cfg = mvm.Config;
            int pSort = 0;
            var c = (Catalog)cfg.Model.GroupCatalogs.GroupListCatalogs.NodeAddNewSubNode();
            var g = c.GroupProperties;
            var p0 = (Property)g.NodeAddNewSubNode();
            Assert.AreEqual(++pSort, p0.ExplicitSortingPosition);
            Assert.AreEqual("Property1", p0.Name);
            Assert.AreEqual(0, g.IndexOf(p0));
            var p1 = (Property)g.NodeAddNewSubNode();
            Assert.AreEqual(++pSort, p1.ExplicitSortingPosition);
            Assert.AreEqual("Property2", p1.Name);
            Assert.AreEqual(1, g.IndexOf(p1));
            var p2 = (Property)g.NodeAddNewSubNode();
            Assert.AreEqual(++pSort, p2.ExplicitSortingPosition);
            Assert.AreEqual("Property3", p2.Name);
            Assert.AreEqual(2, g.IndexOf(p2));

            g.SortType = EnumSortingType.ASCENDING;
            Assert.AreEqual(0, g.IndexOf(p0));
            Assert.AreEqual(1, g.IndexOf(p1));
            Assert.AreEqual(2, g.IndexOf(p2));

            g.SortType = EnumSortingType.DESCENDING;
            Assert.AreEqual(0, g.IndexOf(p2));
            Assert.AreEqual(1, g.IndexOf(p1));
            Assert.AreEqual(2, g.IndexOf(p0));

            g.SortType = EnumSortingType.EXPLICIT;
            Assert.AreEqual(0, g.IndexOf(p0));
            Assert.AreEqual(1, g.IndexOf(p1));
            Assert.AreEqual(2, g.IndexOf(p2));

            Assert.AreEqual(1, p0.ExplicitSortingPosition);
            Assert.AreEqual(2, p1.ExplicitSortingPosition);
            Assert.AreEqual(3, p2.ExplicitSortingPosition);
            mvm.Config.SelectedNode = p1;
            mvm.BtnSelectionUp.Execute();
            Assert.AreEqual(p1, mvm.Config.SelectedNode);
            Assert.AreEqual(2, p0.ExplicitSortingPosition);
            Assert.AreEqual(1, p1.ExplicitSortingPosition);
            Assert.AreEqual(3, p2.ExplicitSortingPosition);
            Assert.AreEqual(0, g.IndexOf(p1));
            Assert.AreEqual(1, g.IndexOf(p0));
            Assert.AreEqual(2, g.IndexOf(p2));

            mvm.BtnSelectionDown.Execute();
            Assert.AreEqual(1, p0.ExplicitSortingPosition);
            Assert.AreEqual(2, p1.ExplicitSortingPosition);
            Assert.AreEqual(3, p2.ExplicitSortingPosition);
            Assert.AreEqual(0, g.IndexOf(p0));
            Assert.AreEqual(1, g.IndexOf(p1));
            Assert.AreEqual(2, g.IndexOf(p2));

            mvm.Config.SelectedNode = p1;
            mvm.BtnAddNew.Execute();
            Assert.AreEqual("Property4", g.ListProperties[1].Name);
            Assert.AreEqual(1, p0.ExplicitSortingPosition);
            Assert.AreEqual(2, g.ListProperties[1].ExplicitSortingPosition);
            Assert.AreEqual(3, p1.ExplicitSortingPosition);
            Assert.AreEqual(4, p2.ExplicitSortingPosition);
            Assert.AreEqual(0, g.IndexOf(p0));
            Assert.AreEqual(2, g.IndexOf(p1));
            Assert.AreEqual(3, g.IndexOf(p2));

        }
        #endregion Unique position for Protobuf

        [TestMethod]
        public async System.Threading.Tasks.Task Register002_Validation()
        {
            var cancellation = new CancellationTokenSource();
            var token = cancellation.Token;

            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            vm.BtnNewConfig.Execute();

            var cfg = vm.Config;

            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.AreEqual(0, cfg.CountErrors);
            Assert.AreEqual(0, cfg.CountInfos);
            Assert.AreEqual(0, cfg.CountWarnings);
            Assert.IsEmpty(cfg.ValidationCollection);

            // Can work with register without catalogs and docs
            var reg1 = cfg.Model.GroupDocuments.GroupRegisters.AddRegister("turnover", EnumRegisterType.TURNOVER);
            reg1.PropertyMoneyAccumulatorLength = 20;
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.AreEqual(0, cfg.CountInfos);
            Assert.AreEqual(0, cfg.CountWarnings);
            Assert.AreEqual(1, cfg.CountErrors);
            //cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Dimensions are not selected."));
            var valmesstmp = cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. List of Document types for Register is empty"));

            // Remove one error by adding document for register
            var doc1 = cfg.Model.GroupDocuments.AddDocument("doc1");
            var seq = cfg.Model.GroupDocuments.GroupListSequences.NodeAddNewSubNode();
            doc1.SequenceGuid = seq.Guid;
            reg1.SelectedDoc = doc1;
            reg1.ListObjectDocRefs.Add(new ComplexRef("", doc1.Guid));
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.AreEqual(0, cfg.CountInfos);
            Assert.AreEqual(0, cfg.CountWarnings);
            Assert.AreEqual(1, cfg.CountErrors);
            //cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Dimensions are not selected."));
            valmesstmp = cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. There are no any mappings for 'doc1' document."));

            // Dimension without selected catalog will produce another error
            var dim1 = (RegisterDimension)reg1.AddDimension("cat_dimension1");
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.AreEqual(0, cfg.CountInfos);
            Assert.AreEqual(0, cfg.CountWarnings);
            Assert.AreEqual(2, cfg.CountErrors);
            valmesstmp = cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. There are no any mappings for 'doc1' document."));
            valmesstmp = cfg.ValidationCollection.Single(err => err.Message.StartsWith("Catalog type is not selected for register dimension"));

            // Set catalog type for dimension
            var cat1 = cfg.Model.GroupCatalogs.GroupListCatalogs.AddCatalog("cat1");
            dim1.DimensionCatalogGuid = cat1.Guid;
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.AreEqual(0, cfg.CountInfos);
            Assert.AreEqual(0, cfg.CountWarnings);
            Assert.AreEqual(1, cfg.CountErrors);
            valmesstmp = cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. There are no any mappings for 'doc1' document."));

            // Map Money Accumulator to property with low accuracy
            var p_num28_5 = doc1.AddPropertyNumerical("num28_5", 28, 5);
            Register.MappingRegPropertyAdd(reg1, doc1.Guid, cfg.Model.GetPropertyGuid(reg1, EnumSpecialPropertyType.ACCUMULATOR_MONEY), p_num28_5.Guid);
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.AreEqual(0, cfg.CountWarnings);
            Assert.AreEqual(2, cfg.CountInfos);
            Assert.AreEqual(2, cfg.CountErrors);
            valmesstmp = cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Dimension 'cat_dimension1' is not mapped to 'doc1' document property."));
            valmesstmp = cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Accumulator property 'AccumulatedQty' is not mapped to 'doc1' document property."));
            valmesstmp = cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Accumulator property 'AccumulatedMoney' has length less than length 'num28_5' property of 'doc1' document."));
            valmesstmp = cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Accumulator property 'AccumulatedMoney' has accuracy less than accuracy 'num28_5' property of 'doc1' document."));

            // Map Money Accumulator
            Register.MappingRegPropertyRemove(reg1, doc1.Guid, cfg.Model.GetPropertyGuid(reg1, EnumSpecialPropertyType.ACCUMULATOR_MONEY));
            var p_num10_2 = doc1.AddPropertyNumerical("num10_2", 10, 2);
            Register.MappingRegPropertyAdd(reg1, doc1.Guid, cfg.Model.GetPropertyGuid(reg1, EnumSpecialPropertyType.ACCUMULATOR_MONEY), p_num10_2.Guid);
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.AreEqual(0, cfg.CountInfos);
            Assert.AreEqual(0, cfg.CountWarnings);
            Assert.AreEqual(2, cfg.CountErrors);
            valmesstmp = cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Dimension 'cat_dimension1' is not mapped to 'doc1' document property."));
            valmesstmp = cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Accumulator property 'AccumulatedQty' is not mapped to 'doc1' document property."));

            // Map dimension
            var det1 = doc1.AddDetails("det1");
            var p_det1_cat1 = det1.AddPropertyCatalog("cat1", cat1.Guid, true);
            Register.MappingRegPropertyAdd(reg1, doc1.Guid, dim1.PropertyRefDimensionCatalog.Guid, p_det1_cat1.Guid);
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.AreEqual(0, cfg.CountInfos);
            Assert.AreEqual(0, cfg.CountWarnings);
            Assert.AreEqual(2, cfg.CountErrors);
            valmesstmp = cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Accumulator property 'AccumulatedQty' is not mapped to 'doc1' document property."));
            valmesstmp = cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Accumulator property 'AccumulatedMoney' not mapped on a same record as a deepest dimension 'cat_dimension1' of 'doc1' document."));
            // Map Money Accumulator on a same record as deepest dimension
            var pd_num10_2 = det1.AddPropertyNumerical("num10_2", 10, 2);
            Register.MappingRegPropertyRemove(reg1, doc1.Guid, cfg.Model.GetPropertyGuid(reg1, EnumSpecialPropertyType.ACCUMULATOR_MONEY));
            Register.MappingRegPropertyAdd(reg1, doc1.Guid, cfg.Model.GetPropertyGuid(reg1, EnumSpecialPropertyType.ACCUMULATOR_MONEY), pd_num10_2.Guid);
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.AreEqual(0, cfg.CountInfos);
            Assert.AreEqual(0, cfg.CountWarnings);
            Assert.AreEqual(1, cfg.CountErrors);
            valmesstmp = cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Accumulator property 'AccumulatedQty' is not mapped to 'doc1' document property."));

            // Map Qty Accumulator on a same record as deepest dimension
            var pd_num10_4 = det1.AddPropertyNumerical("num10_4", 10, 4);
            Register.MappingRegPropertyAdd(reg1, doc1.Guid, cfg.Model.GetPropertyGuid(reg1, EnumSpecialPropertyType.ACCUMULATOR_QTY), pd_num10_4.Guid);
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.AreEqual(0, cfg.CountInfos);
            Assert.AreEqual(0, cfg.CountWarnings);
            Assert.AreEqual(0, cfg.CountErrors);

            // Add dimension
            var dim2 = (RegisterDimension)reg1.AddDimension("cat_dimension2");
            Register.MappingRegPropertyAdd(reg1, doc1.Guid, cfg.Model.GetPropertyGuid(reg1, EnumSpecialPropertyType.ACCUMULATOR_QTY), pd_num10_4.Guid);
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.AreEqual(0, cfg.CountInfos);
            Assert.AreEqual(0, cfg.CountWarnings);
            Assert.AreEqual(1, cfg.CountErrors);
            valmesstmp = cfg.ValidationCollection.Single(err => err.Message.StartsWith("Catalog type is not selected for register dimension."));

            // Change dimension type to same as first dimension type
            dim2.DimensionCatalogGuid = cat1.Guid;
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.AreEqual(0, cfg.CountInfos);
            Assert.AreEqual(0, cfg.CountWarnings);
            Assert.AreEqual(3, cfg.CountErrors);
            valmesstmp = cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Dimension 'cat_dimension2' is not mapped to 'doc1' document property."));
            valmesstmp = cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover' dimension 'cat_dimension1'. Selected catalog type for register dimension is already used for 'cat_dimension2' dimension."));
            valmesstmp = cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover' dimension 'cat_dimension2'. Selected catalog type for register dimension is already used for 'cat_dimension1' dimension."));

            // Change dimension type to another catalog
            var cat2 = cfg.Model.GroupCatalogs.GroupListCatalogs.AddCatalog("cat2");
            dim2.DimensionCatalogGuid = cat2.Guid;
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.AreEqual(0, cfg.CountInfos);
            Assert.AreEqual(0, cfg.CountWarnings);
            Assert.AreEqual(1, cfg.CountErrors);
            valmesstmp = cfg.ValidationCollection.Single(err => err.Message.StartsWith("Register 'turnover'. Dimension 'cat_dimension2' is not mapped to 'doc1' document property."));

            // Map dimension 2
            var p_doc1_cat2 = doc1.AddPropertyCatalog("cat2", cat2.Guid, true);
            Register.MappingRegPropertyAdd(reg1, doc1.Guid, dim2.PropertyRefDimensionCatalog.Guid, p_doc1_cat2.Guid);
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.AreEqual(0, cfg.CountInfos);
            Assert.AreEqual(0, cfg.CountWarnings);
            Assert.AreEqual(0, cfg.CountErrors);

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
        //[Ignore]
        [TestMethod]
        public void PropertyUniqueGuidForCatalogs()
        {
            var mvm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath());
            //mvm.BtnNewConfig.Execute(@".\kuku.vcfg");
            mvm.BtnNewConfig.Execute();
            var cfg = mvm.Config;

            var gc = cfg.Model.GroupCatalogs.GroupListCatalogs;

            // Catalogs
            gc.UseCodeProperty = true;
            var c1 = (Catalog)gc.NodeAddNewSubNode();
            var c2 = (Catalog)gc.NodeAddNewSubNode();
            var lst = new List<IProperty>();
            var p1 = cfg.Model.GroupCatalogs.GroupListCatalogs[0].GetCodeProperty(lst);
            var p2 = cfg.Model.GroupCatalogs.GroupListCatalogs[1].GetCodeProperty(lst);
            Assert.AreNotEqual(p1.Guid, p2.Guid);

            // Self tree catalogs
            c1.UseTree = true;
            c2.UseTree = true;
            lst = [.. c1.GetAllProperties(false)];
            p1 = lst.Single(t => t.Name == gc.PropertyCodeName);
            var p1h = lst.Single(t => t.Name == IProperty.SpecialRefTreeParentName + "Id");
            lst = [.. c2.GetAllProperties(false)];
            p2 = lst.Single(t => t.Name == gc.PropertyCodeName);
            var p2h = lst.Single(t => t.Name == IProperty.SpecialRefTreeParentName + "Id");
            Assert.AreNotEqual(p1.Guid, p2.Guid);
            Assert.AreNotEqual(p1h.Guid, p2h.Guid);

            // Separate tree folder catalogs
            c1.UseSeparateTreeForFolders = true;
            c2.UseSeparateTreeForFolders = true;
            lst = [.. c1.GetAllFolderProperties(false)];
            p1 = lst.Single(t => t.Name == gc.PropertyCodeName);
            p1h = lst.Single(t => t.Name == IProperty.SpecialRefTreeParentName + "Id");
            lst = [.. c2.GetAllFolderProperties(false)];
            p2 = lst.Single(t => t.Name == gc.PropertyCodeName);
            p2h = lst.Single(t => t.Name == IProperty.SpecialRefTreeParentName + "Id");
            Assert.AreNotEqual(p1.Guid, p2.Guid);
            Assert.AreNotEqual(p1h.Guid, p2h.Guid);

            lst = [.. c1.GetAllProperties(false)];
            p1 = lst.Single(t => t.Name == gc.PropertyCodeName);
            p1h = lst.Single(t => t.Name == IProperty.SpecialRefParentName + "Id");
            lst = [.. c2.GetAllProperties(false)];
            p2 = lst.Single(t => t.Name == gc.PropertyCodeName);
            p2h = lst.Single(t => t.Name == IProperty.SpecialRefParentName + "Id");
            Assert.AreNotEqual(p1.Guid, p2.Guid);
            Assert.AreNotEqual(p1h.Guid, p2h.Guid);

            // Catalog tabs
            var t1 = (Detail)c1.GroupDetails.NodeAddNewSubNode();
            lst.Clear();
            t1.GetSpecialProperties(lst, false);
            p1h = lst.Single(t => t.Name == IProperty.SpecialRefParentName + "Id");
            var t2 = (Detail)c2.GroupDetails.NodeAddNewSubNode();
            lst.Clear();
            t2.GetSpecialProperties(lst, false);
            p2h = lst.Single(t => t.Name == IProperty.SpecialRefParentName + "Id");
            Assert.AreNotEqual(p1h.Guid, p2h.Guid);

            // Catalog folder tabs
            t1 = (Detail)c1.Folder.GroupDetails.NodeAddNewSubNode();
            lst.Clear();
            t1.GetSpecialProperties(lst, false);
            p1h = lst.Single(t => t.Name == IProperty.SpecialRefParentName + "Id");
            t2 = (Detail)c2.Folder.GroupDetails.NodeAddNewSubNode();
            lst.Clear();
            t2.GetSpecialProperties(lst, false);
            p2h = lst.Single(t => t.Name == IProperty.SpecialRefParentName + "Id");
            Assert.AreNotEqual(p1h.Guid, p2h.Guid);
        }
        //[Ignore]
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
            var lst = cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments[0].GetPropertiesForUI(false).ToList();
            var p1 = lst.Single(t => t.Name == cfg.Model.GroupDocuments.GroupListDocuments.PropertyDocNumberName);
            lst = [.. cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments[1].GetPropertiesForUI(false)];
            var p2 = lst.Single(t => t.Name == cfg.Model.GroupDocuments.GroupListDocuments.PropertyDocNumberName);
            Assert.AreNotEqual(p1.Guid, p2.Guid);

            // Document tabs
            var t1 = (Detail)d1.GroupDetails.NodeAddNewSubNode();
            lst.Clear();
            t1.GetSpecialProperties(lst, false);
            var t2 = (Detail)d2.GroupDetails.NodeAddNewSubNode();
            lst.Clear();
            t2.GetSpecialProperties(lst, false);
        }
    }
}
