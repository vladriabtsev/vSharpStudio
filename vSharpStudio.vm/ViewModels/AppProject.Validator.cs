using System;
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
                //RuleFor(x => x.MinValueString).NotEmpty().WithMessage("Please provide minimum value").WithSeverity(Severity.Warning);
                //RuleFor(x => x.MaxValueString).NotEmpty().WithMessage("Please provide maximum value").WithSeverity(Severity.Warning);
                //RuleFor(x => x.MinValueString).Must(ParsableToBigInteger).WithMessage("Can't parse to integer");
                //RuleFor(x => x.MaxValueString).Must(ParsableToBigInteger).WithMessage("Can't parse to integer");
                //RuleFor(x => x.Length).GreaterThan(0u);
                //RuleFor(x => x.Accuracy).LessThan(x => x.Length);
                //RuleFor(x => x.ObjectName).NotEmpty().When(x => x.DataTypeEnum == EnumDataType.Catalog).WithMessage("Please select catalog name");
                //RuleFor(x => x.ObjectName).NotEmpty().When(x => x.DataTypeEnum == EnumDataType.Document).WithMessage("Please select document name");
                RuleFor(x => x.ConnectionStringName).Must((o, name) => { return IsNotExists(o); }).WithMessage(Config.ValidationMessages.CONN_STR_NAME_NOT_EXISTS);
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
            private bool IsNotExists(AppProject val)
            {
                IParent p = (IParent)val;
                while (p.Parent != null)
                    p = p.Parent;
                Config cfg = (Config)p;
                if (string.IsNullOrWhiteSpace(val.ConnectionStringName))
                    return true;
                foreach (var t in cfg.ListConnectionStringVMs)
                {
                    if (val.ConnectionStringName == t.Name)
                        return true;
                }
                return false;
            }
        }
    }
}
