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

                RuleFor(p => p.DataTypeLength).Must((p, y) =>
                {
                    if (p.DataTypeEnum != common.EnumEnumerationType.STRING_VALUE)
                        return true;
                    if (y < 0) return false;
                    return true;
                }).WithMessage(Config.ValidationMessages.TYPE_LENGTH_POSITIVE);
                RuleFor(p => p.DataTypeLength).Must((p, y) =>
                {
                    if (p.DataTypeEnum != common.EnumEnumerationType.STRING_VALUE)
                        return true;
                    if (y == 0)
                        return false;
                    return true;
                }).WithMessage(Config.ValidationMessages.TYPE_LENGTH_GREATER_THAN_ZERO);
            }
            public static bool IsStartNotWithDigit(string val)
            {
                if (string.IsNullOrWhiteSpace(val))
                    return true;
                char b = val[0];
                return !char.IsDigit(b);
            }
            public static bool IsNotContainsSpace(string val)
            {
                if (string.IsNullOrWhiteSpace(val))
                    return true;
                return !val.Contains(" ");
            }
            private bool IsUnique(Enumeration val)
            {
                if (val.Parent == null)
                    return true;
                if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
                    return true;
                GroupListEnumerations p = (GroupListEnumerations)val.Parent;
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
