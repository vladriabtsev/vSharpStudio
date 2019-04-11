using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public class EntityObjectBaseWithGuid<T, TValidator> : ViewModelValidatableWithSeverity<T, TValidator>
      where TValidator : AbstractValidator<T>
      where T : EntityObjectBaseWithGuid<T, TValidator>, IConfigObject
    {
        public EntityObjectBaseWithGuid(TValidator validator)
            : base(validator)
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
            protected set
            {
                _Guid = value;
                NotifyPropertyChanged();
            }
        }
        private string _Guid = null;
        public override int CompareToById(T other) { return this.Guid.CompareTo(other.Guid); }
        public override void Restore(T from) { throw new NotImplementedException("Please override Restore method"); }
        public override T Backup() { throw new NotImplementedException("Please override Backup method"); }
        protected override void OnCountErrorsChanged() { }
        protected override void OnCountWarningsChanged() { }
        protected override void OnCountInfosChanged() { }
    }
}
