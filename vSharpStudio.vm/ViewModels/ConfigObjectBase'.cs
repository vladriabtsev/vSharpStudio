using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public class ConfigObjectBase<T, TValidator> : ViewModelValidatableWithSeverity<T, TValidator>
      where TValidator : AbstractValidator<T>
      where T : ConfigObjectBase<T, TValidator>
    {
        public ConfigObjectBase(TValidator validator)
            : base(validator)
        {
        }
        public override int CompareToById(T other) { return base.CompareToById(other); }
        public override void Restore(T from) { throw new NotImplementedException("Please override Restore method"); }
        public override T Backup() { throw new NotImplementedException("Please override Backup method"); }
        protected override void OnCountErrorsChanged() { }
        protected override void OnCountWarningsChanged() { }
        protected override void OnCountInfosChanged() { }
    }
}
