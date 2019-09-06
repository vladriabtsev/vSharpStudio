﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class AppProject
    {
        public partial class AppProjectValidator
        {
            public AppProjectValidator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
                RuleFor(x => x.Name).Must(Enumeration.EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
                RuleFor(x => x.Name).Must(Enumeration.EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
                RuleFor(x => x.Name).Must((o, name) => { return IsUnique(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            }
            private bool IsUnique(AppProject val)
            {
                if (val.Parent==null)
                    return true;
                if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
                    return true;
                AppSolution p = (AppSolution)val.Parent;
                foreach (var t in p.ListAppProjects)
                {
                    if ((val.Guid != t.Guid) && (val.Name == t.Name))
                        return false;
                }
                return true;
            }
        }
    }
}