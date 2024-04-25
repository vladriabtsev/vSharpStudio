using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ViewModelBase
{
    public class VmValidatable<T, TValidator> : VmEditable<T>, INotifyDataErrorInfo, IValidatable
        where TValidator : AbstractValidator<T>
        where T : VmValidatable<T, TValidator>//, IComparable<T>
    {
        public override string ToDebugString()
        {
            int CountErrors()
            {
                var cnt = 0;
                foreach (var t in _errors)
                {
                    cnt += t.Value.Count;
                }
                return cnt;
            }
            var cnt = CountErrors();
            var mes = "";
            if (cnt > 0)
                mes = " ErrCnt:" + cnt;
            return base.ToDebugString() + mes;
        }
        public VmValidatable(TValidator validator)
        {
            this._validator = validator;
        }
        protected readonly TValidator _validator;
        protected bool ValidationChange(FluentValidation.Results.ValidationResult res)
        {
            ClearAllErrors();
            if (!res.IsValid)
            {
                foreach (var t in res.Errors)
                {
                    if (!_errors.ContainsKey(t.PropertyName))
                        _errors[t.PropertyName] = new List<string>();
                    _errors[t.PropertyName].Add(t.ErrorMessage);
                }
                var properties = _errors.Select(error => error.Key).ToList();
                foreach (var propertyName in properties)
                    RaiseErrorsChanged(propertyName);
            }
            return res.IsValid;
        }
        public bool Validate()
        {
            var res = this._validator.Validate((T)this);
            var isValid = ValidationChange(res);
            OnPropertyChanged(nameof(this.HasErrors));
            return isValid;
        }
        public async Task<bool> ValidateAsync()
        {
            var res = await this._validator.ValidateAsync((T)this);
            var isValid = ValidationChange(res);
            OnPropertyChanged(nameof(this.HasErrors));
            return isValid;
        }
        protected bool ValidateProperty([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            Debug.Assert(propertyName != null);
#if DEBUG
            if (IsNotValidate)
                return true;
#endif
            var res = this._validator.Validate((T)this);
            if (!res.IsValid)
            {
                bool found = false;
                foreach (var t in res.Errors)
                {
                    if (t.PropertyName != propertyName)
                        continue;
                    found = true;
                    if (!_errors.ContainsKey(t.PropertyName))
                        _errors[t.PropertyName] = new List<string>();
                    _errors[t.PropertyName].Add(t.ErrorMessage);
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
            }
            return res.IsValid;
        }
        //protected async override void ValidatePropertyAsync(string propertyName)
        //{
        //    var res = await this._validator.ValidateAsync(this);
        //    if (!res.IsValid)
        //    {
        //        bool found = false;
        //        foreach (var t in res.Errors)
        //        {
        //            if (t.PropertyName != propertyName)
        //                continue;
        //            found = true;
        //            if (!_errors.ContainsKey(t.PropertyName))
        //                _errors[t.PropertyName] = new List<string>();
        //            _errors[t.PropertyName].Add(t.ErrorMessage);
        //        }
        //        if (found)
        //        {
        //            RaiseErrorsChanged(propertyName);
        //        }
        //        else if (_errors.ContainsKey(propertyName))
        //        {
        //            _errors.Remove(propertyName);
        //            RaiseErrorsChanged(propertyName);
        //        }
        //    }
        //}
        #region INotifyDataErrorInfo methods and helpers
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public void SetError(string errorMessage, [System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            Debug.Assert(propertyName != null);
            if (!_errors.ContainsKey(propertyName))
                _errors.Add(propertyName, new List<string> { errorMessage });
            RaiseErrorsChanged(propertyName);
            OnPropertyChanged(nameof(this.HasErrors));
        }
        public void SetOneError(string errorMessage, [System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            Debug.Assert(propertyName != null);
            ClearError(propertyName);
            SetError(errorMessage, propertyName);
        }
        protected void ClearError(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
                _errors.Remove(propertyName);
            RaiseErrorsChanged(propertyName);

            OnPropertyChanged(nameof(this.HasErrors));
        }
        protected void ClearErrors([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            Debug.Assert(propertyName != null);
            if (_errors.ContainsKey(propertyName))
                _errors.Remove(propertyName);
            RaiseErrorsChanged(propertyName);

            OnPropertyChanged(nameof(this.HasErrors));
        }
        protected void ClearAllErrors()
        {
            var properties = _errors.Select(error => error.Key).ToList();
            foreach (var propertyName in properties)
                ClearError(propertyName);
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
        public bool HasErrors
        {
            get { return _errors.Count > 0; }
        }
        #endregion
    }
}
