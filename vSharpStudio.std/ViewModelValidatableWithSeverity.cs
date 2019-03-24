using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace ViewModelBase
{
    public class ViewModelValidatableWithSeverity<T, TValidator> : ViewModelEditable<T>, INotifyDataErrorInfo, IValidatable
      where TValidator : AbstractValidator<T>
      where T : ViewModelValidatableWithSeverity<T, TValidator>
    {
        public ViewModelValidatableWithSeverity(TValidator validator, SortableObservableCollection<ValidationMessage> validationCollection = null)
        {
            this._validator = validator;
            this.ValidationCollection = validationCollection;
        }
        protected readonly IValidator _validator;
        public SortableObservableCollection<ValidationMessage> ValidationCollection { get; private set; }
        protected bool ValidationChange(FluentValidation.Results.ValidationResult res)
        {
            ClearAllErrors();
            if (!res.IsValid)
            {
                foreach (var t in res.Errors)
                {
                    switch (t.Severity)
                    {
                        case Severity.Error:
                            if (!_errors.ContainsKey(t.PropertyName))
                                _errors[t.PropertyName] = new List<string>();
                            _errors[t.PropertyName].Add(t.ErrorMessage);
                            break;
                        case Severity.Warning:
                            if (!_warnings.ContainsKey(t.PropertyName))
                                _warnings[t.PropertyName] = new List<string>();
                            _warnings[t.PropertyName].Add(t.ErrorMessage);
                            break;
                        case Severity.Info:
                            if (!_infos.ContainsKey(t.PropertyName))
                                _infos[t.PropertyName] = new List<string>();
                            _infos[t.PropertyName].Add(t.ErrorMessage);
                            break;
                    }
                }
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (var t in _errors)
                    dic[t.Key] = null;
                foreach (var t in _warnings)
                    dic[t.Key] = null;
                foreach (var t in _infos)
                    dic[t.Key] = null;
                var properties = dic.Select(error => error.Key).ToList();
                foreach (var propertyName in properties)
                    RaiseErrorsChanged(propertyName);
            }
            return res.IsValid;
        }
        public bool Validate()
        {
            var res = this._validator.Validate(this);
            var isValid = ValidationChange(res);
            NotifyPropertyChanged(m => m.HasErrors);
            NotifyPropertyChanged(m => m.HasWarnings);
            NotifyPropertyChanged(m => m.HasInfos);
            return isValid;
        }
        public async void ValidateAsync()
        {
            var res = await this._validator.ValidateAsync(this);
            ValidationChange(res);
            NotifyPropertyChanged(m => m.HasErrors);
            NotifyPropertyChanged(m => m.HasWarnings);
            NotifyPropertyChanged(m => m.HasInfos);
        }
        protected void ValidateProperty([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            OnValidateProperty(propertyName);
        }
        protected async override void OnValidateProperty(string propertyName)
        {
            var res = await this._validator.ValidateAsync(this);
            if (!res.IsValid)
            {
                bool found = false;
                foreach (var t in res.Errors)
                {
                    if (t.PropertyName != propertyName)
                        continue;
                    found = true;
                    switch (t.Severity)
                    {
                        case Severity.Error:
                            if (!_errors.ContainsKey(t.PropertyName))
                                _errors[t.PropertyName] = new List<string>();
                            _errors[t.PropertyName].Add(t.ErrorMessage);
                            break;
                        case Severity.Warning:
                            if (!_warnings.ContainsKey(t.PropertyName))
                                _warnings[t.PropertyName] = new List<string>();
                            _warnings[t.PropertyName].Add(t.ErrorMessage);
                            break;
                        case Severity.Info:
                            if (!_infos.ContainsKey(t.PropertyName))
                                _infos[t.PropertyName] = new List<string>();
                            _infos[t.PropertyName].Add(t.ErrorMessage);
                            break;
                    }
                }
                if (found)
                {
                    RaiseErrorsChanged(propertyName);
                }
                else if (_errors.ContainsKey(propertyName))
                {
                    _errors.Remove(propertyName);
                    RaiseErrorsChanged(propertyName);
                }
                else if (_warnings.ContainsKey(propertyName))
                {
                    _warnings.Remove(propertyName);
                    RaiseErrorsChanged(propertyName);
                }
                else if (_infos.ContainsKey(propertyName))
                {
                    _infos.Remove(propertyName);
                    RaiseErrorsChanged(propertyName);
                }
            }
        }

        #region INotifyDataErrorInfo methods and helpers
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, List<string>> _warnings = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, List<string>> _infos = new Dictionary<string, List<string>>();
        public void SetError(string errorMessage, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors.Add(propertyName, new List<string> { errorMessage });
            RaiseErrorsChanged(propertyName);
            NotifyPropertyChanged(m => m.HasErrors);
        }
        public void SetWarning(string warningMessage, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (!_warnings.ContainsKey(propertyName))
                _warnings.Add(propertyName, new List<string> { warningMessage });
            RaiseErrorsChanged(propertyName);
            NotifyPropertyChanged(m => m.HasWarnings);
        }
        public void SetInfo(string infoMessage, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (!_infos.ContainsKey(propertyName))
                _infos.Add(propertyName, new List<string> { infoMessage });
            RaiseErrorsChanged(propertyName);
            NotifyPropertyChanged(m => m.HasInfos);
        }
        public void SetOneError(string errorMessage, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            ClearError(propertyName);
            SetError(errorMessage, propertyName);
        }
        protected void ClearError(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
                _errors.Remove(propertyName);
            if (_warnings.ContainsKey(propertyName))
                _warnings.Remove(propertyName);
            if (_infos.ContainsKey(propertyName))
                _infos.Remove(propertyName);
            RaiseErrorsChanged(propertyName);

            NotifyPropertyChanged(m => m.HasErrors);
            NotifyPropertyChanged(m => m.HasWarnings);
            NotifyPropertyChanged(m => m.HasInfos);
        }
        protected void ClearErrors([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (_errors.ContainsKey(propertyName))
                _errors.Remove(propertyName);
            if (_warnings.ContainsKey(propertyName))
                _warnings.Remove(propertyName);
            if (_infos.ContainsKey(propertyName))
                _infos.Remove(propertyName);
            RaiseErrorsChanged(propertyName);

            NotifyPropertyChanged(m => m.HasErrors);
            NotifyPropertyChanged(m => m.HasWarnings);
            NotifyPropertyChanged(m => m.HasInfos);
        }
        protected void ClearAllErrors()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var t in _errors)
                dic[t.Key] = null;
            foreach (var t in _warnings)
                dic[t.Key] = null;
            foreach (var t in _infos)
                dic[t.Key] = null;
            var properties = dic.Select(error => error.Key).ToList();
            foreach (var propertyName in properties)
                ClearError(propertyName);
        }
        public void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return null;
            return _errors.ContainsKey(propertyName)
                ? _errors[propertyName]
                : null;
        }
        public IEnumerable GetWarnings(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return null;
            return _warnings.ContainsKey(propertyName)
                ? _warnings[propertyName]
                : null;
        }
        public IEnumerable GetInfos(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return null;
            return _infos.ContainsKey(propertyName)
                ? _infos[propertyName]
                : null;
        }
        public bool HasErrors
        {
            get { return _errors.Count > 0; }
        }
        public bool HasWarnings
        {
            get { return _warnings.Count > 0; }
        }
        public bool HasInfos
        {
            get { return _infos.Count > 0; }
        }
        #endregion
    }
}
