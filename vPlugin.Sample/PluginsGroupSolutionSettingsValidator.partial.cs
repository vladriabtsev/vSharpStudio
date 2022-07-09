using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
//using FluentValidation.Results;

namespace vPlugin.Sample
{
    public partial class PluginsGroupSolutionSettingsValidator
    {
        public PluginsGroupSolutionSettingsValidator()
        {
            this.RuleFor(p => p.Description).Must((p, y) =>
            {
                if (string.IsNullOrEmpty(y))
                {
                    return false;
                }
                return true;
            }).WithMessage("Has to be not empty");
        }
    }
}
