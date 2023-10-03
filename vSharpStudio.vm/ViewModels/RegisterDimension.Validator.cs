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
    public partial class RegisterDimentionValidator
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
        public RegisterDimentionValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            //this.RuleFor(x => x.Name).Custom((name, cntx) =>
            //{
            //    if (string.IsNullOrEmpty(name))
            //        return;
            //    var p = (Property)cntx.InstanceToValidate;
            //    if (p.Parent == null)
            //        return;
            //    if (p.Parent is Property) // no need validate extended property 
            //        return;
            //    var pg = p.ParentGroupListProperties;
            //    GroupListProperties? pgs = null;
            //    var model = pg.Cfg.Model;
            //    if (pg.Parent is Catalog c)
            //    {
            //        Debug.Assert(c.Parent != null);
            //        var gc = (IGroupListCatalogs)c.Parent;
            //        if (!p.isSpecialItself)
            //            ValidateSpecialProperties(name, cntx, p, c);
            //        if (c.UseTree)
            //        {
            //            if (c.UseSeparateTreeForFolders)
            //            {
            //                if (name == "RefParent")
            //                {
            //                    var vf = new ValidationFailure(nameof(p.Name),
            //                        $"Catalog parameter 'Use Tree' is set to 'true' and 'Separate Folder' is set to 'true'. Property name 'RefParent' is reserved for auto generated property");
            //                    vf.Severity = Severity.Error;
            //                    cntx.AddFailure(vf);
            //                }
            //            }
            //            else
            //            {
            //                if (name == "RefTreeParent")
            //                {
            //                    var vf = new ValidationFailure(nameof(p.Name),
            //                        $"Catalog parameter 'Use Tree' is set to 'true' and 'Separate Folder' is set to 'false'. Property name 'RefTreeParent' is reserved for auto generated property");
            //                    vf.Severity = Severity.Error;
            //                    cntx.AddFailure(vf);
            //                }
            //                if (c.UseTree && !c.UseSeparateTreeForFolders)
            //                {
            //                    if (model.PropertyIsFolderName == name)
            //                    {
            //                        var vf = new ValidationFailure(nameof(p.Name),
            //                            $"Catalog parameter 'Explicit Folders' is set to 'true'. Property name '{model.PropertyIsFolderName}' is reserved for auto generated property");
            //                        vf.Severity = Severity.Error;
            //                        cntx.AddFailure(vf);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    else if (pg.Parent is CatalogFolder cf)
            //    {
            //        var cc = cf.ParentCatalog;
            //        if (!p.isSpecialItself)
            //            ValidateSpecialProperties(name, cntx, p, cf);
            //        if (cc.UseTree)
            //        {
            //            if (cc.UseSeparateTreeForFolders)
            //            {
            //                if (name == "RefTreeParent")
            //                {
            //                    var vf = new ValidationFailure(nameof(p.Name),
            //                        $"Catalog parameter 'Use Tree' is set to 'true' and 'Separate Folder' is set to 'true'. Property name 'RefTreeParent' is reserved for auto generated property");
            //                    vf.Severity = Severity.Error;
            //                    cntx.AddFailure(vf);
            //                }
            //            }
            //        }
            //    }
            //    else if (pg.Parent is Detail dd)
            //    {
            //        ValidateSpecialProperties(name, cntx, p, dd);
            //        if (name == "RefParent")
            //        {
            //            var vf = new ValidationFailure(nameof(p.Name),
            //                $"Property name 'RefParent' is reserved for auto generated property");
            //            vf.Severity = Severity.Error;
            //            cntx.AddFailure(vf);
            //        }
            //    }
            //    else if (pg.Parent is Document d)
            //    {
            //        ValidateSpecialProperties(name, cntx, p, d);
            //        pgs = d.ParentGroupListDocuments.ParentGroupDocuments.GroupSharedProperties;
            //    }
            //    else if (pg.Parent is GroupDocuments gd)
            //    {
            //        ValidateSpecialProperties(name, cntx, p, gd);
            //    }
            //    else
            //    {
            //        Debug.Assert(false);
            //        throw new NotImplementedException();
            //    }
            //    if (pgs != null)
            //    {
            //        foreach (var t in pgs.ListProperties.ToList())
            //        {
            //            if ((p.Guid != t.Guid) && (name == t.Name))
            //            {
            //                var vf = new ValidationFailure(nameof(p.Name),
            //                    $"Not unique property name '{name}'. Same as shared property");
            //                vf.Severity = Severity.Error;
            //                cntx.AddFailure(vf);
            //            }
            //        }
            //    }
            //    foreach (var t in pg.ListProperties.ToList())
            //    {
            //        if ((p.Guid != t.Guid) && (name == t.Name))
            //        {
            //            var vf = new ValidationFailure(nameof(p.Name),
            //                $"Not unique property name '{name}'");
            //            vf.Severity = Severity.Error;
            //            cntx.AddFailure(vf);
            //        }
            //    }
            //});

            //this.RuleFor(x => x.Length).Custom((name, cntx) =>
            //{
            //    var p = (Property)cntx.InstanceToValidate;
            //    if (p.DataTypeEnum == EnumDataType.STRING)
            //    {
            //        if (!string.IsNullOrWhiteSpace(p.MinLengthRequirement))
            //        {
            //            BigInteger vmin;
            //            if (BigInteger.TryParse(p.MinLengthRequirement, out vmin))
            //            {
            //                if (p.Length <= vmin)
            //                {
            //                    var vf = new ValidationFailure(nameof(p.Length),
            //                        $"Value less or equal than {nameof(p.MinLengthRequirement)} property value");
            //                    vf.Severity = Severity.Error;
            //                    cntx.AddFailure(vf);
            //                }
            //            }
            //        }
            //        if (!string.IsNullOrWhiteSpace(p.MaxLengthRequirement))
            //        {
            //            BigInteger vmax;
            //            if (BigInteger.TryParse(p.MaxLengthRequirement, out vmax))
            //            {
            //                if (p.Length <= vmax)
            //                {
            //                    var vf = new ValidationFailure(nameof(p.Length),
            //                        $"Value less or equal than {nameof(p.MaxLengthRequirement)} property value");
            //                    vf.Severity = Severity.Error;
            //                    cntx.AddFailure(vf);
            //                }
            //            }
            //        }
            //    }
            //    else if (p.DataTypeEnum == EnumDataType.NUMERICAL)
            //    {
            //        if (p.Length <= p.Accuracy)
            //        {
            //            var vf = new ValidationFailure(nameof(p.Length),
            //                $"Value less or equal than {nameof(p.Accuracy)} property value");
            //            vf.Severity = Severity.Error;
            //            cntx.AddFailure(vf);
            //        }
            //        if (p.Length > 38)
            //        {
            //            var vf = new ValidationFailure(nameof(p.Length),
            //                $"Value greater than 38 is not supported");
            //            vf.Severity = Severity.Error;
            //            cntx.AddFailure(vf);
            //        }
            //    }
            //});
            //this.RuleFor(x => x.Accuracy).Custom((name, cntx) =>
            //{
            //    var p = (Property)cntx.InstanceToValidate;
            //    if (p.DataTypeEnum != EnumDataType.NUMERICAL)
            //        return;
            //    if (p.IsPositive)
            //        return;
            //    if (p.Accuracy >= p.Length)
            //    {
            //        var vf = new ValidationFailure(nameof(p.Accuracy),
            //            $"Value greater or equal than {nameof(p.Length)} property value");
            //        vf.Severity = Severity.Error;
            //        cntx.AddFailure(vf);
            //    }
            //});

            //this.RuleFor(x => x.MinLengthRequirement).Custom((name, cntx) =>
            //{
            //    var p = (Property)cntx.InstanceToValidate;
            //    if (p.DataTypeEnum != EnumDataType.STRING)
            //        return;
            //    if (string.IsNullOrWhiteSpace(p.MinLengthRequirement))
            //        return;
            //    BigInteger vmin;
            //    if (!BigInteger.TryParse(p.MinLengthRequirement, out vmin))
            //    {
            //        var vf = new ValidationFailure(nameof(p.MinLengthRequirement),
            //            $"Can't parse to INTEGER");
            //        vf.Severity = Severity.Error;
            //        cntx.AddFailure(vf);
            //        return;
            //    }
            //    if (vmin >= p.Length)
            //    {
            //        var vf = new ValidationFailure(nameof(p.MinLengthRequirement),
            //            $"Value greater or equal than {nameof(p.Length)} property value");
            //        vf.Severity = Severity.Error;
            //        cntx.AddFailure(vf);
            //    }
            //    if (string.IsNullOrWhiteSpace(p.MaxLengthRequirement))
            //        return;
            //    BigInteger vmax;
            //    if (BigInteger.TryParse(p.MaxLengthRequirement, out vmax))
            //    {
            //        if (vmin >= vmax)
            //        {
            //            var vf = new ValidationFailure(nameof(p.MinLengthRequirement),
            //                $"Value greater or equal than {nameof(p.MaxLengthRequirement)} property value");
            //            vf.Severity = Severity.Error;
            //            cntx.AddFailure(vf);
            //        }
            //    }
            //});
            //this.RuleFor(x => x.MaxLengthRequirement).Custom((name, cntx) =>
            //{
            //    var p = (Property)cntx.InstanceToValidate;
            //    if (p.DataTypeEnum != EnumDataType.STRING)
            //        return;
            //    if (string.IsNullOrWhiteSpace(p.MaxLengthRequirement))
            //        return;
            //    BigInteger vmax;
            //    if (!BigInteger.TryParse(p.MaxLengthRequirement, out vmax))
            //    {
            //        var vf = new ValidationFailure(nameof(p.MaxLengthRequirement),
            //            $"Can't parse to INTEGER");
            //        vf.Severity = Severity.Error;
            //        cntx.AddFailure(vf);
            //        return;
            //    }
            //    if (vmax >= p.Length)
            //    {
            //        var vf = new ValidationFailure(nameof(p.MaxLengthRequirement),
            //            $"Value greater or equal than {nameof(p.Length)} property value");
            //        vf.Severity = Severity.Error;
            //        cntx.AddFailure(vf);
            //    }
            //    if (string.IsNullOrWhiteSpace(p.MinLengthRequirement))
            //        return;
            //    BigInteger vmin;
            //    if (BigInteger.TryParse(p.MinLengthRequirement, out vmin))
            //    {
            //        if (vmin >= vmax)
            //        {
            //            var vf = new ValidationFailure(nameof(p.MaxLengthRequirement),
            //                $"Value less or equal than {nameof(p.MinLengthRequirement)} property value");
            //            vf.Severity = Severity.Error;
            //            cntx.AddFailure(vf);
            //        }
            //    }
            //});
            //this.RuleFor(x => x.RangeValuesRequirementStr).Custom((x, cntx) =>
            //{
            //    var p = (Property)cntx.InstanceToValidate;
            //    ValidateRangeValuesRequirements(cntx, p);
            //});
        }
        //private static void ValidateSpecialProperties(string name, ValidationContext<Property> cntx, Property p, Catalog c)
        //{
        //    var model = c.ParentGroupListCatalogs.ParentModel;
        //    if (c.GetUseCodeProperty())
        //    {
        //        if (model.PropertyCodeName == name)
        //        {
        //            var vf = new ValidationFailure(nameof(p.Name),
        //                $"Catalog parameter 'UseCodeProperty' is set to 'true'. Property name '{model.PropertyCodeName}' is reserved for auto generated property");
        //            vf.Severity = Severity.Error;
        //            cntx.AddFailure(vf);
        //        }
        //    }
        //    if (c.GetUseNameProperty())
        //    {
        //        if (model.PropertyNameName == name)
        //        {
        //            var vf = new ValidationFailure(nameof(p.Name),
        //                $"Catalog parameter 'UseNameProperty' is set to 'true'. Property name '{model.PropertyNameName}' is reserved for auto generated property");
        //            vf.Severity = Severity.Error;
        //            cntx.AddFailure(vf);
        //        }
        //    }
        //    if (c.GetUseDescriptionProperty())
        //    {
        //        if (model.PropertyDescriptionName == name)
        //        {
        //            var vf = new ValidationFailure(nameof(p.Name),
        //                $"Catalog parameter 'UseDescriptionProperty' is set to 'true'. Property name '{model.PropertyDescriptionName}' is reserved for auto generated property");
        //            vf.Severity = Severity.Error;
        //            cntx.AddFailure(vf);
        //        }
        //    }
        //}
        //private static void ValidateSpecialProperties(string name, ValidationContext<Property> cntx, Property p, CatalogFolder cf)
        //{
        //    var model = cf.ParentCatalog.ParentGroupListCatalogs.ParentModel;
        //    if (cf.GetUseCodeProperty())
        //    {
        //        if (model.PropertyCodeName == name)
        //        {
        //            var vf = new ValidationFailure(nameof(p.Name),
        //                $"Catalog folder parameter 'UseCodeProperty' is set to 'true'. Property name '{model.PropertyCodeName}' is reserved for auto generated property");
        //            vf.Severity = Severity.Error;
        //            cntx.AddFailure(vf);
        //        }
        //    }
        //    if (cf.GetUseNameProperty())
        //    {
        //        if (model.PropertyNameName == name)
        //        {
        //            var vf = new ValidationFailure(nameof(p.Name),
        //                $"Catalog folder parameter 'UseNameProperty' is set to 'true'. Property name '{model.PropertyNameName}' is reserved for auto generated property");
        //            vf.Severity = Severity.Error;
        //            cntx.AddFailure(vf);
        //        }
        //    }
        //    if (cf.GetUseDescriptionProperty())
        //    {
        //        if (model.PropertyDescriptionName == name)
        //        {
        //            var vf = new ValidationFailure(nameof(p.Name),
        //                $"Catalog folder parameter 'UseDescriptionProperty' is set to 'true'. Property name '{model.PropertyDescriptionName}' is reserved for auto generated property");
        //            vf.Severity = Severity.Error;
        //            cntx.AddFailure(vf);
        //        }
        //    }
        //}
        //private static void ValidateSpecialProperties(string name, ValidationContext<Property> cntx, Property p, Detail dd)
        //{
        //    var model = dd.Cfg.Model;
        //    if (dd.GetUseCodeProperty())
        //    {
        //        if (model.PropertyCodeName == name)
        //        {
        //            var vf = new ValidationFailure(nameof(p.Name),
        //                $"Detail parameter 'UseCodeProperty' is set to 'true'. Property name '{model.PropertyCodeName}' is reserved for auto generated property");
        //            vf.Severity = Severity.Error;
        //            cntx.AddFailure(vf);
        //        }
        //    }
        //    if (dd.GetUseNameProperty())
        //    {
        //        if (model.PropertyNameName == name)
        //        {
        //            var vf = new ValidationFailure(nameof(p.Name),
        //                $"Detail parameter 'UseNameProperty' is set to 'true'. Property name '{model.PropertyNameName}' is reserved for auto generated property");
        //            vf.Severity = Severity.Error;
        //            cntx.AddFailure(vf);
        //        }
        //    }
        //    if (dd.GetUseDescriptionProperty())
        //    {
        //        if (model.PropertyDescriptionName == name)
        //        {
        //            var vf = new ValidationFailure(nameof(p.Name),
        //                $"Detail parameter 'UseDescriptionProperty' is set to 'true'. Property name '{model.PropertyDescriptionName}' is reserved for auto generated property");
        //            vf.Severity = Severity.Error;
        //            cntx.AddFailure(vf);
        //        }
        //    }
        //}
        //private static void ValidateSpecialProperties(string name, ValidationContext<Property> cntx, Property p, Document d)
        //{
        //    var model = d.ParentGroupListDocuments.ParentGroupDocuments.ParentModel;
        //    if (d.GetUseDocNumberProperty())
        //    {
        //        if (model.PropertyDocCodeName == name)
        //        {
        //            var vf = new ValidationFailure(nameof(p.Name),
        //                $"Document parameter 'UseDocCodeProperty' is set to 'true'. Property name '{model.PropertyDocCodeName}' is reserved for auto generated property");
        //            vf.Severity = Severity.Error;
        //            cntx.AddFailure(vf);
        //        }
        //    }
        //    if (d.GetUseDocDateProperty())
        //    {
        //        if (model.PropertyDocDateName == name)
        //        {
        //            var vf = new ValidationFailure(nameof(p.Name),
        //                $"Document parameter 'UseDocDateProperty' is set to 'true'. Property name '{model.PropertyDocDateName}' is reserved for auto generated property");
        //            vf.Severity = Severity.Error;
        //            cntx.AddFailure(vf);
        //        }
        //    }
        //}
        //private static void ValidateSpecialProperties(string name, ValidationContext<Property> cntx, Property p, GroupDocuments gd)
        //{
        //    var model = gd.ParentModel;
        //    if (gd.GetUseDocCodeProperty())
        //    {
        //        if (model.PropertyDocCodeName == name)
        //        {
        //            var vf = new ValidationFailure(nameof(p.Name),
        //                $"Document parameter 'UseDocCodeProperty' is set to 'true'. Property name '{model.PropertyDocCodeName}' is reserved for auto generated property");
        //            vf.Severity = Severity.Error;
        //            cntx.AddFailure(vf);
        //        }
        //    }
        //    if (gd.GetUseDocDateProperty())
        //    {
        //        if (model.PropertyDocDateName == name)
        //        {
        //            var vf = new ValidationFailure(nameof(p.Name),
        //                $"Document parameter 'UseDocDateProperty' is set to 'true'. Property name '{model.PropertyDocDateName}' is reserved for auto generated property");
        //            vf.Severity = Severity.Error;
        //            cntx.AddFailure(vf);
        //        }
        //    }
        //}
        //private bool ParsableToBigInteger(string val)
        //{
        //    if (string.IsNullOrWhiteSpace(val))
        //    {
        //        return true;
        //    }

        //    BigInteger v;
        //    return BigInteger.TryParse(val, out v);
        //}
    }
}
