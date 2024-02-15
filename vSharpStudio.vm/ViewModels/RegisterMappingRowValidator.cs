using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public class RegisterMappingRowValidator : ValidatorBase<RegisterMappingRow, RegisterMappingRowValidator>
    {
        public RegisterMappingRowValidator()
        {
            this.RuleFor(x => x.Selected).Custom((sel, cntx) =>
            {
                if (sel == null)
                    return;
                var row = (RegisterMappingRow)cntx.InstanceToValidate;
                var nmap = 0;
                foreach (var t in row.Reg.ListMappings)
                {
                    if (t == row || t.Selected == null)
                            continue;
                    if (t.Selected.Guid == sel.Guid)
                        nmap++;
                    //switch (row.Reg.RegisterType)
                    //{
                    //    case common.EnumRegisterType.BALANCE_AND_TURNOVER:
                    //    case common.EnumRegisterType.BALANCE:
                    //    case common.EnumRegisterType.TURNOVER:
                    //        if (row.Reg.UseMoneyAccumulator)
                    //        {
                    //            if (rguid == row.Reg.TableTurnoverPropertyMoneyAccumulatorGuid)
                    //                nmap++;
                    //        }
                    //        if (row.Reg.UseQtyAccumulator)
                    //        {
                    //            if (rguid == row.Reg.TableTurnoverPropertyQtyAccumulatorGuid)
                    //                nmap++;
                    //        }
                    //        break;
                    //    default:
                    //        throw new InvalidOperationException();
                    //}
                }
                if (nmap > 0)
                {
                    var vf = new ValidationFailure(nameof(row.Selected),
                        $"Selected catalog property '{sel.Name}' is used more than ones for mapping");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });


            //this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            //this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            //this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            //this.RuleFor(x => x.Dimension.TableTurnoverPropertyMoneyAccumulatorAccuracy).Custom((acc, cntx) =>
            //{
            //    var p = (RegisterMappingRow)cntx.InstanceToValidate;
            //    if (!p.UseMoneyAccumulator)
            //        return;
            //    if (acc >= p.TableTurnoverPropertyMoneyAccumulatorLength)
            //    {
            //        var vf = new ValidationFailure(nameof(p.TableTurnoverPropertyMoneyAccumulatorAccuracy),
            //            $"Value greater or equal than {nameof(p.TableTurnoverPropertyMoneyAccumulatorLength)} property value");
            //        vf.Severity = Severity.Error;
            //        cntx.AddFailure(vf);
            //    }
            //});
        }
    }
}
