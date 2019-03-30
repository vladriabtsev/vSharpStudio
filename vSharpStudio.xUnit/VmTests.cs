using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ViewModelBase;
using vSharpStudio.ViewModels;
using vSharpStudio.vm.ViewModels;
using Xunit;
using static Proto.Config.proto_data_type.Types;

namespace vSharpStudio.xUnit
{
    public class VmTests
    {
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
            var cfg = new ConfigRoot();
            Assert.True(cfg.ValidationCollection != null);
            Assert.True(cfg.ValidationCollection.Count == 0);
        }
        [Fact]
        public void Validation002_ValidationCollectionContainsValidationMessagesFromSubNodesForSelectedNode()
        {
            var cfg = new ConfigRoot();
            var c = new Catalog();
            cfg.Catalogs.ListCatalogs.Add(c);

            string mes1 = "test error message";
            string mes2 = "test warning message";
            string mes3 = "test info message";

            c.SetInfo(mes3);
            c.SetWarning(mes2);
            c.SetError(mes1);

            cfg.ValidateSubTreeFromNode(c); //.ConfigureAwait(continueOnCapturedContext: false);

            Assert.True(cfg.ValidationCollection.Count == 3);
            var p = cfg.ValidationCollection[0];
            Assert.True(p.Severity == FluentValidation.Severity.Error);
            Assert.True(p.Message == mes1);
            Assert.True(p.Model == c);
            p = cfg.ValidationCollection[1];
            Assert.True(p.Severity == FluentValidation.Severity.Warning);
            Assert.True(p.Message == mes2);
            Assert.True(p.Model == c);
            p = cfg.ValidationCollection[2];
            Assert.True(p.Severity == FluentValidation.Severity.Info);
            Assert.True(p.Message == mes3);
            Assert.True(p.Model == c);

            var c2 = new Catalog();
            cfg.Catalogs.ListCatalogs.Add(c2);
            c2.SetWarning(mes2, 10);

            cfg.ValidateSubTreeFromNode(c);

            Assert.True(cfg.ValidationCollection.Count == 3);
            p = cfg.ValidationCollection[0];
            Assert.True(p.Severity == FluentValidation.Severity.Error);
            Assert.True(p.Message == mes1);
            Assert.True(p.Model == c);
            p = cfg.ValidationCollection[1];
            Assert.True(p.Severity == FluentValidation.Severity.Warning);
            Assert.True(p.Message == mes2);
            Assert.True(p.Model == c);
            p = cfg.ValidationCollection[2];
            Assert.True(p.Severity == FluentValidation.Severity.Info);
            Assert.True(p.Message == mes3);
            Assert.True(p.Model == c);

            cfg.ValidateSubTreeFromNode(c2);

            Assert.True(cfg.ValidationCollection.Count == 1);
            p = cfg.ValidationCollection[0];
            Assert.True(p.Severity == FluentValidation.Severity.Warning);
            Assert.True(p.Message == mes2);
            Assert.True(p.Model == c2);

            cfg.ValidateSubTreeFromNode(cfg);

            Assert.True(cfg.ValidationCollection.Count == 4);
            p = cfg.ValidationCollection[0];
            Assert.True(p.Severity == FluentValidation.Severity.Error);
            Assert.True(p.Message == mes1);
            Assert.True(p.Model == c);
            p = cfg.ValidationCollection[1];
            Assert.True(p.Severity == FluentValidation.Severity.Warning);
            Assert.True(p.Message == mes2);
            Assert.True(p.Model == c2);
            p = cfg.ValidationCollection[2];
            Assert.True(p.Severity == FluentValidation.Severity.Warning);
            Assert.True(p.Message == mes2);
            Assert.True(p.Model == c);
            p = cfg.ValidationCollection[3];
            Assert.True(p.Severity == FluentValidation.Severity.Info);
            Assert.True(p.Message == mes3);
            Assert.True(p.Model == c);

        }
        [Fact]
        public void Validation003_TreeNodesContainQtyAndLevelForValidationMessages()
        {
            //var cfg = new RootConfig(new SortedObservableCollection<ValidationMessage>());
            //Assert.True(cfg.ValidationCollection != null);
            //Assert.True(cfg.ValidationCollection.Count == 0);
            Assert.True(false);
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
