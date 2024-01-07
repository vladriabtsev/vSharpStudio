using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupDocumentsValidator
    {
        public GroupDocumentsValidator()
        {
            this.RuleFor(x => x.PrefixForCompositionNames).Must((o, prefix) =>
            {
                if (!o.ParentModel.IsUseNameComposition)
                    return true;
                if (!string.IsNullOrWhiteSpace(prefix))
                    return true;
                return false;
            }).WithMessage("Prefix can't be empty if name composition usage is chosen for composite names in the model");
            this.RuleFor(x => x.MondayBeforeFirstDocDate).Must((o, monday) =>
            {
                if (monday != null)
                {
                    var dt = monday.ToDateTime();
                    if (dt.DayOfWeek == DayOfWeek.Monday)
                        return true;
                }
                return false;
            }).WithMessage("Selected day has to be Monday");
            this.RuleFor(x => x.TimelineName).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.TimelineName).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.TimelineName).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
        }
    }
}
