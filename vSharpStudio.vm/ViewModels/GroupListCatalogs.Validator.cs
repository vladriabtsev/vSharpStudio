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
            this.RuleFor(x => x.PrefixForDbTables).Must((o, prefix) =>
            {
                if (!string.IsNullOrWhiteSpace(prefix))
                    return true;
                if (o.ParentModel.IsUseGroupPrefix)
                    return false;
                return true;
            }).WithMessage("Prefix can't be empty if prefix usage is chosen for DB table names in the model");
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
