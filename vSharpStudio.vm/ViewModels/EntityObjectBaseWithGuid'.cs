using System;
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
        public override void Restore(T from) { throw new NotImplementedException("Please override Restore method"); }
        public override T Backup() { throw new NotImplementedException("Please override Backup method"); }
    }
}
