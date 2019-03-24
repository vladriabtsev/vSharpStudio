using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ViewModelBase;
using vSharpStudio.vm.ViewModels;
using Xunit;

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
        protected override TestValidatable Backup()
        {
            var tmp = new TestValidatable();
            tmp.TestPublicPropery = this.TestPublicPropery;
            return tmp;
        }
        protected override void Restore(TestValidatable from)
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
        [Fact]
        public void Editable002CanCancelEditPrivateField()
        {
            TestEditable vm = new TestEditable();
            vm.SetPrivateField("1");
            vm.BeginEdit();
            vm.SetPrivateField("2");
            vm.CancelEdit();
            Assert.Equal("1", vm.GetPrivateField());
        }
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
    }
}
