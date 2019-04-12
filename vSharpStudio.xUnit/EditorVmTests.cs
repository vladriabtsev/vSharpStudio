using System;
using vSharpStudio.vm.ViewModels;
using Xunit;
using ViewModelBase;

namespace vSharpStudio.xUnit
{
    public class EditorVmTests
    {
        public EditorVmTests()
        {
            ViewModelBindable.isUnitTests = true;
        }
        #region Config
        [Fact]
        public void Config001GuidInit()
        {
            //Proto.Config.proto_config dto = new Proto.Config.proto_config();
            //Config cfg = new Config(dto);
            //Constant c = Constant.Create();
            //cfg.Constants.ListConstants.Add(c);
            var cfg = new Config();
            Assert.True(cfg.Guid.Length > 0);
        }
        [Fact]
        public void Config002CanSaveAndRestore()
        {
            var cfg = new Config();
            cfg.Constants.ListConstants.Add(new Constant());
            string json = cfg.ExportToJson();
            Assert.True(json.Length > 0);
            var cfg2 = new Config(json);
            Assert.True(cfg2.Constants.ListConstants.Count == 1);
            Assert.True(cfg2.Constants.ListConstants[0].Name == typeof(Constant).Name + 1);
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
            cfg.Constants.ListConstants.Add(cnst);
            Assert.Equal("Constant1", cnst.Name);
            var cnst2 = new Constant();
            cfg.Constants.ListConstants.Add(cnst2);
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

        #region SortingByNameAndValue
        [Fact]
        public void SortingByNameAndValue001_UpdateSortingValueWhenNameIsChanged()
        {
            var cfg = new Config();
            var cnst = new Constant(cfg.Constants);
            cfg.Constants.ListConstants.Add(cnst);
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
        public void SortingByNameAndValue002_RestoreSortingValueWhenObjectRestoredFromFile()
        {
            var cfg = new Config();
            var cnst = new Constant(cfg.Constants);
            cfg.Constants.ListConstants.Add(cnst);
            cnst.Name = "abc1";
            var curr = cnst.SortingValue;

            string json = cfg.ExportToJson();
            var cfg2 = new Config(json);

            Assert.True(cfg2.Constants.ListConstants[0].Name == cnst.Name);
            Assert.True(cfg2.Constants.ListConstants[0].SortingValue == cnst.SortingValue);
        }
        [Fact]
        public void SortingByNameAndValue003_ReSortedWhenSortingValueIsChanged()
        {
            var cfg = new Config();
            var cnst = new Constant(cfg.Constants);
            cfg.Constants.ListConstants.Add(cnst);
            cnst.Name = "abc1";

            var cnst2 = new Constant(cfg.Constants);
            cfg.Constants.ListConstants.Add(cnst2);
            cnst2.Name = "abc1";

            Assert.True(cnst.Guid != cnst2.Guid);
            Assert.True(cfg.Constants.ListConstants[0].Guid == cnst.Guid);
            Assert.True(cfg.Constants.ListConstants[1].Guid == cnst2.Guid);

            cnst2.Name = "abc0";
            Assert.True(cfg.Constants.ListConstants[1].Guid == cnst.Guid);
            Assert.True(cfg.Constants.ListConstants[0].Guid == cnst2.Guid);
        }
        #endregion SortingByNameAndValue
    }
}
