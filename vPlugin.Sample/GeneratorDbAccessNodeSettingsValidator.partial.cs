using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
//using FluentValidation.Results;

namespace vPlugin.Sample
{
    public partial class GeneratorDbAccessNodeSettingsValidator
    {
        public GeneratorDbAccessNodeSettingsValidator()
        {
            this.RuleFor(p => p.IsPropertyParam1).Must((p, y) =>
            {
                if (y)
                {
                    return false;
                }
                return true;
            }).WithMessage("Has to be false");
        }
    }
}
