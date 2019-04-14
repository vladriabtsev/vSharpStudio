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
            var cnst = new Constant();
            cfg.ConstantGroup.ListConstants.Add(cnst);
            Assert.Equal("Constant1", cnst.Name);
            var cnst2 = new Constant();
            cfg.ConstantGroup.ListConstants.Add(cnst2);
            Assert.Equal("Constant2", cnst2.Name);
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
            var cnst = new Constant(cfg.ConstantGroup);
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
            var cnst = new Constant(cfg.ConstantGroup);
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
            var cnst = new Constant(cfg.ConstantGroup);
            cfg.ConstantGroup.ListConstants.Add(cnst);
            cnst.Name = "abc1";

            var cnst2 = new Constant(cfg.ConstantGroup);
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

            #region Catalog
            ITreeConfigNode cnst = cfg.CatalogGroup;
            Assert.True(cnst.NodeCanLeft() == false);
            Assert.True(cnst.NodeCanRight() == true);
            Assert.True(cnst.NodeCanMoveUp() == false);
            Assert.True(cnst.NodeCanMoveDown() == false);
            Assert.True(cnst.NodeCanAddNew() == false);
            Assert.True(cnst.NodeCanAddNewSubNode() == true);
            var ctlg = new Catalog(cfg.CatalogGroup);
            Assert.True(cfg.SelectedNode == null);
            cnst = ctlg;
            Assert.True(cnst.NodeCanLeft() == true);
            Assert.True(cnst.NodeCanRight() == true);
            Assert.True(cnst.NodeCanMoveUp() == false);
            Assert.True(cnst.NodeCanMoveDown() == false);
            Assert.True(cnst.NodeCanAddNew() == true);
            Assert.True(cnst.NodeCanAddNewSubNode() == false);

            #region Properties

            cnst = ctlg.PropertyGroup;
            Assert.True(cnst.NodeCanLeft() == true);
            Assert.True(cnst.NodeCanRight() == true);
            Assert.True(cnst.NodeCanMoveUp() == false);
            Assert.True(cnst.NodeCanMoveDown() == false);
            Assert.True(cnst.NodeCanAddNew() == false);
            Assert.True(cnst.NodeCanAddNew() == false);
            Assert.True(cnst.NodeCanAddNewSubNode() == true);
            cnst = cnst.NodeAddNewSubNode();
            Assert.True(cfg.SelectedNode != null);
            Assert.True(cfg.SelectedNode.Guid == cnst.Guid);
            Assert.True(cnst.NodeCanLeft() == true);
            Assert.True(cnst.NodeCanRight() == false);
            Assert.True(cnst.NodeCanMoveUp() == false);
            Assert.True(cnst.NodeCanMoveDown() == false);
            Assert.True(cnst.NodeCanAddNew() == true);
            Assert.True(cnst.NodeCanAddClone() == true);
            Assert.True(cnst.NodeCanAddNew() == true);
            Assert.True(cnst.NodeCanAddNewSubNode() == false);
            Property prev = (Property)cnst;
            // change property parameters
            prev.DataType.MinValue = 5;
            prev.DataType.MaxValue = 6;

            cnst = cnst.NodeAddClone();
            Assert.True(cfg.SelectedNode != null);
            Assert.True(cfg.SelectedNode.Guid == cnst.Guid);
            Property cloned = (Property)cnst;
            // test cloned property parameters
            Assert.True(cloned.DataType.MinValueString == "5");
            Assert.True(cloned.DataType.MaxValueString == "6");

            Assert.True(cnst.NodeCanLeft() == true);
            Assert.True(cnst.NodeCanRight() == false);
            Assert.True(cnst.NodeCanMoveUp() == true);
            Assert.True(cnst.NodeCanMoveDown() == false);
            cnst.NodeMoveUp();
            Assert.True(cnst.NodeCanMoveUp() == false);
            Assert.True(cnst.NodeCanMoveDown() == true);

            #endregion Properties

            #endregion Catalog

            cnst = cfg.ConstantGroup;
            Assert.True(cnst.NodeCanLeft() == false);
            Assert.True(cnst.NodeCanRight() == true);
            Assert.True(cnst.NodeCanMoveUp() == false);
            Assert.True(cnst.NodeCanMoveDown() == false);
            Assert.True(cnst.NodeCanAddNew() == false);
            Assert.True(cnst.NodeCanAddNewSubNode() == true);
            cnst = new Constant(cfg.ConstantGroup);
            Assert.True(cnst.NodeCanLeft() == true);
            Assert.True(cnst.NodeCanRight() == false);
            Assert.True(cnst.NodeCanMoveUp() == false);
            Assert.True(cnst.NodeCanMoveDown() == false);
            Assert.True(cnst.NodeCanAddNew() == true);
            Assert.True(cnst.NodeCanAddNewSubNode() == false);

            cnst = cfg.EnumerationGroup;
            Assert.True(cnst.NodeCanLeft() == false);
            Assert.True(cnst.NodeCanRight() == true);
            Assert.True(cnst.NodeCanMoveUp() == false);
            Assert.True(cnst.NodeCanMoveDown() == false);
            Assert.True(cnst.NodeCanAddNew() == false);
            Assert.True(cnst.NodeCanAddNewSubNode() == true);
            cnst = new Enumeration(cfg.EnumerationGroup);
            Assert.True(cnst.NodeCanLeft() == true);
            Assert.True(cnst.NodeCanRight() == true);
            Assert.True(cnst.NodeCanMoveUp() == false);
            Assert.True(cnst.NodeCanMoveDown() == false);
            Assert.True(cnst.NodeCanAddNew() == true);
            Assert.True(cnst.NodeCanAddNewSubNode() == false);
        }
        #endregion ITreeConfigNode
    }
}
