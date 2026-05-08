using System;
using FluentValidation;
using FluentValidation.Results;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupDocumentsValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(GroupDocumentsValidator));
        public GroupDocumentsValidator()
        {
            this.GeneralRules();
            this.RuleFor(x => x.ScopePeriodStartMonth).Custom((prefix, cntx) =>
            {
                var p = (GroupDocuments)cntx.InstanceToValidate;
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
                var p = (GroupDocuments)cntx.InstanceToValidate;
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
                var p = (GroupDocuments)cntx.InstanceToValidate;
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
                var p = (GroupDocuments)cntx.InstanceToValidate;
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
