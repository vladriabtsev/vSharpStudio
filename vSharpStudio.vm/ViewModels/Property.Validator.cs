using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                    var model = (IModel)gc.Parent;
                    if (model.DbSettings.PKeyName == name)
                    {
                        var vf = new ValidationFailure(nameof(p.Name),
                            $"Model DbSettings parameter 'PKeyName' is set to '{name}'. This Property name is reserved for auto generated ID property");
                        vf.Severity = Severity.Error;
                        cntx.AddFailure(vf);
                    }
                    ValidateSpecialProperties(name, cntx, p, c, gc);
                    if (c.UseTree && !c.UseSeparatePropertiesForGroups && c.UseFolderTypeExplicitly)
                    {
                        if (gc.PropertyIsFolderName == name)
                        {
                            var vf = new ValidationFailure(nameof(p.Name),
                                $"Catalog parameter 'UseFolderTypeExplicitly' is set to 'true'. Property name '{gc.PropertyIsFolderName}' is reserved for auto generated property");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
                        if (gc.PropertyIsOpenName == name)
                        {
                            var vf = new ValidationFailure(nameof(p.Name),
                                $"Catalog parameter 'UseFolderTypeExplicitly' is set to 'true'. Property name '{gc.PropertyIsOpenName}' is reserved for auto generated property");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
                    }
                }
                else if (pg.Parent is CatalogFolder)
                {
                    var c = (Catalog)pg.Parent.Parent;
                    var gc = (IGroupListCatalogs)c.Parent;
                    var cfg = pg.GetConfig();
                    if (cfg.Model.DbSettings.PKeyName == name)
                    {
                        var vf = new ValidationFailure(nameof(p.Name),
                            $"Model DbSettings parameter 'PKeyName' is set to '{name}'. This Property name is reserved for auto generated ID property");
                        vf.Severity = Severity.Error;
                        cntx.AddFailure(vf);
                    }
                    ValidateSpecialProperties(name, cntx, p, c, gc);
                    if (c.UseTree && !c.UseSeparatePropertiesForGroups && c.UseFolderTypeExplicitly)
                    {
                        if (gc.PropertyIsOpenName == name)
                        {
                            var vf = new ValidationFailure(nameof(p.Name),
                                $"Catalog parameter 'UseFolderTypeExplicitly' is set to 'true'. Property name '{gc.PropertyIsOpenName}' is reserved for auto generated property");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
                    }
                }
                else if (pg.Parent is PropertiesTab)
                {
                    var cfg = pg.GetConfig();
                    if (cfg.Model.DbSettings.PKeyName == name)
                    {
                        var vf = new ValidationFailure(nameof(p.Name),
                            $"Model DbSettings parameter 'PKeyName' is set to '{name}'. This Property name is reserved for auto generated ID property");
                        vf.Severity = Severity.Error;
                        cntx.AddFailure(vf);
                    }
                }
                else if (pg.Parent is Document)
                {
                }
                else if (pg.Parent is GroupDocuments)
                {
                }
                else
                {
                    Debug.Assert(false);
                    throw new NotImplementedException();
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
            //TODO warning validation for potencial data loss when length of Code, Name, Description is decreased
            //TODO warning validation for potencial data loss when data type change
            //TODO warning validation for potencial data. Validation against  previous current version and previous stable version.
        }

        private static void ValidateSpecialProperties(string name, ValidationContext<Property> cntx, Property p, Catalog c, IGroupListCatalogs gc)
        {
            if (c.UseCodeProperty)
            {
                if (gc.PropertyCodeName == name)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Catalog parameter 'UseCodeProperty' is set to 'true'. Property name '{gc.PropertyCodeName}' is reserved for auto generated property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
            if (c.UseNameProperty)
            {
                if (gc.PropertyNameName == name)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Catalog parameter 'UseNameProperty' is set to 'true'. Property name '{gc.PropertyNameName}' is reserved for auto generated property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
            if (c.UseDescriptionProperty)
            {
                if (gc.PropertyDescriptionName == name)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Catalog parameter 'UseDescriptionProperty' is set to 'true'. Property name '{gc.PropertyDescriptionName}' is reserved for auto generated property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
        }
    }
}
