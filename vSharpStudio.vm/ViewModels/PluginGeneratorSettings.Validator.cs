using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class PluginGeneratorSettingsValidator
    {
        public PluginGeneratorSettingsValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Must((o, name) => { return this.IsUnique(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            this.RuleFor(x => x.FilePath).Must((o, fp) =>
            {
                if (o.IsPrivate && string.IsNullOrWhiteSpace(fp))
                {
                    return false;
                }

                return true;
            }).WithMessage(Config.ValidationMessages.FILE_PATH_FOR_PRIVATE_PATH);
            this.RuleFor(x => x.IsPrivate).Must((o, p) =>
            {
                if (p && string.IsNullOrWhiteSpace(o.FilePath))
                {
                    return false;
                }

                return true;
            }).WithMessage(Config.ValidationMessages.FILE_PATH_FOR_IS_PRIVATE);
        }

        private bool IsUnique(PluginGeneratorSettings val)
        {
            if (val.Parent == null)
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
            {
                return true;
            }

            if (val.Parent is PluginGenerator)
            {
                PluginGenerator p = (PluginGenerator)val.Parent;
                foreach (var t in p.ListSettings)
                {
                    if ((val.Guid != t.Guid) && (val.Name == t.Name))
                    {
                        return false;
                    }
                }
            }
            else
            {
                throw new Exception();
            }
            return true;
        }
    }
}
