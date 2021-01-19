using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public partial class EnumerationPairValidator
    {
        public EnumerationPairValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Must((o, name) => { return this.IsUniqueName(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);

            this.RuleFor(x => x.Value).Must((o, name) => { return this.IsValueNotEmpty(o); }).WithMessage(Config.ValidationMessages.ENUM_VALUE_CANT_BE_EMPTY);
            this.RuleFor(x => x.Value).Must((o, name) => { return this.IsUniqueValue(o); }).WithMessage(Config.ValidationMessages.ENUM_VALUE_HAS_TO_BE_UNIQUE);
            this.RuleFor(x => x.Value).Must((o, name) => { return this.IsValueConvertable(o); }).WithMessage(Config.ValidationMessages.ENUM_VALUE_NOT_CONVERTABLE);

            this.RuleFor(x => x.Value).Must((o, name) =>
            {
                if (!o.IsDefault)
                    return true;
                var p = (Enumeration)o.Parent;
                foreach (var t in p.ListEnumerationPairs)
                {
                    if ((o.Guid != t.Guid) && t.IsDefault)
                    {
                        return false;
                    }
                }
                return true;
            }).WithMessage("Only one enumeration value can be selected as default");

            this.RuleFor(x => x.Value).Custom((path, cntx) =>
            {
                var pg = (EnumerationPair)cntx.InstanceToValidate;
                var prev = (EnumerationPair)pg.PrevCurrentVersion();
                var ver = "CURRENT";
                if (prev != null && pg.Value != prev.Value)
                {
                    var vf = new ValidationFailure(nameof(pg.Value),
                        $"Comparison with previous {ver} version. Enumeration value was changed from '{prev.Value}' to '{pg.Value}'");
                    vf.Severity = Severity.Warning;
                    cntx.AddFailure(vf);
                }
                prev = (EnumerationPair)pg.PrevStableVersion();
                ver = "STABLE";
                if (prev != null && pg.Value != prev.Value)
                {
                    var vf = new ValidationFailure(nameof(pg.Value),
                        $"Comparison with previous {ver} version. Enumeration value was changed from '{prev.Value}' to '{pg.Value}'");
                    vf.Severity = Severity.Warning;
                    cntx.AddFailure(vf);
                }
            });
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
        private bool IsUniqueName(EnumerationPair val)
        {
            if (val.Parent == null)
            {
                return true;
            }
            if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
            {
                return true;
            }
            Enumeration p = (Enumeration)val.Parent;
            foreach (var t in p.ListEnumerationPairs)
            {
                if ((val.Guid != t.Guid) && (val.Name == t.Name))
                {
                    return false;
                }
            }
            return true;
        }
        private bool IsValueNotEmpty(EnumerationPair val)
        {
            if (val.Parent == null)
            {
                return true;
            }
            if (string.IsNullOrWhiteSpace(val.Value))
            {
                return false;
            }
            return true;
        }
        private bool IsUniqueValue(EnumerationPair val)
        {
            if (val.Parent == null)
            {
                return true;
            }
            Enumeration p = (Enumeration)val.Parent;
            foreach (var t in p.ListEnumerationPairs)
            {
                if (string.IsNullOrWhiteSpace(val.Value))
                {
                    continue;
                }
                if ((val.Guid != t.Guid) && (val.Value == t.Value))
                {
                    return false;
                }
            }
            return true;
        }
        private bool IsValueConvertable(EnumerationPair val)
        {
            if (string.IsNullOrWhiteSpace(val.Value))
            {
                return true;
            }
            if (val.Parent == null)
            {
                return true;
            }
            Enumeration p = (Enumeration)val.Parent;
            switch (p.DataTypeEnum)
            {
                case common.EnumEnumerationType.BYTE_VALUE:
                    byte b = 0;
                    if (!byte.TryParse(val.Value, out b))
                    {
                        return false;
                    }
                    break;
                case common.EnumEnumerationType.INTEGER_VALUE:
                    int i = 0;
                    if (!Int32.TryParse(val.Value, out i))
                    {
                        return false;
                    }
                    break;
                case common.EnumEnumerationType.SHORT_VALUE:
                    Int16 t = 0;
                    if (!Int16.TryParse(val.Value, out t))
                    {
                        return false;
                    }
                    break;
                case common.EnumEnumerationType.STRING_VALUE:
                    break;
                default:
                    throw new ArgumentException();
            }
            return true;
        }
    }
}
