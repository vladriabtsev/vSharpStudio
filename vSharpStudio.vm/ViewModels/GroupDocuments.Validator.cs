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
            this.RuleFor(x => x.FiscalYearStartMonth).Custom((prefix, cntx) =>
            {
                var p = (GroupDocuments)cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (p.FiscalYearStartMonth == common.EnumMonths.MONTH_NOT_SELECTED)
                {
                    var vf = new ValidationFailure(nameof(p.FiscalYearStartMonth),
                        $"Financial start month is not selected.")
                    {
                        Severity = Severity.Error
                    };
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.FiscalYearStartMonthDay).Custom((prefix, cntx) =>
            {
                var p = (GroupDocuments)cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (p.FiscalYearStartMonthDay < 1 || p.FiscalYearStartMonthDay > 31)
                {
                    var vf = new ValidationFailure(nameof(p.FiscalYearStartMonthDay),
                        $"Valid start day of month has to be from 1 to 31.")
                    {
                        Severity = Severity.Error
                    };
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.FiscalYearStartMethod).Custom((prefix, cntx) =>
            {
                var p = (GroupDocuments)cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (p.FiscalYearStartMethod == common.EnumFiscalYearStartMethod.FISCAL_YEAR_START_METHOD_NOT_SELECTED)
                {
                    var vf = new ValidationFailure(nameof(p.FiscalYearStartMethod),
                        $"Fiscal year start method is not selected.")
                    {
                        Severity = Severity.Error
                    };
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.FiscalYearStartWeekDay).Custom((prefix, cntx) =>
            {
                var p = (GroupDocuments)cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (p.FiscalYearStartMethod == common.EnumFiscalYearStartMethod.FISCAL_YEAR_START_METHOD_WEEK_DAY_BEFORE_MONTH_DAY && p.FiscalYearStartWeekDay == common.EnumWeekDays.WEEK_NOT_SELECTED)
                {
                    var vf = new ValidationFailure(nameof(p.FiscalYearStartWeekDay),
                        $"Fiscal year start week day is not selected.")
                    {
                        Severity = Severity.Error
                    };
                    cntx.AddFailure(vf);
                }
            });
        }
    }
}
