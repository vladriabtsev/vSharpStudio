using FluentValidation;
using FluentValidation.Results;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class CatalogCodePropertySettingsValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(CatalogCodePropertySettingsValidator));
        public CatalogCodePropertySettingsValidator()
        {
            this.GeneralRules();
            this.RuleFor(x => x.MaxSequenceLength).GreaterThan(0u);
            this.RuleFor(x => x.MaxSequenceLength).LessThan(20u);
            this.RuleFor(x => x.Prefix).Custom((prefix, cntx) =>
            {
                var p = cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                //if (string.IsNullOrWhiteSpace(p.SequenceGuid))
                //{
                if (string.IsNullOrWhiteSpace(p.Prefix))
                    p.Prefix = "";
                if (p.Prefix.Length > 0 && (p.SequenceType == common.EnumCodeType.Number))
                {
                    var vf = Common.CreateValidationFailure(nameof(p.Prefix),
                        $"Prefix for numbers is not used. Expected to be empty");
                    cntx.AddFailure(vf);
                }
                //}
            });
        }
    }
}
