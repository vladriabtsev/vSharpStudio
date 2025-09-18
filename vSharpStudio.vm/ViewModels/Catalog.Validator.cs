using FluentValidation;
using FluentValidation.Results;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class CatalogValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(CatalogValidator));
        public CatalogValidator()
        {
            this.GeneralRules();
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Must((o, name) => { return this.IsUnique(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            this.RuleFor(x => x.Name).Custom((name, cntx) =>
            {
                var c = (ICatalog)cntx.InstanceToValidate;
                var mes = c.Cfg.GroupAppSolutions.TableNameValidation(c.CompositeName);
                if (!string.IsNullOrEmpty(mes))
                {
                    var vf = new ValidationFailure(nameof(c.Name), mes)
                    {
                        Severity = Severity.Error
                    };
                    cntx.AddFailure(vf);
                }
            });
        }
        private bool IsUnique(Catalog val)
        {
            if (val.Parent == null)
            {
                return true;
            }
            if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
            {
                return true;
            }
            GroupListCatalogs p = (GroupListCatalogs)val.Parent;
            foreach (var t in p.ListCatalogs)
            {
                if ((val.Guid != t.Guid) && (val.Name == t.Name))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
