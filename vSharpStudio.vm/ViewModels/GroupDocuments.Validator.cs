using System;
using FluentValidation;
using FluentValidation.Results;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupDocumentsValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(GroupDocumentsValidator));
        public GroupDocumentsValidator()
        {
            this.GeneralRules();
            this.RuleFor(x => x.FiscalYearStartMonth).Custom((prefix, cntx) =>
            {
                var p = cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (p.FiscalYearStartMonth == common.EnumMonths.MONTH_NOT_SELECTED)
                {
                    var vf = Common.CreateValidationFailure(nameof(p.FiscalYearStartMonth),
                        $"Financial start month is not selected.");
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.FiscalYearStartMonthDay).Custom((prefix, cntx) =>
            {
                var p = cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (p.FiscalYearStartMonthDay < 1 || p.FiscalYearStartMonthDay > 31)
                {
                    var vf = Common.CreateValidationFailure(nameof(p.FiscalYearStartMonthDay),
                        $"Valid start day of month has to be from 1 to 31.");
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.FiscalYearStartWeekDay).Custom((prefix, cntx) =>
            {
                var p = cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (p.FiscalYearStartMethod == common.EnumFiscalYearStartMethod.FISCAL_YEAR_START_METHOD_WEEK_DAY_BEFORE_MONTH_DAY && p.FiscalYearStartWeekDay == common.EnumWeekDays.WEEK_NOT_SELECTED)
                {
                    var vf = Common.CreateValidationFailure(nameof(p.FiscalYearStartWeekDay),
                        $"Fiscal year start week day is not selected.");
                    cntx.AddFailure(vf);
                }
            });
        }
    }
}
