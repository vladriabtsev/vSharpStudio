using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.ViewModels;
using vSharpStudio.vm.ViewModels;

namespace vSharpStudio.Unit
{
    [TestClass]
    public class EditorVmTests
    {
        private readonly ILogger _logger;
        static EditorVmTests()
        {
        }
        public EditorVmTests()
        {
            AppLogger.LogLevel = LogLevel.Trace;
            AppLogger.UseDebug = true;
            _logger = AppLogger.CreateLogger(nameof(EditorVmTests));
            VmBindable.isUnitTests = true;
        }

        //internal static void InitLogging(object type)
        //{
        //    if (ApplicationLogging.LogerProvider == null)
        //    {
        //        // Log.Logger = new LoggerConfiguration()
        //        //    .MinimumLevel.Debug()
        //        //    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
        //        //    //.WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
        //        //    .CreateLogger().ForContext(type.GetType());
        //        // var serviceCollection = new ServiceCollection();
        //        // var lp = serviceCollection.AddLogging(loggingBuilder =>
        //        // {
        //        //    //loggingBuilder.AddFilter((p) => { return p >= LogLevel.Trace; });
        //        //    //loggingBuilder.AddConsole((o) => { o.IncludeScopes = true; });
        //        //    loggingBuilder.AddSerilog();
        //        //    //loggingBuilder.AddConfiguration(new )
        //        //    //loggingBuilder.AddDebug();
        //        // }).BuildServiceProvider().GetRequiredService<ILoggerProvider>();
        //        // ApplicationLogging.LogerProvider = lp;
        //        Log.Logger = new LoggerConfiguration()
        //            .MinimumLevel.Verbose()
        //            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
        //            // .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
        //            .CreateLogger();
        //        ApplicationLogging.LogerProvider = new SerilogLoggerProvider(Log.Logger);
        //    }
        //}

        #region Config
        [TestMethod]
        public void Config001GuidInit()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            Assert.IsGreaterThan(0, cfg.Guid.Length);
        }

        [TestMethod]
        public void Config002CanSaveAndRestore()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            var gr = cfg.Model.GroupConstantGroups.AddGroupConstants("Gr");
            gr.NodeAddNewSubNode();
            string json = cfg.ExportToJson();
            Assert.IsGreaterThan(0, json.Length);
            var cfg2 = new Config(json);
            Assert.HasCount(1, cfg2.Model.GroupConstantGroups.ListConstantGroups);
            Assert.AreEqual("Gr", cfg2.Model.GroupConstantGroups.ListConstantGroups[0].Name);
            //Assert.IsTrue(cfg2.Model.GroupConstantGroups.ListConstantGroups[0].Name == typeof(Constant).Name + 1);
        }

        [TestMethod]
        public void Config003CanSaveAndRestoreSortingValue()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            var gr = cfg.Model.GroupConstantGroups.AddGroupConstants("Gr");
            gr.NodeAddNewSubNode();
            Assert.AreEqual(typeof(Constant).Name + 1, gr.ListConstants[0].Name);
            Assert.AreEqual(1, gr.ListConstants[0].ExplicitSortingPosition);
            gr.NodeAddNewSubNode();
            Assert.AreEqual(typeof(Constant).Name + 2, gr.ListConstants[1].Name);
            Assert.AreEqual(2, gr.ListConstants[1].ExplicitSortingPosition);
            string json = cfg.ExportToJson();
            Assert.IsGreaterThan(0, json.Length);
            var cfg2 = new Config(json);
            var gr2 = cfg2.Model.GroupConstantGroups.ListConstantGroups[0];
            Assert.HasCount(2, gr2.ListConstants);
            Assert.AreEqual(typeof(Constant).Name + 1, gr2.ListConstants[0].Name);
            Assert.AreEqual(typeof(Constant).Name + 2, gr2.ListConstants[1].Name);

            gr.ListConstants[1].NodeMoveUp();
            Assert.AreEqual(typeof(Constant).Name + 2, gr.ListConstants[0].Name);
            Assert.AreEqual(typeof(Constant).Name + 1, gr.ListConstants[1].Name);
            gr.ListConstants[0].NodeMoveUp();
            Assert.AreEqual(typeof(Constant).Name + 2, gr.ListConstants[0].Name);
            Assert.AreEqual(typeof(Constant).Name + 1, gr.ListConstants[1].Name);
            json = cfg.ExportToJson();
            Assert.IsGreaterThan(0, json.Length);
            cfg2 = new Config(json);
            gr2 = cfg2.Model.GroupConstantGroups.ListConstantGroups[0];
            Assert.HasCount(2, gr2.ListConstants);
            Assert.AreEqual(typeof(Constant).Name + 2, gr2.ListConstants[0].Name);
            Assert.AreEqual(typeof(Constant).Name + 1, gr2.ListConstants[1].Name);

            gr.ListConstants[0].NodeMoveDown();
            Assert.AreEqual(typeof(Constant).Name + 1, gr.ListConstants[0].Name);
            Assert.AreEqual(typeof(Constant).Name + 2, gr.ListConstants[1].Name);
            gr.ListConstants[1].NodeMoveDown();
            Assert.AreEqual(typeof(Constant).Name + 1, gr.ListConstants[0].Name);
            Assert.AreEqual(typeof(Constant).Name + 2, gr.ListConstants[1].Name);
            json = cfg.ExportToJson();
            Assert.IsGreaterThan(0, json.Length);
            cfg2 = new Config(json);
            gr2 = cfg2.Model.GroupConstantGroups.ListConstantGroups[0];
            Assert.HasCount(2, gr2.ListConstants);
            Assert.AreEqual(typeof(Constant).Name + 1, gr2.ListConstants[0].Name);
            Assert.AreEqual(typeof(Constant).Name + 2, gr2.ListConstants[1].Name);
        }
        // TODO business validation tests
        // [TestMethod]
        // public void Config003ValidationIsDbFromConnectionStringInfoConnectionStringName()
        // {
        //    var cfg = new Config(new SortedObservableCollection<ValidationMessage>());
        //    cfg.IsDbFromConnectionString = true;
        //    cfg.Validate();
        //    Assert.False(cfg.HasErrors);
        //    Assert.IsTrue(cfg.HasWarnings);
        //    Assert.False(cfg.HasInfos);
        //    Assert.IsTrue(cfg.ValidationCollection.Count == 1);
        //    Assert.IsTrue(cfg.ValidationCollection[0].SortingValue >= 1 << ValidationMessage.MultiplierShift);
        // }
        #endregion Config

        #region Constant
        [TestMethod]
        public void Constant001GuidInit()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            var c = cfg.Model.GroupConstantGroups.NodeAddNewSubNode();
            Assert.IsGreaterThan(0, c.Guid.Length);
        }

        [TestMethod]
        public void Constant002AddedParent()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            var gr = cfg.Model.GroupConstantGroups.AddGroupConstants("Gr");
            gr.NodeAddNewSubNode();
            Assert.AreEqual(cfg.Model.GroupConstantGroups.ListConstantGroups[0].Parent.Guid, cfg.Model.GroupConstantGroups.Guid);
            cfg.Model.GroupConstantGroups.ListConstantGroups[0].NodeAddNew();
            Assert.AreEqual(cfg.Model.GroupConstantGroups.ListConstantGroups[1].Parent.Guid, cfg.Model.GroupConstantGroups.Guid);
        }

        [TestMethod]
        public void Constant003AddedDefaultName()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            var gr = cfg.Model.GroupConstantGroups.AddGroupConstants("Gr");
            gr.NodeAddNewSubNode();
            Assert.AreEqual(Defaults.ConstantName + "1", cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].Name);
            cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].NodeAddNew(); // insert before current
            Assert.AreEqual(Defaults.ConstantName + "2", cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].Name);
        }
        #endregion Constant

        #region Enum
        [TestMethod]
        public void Enum001GuidInit()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            var en = cfg.Model.GroupEnumerations.NodeAddNewSubNode();
            Assert.IsGreaterThan(0, en.Guid.Length);
        }

        [TestMethod]
        public void Enum002AddedParent()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            cfg.Model.GroupEnumerations.NodeAddNewSubNode();
            Assert.AreEqual(cfg.Model.GroupEnumerations[0].Parent.Guid, cfg.Model.GroupEnumerations.Guid);
            cfg.Model.GroupEnumerations[0].NodeAddNew();
            Assert.AreEqual(cfg.Model.GroupEnumerations[1].Parent.Guid, cfg.Model.GroupEnumerations.Guid);
        }
        #endregion Enum

        #region Catalog
        [TestMethod]
        public void Catalog001GuidInit()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            var c = cfg.Model.GroupCatalogs.AddCatalog();
            Assert.IsGreaterThan(0, c.Guid.Length);
            var p = c.AddProperty("test");
            Assert.IsGreaterThan(0, p.Guid.Length);
        }
        #endregion Catalog

        #region Diff
        // [TestMethod]
        // public void DiffConstant001Added()
        // {
        //    Assert.IsTrue(false);
        // }
        // [TestMethod]
        // public void DiffConfig001CanDiffwithDb()
        // {
        //    Assert.IsTrue(false);
        // }
        #endregion Diff

        #region ITreeConfigNode
#if DEBUG
        //[TestMethod]
        //public void ITreeConfigNode001_UpdateSortingValueWhenNameIsChanged()
        //{
        //    var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
        //    var cfg = vm.Config;
        //    VmBindable.IsNotValidate = true;
        //    var gc = cfg.Model.GroupConstantGroups.AddGroupConstants("Gr");
        //    var cnst = new Constant(gc);
        //    gc.Add(cnst);
        //    var curr = cnst.SortingValue;
        //    cnst.Name = "abc1";
        //    Assert.AreNotEqual(curr, cnst.SortingValue);
        //    curr = cnst.SortingValue;
        //    cnst.Name = "ABC1";
        //    Assert.AreEqual(curr, cnst.SortingValue);

        //    cnst.Name = "_0";
        //    curr = cnst.SortingValue;
        //    cnst.Name = "00";
        //    Assert.IsGreaterThan(curr, cnst.SortingValue);

        //    cnst.Name = "_";
        //    curr = cnst.SortingValue;
        //    cnst.Name = "0";
        //    Assert.IsGreaterThan(curr, cnst.SortingValue);

        //    cnst.Name = "0";
        //    curr = cnst.SortingValue;
        //    cnst.Name = "1";
        //    Assert.IsGreaterThan(curr, cnst.SortingValue);

        //    cnst.Name = "9";
        //    curr = cnst.SortingValue;
        //    cnst.Name = "A";
        //    Assert.IsGreaterThan(curr, cnst.SortingValue);

        //    cnst.Name = "A";
        //    curr = cnst.SortingValue;
        //    cnst.Name = "B";
        //    Assert.IsGreaterThan(curr, cnst.SortingValue);

        //    cnst.Name = "A";
        //    curr = cnst.SortingValue;
        //    cnst.Name = "a";
        //    Assert.AreEqual(curr, cnst.SortingValue);

        //    // cnst.Name = "__";
        //    cnst.Name = "_z";
        //    curr = cnst.SortingValue;
        //    cnst.Name = "0_";
        //    Assert.IsGreaterThan(curr, cnst.SortingValue);

        //    cnst.Name = "ABC1";
        //    curr = cnst.SortingValue;
        //    cnst.Name = "BBC1";
        //    Assert.IsGreaterThan(curr, cnst.SortingValue);
        //    cnst.Name = "ACC1";
        //    Assert.IsGreaterThan(curr, cnst.SortingValue);
        //    cnst.Name = "ABD1";
        //    Assert.IsGreaterThan(curr, cnst.SortingValue);
        //    cnst.Name = "ABC2";
        //    Assert.IsGreaterThan(curr, cnst.SortingValue);

        //    cnst.Name = "ABC0";
        //    Assert.IsLessThan(curr, cnst.SortingValue);
        //    cnst.Name = "ABB1";
        //    Assert.IsLessThan(curr, cnst.SortingValue);
        //    cnst.Name = "AAC1";
        //    Assert.IsLessThan(curr, cnst.SortingValue);
        //}
#endif

        [TestMethod]
        public void ITreeConfigNode002_RestoreSortingValueWhenObjectRestoredFromFile()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            var gc = cfg.Model.GroupConstantGroups.AddGroupConstants("Gr");
            var cnst = new Constant(gc);
            gc.Add(cnst);
            cnst.Name = "abc1";
            //var curr = cnst.SortingValue;

            string json = cfg.ExportToJson();
            var cfg2 = new Config(json);

            Assert.AreEqual(cnst.Name, cfg2.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].Name);
            Assert.AreEqual(cnst.ExplicitSortingPosition, cfg2.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].ExplicitSortingPosition);
        }

        [TestMethod]
        public void ITreeConfigNode003_ReSortedWhenSortingValueIsChanged()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            var gc = cfg.Model.GroupConstantGroups.AddGroupConstants("Gr");
            var cnst = new Constant(gc);
            gc.Add(cnst);
            cnst.Name = "abc1";

            var cnst2 = new Constant(gc);
            gc.Add(cnst2);
            cnst2.Name = "abc1";

            Assert.AreNotEqual(cnst2.Guid, cnst.Guid);

            gc.SortType = EnumSortingType.ASCENDING;

            cnst2.Name = "abc0";
            Assert.AreEqual(cnst.Guid, cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[1].Guid);
            Assert.AreEqual(cnst2.Guid, cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].Guid);

            cnst2.Name = "abc2";
            Assert.AreEqual(cnst.Guid, cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].Guid);
            Assert.AreEqual(cnst2.Guid, cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[1].Guid);
        }

        [TestMethod]
        public void ITreeConfigNode003_CanConfigTreeCommands()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;

            #region Constants
            Assert.IsTrue(cfg.Model.GroupConstantGroups.NodeCanLeft());
            Assert.IsTrue(cfg.Model.GroupConstantGroups.NodeCanRight());
            Assert.IsFalse(cfg.Model.GroupConstantGroups.NodeCanMoveUp());
            Assert.IsFalse(cfg.Model.GroupConstantGroups.NodeCanMoveDown());
            Assert.IsFalse(cfg.Model.GroupConstantGroups.NodeCanAddNew());
            Assert.IsTrue(cfg.Model.GroupConstantGroups.NodeCanAddNewSubNode());

            Assert.IsNull(cfg.SelectedNode);
            var gr = cfg.Model.GroupConstantGroups.AddGroupConstants("Gr");
            Assert.IsNotNull(cfg.SelectedNode);
            Assert.AreEqual(gr, cfg.SelectedNode);
            Assert.AreEqual(gr.Guid, cfg.SelectedNode.Guid);

            cfg.Model.GroupConstantGroups.AddGroupConstants("Gr2");
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].NodeCanLeft());
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].NodeCanRight());
            Assert.IsFalse(cfg.Model.GroupConstantGroups.ListConstantGroups[0].NodeCanMoveUp());
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].NodeCanMoveDown());
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[1].NodeCanMoveUp());
            Assert.IsFalse(cfg.Model.GroupConstantGroups.ListConstantGroups[1].NodeCanMoveDown());
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].NodeCanAddNew());
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].NodeCanAddNewSubNode());

            var cnst = gr.NodeAddNewSubNode();
            var cnst2 = gr.NodeAddNewSubNode();
            Assert.IsFalse(cnst.NodeCanMoveUp());
            Assert.IsTrue(cnst.NodeCanMoveDown());
            Assert.IsTrue(cnst2.NodeCanMoveUp());
            Assert.IsFalse(cnst2.NodeCanMoveDown());

            #endregion Constants

            #region Enumerations

            Assert.IsFalse(cfg.Model.GroupEnumerations.NodeCanLeft());
            Assert.IsTrue(cfg.Model.GroupEnumerations.NodeCanRight());
            Assert.IsFalse(cfg.Model.GroupEnumerations.NodeCanMoveUp());
            Assert.IsFalse(cfg.Model.GroupEnumerations.NodeCanMoveDown());
            Assert.IsFalse(cfg.Model.GroupEnumerations.NodeCanAddNew());
            Assert.IsTrue(cfg.Model.GroupEnumerations.NodeCanAddNewSubNode());
            cfg.Model.GroupEnumerations.NodeAddNewSubNode();
            Assert.IsNotNull(cfg.SelectedNode);
            Assert.AreEqual(cfg.Model.GroupEnumerations[0], cfg.SelectedNode);
            Assert.AreEqual(cfg.Model.GroupEnumerations[0].Guid, cfg.SelectedNode.Guid);

            Assert.IsTrue(cfg.Model.GroupEnumerations.NodeCanRight());
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].NodeCanLeft());
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].NodeCanRight());
            Assert.IsFalse(cfg.Model.GroupEnumerations[0].NodeCanMoveUp());
            Assert.IsFalse(cfg.Model.GroupEnumerations[0].NodeCanMoveDown());
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].NodeCanAddNew());
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].NodeCanAddNewSubNode());

            // #region Properties

            // cfg.GroupCatalogs[0].NodeAddNewSubNode();
            // Assert.IsTrue(cfg.SelectedNode != null);
            // Assert.IsTrue(cfg.SelectedNode == cfg.GroupCatalogs[0].ListProperties[0]);
            // Assert.IsTrue(cfg.GroupCatalogs[0].ListProperties[0].NodeCanLeft() == true);
            // Assert.IsTrue(cfg.GroupCatalogs[0].ListProperties[0].NodeCanRight() == true);
            // Assert.IsTrue(cfg.GroupCatalogs[0].ListProperties[0].NodeCanMoveUp() == false);
            // Assert.IsTrue(cfg.GroupCatalogs[0].ListProperties[0].NodeCanMoveDown() == false);
            // Assert.IsTrue(cfg.GroupCatalogs[0].ListProperties[0].NodeCanAddNew() == true);
            // Assert.IsTrue(cfg.GroupCatalogs[0].ListProperties[0].NodeCanAddNewSubNode() == true);

            // cfg.GroupCatalogs[0].ListProperties[0].NodeAddNew();
            // Assert.IsTrue(cfg.SelectedNode == cfg.GroupCatalogs[0].ListProperties[1]);
            // Assert.IsTrue(cfg.SelectedNode.Guid == cfg.GroupCatalogs[0].ListProperties[1].Guid);
            // Assert.IsTrue(cfg.GroupCatalogs[0].ListProperties[1].NodeCanLeft() == true);
            // Assert.IsTrue(cfg.GroupCatalogs[0].ListProperties[1].NodeCanRight() == true);
            // Assert.IsTrue(cfg.GroupCatalogs[0].ListProperties[1].NodeCanMoveUp() == true);
            // Assert.IsTrue(cfg.GroupCatalogs[0].ListProperties[1].NodeCanMoveDown() == false);
            // Assert.IsTrue(cfg.GroupCatalogs[0].ListProperties[1].NodeCanAddNew() == true);
            // Assert.IsTrue(cfg.GroupCatalogs[0].ListProperties[1].NodeCanAddNewSubNode() == true);

            // var p = cfg.GroupCatalogs[0].ListProperties[1];
            // p.NodeMoveUp();
            // Assert.IsTrue(p == cfg.GroupCatalogs[0].ListProperties[0]);
            // Assert.IsTrue(cfg.SelectedNode == cfg.GroupCatalogs[0].ListProperties[0]);

            //// change property parameters
            // p.DataType.MinValue = 5;
            // p.DataType.MaxValue = 6;

            // p.NodeAddClone();
            // Assert.IsTrue(p == cfg.GroupCatalogs[0].ListProperties[0]);
            // Assert.IsTrue(cfg.GroupCatalogs[0].ListProperties[1].Name == cfg.GroupCatalogs[0].ListProperties[0].Name + "2");
            // Assert.IsTrue(5 == cfg.GroupCatalogs[0].ListProperties[1].DataType.MinValue);
            // Assert.IsTrue(6 == cfg.GroupCatalogs[0].ListProperties[1].DataType.MaxValue);

            // #endregion Properties

            #endregion Enumerations

            #region Catalogs

            Assert.IsFalse(cfg.Model.GroupCatalogs.NodeCanLeft());
            Assert.IsTrue(cfg.Model.GroupCatalogs.NodeCanRight());
            Assert.IsFalse(cfg.Model.GroupCatalogs.NodeCanMoveUp());
            Assert.IsFalse(cfg.Model.GroupCatalogs.NodeCanMoveDown());
            Assert.IsFalse(cfg.Model.GroupCatalogs.NodeCanAddNew());
            Assert.IsTrue(cfg.Model.GroupCatalogs.NodeCanAddNewSubNode());
            cfg.Model.GroupCatalogs.NodeAddNewSubNode();
            Assert.IsNotNull(cfg.SelectedNode);
            Assert.AreEqual(cfg.Model.GroupCatalogs[0], cfg.SelectedNode);
            Assert.AreEqual(cfg.Model.GroupCatalogs[0].Guid, cfg.SelectedNode.Guid);

            Assert.IsTrue(cfg.Model.GroupCatalogs.NodeCanRight());
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].NodeCanLeft());
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].NodeCanRight());
            Assert.IsFalse(cfg.Model.GroupCatalogs[0].NodeCanMoveUp());
            Assert.IsFalse(cfg.Model.GroupCatalogs[0].NodeCanMoveDown());
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].NodeCanAddNew());
            Assert.IsFalse(cfg.Model.GroupCatalogs[0].NodeCanAddNewSubNode());

            #region Properties

            cfg.Model.GroupCatalogs[0].GroupProperties.NodeAddNewSubNode();
            Assert.IsNotNull(cfg.SelectedNode);
            Assert.AreEqual(cfg.Model.GroupCatalogs[0].GroupProperties[0], cfg.SelectedNode);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeCanLeft());
            Assert.IsFalse(cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeCanRight());
            Assert.IsFalse(cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeCanMoveUp());
            Assert.IsFalse(cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeCanMoveDown());
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeCanAddNew());
            Assert.IsFalse(cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeCanAddNewSubNode());

            cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeAddNew();
            Assert.AreEqual(cfg.Model.GroupCatalogs[0].GroupProperties[0], cfg.SelectedNode);
            Assert.AreEqual(cfg.Model.GroupCatalogs[0].GroupProperties[0].Guid, cfg.SelectedNode.Guid);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[1].NodeCanLeft());
            Assert.IsFalse(cfg.Model.GroupCatalogs[0].GroupProperties[1].NodeCanRight());
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[1].NodeCanMoveUp());
            Assert.IsFalse(cfg.Model.GroupCatalogs[0].GroupProperties[1].NodeCanMoveDown());
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[1].NodeCanAddNew());
            Assert.IsFalse(cfg.Model.GroupCatalogs[0].GroupProperties[1].NodeCanAddNewSubNode());

            var p = cfg.Model.GroupCatalogs[0].GroupProperties[1];
            p.NodeMoveUp();
            Assert.AreEqual(cfg.Model.GroupCatalogs[0].GroupProperties[0], p);
            Assert.AreEqual(cfg.Model.GroupCatalogs[0].GroupProperties[0], cfg.SelectedNode);

            // change property parameters
            // p.DataType.MinValue = 5;
            // p.DataType.MaxValue = 6;

            p.NodeAddClone();
            Assert.AreEqual(cfg.Model.GroupCatalogs[0].GroupProperties[1], p);
            Assert.AreEqual(cfg.Model.GroupCatalogs[0].GroupProperties[0].Name + "2", cfg.Model.GroupCatalogs[0].GroupProperties[1].Name);
            // Assert.IsTrue(5 == cfg.Model.GroupCatalogs[0].GroupProperties.ListProperties[2].DataType.MinValue);
            // Assert.IsTrue(6 == cfg.Model.GroupCatalogs[0].GroupProperties.ListProperties[2].DataType.MaxValue);

            #endregion Properties

            #endregion Catalogs

        }
        #endregion ITreeConfigNode

        #region Compare Tree
        private MainPageVM CreateVM()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;

            cfg.Model.GroupEnumerations.NodeAddNewSubNode();
            cfg.Model.GroupEnumerations[0].DataTypeEnum = EnumEnumerationType.INTEGER_VALUE;
            cfg.Model.GroupEnumerations[0].ListEnumerationPairs.Add(new EnumerationPair(cfg.Model.GroupEnumerations[0]) { Name = "one", Value = "1" });

            var gr = cfg.Model.GroupConstantGroups.AddGroupConstants("Gr");
            var cnst1 = (Constant)gr.NodeAddNewSubNode();
            cnst1.DataType.DataTypeEnum = EnumDataType.BOOL;
            var cnst2 = (Constant)gr.NodeAddNewSubNode();
            cnst2.DataType.DataTypeEnum = EnumDataType.ENUMERATION;
            cnst2.DataType.ObjectRef0.ForeignObjectGuid = cfg.Model.GroupEnumerations[0].Guid;

            return vm;
        }

        [TestMethod]
        public void Rules001_DataType()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            var c = cfg.Model.GroupCatalogs.AddCatalog("Test1");
            var p = c.AddProperty("tp");
            var dt = p.DataType; ;

            dt.Validate();
            Assert.AreEqual(0, dt.CountErrors);
            Assert.AreEqual(0, dt.CountInfos);
            Assert.AreEqual(0, dt.CountWarnings);
            Assert.IsEmpty(dt.ValidationCollection);
            // Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityObjectName == Visibility.Collapsed);

            dt.DataTypeEnum = EnumDataType.ANY;
            dt.Validate();
            Assert.AreEqual(0, dt.CountErrors);
            Assert.AreEqual(0, dt.CountInfos);
            Assert.AreEqual(0, dt.CountWarnings);
            Assert.IsEmpty(dt.ValidationCollection);


            // Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityObjectName == Visibility.Collapsed);

            dt.DataTypeEnum = EnumDataType.BOOL;
            dt.Validate();
            Assert.AreEqual(0, dt.CountErrors);
            Assert.AreEqual(0, dt.CountInfos);
            Assert.AreEqual(0, dt.CountWarnings);
            Assert.IsEmpty(dt.ValidationCollection);
            // Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityObjectName == Visibility.Collapsed);

            dt.DataTypeEnum = EnumDataType.CATALOG;
            dt.Validate();
            Assert.AreEqual(0, dt.CountErrors);
            Assert.AreEqual(0, dt.CountInfos);
            Assert.AreEqual(0, dt.CountWarnings);
            Assert.IsTrue(dt.HasErrors);
            Assert.HasCount(2, dt.ValidationCollection);
            dt.ValidationCollection.Single(msg => msg.Message == Config.ValidationMessages.TYPE_EMPTY_CATALOG);
            // Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityObjectName == Visibility.Visible);

            dt.DataTypeEnum = EnumDataType.CATALOGS;
            dt.Validate();
            Assert.AreEqual(0, dt.CountErrors);
            Assert.AreEqual(0, dt.CountInfos);
            Assert.AreEqual(0, dt.CountWarnings);
            Assert.IsEmpty(dt.ValidationCollection);
            // Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityObjectName == Visibility.Collapsed);

            // dt.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.Constant;
            // dt.Validate();
            // Assert.IsTrue(dt.CountErrors == 0);
            // Assert.IsTrue(dt.CountInfos == 0);
            // Assert.IsTrue(dt.CountWarnings == 0);
            // Assert.IsTrue(dt.HasErrors);
            // Assert.IsTrue(dt.ValidationCollection.Count == 1);
            // Assert.IsTrue(dt.ValidationCollection[0].Message == Config.ValidationMessages.TYPE_EMPTY_CONSTANTName);
            // Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityObjectName == Visibility.Visible);

            dt.DataTypeEnum = EnumDataType.ENUMERATION;
            dt.Validate();
            Assert.AreEqual(0, dt.CountErrors);
            Assert.AreEqual(0, dt.CountInfos);
            Assert.AreEqual(0, dt.CountWarnings);
            Assert.IsTrue(dt.HasErrors);
            Assert.HasCount(2, dt.ValidationCollection);
            dt.ValidationCollection.Single(msg => msg.Message == Config.ValidationMessages.TYPE_EMPTY_ENUMERATION);
            //Assert.AreEqual(Config.ValidationMessages.TYPE_EMPTY_ENUMERATION, dt.ValidationCollection[0].Message);
            // Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityObjectName == Visibility.Visible);

            dt.DataTypeEnum = EnumDataType.NUMERICAL;
            dt.Validate();
            Assert.AreEqual(0, dt.CountErrors);
            Assert.AreEqual(0, dt.CountInfos);
            Assert.AreEqual(0, dt.CountWarnings);
            // Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Visible);
            // Assert.IsTrue(dt.VisibilityLength == Visibility.Visible);
            // Assert.IsTrue(dt.VisibilityObjectName == Visibility.Collapsed);

            dt.DataTypeEnum = EnumDataType.STRING_FIXED;
            dt.Validate();
            Assert.AreEqual(0, dt.CountErrors);
            Assert.AreEqual(0, dt.CountInfos);
            Assert.AreEqual(0, dt.CountWarnings);
            Assert.IsEmpty(dt.ValidationCollection);

            dt.DataTypeEnum = EnumDataType.STRING;
            dt.Validate();
            Assert.AreEqual(0, dt.CountErrors);
            Assert.AreEqual(0, dt.CountInfos);
            Assert.AreEqual(0, dt.CountWarnings);
            Assert.IsEmpty(dt.ValidationCollection);
            // Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityLength == Visibility.Visible);
            // Assert.IsTrue(dt.VisibilityObjectName == Visibility.Collapsed);

            dt.DataTypeEnum = EnumDataType.ULID;
            dt.Validate();
            Assert.AreEqual(0, dt.CountErrors);
            Assert.AreEqual(0, dt.CountInfos);
            Assert.AreEqual(0, dt.CountWarnings);
            Assert.IsEmpty(dt.ValidationCollection);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task Rules002_Enumeration()
        {
            var cancellation = new CancellationTokenSource();
            var token = cancellation.Token;

            var vm = this.CreateVM();
            var cfg = vm.Config;
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.AreEqual(0, cfg.CountErrors);
            Assert.AreEqual(0, cfg.CountInfos);
            Assert.AreEqual(0, cfg.CountWarnings);
            Assert.IsEmpty(cfg.ValidationCollection);

            cfg.Model.GroupEnumerations[0].Name = "1a";
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.AreEqual(1, cfg.Model.GroupEnumerations[0].CountErrors);
            Assert.AreEqual(0, cfg.Model.GroupEnumerations[0].CountInfos);
            Assert.AreEqual(0, cfg.Model.GroupEnumerations[0].CountWarnings);
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].HasErrors);
            Assert.HasCount(1, cfg.Model.GroupEnumerations[0].ValidationCollection);
            Assert.AreEqual(FluentValidation.Severity.Error, cfg.Model.GroupEnumerations[0].ValidationCollection[0].Severity);
            Assert.AreEqual(Config.ValidationMessages.NAME_START_WITH_DIGIT, cfg.Model.GroupEnumerations[0].ValidationCollection[0].Message);

            // intermediate node contains only validation count
            Assert.IsEmpty(cfg.Model.GroupEnumerations.ValidationCollection);
            Assert.AreEqual(1, cfg.Model.GroupEnumerations.CountErrors);
            Assert.AreEqual(0, cfg.Model.GroupEnumerations.CountInfos);
            Assert.AreEqual(0, cfg.Model.GroupEnumerations.CountWarnings);

            // ValidateSubTreeFromNode(node). node contains full list of validations
            Assert.AreEqual(1, cfg.CountErrors);
            Assert.AreEqual(0, cfg.CountInfos);
            Assert.AreEqual(0, cfg.CountWarnings);
            Assert.HasCount(1, cfg.ValidationCollection);
            Assert.AreEqual(FluentValidation.Severity.Error, cfg.ValidationCollection[0].Severity);
            // Assert.IsTrue(cfg.ValidationCollection[1].Severity == FluentValidation.Severity.Error);
            if (cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT)
            {
                Assert.AreEqual(Config.ValidationMessages.NAME_START_WITH_DIGIT, cfg.ValidationCollection[0].Message);
                // Assert.IsTrue(cfg.ValidationCollection[1].Message == Config.ValidationMessages.TYPE_OBJECT_IS_NOT_FOUND);
            }
            // else
            // {
            //    Assert.IsTrue(cfg.ValidationCollection[1].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT);
            //    Assert.IsTrue(cfg.ValidationCollection[0].Message == Config.ValidationMessages.TYPE_OBJECT_IS_NOT_FOUND);
            // }

            cfg.Model.GroupEnumerations[0].Name = " ab";
            Assert.AreEqual("ab", cfg.Model.GroupEnumerations[0].Name);
            cfg.Model.GroupEnumerations[0].Validate();
            Assert.IsFalse(cfg.Model.GroupEnumerations[0].HasErrors);

            cfg.Model.GroupEnumerations[0].Name = "ab ";
            Assert.AreEqual("ab", cfg.Model.GroupEnumerations[0].Name);
            cfg.Model.GroupEnumerations[0].Validate();
            Assert.IsFalse(cfg.Model.GroupEnumerations[0].HasErrors);

            cfg.Model.GroupEnumerations[0].Name = "a b";
            // cfg.Model.GroupConstants[1].DataType.ObjectName = "a b";
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.HasCount(1, cfg.ValidationCollection);
            Assert.AreEqual(FluentValidation.Severity.Error, cfg.ValidationCollection[0].Severity);
            Assert.AreEqual(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE, cfg.ValidationCollection[0].Message);

            cfg.Model.GroupEnumerations.NodeAddNewSubNode();
            cfg.Model.GroupEnumerations[0].Name = "ab";
            cfg.Model.GroupEnumerations[1].Name = "ab";
            // cfg.Model.GroupConstants[1].DataType.ObjectName = "ab";
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.HasCount(2, cfg.ValidationCollection);
            Assert.HasCount(2, (from p in cfg.ValidationCollection where p.Severity == FluentValidation.Severity.Error select p).ToList());
            Assert.HasCount(2, (from p in cfg.ValidationCollection where p.Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE select p).ToList());
            // Assert.IsTrue((from p in cfg.ValidationCollection where p.Message == Config.ValidationMessages.TYPE_LENGTH_GREATER_THAN_ZERO select p).ToList().Count() == 1);
            Assert.HasCount(1, cfg.Model.GroupEnumerations[1].ValidationCollection);
            Assert.AreEqual(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE, cfg.Model.GroupEnumerations[1].ValidationCollection[0].Message);
            Assert.IsTrue(cfg.Model.GroupEnumerations[1].HasErrors);
            var errenum = cfg.Model.GroupEnumerations[1].GetErrors("Name").GetEnumerator();
            Assert.IsTrue(errenum.MoveNext());
            Assert.AreEqual(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE, (string)errenum.Current);
            Assert.IsFalse(errenum.MoveNext());
        }

        [TestMethod]
        [Ignore]
        public async System.Threading.Tasks.Task Rules003_Constant()
        {
            var cancellation = new CancellationTokenSource();
            var token = cancellation.Token;

            var vm = this.CreateVM();
            var cfg = vm.Config;

            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.AreEqual(0, cfg.CountErrors);
            Assert.AreEqual(0, cfg.CountInfos);
            Assert.AreEqual(0, cfg.CountWarnings);
            Assert.IsEmpty(cfg.ValidationCollection);

            //string prev = cfg.Model.GroupConstants[0].DataType.ObjectName;
            //cfg.Model.GroupConstants[0].DataType.ObjectName = "123";
            //cfg.ValidateSubTreeFromNode(cfg);
            //Assert.IsTrue(cfg.CountErrors == 1);
            //Assert.IsTrue(cfg.CountInfos == 0);
            //Assert.IsTrue(cfg.CountWarnings == 0);
            //Assert.IsTrue(cfg.ValidationCollection.Count == 1);
            //Assert.IsTrue(cfg.Model.GroupConstants[0].CountErrors == 1);
            //Assert.IsTrue(cfg.Model.GroupConstants[0].CountInfos == 0);
            //Assert.IsTrue(cfg.Model.GroupConstants[0].CountWarnings == 0);
            //Assert.IsTrue(cfg.Model.GroupConstants[0].DataType.ValidationCollection.Count == 1);


            //cfg.Model.GroupConstants[0].DataType.ObjectName = prev;
            //cfg.Model.GroupEnumerations[0].Name = "1a";
            //cfg.ValidateSubTreeFromNode(cfg);
            //Assert.IsTrue(cfg.Model.GroupEnumerations[0].CountErrors == 1);
            //Assert.IsTrue(cfg.Model.GroupEnumerations[0].CountInfos == 0);
            //Assert.IsTrue(cfg.Model.GroupEnumerations[0].CountWarnings == 0);
            //Assert.IsTrue(cfg.Model.GroupEnumerations[0].HasErrors);
            //Assert.IsTrue(cfg.Model.GroupEnumerations[0].ValidationCollection.Count == 1);
            //Assert.IsTrue(cfg.Model.GroupEnumerations[0].ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            //Assert.IsTrue(cfg.Model.GroupEnumerations[0].ValidationCollection[0].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT);

            //// intermediate node contains only validation count
            //Assert.IsTrue(cfg.Model.GroupEnumerations.ValidationCollection.Count == 0);
            //Assert.IsTrue(cfg.Model.GroupEnumerations.CountErrors == 1);
            //Assert.IsTrue(cfg.Model.GroupEnumerations.CountInfos == 0);
            //Assert.IsTrue(cfg.Model.GroupEnumerations.CountWarnings == 0);

            //// ValidateSubTreeFromNode(node). node contains full list of validations
            //Assert.IsTrue(cfg.CountErrors == 1);
            //Assert.IsTrue(cfg.CountInfos == 0);
            //Assert.IsTrue(cfg.CountWarnings == 0);
            //Assert.IsTrue(cfg.ValidationCollection.Count == 1);
            //Assert.IsTrue(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            //Assert.IsTrue(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT);

            //cfg.Model.GroupEnumerations[0].Name = " ab";
            //Assert.IsTrue(cfg.Model.GroupEnumerations[0].Name == "ab");
            //cfg.Model.GroupEnumerations[0].Validate();
            //Assert.False(cfg.Model.GroupEnumerations[0].HasErrors);

            //cfg.Model.GroupEnumerations[0].Name = "ab ";
            //Assert.IsTrue(cfg.Model.GroupEnumerations[0].Name == "ab");
            //cfg.Model.GroupEnumerations[0].Validate();
            //Assert.False(cfg.Model.GroupEnumerations[0].HasErrors);

            //cfg.Model.GroupEnumerations[0].Name = "a b";
            //cfg.ValidateSubTreeFromNode(cfg);
            //Assert.IsTrue(cfg.ValidationCollection.Count == 1);
            //Assert.IsTrue(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            //Assert.IsTrue(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);

            //cfg.Model.GroupEnumerations.NodeAddNewSubNode();
            //cfg.Model.GroupEnumerations[0].Name = "ab";
            //cfg.Model.GroupEnumerations[1].Name = "ab";
            //cfg.ValidateSubTreeFromNode(cfg);
            //Assert.IsTrue(cfg.ValidationCollection.Count == 2);
            //Assert.IsTrue(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            //Assert.IsTrue(cfg.ValidationCollection[1].Severity == FluentValidation.Severity.Error);
            //Assert.IsTrue(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            //Assert.IsTrue(cfg.ValidationCollection[1].Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            //Assert.IsTrue(cfg.Model.GroupEnumerations[1].ValidationCollection.Count == 1);
            //Assert.IsTrue(cfg.Model.GroupEnumerations[1].ValidationCollection[0].Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            //Assert.IsTrue(cfg.Model.GroupEnumerations[1].HasErrors == true);
            //var errenum = cfg.Model.GroupEnumerations[1].GetErrors("Name").GetEnumerator();
            //Assert.IsTrue(errenum.MoveNext() == true);
            //Assert.IsTrue((string)errenum.Current == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            //Assert.IsTrue(errenum.MoveNext() == false);
        }
        #endregion Compare Tree

        #region Register
        [TestMethod]
        public async System.Threading.Tasks.Task Register_Turnover_Mapping()
        {
            string regName = "reg1";
            string cat1Name = "cat1";
            string cat2Name = "cat2";
            string docName = "doc1";

            var cancellation = new CancellationTokenSource();
            var token = cancellation.Token;

            var vm = this.CreateVM();
            var cfg = vm.Config;

            var c1 = vm.Config.Model.GroupCatalogs.AddCatalog(cat1Name);
            var c2 = vm.Config.Model.GroupCatalogs.AddCatalog(cat2Name);

            var d = vm.Config.Model.GroupDocuments.AddDocument(docName);
            var seq = vm.Config.Model.GroupDocuments.GroupListSequences.AddSequence("Seq");
            d.SequenceGuid = seq.Guid;
            var pMoney = d.AddPropertyNumerical("Money", 19, 2);
            var pQty = d.AddPropertyNumerical("Qty", 17, 3);

            var gr = vm.Config.Model.GroupDocuments.GroupRegisters;
            var r = gr.AddRegister(regName);
            r.RegisterType = EnumRegisterType.TURNOVER;
            r.RegisterBalancePeriodicity = EnumRegisterBalancePeriodicity.REGISTER_PERIOD_DAY;
            r.ListSelectedDocuments.Add(d);
            Assert.HasCount(1, r.ListObjectDocRefs);
            r.SelectedDoc = d;

            // 1. Can find doc numerical property to map register property.
            r.PropertyQtyAccumulatorLength = 28;
            r.PropertyQtyAccumulatorAccuracy = 4;
            r.PropertyMoneyAccumulatorLength = 28;
            r.PropertyMoneyAccumulatorAccuracy = 4;
            Register.UpdateListMappings(r, d);
            Assert.HasCount(2, r.ListMappings);
            var mrec = r.ListMappings.Single(m => m.Name == r.PropertyQtyAccumulatorName);
            mrec = r.ListMappings.Single(m => m.Name == r.PropertyMoneyAccumulatorName);
            var regtmp = mrec.ListToMap.Single(m => m.Name == pMoney.Name);
            regtmp = mrec.ListToMap.Single(m => m.Name == pQty.Name);

            var s_qty5_2 = cfg.Model.GroupDocuments.DocumentTimeline.AddPropertyNumerical("qty", 15, 1);
            Register.UpdateListMappings(r, d);
            mrec = r.ListMappings.Single(m => m.Name == r.PropertyQtyAccumulatorName);
            Assert.HasCount(3, mrec.ListToMap);
            regtmp = mrec.ListToMap.Single(m => m.Name == pMoney.Name);
            regtmp = mrec.ListToMap.Single(m => m.Name == pQty.Name);
            regtmp = mrec.ListToMap.Single(m => m.Name == s_qty5_2.Name);

            // Length of doc property has to be less or equal than numerical register property length.
            r.PropertyQtyAccumulatorLength = 16;
            r.PropertyMoneyAccumulatorLength = 16;
            Register.UpdateListMappings(r, d);
            mrec = r.ListMappings.Single(m => m.Name == r.PropertyQtyAccumulatorName);
            regtmp = mrec.ListToMap.Single(m => m.Name == s_qty5_2.Name);
            Assert.HasCount(1, mrec.ListToMap);
            mrec = r.ListMappings.Single(m => m.Name == r.PropertyMoneyAccumulatorName);
            regtmp = mrec.ListToMap.Single(m => m.Name == s_qty5_2.Name);
            Assert.HasCount(1, mrec.ListToMap);

            r.PropertyQtyAccumulatorLength = 17;
            r.PropertyMoneyAccumulatorLength = 17;
            Register.UpdateListMappings(r, d);
            mrec = r.ListMappings.Single(m => m.Name == r.PropertyQtyAccumulatorName);
            Assert.HasCount(2, mrec.ListToMap);
            regtmp = mrec.ListToMap.Single(m => m.Name == pQty.Name);
            regtmp = mrec.ListToMap.Single(m => m.Name == s_qty5_2.Name);
            mrec = r.ListMappings.Single(m => m.Name == r.PropertyMoneyAccumulatorName);
            Assert.HasCount(2, mrec.ListToMap);
            regtmp = mrec.ListToMap.Single(m => m.Name == pQty.Name);
            regtmp = mrec.ListToMap.Single(m => m.Name == s_qty5_2.Name);

            r.PropertyQtyAccumulatorLength = 28;
            r.PropertyQtyAccumulatorAccuracy = 4;
            r.PropertyMoneyAccumulatorLength = 28;
            r.PropertyMoneyAccumulatorAccuracy = 4;
            // Accuracy of doc property has to be less or equal than numerical register property accuracy.
            r.PropertyQtyAccumulatorAccuracy = 1;
            r.PropertyMoneyAccumulatorAccuracy = 1;
            Register.UpdateListMappings(r, d);
            mrec = r.ListMappings.Single(m => m.Name == r.PropertyQtyAccumulatorName);
            Assert.HasCount(1, mrec.ListToMap);
            regtmp = mrec.ListToMap.Single(m => m.Name == s_qty5_2.Name);
            mrec = r.ListMappings.Single(m => m.Name == r.PropertyMoneyAccumulatorName);
            Assert.HasCount(1, mrec.ListToMap);
            regtmp = mrec.ListToMap.Single(m => m.Name == s_qty5_2.Name);

            r.PropertyQtyAccumulatorAccuracy = 2;
            r.PropertyMoneyAccumulatorAccuracy = 2;
            Register.UpdateListMappings(r, d);
            mrec = r.ListMappings.Single(m => m.Name == r.PropertyQtyAccumulatorName);
            Assert.HasCount(2, mrec.ListToMap);
            regtmp = mrec.ListToMap.Single(m => m.Name == pMoney.Name);
            regtmp = mrec.ListToMap.Single(m => m.Name == s_qty5_2.Name);
            mrec = r.ListMappings.Single(m => m.Name == r.PropertyMoneyAccumulatorName);
            Assert.HasCount(2, mrec.ListToMap);
            regtmp = mrec.ListToMap.Single(m => m.Name == pMoney.Name);
            regtmp = mrec.ListToMap.Single(m => m.Name == s_qty5_2.Name);

            // 2. Can find doc shared property to map register dimension 
            var pSharedC1 = d.ParentGroupListDocuments.ParentGroupDocuments.DocumentTimeline.AddPropertyCatalog("pSharedC1", c1);
            r.AddDimension(pSharedC1.Name, c1);
            Register.UpdateListMappings(r, d);
            Assert.HasCount(3, r.ListMappings);
            mrec = r.ListMappings.Single(m => m.Name == pSharedC1.Name);
            Assert.HasCount(1, mrec.ListToMap);
            regtmp = mrec.ListToMap.Single(m => m.Name == pSharedC1.Name);

            // 3. Can find doc property to map register dimension 
            var pSharedC2 = d.AddPropertyCatalog("pSharedC2", c2.Guid);
            r.AddDimension(pSharedC2.Name, c2);
            Register.UpdateListMappings(r, d);
            Assert.HasCount(4, r.ListMappings);
            mrec = r.ListMappings.Single(m => m.Name == pSharedC2.Name);
            Assert.HasCount(1, mrec.ListToMap);
            regtmp = mrec.ListToMap.Single(m => m.Name == pSharedC2.Name);

            // 4. Can find doc string property to map register property.

            //r.GroupProperties.AddProperty("str5", EnumDataType.STRING, 5, 0);
            //r.GroupProperties.AddProperty("str6", EnumDataType.STRING, 6, 0);
            //r.GroupProperties.AddProperty("str7", EnumDataType.STRING, 7, 0);
            //r.UpdateListMappings();
            //Assert.AreEqual(7, r.ListMappings.Count);

            // Length of doc property has to be less or equal than register property length.

            // 5. Can find doc string property to map register attached property.
            // Length of doc property has to be less or equal than register property length.

            // 6. Can find doc catalog property to map register attached property.

            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
        }
        #endregion Register

        #region Db table names
        [TestMethod]
        public void DbName001_Catalog()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;

            cfg.Model.IsUseNameComposition = false;
            cfg.Model.GroupCatalogs.PrefixForCompositionNames = "Cat";
            var c = cfg.Model.GroupCatalogs.AddCatalog("Test1");
            Assert.AreEqual(cfg.Model.GroupCatalogs[0].Name, cfg.Model.GroupCatalogs[0].CompositeName);
            cfg.Model.IsUseNameComposition = true;
            Assert.AreEqual(cfg.Model.GroupCatalogs.PrefixForCompositionNames + cfg.Model.GroupCatalogs[0].Name, cfg.Model.GroupCatalogs[0].CompositeName);
            cfg.Model.IsUseShortIdComposition = true;
            Assert.AreEqual("c1", c.NameShortId);
            cfg.Model.IsTryUseNameCompositionIfPossible = true;
            Assert.AreEqual("c1", c.NameShortId);

            cfg.Model.IsUseNameComposition = false;
            var t = cfg.Model.GroupCatalogs[0].GroupDetails.AddPropertiesTab("Tab1");
            Assert.AreEqual(cfg.Model.GroupCatalogs[0].Name, cfg.Model.GroupCatalogs[0].CompositeName);
            Assert.AreEqual(cfg.Model.GroupCatalogs[0].GroupDetails[0].Name, cfg.Model.GroupCatalogs[0].GroupDetails[0].CompositeName);
            cfg.Model.IsUseNameComposition = true;
            Assert.AreEqual(cfg.Model.GroupCatalogs.PrefixForCompositionNames + cfg.Model.GroupCatalogs[0].Name, cfg.Model.GroupCatalogs[0].CompositeName);
            Assert.AreEqual(cfg.Model.GroupCatalogs.PrefixForCompositionNames + cfg.Model.GroupCatalogs[0].Name + cfg.Model.GroupCatalogs[0].GroupDetails[0].Name,
                cfg.Model.GroupCatalogs[0].GroupDetails[0].CompositeName);
            cfg.Model.IsUseShortIdComposition = true;
            Assert.AreEqual("c1t2", t.NameShortId);
            cfg.Model.IsTryUseNameCompositionIfPossible = true;
            Assert.AreEqual("c1t2", t.NameShortId);

        }
        [TestMethod]
        public void DbName002_Document()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;

            cfg.Model.IsUseNameComposition = false;
            cfg.Model.GroupDocuments.PrefixForCompositionNames = "Cat";
            var d =cfg.Model.GroupDocuments.GroupListDocuments.AddDocument("Test1");
            Assert.AreEqual(cfg.Model.GroupDocuments.GroupListDocuments[0].Name, cfg.Model.GroupDocuments.GroupListDocuments[0].CompositeName);
            cfg.Model.IsUseNameComposition = true;
            Assert.AreEqual(cfg.Model.GroupDocuments.PrefixForCompositionNames + cfg.Model.GroupDocuments.GroupListDocuments[0].Name, cfg.Model.GroupDocuments.GroupListDocuments[0].CompositeName);
            Assert.AreEqual("d1", d.NameShortId);
            cfg.Model.IsTryUseNameCompositionIfPossible = true;
            Assert.AreEqual("d1", d.NameShortId);

            cfg.Model.IsUseNameComposition = false;
            var t = cfg.Model.GroupDocuments.GroupListDocuments[0].GroupDetails.AddPropertiesTab("Tab1");
            Assert.AreEqual(cfg.Model.GroupDocuments.GroupListDocuments[0].Name, cfg.Model.GroupDocuments.GroupListDocuments[0].CompositeName);
            Assert.AreEqual(cfg.Model.GroupDocuments.GroupListDocuments[0].GroupDetails[0].Name, cfg.Model.GroupDocuments.GroupListDocuments[0].GroupDetails[0].CompositeName);
            cfg.Model.IsUseNameComposition = true;
            Assert.AreEqual(cfg.Model.GroupDocuments.PrefixForCompositionNames + cfg.Model.GroupDocuments.GroupListDocuments[0].Name, cfg.Model.GroupDocuments.GroupListDocuments[0].CompositeName);
            Assert.AreEqual(cfg.Model.GroupDocuments.PrefixForCompositionNames + cfg.Model.GroupDocuments.GroupListDocuments[0].Name + cfg.Model.GroupDocuments.GroupListDocuments[0].GroupDetails[0].Name,
                cfg.Model.GroupDocuments.GroupListDocuments[0].GroupDetails[0].CompositeName);
            Assert.AreEqual("d1t2", t.NameShortId);
            cfg.Model.IsTryUseNameCompositionIfPossible = true;
            Assert.AreEqual("d1t2", t.NameShortId);
        }
        [TestMethod]
        public void DbName003_UniqueDbNamesValidation()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;

            cfg.Model.IsUseNameComposition = false;
            cfg.Model.GroupCatalogs.PrefixForCompositionNames = "Cat";
            cfg.Model.GroupCatalogs.AddCatalog("Test1");
            cfg.Model.Validate();
            Assert.IsEmpty(cfg.Model.ValidationCollection);

            cfg.Model.GroupDocuments.GroupListDocuments.AddDocument("Test1");
            cfg.Model.Validate();
            Assert.HasCount(1, cfg.Model.ValidationCollection);

            cfg.Model.GroupCatalogs.ListCatalogs[0].GroupDetails.AddTab("Test1");
            cfg.Model.Validate();
            Assert.HasCount(2, cfg.Model.ValidationCollection);

            cfg.Model.IsUseNameComposition = true;
            cfg.Model.Validate();
            Assert.IsEmpty(cfg.Model.ValidationCollection);
        }

        #endregion Db table names
        [TestMethod]
        public void ShortId()
        {
            const int nbits = 26; // bits for short ID
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            Assert.AreEqual(0u, cfg.Model.LastTypeShortRefId);

            var cg = cfg.Model.GroupConstantGroups.AddGroupConstants("CnstGroup1");
            Assert.AreEqual(1u, cfg.Model.LastTypeShortRefId);
            Assert.AreEqual(1u, cg.ShortId);

            var cnst = cg.AddConstant("Cnst");
            Assert.AreEqual(2u, cfg.Model.LastTypeShortRefId);
            Assert.AreEqual(2u, cnst.ShortId);
            Assert.AreEqual(2u + (1u << nbits), cnst.ShortRefId);

            var c1 = cfg.Model.GroupCatalogs.AddCatalog("Cat1");
            Assert.AreEqual(3u, cfg.Model.LastTypeShortRefId);
            Assert.AreEqual(3u, c1.ShortId);
            Assert.AreEqual(3u + (2u << nbits), c1.ShortRefId);

            var t1 = cfg.Model.GroupCatalogs[0].GroupDetails.AddPropertiesTab("CatTab1");
            Assert.AreEqual(4u, cfg.Model.LastTypeShortRefId);
            Assert.AreEqual(4u, t1.ShortId);
            Assert.AreEqual(4u + (3u << nbits), t1.ShortRefId);

            var d1 = cfg.Model.GroupDocuments.AddDocument("Doc1");
            Assert.AreEqual(5u, cfg.Model.LastTypeShortRefId);
            Assert.AreEqual(5u, d1.ShortId);
            Assert.AreEqual(5u + (7u << nbits), d1.ShortRefId);

            var t2 = cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments[0].GroupDetails.AddPropertiesTab("DocTab1");
            Assert.AreEqual(6u, cfg.Model.LastTypeShortRefId);
            Assert.AreEqual(6u, t2.ShortId);
            Assert.AreEqual(6u + (8u << nbits), t2.ShortRefId);

            string json = cfg.ExportToJson();
            var cfg2 = new Config(json);
            Assert.AreEqual(3u, cfg2.Model.GroupCatalogs.ListCatalogs[0].ShortId);
            Assert.AreEqual(3u + (2u << nbits), cfg2.Model.GroupCatalogs.ListCatalogs[0].ShortRefId);
            Assert.AreEqual(4u, cfg2.Model.GroupCatalogs.ListCatalogs[0].GroupDetails.ListDetails[0].ShortId);
            Assert.AreEqual(4u + (3u << nbits), cfg2.Model.GroupCatalogs.ListCatalogs[0].GroupDetails.ListDetails[0].ShortRefId);
            Assert.AreEqual(5u, cfg2.Model.GroupDocuments.GroupListDocuments.ListDocuments[0].ShortId);
            Assert.AreEqual(5u + (7u << nbits), cfg2.Model.GroupDocuments.GroupListDocuments.ListDocuments[0].ShortRefId);
            Assert.AreEqual(6u, cfg2.Model.GroupDocuments.GroupListDocuments.ListDocuments[0].GroupDetails.ListDetails[0].ShortId);
            Assert.AreEqual(6u + (8u << nbits), cfg2.Model.GroupDocuments.GroupListDocuments.ListDocuments[0].GroupDetails.ListDetails[0].ShortRefId);

            Assert.AreEqual(6u, cfg2.Model.LastTypeShortRefId);
        }
        [TestMethod]
        public async Task RelationTests()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            // Use History is saved and restored simple data
            // Use History is saved and restored complex OneToOne data
            // Use History is saved and restored complex OneToMany data
            // Use History is saved and restored complex ManyToMany data


            #region One To One
            Assert.IsEmpty(cfg.Model.GroupRelations.GroupListOneToOneRelations.ListRelations);
            var c1 = cfg.Model.GroupCatalogs.AddCatalog("cat");
            var d2 = cfg.Model.GroupDocuments.AddDocument("test_doc");
            var seq = cfg.Model.GroupDocuments.GroupListSequences.AddSequence("seq");
            d2.SequenceGuid = seq.Guid;

            // 1. EnumOneToOneRefType.ONE_TO_ONE_REF_BOTH_DIRECTIONS
            // without history, not optimistic
            var rel = cfg.Model.GroupRelations.GroupListOneToOneRelations.AddRelation("test_one_to_one_rel", c1, d2, false);
            await vm.BtnConfigValidateAsync.ExecuteAsync();
            Assert.AreEqual(0, vm.Config.CountErrors);
            var lst = rel.GetIncludedProperties(null, false, false);
            Assert.IsEmpty(lst);
            // RefCat2
            lst = c1.GetIncludedProperties(null, false, true);
            Assert.HasCount(3, lst);
            Assert.AreEqual(EnumDataType.DOCUMENT, lst[0].DataType.DataTypeEnum);
            Assert.AreEqual("test_one_to_one_rel", lst[0].Name);
            // RefCat1
            lst = d2.GetIncludedProperties(null, false, true);
            Assert.HasCount(2, lst);
            Assert.AreEqual(EnumDataType.CATALOG, lst[1].DataType.DataTypeEnum);
            Assert.AreEqual("test_one_to_one_rel", lst[1].Name);

            // 2.
            rel.RefType = EnumOneToOneRefType.ONE_TO_ONE_REF_FROM_FIRST_TO_SECOND_ONLY;
            await vm.BtnConfigValidateAsync.ExecuteAsync();
            Assert.AreEqual(0, vm.Config.CountErrors);
            lst = rel.GetIncludedProperties(null, false, false);
            Assert.IsEmpty(lst);
            // RefCat2
            lst = c1.GetIncludedProperties(null, false, true);
            Assert.HasCount(3, lst);
            Assert.AreEqual(EnumDataType.DOCUMENT, lst[0].DataType.DataTypeEnum);
            Assert.AreEqual("test_one_to_one_rel", lst[0].Name);
            // nothing
            lst = d2.GetIncludedProperties(null, false, true);
            Assert.HasCount(1, lst);

            // 3.
            rel.RefType = EnumOneToOneRefType.ONE_TO_ONE_REF_FROM_SECOND_TO_FIRST_ONLY;
            await vm.BtnConfigValidateAsync.ExecuteAsync();
            Assert.AreEqual(0, vm.Config.CountErrors);
            lst = rel.GetIncludedProperties(null, false, false);
            Assert.IsEmpty(lst);
            // nothing
            lst = c1.GetIncludedProperties(null, false, true);
            Assert.HasCount(2, lst);
            // RefCat1
            lst = d2.GetIncludedProperties(null, false, true);
            Assert.HasCount(2, lst);
            Assert.AreEqual(EnumDataType.CATALOG, lst[1].DataType.DataTypeEnum);
            Assert.AreEqual("test_one_to_one_rel", lst[1].Name);
            #endregion One To One

            #region Many To Many
            Assert.IsEmpty(cfg.Model.GroupRelations.GroupListManyToManyRelations.ListRelations);
            c1 = cfg.Model.GroupCatalogs.AddCatalog("cat2");
            d2 = cfg.Model.GroupDocuments.AddDocument("test_doc2");
            var seq2 = cfg.Model.GroupDocuments.GroupListSequences.AddSequence("seq2");
            d2.SequenceGuid = seq2.Guid;

            // 1. EnumOneToOneRefType.ONE_TO_ONE_NOT_SELECTED
            // without history, not optimistic
            var rel2 = cfg.Model.GroupRelations.GroupListManyToManyRelations.AddRelation("test_many_to_many_rel", c1, d2, false);
            await vm.BtnConfigValidateAsync.ExecuteAsync();
            Assert.AreEqual(0, vm.Config.CountErrors);

            lst = c1.GetIncludedProperties(null, false, true);
            Assert.HasCount(2, lst);
            lst = d2.GetIncludedProperties(null, false, true);
            Assert.HasCount(1, lst);

            lst = rel2.GetIncludedProperties(null, false, true);
            Assert.HasCount(2, lst);
            var ptmp = lst.Single(n => n.Name == "test_many_to_many_rel" && n.DataType.DataTypeEnum == EnumDataType.CATALOG);
            ptmp = lst.Single(n => n.Name == "test_many_to_many_rel" && n.DataType.DataTypeEnum == EnumDataType.DOCUMENT);
            #endregion Many To Many
        }
    }
}
