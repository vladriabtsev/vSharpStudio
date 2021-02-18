using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class PropertyValidator
    {
        public PropertyValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Custom((name, cntx) =>
            {
                var p = (Property)cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                var pg = (GroupListProperties)p.Parent;
                //if (pg.Parent == null)
                //    return;
                if (pg.Parent is Catalog)
                {
                    var c = (Catalog)pg.Parent;
                    var gc = (IGroupListCatalogs)c.Parent;
                    if (c.UseCodeProperty)
                    {
                        if (gc.PropertyCode == name)
                        {
                            var vf = new ValidationFailure(nameof(p.Name),
                                $"Catalog parameter 'UseCodeProperty' is set to 'true'. Property name '{gc.PropertyCode}' can't be used here");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
                    }
                    if (c.UseNameProperty)
                    {
                        if (gc.PropertyName == name)
                        {
                            var vf = new ValidationFailure(nameof(p.Name),
                                $"Catalog parameter 'UseNameProperty' is set to 'true'. Property name '{gc.PropertyName}' can't be used here");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
                    }
                    if (c.UseDescriptionProperty)
                    {
                        if (gc.PropertyDescription == name)
                        {
                            var vf = new ValidationFailure(nameof(p.Name),
                                $"Catalog parameter 'UseDescriptionProperty' is set to 'true'. Property name '{gc.PropertyDescription}' can't be used here");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
                    }
                }
                foreach (var t in pg.ListProperties)
                {
                    if ((p.Guid != t.Guid) && (name == t.Name))
                    {
                        var vf = new ValidationFailure(nameof(p.Name),
                            $"Not unique property name '{name}'");
                        vf.Severity = Severity.Error;
                        cntx.AddFailure(vf);
                    }
                }
            });
            //TODO warning validation for potensial data loss when length of Code, Name, Description is decreased
            //TODO warning validation for potensial data loss when data type change
            //TODO warning validation for potensial data. Validation against  previous current version and previous stable version.
        }
    }
}
