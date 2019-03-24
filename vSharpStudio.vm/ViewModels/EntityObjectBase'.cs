using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public class EntityObjectBase<T, TValidator> : ViewModelValidatableWithSeverity<T, TValidator>
      where TValidator : AbstractValidator<T>
      where T : EntityObjectBase<T, TValidator>
    {
        public EntityObjectBase(TValidator validator, SortableObservableCollection<ValidationMessage> validationCollection )
            : base(validator, validationCollection)
        {
        }
    }
}
