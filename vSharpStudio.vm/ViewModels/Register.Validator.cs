using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class RegisterValidator
    {
        //public static PropertyRangeValuesRequirements GetRangeValidation(Property p)
        //{
        //    return PropertyRangeValuesRequirements.GetRangeValidation(p);
        //}
        //private void ValidateRangeValuesRequirements(ValidationContext<Property> cntx, Property p)
        //{
        //    var req = PropertyValidator.GetRangeValidation(p);
        //    if (req.IsHasErrors)
        //    {
        //        foreach (var t in req.ListErrors)
        //        {
        //            var vf = new ValidationFailure(nameof(p.RangeValuesRequirementStr), t);
        //            vf.Severity = Severity.Error;
        //            cntx.AddFailure(vf);
        //        }
        //    }
        //}
        public RegisterValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.TableDimensionPropertyMoneyAccumulatorAccuracy).Custom((acc, cntx) =>
            {
                var p = (Register)cntx.InstanceToValidate;
                if (!p.UseMoneyAccumulator)
                    return;
                if (acc >= p.TableDimensionPropertyMoneyAccumulatorLength)
                {
                    var vf = new ValidationFailure(nameof(p.TableDimensionPropertyMoneyAccumulatorAccuracy),
                        $"Value greater or equal than {nameof(p.TableDimensionPropertyMoneyAccumulatorLength)} property value");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.TableDimensionPropertyMoneyAccumulatorLength).Custom((len, cntx) =>
            {
                var p = (Register)cntx.InstanceToValidate;
                if (!p.UseMoneyAccumulator)
                    return;
                if (len <= p.TableDimensionPropertyMoneyAccumulatorAccuracy)
                {
                    var vf = new ValidationFailure(nameof(p.TableDimensionPropertyMoneyAccumulatorLength),
                        $"Value less or equal than {nameof(p.TableDimensionPropertyMoneyAccumulatorAccuracy)} property value");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
                if (len > 28)
                {
                    var vf = new ValidationFailure(nameof(p.TableDimensionPropertyMoneyAccumulatorLength),
                        $"Value greater than 28 is not supported");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.TableDimensionPropertyQtyAccumulatorAccuracy).Custom((acc, cntx) =>
            {
                var p = (Register)cntx.InstanceToValidate;
                if (!p.UseQtyAccumulator)
                    return;
                if (acc >= p.TableDimensionPropertyQtyAccumulatorLength)
                {
                    var vf = new ValidationFailure(nameof(p.TableDimensionPropertyQtyAccumulatorAccuracy),
                        $"Value greater or equal than {nameof(p.TableDimensionPropertyQtyAccumulatorLength)} property value");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.TableDimensionPropertyQtyAccumulatorLength).Custom((len, cntx) =>
            {
                var p = (Register)cntx.InstanceToValidate;
                if (!p.UseQtyAccumulator)
                    return;
                if (len <= p.TableDimensionPropertyQtyAccumulatorAccuracy)
                {
                    var vf = new ValidationFailure(nameof(p.TableDimensionPropertyQtyAccumulatorLength),
                        $"Value less or equal than {nameof(p.TableDimensionPropertyQtyAccumulatorAccuracy)} property value");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
                if (len > 28)
                {
                    var vf = new ValidationFailure(nameof(p.TableDimensionPropertyQtyAccumulatorLength),
                        $"Value greater than 28 is not supported");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.UseQtyAccumulator).Custom((use, cntx) =>
            {
                var p = (Register)cntx.InstanceToValidate;
                if (!use && !p.UseMoneyAccumulator)
                {
                    var vf = new ValidationFailure(nameof(p.UseQtyAccumulator),
                        $"At least one accumulator type has to be selected");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.UseMoneyAccumulator).Custom((use, cntx) =>
            {
                var p = (Register)cntx.InstanceToValidate;
                if (!use && !p.UseQtyAccumulator)
                {
                    var vf = new ValidationFailure(nameof(p.UseMoneyAccumulator),
                        $"At least one accumulator type has to be selected");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.GroupRegisterDimensions.ListDimensions).Must((lst) =>
            {
                if (lst.Count == 0)
                    return false;
                return true;
            }).WithMessage("Register dimentions are not selected");
            this.RuleFor(x => x.ListDocGuids).Must((lst) =>
            {
                if (lst.Count == 0)
                    return false;
                return true;
            }).WithMessage("List of Document types for Register is empty");
        }
    }
}
