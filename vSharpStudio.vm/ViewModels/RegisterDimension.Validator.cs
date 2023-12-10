using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class RegisterDimensionValidator
    {
        public RegisterDimensionValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.DimensionCatalogGuid).NotEmpty().WithMessage("Catalog type is not selected for register dimension.");
            this.RuleFor(x => x.DimensionCatalogGuid).Custom((cguid, cntx) =>
            {
                if (string.IsNullOrEmpty(cguid))
                    return;
                var rd = (RegisterDimension)cntx.InstanceToValidate;
                if (!rd.Cfg.DicNodes.ContainsKey(cguid))
                {
                    var vf = new ValidationFailure(nameof(rd.DimensionCatalogGuid),
                        $"Selected catalog type for register dimension is not found in the configuration.");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
                var r = rd.ParentGroupListRegisterDimensions.ParentRegister;
                foreach(var t in rd.ParentGroupListRegisterDimensions.ListDimensions)
                {
                    if (t.Guid == rd.Guid) continue;
                    if (t.DimensionCatalogGuid == rd.DimensionCatalogGuid)
                    {
                        var vf = new ValidationFailure(nameof(rd.DimensionCatalogGuid),
                            $"Register '{r.Name}' dimension '{rd.Name}'. Selected catalog type for register dimension is already used for '{t.Name}' dimension.");
                        vf.Severity = Severity.Error;
                        cntx.AddFailure(vf);
                    }
                }
            });
        }
    }
}
