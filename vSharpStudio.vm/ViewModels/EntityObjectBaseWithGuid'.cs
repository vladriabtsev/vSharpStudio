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
        public string Guid
        {
            get
            {
                if (_Guid == null)
                {
                    _Guid = System.Guid.NewGuid().ToString();
                    NotifyPropertyChanged(); // to recognize object was changed
                }
                return _Guid;
            }
            set
            {
                if (_Guid != null)
                    throw new InvalidOperationException("Guid already assigned");
                _Guid = value;
                NotifyPropertyChanged();
            }
        }
        private string _Guid = null;
        public override int CompareToById(T other) { return this.Guid.CompareTo(other.Guid); }
        public override void Restore(T from) { throw new NotImplementedException("Please override Restore method"); }
        public override T Backup() { throw new NotImplementedException("Please override Backup method"); }
    }
}
