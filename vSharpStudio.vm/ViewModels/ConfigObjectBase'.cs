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
        public string Name
        {
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    NotifyPropertyChanged();
                }
            }
            get { return _Name; }
        }
        private string _Name = null;
        //public bool IsSelected
        //{
        //    set
        //    {
        //        if (_IsSelected != value)
        //        {
        //            _IsSelected = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //    get { return _IsSelected; }
        //}
        //private bool _IsSelected;
        //public bool IsExpanded
        //{
        //    set
        //    {
        //        if (_IsExpanded != value)
        //        {
        //            _IsExpanded = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //    get { return _IsExpanded; }
        //}
        //private bool _IsExpanded;
        public override int CompareToById(T other) { return base.CompareToById(other); }
        public override void Restore(T from) { throw new NotImplementedException("Please override Restore method"); }
        public override T Backup() { throw new NotImplementedException("Please override Backup method"); }
        protected override void OnCountErrorsChanged() { }
        protected override void OnCountWarningsChanged() { }
        protected override void OnCountInfosChanged() { }
    }
}
