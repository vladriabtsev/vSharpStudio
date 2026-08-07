using FluentValidation;
using FluentValidation.Results;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class DetailValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(DetailValidator));
        public DetailValidator()
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
                var pg = (GroupListDetails)p.Parent;
                //if (pg.Parent == null)
                //    return;
                if (pg.Parent is Catalog)
                {
                    var c = (Catalog)pg.Parent;
                    if (c.UseTree && c.UseSeparateTreeForFolders)
                    {
                        if (name == c.Folder.Name)
                        {
                            var vf = Common.CreateValidationFailure(nameof(p.Name),
                                $"Properties tab name can't be same as catalog folder name '{name}'");
                            cntx.AddFailure(vf);
                        }
                    }
                }
                foreach (var t in pg.ListDetails)
                {
                    if ((p.Guid != t.Guid) && (name == t.Name))
                    {
                        var vf = Common.CreateValidationFailure(nameof(p.Name),
                            $"Not unique properties tab name '{name}'");
                        cntx.AddFailure(vf);
                    }
                }
            });
            #region Auto UI
            this.RuleFor(x => x.IsStopTabControl).Custom((isStopTabControl, cntx) =>
            {
                if (!isStopTabControl)
                    return;
                var p = cntx.InstanceToValidate;
                var grp = p.ParentGroupListDetails;
                var indx = grp.ListDetails.IndexOf(p);
                if (indx == 0)
                {
                    var vf = Common.CreateValidationFailure(nameof(p.IsStopTabControl),
                        $"Can't stop using tab control when it is first field");
                    cntx.AddFailure(vf);
                    return;
                }
                var is_tab = false;
                for (int i = indx - 1; i >= 0; i--)
                {
                    p = grp.ListDetails[i];
                    if (p.IsStartNewTabControl)
                    {
                        is_tab = true;
                        break;
                    }
                }
                if (!is_tab)
                {
                    var vf = Common.CreateValidationFailure(nameof(p.IsStopTabControl),
                        $"Can't stop using tab control when there are no current tab control");
                    cntx.AddFailure(vf);
                    return;
                }
            });
            this.RuleFor(x => x.IsStartNewTabControl).Custom((isStartNewTabControl, cntx) =>
            {
                if (!isStartNewTabControl)
                    return;
                var p = cntx.InstanceToValidate;
                if (p.IsStopTabControl)
                {
                    var vf = Common.CreateValidationFailure(nameof(p.IsStartNewTabControl),
                        $"Can't start new tab control and stop at the same time");
                    cntx.AddFailure(vf);
                }
            });
            #endregion Auto UI
        }
    }
}
