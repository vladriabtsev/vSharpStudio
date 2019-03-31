using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModelBase
{
    public class ValidatorBase<T, TValidator> : AbstractValidator<T>
      where TValidator : ValidatorBase<T, TValidator>, new()
    {
        private static TValidator _validator = default(TValidator);
        public static TValidator Validator
        {
            get
            {
                if (_validator == null)
                    _validator = new TValidator();
                return _validator;
            }
        }
        public static void Reset()
        {
            _validator = default(TValidator);
        }
    }
}
