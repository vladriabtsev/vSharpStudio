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
using Microsoft.CodeAnalysis.Operations;
using NSubstitute;
using Xceed.Wpf.Toolkit;
using System.Threading;
using Newtonsoft.Json.Linq;
using vSharpStudio.ViewModels;
using Polly.Caching;
using ApplicationLogging;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Rename;

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
            VmBindable.isUnitTests = true;
            if (_logger == null)
                //_logger = Logger.ServiceProvider.GetRequiredService<ILogger<PluginTests>>();
                _logger = ApplicationLogging.Logger.CreateLogger<PluginTests>();
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

        #region SortedCollection
        public partial class TestValidator : ValidatorBase<TestSortable, TestValidator> { }

        [DebuggerDisplay("{Name} {SortingValue} Guid:{Guid,nq}")]
        public class TestSortable : ConfigObjectVmBase<TestSortable, TestValidator>, ITreeConfigNodeSortable
        {
            public TestSortable() : base(null, TestValidator.Validator) { }
            public string Guid // Property.tt Line: 58
            {
                get { return this._Guid; }
                set
                {
                    if (this._Guid != value)
                    {
                        this._Guid = value;
                        this.OnPropertyChanged();
                        this.ValidateProperty();
                        this.IsChanged = true;
                    }
                }
            }
            public string Name
            {
                get { return this._Name; }
                set
                {
                    if (this._Name != value)
                    {
                        this._Name = value;
                        this.OnPropertyChanged();
                        this.ValidateProperty();
                        this.IsChanged = true;
                    }
                }
            }
            public ulong SortingValue
            {
                get { return this._SortingValue; }
                set
                {
                    if (this._SortingValue != value)
                    {
                        this._SortingValue = value;
                        this.OnPropertyChanged();
                        this.ValidateProperty();
                        this.IsChanged = true;
                    }
                }
            }
            public void SetSortingValueField(ulong sortValue)
            {
                this._SortingValue = sortValue;
            }
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
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            Assert.IsTrue(cfg.Guid.Length > 0);
        }

        [TestMethod]
        public void Config002CanSaveAndRestore()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            var gr = cfg.Model.GroupConstantGroups.AddGroupConstants("Gr");
            gr.NodeAddNewSubNode();
            string json = cfg.ExportToJson();
            Assert.IsTrue(json.Length > 0);
            var cfg2 = new Config(json);
            Assert.IsTrue(cfg2.Model.GroupConstantGroups.ListConstantGroups.Count() == 1);
            Assert.IsTrue(cfg2.Model.GroupConstantGroups.ListConstantGroups[0].Name == "Gr");
            //Assert.IsTrue(cfg2.Model.GroupConstantGroups.ListConstantGroups[0].Name == typeof(Constant).Name + 1);
        }

        [TestMethod]
        public void Config003CanSaveAndRestoreSortingValue()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            var gr = cfg.Model.GroupConstantGroups.AddGroupConstants("Gr");
            gr.NodeAddNewSubNode();
            gr.NodeAddNewSubNode();
            cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[1].NodeMoveUp();
            string json = cfg.ExportToJson();
            Assert.IsTrue(json.Length > 0);
            var cfg2 = new Config(json);
            Assert.IsTrue(cfg2.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants.Count() == 2);
            Assert.IsTrue(cfg2.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].Name == typeof(Constant).Name + 2);
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
            Assert.IsTrue(c.Guid.Length > 0);
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
            cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].NodeAddNew();
            Assert.AreEqual(Defaults.ConstantName + "2", cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[1].Name);
        }
        #endregion Constant

        #region Enum
        [TestMethod]
        public void Enum001GuidInit()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            var en = cfg.Model.GroupEnumerations.NodeAddNewSubNode();
            Assert.IsTrue(en.Guid.Length > 0);
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
            Assert.IsTrue(c.Guid.Length > 0);
            var p = c.AddProperty("test");
            Assert.IsTrue(p.Guid.Length > 0);
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
        [TestMethod]
        public void ITreeConfigNode001_UpdateSortingValueWhenNameIsChanged()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            VmBindable.isNotValidateForUnitTests = true;
            var gc = cfg.Model.GroupConstantGroups.AddGroupConstants("Gr");
            var cnst = new Constant(gc);
            gc.Add(cnst);
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
            var curr = cnst.SortingValue;

            string json = cfg.ExportToJson();
            var cfg2 = new Config(json);

            Assert.IsTrue(cfg2.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].Name == cnst.Name);
            Assert.IsTrue(cfg2.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].SortingValue == cnst.SortingValue);
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

            Assert.IsTrue(cnst.Guid != cnst2.Guid);

            cnst2.Name = "abc0";
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].SortingValue < cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[1].SortingValue);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[1].Guid == cnst.Guid);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].Guid == cnst2.Guid);

            cnst2.Name = "abc2";
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].SortingValue < cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[1].SortingValue);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[0].Guid == cnst.Guid);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].ListConstants[1].Guid == cnst2.Guid);
        }

        [TestMethod]
        public void ITreeConfigNode003_CanConfigTreeCommands()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;

            #region Constants
            Assert.IsTrue(cfg.Model.GroupConstantGroups.NodeCanLeft() == true);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.NodeCanRight() == true);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.NodeCanMoveUp() == false);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.NodeCanMoveDown() == false);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.NodeCanAddNew() == false);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.NodeCanAddNewSubNode() == true);

            Assert.IsTrue(cfg.SelectedNode == null);
            var gr = cfg.Model.GroupConstantGroups.AddGroupConstants("Gr");
            Assert.IsTrue(cfg.SelectedNode != null);
            Assert.IsTrue(cfg.SelectedNode == gr);
            Assert.IsTrue(cfg.SelectedNode.Guid == gr.Guid);

            cfg.Model.GroupConstantGroups.AddGroupConstants("Gr2");
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].NodeCanLeft() == true);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].NodeCanRight() == true);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].NodeCanMoveUp() == false);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].NodeCanMoveDown() == true);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[1].NodeCanMoveUp() == true);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[1].NodeCanMoveDown() == false);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].NodeCanAddNew() == true);
            Assert.IsTrue(cfg.Model.GroupConstantGroups.ListConstantGroups[0].NodeCanAddNewSubNode() == true);

            var cnst = gr.NodeAddNewSubNode();
            var cnst2 = gr.NodeAddNewSubNode();
            Assert.IsTrue(cnst.NodeCanMoveUp() == false);
            Assert.IsTrue(cnst.NodeCanMoveDown() == true);
            Assert.IsTrue(cnst2.NodeCanMoveUp() == true);
            Assert.IsTrue(cnst2.NodeCanMoveDown() == false);

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
            Assert.IsTrue(cfg.Model.GroupEnumerations[0].NodeCanAddNewSubNode() == true);

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
            Assert.IsTrue(dt.CountErrors == 0);
            Assert.IsTrue(dt.CountInfos == 0);
            Assert.IsTrue(dt.CountWarnings == 0);
            Assert.IsTrue(dt.ValidationCollection.Count == 0);
            // Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityObjectName == Visibility.Collapsed);

            //dt.DataTypeEnum = EnumDataType.ANY;
            //dt.Validate();
            //Assert.IsTrue(dt.CountErrors == 0);
            //Assert.IsTrue(dt.CountInfos == 0);
            //Assert.IsTrue(dt.CountWarnings == 0);
            //Assert.IsTrue(dt.ValidationCollection.Count == 0);


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
            Assert.IsTrue(dt.ValidationCollection.Count == 2);
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
            // Assert.IsTrue(dt.ValidationCollection[0].Message == Config.ValidationMessages.TYPE_EMPTY_CONSTANTName);
            // Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            // Assert.IsTrue(dt.VisibilityObjectName == Visibility.Visible);

            dt.DataTypeEnum = EnumDataType.ENUMERATION;
            dt.Validate();
            Assert.IsTrue(dt.CountErrors == 0);
            Assert.IsTrue(dt.CountInfos == 0);
            Assert.IsTrue(dt.CountWarnings == 0);
            Assert.IsTrue(dt.HasErrors);
            Assert.IsTrue(dt.ValidationCollection.Count == 2);
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
        async public System.Threading.Tasks.Task Rules002_Enumeration()
        {
            var cancellation = new CancellationTokenSource();
            var token = cancellation.Token;

            var vm = this.CreateVM();
            var cfg = vm.Config;
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.IsTrue(cfg.CountErrors == 0);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.ValidationCollection.Count == 0);

            cfg.Model.GroupEnumerations[0].Name = "1a";
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
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
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.IsTrue(cfg.ValidationCollection.Count == 1);
            Assert.IsTrue(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            Assert.IsTrue(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);

            cfg.Model.GroupEnumerations.NodeAddNewSubNode();
            cfg.Model.GroupEnumerations[0].Name = "ab";
            cfg.Model.GroupEnumerations[1].Name = "ab";
            // cfg.Model.GroupConstants[1].DataType.ObjectName = "ab";
            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
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
        [Ignore]
        async public System.Threading.Tasks.Task Rules003_Constant()
        {
            var cancellation = new CancellationTokenSource();
            var token = cancellation.Token;

            var vm = this.CreateVM();
            var cfg = vm.Config;

            await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
            Assert.IsTrue(cfg.CountErrors == 0);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.ValidationCollection.Count == 0);

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
        async public System.Threading.Tasks.Task Register_Turnover_Mapping()
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
            r.RegisterPeriodicity = EnumRegisterPeriodicity.REGISTER_PERIOD_DAY;
            r.ListSelectedDocuments.Add(d);
            Assert.AreEqual(1, r.ListObjectDocRefs.Count);
            r.SelectedDoc = d;

            // 1. Can find doc numerical property to map register property.
            r.TableTurnoverPropertyQtyAccumulatorLength = 28;
            r.TableTurnoverPropertyQtyAccumulatorAccuracy = 4;
            r.TableTurnoverPropertyMoneyAccumulatorLength = 28;
            r.TableTurnoverPropertyMoneyAccumulatorAccuracy = 4;
            r.UpdateListMappings();
            Assert.AreEqual(2, r.ListMappings.Count);
            var mrec = r.ListMappings.Single(m => m.Name == r.TableTurnoverPropertyQtyAccumulatorName);
            mrec = r.ListMappings.Single(m => m.Name == r.TableTurnoverPropertyMoneyAccumulatorName);
            mrec.ListToMap.Single(m => m.Name == pMoney.Name);
            mrec.ListToMap.Single(m => m.Name == pQty.Name);

            var s_qty5_2 = cfg.Model.GroupDocuments.DocumentTimeline.AddPropertyNumerical("qty", 15, 1);
            r.UpdateListMappings();
            mrec = r.ListMappings.Single(m => m.Name == r.TableTurnoverPropertyQtyAccumulatorName);
            Assert.AreEqual(3, mrec.ListToMap.Count);
            mrec.ListToMap.Single(m => m.Name == pMoney.Name);
            mrec.ListToMap.Single(m => m.Name == pQty.Name);
            mrec.ListToMap.Single(m => m.Name == s_qty5_2.Name);

            // Length of doc property has to be less or equal than numerical register property length.
            r.TableTurnoverPropertyQtyAccumulatorLength = 16;
            r.TableTurnoverPropertyMoneyAccumulatorLength = 16;
            r.UpdateListMappings();
            mrec = r.ListMappings.Single(m => m.Name == r.TableTurnoverPropertyQtyAccumulatorName);
            mrec.ListToMap.Single(m => m.Name == s_qty5_2.Name);
            Assert.AreEqual(1, mrec.ListToMap.Count);
            mrec = r.ListMappings.Single(m => m.Name == r.TableTurnoverPropertyMoneyAccumulatorName);
            mrec.ListToMap.Single(m => m.Name == s_qty5_2.Name);
            Assert.AreEqual(1, mrec.ListToMap.Count);

            r.TableTurnoverPropertyQtyAccumulatorLength = 17;
            r.TableTurnoverPropertyMoneyAccumulatorLength = 17;
            r.UpdateListMappings();
            mrec = r.ListMappings.Single(m => m.Name == r.TableTurnoverPropertyQtyAccumulatorName);
            Assert.AreEqual(2, mrec.ListToMap.Count);
            mrec.ListToMap.Single(m => m.Name == pQty.Name);
            mrec.ListToMap.Single(m => m.Name == s_qty5_2.Name);
            mrec = r.ListMappings.Single(m => m.Name == r.TableTurnoverPropertyMoneyAccumulatorName);
            Assert.AreEqual(2, mrec.ListToMap.Count);
            mrec.ListToMap.Single(m => m.Name == pQty.Name);
            mrec.ListToMap.Single(m => m.Name == s_qty5_2.Name);

            r.TableTurnoverPropertyQtyAccumulatorLength = 28;
            r.TableTurnoverPropertyQtyAccumulatorAccuracy = 4;
            r.TableTurnoverPropertyMoneyAccumulatorLength = 28;
            r.TableTurnoverPropertyMoneyAccumulatorAccuracy = 4;
            // Accuracy of doc property has to be less or equal than numerical register property accuracy.
            r.TableTurnoverPropertyQtyAccumulatorAccuracy = 1;
            r.TableTurnoverPropertyMoneyAccumulatorAccuracy = 1;
            r.UpdateListMappings();
            mrec = r.ListMappings.Single(m => m.Name == r.TableTurnoverPropertyQtyAccumulatorName);
            Assert.AreEqual(1, mrec.ListToMap.Count);
            mrec.ListToMap.Single(m => m.Name == s_qty5_2.Name);
            mrec = r.ListMappings.Single(m => m.Name == r.TableTurnoverPropertyMoneyAccumulatorName);
            Assert.AreEqual(1, mrec.ListToMap.Count);
            mrec.ListToMap.Single(m => m.Name == s_qty5_2.Name);

            r.TableTurnoverPropertyQtyAccumulatorAccuracy = 2;
            r.TableTurnoverPropertyMoneyAccumulatorAccuracy = 2;
            r.UpdateListMappings();
            mrec = r.ListMappings.Single(m => m.Name == r.TableTurnoverPropertyQtyAccumulatorName);
            Assert.AreEqual(2, mrec.ListToMap.Count);
            mrec.ListToMap.Single(m => m.Name == pMoney.Name);
            mrec.ListToMap.Single(m => m.Name == s_qty5_2.Name);
            mrec = r.ListMappings.Single(m => m.Name == r.TableTurnoverPropertyMoneyAccumulatorName);
            Assert.AreEqual(2, mrec.ListToMap.Count);
            mrec.ListToMap.Single(m => m.Name == pMoney.Name);
            mrec.ListToMap.Single(m => m.Name == s_qty5_2.Name);

            // 2. Can find doc shared property to map register dimension 
            var pSharedC1 = d.ParentGroupListDocuments.ParentGroupDocuments.DocumentTimeline.AddPropertyCatalog("pSharedC1", c1);
            r.AddDimension(pSharedC1.Name, c1);
            r.UpdateListMappings();
            Assert.AreEqual(3, r.ListMappings.Count);
            mrec = r.ListMappings.Single(m => m.Name == pSharedC1.Name);
            Assert.AreEqual(1, mrec.ListToMap.Count);
            mrec.ListToMap.Single(m => m.Name == pSharedC1.Name);

            // 3. Can find doc property to map register dimension 
            var pSharedC2 = d.AddPropertyCatalog("pSharedC2", c2.Guid);
            r.AddDimension(pSharedC2.Name, c2);
            r.UpdateListMappings();
            Assert.AreEqual(4, r.ListMappings.Count);
            mrec = r.ListMappings.Single(m => m.Name == pSharedC2.Name);
            Assert.AreEqual(1, mrec.ListToMap.Count);
            mrec.ListToMap.Single(m => m.Name == pSharedC2.Name);

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

            //await cfg.ValidateSubTreeFromNodeAsync(cfg, null, token);
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
            cfg.Model.GroupCatalogs.AddCatalog("Test1");
            Assert.AreEqual(cfg.Model.GroupCatalogs[0].Name, cfg.Model.GroupCatalogs[0].CompositeName);
            cfg.Model.IsUseNameComposition = true;
            Assert.AreEqual(cfg.Model.GroupCatalogs.PrefixForCompositionNames + cfg.Model.GroupCatalogs[0].Name, cfg.Model.GroupCatalogs[0].CompositeName);

            cfg.Model.IsUseNameComposition = false;
            cfg.Model.GroupCatalogs[0].GroupDetails.AddPropertiesTab("Tab1");
            Assert.AreEqual(cfg.Model.GroupCatalogs[0].Name, cfg.Model.GroupCatalogs[0].CompositeName);
            Assert.AreEqual(cfg.Model.GroupCatalogs[0].GroupDetails[0].Name, cfg.Model.GroupCatalogs[0].GroupDetails[0].CompositeName);
            cfg.Model.IsUseNameComposition = true;
            Assert.AreEqual(cfg.Model.GroupCatalogs.PrefixForCompositionNames + cfg.Model.GroupCatalogs[0].Name, cfg.Model.GroupCatalogs[0].CompositeName);
            Assert.AreEqual(cfg.Model.GroupCatalogs.PrefixForCompositionNames + cfg.Model.GroupCatalogs[0].Name + cfg.Model.GroupCatalogs[0].GroupDetails[0].Name,
                cfg.Model.GroupCatalogs[0].GroupDetails[0].CompositeName);

        }
        [TestMethod]
        public void DbName002_Document()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;

            cfg.Model.IsUseNameComposition = false;
            cfg.Model.GroupDocuments.PrefixForCompositionNames = "Cat";
            cfg.Model.GroupDocuments.GroupListDocuments.AddDocument("Test1");
            Assert.AreEqual(cfg.Model.GroupDocuments.GroupListDocuments[0].Name, cfg.Model.GroupDocuments.GroupListDocuments[0].CompositeName);
            cfg.Model.IsUseNameComposition = true;
            Assert.AreEqual(cfg.Model.GroupDocuments.PrefixForCompositionNames + cfg.Model.GroupDocuments.GroupListDocuments[0].Name, cfg.Model.GroupDocuments.GroupListDocuments[0].CompositeName);

            cfg.Model.IsUseNameComposition = false;
            cfg.Model.GroupDocuments.GroupListDocuments[0].GroupDetails.AddPropertiesTab("Tab1");
            Assert.AreEqual(cfg.Model.GroupDocuments.GroupListDocuments[0].Name, cfg.Model.GroupDocuments.GroupListDocuments[0].CompositeName);
            Assert.AreEqual(cfg.Model.GroupDocuments.GroupListDocuments[0].GroupDetails[0].Name, cfg.Model.GroupDocuments.GroupListDocuments[0].GroupDetails[0].CompositeName);
            cfg.Model.IsUseNameComposition = true;
            Assert.AreEqual(cfg.Model.GroupDocuments.PrefixForCompositionNames + cfg.Model.GroupDocuments.GroupListDocuments[0].Name, cfg.Model.GroupDocuments.GroupListDocuments[0].CompositeName);
            Assert.AreEqual(cfg.Model.GroupDocuments.PrefixForCompositionNames + cfg.Model.GroupDocuments.GroupListDocuments[0].Name + cfg.Model.GroupDocuments.GroupListDocuments[0].GroupDetails[0].Name,
                cfg.Model.GroupDocuments.GroupListDocuments[0].GroupDetails[0].CompositeName);
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
            Assert.AreEqual(0, cfg.Model.ValidationCollection.Count);

            cfg.Model.GroupDocuments.GroupListDocuments.AddDocument("Test1");
            cfg.Model.Validate();
            Assert.AreEqual(1, cfg.Model.ValidationCollection.Count);

            cfg.Model.GroupCatalogs.ListCatalogs[0].GroupDetails.AddTab("Test1");
            cfg.Model.Validate();
            Assert.AreEqual(2, cfg.Model.ValidationCollection.Count);

            cfg.Model.IsUseNameComposition = true;
            cfg.Model.Validate();
            Assert.AreEqual(0, cfg.Model.ValidationCollection.Count);
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
        async public Task RelationTests()
        {
            var vm = MainPageVM.Create(MainPageVM.GetvSharpStudioPluginsPath(), null, true);
            var cfg = vm.Config;
            // Use History is saved and restored simple data
            // Use History is saved and restored complex OneToOne data
            // Use History is saved and restored complex OneToMany data
            // Use History is saved and restored complex ManyToMany data


            #region One To One
            Assert.AreEqual(0, cfg.Model.GroupRelations.GroupListOneToOneRelations.ListRelations.Count);
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
            Assert.AreEqual(0, lst.Count);
            // RefCat2
            lst = c1.GetIncludedProperties(null, false, true);
            Assert.AreEqual(3, lst.Count);
            Assert.AreEqual(EnumDataType.DOCUMENT, lst[0].DataType.DataTypeEnum);
            Assert.AreEqual("test_one_to_one_rel", lst[0].Name);
            // RefCat1
            lst = d2.GetIncludedProperties(null, false, true);
            Assert.AreEqual(1, lst.Count);
            Assert.AreEqual(EnumDataType.CATALOG, lst[0].DataType.DataTypeEnum);
            Assert.AreEqual("test_one_to_one_rel", lst[0].Name);

            // 2.
            rel.RefType = EnumOneToOneRefType.ONE_TO_ONE_REF_FROM_FIRST_TO_SECOND_ONLY;
            await vm.BtnConfigValidateAsync.ExecuteAsync();
            Assert.AreEqual(0, vm.Config.CountErrors);
            lst = rel.GetIncludedProperties(null, false, false);
            Assert.AreEqual(0, lst.Count);
            // RefCat2
            lst = c1.GetIncludedProperties(null, false, true);
            Assert.AreEqual(3, lst.Count);
            Assert.AreEqual(EnumDataType.DOCUMENT, lst[0].DataType.DataTypeEnum);
            Assert.AreEqual("test_one_to_one_rel", lst[0].Name);
            // nothing
            lst = d2.GetIncludedProperties(null, false, true);
            Assert.AreEqual(0, lst.Count);

            // 3.
            rel.RefType = EnumOneToOneRefType.ONE_TO_ONE_REF_FROM_SECOND_TO_FIRST_ONLY;
            await vm.BtnConfigValidateAsync.ExecuteAsync();
            Assert.AreEqual(0, vm.Config.CountErrors);
            lst = rel.GetIncludedProperties(null, false, false);
            Assert.AreEqual(0, lst.Count);
            // nothing
            lst = c1.GetIncludedProperties(null, false, true);
            Assert.AreEqual(2, lst.Count);
            // RefCat1
            lst = d2.GetIncludedProperties(null, false, true);
            Assert.AreEqual(1, lst.Count);
            Assert.AreEqual(EnumDataType.CATALOG, lst[0].DataType.DataTypeEnum);
            Assert.AreEqual("test_one_to_one_rel", lst[0].Name);
            #endregion One To One

            #region Many To Many
            Assert.AreEqual(0, cfg.Model.GroupRelations.GroupListManyToManyRelations.ListRelations.Count);
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
            Assert.AreEqual(2, lst.Count);
            lst = d2.GetIncludedProperties(null, false, true);
            Assert.AreEqual(0, lst.Count);

            lst = rel2.GetIncludedProperties(null, false, true);
            Assert.AreEqual(2, lst.Count);
            lst.Single(n => n.Name == "test_many_to_many_rel" && n.DataType.DataTypeEnum == EnumDataType.CATALOG);
            lst.Single(n => n.Name == "test_many_to_many_rel" && n.DataType.DataTypeEnum == EnumDataType.DOCUMENT);
            #endregion Many To Many
        }
    }
}
