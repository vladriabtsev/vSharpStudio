using System;
using vSharpStudio.vm.ViewModels;
using Xunit;
using ViewModelBase;

namespace vSharpStudio.xUnit
{
    public class VmBaseTests
    {
        #region Validation
        [Fact]
        public void Validation001_ValidationCollectionEmptyAfterInit()
        {
            var cfg = new Config(new SortableObservableCollection<ValidationMessage>());
            Assert.True(cfg.ValidationCollection != null);
            Assert.True(cfg.ValidationCollection.Count == 0);
        }
        [Fact]
        public void Validation002_CanSetErrror()
        {
            var cfg = new Config(new SortableObservableCollection<ValidationMessage>());
            string mes = "test error message";
            cfg.SetError(mes);
            Assert.True(cfg.ValidationCollection.Count == 1);
            var p = cfg.ValidationCollection[0];
            Assert.True(p.Severity == FluentValidation.Severity.Error);
            Assert.True(p.Message == mes);
            Assert.True(p.Model == cfg);
        }
        [Fact]
        public void Validation003_CanSetWarning()
        {
            var cfg = new Config(new SortableObservableCollection<ValidationMessage>());
            string mes = "test warning message";
            cfg.SetWarning(mes);
            Assert.True(cfg.ValidationCollection.Count == 1);
            var p = cfg.ValidationCollection[0];
            Assert.True(p.Severity == FluentValidation.Severity.Warning);
            Assert.True(p.Message == mes);
            Assert.True(p.Model == cfg);
        }
        [Fact]
        public void Validation004_CanSetInfo()
        {
            var cfg = new Config(new SortableObservableCollection<ValidationMessage>());
            string mes = "test info message";
            cfg.SetWarning(mes);
            Assert.True(cfg.ValidationCollection.Count == 1);
            var p = cfg.ValidationCollection[0];
            Assert.True(p.Severity == FluentValidation.Severity.Info);
            Assert.True(p.Message == mes);
            Assert.True(p.Model == cfg);
        }
        [Fact]
        public void Validation005_CanSortBySeverity()
        {
            var cfg = new Config(new SortableObservableCollection<ValidationMessage>());
            string mes1 = "test error message";
            string mes2 = "test warning message";
            string mes3 = "test info message";

            cfg.SetInfo(mes3);
            cfg.SetWarning(mes2);
            cfg.SetError(mes1);
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

            cfg.SetWarning(mes2);
            cfg.SetError(mes1);
            cfg.SetInfo(mes3);
            Assert.True(cfg.ValidationCollection.Count == 3);
            p = cfg.ValidationCollection[0];
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

            cfg.SetInfo(mes3);
            cfg.SetError(mes1);
            cfg.SetWarning(mes2);
            Assert.True(cfg.ValidationCollection.Count == 3);
            p = cfg.ValidationCollection[0];
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
        #endregion Validation
    }
}
