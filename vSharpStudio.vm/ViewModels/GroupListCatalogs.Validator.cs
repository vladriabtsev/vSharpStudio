using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupListCatalogsValidator
    {
        public GroupListCatalogsValidator()
        {
            this.RuleFor(x => x.PrefixForCompositionNames).Must((o, prefix) =>
            {
                if (!o.ParentModel.IsUseNameComposition)
                    return true;
                if (!string.IsNullOrWhiteSpace(prefix))
                    return true;
                return false;
            }).WithMessage("Prefix can't be empty if name composition usage is chosen for composite names in the model");
            this.RuleFor(x => x.ShortIdTypeForCacheKey).NotEmpty().WithMessage("Can't be empty");
            this.RuleFor(x => x.ShortIdTypeForCacheKey)
                .Must((o, id) =>
                {
                    if (string.IsNullOrEmpty(id))
                        return true;
                    return !char.IsDigit(id[id.Length - 1]);
                })
                .WithMessage("Short type ID can't contain digit as a last symbol");
        }
    }
}
