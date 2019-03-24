﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public class EntityObjectBaseWithGuid<T, TValidator> : ViewModelValidatableWithSeverity<T, TValidator>, IGuid
      where TValidator : AbstractValidator<T>
      where T : EntityObjectBaseWithGuid<T, TValidator>
    {
        public EntityObjectBaseWithGuid(TValidator validator, SortedObservableCollection<ValidationMessage> validationCollection)
            : base(validator, validationCollection)
        {
        }
        public string Guid { get; protected set; }
        public override int CompareToById(T other) { return this.Guid.CompareTo(other.Guid); }
        protected override T Backup() { return base.Backup(); }
        protected override void Restore(T from) { base.Restore(from); }
    }
}
