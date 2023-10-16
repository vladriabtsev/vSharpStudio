using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ViewModelBase
{
    public class VmValidatableWithSeverity<T, TValidator> : VmEditable<T>, INotifyDataErrorInfo, IValidatableWithSeverity//, IComparable
        where TValidator : AbstractValidator<T>
        where T : VmValidatableWithSeverity<T, TValidator>//, IComparable<T>
    {
        public override string ToDebugString()
        {
            var mes = "";
            if (this.CountErrors > 0)
                mes = " ErrCnt:" + this.CountErrors;
            return base.ToDebugString() + mes;
        }
        public VmValidatableWithSeverity(TValidator? validator)
        {
            Debug.Assert(validator != null);
            this._validator = validator;
            this._ValidationCollection = new SortedObservableCollection<ValidationMessage>();
            this._ValidationCollection.SortDirection = SortDirection.Descending;
        }
        protected TValidator _validator { get; private set; }
        protected void SetValidator(TValidator validator) { this._validator = validator; }
        protected virtual void OnCountErrorsChanged() { }
        protected virtual void OnCountWarningsChanged() { }
        protected virtual void OnCountInfosChanged() { }
        //protected virtual ValidationResult ValidatePluginGeneratorSettings()
        //{
        //    return new ValidationResult();
        //}
        //protected virtual Task<ValidationResult> ValidatePluginGeneratorSettingsAsync()
        //{
        //    return Task.FromResult(new ValidationResult());
        //}

        /// <summary>
        /// Amount of errors in this node and all subnodes
        /// </summary>
        [Browsable(false)]
        public int CountErrors
        {
            get { return _CountErrors; }
            set
            {
                if (_CountErrors != value)
                {
                    UIDispatcher.Invoke(() =>
                    {
                        _CountErrors = value;
                        NotifyPropertyChanged();
                        OnCountErrorsChanged();
                    });
                }
            }
        }
        private int _CountErrors;
        /// <summary>
        /// Amount of warnings in this node and all subnodes
        /// </summary>
        [Browsable(false)]
        public int CountWarnings
        {
            get { return _CountWarnings; }
            set
            {
                if (_CountWarnings != value)
                {
                    UIDispatcher.Invoke(() =>
                    {
                        _CountWarnings = value;
                        NotifyPropertyChanged();
                        OnCountWarningsChanged();
                    });
                }
            }
        }
        private int _CountWarnings;
        /// <summary>
        /// Amount of info messages in this node and all subnodes
        /// </summary>
        [Browsable(false)]
        public int CountInfos
        {
            get { return _CountInfos; }
            set
            {
                if (_CountInfos != value)
                {
                    UIDispatcher.Invoke(() =>
                    {
                        _CountInfos = value;
                        NotifyPropertyChanged();
                        OnCountInfosChanged();
                    });
                }
            }
        }
        private int _CountInfos;
        [Browsable(false)]
        public SortedObservableCollection<ValidationMessage> ValidationCollection
        {
            get { return _ValidationCollection; }
            set
            {
                if (_ValidationCollection != value)
                {
                    UIDispatcher.Invoke(() =>
                    {
                        _ValidationCollection = value;
                        NotifyPropertyChanged();
                    });
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
                    ValidationMessage? msg = null;
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
                    UIDispatcher.Invoke(() =>
                    {
                        Debug.Assert(msg != null);
                        ValidationCollection.Add(msg);
                    });
                }
                Dictionary<string, string?> dic = new Dictionary<string, string?>();
                foreach (var t in _errors)
                    dic[t.Key] = null;
                foreach (var t in _warnings)
                    dic[t.Key] = null;
                foreach (var t in _infos)
                    dic[t.Key] = null;
                var properties = dic.Select(error => error.Key).ToList();
                UIDispatcher.Invoke(() =>
                {
                    foreach (var propertyName in properties)
                        RaiseErrorsChanged(propertyName);
                });
            }
            return res.IsValid;
        }
        protected virtual void OnValidated(ValidationResult val_res) { }
        protected virtual Task[] OnValidatedAsync(ValidationResult val_res) { var tsks = new Task[0]; return tsks; }
        protected ValidationResult? ValidationResult { get; private set; }
        public bool Validate()
        {
            var res = this._validator.Validate((T)this);
            //var resPlgn = this.ValidatePluginGeneratorSettings();
            //res.Errors.AddRange(resPlgn.Errors);
            this.ValidationResult = res;
            OnValidated(res);
            var isValid = ValidationChange(res);
            NotifyPropertyChanged(nameof(this.HasErrors));
            NotifyPropertyChanged(nameof(this.HasWarnings));
            NotifyPropertyChanged(nameof(this.HasInfos));
            return isValid;
        }
        public async Task<bool> ValidateAsync()
        {
            var res = await this._validator.ValidateAsync((T)this);
            //var resPlgn = await this.ValidatePluginGeneratorSettingsAsync();
            //res.Errors.AddRange(resPlgn.Errors);
            var tsks = OnValidatedAsync(res);
            await Task.WhenAll(tsks);
            var isValid = ValidationChange(res);
            NotifyPropertyChanged(nameof(this.HasErrors));
            NotifyPropertyChanged(nameof(this.HasWarnings));
            NotifyPropertyChanged(nameof(this.HasInfos));
            return isValid;
        }
        protected void ValidateProperty(Expression<Func<T>> property)
        {
            var propertyName = this.GetPropertyName<T>(property);
            ValidateProperty(propertyName);
        }
        protected bool ValidateProperty([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            Debug.Assert(propertyName != null);
#if DEBUG
            if (isNotValidateForUnitTests)
                return true;
#endif
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
        protected void ClearErrors([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            Debug.Assert(propertyName != null);
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
            Dictionary<string, string?> dic = new Dictionary<string, string?>();
            foreach (var t in _errors)
                dic[t.Key] = null;
            foreach (var t in _warnings)
                dic[t.Key] = null;
            foreach (var t in _infos)
                dic[t.Key] = null;
            var properties = dic.Select(error => error.Key).ToList();
            UIDispatcher.Invoke(() =>
            {
                foreach (var propertyName in properties)
                    ClearError(propertyName);
                ValidationCollection.Clear();
            });
        }
        public void RaiseErrorsChanged(string propertyName)
        {
            if (this.ErrorsChanged != null)
                this.ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public IEnumerable GetErrors(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return new object[0];
            return _errors.ContainsKey(propertyName)
                ? _errors[propertyName]
                : new object[0];
        }
        public IEnumerable GetWarnings(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return new object[0];
            return _warnings.ContainsKey(propertyName)
                ? _warnings[propertyName]
                : new object[0];
        }
        public IEnumerable GetInfos(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return new object[0];
            return _infos.ContainsKey(propertyName)
                ? _infos[propertyName]
                : new object[0];
        }
        /// <summary>
        /// Has errors in this node (without children)
        /// </summary>
        [Browsable(false)]
        public bool HasErrors
        {
            get { return _errors.Count > 0; }
        }
        /// <summary>
        /// Has warnings in this node (without children)
        /// </summary>
        [Browsable(false)]
        public bool HasWarnings
        {
            get { return _warnings.Count > 0; }
        }
        /// <summary>
        /// Has info messages in this node (without children)
        /// </summary>
        [Browsable(false)]
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
