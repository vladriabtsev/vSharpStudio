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
                this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
                this.RuleFor(x => x.Name).Must(IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
                this.RuleFor(x => x.Name).Must(IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
                this.RuleFor(x => x.Name).Must((o, name) => { return this.IsUnique(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);

                this.RuleFor(p => p.DataTypeLength).Must((p, y) =>
                {
                    if (p.DataTypeEnum != common.EnumEnumerationType.STRING_VALUE)
                    {
                        return true;
                    }

                    if (y < 0)
                    {
                        return false;
                    }

                    return true;
                }).WithMessage(Config.ValidationMessages.TYPE_LENGTH_POSITIVE);
                this.RuleFor(p => p.DataTypeLength).Must((p, y) =>
                {
                    if (p.DataTypeEnum != common.EnumEnumerationType.STRING_VALUE)
                    {
                        return true;
                    }

                    if (y == 0)
                    {
                        return false;
                    }

                    return true;
                }).WithMessage(Config.ValidationMessages.TYPE_LENGTH_GREATER_THAN_ZERO);

                this.RuleFor(p => p.DataTypeLength).Must((p, y) =>
                {
                    this.prev = p.GetPrevious();
                    if (this.prev != null && p.DataTypeEnum == common.EnumEnumerationType.STRING_VALUE && p.DataTypeEnum == this.prev.DataTypeEnum)
                    {
                        if (p.DataTypeLength < this.prev.DataTypeLength)
                        {
                            return false;
                        }
                    }
                    return true;
                }).WithMessage(string.Format(Config.ValidationMessages.WARNING_DATA_LENGTH_DANGEROUS_CHANGE, this.prev?.DataTypeLength)).WithSeverity(Severity.Warning);
                this.RuleFor(p => p.DataTypeEnum).Must((p, y) =>
                {
                    this.prev = p.GetPrevious();
                    if (this.prev != null && p.DataTypeEnum < this.prev.DataTypeEnum)
                    {
                        return false;
                    }

                    return true;
                }).WithMessage(string.Format(Config.ValidationMessages.WARNING_DATA_TYPE_DANGEROUS_CHANGE, this.prev?.DataTypeEnum)).WithSeverity(Severity.Warning);
            }

            public static bool IsStartNotWithDigit(string val)
            {
                if (string.IsNullOrWhiteSpace(val))
                {
                    return true;
                }

                char b = val[0];
                return !char.IsDigit(b);
            }

            public static bool IsNotContainsSpace(string val)
            {
                if (string.IsNullOrWhiteSpace(val))
                {
                    return true;
                }

                return !val.Contains(" ");
            }

            private bool IsUnique(Enumeration val)
            {
                if (val.Parent == null)
                {
                    return true;
                }

                if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
                {
                    return true;
                }

                GroupListEnumerations p = (GroupListEnumerations)val.Parent;
                foreach (var t in p.ListEnumerations)
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
