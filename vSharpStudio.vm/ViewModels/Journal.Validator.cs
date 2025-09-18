using System.Diagnostics;
using FluentValidation;
using FluentValidation.Results;

namespace vSharpStudio.vm.ViewModels
{
    public partial class JournalValidator
    {
        public JournalValidator()
        {
            this.GeneralRules();
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Custom((name, cntx) =>
            {
                if (string.IsNullOrEmpty(name))
                    return;
                var p = (Journal)cntx.InstanceToValidate;
                Debug.Assert(p.Parent != null);
                var pg = p.ParentGroupListJournals;
                foreach (var t in pg.ListJournals)
                {
                    if ((p.Guid != t.Guid) && (name == t.Name))
                    {
                        var vf = new ValidationFailure(nameof(p.Name),
                            $"Not unique journal name '{name}'")
                        {
                            Severity = Severity.Error
                        };
                        cntx.AddFailure(vf);
                    }
                }
            });
        }
    }
}
