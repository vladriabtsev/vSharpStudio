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
            ILoggerFactory loggerFactory = std.ApplicationLogging.LoggerFactory;
            loggerFactory.AddProvider(new DebugLoggerProvider());
            _logger = loggerFactory.CreateLogger<VmTests>();
            _logger.LogInformation("======================  Start VmTests tests ===============================");
        }

        #region Editable

        [Fact]
        public void Editable011CanCancelSameLevelSimpleProperty()
        {
            Catalog vm = new Catalog();
            vm.Name = "test1";
            vm.HiLoSchema = "schema1";
            vm.BeginEdit();
            vm.Name = "test2";
            vm.HiLoSchema = "schema2";
            vm.CancelEdit();
            Assert.True(vm.Name == "test1");
            Assert.True(vm.HiLoSchema == "schema1");
        }
        [Fact]
        public void Editable012CanCancelSameLevelNullable()
        {
            Catalog vm = new Catalog();
            vm.IsPrimaryKeyClustered = true;
            vm.BeginEdit();
            vm.IsPrimaryKeyClustered = false;
            vm.CancelEdit();
            Assert.True(vm.IsPrimaryKeyClustered);
        }
        [Fact]
        public void Editable013CanCancelSecondLevelSimpleProperty()
        {
            Catalog vm = new Catalog();
            vm.Properties.Name = "test1";
            vm.BeginEdit();
            vm.Properties.Name = "test2";
            vm.CancelEdit();
            Assert.True(vm.Properties.Name == "test1");
        }
        [Fact]
        public void Editable014CanCancelSecondLevelCollection()
        {
            Catalog vm = new Catalog();
            var prop = new Property();
            prop.Name = "test1";
            vm.Properties.ListProperties.Add(prop);
            vm.BeginEdit();
            vm.Properties.ListProperties[0].Name = "test2";
            vm.CancelEdit();
            Assert.True(vm.Properties.ListProperties[0].Name == "test1");
            vm.BeginEdit();
            prop = new Property() { Name = "test3" };
            vm.Properties.ListProperties.Add(prop);
            Assert.True(vm.Properties.ListProperties.Count == 2);
            vm.CancelEdit();
            Assert.True(vm.Properties.ListProperties.Count == 1);
            Assert.True(vm.Properties.ListProperties[0].Name == "test1");
        }
        [Fact]
        public void Editable021CanCancelCatalogPropertiy()
        {
            Catalog vm = new Catalog();
            vm.BeginEdit();
            vm.Properties.ListProperties.Add(new Property("pdouble0", EnumDataType.Numerical, 10, 0));
            vm.CancelEdit();
            Assert.True(vm.Properties.ListProperties.Count == 0);
            vm.Properties.ListProperties.Add(new Property("pdouble0", EnumDataType.Numerical, 10, 0));
            vm.BeginEdit();
            vm.Properties.ListProperties[0].DataType.DataTypeEnum = EnumDataType.String;
            vm.CancelEdit();
            Assert.Single(vm.Properties.ListProperties);
            Assert.True(vm.Properties.ListProperties[0].DataType.DataTypeEnum == EnumDataType.Numerical);
            vm.BeginEdit();
            vm.Properties.ListProperties.Clear();
            vm.CancelEdit();
            Assert.Single(vm.Properties.ListProperties);
            Assert.True(vm.Properties.ListProperties[0].DataType.DataTypeEnum == EnumDataType.Numerical);
        }
        #endregion Editable

        #region Async
        [Fact]
        public void Async001_CanHandleException()
        {
            var t = AsyncWithOneException();
            t.Wait();
            if (t.IsFaulted)
            {

            }
        }
        Task AsyncWithOneException()
        {
            Task task = Task.Run(() =>
             {
                 throw new Exception("test 1");
             });
            return task;
        }
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
            var c = new Catalog();
            cfg.Catalogs.ListCatalogs.Add(c);

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

            Assert.True(cfg.ListAllValidationMessages.Count == 4);
            var p = cfg.ListAllValidationMessages[0];
            Assert.True(p.Severity == FluentValidation.Severity.Error);
            Assert.True(p.Message == mes1);
            Assert.True(p.Model == c);
            p = cfg.ListAllValidationMessages[1];
            Assert.True(p.Severity == FluentValidation.Severity.Warning);
            Assert.True(p.Message == mes22);
            Assert.True(p.Model == c);
            p = cfg.ListAllValidationMessages[2];
            Assert.True(p.Severity == FluentValidation.Severity.Warning);
            Assert.True(p.Message == mes2);
            Assert.True(p.Model == c);
            p = cfg.ListAllValidationMessages[3];
            Assert.True(p.Severity == FluentValidation.Severity.Info);
            Assert.True(p.Message == mes3);
            Assert.True(p.Model == c);

            Assert.Equal(1, c.CountErrors);
            Assert.Equal(2, c.CountWarnings);
            Assert.Equal(1, c.CountInfos);

            Assert.Equal(1, cfg.Catalogs.CountErrors);
            Assert.Equal(2, cfg.Catalogs.CountWarnings);
            Assert.Equal(1, cfg.Catalogs.CountInfos);

            Assert.Equal(1, cfg.CountErrors);
            Assert.Equal(2, cfg.CountWarnings);
            Assert.Equal(1, cfg.CountInfos);
        }
        [Fact]
        public void Validation004_EditingDataChangeListValidationMessagesInTheParentNodes()
        {
            //var cfg = new RootConfig(new SortedObservableCollection<ValidationMessage>());
            //Assert.True(cfg.ValidationCollection != null);
            //Assert.True(cfg.ValidationCollection.Count == 0);
            Assert.True(false);
        }
        [Fact]
        public void Validation005_SecondValidationOnEntityLevelIsRemovingLegacyMessages()
        {
            //var cfg = new RootConfig(new SortedObservableCollection<ValidationMessage>());
            //Assert.True(cfg.ValidationCollection != null);
            //Assert.True(cfg.ValidationCollection.Count == 0);
            Assert.True(false);
        }
        #endregion Validatable
    }
}
