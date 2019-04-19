using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Enumeration
    {
        public partial class EnumerationValidator
        {
            public EnumerationValidator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
                RuleFor(x => x.Name).Must(IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
                RuleFor(x => x.Name).Must(IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
                RuleFor(x => x.Name).Must((o, name) => { return IsUnique(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
                //RuleFor(x => x.MinValueString).NotEmpty().WithMessage("Please provide minimum value").WithSeverity(Severity.Warning);
                //RuleFor(x => x.MaxValueString).NotEmpty().WithMessage("Please provide maximum value").WithSeverity(Severity.Warning);
                //RuleFor(x => x.MinValueString).Must(ParsableToBigInteger).WithMessage("Can't parse to integer");
                //RuleFor(x => x.MaxValueString).Must(ParsableToBigInteger).WithMessage("Can't parse to integer");
                //RuleFor(x => x.Length).GreaterThan(0u);
                //RuleFor(x => x.Accuracy).LessThan(x => x.Length);
                //RuleFor(x => x.ObjectName).NotEmpty().When(x => x.DataTypeEnum == EnumDataType.Catalog).WithMessage("Please select catalog name");
                //RuleFor(x => x.ObjectName).NotEmpty().When(x => x.DataTypeEnum == EnumDataType.Document).WithMessage("Please select document name");
            }
            private bool IsStartNotWithDigit(string val)
            {
                if (string.IsNullOrWhiteSpace(val))
                    return true;
                char b = val[0];
                return !char.IsDigit(b);
            }
            private bool IsNotContainsSpace(string val)
            {
                if (string.IsNullOrWhiteSpace(val))
                    return true;
                return !val.Contains(" ");
            }
            private bool IsUnique(Enumeration val)
            {
                if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
                    return true;
                GroupEnumerations p = (GroupEnumerations)val.Parent;
                foreach (var t in p.ListEnumerations)
                {
                    if ((val.Guid != t.Guid) && (val.Name == t.Name))
                        return false;
                }
                return true;
            }
        }
    }
}
