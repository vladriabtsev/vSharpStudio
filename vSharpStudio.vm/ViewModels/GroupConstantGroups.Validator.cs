using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupConstantGroupsValidator
    {
        public GroupConstantGroupsValidator()
        {
            this.RuleFor(x => x.PrefixForCompositionNames).Must((o, prefix) =>
            {
                if (!o.ParentModel.IsUseNameComposition)
                    return true;
                if (!string.IsNullOrWhiteSpace(prefix))
                    return true;
                return false;
            }).WithMessage("Prefix can't be empty if name composition usage is chosen for composite names in the model");
        }
    }
}
