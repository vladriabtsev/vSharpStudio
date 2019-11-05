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
            Enumeration prev = null;
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

                RuleFor(p => p.DataTypeLength).Must((p, y) =>
                {
                    prev = p.GetPrevious();
                    if (prev != null && p.DataTypeEnum == common.EnumEnumerationType.STRING_VALUE && p.DataTypeEnum == prev.DataTypeEnum)
                    {
                        if (p.DataTypeLength < prev.DataTypeLength)
                            return false;
                    }
                    return true;
                }).WithMessage(string.Format(Config.ValidationMessages.WARNING_DATA_LENGTH_DANGEROUS_CHANGE, prev?.DataTypeLength)).WithSeverity(Severity.Warning);
                RuleFor(p => p.DataTypeEnum).Must((p, y) =>
                {
                    prev = p.GetPrevious();
                    if (prev != null && p.DataTypeEnum < prev.DataTypeEnum)
                        return false;
                    return true;
                }).WithMessage(string.Format(Config.ValidationMessages.WARNING_DATA_TYPE_DANGEROUS_CHANGE, prev?.DataTypeEnum)).WithSeverity(Severity.Warning);
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
