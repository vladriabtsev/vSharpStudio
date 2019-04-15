using System;
using vSharpStudio.vm.ViewModels;
using Xunit;
using ViewModelBase;
using System.Diagnostics;

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
            //cfg.ConstantGroup.ListConstants.Add(c);
            var cfg = new Config();
            Assert.True(cfg.Guid.Length > 0);
        }
        [Fact]
        public void Config002CanSaveAndRestore()
        {
            var cfg = new Config();
            cfg.ConstantGroup.ListConstants.Add(new Constant());
            string json = cfg.ExportToJson();
            Assert.True(json.Length > 0);
            var cfg2 = new Config(json);
            Assert.True(cfg2.ConstantGroup.ListConstants.Count == 1);
            Assert.True(cfg2.ConstantGroup.ListConstants[0].Name == typeof(Constant).Name + 1);
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
        public void Constant001AddedDefaultName()
        {
            var cfg = new Config();
            cfg.ConstantGroup.NodeAddNewSubNode();
            Assert.Equal(Constant.DefaultName + "1", cfg.ConstantGroup.ListConstants[0].Name);
            cfg.ConstantGroup.ListConstants[0].NodeAddNew();
            Assert.Equal(Constant.DefaultName + "2", cfg.ConstantGroup.ListConstants[1].Name);
        }
        #endregion Constant
        #region Enum
        [Fact]
        public void Enum001GuidInit()
        {
            var cfg = new Enumeration();
            Assert.True(cfg.Guid.Length > 0);
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
            var cnst = new Constant() { Parent = cfg.ConstantGroup };
            cfg.ConstantGroup.ListConstants.Add(cnst);
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
            var cnst = new Constant() { Parent = cfg.ConstantGroup };
            cfg.ConstantGroup.ListConstants.Add(cnst);
            cnst.Name = "abc1";
            var curr = cnst.SortingValue;

            string json = cfg.ExportToJson();
            var cfg2 = new Config(json);

            Assert.True(cfg2.ConstantGroup.ListConstants[0].Name == cnst.Name);
            Assert.True(cfg2.ConstantGroup.ListConstants[0].SortingValue == cnst.SortingValue);
        }
        [Fact]
        public void ITreeConfigNode003_ReSortedWhenSortingValueIsChanged()
        {
            var cfg = new Config();
            var cnst = new Constant() { Parent = cfg.ConstantGroup };
            cfg.ConstantGroup.ListConstants.Add(cnst);
            cnst.Name = "abc1";

            var cnst2 = new Constant() { Parent = cfg.ConstantGroup };
            cfg.ConstantGroup.ListConstants.Add(cnst2);
            cnst2.Name = "abc1";

            Assert.True(cnst.Guid != cnst2.Guid);

            cnst2.Name = "abc0";
            Assert.True(cfg.ConstantGroup.ListConstants[0].SortingValue < cfg.ConstantGroup.ListConstants[1].SortingValue);
            Assert.True(cfg.ConstantGroup.ListConstants[1].Guid == cnst.Guid);
            Assert.True(cfg.ConstantGroup.ListConstants[0].Guid == cnst2.Guid);

            cnst2.Name = "abc2";
            Assert.True(cfg.ConstantGroup.ListConstants[0].SortingValue < cfg.ConstantGroup.ListConstants[1].SortingValue);
            Assert.True(cfg.ConstantGroup.ListConstants[0].Guid == cnst.Guid);
            Assert.True(cfg.ConstantGroup.ListConstants[1].Guid == cnst2.Guid);
        }
        [Fact]
        public void ITreeConfigNode003_CanLeftRight()
        {
            var cfg = new ViewModels.ConfigRoot();

            #region Constants

            Assert.True(cfg.ConstantGroup.NodeCanLeft() == false);
            Assert.True(cfg.ConstantGroup.NodeCanRight() == true);
            Assert.True(cfg.ConstantGroup.NodeCanMoveUp() == false);
            Assert.True(cfg.ConstantGroup.NodeCanMoveDown() == false);
            Assert.True(cfg.ConstantGroup.NodeCanAddNew() == false);
            Assert.True(cfg.ConstantGroup.NodeCanAddNewSubNode() == true);
            Assert.True(cfg.SelectedNode == null);
            cfg.ConstantGroup.NodeAddNewSubNode();
            Assert.True(cfg.SelectedNode != null);
            Assert.True(cfg.SelectedNode == cfg.ConstantGroup.ListConstants[0]);
            Assert.True(cfg.SelectedNode.Guid == cfg.ConstantGroup.ListConstants[0].Guid);

            Assert.True(cfg.ConstantGroup.ListConstants[0].NodeCanLeft() == true);
            Assert.True(cfg.ConstantGroup.ListConstants[0].NodeCanRight() == false);
            Assert.True(cfg.ConstantGroup.ListConstants[0].NodeCanMoveUp() == false);
            Assert.True(cfg.ConstantGroup.ListConstants[0].NodeCanMoveDown() == false);
            Assert.True(cfg.ConstantGroup.ListConstants[0].NodeCanAddNew() == true);
            Assert.True(cfg.ConstantGroup.ListConstants[0].NodeCanAddNewSubNode() == false);

            cfg.ConstantGroup.NodeAddNewSubNode();
            Assert.True(cfg.ConstantGroup.ListConstants[0].NodeCanMoveUp() == false);
            Assert.True(cfg.ConstantGroup.ListConstants[0].NodeCanMoveDown() == true);
            Assert.True(cfg.ConstantGroup.ListConstants[1].NodeCanMoveUp() == true);
            Assert.True(cfg.ConstantGroup.ListConstants[1].NodeCanMoveDown() == false);

            #endregion Constants

            #region Enumerations

            Assert.True(cfg.EnumerationGroup.NodeCanLeft() == false);
            Assert.True(cfg.EnumerationGroup.NodeCanRight() == true);
            Assert.True(cfg.EnumerationGroup.NodeCanMoveUp() == false);
            Assert.True(cfg.EnumerationGroup.NodeCanMoveDown() == false);
            Assert.True(cfg.EnumerationGroup.NodeCanAddNew() == false);
            Assert.True(cfg.EnumerationGroup.NodeCanAddNewSubNode() == true);
            cfg.EnumerationGroup.NodeAddNewSubNode();
            Assert.True(cfg.SelectedNode != null);
            Assert.True(cfg.SelectedNode == cfg.EnumerationGroup.ListEnumerations[0]);
            Assert.True(cfg.SelectedNode.Guid == cfg.EnumerationGroup.ListEnumerations[0].Guid);

            Assert.True(cfg.EnumerationGroup.ListEnumerations[0].NodeCanLeft() == true);
            Assert.True(cfg.EnumerationGroup.ListEnumerations[0].NodeCanRight() == true);
            Assert.True(cfg.EnumerationGroup.ListEnumerations[0].NodeCanMoveUp() == false);
            Assert.True(cfg.EnumerationGroup.ListEnumerations[0].NodeCanMoveDown() == false);
            Assert.True(cfg.EnumerationGroup.ListEnumerations[0].NodeCanAddNew() == true);
            Assert.True(cfg.EnumerationGroup.ListEnumerations[0].NodeCanAddNewSubNode() == false);

            //#region Properties

            //cfg.CatalogGroup.ListCatalogs[0].NodeAddNewSubNode();
            //Assert.True(cfg.SelectedNode != null);
            //Assert.True(cfg.SelectedNode == cfg.CatalogGroup.ListCatalogs[0].ListProperties[0]);
            //Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[0].NodeCanLeft() == true);
            //Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[0].NodeCanRight() == true);
            //Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[0].NodeCanMoveUp() == false);
            //Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[0].NodeCanMoveDown() == false);
            //Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[0].NodeCanAddNew() == true);
            //Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[0].NodeCanAddNewSubNode() == true);

            //cfg.CatalogGroup.ListCatalogs[0].ListProperties[0].NodeAddNew();
            //Assert.True(cfg.SelectedNode == cfg.CatalogGroup.ListCatalogs[0].ListProperties[1]);
            //Assert.True(cfg.SelectedNode.Guid == cfg.CatalogGroup.ListCatalogs[0].ListProperties[1].Guid);
            //Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[1].NodeCanLeft() == true);
            //Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[1].NodeCanRight() == true);
            //Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[1].NodeCanMoveUp() == true);
            //Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[1].NodeCanMoveDown() == false);
            //Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[1].NodeCanAddNew() == true);
            //Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[1].NodeCanAddNewSubNode() == true);

            //var p = cfg.CatalogGroup.ListCatalogs[0].ListProperties[1];
            //p.NodeMoveUp();
            //Assert.True(p == cfg.CatalogGroup.ListCatalogs[0].ListProperties[0]);
            //Assert.True(cfg.SelectedNode == cfg.CatalogGroup.ListCatalogs[0].ListProperties[0]);

            //// change property parameters
            //p.DataType.MinValue = 5;
            //p.DataType.MaxValue = 6;

            //p.NodeAddClone();
            //Assert.True(p == cfg.CatalogGroup.ListCatalogs[0].ListProperties[0]);
            //Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[1].Name == cfg.CatalogGroup.ListCatalogs[0].ListProperties[0].Name + "2");
            //Assert.True(5 == cfg.CatalogGroup.ListCatalogs[0].ListProperties[1].DataType.MinValue);
            //Assert.True(6 == cfg.CatalogGroup.ListCatalogs[0].ListProperties[1].DataType.MaxValue);

            //#endregion Properties

            #endregion Enumerations

            #region Catalogs

            Assert.True(cfg.CatalogGroup.NodeCanLeft() == false);
            Assert.True(cfg.CatalogGroup.NodeCanRight() == true);
            Assert.True(cfg.CatalogGroup.NodeCanMoveUp() == false);
            Assert.True(cfg.CatalogGroup.NodeCanMoveDown() == false);
            Assert.True(cfg.CatalogGroup.NodeCanAddNew() == false);
            Assert.True(cfg.CatalogGroup.NodeCanAddNewSubNode() == true);
            cfg.CatalogGroup.NodeAddNewSubNode();
            Assert.True(cfg.SelectedNode != null);
            Assert.True(cfg.SelectedNode == cfg.CatalogGroup.ListCatalogs[0]);
            Assert.True(cfg.SelectedNode.Guid == cfg.CatalogGroup.ListCatalogs[0].Guid);

            Assert.True(cfg.CatalogGroup.ListCatalogs[0].NodeCanLeft() == true);
            Assert.True(cfg.CatalogGroup.ListCatalogs[0].NodeCanRight() == true);
            Assert.True(cfg.CatalogGroup.ListCatalogs[0].NodeCanMoveUp() == false);
            Assert.True(cfg.CatalogGroup.ListCatalogs[0].NodeCanMoveDown() == false);
            Assert.True(cfg.CatalogGroup.ListCatalogs[0].NodeCanAddNew() == true);
            Assert.True(cfg.CatalogGroup.ListCatalogs[0].NodeCanAddNewSubNode() == true);

            #region Properties

            cfg.CatalogGroup.ListCatalogs[0].NodeAddNewSubNode();
            Assert.True(cfg.SelectedNode != null);
            Assert.True(cfg.SelectedNode == cfg.CatalogGroup.ListCatalogs[0].ListProperties[0]);
            Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[0].NodeCanLeft() == true);
            Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[0].NodeCanRight() == false);
            Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[0].NodeCanMoveUp() == false);
            Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[0].NodeCanMoveDown() == false);
            Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[0].NodeCanAddNew() == true);
            Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[0].NodeCanAddNewSubNode() == false);

            cfg.CatalogGroup.ListCatalogs[0].ListProperties[0].NodeAddNew();
            Assert.True(cfg.SelectedNode == cfg.CatalogGroup.ListCatalogs[0].ListProperties[1]);
            Assert.True(cfg.SelectedNode.Guid == cfg.CatalogGroup.ListCatalogs[0].ListProperties[1].Guid);
            Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[1].NodeCanLeft() == true);
            Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[1].NodeCanRight() == false);
            Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[1].NodeCanMoveUp() == true);
            Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[1].NodeCanMoveDown() == false);
            Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[1].NodeCanAddNew() == true);
            Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[1].NodeCanAddNewSubNode() == false);

            var p = cfg.CatalogGroup.ListCatalogs[0].ListProperties[1];
            p.NodeMoveUp();
            Assert.True(p == cfg.CatalogGroup.ListCatalogs[0].ListProperties[0]);
            Assert.True(cfg.SelectedNode == cfg.CatalogGroup.ListCatalogs[0].ListProperties[0]);

            // change property parameters
            p.DataType.MinValue = 5;
            p.DataType.MaxValue = 6;

            p.NodeAddClone();
            Assert.True(p == cfg.CatalogGroup.ListCatalogs[0].ListProperties[2]);
            Assert.True(cfg.CatalogGroup.ListCatalogs[0].ListProperties[2].Name == cfg.CatalogGroup.ListCatalogs[0].ListProperties[0].Name + "2");
            Assert.True(5 == cfg.CatalogGroup.ListCatalogs[0].ListProperties[2].DataType.MinValue);
            Assert.True(6 == cfg.CatalogGroup.ListCatalogs[0].ListProperties[2].DataType.MaxValue);

            #endregion Properties

            #endregion Catalogs

        }
        #endregion ITreeConfigNode
    }
}
