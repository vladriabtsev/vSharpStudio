using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Results;

namespace vSharpStudio.vm.ViewModels
{
    public partial class DocumentNumberPropertySettingsValidator
    {
        public DocumentNumberPropertySettingsValidator()
        {
            this.RuleFor(x => x.MaxSequenceLength).GreaterThan(0u);
            this.RuleFor(x => x.MaxSequenceLength).LessThan(15u);
            this.RuleFor(x => x.Prefix).Custom((prefix, cntx) =>
            {
                var p = (DocumentNumberPropertySettings)cntx.InstanceToValidate;
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
            this.RuleFor(x => x.ScopePeriodStartMonthDay).Custom((day, cntx) =>
            {
                var p = (DocumentNumberPropertySettings)cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (p.ScopePeriodStartMonth == common.EnumMonths.MONTH_NOT_SELECTED)
                    return;
                uint day_max = 0;
                switch (p.ScopePeriodStartMonth)
                {
                    case common.EnumMonths.MONTH_NOT_SELECTED:
                        break;
                    case common.EnumMonths.MONTH_JANUARY:
                        day_max = 31;
                        break;
                    case common.EnumMonths.MONTH_FEBRUARY:
                        day_max = 28;
                        break;
                    case common.EnumMonths.MONTH_MARCH:
                        day_max = 31;
                        break;
                    case common.EnumMonths.MONTH_APRIL:
                        day_max = 30;
                        break;
                    case common.EnumMonths.MONTH_MAY:
                        day_max = 31;
                        break;
                    case common.EnumMonths.MONTH_JUNE:
                        day_max = 30;
                        break;
                    case common.EnumMonths.MONTH_JULY:
                        day_max = 31;
                        break;
                    case common.EnumMonths.MONTH_AUGUST:
                        day_max = 31;
                        break;
                    case common.EnumMonths.MONTH_SEPTEMBER:
                        day_max = 30;
                        break;
                    case common.EnumMonths.MONTH_OCTOBER:
                        day_max = 31;
                        break;
                    case common.EnumMonths.MONTH_NOVEMBER:
                        day_max = 30;
                        break;
                    case common.EnumMonths.MONTH_DECEMBER:
                        day_max = 31;
                        break;
                }
                if (day < 1)
                {
                    var vf = new ValidationFailure(nameof(p.ScopePeriodStartMonthDay),
                        "Day number can't be less than 1");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
                else if (day > day_max)
                {
                    var vf = new ValidationFailure(nameof(p.ScopePeriodStartMonthDay),
                        $"Day number for selected month can't be greater than {day_max}");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
        }
    }
}
