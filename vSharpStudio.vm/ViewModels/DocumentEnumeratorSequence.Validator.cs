using FluentValidation;
using FluentValidation.Results;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class DocumentEnumeratorSequenceValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(DocumentEnumeratorSequenceValidator));
        public DocumentEnumeratorSequenceValidator()
        {
            this.GeneralRules();
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Custom((name, cntx) =>
            {
                var p = cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                var gs = (GroupListEnumeratorSequences)p.Parent;
                foreach (var t in gs.ListEnumeratorSequences)
                {
                    if (t.Guid != p.Guid && name == t.Name)
                    {
                        var vf = Common.CreateValidationFailure(nameof(p.Name),
                            $"Sequence name is not unique '{name}'");
                        cntx.AddFailure(vf);
                    }
                }
            });
            this.RuleFor(x => x.MaxSequenceLength).GreaterThan(0u);
            this.RuleFor(x => x.MaxSequenceLength).LessThan(20u);
            this.RuleFor(x => x.Prefix).Custom((prefix, cntx) =>
            {
                var p = cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (string.IsNullOrWhiteSpace(p.Prefix))
                    p.Prefix = "";
                if (p.Prefix.Length > 0 && (p.SequenceType == common.EnumCodeType.Number))
                {
                    var vf = Common.CreateValidationFailure(nameof(p.Prefix),
                        $"Sequence '{p.Name}'. Prefix for numbers is not used. Expected to be empty");
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.ScopeOfUnique).Custom((prefix, cntx) =>
            {
                var p = cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (p.ScopeOfUnique == common.EnumDocNumberUniqueScope.DOC_UNIQUE_NOT_SELECTED)
                {
                    var vf = Common.CreateValidationFailure(nameof(p.ScopeOfUnique),
                        $"Sequence '{p.Name}'. Scope of uniqueness for document number is not selected.");
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.ScopePeriodStartWeekDay).Custom((prefix, cntx) =>
            {
                var p = cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (p.ScopeOfUnique == common.EnumDocNumberUniqueScope.DOC_UNIQUE_WEEK && p.ScopePeriodStartWeekDay == common.EnumWeekDays.WEEK_NOT_SELECTED)
                {
                    var vf = Common.CreateValidationFailure(nameof(p.ScopePeriodStartWeekDay),
                        $"Sequence '{p.Name}'. Week start day is not selected.");
                    cntx.AddFailure(vf);
                }
            });
        }
    }
}
