using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ViewModelBase;
using vSharpStudio.vm.ViewModels;
using Xunit;
using static Proto.Config.proto_data_type.Types;

namespace vSharpStudio.xUnit
{
    public class TestEditable : ViewModelEditable<TestEditable>
    {
        public TestEditable()
        {
            TestPublicListPropery = new List<string>();
            TestPublicObservableCollectionPropery = new ObservableCollection<string>();
        }
        public string TestPublicPropery { get; set; }
        private string testPrivateField;
        public void SetPrivateField(string s)
        {
            testPrivateField = s;
        }
        public string GetPrivateField()
        {
            return testPrivateField;
        }
        public List<string> TestPublicListPropery { get; set; }
        public ObservableCollection<string> TestPublicObservableCollectionPropery { get; set; }
        public override TestEditable Backup()
        {
            var tmp = new TestEditable();
            tmp.TestPublicPropery = this.TestPublicPropery;
            tmp.TestPublicObservableCollectionPropery = new ObservableCollection<string>();
            foreach (var t in this.TestPublicObservableCollectionPropery)
            {
                tmp.TestPublicObservableCollectionPropery.Add(t);
            }
            tmp.TestPublicListPropery = new List<string>();
            foreach (var t in this.TestPublicListPropery)
            {
                tmp.TestPublicListPropery.Add(t);
            }
            return tmp;
        }
        public override void Restore(TestEditable from)
        {
            this.TestPublicPropery = from.TestPublicPropery;
            this.TestPublicObservableCollectionPropery = from.TestPublicObservableCollectionPropery;
            this.TestPublicListPropery = from.TestPublicListPropery;
        }
    }
    public class TestValidator : ValidatorBase<TestValidatable, TestValidator>
    {

    }
    public class TestValidatable : ViewModelValidatable<TestValidatable, TestValidator>
    {
        public TestValidatable() : base(TestValidator.Validator)
        {
            TestPublicListPropery = new List<string>();
            TestPublicObservableCollectionPropery = new ObservableCollection<string>();
        }
        public string TestPublicPropery { get; set; }
        private string testPrivateField;
        public void SetPrivateField(string s)
        {
            testPrivateField = s;
        }
        public string GetPrivateField()
        {
            return testPrivateField;
        }
        public List<string> TestPublicListPropery { get; set; }
        public ObservableCollection<string> TestPublicObservableCollectionPropery { get; set; }
        public override TestValidatable Backup()
        {
            var tmp = new TestValidatable();
            tmp.TestPublicPropery = this.TestPublicPropery;
            return tmp;
        }
        public override void Restore(TestValidatable from)
        {
            this.TestPublicPropery = from.TestPublicPropery;
        }
    }

    public class VmTests
    {
        [Fact]
        public void Editable001CanCancelEditPublicProperty()
        {
            TestEditable vm = new TestEditable();
            vm.TestPublicPropery = "1";
            vm.BeginEdit();
            vm.TestPublicPropery = "2";
            vm.CancelEdit();
            Assert.Equal("1", vm.TestPublicPropery);
        }
        //[Fact]
        //public void Editable002CanCancelEditPrivateField()
        //{
        //    TestEditable vm = new TestEditable();
        //    vm.SetPrivateField("1");
        //    vm.BeginEdit();
        //    vm.SetPrivateField("2");
        //    vm.CancelEdit();
        //    Assert.Equal("1", vm.GetPrivateField());
        //}
        [Fact]
        public void Editable003CanCancelEditPublicListProperty()
        {
            TestEditable vm = new TestEditable();
            vm.TestPublicListPropery.Add("1");
            vm.BeginEdit();
            vm.TestPublicListPropery.Add("2");
            vm.CancelEdit();
            Assert.Single(vm.TestPublicListPropery);
            Assert.Equal("1", vm.TestPublicListPropery[0]);
        }
        [Fact]
        public void Editable004CanCancelEditPublicObservableProperty()
        {
            TestEditable vm = new TestEditable();
            vm.TestPublicObservableCollectionPropery.Add("1");
            vm.BeginEdit();
            vm.TestPublicObservableCollectionPropery.Add("2");
            vm.CancelEdit();
            Assert.Single(vm.TestPublicObservableCollectionPropery);
            Assert.Equal("1", vm.TestPublicObservableCollectionPropery[0]);
        }
        [Fact]
        public void Editable005CanEndEditPublicProperty()
        {
            TestEditable vm = new TestEditable();
            vm.TestPublicPropery = "1";
            vm.BeginEdit();
            vm.TestPublicPropery = "2";
            vm.EndEdit();
            Assert.Equal("2", vm.TestPublicPropery);
        }
        [Fact]
        public void Editable006CanEndEditPrivateField()
        {
            TestEditable vm = new TestEditable();
            vm.SetPrivateField("1");
            vm.BeginEdit();
            vm.SetPrivateField("2");
            vm.EndEdit();
            Assert.Equal("2", vm.GetPrivateField());
        }
        [Fact]
        public void Editable007CanEndEditPublicListProperty()
        {
            TestEditable vm = new TestEditable();
            vm.TestPublicListPropery.Add("1");
            vm.BeginEdit();
            vm.TestPublicListPropery.Add("2");
            vm.EndEdit();
            Assert.Equal(2, vm.TestPublicListPropery.Count);
            Assert.Equal("1", vm.TestPublicListPropery[0]);
            Assert.Equal("2", vm.TestPublicListPropery[1]);
        }
        [Fact]
        public void Editable008CanEndEditPublicObservableProperty()
        {
            TestEditable vm = new TestEditable();
            vm.TestPublicObservableCollectionPropery.Add("1");
            vm.BeginEdit();
            vm.TestPublicObservableCollectionPropery.Add("2");
            vm.EndEdit();
            Assert.Equal(2, vm.TestPublicObservableCollectionPropery.Count);
            Assert.Equal("1", vm.TestPublicObservableCollectionPropery[0]);
            Assert.Equal("2", vm.TestPublicObservableCollectionPropery[1]);
        }
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
            vm.Properties.ListProperties.Add(new Property("pdouble0", EnumDataType.Numerical, 10, 0));
            vm.BeginEdit();
            vm.Properties.ListProperties[0].DataType.DataTypeEnum = EnumDataType.String;
            vm.CancelEdit();
            Assert.Single(vm.Properties.ListProperties);
            Assert.True(vm.Properties.ListProperties[0].DataType.DataTypeEnum == EnumDataType.Numerical);
        }
    }
}
