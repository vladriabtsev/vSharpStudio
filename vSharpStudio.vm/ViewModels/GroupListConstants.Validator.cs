using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupListConstantsValidator
    {
        public GroupListConstantsValidator()
        {
            this.RuleFor(x => x.ShortIdTypeForCacheKey).NotEmpty().WithMessage("Can't be empty");
            this.RuleFor(x => x.ShortIdTypeForCacheKey)
                .Must((o, id) =>
                {
                    if (string.IsNullOrEmpty(id))
                        return true;
                    return !char.IsDigit(id[id.Length - 1]);
                })
                .WithMessage("Short type ID can't contain digit as a last symbol");
            //this.RuleFor(x => x.PrefixForDbTables).Must((o, prefix) =>
            //{
            //    if (!string.IsNullOrWhiteSpace(prefix))
            //        return true;
            //    var cfg = o.GetConfig();
            //    if (cfg.Model.IsUseGroupPrefix)
            //        return false;
            //    return true;
            //}).WithMessage("Prefix can't be empty if prefix usage is chosen for DB table names in the model");
            //this.RuleFor(x => x.PropertyCodeName).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            //this.RuleFor(x => x.PropertyCodeName).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            //this.RuleFor(x => x.PropertyCodeName).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            //this.RuleFor(x => x.PropertyNameName).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            //this.RuleFor(x => x.PropertyNameName).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            //this.RuleFor(x => x.PropertyNameName).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            //this.RuleFor(x => x.PropertyDescriptionName).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            //this.RuleFor(x => x.PropertyDescriptionName).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            //this.RuleFor(x => x.PropertyDescriptionName).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
        }
    }
}
