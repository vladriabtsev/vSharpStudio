using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using ViewModelBase;
using vSharpStudio.ViewModels;
using vSharpStudio.vm.ViewModels;
using Xunit;
using Xunit.Abstractions;
using static Proto.Config.proto_data_type.Types;

namespace vSharpStudio.xUnit
{
    public class VmTests
    {
        ILogger _logger = null;
        public VmTests(ITestOutputHelper output)
        {
            ViewModelBindable.isUnitTests = true;
            ILoggerFactory loggerFactory = std.ApplicationLogging.LoggerFactory;
            loggerFactory.AddProvider(new DebugLoggerProvider());
            _logger = loggerFactory.CreateLogger<VmTests>();
            _logger.LogInformation("======================  Start VmTests tests ===============================");
        }

        #region Editable

        [Fact]
        public void Editable011CanCancelSameLevelSimpleProperty()
        {
            Catalog vm = new Catalog
            {
                Name = "test1",
            };
            vm.IdDbGenerator.HiLoSchema = "schema1";
            vm.BeginEdit();
            vm.Name = "test2";
            vm.IdDbGenerator.HiLoSchema = "schema2";
            vm.CancelEdit();
            Assert.True(vm.Name == "test1");
            Assert.True(vm.IdDbGenerator.HiLoSchema == "schema1");
        }
        [Fact]
        public void Editable012CanCancelSameLevelNullable()
        {
            Catalog vm = new Catalog
            {
            };
            vm.IdDbGenerator.IsPrimaryKeyClustered = true;
            vm.BeginEdit();
            vm.IdDbGenerator.IsPrimaryKeyClustered = false;
            vm.CancelEdit();
            Assert.True(vm.IdDbGenerator.IsPrimaryKeyClustered);
        }
        [Fact]
        public void Editable013CanCancelSecondLevelSimpleProperty()
        {
            Catalog vm = new Catalog();
            vm.Name = "test1";
            vm.BeginEdit();
            vm.Name = "test2";
            vm.CancelEdit();
            Assert.True(vm.Name == "test1");
        }
        [Fact]
        public void Editable014CanCancelSecondLevelCollection()
        {
            Catalog vm = new Catalog();
            var prop = new Property
            {
                Name = "test1"
            };
            vm.GroupProperties.ListProperties.Add(prop);
            vm.BeginEdit();
            vm.GroupProperties.ListProperties[0].Name = "test2";
            vm.CancelEdit();
            Assert.True(vm.GroupProperties.ListProperties[0].Name == "test1");
            vm.BeginEdit();
            prop = new Property() { Name = "test3" };
            vm.GroupProperties.ListProperties.Add(prop);
            Assert.True(vm.GroupProperties.ListProperties.Count == 2);
            vm.CancelEdit();
            Assert.True(vm.GroupProperties.ListProperties.Count == 1);
            Assert.True(vm.GroupProperties.ListProperties[0].Name == "test1");
        }
        [Fact]
        public void Editable021CanCancelCatalogPropertiy()
        {
            Catalog vm = new Catalog();
            vm.BeginEdit();
            vm.GroupProperties.ListProperties.Add(new Property("pdouble0", EnumDataType.Numerical, 10, 0));
            vm.CancelEdit();
            Assert.True(vm.GroupProperties.ListProperties.Count == 0);
            vm.GroupProperties.ListProperties.Add(new Property("pdouble0", EnumDataType.Numerical, 10, 0));
            vm.BeginEdit();
            vm.GroupProperties.ListProperties[0].DataType.DataTypeEnum = EnumDataType.String;
            vm.CancelEdit();
            Assert.Single(vm.GroupProperties.ListProperties);
            Assert.True(vm.GroupProperties.ListProperties[0].DataType.DataTypeEnum == EnumDataType.Numerical);
            vm.BeginEdit();
            vm.GroupProperties.ListProperties.Clear();
            vm.CancelEdit();
            Assert.Single(vm.GroupProperties.ListProperties);
            Assert.True(vm.GroupProperties.ListProperties[0].DataType.DataTypeEnum == EnumDataType.Numerical);
        }
        #endregion Editable

        #region Async
        // https://blog.stephencleary.com/2012/02/async-and-await.html
        // https://msdn.microsoft.com/en-us/magazine/jj991977.aspx
        [Fact]
        public void Async001_CanHandleException()
        {
            Assert.ThrowsAsync<ArgumentException>(() => AsyncWithOneException());
        }
        Task AsyncWithOneException()
        {
            Task task = Task.Run(() =>
             {
                 throw new ArgumentException("test 1");
             });
            return task;
        }
        //[Fact]
        //async public void Async002_TestAsync()
        //{
        //    await AsyncWithOneException();
        //}
        #endregion Async

        #region Validatable
        [Fact]
        public void Validation001_ValidationCollectionEmptyAfterInit()
        {
            Config.ConfigValidator.Reset();
            var cfg = new ConfigRoot();
            Assert.True(cfg.ValidationCollection != null);
            Assert.True(cfg.ValidationCollection.Count == 0);
        }
        [Fact]
        public void Validation002_ValidationCollectionContainsValidationMessagesFromSubNodesForSelectedNode()
        {
            Config.ConfigValidator.Reset();
            Catalog.CatalogValidator.Reset();
            var cfg = new ConfigRoot();
            var c = new Catalog() {  Name="test" };
            cfg.GroupCatalogs.ListCatalogs.Add(c);

            string mes1 = "test error message";
            string mes2 = "test warning message";
            string mes22 = "test warning2 message";
            string mes3 = "test info message";

            Catalog.CatalogValidator.Validator.RuleFor(x => x).Null().WithMessage(mes22).WithSeverity(Severity.Warning).WithState(x => SeverityWeight.VeryHigh);
            Catalog.CatalogValidator.Validator.RuleFor(x => x).Null().WithMessage(mes1).WithSeverity(Severity.Error).WithState(x => SeverityWeight.VeryLow);
            Catalog.CatalogValidator.Validator.RuleFor(x => x).Null().WithMessage(mes3).WithSeverity(Severity.Info).WithState(x => SeverityWeight.VeryHigh);
            Catalog.CatalogValidator.Validator.RuleFor(x => x).Null().WithMessage(mes2).WithSeverity(Severity.Warning).WithState(x => SeverityWeight.VeryLow);

            cfg.Validate();

            cfg.ValidateSubTreeFromNode(c, _logger); //.ConfigureAwait(continueOnCapturedContext: false);

            Assert.True(cfg.ValidationCollection.Count == 0);
            Assert.True(c.ValidationCollection.Count == 4);
            var p = c.ValidationCollection[0];
            Assert.True(p.Severity == FluentValidation.Severity.Error);
            Assert.True(p.Message == mes1);
            Assert.True(p.Model == c);
            p = c.ValidationCollection[1];
            Assert.True(p.Severity == FluentValidation.Severity.Warning);
            Assert.True(p.Message == mes22);
            Assert.True(p.Model == c);
            p = c.ValidationCollection[2];
            Assert.True(p.Severity == FluentValidation.Severity.Warning);
            Assert.True(p.Message == mes2);
            Assert.True(p.Model == c);
            p = c.ValidationCollection[3];
            Assert.True(p.Severity == FluentValidation.Severity.Info);
            Assert.True(p.Message == mes3);
            Assert.True(p.Model == c);

            Assert.Equal(1, c.CountErrors);
            Assert.Equal(2, c.CountWarnings);
            Assert.Equal(1, c.CountInfos);

            Assert.Equal(1, cfg.GroupCatalogs.CountErrors);
            Assert.Equal(2, cfg.GroupCatalogs.CountWarnings);
            Assert.Equal(1, cfg.GroupCatalogs.CountInfos);

            Assert.Equal(1, cfg.CountErrors);
            Assert.Equal(2, cfg.CountWarnings);
            Assert.Equal(1, cfg.CountInfos);

            cfg.ValidateSubTreeFromNode(cfg, _logger); //.ConfigureAwait(continueOnCapturedContext: false);
            Assert.True(cfg.ValidationCollection.Count == 4);
        }
        #endregion Validatable

    }
}
