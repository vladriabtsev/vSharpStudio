using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace ViewModelBase
{
    public class ViewModelValidatableWithSeverity<T, TValidator>
        : ViewModelEditable<T>, INotifyDataErrorInfo, IValidatableWithSeverity//, IComparable
      where TValidator : AbstractValidator<T>
      where T : ViewModelValidatableWithSeverity<T, TValidator>//, IComparable<T>
    {
        public ViewModelValidatableWithSeverity(TValidator validator)
        {
            this._validator = validator;
            this.ValidationCollection = new SortedObservableCollection<ValidationMessage>();
            this.ValidationCollection.SortDirection = SortDirection.Descending;
        }
        protected readonly IValidator _validator;
        protected virtual void OnCountErrorsChanged() { }
        protected virtual void OnCountWarningsChanged() { }
        protected virtual void OnCountInfosChanged() { }
        [BrowsableAttribute(false)]
        public int CountErrors
        {
            get { return _CountErrors; }
            set
            {
                if (_CountErrors != value)
                {
                    _CountErrors = value;
                    NotifyPropertyChanged();
                    OnCountErrorsChanged();
                }
            }
        }
        private int _CountErrors;
        [BrowsableAttribute(false)]
        public int CountWarnings
        {
            get { return _CountWarnings; }
            set
            {
                if (_CountWarnings != value)
                {
                    _CountWarnings = value;
                    NotifyPropertyChanged();
                    OnCountWarningsChanged();
                }
            }
        }
        private int _CountWarnings;
        [BrowsableAttribute(false)]
        public int CountInfos
        {
            get { return _CountInfos; }
            set
            {
                if (_CountInfos != value)
                {
                    _CountInfos = value;
                    NotifyPropertyChanged();
                    OnCountInfosChanged();
                }
            }
        }
        private int _CountInfos;
        [BrowsableAttribute(false)]
        public SortedObservableCollection<ValidationMessage> ValidationCollection { get; private set; }
        protected bool ValidationChange(FluentValidation.Results.ValidationResult res)
        {
            ClearAllErrors();
            if (!res.IsValid)
            {
                foreach (var t in res.Errors)
                {
                    ValidationMessage msg = null;
                    switch (t.Severity)
                    {
                        case Severity.Error:
                            if (!_errors.ContainsKey(t.PropertyName))
                                _errors[t.PropertyName] = new List<string>();
                            _errors[t.PropertyName].Add(t.ErrorMessage);
                            msg = new ValidationMessage<T>((T)this, t.PropertyName, FluentValidation.Severity.Error, t.CustomState == null ? SeverityWeight.Normal : (SeverityWeight)t.CustomState, t.ErrorMessage);
                            break;
                        case Severity.Warning:
                            if (!_warnings.ContainsKey(t.PropertyName))
                                _warnings[t.PropertyName] = new List<string>();
                            _warnings[t.PropertyName].Add(t.ErrorMessage);
                            msg = new ValidationMessage<T>((T)this, t.PropertyName, FluentValidation.Severity.Warning, t.CustomState == null ? SeverityWeight.Normal : (SeverityWeight)t.CustomState, t.ErrorMessage);
                            break;
                        case Severity.Info:
                            if (!_infos.ContainsKey(t.PropertyName))
                                _infos[t.PropertyName] = new List<string>();
                            _infos[t.PropertyName].Add(t.ErrorMessage);
                            msg = new ValidationMessage<T>((T)this, t.PropertyName, FluentValidation.Severity.Info, t.CustomState == null ? SeverityWeight.Normal : (SeverityWeight)t.CustomState, t.ErrorMessage);
                            break;
                    }
                    ValidationCollection.Add(msg);
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
        public override void Restore(T from) { throw new NotImplementedException("Please override Restore method"); }
        public override T Backup() { throw new NotImplementedException("Please override Backup method"); }

        #region INotifyDataErrorInfo methods and helpers
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, List<string>> _warnings = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, List<string>> _infos = new Dictionary<string, List<string>>();
        // than higher weight than higher importance of the message
        //public void SetError(string errorMessage, byte weight = 0, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        //{
        //    if (!_errors.ContainsKey(propertyName))
        //        _errors.Add(propertyName, new List<string> { errorMessage });
        //    var msg = new ValidationMessage<T>((T)this, propertyName, FluentValidation.Severity.Error, weight, errorMessage);
        //    ValidationCollection.Add(msg);
        //    RaiseErrorsChanged(propertyName);
        //    NotifyPropertyChanged(m => m.HasErrors);
        //}
        //// than higher weight than higher importance of the message
        //public void SetWarning(string warningMessage, byte weight = 0, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        //{
        //    if (!_warnings.ContainsKey(propertyName))
        //        _warnings.Add(propertyName, new List<string> { warningMessage });
        //    var msg = new ValidationMessage<T>((T)this, propertyName, FluentValidation.Severity.Warning, weight, warningMessage);
        //    ValidationCollection.Add(msg);
        //    RaiseErrorsChanged(propertyName);
        //    NotifyPropertyChanged(m => m.HasWarnings);
        //}
        //// than higher weight than higher importance of the message
        //public void SetInfo(string infoMessage, byte weight = 0, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        //{
        //    if (!_infos.ContainsKey(propertyName))
        //        _infos.Add(propertyName, new List<string> { infoMessage });
        //    var msg = new ValidationMessage<T>((T)this, propertyName, FluentValidation.Severity.Info, weight, infoMessage);
        //    ValidationCollection.Add(msg);
        //    RaiseErrorsChanged(propertyName);
        //    NotifyPropertyChanged(m => m.HasInfos);
        //}
        // than higher weight than higher importance of the message
        //public void SetOneError(string errorMessage, byte weight = 0, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        //{
        //    ClearError(propertyName);
        //    SetError(errorMessage, weight, propertyName);
        //}
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
            ValidationCollection.Clear();
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

        public virtual int CompareToById(T other) { throw new NotImplementedException("Please override CompareToById method"); }
        public int CompareTo(object obj)
        {
            int res = obj.GetType().Name.CompareTo(typeof(T).Name);
            if (res != 0) return res;
            return CompareToById((T)obj);
        }

        [BrowsableAttribute(false)]
        public bool HasErrors
        {
            get { return _errors.Count > 0; }
        }
        [BrowsableAttribute(false)]
        public bool HasWarnings
        {
            get { return _warnings.Count > 0; }
        }
        [BrowsableAttribute(false)]
        public bool HasInfos
        {
            get { return _infos.Count > 0; }
        }
        #endregion
    }
}
