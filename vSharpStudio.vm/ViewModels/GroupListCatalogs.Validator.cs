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
                var cfg = o.GetConfig();
                if (cfg.Model.IsUseGroupPrefix)
                    return false;
                return true;
            }).WithMessage("Prefix can't be empty if prefix usage is chosen for DB table names in the model");
        }
    }
}
