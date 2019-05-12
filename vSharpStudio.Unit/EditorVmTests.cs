﻿using System;
using vSharpStudio.vm.ViewModels;
using ViewModelBase;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace vSharpStudio.xUnit
{
    [TestClass]
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
            //Proto.Config.proto_config dto = new Proto.Config.proto_config();
            //Config cfg = new Config(dto);
            //Constant c = Constant.Create();
            //cfg.GroupConstants.Children.Add(c);
            var cfg = new Config();
            Assert.IsTrue(cfg.Guid.Length > 0);
        }
        [TestMethod]
        public void Config002CanSaveAndRestore()
        {
            var cfg = new Config();
            cfg.GroupConstants.NodeAddNewSubNode();
            string json = cfg.ExportToJson();
            Assert.IsTrue(json.Length > 0);
            var cfg2 = new Config(json);
            Assert.IsTrue(cfg2.GroupConstants.Children.Count == 1);
            Assert.IsTrue(cfg2.GroupConstants.Children[0].Name == typeof(Constant).Name + 1);
        }
        [TestMethod]
        public void Config003CanSaveAndRestoreSortingValue()
        {
            var cfg = new Config();
            cfg.GroupConstants.NodeAddNewSubNode();
            cfg.GroupConstants.NodeAddNewSubNode();
            cfg.GroupConstants.Children[1].NodeMoveUp();
            string json = cfg.ExportToJson();
            Assert.IsTrue(json.Length > 0);
            var cfg2 = new Config(json);
            Assert.IsTrue(cfg2.GroupConstants.Children.Count == 2);
            Assert.IsTrue(cfg2.GroupConstants.Children[0].Name == typeof(Constant).Name + 2);
        }
        // TODO business validation tests
        //[TestMethod]
        //public void Config003ValidationIsDbFromConnectionStringInfoConnectionStringName()
        //{
        //    var cfg = new Config(new SortedObservableCollection<ValidationMessage>());
        //    cfg.IsDbFromConnectionString = true;
        //    cfg.Validate();
        //    Assert.False(cfg.HasErrors);
        //    Assert.IsTrue(cfg.HasWarnings);
        //    Assert.False(cfg.HasInfos);
        //    Assert.IsTrue(cfg.ValidationCollection.Count == 1);
        //    Assert.IsTrue(cfg.ValidationCollection[0].SortingValue >= 1 << ValidationMessage.MultiplierShift);
        //}
        #endregion Config

        #region Constant
        [TestMethod]
        public void Constant001GuidInit()
        {
            var cfg = new Constant();
            Assert.IsTrue(cfg.Guid.Length > 0);
        }
        [TestMethod]
        public void Constant002AddedParent()
        {
            var cfg = new Config();
            cfg.GroupConstants.NodeAddNewSubNode();
            Assert.AreEqual(cfg.GroupConstants.Children[0].Parent.Guid, cfg.GroupConstants.Guid);
            cfg.GroupConstants.Children[0].NodeAddNew();
            Assert.AreEqual(cfg.GroupConstants.Children[1].Parent.Guid, cfg.GroupConstants.Guid);
        }
        [TestMethod]
        public void Constant003AddedDefaultName()
        {
            var cfg = new Config();
            cfg.GroupConstants.NodeAddNewSubNode();
            Assert.AreEqual(Constant.DefaultName + "1", cfg.GroupConstants.Children[0].Name);
            cfg.GroupConstants.Children[0].NodeAddNew();
            Assert.AreEqual(Constant.DefaultName + "2", cfg.GroupConstants.Children[1].Name);
        }
        #endregion Constant

        #region Enum
        [TestMethod]
        public void Enum001GuidInit()
        {
            var cfg = new Enumeration();
            Assert.IsTrue(cfg.Guid.Length > 0);
        }
        [TestMethod]
        public void Enum002AddedParent()
        {
            var cfg = new Config();
            cfg.GroupEnumerations.NodeAddNewSubNode();
            Assert.AreEqual(cfg.GroupEnumerations.Children[0].Parent.Guid, cfg.GroupEnumerations.Guid);
            cfg.GroupEnumerations.Children[0].NodeAddNew();
            Assert.AreEqual(cfg.GroupEnumerations.Children[1].Parent.Guid, cfg.GroupEnumerations.Guid);
        }
        #endregion Enum

        #region Property
        [TestMethod]
        public void Property001GuidInit()
        {
            var cfg = new Property();
            Assert.IsTrue(cfg.Guid.Length > 0);
        }
        #endregion Property

        #region Catalog
        [TestMethod]
        public void Catalog001GuidInit()
        {
            var cfg = new Catalog();
            Assert.IsTrue(cfg.Guid.Length > 0);
        }
        #endregion Catalog

        #region Diff
        //[TestMethod]
        //public void DiffConstant001Added()
        //{
        //    Assert.IsTrue(false);
        //}
        //[TestMethod]
        //public void DiffConfig001CanDiffwithDb()
        //{
        //    Assert.IsTrue(false);
        //}
        #endregion Diff

        #region ITreeConfigNode
        [TestMethod]
        public void ITreeConfigNode001_UpdateSortingValueWhenNameIsChanged()
        {
            var cfg = new Config();
            ViewModelBindable.isNotValidateForUnitTests = true;

            var cnst = new Constant() { Parent = cfg.GroupConstants };
            cfg.GroupConstants.Children.Add(cnst);
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

            //cnst.Name = "__";
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
            var cnst = new Constant() { Parent = cfg.GroupConstants };
            cfg.GroupConstants.Children.Add(cnst);
            cnst.Name = "abc1";
            var curr = cnst.SortingValue;

            string json = cfg.ExportToJson();
            var cfg2 = new Config(json);

            Assert.IsTrue(cfg2.GroupConstants.Children[0].Name == cnst.Name);
            Assert.IsTrue(cfg2.GroupConstants.Children[0].SortingValue == cnst.SortingValue);
        }
        [TestMethod]
        public void ITreeConfigNode003_ReSortedWhenSortingValueIsChanged()
        {
            var cfg = new Config();
            var cnst = new Constant() { Parent = cfg.GroupConstants };
            cfg.GroupConstants.Children.Add(cnst);
            cnst.Name = "abc1";

            var cnst2 = new Constant() { Parent = cfg.GroupConstants };
            cfg.GroupConstants.Children.Add(cnst2);
            cnst2.Name = "abc1";

            Assert.IsTrue(cnst.Guid != cnst2.Guid);

            cnst2.Name = "abc0";
            Assert.IsTrue(cfg.GroupConstants.Children[0].SortingValue < cfg.GroupConstants.Children[1].SortingValue);
            Assert.IsTrue(cfg.GroupConstants.Children[1].Guid == cnst.Guid);
            Assert.IsTrue(cfg.GroupConstants.Children[0].Guid == cnst2.Guid);

            cnst2.Name = "abc2";
            Assert.IsTrue(cfg.GroupConstants.Children[0].SortingValue < cfg.GroupConstants.Children[1].SortingValue);
            Assert.IsTrue(cfg.GroupConstants.Children[0].Guid == cnst.Guid);
            Assert.IsTrue(cfg.GroupConstants.Children[1].Guid == cnst2.Guid);
        }
        [TestMethod]
        public void ITreeConfigNode003_CanConfigTreeCommands()
        {
            var cfg = new Config();

            #region Constants

            Assert.IsTrue(cfg.GroupConstants.NodeCanLeft() == false);
            Assert.IsTrue(cfg.GroupConstants.NodeCanRight() == true);
            Assert.IsTrue(cfg.GroupConstants.NodeCanMoveUp() == false);
            Assert.IsTrue(cfg.GroupConstants.NodeCanMoveDown() == false);
            Assert.IsTrue(cfg.GroupConstants.NodeCanAddNew() == false);
            Assert.IsTrue(cfg.GroupConstants.NodeCanAddNewSubNode() == true);
            Assert.IsTrue(cfg.SelectedNode == null);
            cfg.GroupConstants.NodeAddNewSubNode();
            Assert.IsTrue(cfg.SelectedNode != null);
            Assert.IsTrue(cfg.SelectedNode == cfg.GroupConstants.Children[0]);
            Assert.IsTrue(cfg.SelectedNode.Guid == cfg.GroupConstants.Children[0].Guid);

            Assert.IsTrue(cfg.GroupConstants.NodeCanRight() == true);
            Assert.IsTrue(cfg.GroupConstants.Children[0].NodeCanLeft() == true);
            Assert.IsTrue(cfg.GroupConstants.Children[0].NodeCanRight() == false);
            Assert.IsTrue(cfg.GroupConstants.Children[0].NodeCanMoveUp() == false);
            Assert.IsTrue(cfg.GroupConstants.Children[0].NodeCanMoveDown() == false);
            Assert.IsTrue(cfg.GroupConstants.Children[0].NodeCanAddNew() == true);
            Assert.IsTrue(cfg.GroupConstants.Children[0].NodeCanAddNewSubNode() == false);

            cfg.GroupConstants.NodeAddNewSubNode();
            Assert.IsTrue(cfg.GroupConstants.Children[0].NodeCanMoveUp() == false);
            Assert.IsTrue(cfg.GroupConstants.Children[0].NodeCanMoveDown() == true);
            Assert.IsTrue(cfg.GroupConstants.Children[1].NodeCanMoveUp() == true);
            Assert.IsTrue(cfg.GroupConstants.Children[1].NodeCanMoveDown() == false);

            #endregion Constants

            #region Enumerations

            Assert.IsTrue(cfg.GroupEnumerations.NodeCanLeft() == false);
            Assert.IsTrue(cfg.GroupEnumerations.NodeCanRight() == true);
            Assert.IsTrue(cfg.GroupEnumerations.NodeCanMoveUp() == false);
            Assert.IsTrue(cfg.GroupEnumerations.NodeCanMoveDown() == false);
            Assert.IsTrue(cfg.GroupEnumerations.NodeCanAddNew() == false);
            Assert.IsTrue(cfg.GroupEnumerations.NodeCanAddNewSubNode() == true);
            cfg.GroupEnumerations.NodeAddNewSubNode();
            Assert.IsTrue(cfg.SelectedNode != null);
            Assert.IsTrue(cfg.SelectedNode == cfg.GroupEnumerations.Children[0]);
            Assert.IsTrue(cfg.SelectedNode.Guid == cfg.GroupEnumerations.Children[0].Guid);

            Assert.IsTrue(cfg.GroupEnumerations.NodeCanRight() == true);
            Assert.IsTrue(cfg.GroupEnumerations.Children[0].NodeCanLeft() == true);
            Assert.IsTrue(cfg.GroupEnumerations.Children[0].NodeCanRight() == true);
            Assert.IsTrue(cfg.GroupEnumerations.Children[0].NodeCanMoveUp() == false);
            Assert.IsTrue(cfg.GroupEnumerations.Children[0].NodeCanMoveDown() == false);
            Assert.IsTrue(cfg.GroupEnumerations.Children[0].NodeCanAddNew() == true);
            Assert.IsTrue(cfg.GroupEnumerations.Children[0].NodeCanAddNewSubNode() == false);

            //#region Properties

            //cfg.GroupCatalogs.Children[0].NodeAddNewSubNode();
            //Assert.IsTrue(cfg.SelectedNode != null);
            //Assert.IsTrue(cfg.SelectedNode == cfg.GroupCatalogs.Children[0].ListProperties[0]);
            //Assert.IsTrue(cfg.GroupCatalogs.Children[0].ListProperties[0].NodeCanLeft() == true);
            //Assert.IsTrue(cfg.GroupCatalogs.Children[0].ListProperties[0].NodeCanRight() == true);
            //Assert.IsTrue(cfg.GroupCatalogs.Children[0].ListProperties[0].NodeCanMoveUp() == false);
            //Assert.IsTrue(cfg.GroupCatalogs.Children[0].ListProperties[0].NodeCanMoveDown() == false);
            //Assert.IsTrue(cfg.GroupCatalogs.Children[0].ListProperties[0].NodeCanAddNew() == true);
            //Assert.IsTrue(cfg.GroupCatalogs.Children[0].ListProperties[0].NodeCanAddNewSubNode() == true);

            //cfg.GroupCatalogs.Children[0].ListProperties[0].NodeAddNew();
            //Assert.IsTrue(cfg.SelectedNode == cfg.GroupCatalogs.Children[0].ListProperties[1]);
            //Assert.IsTrue(cfg.SelectedNode.Guid == cfg.GroupCatalogs.Children[0].ListProperties[1].Guid);
            //Assert.IsTrue(cfg.GroupCatalogs.Children[0].ListProperties[1].NodeCanLeft() == true);
            //Assert.IsTrue(cfg.GroupCatalogs.Children[0].ListProperties[1].NodeCanRight() == true);
            //Assert.IsTrue(cfg.GroupCatalogs.Children[0].ListProperties[1].NodeCanMoveUp() == true);
            //Assert.IsTrue(cfg.GroupCatalogs.Children[0].ListProperties[1].NodeCanMoveDown() == false);
            //Assert.IsTrue(cfg.GroupCatalogs.Children[0].ListProperties[1].NodeCanAddNew() == true);
            //Assert.IsTrue(cfg.GroupCatalogs.Children[0].ListProperties[1].NodeCanAddNewSubNode() == true);

            //var p = cfg.GroupCatalogs.Children[0].ListProperties[1];
            //p.NodeMoveUp();
            //Assert.IsTrue(p == cfg.GroupCatalogs.Children[0].ListProperties[0]);
            //Assert.IsTrue(cfg.SelectedNode == cfg.GroupCatalogs.Children[0].ListProperties[0]);

            //// change property parameters
            //p.DataType.MinValue = 5;
            //p.DataType.MaxValue = 6;

            //p.NodeAddClone();
            //Assert.IsTrue(p == cfg.GroupCatalogs.Children[0].ListProperties[0]);
            //Assert.IsTrue(cfg.GroupCatalogs.Children[0].ListProperties[1].Name == cfg.GroupCatalogs.Children[0].ListProperties[0].Name + "2");
            //Assert.IsTrue(5 == cfg.GroupCatalogs.Children[0].ListProperties[1].DataType.MinValue);
            //Assert.IsTrue(6 == cfg.GroupCatalogs.Children[0].ListProperties[1].DataType.MaxValue);

            //#endregion Properties

            #endregion Enumerations

            #region Catalogs

            Assert.IsTrue(cfg.GroupCatalogs.NodeCanLeft() == false);
            Assert.IsTrue(cfg.GroupCatalogs.NodeCanRight() == true);
            Assert.IsTrue(cfg.GroupCatalogs.NodeCanMoveUp() == false);
            Assert.IsTrue(cfg.GroupCatalogs.NodeCanMoveDown() == false);
            Assert.IsTrue(cfg.GroupCatalogs.NodeCanAddNew() == false);
            Assert.IsTrue(cfg.GroupCatalogs.NodeCanAddNewSubNode() == true);
            cfg.GroupCatalogs.NodeAddNewSubNode();
            Assert.IsTrue(cfg.SelectedNode != null);
            Assert.IsTrue(cfg.SelectedNode == cfg.GroupCatalogs.Children[0]);
            Assert.IsTrue(cfg.SelectedNode.Guid == cfg.GroupCatalogs.Children[0].Guid);

            Assert.IsTrue(cfg.GroupCatalogs.NodeCanRight() == true);
            Assert.IsTrue(cfg.GroupCatalogs.Children[0].NodeCanLeft() == true);
            Assert.IsTrue(cfg.GroupCatalogs.Children[0].NodeCanRight() == true);
            Assert.IsTrue(cfg.GroupCatalogs.Children[0].NodeCanMoveUp() == false);
            Assert.IsTrue(cfg.GroupCatalogs.Children[0].NodeCanMoveDown() == false);
            Assert.IsTrue(cfg.GroupCatalogs.Children[0].NodeCanAddNew() == true);
            Assert.IsTrue(cfg.GroupCatalogs.Children[0].NodeCanAddNewSubNode() == true);

            #region Properties

            cfg.GroupCatalogs.Children[0].NodeAddNewSubNode();
            Assert.IsTrue(cfg.SelectedNode != null);
            Assert.IsTrue(cfg.SelectedNode == cfg.GroupCatalogs[0].GroupProperties[0]);
            Assert.IsTrue(cfg.GroupCatalogs[0].GroupProperties[0].NodeCanLeft() == true);
            Assert.IsTrue(cfg.GroupCatalogs[0].GroupProperties[0].NodeCanRight() == false);
            Assert.IsTrue(cfg.GroupCatalogs[0].GroupProperties[0].NodeCanMoveUp() == false);
            Assert.IsTrue(cfg.GroupCatalogs[0].GroupProperties[0].NodeCanMoveDown() == false);
            Assert.IsTrue(cfg.GroupCatalogs[0].GroupProperties[0].NodeCanAddNew() == true);
            Assert.IsTrue(cfg.GroupCatalogs[0].GroupProperties[0].NodeCanAddNewSubNode() == false);

            cfg.GroupCatalogs[0].GroupProperties[0].NodeAddNew();
            Assert.IsTrue(cfg.SelectedNode == cfg.GroupCatalogs[0].GroupProperties[1]);
            Assert.IsTrue(cfg.SelectedNode.Guid == cfg.GroupCatalogs[0].GroupProperties[1].Guid);
            Assert.IsTrue(cfg.GroupCatalogs[0].GroupProperties[1].NodeCanLeft() == true);
            Assert.IsTrue(cfg.GroupCatalogs[0].GroupProperties[1].NodeCanRight() == false);
            Assert.IsTrue(cfg.GroupCatalogs[0].GroupProperties[1].NodeCanMoveUp() == true);
            Assert.IsTrue(cfg.GroupCatalogs[0].GroupProperties[1].NodeCanMoveDown() == false);
            Assert.IsTrue(cfg.GroupCatalogs[0].GroupProperties[1].NodeCanAddNew() == true);
            Assert.IsTrue(cfg.GroupCatalogs[0].GroupProperties[1].NodeCanAddNewSubNode() == false);

            var p = cfg.GroupCatalogs[0].GroupProperties[1];
            p.NodeMoveUp();
            Assert.IsTrue(p == cfg.GroupCatalogs[0].GroupProperties[0]);
            Assert.IsTrue(cfg.SelectedNode == cfg.GroupCatalogs[0].GroupProperties[0]);

            // change property parameters
            //p.DataType.MinValue = 5;
            //p.DataType.MaxValue = 6;

            p.NodeAddClone();
            Assert.IsTrue(p == cfg.GroupCatalogs[0].GroupProperties[2]);
            Assert.IsTrue(cfg.GroupCatalogs[0].GroupProperties[2].Name == cfg.GroupCatalogs[0].GroupProperties[0].Name + "2");
            //Assert.IsTrue(5 == cfg.GroupCatalogs.Children[0].GroupProperties.ListProperties[2].DataType.MinValue);
            //Assert.IsTrue(6 == cfg.GroupCatalogs.Children[0].GroupProperties.ListProperties[2].DataType.MaxValue);

            #endregion Properties

            #endregion Catalogs

        }
        #endregion ITreeConfigNode

        #region Compare Tree
        private Config createTree()
        {
            var cfg = new Config();

            cfg.GroupEnumerations.NodeAddNewSubNode();
            cfg.GroupEnumerations[0].DataTypeEnum = Proto.Config.proto_enumeration.Types.EnumEnumerationType.Integer;
            cfg.GroupEnumerations[0].Children.Add(new EnumerationPair() { Name = "one", Value = "1" });

            cfg.GroupConstants.NodeAddNewSubNode();
            cfg.GroupConstants[0].DataType.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.Bool;
            cfg.GroupConstants.NodeAddNewSubNode();
            cfg.GroupConstants[1].DataType.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.Enumeration;
            cfg.GroupConstants[1].DataType.ObjectGuid = cfg.GroupEnumerations.Children[0].Guid;

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
            //Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            //Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            //Assert.IsTrue(dt.VisibilityObjectName == Visibility.Collapsed);

            dt.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.Any;
            dt.Validate();
            Assert.IsTrue(dt.CountErrors == 0);
            Assert.IsTrue(dt.CountInfos == 0);
            Assert.IsTrue(dt.CountWarnings == 0);
            Assert.IsTrue(dt.ValidationCollection.Count == 0);
            //Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            //Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            //Assert.IsTrue(dt.VisibilityObjectName == Visibility.Collapsed);

            dt.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.Bool;
            dt.Validate();
            Assert.IsTrue(dt.CountErrors == 0);
            Assert.IsTrue(dt.CountInfos == 0);
            Assert.IsTrue(dt.CountWarnings == 0);
            Assert.IsTrue(dt.ValidationCollection.Count == 0);
            //Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            //Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            //Assert.IsTrue(dt.VisibilityObjectName == Visibility.Collapsed);

            dt.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.Catalog;
            dt.Validate();
            Assert.IsTrue(dt.CountErrors == 0);
            Assert.IsTrue(dt.CountInfos == 0);
            Assert.IsTrue(dt.CountWarnings == 0);
            Assert.IsTrue(dt.HasErrors);
            Assert.IsTrue(dt.ValidationCollection.Count == 1);
            Assert.IsTrue(dt.ValidationCollection[0].Message == Config.ValidationMessages.TYPE_EMPTY_CATALOG);
            //Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            //Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            //Assert.IsTrue(dt.VisibilityObjectName == Visibility.Visible);

            dt.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.Catalogs;
            dt.Validate();
            Assert.IsTrue(dt.CountErrors == 0);
            Assert.IsTrue(dt.CountInfos == 0);
            Assert.IsTrue(dt.CountWarnings == 0);
            Assert.IsTrue(dt.ValidationCollection.Count == 0);
            //Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            //Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            //Assert.IsTrue(dt.VisibilityObjectName == Visibility.Collapsed);

            //dt.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.Constant;
            //dt.Validate();
            //Assert.IsTrue(dt.CountErrors == 0);
            //Assert.IsTrue(dt.CountInfos == 0);
            //Assert.IsTrue(dt.CountWarnings == 0);
            //Assert.IsTrue(dt.HasErrors);
            //Assert.IsTrue(dt.ValidationCollection.Count == 1);
            //Assert.IsTrue(dt.ValidationCollection[0].Message == Config.ValidationMessages.TYPE_EMPTY_CONSTANT_NAME);
            //Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            //Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            //Assert.IsTrue(dt.VisibilityObjectName == Visibility.Visible);

            dt.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.Enumeration;
            dt.Validate();
            Assert.IsTrue(dt.CountErrors == 0);
            Assert.IsTrue(dt.CountInfos == 0);
            Assert.IsTrue(dt.CountWarnings == 0);
            Assert.IsTrue(dt.HasErrors);
            Assert.IsTrue(dt.ValidationCollection.Count == 1);
            Assert.IsTrue(dt.ValidationCollection[0].Message == Config.ValidationMessages.TYPE_EMPTY_ENUMERATION);
            //Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            //Assert.IsTrue(dt.VisibilityLength == Visibility.Collapsed);
            //Assert.IsTrue(dt.VisibilityObjectName == Visibility.Visible);

            dt.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.Numerical;
            dt.Validate();
            Assert.IsTrue(dt.CountErrors == 0);
            Assert.IsTrue(dt.CountInfos == 0);
            Assert.IsTrue(dt.CountWarnings == 0);
            //Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Visible);
            //Assert.IsTrue(dt.VisibilityLength == Visibility.Visible);
            //Assert.IsTrue(dt.VisibilityObjectName == Visibility.Collapsed);

            dt.DataTypeEnum = Proto.Config.proto_data_type.Types.EnumDataType.String;
            dt.Validate();
            Assert.IsTrue(dt.CountErrors == 0);
            Assert.IsTrue(dt.CountInfos == 0);
            Assert.IsTrue(dt.CountWarnings == 0);
            Assert.IsTrue(dt.ValidationCollection.Count == 0);
            //Assert.IsTrue(dt.VisibilityAccuracy == Visibility.Collapsed);
            //Assert.IsTrue(dt.VisibilityLength == Visibility.Visible);
            //Assert.IsTrue(dt.VisibilityObjectName == Visibility.Collapsed);
        }
        [TestMethod]
        public void Rules002_Enumeration()
        {
            var cfg = createTree();

            cfg.ValidateSubTreeFromNode(cfg);
            Assert.IsTrue(cfg.CountErrors == 0);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.ValidationCollection.Count == 0);

            cfg.GroupEnumerations.Children[0].Name = "1a";
            cfg.ValidateSubTreeFromNode(cfg);
            Assert.IsTrue(cfg.GroupEnumerations[0].CountErrors == 1);
            Assert.IsTrue(cfg.GroupEnumerations[0].CountInfos == 0);
            Assert.IsTrue(cfg.GroupEnumerations[0].CountWarnings == 0);
            Assert.IsTrue(cfg.GroupEnumerations[0].HasErrors);
            Assert.IsTrue(cfg.GroupEnumerations[0].ValidationCollection.Count == 1);
            Assert.IsTrue(cfg.GroupEnumerations[0].ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            Assert.IsTrue(cfg.GroupEnumerations[0].ValidationCollection[0].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT);

            // intermediate node contains only validation count
            Assert.IsTrue(cfg.GroupEnumerations.ValidationCollection.Count == 0);
            Assert.IsTrue(cfg.GroupEnumerations.CountErrors == 1);
            Assert.IsTrue(cfg.GroupEnumerations.CountInfos == 0);
            Assert.IsTrue(cfg.GroupEnumerations.CountWarnings == 0);

            // ValidateSubTreeFromNode(node). node contains full list of validations
            Assert.IsTrue(cfg.CountErrors == 2);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.ValidationCollection.Count == 2);
            Assert.IsTrue(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            Assert.IsTrue(cfg.ValidationCollection[1].Severity == FluentValidation.Severity.Error);
            if (cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT)
            {
                Assert.IsTrue(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT);
                Assert.IsTrue(cfg.ValidationCollection[1].Message == Config.ValidationMessages.TYPE_OBJECT_IS_NOT_FOUND);
            }
            else
            {
                Assert.IsTrue(cfg.ValidationCollection[1].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT);
                Assert.IsTrue(cfg.ValidationCollection[0].Message == Config.ValidationMessages.TYPE_OBJECT_IS_NOT_FOUND);
            }

            cfg.GroupEnumerations.Children[0].Name = " ab";
            Assert.IsTrue(cfg.GroupEnumerations.Children[0].Name == "ab");
            cfg.GroupEnumerations.Children[0].Validate();
            Assert.IsFalse(cfg.GroupEnumerations[0].HasErrors);

            cfg.GroupEnumerations.Children[0].Name = "ab ";
            Assert.IsTrue(cfg.GroupEnumerations.Children[0].Name == "ab");
            cfg.GroupEnumerations.Children[0].Validate();
            Assert.IsFalse(cfg.GroupEnumerations[0].HasErrors);

            cfg.GroupEnumerations.Children[0].Name = "a b";
            //cfg.GroupConstants[1].DataType.ObjectName = "a b";
            cfg.ValidateSubTreeFromNode(cfg);
            Assert.IsTrue(cfg.ValidationCollection.Count == 1);
            Assert.IsTrue(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            Assert.IsTrue(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);

            cfg.GroupEnumerations.NodeAddNewSubNode();
            cfg.GroupEnumerations.Children[0].Name = "ab";
            cfg.GroupEnumerations.Children[1].Name = "ab";
            //cfg.GroupConstants[1].DataType.ObjectName = "ab";
            cfg.ValidateSubTreeFromNode(cfg);
            Assert.IsTrue(cfg.ValidationCollection.Count == 2);
            Assert.IsTrue(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            Assert.IsTrue(cfg.ValidationCollection[1].Severity == FluentValidation.Severity.Error);
            Assert.IsTrue(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            Assert.IsTrue(cfg.ValidationCollection[1].Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            Assert.IsTrue(cfg.GroupEnumerations.Children[1].ValidationCollection.Count == 1);
            Assert.IsTrue(cfg.GroupEnumerations.Children[1].ValidationCollection[0].Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            Assert.IsTrue(cfg.GroupEnumerations[1].HasErrors == true);
            var errenum = cfg.GroupEnumerations[1].GetErrors("Name").GetEnumerator();
            Assert.IsTrue(errenum.MoveNext() == true);
            Assert.IsTrue((string)errenum.Current == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            Assert.IsTrue(errenum.MoveNext() == false);
        }
        [TestMethod]
        public void Rules003_Constant()
        {
            var cfg = createTree();

            cfg.ValidateSubTreeFromNode(cfg);
            Assert.IsTrue(cfg.CountErrors == 0);
            Assert.IsTrue(cfg.CountInfos == 0);
            Assert.IsTrue(cfg.CountWarnings == 0);
            Assert.IsTrue(cfg.ValidationCollection.Count == 0);

            //string prev = cfg.GroupConstants.Children[0].DataType.ObjectName;
            //cfg.GroupConstants.Children[0].DataType.ObjectName = "123";
            //cfg.ValidateSubTreeFromNode(cfg);
            //Assert.IsTrue(cfg.CountErrors == 1);
            //Assert.IsTrue(cfg.CountInfos == 0);
            //Assert.IsTrue(cfg.CountWarnings == 0);
            //Assert.IsTrue(cfg.ValidationCollection.Count == 1);
            //Assert.IsTrue(cfg.GroupConstants.Children[0].CountErrors == 1);
            //Assert.IsTrue(cfg.GroupConstants.Children[0].CountInfos == 0);
            //Assert.IsTrue(cfg.GroupConstants.Children[0].CountWarnings == 0);
            //Assert.IsTrue(cfg.GroupConstants.Children[0].DataType.ValidationCollection.Count == 1);


            //cfg.GroupConstants.Children[0].DataType.ObjectName = prev;
            //cfg.GroupEnumerations.Children[0].Name = "1a";
            //cfg.ValidateSubTreeFromNode(cfg);
            //Assert.IsTrue(cfg.GroupEnumerations.Children[0].CountErrors == 1);
            //Assert.IsTrue(cfg.GroupEnumerations.Children[0].CountInfos == 0);
            //Assert.IsTrue(cfg.GroupEnumerations.Children[0].CountWarnings == 0);
            //Assert.IsTrue(cfg.GroupEnumerations.Children[0].HasErrors);
            //Assert.IsTrue(cfg.GroupEnumerations.Children[0].ValidationCollection.Count == 1);
            //Assert.IsTrue(cfg.GroupEnumerations.Children[0].ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            //Assert.IsTrue(cfg.GroupEnumerations.Children[0].ValidationCollection[0].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT);

            //// intermediate node contains only validation count
            //Assert.IsTrue(cfg.GroupEnumerations.ValidationCollection.Count == 0);
            //Assert.IsTrue(cfg.GroupEnumerations.CountErrors == 1);
            //Assert.IsTrue(cfg.GroupEnumerations.CountInfos == 0);
            //Assert.IsTrue(cfg.GroupEnumerations.CountWarnings == 0);

            //// ValidateSubTreeFromNode(node). node contains full list of validations
            //Assert.IsTrue(cfg.CountErrors == 1);
            //Assert.IsTrue(cfg.CountInfos == 0);
            //Assert.IsTrue(cfg.CountWarnings == 0);
            //Assert.IsTrue(cfg.ValidationCollection.Count == 1);
            //Assert.IsTrue(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            //Assert.IsTrue(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_START_WITH_DIGIT);

            //cfg.GroupEnumerations.Children[0].Name = " ab";
            //Assert.IsTrue(cfg.GroupEnumerations.Children[0].Name == "ab");
            //cfg.GroupEnumerations.Children[0].Validate();
            //Assert.False(cfg.GroupEnumerations.Children[0].HasErrors);

            //cfg.GroupEnumerations.Children[0].Name = "ab ";
            //Assert.IsTrue(cfg.GroupEnumerations.Children[0].Name == "ab");
            //cfg.GroupEnumerations.Children[0].Validate();
            //Assert.False(cfg.GroupEnumerations.Children[0].HasErrors);

            //cfg.GroupEnumerations.Children[0].Name = "a b";
            //cfg.ValidateSubTreeFromNode(cfg);
            //Assert.IsTrue(cfg.ValidationCollection.Count == 1);
            //Assert.IsTrue(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            //Assert.IsTrue(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);

            //cfg.GroupEnumerations.NodeAddNewSubNode();
            //cfg.GroupEnumerations.Children[0].Name = "ab";
            //cfg.GroupEnumerations.Children[1].Name = "ab";
            //cfg.ValidateSubTreeFromNode(cfg);
            //Assert.IsTrue(cfg.ValidationCollection.Count == 2);
            //Assert.IsTrue(cfg.ValidationCollection[0].Severity == FluentValidation.Severity.Error);
            //Assert.IsTrue(cfg.ValidationCollection[1].Severity == FluentValidation.Severity.Error);
            //Assert.IsTrue(cfg.ValidationCollection[0].Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            //Assert.IsTrue(cfg.ValidationCollection[1].Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            //Assert.IsTrue(cfg.GroupEnumerations.Children[1].ValidationCollection.Count == 1);
            //Assert.IsTrue(cfg.GroupEnumerations.Children[1].ValidationCollection[0].Message == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            //Assert.IsTrue(cfg.GroupEnumerations.Children[1].HasErrors == true);
            //var errenum = cfg.GroupEnumerations.Children[1].GetErrors("Name").GetEnumerator();
            //Assert.IsTrue(errenum.MoveNext() == true);
            //Assert.IsTrue((string)errenum.Current == Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            //Assert.IsTrue(errenum.MoveNext() == false);
        }
        [TestMethod]
        public void Compare001_CanNothingChanged()
        {
            Assert.IsTrue(false);
        }
        [TestMethod]
        public void Compare002_CanFindAdded()
        {
            Assert.IsTrue(false);
        }
        [TestMethod]
        public void Compare003_CanFindDeleted()
        {
            Assert.IsTrue(false);
        }
        [TestMethod]
        public void Compare004_CanFindChanged()
        {
            Assert.IsTrue(false);
        }
        #endregion Compare Tree
    }
}