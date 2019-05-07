using System;
using vSharpStudio.vm.ViewModels;
using Xunit;
using ViewModelBase;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows;

namespace vSharpStudio.xUnit
{
    public class EditorVmTests
    {
        public EditorVmTests()
        {
            ViewModelBindable.isUnitTests = true;
        }
        #region SortedCollection
        public partial class TestValidator : ValidatorBase<TestSortable, TestValidator> { }
        [DebuggerDisplay("{Name} {SortingValue} Guid:{Guid,nq}")]
        public class TestSortable : ConfigObjectBase<TestSortable, TestValidator>
        {
            public TestSortable() : base(TestValidator.Validator) { }
        }
        [Fact]
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

            Assert.True(sc[0].Guid == t1.Guid);
            Assert.True(sc[1].Guid == t2.Guid);
            Assert.True(sc[2].Guid == t3.Guid);
            Assert.True(sc[3].Guid == t31.Guid);
            Assert.True(sc[4].Guid == t22.Guid);

            Assert.True(sc[0].Name == t1.Name);
            Assert.True(sc[1].Name == t2.Name);
            Assert.True(sc[2].Name == t3.Name);
            Assert.True(sc[3].Name == t31.Name);
            Assert.True(sc[4].Name == t22.Name);
        }
        #endregion SortedCollection
        #region Config
        [Fact]
        public void Config001GuidInit()
        {
            //Proto.Config.proto_config dto = new Proto.Config.proto_config();
            //Config cfg = new Config(dto);
            //Constant c = Constant.Create();
            //cfg.GroupConstants.Children.Add(c);
            var cfg = new Config();
            Assert.True(cfg.Guid.Length > 0);
        }
        [Fact]
        public void Config002CanSaveAndRestore()
        {
            var cfg = new Config();
            cfg.GroupConstants.NodeAddNewSubNode();
            string json = cfg.ExportToJson();
            Assert.True(json.Length > 0);
            var cfg2 = new Config(json);
            Assert.True(cfg2.GroupConstants.Children.Count == 1);
            Assert.True(cfg2.GroupConstants.Children[0].Name == typeof(Constant).Name + 1);
        }
        [Fact]
        public void Config003CanSaveAndRestoreSortingValue()
        {
            var cfg = new Config();
            cfg.GroupConstants.NodeAddNewSubNode();
            cfg.GroupConstants.NodeAddNewSubNode();
            cfg.GroupConstants.Children[1].NodeMoveUp();
            string json = cfg.ExportToJson();
            Assert.True(json.Length > 0);
            var cfg2 = new Config(json);
            Assert.True(cfg2.GroupConstants.Children.Count == 2);
            Assert.True(cfg2.GroupConstants.Children[0].Name == typeof(Constant).Name + 2);
        }
        // TODO business validation tests
        //[Fact]
        //public void Config003ValidationIsDbFromConnectionStringInfoConnectionStringName()
        //{
        //    var cfg = new Config(new SortedObservableCollection<ValidationMessage>());
        //    cfg.IsDbFromConnectionString = true;
        //    cfg.Validate();
        //    Assert.False(cfg.HasErrors);
        //    Assert.True(cfg.HasWarnings);
        //    Assert.False(cfg.HasInfos);
        //    Assert.True(cfg.ValidationCollection.Count == 1);
        //    Assert.True(cfg.ValidationCollection[0].SortingValue >= 1 << ValidationMessage.MultiplierShift);
        //}
        #endregion Config

        #region Constant
        [Fact]
        public void Constant001GuidInit()
        {
            var cfg = new Constant();
            Assert.True(cfg.Guid.Length > 0);
        }
        [Fact]
        public void Constant002AddedParent()
        {
            var cfg = new Config();
            cfg.GroupConstants.NodeAddNewSubNode();
            Assert.Equal(cfg.GroupConstants.Children[0].Parent.Guid, cfg.GroupConstants.Guid);
            cfg.GroupConstants.Children[0].NodeAddNew();
            Assert.Equal(cfg.GroupConstants.Children[1].Parent.Guid, cfg.GroupConstants.Guid);
        }
        [Fact]
        public void Constant003AddedDefaultName()
        {
            var cfg = new Config();
            cfg.GroupConstants.NodeAddNewSubNode();
            Assert.Equal(Constant.DefaultName + "1", cfg.GroupConstants.Children[0].Name);
            cfg.GroupConstants.Children[0].NodeAddNew();
            Assert.Equal(Constant.DefaultName + "2", cfg.GroupConstants.Children[1].Name);
        }
        #endregion Constant

        #region Enum
        [Fact]
        public void Enum001GuidInit()
        {
            var cfg = new Enumeration();
            Assert.True(cfg.Guid.Length > 0);
        }
        [Fact]
        public void Enum002AddedParent()
        {
            var cfg = new Config();
            cfg.GroupEnumerations.NodeAddNewSubNode();
            Assert.Equal(cfg.GroupEnumerations.Children[0].Parent.Guid, cfg.GroupEnumerations.Guid);
            cfg.GroupEnumerations.Children[0].NodeAddNew();
            Assert.Equal(cfg.GroupEnumerations.Children[1].Parent.Guid, cfg.GroupEnumerations.Guid);
        }
        #endregion Enum

        #region Property
        [Fact]
        public void Property001GuidInit()
        {
            var cfg = new Property();
            Assert.True(cfg.Guid.Length > 0);
        }
        #endregion Property

        #region Catalog
        [Fact]
        public void Catalog001GuidInit()
        {
            var cfg = new Catalog();
            Assert.True(cfg.Guid.Length > 0);
        }
        #endregion Catalog

        #region Diff
        //[Fact]
        //public void DiffConstant001Added()
        //{
        //    Assert.True(false);
        //}
        //[Fact]
        //public void DiffConfig001CanDiffwithDb()
        //{
        //    Assert.True(false);
        //}
        #endregion Diff

        #region ITreeConfigNode
        [Fact]
        public void ITreeConfigNode001_UpdateSortingValueWhenNameIsChanged()
        {
            var cfg = new Config();
            ViewModelBindable.isNotValidateForUnitTests = true;

            var cnst = new Constant() { Parent = cfg.GroupConstants };
            cfg.GroupConstants.Children.Add(cnst);
            var curr = cnst.SortingValue;
            cnst.Name = "abc1";
            Assert.True(cnst.SortingValue != curr);
            curr = cnst.SortingValue;
            cnst.Name = "ABC1";
            Assert.True(cnst.SortingValue == curr);

            cnst.Name = "_0";
            curr = cnst.SortingValue;
            cnst.Name = "00";
            Assert.True(cnst.SortingValue > curr);

            cnst.Name = "_";
            curr = cnst.SortingValue;
            cnst.Name = "0";
            Assert.True(cnst.SortingValue > curr);

            cnst.Name = "0";
            curr = cnst.SortingValue;
            cnst.Name = "1";
            Assert.True(cnst.SortingValue > curr);

            cnst.Name = "9";
            curr = cnst.SortingValue;
            cnst.Name = "A";
            Assert.True(cnst.SortingValue > curr);

            cnst.Name = "A";
            curr = cnst.SortingValue;
            cnst.Name = "B";
            Assert.True(cnst.SortingValue > curr);

            cnst.Name = "A";
            curr = cnst.SortingValue;
            cnst.Name = "a";
            Assert.True(cnst.SortingValue == curr);

            //cnst.Name = "__";
            cnst.Name = "_z";
            curr = cnst.SortingValue;
            cnst.Name = "0_";
            Assert.True(cnst.SortingValue > curr);

            cnst.Name = "ABC1";
            curr = cnst.SortingValue;
            cnst.Name = "BBC1";
            Assert.True(cnst.SortingValue > curr);
            cnst.Name = "ACC1";
            Assert.True(cnst.SortingValue > curr);
            cnst.Name = "ABD1";
            Assert.True(cnst.SortingValue > curr);
            cnst.Name = "ABC2";
            Assert.True(cnst.SortingValue > curr);

            cnst.Name = "ABC0";
            Assert.True(cnst.SortingValue < curr);
            cnst.Name = "ABB1";
            Assert.True(cnst.SortingValue < curr);
            cnst.Name = "AAC1";
            Assert.True(cnst.SortingValue < curr);
        }
        [Fact]
        public void ITreeConfigNode002_RestoreSortingValueWhenObjectRestoredFromFile()
        {
            var cfg = new Config();
            var cnst = new Constant() { Parent = cfg.GroupConstants };
            cfg.GroupConstants.Children.Add(cnst);
            cnst.Name = "abc1";
            var curr = cnst.SortingValue;

            string json = cfg.ExportToJson();
            var cfg2 = new Config(json);

            Assert.True(cfg2.GroupConstants.Children[0].Name == cnst.Name);
            Assert.True(cfg2.GroupConstants.Children[0].SortingValue == cnst.SortingValue);
        }
        [Fact]
        public void ITreeConfigNode003_ReSortedWhenSortingValueIsChanged()
        {
            var cfg = new Config();
            var cnst = new Constant() { Parent = cfg.GroupConstants };
            cfg.GroupConstants.Children.Add(cnst);
            cnst.Name = "abc1";

            var cnst2 = new Constant() { Parent = cfg.GroupConstants };
            cfg.GroupConstants.Children.Add(cnst2);
            cnst2.Name = "abc1";

            Assert.True(cnst.Guid != cnst2.Guid);

            cnst2.Name = "abc0";
            Assert.True(cfg.GroupConstants.Children[0].SortingValue < cfg.GroupConstants.Children[1].SortingValue);
            Assert.True(cfg.GroupConstants.Children[1].Guid == cnst.Guid);
            Assert.True(cfg.GroupConstants.Children[0].Guid == cnst2.Guid);

            cnst2.Name = "abc2";
            Assert.True(cfg.GroupConstants.Children[0].SortingValue < cfg.GroupConstants.Children[1].SortingValue);
            Assert.True(cfg.GroupConstants.Children[0].Guid == cnst.Guid);
            Assert.True(cfg.GroupConstants.Children[1].Guid == cnst2.Guid);
        }
        [Fact]
        public void ITreeConfigNode003_CanConfigTreeCommands()
        {
            var cfg = new ViewModels.ConfigRoot();

            #region Constants

            Assert.True(cfg.GroupConstants.NodeCanLeft() == false);
            Assert.True(cfg.GroupConstants.NodeCanRight() == true);
            Assert.True(cfg.GroupConstants.NodeCanMoveUp() == false);
            Assert.True(cfg.GroupConstants.NodeCanMoveDown() == false);
            Assert.True(cfg.GroupConstants.NodeCanAddNew() == false);
            Assert.True(cfg.GroupConstants.NodeCanAddNewSubNode() == true);
            Assert.True(cfg.SelectedNode == null);
            cfg.GroupConstants.NodeAddNewSubNode();
            Assert.True(cfg.SelectedNode != null);
            Assert.True(cfg.SelectedNode == cfg.GroupConstants.Children[0]);
            Assert.True(cfg.SelectedNode.Guid == cfg.GroupConstants.Children[0].Guid);

            Assert.True(cfg.GroupConstants.NodeCanRight() == true);
            Assert.True(cfg.GroupConstants.Children[0].NodeCanLeft() == true);
            Assert.True(cfg.GroupConstants.Children[0].NodeCanRight() == false);
            Assert.True(cfg.GroupConstants.Children[0].NodeCanMoveUp() == false);
            Assert.True(cfg.GroupConstants.Children[0].NodeCanMoveDown() == false);
            Assert.True(cfg.GroupConstants.Children[0].NodeCanAddNew() == true);
            Assert.True(cfg.GroupConstants.Children[0].NodeCanAddNewSubNode() == false);

            cfg.GroupConstants.NodeAddNewSubNode();
            Assert.True(cfg.GroupConstants.Children[0].NodeCanMoveUp() == false);
            Assert.True(cfg.GroupConstants.Children[0].NodeCanMoveDown() == true);
            Assert.True(cfg.GroupConstants.Children[1].NodeCanMoveUp() == true);
            Assert.True(cfg.GroupConstants.Children[1].NodeCanMoveDown() == false);

            #endregion Constants

            #region Enumerations

            Assert.True(cfg.GroupEnumerations.NodeCanLeft() == false);
            Assert.True(cfg.GroupEnumerations.NodeCanRight() == true);
            Assert.True(cfg.GroupEnumerations.NodeCanMoveUp() == false);
            Assert.True(cfg.GroupEnumerations.NodeCanMoveDown() == false);
            Assert.True(cfg.GroupEnumerations.NodeCanAddNew() == false);
            Assert.True(cfg.GroupEnumerations.NodeCanAddNewSubNode() == true);
            cfg.GroupEnumerations.NodeAddNewSubNode();
            Assert.True(cfg.SelectedNode != null);
            Assert.True(cfg.SelectedNode == cfg.GroupEnumerations.Children[0]);
            Assert.True(cfg.SelectedNode.Guid == cfg.GroupEnumerations.Children[0].Guid);

            Assert.True(cfg.GroupEnumerations.NodeCanRight() == true);
            Assert.True(cfg.GroupEnumerations.Children[0].NodeCanLeft() == true);
            Assert.True(cfg.GroupEnumerations.Children[0].NodeCanRight() == true);
            Assert.True(cfg.GroupEnumerations.Children[0].NodeCanMoveUp() == false);
            Assert.True(cfg.GroupEnumerations.Children[0].NodeCanMoveDown() == false);
            Assert.True(cfg.GroupEnumerations.Children[0].NodeCanAddNew() == true);
            Assert.True(cfg.GroupEnumerations.Children[0].NodeCanAddNewSubNode() == false);

            //#region Properties

            //cfg.GroupCatalogs.Children[0].NodeAddNewSubNode();
            //Assert.True(cfg.SelectedNode != null);
            //Assert.True(cfg.SelectedNode == cfg.GroupCatalogs.Children[0].ListProperties[0]);
            //Assert.True(cfg.GroupCatalogs.Children[0].ListProperties[0].NodeCanLeft() == true);
            //Assert.True(cfg.GroupCatalogs.Children[0].ListProperties[0].NodeCanRight() == true);
            //Assert.True(cfg.GroupCatalogs.Children[0].ListProperties[0].NodeCanMoveUp() == false);
            //Assert.True(cfg.GroupCatalogs.Children[0].ListProperties[0].NodeCanMoveDown() == false);
            //Assert.True(cfg.GroupCatalogs.Children[0].ListProperties[0].NodeCanAddNew() == true);
            //Assert.True(cfg.GroupCatalogs.Children[0].ListProperties[0].NodeCanAddNewSubNode() == true);

            //cfg.GroupCatalogs.Children[0].ListProperties[0].NodeAddNew();
            //Assert.True(cfg.SelectedNode == cfg.GroupCatalogs.Children[0].ListProperties[1]);
            //Assert.True(cfg.SelectedNode.Guid == cfg.GroupCatalogs.Children[0].ListProperties[1].Guid);
            //Assert.True(cfg.GroupCatalogs.Children[0].ListProperties[1].NodeCanLeft() == true);
            //Assert.True(cfg.GroupCatalogs.Children[0].ListProperties[1].NodeCanRight() == true);
            //Assert.True(cfg.GroupCatalogs.Children[0].ListProperties[1].NodeCanMoveUp() == true);
            //Assert.True(cfg.GroupCatalogs.Children[0].ListProperties[1].NodeCanMoveDown() == false);
            //Assert.True(cfg.GroupCatalogs.Children[0].ListProperties[1].NodeCanAddNew() == true);
            //Assert.True(cfg.GroupCatalogs.Children[0].ListProperties[1].NodeCanAddNewSubNode() == true);

            //var p = cfg.GroupCatalogs.Children[0].ListProperties[1];
            //p.NodeMoveUp();
            //Assert.True(p == cfg.GroupCatalogs.Children[0].ListProperties[0]);
            //Assert.True(cfg.SelectedNode == cfg.GroupCatalogs.Children[0].ListProperties[0]);

            //// change property parameters
            //p.DataType.MinValue = 5;
            //p.DataType.MaxValue = 6;

            //p.NodeAddClone();
            //Assert.True(p == cfg.GroupCatalogs.Children[0].ListProperties[0]);
            //Assert.True(cfg.GroupCatalogs.Children[0].ListProperties[1].Name == cfg.GroupCatalogs.Children[0].ListProperties[0].Name + "2");
            //Assert.True(5 == cfg.GroupCatalogs.Children[0].ListProperties[1].DataType.MinValue);
            //Assert.True(6 == cfg.GroupCatalogs.Children[0].ListProperties[1].DataType.MaxValue);

            //#endregion Properties

            #endregion Enumerations

            #region Catalogs

            Assert.True(cfg.GroupCatalogs.NodeCanLeft() == false);
            Assert.True(cfg.GroupCatalogs.NodeCanRight() == true);
            Assert.True(cfg.GroupCatalogs.NodeCanMoveUp() == false);
            Assert.True(cfg.GroupCatalogs.NodeCanMoveDown() == false);
            Assert.True(cfg.GroupCatalogs.NodeCanAddNew() == false);
            Assert.True(cfg.GroupCatalogs.NodeCanAddNewSubNode() == true);
            cfg.GroupCatalogs.NodeAddNewSubNode();
            Assert.True(cfg.SelectedNode != null);
            Assert.True(cfg.SelectedNode == cfg.GroupCatalogs.Children[0]);
            Assert.True(cfg.SelectedNode.Guid == cfg.GroupCatalogs.Children[0].Guid);

            Assert.True(cfg.GroupCatalogs.NodeCanRight() == true);
            Assert.True(cfg.GroupCatalogs.Children[0].NodeCanLeft() == true);
            Assert.True(cfg.GroupCatalogs.Children[0].NodeCanRight() == true);
            Assert.True(cfg.GroupCatalogs.Children[0].NodeCanMoveUp() == false);
            Assert.True(cfg.GroupCatalogs.Children[0].NodeCanMoveDown() == false);
            Assert.True(cfg.GroupCatalogs.Children[0].NodeCanAddNew() == true);
            Assert.True(cfg.GroupCatalogs.Children[0].NodeCanAddNewSubNode() == true);

            #region Properties

            cfg.GroupCatalogs.Children[0].NodeAddNewSubNode();
            Assert.True(cfg.SelectedNode != null);
            Assert.True(cfg.SelectedNode == cfg.GroupCatalogs[0].GroupProperties[0]);
            Assert.True(cfg.GroupCatalogs[0].GroupProperties[0].NodeCanLeft() == true);
            Assert.True(cfg.GroupCatalogs[0].GroupProperties[0].NodeCanRight() == false);
            Assert.True(cfg.GroupCatalogs[0].GroupProperties[0].NodeCanMoveUp() == false);
            Assert.True(cfg.GroupCatalogs[0].GroupProperties[0].NodeCanMoveDown() == false);
            Assert.True(cfg.GroupCatalogs[0].GroupProperties[0].NodeCanAddNew() == true);
            Assert.True(cfg.GroupCatalogs[0].GroupProperties[0].NodeCanAddNewSubNode() == false);

            cfg.GroupCatalogs[0].GroupProperties[0].NodeAddNew();
            Assert.True(cfg.SelectedNode == cfg.GroupCatalogs[0].GroupProperties[1]);
            Assert.True(cfg.SelectedNode.Guid == cfg.GroupCatalogs[0].GroupProperties[1].Guid);
            Assert.True(cfg.GroupCatalogs[0].GroupProperties[1].NodeCanLeft() == true);
            Assert.True(cfg.GroupCatalogs[0].GroupProperties[1].NodeCanRight() == false);
            Assert.True(cfg.GroupCatalogs[0].GroupProperties[1].NodeCanMoveUp() == true);
            Assert.True(cfg.GroupCatalogs[0].GroupProperties[1].NodeCanMoveDown() == false);
            Assert.True(cfg.GroupCatalogs[0].GroupProperties[1].NodeCanAddNew() == true);
            Assert.True(cfg.GroupCatalogs[0].GroupProperties[1].NodeCanAddNewSubNode() == false);

            var p = cfg.GroupCatalogs[0].GroupProperties[1];
            p.NodeMoveUp();
            Assert.True(p == cfg.GroupCatalogs[0].GroupProperties[0]);
            Assert.True(cfg.SelectedNode == cfg.GroupCatalogs[0].GroupProperties[0]);

            // change property parameters
            //p.DataType.MinValue = 5;
            //p.DataType.MaxValue = 6;

            p.NodeAddClone();
            Assert.True(p == cfg.GroupCatalogs[0].GroupProperties[2]);
            Assert.True(cfg.GroupCatalogs[0].GroupProperties[2].Name == cfg.GroupCatalogs[0].GroupProperties[0].Name + "2");
            //Assert.True(5 == cfg.GroupCatalogs.Children[0].GroupProperties.ListProperties[2].DataType.MinValue);
            //Assert.True(6 == cfg.GroupCatalogs.Children[0].GroupProperties.ListProperties[2].DataType.MaxValue);

            #endregion Properties

            #endregion Catalogs

        }
        #endregion ITreeConfigNode

        #region Compare Tree
        private ViewModels.ConfigRoot createTree()
        {
            var cfg = new ViewModels.ConfigRoot();

            cfg.GroupEnumerations.NodeAddNewSubNode();
            cfg.GroupEnumerations[0].DataTypeEnum = Proto.Config.proto_enumeration.Types.EnumEnumerationType.Integer;
            cfg.GroupEnumerations[0].Children.Add(new EnumerationPair() { Name = "one", Value = "1" });

            cfg.GroupConstants.NodeAddNewSubNode();
            cfg.GroupConstants[0].DataType.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.Bool;
            cfg.GroupConstants.NodeAddNewSubNode();
            cfg.GroupConstants[1].DataType.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.Enumeration;
            cfg.GroupConstants[1].DataType.ObjectName = cfg.GroupEnumerations.Children[0].Name;

            return cfg;
        }
        [Fact]
        public void Rules001_DataType()
        {
            var dt = new DataType();

            dt.Validate();
            Assert.True(dt.CountErrors == 0);
            Assert.True(dt.CountInfos == 0);
            Assert.True(dt.CountWarnings == 0);
            Assert.True(dt.ValidationCollection.Count == 0);
            //Assert.True(dt.VisibilityAccuracy == Visibility.Collapsed);
            //Assert.True(dt.VisibilityLength == Visibility.Collapsed);
            //Assert.True(dt.VisibilityObjectName == Visibility.Collapsed);

            dt.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.Any;
            dt.Validate();
            Assert.True(dt.CountErrors == 0);
            Assert.True(dt.CountInfos == 0);
            Assert.True(dt.CountWarnings == 0);
            Assert.True(dt.ValidationCollection.Count == 0);
            //Assert.True(dt.VisibilityAccuracy == Visibility.Collapsed);
            //Assert.True(dt.VisibilityLength == Visibility.Collapsed);
            //Assert.True(dt.VisibilityObjectName == Visibility.Collapsed);

            dt.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.Bool;
            dt.Validate();
            Assert.True(dt.CountErrors == 0);
            Assert.True(dt.CountInfos == 0);
            Assert.True(dt.CountWarnings == 0);
            Assert.True(dt.ValidationCollection.Count == 0);
            //Assert.True(dt.VisibilityAccuracy == Visibility.Collapsed);
            //Assert.True(dt.VisibilityLength == Visibility.Collapsed);
            //Assert.True(dt.VisibilityObjectName == Visibility.Collapsed);

            dt.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.Catalog;
            dt.Validate();
            Assert.True(dt.CountErrors == 0);
            Assert.True(dt.CountInfos == 0);
            Assert.True(dt.CountWarnings == 0);
            Assert.True(dt.HasErrors);
            Assert.True(dt.ValidationCollection.Count == 1);
            Assert.True(dt.ValidationCollection[0].Message == Config.ValidationMessages.TYPE_EMPTY_CATALOG_NAME);
            //Assert.True(dt.VisibilityAccuracy == Visibility.Collapsed);
            //Assert.True(dt.VisibilityLength == Visibility.Collapsed);
            //Assert.True(dt.VisibilityObjectName == Visibility.Visible);

            dt.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.Catalogs;
            dt.Validate();
            Assert.True(dt.CountErrors == 0);
            Assert.True(dt.CountInfos == 0);
            Assert.True(dt.CountWarnings == 0);
            Assert.True(dt.ValidationCollection.Count == 0);
            //Assert.True(dt.VisibilityAccuracy == Visibility.Collapsed);
            //Assert.True(dt.VisibilityLength == Visibility.Collapsed);
            //Assert.True(dt.VisibilityObjectName == Visibility.Collapsed);

            //dt.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.Constant;
            //dt.Validate();
            //Assert.True(dt.CountErrors == 0);
            //Assert.True(dt.CountInfos == 0);
            //Assert.True(dt.CountWarnings == 0);
            //Assert.True(dt.HasErrors);
            //Assert.True(dt.ValidationCollection.Count == 1);
            //Assert.True(dt.ValidationCollection[0].Message == Config.ValidationMessages.TYPE_EMPTY_CONSTANT_NAME);
            //Assert.True(dt.VisibilityAccuracy == Visibility.Collapsed);
            //Assert.True(dt.VisibilityLength == Visibility.Collapsed);
            //Assert.True(dt.VisibilityObjectName == Visibility.Visible);

            dt.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.Enumeration;
            dt.Validate();
            Assert.True(dt.CountErrors == 0);
            Assert.True(dt.CountInfos == 0);
            Assert.True(dt.CountWarnings == 0);
            Assert.True(dt.HasErrors);
            Assert.True(dt.ValidationCollection.Count == 1);
            Assert.True(dt.ValidationCollection[0].Message == Config.ValidationMessages.TYPE_EMPTY_ENUMERATION_NAME);
            //Assert.True(dt.VisibilityAccuracy == Visibility.Collapsed);
            //Assert.True(dt.VisibilityLength == Visibility.Collapsed);
            //Assert.True(dt.VisibilityObjectName == Visibility.Visible);

            dt.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.Numerical;
            dt.Validate();
            Assert.True(dt.CountErrors == 0);
            Assert.True(dt.CountInfos == 0);
            Assert.True(dt.CountWarnings == 0);
            //Assert.True(dt.VisibilityAccuracy == Visibility.Visible);
            //Assert.True(dt.VisibilityLength == Visibility.Visible);
            //Assert.True(dt.VisibilityObjectName == Visibility.Collapsed);

            dt.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.String;
            dt.Validate();
            Assert.True(dt.CountErrors == 0);
            Assert.True(dt.CountInfos == 0);
            Assert.True(dt.CountWarnings == 0);
            Assert.True(dt.ValidationCollection.Count == 0);
            //Assert.True(dt.VisibilityAccuracy == Visibility.Collapsed);
            //Assert.True(dt.VisibilityLength == Visibility.Visible);
            //Assert.True(dt.VisibilityObjectName == Visibility.Collapsed);
        }
        [Fact]
        public void Rules002_Enumeration()
        {
            var cfg = createTree();

            cfg.ValidateSubTreeFromNode(cfg);
            Assert.True(cfg.CountErrors == 0);
            Assert.True(cfg.CountInfos == 0);
            Assert.True(cfg.CountWarnings == 0);
            Assert.True(cfg.ValidationCollection.Count == 0);

            cfg.GroupEnumerations.Children[0].Name = "1a";
            cfg.ValidateSubTreeFromNode(cfg);
            Assert.True(cfg.GroupEnumerations[0].CountErrors == 1);
            Assert.True(cfg.GroupEnumerations[0].CountInfos == 0);
            Assert.True(cfg.GroupEnumerations[0].CountWarnings == 0);
            Assert.True(cfg.GroupEnumerations[0].HasErrors);
            Assert.True(cfg.GroupEnumerations[0].ValidationCollection.Count == 1);
            Assert.True(cfg.GroupEnumerations[0].ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            Assert.True(cfg.GroupEnumerations[0].ValidationCollection[0].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT);

            // intermediate node contains only validation count
            Assert.True(cfg.GroupEnumerations.ValidationCollection.Count == 0);
            Assert.True(cfg.GroupEnumerations.CountErrors == 1);
            Assert.True(cfg.GroupEnumerations.CountInfos == 0);
            Assert.True(cfg.GroupEnumerations.CountWarnings == 0);

            // ValidateSubTreeFromNode(node). node contains full list of validations
            Assert.True(cfg.CountErrors == 2);
            Assert.True(cfg.CountInfos == 0);
            Assert.True(cfg.CountWarnings == 0);
            Assert.True(cfg.ValidationCollection.Count == 2);
            Assert.True(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            Assert.True(cfg.ValidationCollection[1].Severity == FluentValidation.Severity.Error);
            if (cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT)
            {
                Assert.True(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT);
                Assert.True(cfg.ValidationCollection[1].Message == Config.ValidationMessages.TYPE_WRONG_OBJECT_NAME);
            }
            else
            {
                Assert.True(cfg.ValidationCollection[1].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT);
                Assert.True(cfg.ValidationCollection[0].Message == Config.ValidationMessages.TYPE_WRONG_OBJECT_NAME);
            }

            cfg.GroupEnumerations.Children[0].Name = " ab";
            Assert.True(cfg.GroupEnumerations.Children[0].Name == "ab");
            cfg.GroupEnumerations.Children[0].Validate();
            Assert.False(cfg.GroupEnumerations[0].HasErrors);

            cfg.GroupEnumerations.Children[0].Name = "ab ";
            Assert.True(cfg.GroupEnumerations.Children[0].Name == "ab");
            cfg.GroupEnumerations.Children[0].Validate();
            Assert.False(cfg.GroupEnumerations[0].HasErrors);

            cfg.GroupEnumerations.Children[0].Name = "a b";
            cfg.GroupConstants[1].DataType.ObjectName = "a b";
            cfg.ValidateSubTreeFromNode(cfg);
            Assert.True(cfg.ValidationCollection.Count == 1);
            Assert.True(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            Assert.True(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);

            cfg.GroupEnumerations.NodeAddNewSubNode();
            cfg.GroupEnumerations.Children[0].Name = "ab";
            cfg.GroupEnumerations.Children[1].Name = "ab";
            cfg.GroupConstants[1].DataType.ObjectName = "ab";
            cfg.ValidateSubTreeFromNode(cfg);
            Assert.True(cfg.ValidationCollection.Count == 2);
            Assert.True(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            Assert.True(cfg.ValidationCollection[1].Severity == FluentValidation.Severity.Error);
            Assert.True(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            Assert.True(cfg.ValidationCollection[1].Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            Assert.True(cfg.GroupEnumerations.Children[1].ValidationCollection.Count == 1);
            Assert.True(cfg.GroupEnumerations.Children[1].ValidationCollection[0].Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            Assert.True(cfg.GroupEnumerations[1].HasErrors == true);
            var errenum = cfg.GroupEnumerations[1].GetErrors("Name").GetEnumerator();
            Assert.True(errenum.MoveNext() == true);
            Assert.True((string)errenum.Current == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            Assert.True(errenum.MoveNext() == false);
        }
        [Fact]
        public void Rules003_Constant()
        {
            var cfg = createTree();

            cfg.ValidateSubTreeFromNode(cfg);
            Assert.True(cfg.CountErrors == 0);
            Assert.True(cfg.CountInfos == 0);
            Assert.True(cfg.CountWarnings == 0);
            Assert.True(cfg.ValidationCollection.Count == 0);

            //string prev = cfg.GroupConstants.Children[0].DataType.ObjectName;
            //cfg.GroupConstants.Children[0].DataType.ObjectName = "123";
            //cfg.ValidateSubTreeFromNode(cfg);
            //Assert.True(cfg.CountErrors == 1);
            //Assert.True(cfg.CountInfos == 0);
            //Assert.True(cfg.CountWarnings == 0);
            //Assert.True(cfg.ValidationCollection.Count == 1);
            //Assert.True(cfg.GroupConstants.Children[0].CountErrors == 1);
            //Assert.True(cfg.GroupConstants.Children[0].CountInfos == 0);
            //Assert.True(cfg.GroupConstants.Children[0].CountWarnings == 0);
            //Assert.True(cfg.GroupConstants.Children[0].DataType.ValidationCollection.Count == 1);


            //cfg.GroupConstants.Children[0].DataType.ObjectName = prev;
            //cfg.GroupEnumerations.Children[0].Name = "1a";
            //cfg.ValidateSubTreeFromNode(cfg);
            //Assert.True(cfg.GroupEnumerations.Children[0].CountErrors == 1);
            //Assert.True(cfg.GroupEnumerations.Children[0].CountInfos == 0);
            //Assert.True(cfg.GroupEnumerations.Children[0].CountWarnings == 0);
            //Assert.True(cfg.GroupEnumerations.Children[0].HasErrors);
            //Assert.True(cfg.GroupEnumerations.Children[0].ValidationCollection.Count == 1);
            //Assert.True(cfg.GroupEnumerations.Children[0].ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            //Assert.True(cfg.GroupEnumerations.Children[0].ValidationCollection[0].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT);

            //// intermediate node contains only validation count
            //Assert.True(cfg.GroupEnumerations.ValidationCollection.Count == 0);
            //Assert.True(cfg.GroupEnumerations.CountErrors == 1);
            //Assert.True(cfg.GroupEnumerations.CountInfos == 0);
            //Assert.True(cfg.GroupEnumerations.CountWarnings == 0);

            //// ValidateSubTreeFromNode(node). node contains full list of validations
            //Assert.True(cfg.CountErrors == 1);
            //Assert.True(cfg.CountInfos == 0);
            //Assert.True(cfg.CountWarnings == 0);
            //Assert.True(cfg.ValidationCollection.Count == 1);
            //Assert.True(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            //Assert.True(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT);

            //cfg.GroupEnumerations.Children[0].Name = " ab";
            //Assert.True(cfg.GroupEnumerations.Children[0].Name == "ab");
            //cfg.GroupEnumerations.Children[0].Validate();
            //Assert.False(cfg.GroupEnumerations.Children[0].HasErrors);

            //cfg.GroupEnumerations.Children[0].Name = "ab ";
            //Assert.True(cfg.GroupEnumerations.Children[0].Name == "ab");
            //cfg.GroupEnumerations.Children[0].Validate();
            //Assert.False(cfg.GroupEnumerations.Children[0].HasErrors);

            //cfg.GroupEnumerations.Children[0].Name = "a b";
            //cfg.ValidateSubTreeFromNode(cfg);
            //Assert.True(cfg.ValidationCollection.Count == 1);
            //Assert.True(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            //Assert.True(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);

            //cfg.GroupEnumerations.NodeAddNewSubNode();
            //cfg.GroupEnumerations.Children[0].Name = "ab";
            //cfg.GroupEnumerations.Children[1].Name = "ab";
            //cfg.ValidateSubTreeFromNode(cfg);
            //Assert.True(cfg.ValidationCollection.Count == 2);
            //Assert.True(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            //Assert.True(cfg.ValidationCollection[1].Severity == FluentValidation.Severity.Error);
            //Assert.True(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            //Assert.True(cfg.ValidationCollection[1].Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            //Assert.True(cfg.GroupEnumerations.Children[1].ValidationCollection.Count == 1);
            //Assert.True(cfg.GroupEnumerations.Children[1].ValidationCollection[0].Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            //Assert.True(cfg.GroupEnumerations.Children[1].HasErrors == true);
            //var errenum = cfg.GroupEnumerations.Children[1].GetErrors("Name").GetEnumerator();
            //Assert.True(errenum.MoveNext() == true);
            //Assert.True((string)errenum.Current == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            //Assert.True(errenum.MoveNext() == false);
        }
        [Fact]
        public void Compare001_CanNothingChanged()
        {
            Assert.True(false);
        }
        [Fact]
        public void Compare002_CanFindAdded()
        {
            Assert.True(false);
        }
        [Fact]
        public void Compare003_CanFindDeleted()
        {
            Assert.True(false);
        }
        [Fact]
        public void Compare004_CanFindChanged()
        {
            Assert.True(false);
        }
        #endregion Compare Tree
    }
}
