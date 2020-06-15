using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentValidation;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class AppProjectValidator
    {
        public AppProjectValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Must((o, name) => { return this.IsUnique(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            this.RuleFor(x => x.RelativeAppProjectPath)
                .NotEmpty()
                .WithMessage("Project is not selected");
            this.RuleFor(x => x.RelativeAppProjectPath)
                .Must((o, path) =>
                {
                    if (string.IsNullOrEmpty(path))
                        return true;
                    return File.Exists(o.GetProjectPath());
                })
                .WithMessage("Project file was not found");
            this.RuleFor(x => x.ListAppProjectGenerators)
                .Must((o, lst) =>
                {
                    return lst != null && lst.Count > 0;
                })
                .WithMessage("Generators are not added yet").WithSeverity(Severity.Warning);
            this.RuleFor(x => x.Namespace)
                .NotEmpty()
                .WithMessage("Namespace name is not entered");
        }

        private bool IsUnique(AppProject val)
        {
            if (val.Parent == null)
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
            {
                return true;
            }

            AppSolution p = (AppSolution)val.Parent;
            foreach (var t in p.ListAppProjects)
            {
                if ((val.Guid != t.Guid) && (val.Name == t.Name))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
