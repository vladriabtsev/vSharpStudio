using System;
using vSharpStudio.vm.ViewModels;
using Xunit;

namespace vSharpStudio.xUnit
{
    public class EditorVmTests
    {
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
        [Fact]
        public void Config003ValidationIsDbFromConnectionStringInfoConnectionStringName()
        {
            var cfg = new Config();
            cfg.IsDbFromConnectionString = true;
            cfg.Validate();
            Assert.False(cfg.HasErrors);
            Assert.True(cfg.HasWarnings);
            Assert.False(cfg.HasInfos);
            Assert.True(cfg.ValidationCollection.Count == 1);
            Assert.True(cfg.ValidationCollection[0].SortingValue >= 1 << ViewModelBase.ValidationMessage.MultiplierShift);
        }
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
        #region Document
        [Fact]
        public void Document001GuidInit()
        {
            var cfg = new Document();
            Assert.True(cfg.Guid.Length > 0);
        }
        #endregion Document
        #region Diff
        [Fact]
        public void DiffConstant001Added()
        {
            var cfg = new Config();
            Assert.True(cfg.Guid.Length > 0);
        }
        [Fact]
        public void DiffConfig001CanDiffwithDb()
        {
            Assert.True(false);
        }
        #endregion Diff
    }
}
