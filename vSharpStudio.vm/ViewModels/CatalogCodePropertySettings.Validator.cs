using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Results;

namespace vSharpStudio.vm.ViewModels
{
    public partial class CatalogCodePropertySettingsValidator
    {
        public CatalogCodePropertySettingsValidator()
        {
            this.RuleFor(x => x.MaxSequenceLength).GreaterThan(0u);
            this.RuleFor(x => x.MaxSequenceLength).LessThan(15u);
            this.RuleFor(x => x.Prefix).Custom((prefix, cntx) =>
            {
                var p = (CatalogCodePropertySettings)cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (string.IsNullOrWhiteSpace(p.SequenceGuid))
                {
                    if (string.IsNullOrWhiteSpace(p.Prefix))
                        p.Prefix = "";
                    if (p.Prefix.Length > 0 && (p.SequenceType == common.EnumCodeType.Number))
                    {
                        var vf = new ValidationFailure(nameof(p.Prefix),
                            $"Prefix for numbers is not used. Expected to be empty");
                        vf.Severity = Severity.Error;
                        cntx.AddFailure(vf);
                    }
                }
            });
        }
    }
}
