using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ManyToManyGroupRelationsValidator
    {
        public ManyToManyGroupRelationsValidator()
        {
            //this.RuleFor(x => x.PrefixForDbTables).Must((o, prefix) =>
            //{
            //    if (!string.IsNullOrWhiteSpace(prefix))
            //        return true;
            //    if (o.ParentModel.IsUseGroupPrefix)
            //        return false;
            //    return true;
            //}).WithMessage("Prefix can't be empty if prefix usage is chosen for DB table names in the model");
            //this.RuleFor(x => x.MondayBeforeFirstDocDate).Must((o, monday) =>
            //{
            //    if (monday != null)
            //    {
            //        var dt = monday.ToDateTime();
            //        if (dt.DayOfWeek == DayOfWeek.Monday)
            //            return true;
            //    }
            //    return false;
            //}).WithMessage("Selected day has to be Monday");
        }
    }
}
