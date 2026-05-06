using FluentValidation;
using FluentValidation.Results;

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
                var p = (DocumentEnumeratorSequence)cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                var gs = (GroupListEnumeratorSequences)p.Parent;
                foreach (var t in gs.ListEnumeratorSequences)
                {
                    if (t.Guid != p.Guid && name == t.Name)
                    {
                        var vf = new ValidationFailure(nameof(p.Name),
                            $"Sequence name is not unique '{name}'")
                        {
                            Severity = Severity.Error
                        };
                        cntx.AddFailure(vf);
                    }
                }
            });
            this.RuleFor(x => x.MaxSequenceLength).GreaterThan(0u);
            this.RuleFor(x => x.MaxSequenceLength).LessThan(20u);
            this.RuleFor(x => x.Prefix).Custom((prefix, cntx) =>
            {
                var p = (DocumentEnumeratorSequence)cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (string.IsNullOrWhiteSpace(p.Prefix))
                    p.Prefix = "";
                if (p.Prefix.Length > 0 && (p.SequenceType == common.EnumCodeType.Number))
                {
                    var vf = new ValidationFailure(nameof(p.Prefix),
                        $"Prefix for numbers is not used. Expected to be empty")
                    {
                        Severity = Severity.Error
                    };
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.ScopePeriodStartWeekDay).Custom((prefix, cntx) =>
            {
                var p = (DocumentEnumeratorSequence)cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (p.ScopeOfUnique == common.EnumDocNumberUniqueScope.DOC_UNIQUE_WEEK && p.ScopePeriodStartWeekDay == common.EnumWeekDays.WEEK_NOT_SELECTED)
                {
                    var vf = new ValidationFailure(nameof(p.ScopePeriodStartWeekDay),
                        $"Week start day is not selected.")
                    {
                        Severity = Severity.Error
                    };
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.ScopePeriodStartMonth).Custom((prefix, cntx) =>
            {
                var p = (DocumentEnumeratorSequence)cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (p.ScopePeriodStartMonth == common.EnumMonths.MONTH_NOT_SELECTED)
                {
                    var vf = new ValidationFailure(nameof(p.ScopePeriodStartMonth),
                        $"Financial start month is not selected.")
                    {
                        Severity = Severity.Error
                    };
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.ScopePeriodStartMonthDay).Custom((prefix, cntx) =>
            {
                var p = (DocumentEnumeratorSequence)cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (p.ScopePeriodStartMonthDay < 1 || p.ScopePeriodStartMonthDay > 28)
                {
                    var vf = new ValidationFailure(nameof(p.ScopePeriodStartMonthDay),
                        $"Valid start day of month has to be from 1 to 28.")
                    {
                        Severity = Severity.Error
                    };
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.ScopePeriodStartTimeZoneHour).Custom((prefix, cntx) =>
            {
                var p = (DocumentEnumeratorSequence)cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (p.ScopePeriodStartTimeZoneHour < -12 || p.ScopePeriodStartTimeZoneHour > 12)
                {
                    var vf = new ValidationFailure(nameof(p.ScopePeriodStartTimeZoneHour),
                        $"Valid zone hour has to be from -12 to 12.")
                    {
                        Severity = Severity.Error
                    };
                    cntx.AddFailure(vf);
                }
                if (p.ScopePeriodStartTimeZoneHour != 0 && p.ScopePeriodStartTimeZoneMinute != 0 && Math.Sign(p.ScopePeriodStartTimeZoneHour) != Math.Sign(p.ScopePeriodStartTimeZoneMinute))
                {
                    var vf = new ValidationFailure(nameof(p.ScopePeriodStartTimeZoneHour),
                        $"Time zone hours and minutes has to have same sign.")
                    {
                        Severity = Severity.Error
                    };
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.ScopePeriodStartTimeZoneMinute).Custom((prefix, cntx) =>
            {
                var p = (DocumentEnumeratorSequence)cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (p.ScopePeriodStartTimeZoneMinute < -30 || p.ScopePeriodStartTimeZoneMinute > 30)
                {
                    var vf = new ValidationFailure(nameof(p.ScopePeriodStartTimeZoneMinute),
                        $"Valid zone minutes has to be from -30 to 30.")
                    {
                        Severity = Severity.Error
                    };
                    cntx.AddFailure(vf);
                }
                if (p.ScopePeriodStartTimeZoneHour != 0 && p.ScopePeriodStartTimeZoneMinute != 0 && Math.Sign(p.ScopePeriodStartTimeZoneHour) != Math.Sign(p.ScopePeriodStartTimeZoneMinute))
                {
                    var vf = new ValidationFailure(nameof(p.ScopePeriodStartTimeZoneMinute),
                        $"Time zone hours and minutes has to have same sign.")
                    {
                        Severity = Severity.Error
                    };
                    cntx.AddFailure(vf);
                }
            });
        }
    }
}
