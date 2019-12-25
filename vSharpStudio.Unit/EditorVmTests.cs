using System;
using System.Linq;
using vSharpStudio.vm.ViewModels;
using ViewModelBase;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vSharpStudio.common;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Extensions.Logging;

namespace vSharpStudio.Unit
{
    [TestClass]
    public class EditorVmTests
    {
        static EditorVmTests()
        {
            LoggerInit.Init();
        }
        private static Microsoft.Extensions.Logging.ILogger _logger;
        public EditorVmTests()
        {
            ViewModelBindable.isUnitTests = true;
            if (_logger == null)
                //_logger = Logger.ServiceProvider.GetRequiredService<ILogger<PluginTests>>();
                _logger = Logger.CreateLogger<PluginTests>();
        }

        internal static void InitLogging(object type)
        {
            if (ApplicationLogging.LogerProvider == null)
            {
                // Log.Logger = new LoggerConfiguration()
                //    .MinimumLevel.Debug()
                //    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                //    //.WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                //    .CreateLogger().ForContext(type.GetType());
                // var serviceCollection = new ServiceCollection();
                // var lp = serviceCollection.AddLogging(loggingBuilder =>
                // {
                //    //loggingBuilder.AddFilter((p) => { return p >= LogLevel.Trace; });
                //    //loggingBuilder.AddConsole((o) => { o.IncludeScopes = true; });
                //    loggingBuilder.AddSerilog();
                //    //loggingBuilder.AddConfiguration(new )
                //    //loggingBuilder.AddDebug();
                // }).BuildServiceProvider().GetRequiredService<ILoggerProvider>();
                // ApplicationLogging.LogerProvider = lp;
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                    // .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                    .CreateLogger();
                ApplicationLogging.LogerProvider = new SerilogLoggerProvider(Log.Logger);
            }
        }

        #region SortedCollection
        public partial class TestValidator : ValidatorBase<TestSortable, TestValidator> { }

        [DebuggerDisplay("{Name} {SortingValue} Guid:{Guid,nq}")]
        public class TestSortable : ConfigObjectBase<TestSortable, TestValidator>
        {
            public TestSortable()
                : base(null, TestValidator.Validator) { }
        }

        [TestMethod]
        public void SortedCollection001CanSort()
        {
            var sc = new SortedObservableCollection<TestSortable>();
            TestSortable t2 = new TestSortable();
            t2.Name = "t2";
            sc.Add(t2);
            TestSortable t1 = new TestSortable();
            t1.Name = "t1";
            sc.Add(t1);
            TestSortable t3 = new TestSortable();
            t3.Name = "t3";
            sc.Add(t3);

            TestSortable t31 = new TestSortable();
            t31.Name = "t3";
            sc.Add(t31, 1);
            TestSortable t22 = new TestSortable();
            t22.Name = "t2";
            sc.Add(t22, 2);

            Assert.IsTrue(sc[0].Guid == t1.Guid);
            Assert.IsTrue(sc[1].Guid == t2.Guid);
            Assert.IsTrue(sc[2].Guid == t3.Guid);
            Assert.IsTrue(sc[3].Guid == t31.Guid);
            Assert.IsTrue(sc[4].Guid == t22.Guid);

            Assert.IsTrue(sc[0].Name == t1.Name);
            Assert.IsTrue(sc[1].Name == t2.Name);
            Assert.IsTrue(sc[2].Name == t3.Name);
            Assert.IsTrue(sc[3].Name == t31.Name);
            Assert.IsTrue(sc[4].Name == t22.Name);
        }
        #endregion SortedCollection

        #region Config
        [TestMethod]
        public void Config001GuidInit()
        {
            // Proto.Config.proto_config dto = new Proto.Config.proto_config();
            // Config cfg = new Config(dto);
            // Constant c = Constant.Create();
            // cfg.GroupConstants.Add(c);
            var cfg = new Config();
            Assert.IsTrue(cfg.Guid.Length > 0);
        }

        [TestMethod]
        public void Config002CanSaveAndRestore()
        {
            var cfg = new Config();
            cfg.Model.GroupConstants.NodeAddNewSubNode();
            string json = cfg.ExportToJson();
            Assert.IsTrue(json.Length > 0);
            var cfg2 = new Config(json);
            Assert.IsTrue(cfg2.Model.GroupConstants.Count() == 1);
            Assert.IsTrue(cfg2.Model.GroupConstants[0].Name == typeof(Constant).Name + 1);
        }

        [TestMethod]
        public void Config003CanSaveAndRestoreSortingValue()
        {
            var cfg = new Config();
            cfg.Model.GroupConstants.NodeAddNewSubNode();
            cfg.Model.GroupConstants.NodeAddNewSubNode();
            cfg.Model.GroupConstants[1].NodeMoveUp();
            string json = cfg.ExportToJson();
            Assert.IsTrue(json.Length > 0);
            var cfg2 = new Config(json);
            Assert.IsTrue(cfg2.Model.GroupConstants.Count() == 2);
            Assert.IsTrue(cfg2.Model.GroupConstants[0].Name == typeof(Constant).Name + 2);
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
            var cfg = new Constant((ITreeConfigNode)null);
            Assert.IsTrue(cfg.Guid.Length > 0);
        }

        [TestMethod]
        public void Constant002AddedParent()
        {
            var cfg = new Config();
            cfg.Model.GroupConstants.NodeAddNewSubNode();
            Assert.AreEqual(cfg.Model.GroupConstants[0].Parent.Guid, cfg.Model.GroupConstants.Guid);
            cfg.Model.GroupConstants[0].NodeAddNew();
            Assert.AreEqual(cfg.Model.GroupConstants[1].Parent.Guid, cfg.Model.GroupConstants.Guid);
        }

        [TestMethod]
        public void Constant003AddedDefaultName()
        {
            var cfg = new Config();
            cfg.Model.GroupConstants.NodeAddNewSubNode();
            Assert.AreEqual(Constant.DefaultName + "1", cfg.Model.GroupConstants[0].Name);
            cfg.Model.GroupConstants[0].NodeAddNew();
            Assert.AreEqual(Constant.DefaultName + "2", cfg.Model.GroupConstants[1].Name);
        }
        #endregion Constant

        #region Enum
        [TestMethod]
        public void Enum001GuidInit()
        {
            var cfg = new Enumeration((ITreeConfigNode)null);
            Assert.IsTrue(cfg.Guid.Length > 0);
        }

        [TestMethod]
        public void Enum002AddedParent()
        {
            var cfg = new Config();
            cfg.Model.GroupEnumerations.NodeAddNewSubNode();
            Assert.AreEqual(cfg.Model.GroupEnumerations[0].Parent.Guid, cfg.Model.GroupEnumerations.Guid);
            cfg.Model.GroupEnumerations[0].NodeAddNew();
            Assert.AreEqual(cfg.Model.GroupEnumerations[1].Parent.Guid, cfg.Model.GroupEnumerations.Guid);
        }
        #endregion Enum

        #region Property
        [TestMethod]
        public void Property001GuidInit()
        {
            var cfg = new Property((ITreeConfigNode)null);
            Assert.IsTrue(cfg.Guid.Length > 0);
        }
        #endregion Property

        #region Catalog
        [TestMethod]
        public void Catalog001GuidInit()
        {
            var cfg = new Config();
            var c = cfg.Model.GroupCatalogs.AddCatalog();
            Assert.IsTrue(c.Guid.Length > 0);
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
        [TestMethod]
        public void ITreeConfigNode001_UpdateSortingValueWhenNameIsChanged()
        {
            var cfg = new Config();
            ViewModelBindable.isNotValidateForUnitTests = true;

            var cnst = new Constant(cfg.Model.GroupConstants);
            cfg.Model.GroupConstants.Add(cnst);
            var curr = cnst.SortingValue;
            cnst.Name = "abc1";
            Assert.IsTrue(cnst.SortingValue != curr);
            curr = cnst.SortingValue;
            cnst.Name = "ABC1";
            Assert.IsTrue(cnst.SortingValue == curr);

            cnst.Name = "_0";
            curr = cnst.SortingValue;
            cnst.Name = "00";
            Assert.IsTrue(cnst.SortingValue > curr);

            cnst.Name = "_";
            curr = cnst.SortingValue;
            cnst.Name = "0";
            Assert.IsTrue(cnst.SortingValue > curr);

            cnst.Name = "0";
            curr = cnst.SortingValue;
            cnst.Name = "1";
            Assert.IsTrue(cnst.SortingValue > curr);

            cnst.Name = "9";
            curr = cnst.SortingValue;
            cnst.Name = "A";
            Assert.IsTrue(cnst.SortingValue > curr);

            cnst.Name = "A";
            curr = cnst.SortingValue;
            cnst.Name = "B";
            Assert.IsTrue(cnst.SortingValue > curr);

            cnst.Name = "A";
            curr = cnst.SortingValue;
            cnst.Name = "a";
            Assert.IsTrue(cnst.SortingValue == curr);

            // cnst.Name = "__";
            cnst.Name = "_z";
            curr = cnst.SortingValue;
            cnst.Name = "0_";
            Assert.IsTrue(cnst.SortingValue > curr);

            cnst.Name = "ABC1";
            curr = cnst.SortingValue;
            cnst.Name = "BBC1";
            Assert.IsTrue(cnst.SortingValue > curr);
            cnst.Name = "ACC1";
            Assert.IsTrue(cnst.SortingValue > curr);
            cnst.Name = "ABD1";
            Assert.IsTrue(cnst.SortingValue > curr);
            cnst.Name = "ABC2";
            Assert.IsTrue(cnst.SortingValue > curr);

            cnst.Name = "ABC0";
            Assert.IsTrue(cnst.SortingValue < curr);
            cnst.Name = "ABB1";
            Assert.IsTrue(cnst.SortingValue < curr);
            cnst.Name = "AAC1";
            Assert.IsTrue(cnst.SortingValue < curr);
        }

        [TestMethod]
        public void ITreeConfigNode002_RestoreSortingValueWhenObjectRestoredFromFile()
        {
            var cfg = new Config();
            var cnst = new Constant(cfg.Model.GroupConstants);
            cfg.Model.GroupConstants.Add(cnst);
            cnst.Name = "abc1";
            var curr = cnst.SortingValue;

            string json = cfg.ExportToJson();
            var cfg2 = new Config(json);

            Assert.IsTrue(cfg2.Model.GroupConstants[0].Name == cnst.Name);
            Assert.IsTrue(cfg2.Model.GroupConstants[0].SortingValue == cnst.SortingValue);
        }

        [TestMethod]
        public void ITreeConfigNode003_ReSortedWhenSortingValueIsChanged()
        {
            var cfg = new Config();
            var cnst = new Constant(cfg.Model.GroupConstants);
            cfg.Model.GroupConstants.Add(cnst);
            cnst.Name = "abc1";

            var cnst2 = new Constant(cfg.Model.GroupConstants);
            cfg.Model.GroupConstants.Add(cnst2);
            cnst2.Name = "abc1";

            Assert.IsTrue(cnst.Guid != cnst2.Guid);

            cnst2.Name = "abc0";
            Assert.IsTrue(cfg.Model.GroupConstants[0].SortingValue < cfg.Model.GroupConstants[1].SortingValue);
            Assert.IsTrue(cfg.Model.GroupConstants[1].Guid == cnst.Guid);
            Assert.IsTrue(cfg.Model.GroupConstants[0].Guid == cnst2.Guid);

            cnst2.Name = "abc2";
            Assert.IsTrue(cfg.Model.GroupConstants[0].SortingValue < cfg.Model.GroupConstants[1].SortingValue);
            Assert.IsTrue(cfg.Model.GroupConstants[0].Guid == cnst.Guid);
            Assert.IsTrue(cfg.Model.GroupConstants[1].Guid == cnst2.Guid);
        }

        [TestMethod]
        public void ITreeConfigNode003_CanConfigTreeCommands()
        {
            var cfg = new Config();

            #region Constants

            Assert.IsTrue(cfg.Model.GroupConstants.NodeCanLeft() == false);
            Assert.IsTrue(cfg.Model.GroupConstants.NodeCanRight() == true);
            Assert.IsTrue(cfg.Model.GroupConstants.NodeCanMoveUp() == false);
            Assert.IsTrue(cfg.Model.GroupConstants.NodeCanMoveDown() == false);
            Assert.IsTrue(cfg.Model.GroupConstants.NodeCanAddNew() == false);
            Assert.IsTrue(cfg.Model.GroupConstants.NodeCanAddNewSubNode() == true);
            Assert.IsTrue(cfg.SelectedNode == null);
            cfg.Model.GroupConstants.NodeAddNewSubNode();
            Assert.IsTrue(cfg.SelectedNode != null);
            Assert.IsTrue(cfg.SelectedNode == cfg.Model.GroupConstants[0]);
            Assert.IsTrue(cfg.SelectedNode.Guid == cfg.Model.GroupConstants[0].Guid);

            Assert.IsTrue(cfg.Model.GroupConstants.NodeCanRight() == true);
            Assert.IsTrue(cfg.Model.GroupConstants[0].NodeCanLeft() == true);
            Assert.IsTrue(cfg.Model.GroupConstants[0].NodeCanRight() == false);
            Assert.IsTrue(cfg.Model.GroupConstants[0].NodeCanMoveUp() == false);
            Assert.IsTrue(cfg.Model.GroupConstants[0].NodeCanMoveDown() == false);
            Assert.IsTrue(cfg.Model.GroupConstants[0].NodeCanAddNew() == true);
            Assert.IsTrue(cfg.Model.GroupConstants[0].NodeCanAddNewSubNode() == false);

            cfg.Model.GroupConstants.NodeAddNewSubNode();
            Assert.IsTrue(cfg.Model.GroupConstants[0].NodeCanMoveUp() == false);
            Assert.IsTrue(cfg.Model.GroupConstants[0].NodeCanMoveDown() == true);
            Assert.IsTrue(cfg.Model.GroupConstants[1].NodeCanMoveUp() == true);
            Assert.IsTrue(cfg.Model.GroupConstants[1].NodeCanMoveDown() == false);

            #endregion Constants

            #region Enumerations

            Assert.IsTrue(cfg.Model.GroupEnumerations.NodeCanLeft() == false);
            Assert.IsTrue(cfg.Model.GroupEnumerations.NodeCanRight() == true);
            Assert.IsTrue(cfg.Model.GroupEnumerations.NodeCanMoveUp() == false);
            Assert.IsTrue(cfg.Model.GroupEnumerations.NodeCanMoveDown() == false);
            Assert.IsTrue(cfg.Model.GroupEnumerations.NodeCanAddNew() == false);
            Assert.IsTrue(cfg.Model.GroupEnumerations.NodeCanAddNewSubNode() == true);
            cfg.Model.GroupEnumerations.NodeAddNewSubNode();
            Assert.IsTrue(cfg.SelectedNode != null);
            Assert.IsTrue(cfg.SelectedNode == cfg.Model.GroupEnumerations[0]);
            Assert.IsTrue(cfg.SelectedNode.Guid == cfg.Model.GroupEnumerations[0].Guid);

            Assert.IsTrue(cfg.Model.GroupEnumerations.NodeCanRight() == true);
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].NodeCanLeft() == true);
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].NodeCanRight() == true);
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].NodeCanMoveUp() == false);
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].NodeCanMoveDown() == false);
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].NodeCanAddNew() == true);
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].NodeCanAddNewSubNode() == false);

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

            Assert.IsTrue(cfg.Model.GroupCatalogs.NodeCanLeft() == false);
            Assert.IsTrue(cfg.Model.GroupCatalogs.NodeCanRight() == true);
            Assert.IsTrue(cfg.Model.GroupCatalogs.NodeCanMoveUp() == false);
            Assert.IsTrue(cfg.Model.GroupCatalogs.NodeCanMoveDown() == false);
            Assert.IsTrue(cfg.Model.GroupCatalogs.NodeCanAddNew() == false);
            Assert.IsTrue(cfg.Model.GroupCatalogs.NodeCanAddNewSubNode() == true);
            cfg.Model.GroupCatalogs.NodeAddNewSubNode();
            Assert.IsTrue(cfg.SelectedNode != null);
            Assert.IsTrue(cfg.SelectedNode == cfg.Model.GroupCatalogs[0]);
            Assert.IsTrue(cfg.SelectedNode.Guid == cfg.Model.GroupCatalogs[0].Guid);

            Assert.IsTrue(cfg.Model.GroupCatalogs.NodeCanRight() == true);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].NodeCanLeft() == true);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].NodeCanRight() == true);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].NodeCanMoveUp() == false);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].NodeCanMoveDown() == false);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].NodeCanAddNew() == true);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].NodeCanAddNewSubNode() == false);

            #region Properties

            cfg.Model.GroupCatalogs[0].GroupProperties.NodeAddNewSubNode();
            Assert.IsTrue(cfg.SelectedNode != null);
            Assert.IsTrue(cfg.SelectedNode == cfg.Model.GroupCatalogs[0].GroupProperties[0]);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeCanLeft() == true);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeCanRight() == false);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeCanMoveUp() == false);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeCanMoveDown() == false);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeCanAddNew() == true);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeCanAddNewSubNode() == false);

            cfg.Model.GroupCatalogs[0].GroupProperties[0].NodeAddNew();
            Assert.IsTrue(cfg.SelectedNode == cfg.Model.GroupCatalogs[0].GroupProperties[1]);
            Assert.IsTrue(cfg.SelectedNode.Guid == cfg.Model.GroupCatalogs[0].GroupProperties[1].Guid);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[1].NodeCanLeft() == true);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[1].NodeCanRight() == false);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[1].NodeCanMoveUp() == true);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[1].NodeCanMoveDown() == false);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[1].NodeCanAddNew() == true);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[1].NodeCanAddNewSubNode() == false);

            var p = cfg.Model.GroupCatalogs[0].GroupProperties[1];
            p.NodeMoveUp();
            Assert.IsTrue(p == cfg.Model.GroupCatalogs[0].GroupProperties[0]);
            Assert.IsTrue(cfg.SelectedNode == cfg.Model.GroupCatalogs[0].GroupProperties[0]);

            // change property parameters
            // p.DataType.MinValue = 5;
            // p.DataType.MaxValue = 6;

            p.NodeAddClone();
            Assert.IsTrue(p == cfg.Model.GroupCatalogs[0].GroupProperties[2]);
            Assert.IsTrue(cfg.Model.GroupCatalogs[0].GroupProperties[2].Name == cfg.Model.GroupCatalogs[0].GroupProperties[0].Name + "2");
            // Assert.IsTrue(5 == cfg.Model.GroupCatalogs[0].GroupProperties.ListProperties[2].DataType.MinValue);
            // Assert.IsTrue(6 == cfg.Model.GroupCatalogs[0].GroupProperties.ListProperties[2].DataType.MaxValue);

            #endregion Properties

            #endregion Catalogs

        }
        #endregion ITreeConfigNode

        #region Compare Tree
        private Config createTree()
        {
            var cfg = new Config();

            cfg.Model.GroupEnumerations.NodeAddNewSubNode();
            cfg.Model.GroupEnumerations[0].DataTypeEnum = EnumEnumerationType.INTEGER_VALUE;
            cfg.Model.GroupEnumerations[0].ListEnumerationPairs.Add(new EnumerationPair(cfg.Model.GroupEnumerations[0]) { Name = "one", Value = "1" });

            cfg.Model.GroupConstants.NodeAddNewSubNode();
            cfg.Model.GroupConstants[0].DataType.DataTypeEnum = EnumDataType.BOOL;
            cfg.Model.GroupConstants.NodeAddNewSubNode();
            cfg.Model.GroupConstants[1].DataType.DataTypeEnum = EnumDataType.ENUMERATION;
            cfg.Model.GroupConstants[1].DataType.ObjectGuid = cfg.Model.GroupEnumerations[0].Guid;

            return cfg;
        }

        [TestMethod]
        public void Rules001_DataType()
        {
            var dt = new DataType();

            dt.Validate();
            Assert.IsTrue(dt.CountErrors == 0);
            Assert.IsTrue(dt.CountInfos == 0);
            Assert.IsTrue(dt.CountWarnings == 0);
            Assert.IsTrue(dt.ValidationCollection.Count == 0);
            // Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityObjectName == Visibility.Collapsed);

            dt.DataTypeEnum = EnumDataType.ANY;
            dt.Validate();
            Assert.IsTrue(dt.CountErrors == 0);
            Assert.IsTrue(dt.CountInfos == 0);
            Assert.IsTrue(dt.CountWarnings == 0);
            Assert.IsTrue(dt.ValidationCollection.Count == 0);
            // Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityObjectName == Visibility.Collapsed);

            dt.DataTypeEnum = EnumDataType.BOOL;
            dt.Validate();
            Assert.IsTrue(dt.CountErrors == 0);
            Assert.IsTrue(dt.CountInfos == 0);
            Assert.IsTrue(dt.CountWarnings == 0);
            Assert.IsTrue(dt.ValidationCollection.Count == 0);
            // Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityObjectName == Visibility.Collapsed);

            dt.DataTypeEnum = EnumDataType.CATALOG;
            dt.Validate();
            Assert.IsTrue(dt.CountErrors == 0);
            Assert.IsTrue(dt.CountInfos == 0);
            Assert.IsTrue(dt.CountWarnings == 0);
            Assert.IsTrue(dt.HasErrors);
            Assert.IsTrue(dt.ValidationCollection.Count == 1);
            Assert.IsTrue(dt.ValidationCollection[0].Message == Config.ValidationMessages.TYPE_EMPTY_CATALOG);
            // Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityObjectName == Visibility.Visible);

            dt.DataTypeEnum = EnumDataType.CATALOGS;
            dt.Validate();
            Assert.IsTrue(dt.CountErrors == 0);
            Assert.IsTrue(dt.CountInfos == 0);
            Assert.IsTrue(dt.CountWarnings == 0);
            Assert.IsTrue(dt.ValidationCollection.Count == 0);
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
            // Assert.IsTrue(dt.ValidationCollection[0].Message == Config.ValidationMessages.TYPE_EMPTY_CONSTANT_NAME);
            // Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityObjectName == Visibility.Visible);

            dt.DataTypeEnum = EnumDataType.ENUMERATION;
            dt.Validate();
            Assert.IsTrue(dt.CountErrors == 0);
            Assert.IsTrue(dt.CountInfos == 0);
            Assert.IsTrue(dt.CountWarnings == 0);
            Assert.IsTrue(dt.HasErrors);
            Assert.IsTrue(dt.ValidationCollection.Count == 1);
            Assert.IsTrue(dt.ValidationCollection[0].Message == Config.ValidationMessages.TYPE_EMPTY_ENUMERATION);
            // Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityObjectName == Visibility.Visible);

            dt.DataTypeEnum = EnumDataType.NUMERICAL;
            dt.Validate();
            Assert.IsTrue(dt.CountErrors == 0);
            Assert.IsTrue(dt.CountInfos == 0);
            Assert.IsTrue(dt.CountWarnings == 0);
            // Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Visible);
            // Assert.IsTrue(dt.VisibilityLength == Visibility.Visible);
            // Assert.IsTrue(dt.VisibilityObjectName == Visibility.Collapsed);

            dt.DataTypeEnum = EnumDataType.STRING;
            dt.Validate();
            Assert.IsTrue(dt.CountErrors == 0);
            Assert.IsTrue(dt.CountInfos == 0);
            Assert.IsTrue(dt.CountWarnings == 0);
            Assert.IsTrue(dt.ValidationCollection.Count == 0);
            // Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityLength == Visibility.Visible);
            // Assert.IsTrue(dt.VisibilityObjectName == Visibility.Collapsed);
        }

        [TestMethod]
        public void Rules002_Enumeration()
        {
            var cfg = this.createTree();
            cfg.SolutionPath = @"..\..\..\..\";
            cfg.ValidateSubTreeFromNode(cfg);
            Assert.IsTrue(cfg.CountErrors == 0);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.ValidationCollection.Count == 0);

            cfg.Model.GroupEnumerations[0].Name = "1a";
            cfg.ValidateSubTreeFromNode(cfg);
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].CountErrors == 1);
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].CountInfos == 0);
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].CountWarnings == 0);
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].HasErrors);
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].ValidationCollection.Count == 1);
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].ValidationCollection[0].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT);

            // intermediate node contains only validation count
            Assert.IsTrue(cfg.Model.GroupEnumerations.ValidationCollection.Count == 0);
            Assert.IsTrue(cfg.Model.GroupEnumerations.CountErrors == 1);
            Assert.IsTrue(cfg.Model.GroupEnumerations.CountInfos == 0);
            Assert.IsTrue(cfg.Model.GroupEnumerations.CountWarnings == 0);

            // ValidateSubTreeFromNode(node). node contains full list of validations
            Assert.IsTrue(cfg.CountErrors == 1);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.ValidationCollection.Count == 1);
            Assert.IsTrue(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            // Assert.IsTrue(cfg.ValidationCollection[1].Severity == FluentValidation.Severity.Error);
            if (cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT)
            {
                Assert.IsTrue(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT);
                // Assert.IsTrue(cfg.ValidationCollection[1].Message == Config.ValidationMessages.TYPE_OBJECT_IS_NOT_FOUND);
            }
            // else
            // {
            //    Assert.IsTrue(cfg.ValidationCollection[1].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT);
            //    Assert.IsTrue(cfg.ValidationCollection[0].Message == Config.ValidationMessages.TYPE_OBJECT_IS_NOT_FOUND);
            // }

            cfg.Model.GroupEnumerations[0].Name = " ab";
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].Name == "ab");
            cfg.Model.GroupEnumerations[0].Validate();
            Assert.IsFalse(cfg.Model.GroupEnumerations[0].HasErrors);

            cfg.Model.GroupEnumerations[0].Name = "ab ";
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].Name == "ab");
            cfg.Model.GroupEnumerations[0].Validate();
            Assert.IsFalse(cfg.Model.GroupEnumerations[0].HasErrors);

            cfg.Model.GroupEnumerations[0].Name = "a b";
            // cfg.Model.GroupConstants[1].DataType.ObjectName = "a b";
            cfg.ValidateSubTreeFromNode(cfg);
            Assert.IsTrue(cfg.ValidationCollection.Count == 1);
            Assert.IsTrue(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            Assert.IsTrue(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);

            cfg.Model.GroupEnumerations.NodeAddNewSubNode();
            cfg.Model.GroupEnumerations[0].Name = "ab";
            cfg.Model.GroupEnumerations[1].Name = "ab";
            // cfg.Model.GroupConstants[1].DataType.ObjectName = "ab";
            cfg.ValidateSubTreeFromNode(cfg);
            Assert.IsTrue(cfg.ValidationCollection.Count == 2);
            Assert.IsTrue((from p in cfg.ValidationCollection where p.Severity == FluentValidation.Severity.Error select p).ToList().Count() == 2);
            Assert.IsTrue((from p in cfg.ValidationCollection where p.Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE select p).ToList().Count() == 2);
            // Assert.IsTrue((from p in cfg.ValidationCollection where p.Message == Config.ValidationMessages.TYPE_LENGTH_GREATER_THAN_ZERO select p).ToList().Count() == 1);
            Assert.IsTrue(cfg.Model.GroupEnumerations[1].ValidationCollection.Count == 1);
            Assert.IsTrue(cfg.Model.GroupEnumerations[1].ValidationCollection[0].Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            Assert.IsTrue(cfg.Model.GroupEnumerations[1].HasErrors == true);
            var errenum = cfg.Model.GroupEnumerations[1].GetErrors("Name").GetEnumerator();
            Assert.IsTrue(errenum.MoveNext() == true);
            Assert.IsTrue((string)errenum.Current == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            Assert.IsTrue(errenum.MoveNext() == false);
        }

        [TestMethod]
        public void Rules003_Constant()
        {
            var cfg = this.createTree();
            cfg.SolutionPath = @"..\..\..\..\";

            cfg.ValidateSubTreeFromNode(cfg);
            Assert.IsTrue(cfg.CountErrors == 0);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.ValidationCollection.Count == 0);

            // string prev = cfg.Model.GroupConstants[0].DataType.ObjectName;
            // cfg.Model.GroupConstants[0].DataType.ObjectName = "123";
            // cfg.ValidateSubTreeFromNode(cfg);
            // Assert.IsTrue(cfg.CountErrors == 1);
            // Assert.IsTrue(cfg.CountInfos == 0);
            // Assert.IsTrue(cfg.CountWarnings == 0);
            // Assert.IsTrue(cfg.ValidationCollection.Count == 1);
            // Assert.IsTrue(cfg.Model.GroupConstants[0].CountErrors == 1);
            // Assert.IsTrue(cfg.Model.GroupConstants[0].CountInfos == 0);
            // Assert.IsTrue(cfg.Model.GroupConstants[0].CountWarnings == 0);
            // Assert.IsTrue(cfg.Model.GroupConstants[0].DataType.ValidationCollection.Count == 1);


            // cfg.Model.GroupConstants[0].DataType.ObjectName = prev;
            // cfg.Model.GroupEnumerations[0].Name = "1a";
            // cfg.ValidateSubTreeFromNode(cfg);
            // Assert.IsTrue(cfg.Model.GroupEnumerations[0].CountErrors == 1);
            // Assert.IsTrue(cfg.Model.GroupEnumerations[0].CountInfos == 0);
            // Assert.IsTrue(cfg.Model.GroupEnumerations[0].CountWarnings == 0);
            // Assert.IsTrue(cfg.Model.GroupEnumerations[0].HasErrors);
            // Assert.IsTrue(cfg.Model.GroupEnumerations[0].ValidationCollection.Count == 1);
            // Assert.IsTrue(cfg.Model.GroupEnumerations[0].ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            // Assert.IsTrue(cfg.Model.GroupEnumerations[0].ValidationCollection[0].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT);

            //// intermediate node contains only validation count
            // Assert.IsTrue(cfg.Model.GroupEnumerations.ValidationCollection.Count == 0);
            // Assert.IsTrue(cfg.Model.GroupEnumerations.CountErrors == 1);
            // Assert.IsTrue(cfg.Model.GroupEnumerations.CountInfos == 0);
            // Assert.IsTrue(cfg.Model.GroupEnumerations.CountWarnings == 0);

            //// ValidateSubTreeFromNode(node). node contains full list of validations
            // Assert.IsTrue(cfg.CountErrors == 1);
            // Assert.IsTrue(cfg.CountInfos == 0);
            // Assert.IsTrue(cfg.CountWarnings == 0);
            // Assert.IsTrue(cfg.ValidationCollection.Count == 1);
            // Assert.IsTrue(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            // Assert.IsTrue(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT);

            // cfg.Model.GroupEnumerations[0].Name = " ab";
            // Assert.IsTrue(cfg.Model.GroupEnumerations[0].Name == "ab");
            // cfg.Model.GroupEnumerations[0].Validate();
            // Assert.False(cfg.Model.GroupEnumerations[0].HasErrors);

            // cfg.Model.GroupEnumerations[0].Name = "ab ";
            // Assert.IsTrue(cfg.Model.GroupEnumerations[0].Name == "ab");
            // cfg.Model.GroupEnumerations[0].Validate();
            // Assert.False(cfg.Model.GroupEnumerations[0].HasErrors);

            // cfg.Model.GroupEnumerations[0].Name = "a b";
            // cfg.ValidateSubTreeFromNode(cfg);
            // Assert.IsTrue(cfg.ValidationCollection.Count == 1);
            // Assert.IsTrue(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            // Assert.IsTrue(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);

            // cfg.Model.GroupEnumerations.NodeAddNewSubNode();
            // cfg.Model.GroupEnumerations[0].Name = "ab";
            // cfg.Model.GroupEnumerations[1].Name = "ab";
            // cfg.ValidateSubTreeFromNode(cfg);
            // Assert.IsTrue(cfg.ValidationCollection.Count == 2);
            // Assert.IsTrue(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            // Assert.IsTrue(cfg.ValidationCollection[1].Severity == FluentValidation.Severity.Error);
            // Assert.IsTrue(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            // Assert.IsTrue(cfg.ValidationCollection[1].Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            // Assert.IsTrue(cfg.Model.GroupEnumerations[1].ValidationCollection.Count == 1);
            // Assert.IsTrue(cfg.Model.GroupEnumerations[1].ValidationCollection[0].Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            // Assert.IsTrue(cfg.Model.GroupEnumerations[1].HasErrors == true);
            // var errenum = cfg.Model.GroupEnumerations[1].GetErrors("Name").GetEnumerator();
            // Assert.IsTrue(errenum.MoveNext() == true);
            // Assert.IsTrue((string)errenum.Current == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            // Assert.IsTrue(errenum.MoveNext() == false);
        }
        #endregion Compare Tree
    }
}
