using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
//using FluentValidation.Results;

namespace vPlugin.Sample
{
    public partial class GeneratorDbAccessSettingsValidator
    {
        public GeneratorDbAccessSettingsValidator()
        {
            this.RuleFor(p => p.AccessParam3).Must((p, y) =>
            {
                if (y=="error")
                {
                    return false;
                }
                return true;
            }).WithMessage("Has to be not equal 'error'");
        }
    }
}
