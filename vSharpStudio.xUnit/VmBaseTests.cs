using System;
//using vSharpStudio.vm.ViewModels;
using Xunit;
using ViewModelBase;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluentValidation;
using vSharpStudio.common;

namespace vSharpStudio.xUnit
{
    public class VmBaseTests
    {
        public VmBaseTests()
        {
            ViewModelBindable.isUnitTests = true;
        }

        #region IEditable
        [Fact]
        public void Editable001CanCancelEditPublicProperty()
        {
            TestEditable vm = new TestEditable
            {
                TestPublicPropery = "1"
            };
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
            TestEditable vm = new TestEditable
            {
                TestPublicPropery = "1"
            };
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
        #endregion IEditable

        #region Validation
        [Fact]
        public void Validation001_ValidationCollectionEmptyAfterInit()
        {
            Config.ConfigValidator.Reset();
            var cfg = new Config();
            cfg.SolutionPath = @"..\..\..\..\";
            Assert.True(cfg.ValidationCollection != null);
            Assert.True(cfg.ValidationCollection.Count == 0);
        }
        [Fact]
        public void Validation002_CanSetErrror()
        {
            Config.ConfigValidator.Reset();
            var cfg = new Config();
            cfg.SolutionPath = @"..\..\..\..\";
            string mes = "test error message";
            Config.ConfigValidator.Validator.RuleFor(x => x).Null().WithMessage(mes).WithSeverity(Severity.Error);
            cfg.Validate();
            Assert.True(cfg.ValidationCollection.Count == 1);
            var p = cfg.ValidationCollection[0];
            Assert.True(p.Severity == FluentValidation.Severity.Error);
            Assert.True(p.Message == mes);
            Assert.True(p.Model == cfg);
        }
        [Fact]
        public void Validation003_CanSetWarning()
        {
            Config.ConfigValidator.Reset();
            var cfg = new Config();
            cfg.SolutionPath = @"..\..\..\..\";
            string mes = "test warning message";
            Config.ConfigValidator.Validator.RuleFor(x => x).Null().WithMessage(mes).WithSeverity(Severity.Warning);
            cfg.Validate();
            Assert.True(cfg.ValidationCollection.Count == 1);
            var p = cfg.ValidationCollection[0];
            Assert.True(p.Severity == FluentValidation.Severity.Warning);
            Assert.True(p.Message == mes);
            Assert.True(p.Model == cfg);
        }
        [Fact]
        public void Validation004_CanSetInfo()
        {
            Config.ConfigValidator.Reset();
            var cfg = new Config();
            cfg.SolutionPath = @"..\..\..\..\";
            string mes = "test info message";
            Config.ConfigValidator.Validator.RuleFor(x => x).Null().WithMessage(mes).WithSeverity(Severity.Info);
            cfg.Validate();
            Assert.True(cfg.ValidationCollection.Count == 1);
            var p = cfg.ValidationCollection[0];
            Assert.True(p.Severity == FluentValidation.Severity.Info);
            Assert.True(p.Message == mes);
            Assert.True(p.Model == cfg);
        }
        [Fact]
        public void Validation005_CanSortBySeverity()
        {
            Config.ConfigValidator.Reset();
            var cfg = new Config();
            cfg.SolutionPath = @"..\..\..\..\";
            string mes1 = "test error message";
            string mes2 = "test warning message";
            string mes3 = "test info message";

            Config.ConfigValidator.Validator.RuleFor(x => x).Null().WithMessage(mes2).WithSeverity(Severity.Warning);
            Config.ConfigValidator.Validator.RuleFor(x => x).Null().WithMessage(mes3).WithSeverity(Severity.Info);
            Config.ConfigValidator.Validator.RuleFor(x => x).Null().WithMessage(mes1).WithSeverity(Severity.Error);

            cfg.Validate();

            Assert.True(cfg.ValidationCollection.Count == 3);
            var p = cfg.ValidationCollection[0];
            Assert.True(p.Severity == FluentValidation.Severity.Error);
            Assert.True(p.Message == mes1);
            Assert.True(p.Model == cfg);
            p = cfg.ValidationCollection[1];
            Assert.True(p.Severity == FluentValidation.Severity.Warning);
            Assert.True(p.Message == mes2);
            Assert.True(p.Model == cfg);
            p = cfg.ValidationCollection[2];
            Assert.True(p.Severity == FluentValidation.Severity.Info);
            Assert.True(p.Message == mes3);
            Assert.True(p.Model == cfg);
        }
        [Fact]
        public void Validation006_CanSortByWeight()
        {
            Config.ConfigValidator.Reset();
            var cfg = new Config();
            cfg.SolutionPath = @"..\..\..\..\";
            string mes1 = "test error message";
            string mes2 = "test error message2";

            Config.ConfigValidator.Validator.RuleFor(x => x).Null().WithMessage(mes1).WithSeverity(Severity.Error).WithState(x => SeverityWeight.Normal);
            Config.ConfigValidator.Validator.RuleFor(x => x).Null().WithMessage(mes2).WithSeverity(Severity.Error).WithState(x => SeverityWeight.Low);

            cfg.Validate();

            Assert.Equal(2, cfg.ValidationCollection.Count);
            var p = cfg.ValidationCollection[0];
            Assert.Equal(FluentValidation.Severity.Error, p.Severity);
            Assert.True(p.Message == mes1);
            Assert.True(p.Model == cfg);
            p = cfg.ValidationCollection[1];
            Assert.True(p.Severity == FluentValidation.Severity.Error);
            Assert.True(p.Message == mes2);
            Assert.True(p.Model == cfg);
        }
        [Fact]
        public void Validation007_CanSortByWeight2()
        {
            Config.ConfigValidator.Reset();
            var cfg = new Config();
            cfg.SolutionPath = @"..\..\..\..\";
            string mes1 = "test error message";
            string mes2 = "test error message2";

            Config.ConfigValidator.Validator.RuleFor(x => x).Null().WithMessage(mes2).WithSeverity(Severity.Error).WithState(x => SeverityWeight.Low);
            Config.ConfigValidator.Validator.RuleFor(x => x).Null().WithMessage(mes1).WithSeverity(Severity.Error).WithState(x => SeverityWeight.Normal);

            cfg.Validate();

            Assert.True(cfg.ValidationCollection.Count == 2);
            var p = cfg.ValidationCollection[0];
            Assert.True(p.Severity == FluentValidation.Severity.Error);
            Assert.True(p.Message == mes1);
            Assert.True(p.Model == cfg);
            p = cfg.ValidationCollection[1];
            Assert.True(p.Severity == FluentValidation.Severity.Error);
            Assert.True(p.Message == mes2);
            Assert.True(p.Model == cfg);
        }
        #endregion Validation
    }
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
            var tmp = new TestEditable
            {
                TestPublicPropery = this.TestPublicPropery,
                TestPublicObservableCollectionPropery = new ObservableCollection<string>()
            };
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
            var tmp = new TestValidatable
            {
                TestPublicPropery = this.TestPublicPropery
            };
            return tmp;
        }
        public override void Restore(TestValidatable from)
        {
            this.TestPublicPropery = from.TestPublicPropery;
        }
    }

}
