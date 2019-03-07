﻿using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace ViewModelBase
{
  public class ViewModelValidatable<T, TValidator> : ViewModelEditable<T>, INotifyDataErrorInfo, IValidatable
    where TValidator : AbstractValidator<T>
    where T : ViewModelValidatable<T, TValidator>
  {
    public ViewModelValidatable(TValidator validator)
    {
      this._validator = validator;
    }
    protected readonly IValidator _validator;
    //public readonly T DTO { get { return _dto; }}

    //public override void ResetAllChanges()
    //{
    //  base.ResetAllChanges();
    //  ClearAllErrors();
    //}
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
        foreach (var t in _errors)
        {
          RaiseErrorsChanged(t.Key);
        }
      }
      return res.IsValid;
    }
    public bool Validate()
    {
      var res = this._validator.Validate(this);
      return ValidationChange(res);
    }
    public async void ValidateAsync()
    {
      var res = await this._validator.ValidateAsync(this);
      ValidationChange(res);
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
    }

    #region INotifyDataErrorInfo methods and helpers
    private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
    public void SetError(string propertyName, string errorMessage)
    {
      if (!_errors.ContainsKey(propertyName))
        _errors.Add(propertyName, new List<string> { errorMessage });
      RaiseErrorsChanged(propertyName);
    }
    public void SetErrorMessage(string errorMessage, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
    {
      if (!_errors.ContainsKey(propertyName))
        _errors.Add(propertyName, new List<string> { errorMessage });
      RaiseErrorsChanged(propertyName);
    }
    public void SetOneErrorMessage(string errorMessage, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
    {
      ClearError(propertyName);
      SetError(propertyName, errorMessage);
    }
    protected void ClearError(string propertyName)
    {
      if (_errors.ContainsKey(propertyName))
        _errors.Remove(propertyName);
      RaiseErrorsChanged(propertyName);
    }
    protected void ClearErrorMessages([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
    {
      if (_errors.ContainsKey(propertyName))
        _errors.Remove(propertyName);
      RaiseErrorsChanged(propertyName);
    }
    protected void ClearAllErrors()
    {
      var errors = _errors.Select(error => error.Key).ToList();
      foreach (var propertyName in errors)
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
    public bool HasErrors
    {
      get { return _errors.Count > 0; }
    }
    #endregion
  }
}
