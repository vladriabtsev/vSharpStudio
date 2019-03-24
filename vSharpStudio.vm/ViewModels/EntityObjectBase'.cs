﻿using System;
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
        public EntityObjectBase(TValidator validator, SortedObservableCollection<ValidationMessage> validationCollection )
            : base(validator, validationCollection)
        {
        }
        public override int CompareToById(T other)
        {
            throw new Exception();
        }
    }
}