﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class AppSolution
    {
        public partial class AppSolutionValidator
        {
            public AppSolutionValidator()
            {
                this.RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
                this.RuleFor(x => x.Name)
                    .Must(Enumeration.EnumerationValidator.IsStartNotWithDigit)
                    .WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
                this.RuleFor(x => x.Name)
                    .Must(Enumeration.EnumerationValidator.IsNotContainsSpace)
                    .WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
                this.RuleFor(x => x.Name)
                    .Must((o, name) => { return this.IsUnique(o); })
                    .WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
                //this.RuleFor(x => x.PluginsGroup1)
                //    .Must((x, g) =>
                //    {
                //        return !string.IsNullOrWhiteSpace(x.PluginsGroup1?.PluginsGroupGuid)
                //            || !string.IsNullOrWhiteSpace(x.PluginsGroup2?.PluginsGroupGuid)
                //            || !string.IsNullOrWhiteSpace(x.PluginsGroup3?.PluginsGroupGuid);
                //    })
                //    .WithMessage("At least one plugin group has to be selected if need code generation")
                //    .WithSeverity(Severity.Warning);


                // RuleFor(x => x.MinValueString).NotEmpty().WithMessage("Please provide minimum value").WithSeverity(Severity.Warning);
                // RuleFor(x => x.MaxValueString).NotEmpty().WithMessage("Please provide maximum value").WithSeverity(Severity.Warning);
                // RuleFor(x => x.MinValueString).Must(ParsableToBigInteger).WithMessage("Can't parse to integer");
                // RuleFor(x => x.MaxValueString).Must(ParsableToBigInteger).WithMessage("Can't parse to integer");
                // RuleFor(x => x.Length).GreaterThan(0u);
                // RuleFor(x => x.Accuracy).LessThan(x => x.Length);
                // RuleFor(x => x.ObjectName).NotEmpty().When(x => x.DataTypeEnum == EnumDataType.Catalog).WithMessage("Please select catalog name");
                // RuleFor(x => x.ObjectName).NotEmpty().When(x => x.DataTypeEnum == EnumDataType.Document).WithMessage("Please select document name");
            }

            private bool IsUnique(AppSolution val)
            {
                if (val.Parent == null)
                {
                    return true;
                }

                if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
                {
                    return true;
                }

                GroupListAppSolutions p = (GroupListAppSolutions)val.Parent;
                foreach (var t in p.ListAppSolutions)
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
}
