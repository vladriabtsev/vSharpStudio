using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public partial class EnumerationPair
    {
        public partial class EnumerationPairValidator
        {
            string prevValue = null;
            public EnumerationPairValidator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
                RuleFor(x => x.Name).Must(IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
                RuleFor(x => x.Name).Must(IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
                RuleFor(x => x.Name).Must((o, name) => { return IsUniqueName(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);

                RuleFor(x => x.Value).Must((o, name) => { return IsValueNotEmpty(o); }).WithMessage(Config.ValidationMessages.ENUM_VALUE_CANT_BE_EMPTY);
                RuleFor(x => x.Value).Must((o, name) => { return IsUniqueValue(o); }).WithMessage(Config.ValidationMessages.ENUM_VALUE_HAS_TO_BE_UNIQUE);
                RuleFor(x => x.Value).Must((o, name) => { return IsValueConvertable(o); }).WithMessage(Config.ValidationMessages.ENUM_VALUE_NOT_CONVERTABLE);

                RuleFor(p => p.Value).Must((p, y) =>
                {
                    var prev = p.GetPrevious();
                    if (prev != null)
                    {
                        prevValue = prev.Value;
                        if (p.Value != prev.Value)
                            return false;
                    }
                    return true;
                }).WithMessage(string.Format(Config.ValidationMessages.WARNING_ENUM_VALUE_DANGEROUS_CHANGE, prevValue)).WithSeverity(Severity.Warning);
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
            private bool IsUniqueName(EnumerationPair val)
            {
                if (val.Parent == null)
                    return true;
                if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
                    return true;
                Enumeration p = (Enumeration)val.Parent;
                foreach (var t in p.ListEnumerationPairs)
                {
                    if ((val.Guid != t.Guid) && (val.Name == t.Name))
                        return false;
                }
                return true;
            }
            private bool IsValueNotEmpty(EnumerationPair val)
            {
                if (val.Parent == null)
                    return true;
                if (string.IsNullOrWhiteSpace(val.Value))
                    return false;
                //Enumeration p = (Enumeration)val.Parent;
                //switch (p.DataTypeEnum)
                //{
                //    case common.EnumEnumerationType.BYTE_VALUE:
                //        if (string.IsNullOrWhiteSpace(val.Value))
                //            return false;
                //        break;
                //    case common.EnumEnumerationType.INTEGER_VALUE:
                //        if (string.IsNullOrWhiteSpace(val.Value))
                //            return false;
                //        break;
                //    case common.EnumEnumerationType.SHORT_VALUE:
                //        if (string.IsNullOrWhiteSpace(val.Value))
                //            return false;
                //        break;
                //    case common.EnumEnumerationType.STRING_VALUE:
                //        if (string.IsNullOrWhiteSpace(val.Value))
                //            return false;
                //        break;
                //    default:
                //        throw new ArgumentException();
                //}
                return true;
            }
            private bool IsUniqueValue(EnumerationPair val)
            {
                if (val.Parent == null)
                    return true;
                Enumeration p = (Enumeration)val.Parent;
                foreach (var t in p.ListEnumerationPairs)
                {
                    if (string.IsNullOrWhiteSpace(val.Value))
                        continue;
                    if ((val.Guid != t.Guid) && (val.Value == t.Value))
                        return false;
                }
                return true;
            }
            private bool IsValueConvertable(EnumerationPair val)
            {
                if (string.IsNullOrWhiteSpace(val.Value))
                    return true;
                if (val.Parent == null)
                    return true;
                Enumeration p = (Enumeration)val.Parent;
                switch (p.DataTypeEnum)
                {
                    case common.EnumEnumerationType.BYTE_VALUE:
                        byte b = 0;
                        if (!byte.TryParse(val.Value, out b))
                            return false;
                        break;
                    case common.EnumEnumerationType.INTEGER_VALUE:
                        int i = 0;
                        if (!Int32.TryParse(val.Value, out i))
                            return false;
                        break;
                    case common.EnumEnumerationType.SHORT_VALUE:
                        Int16 t = 0;
                        if (!Int16.TryParse(val.Value, out t))
                            return false;
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
}
