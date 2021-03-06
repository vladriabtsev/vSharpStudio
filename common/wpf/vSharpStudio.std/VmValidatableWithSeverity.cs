﻿using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace ViewModelBase
{
    public class VmValidatableWithSeverity<T, TValidator> : VmEditable<T>, INotifyDataErrorInfo, IValidatableWithSeverity//, IComparable
        where TValidator : AbstractValidator<T>
        where T : VmValidatableWithSeverity<T, TValidator>//, IComparable<T>
    {
        public VmValidatableWithSeverity(TValidator validator)
        {
            this.IsValidate = true;
            this._validator = validator;
            this.ValidationCollection = new SortedObservableCollection<ValidationMessage>();
            this.ValidationCollection.SortDirection = SortDirection.Descending;
        }
        protected readonly TValidator _validator;
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
        public SortedObservableCollection<ValidationMessage> ValidationCollection
        {
            get { return _ValidationCollection; }
            set
            {
                if (_ValidationCollection != value)
                {
                    _ValidationCollection = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private SortedObservableCollection<ValidationMessage> _ValidationCollection;
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
                            msg = new ValidationMessage(this, t.PropertyName, FluentValidation.Severity.Error, t.CustomState == null ? SeverityWeight.Normal : (SeverityWeight)t.CustomState, t.ErrorMessage);
                            break;
                        case Severity.Warning:
                            if (!_warnings.ContainsKey(t.PropertyName))
                                _warnings[t.PropertyName] = new List<string>();
                            _warnings[t.PropertyName].Add(t.ErrorMessage);
                            msg = new ValidationMessage(this, t.PropertyName, FluentValidation.Severity.Warning, t.CustomState == null ? SeverityWeight.Normal : (SeverityWeight)t.CustomState, t.ErrorMessage);
                            break;
                        case Severity.Info:
                            if (!_infos.ContainsKey(t.PropertyName))
                                _infos[t.PropertyName] = new List<string>();
                            _infos[t.PropertyName].Add(t.ErrorMessage);
                            msg = new ValidationMessage(this, t.PropertyName, FluentValidation.Severity.Info, t.CustomState == null ? SeverityWeight.Normal : (SeverityWeight)t.CustomState, t.ErrorMessage);
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
            if (!this.IsValidate)
                return true;
            var res = this._validator.Validate((T)this);
            var isValid = ValidationChange(res);
            NotifyPropertyChanged(nameof(this.HasErrors));
            NotifyPropertyChanged(nameof(this.HasWarnings));
            NotifyPropertyChanged(nameof(this.HasInfos));
            return isValid;
        }
        public async void ValidateAsync()
        {
            if (!this.IsValidate)
                return;
            var res = await this._validator.ValidateAsync((T)this);
            ValidationChange(res);
            NotifyPropertyChanged(nameof(this.HasErrors));
            NotifyPropertyChanged(nameof(this.HasWarnings));
            NotifyPropertyChanged(nameof(this.HasInfos));
        }
        protected void ValidateProperty
            (Expression<Func<T>> property)
        {
            var propertyName = this.GetPropertyName<T>(property);
            ValidateProperty(propertyName);
        }
        protected bool ValidateProperty([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (!VmBindable.IsValidateAll)
                return true;
            if (!this.IsValidate)
                return true;
#if DEBUG
            if (isNotValidateForUnitTests)
                return true;
#endif
            //if (!this.IsValidate)
            //    return true;
            var res = this._validator.Validate((T)this);
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
            else
            {
                if (_errors.ContainsKey(propertyName))
                {
                    _errors.Remove(propertyName);
                    RaiseErrorsChanged(propertyName);
                }
                if (_warnings.ContainsKey(propertyName))
                {
                    _warnings.Remove(propertyName);
                    RaiseErrorsChanged(propertyName);
                }
                if (_infos.ContainsKey(propertyName))
                {
                    _infos.Remove(propertyName);
                    RaiseErrorsChanged(propertyName);
                }
            }
            return res.IsValid;
        }

        #region INotifyDataErrorInfo methods and helpers
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, List<string>> _warnings = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, List<string>> _infos = new Dictionary<string, List<string>>();
        protected void ClearError(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
                _errors.Remove(propertyName);
            if (_warnings.ContainsKey(propertyName))
                _warnings.Remove(propertyName);
            if (_infos.ContainsKey(propertyName))
                _infos.Remove(propertyName);
            RaiseErrorsChanged(propertyName);

            NotifyPropertyChanged(nameof(this.HasErrors));
            NotifyPropertyChanged(nameof(this.HasWarnings));
            NotifyPropertyChanged(nameof(this.HasInfos));
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

            NotifyPropertyChanged(nameof(this.HasErrors));
            NotifyPropertyChanged(nameof(this.HasWarnings));
            NotifyPropertyChanged(nameof(this.HasInfos));
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

        public void ValidateSubAndCollectErrors(IValidatableWithSeverity sub)
        {
            sub.Validate();
            //foreach (var t in sub.ValidationCollection)
            //{
            //    this.ValidationCollection.Add(t);
            //}
            this.ValidationCollection.AddRange(sub.ValidationCollection);
        }
    }
}
