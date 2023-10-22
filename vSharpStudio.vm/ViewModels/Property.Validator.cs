using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using CommunityToolkit.Diagnostics;
using System.Windows.Documents;
using FluentValidation;
using FluentValidation.Results;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class PropertyValidator
    {
        public static PropertyRangeValuesRequirements GetRangeValidation(Property p)
        {
            return PropertyRangeValuesRequirements.GetRangeValidation(p);
        }
        private void ValidateRangeValuesRequirements(ValidationContext<Property> cntx, Property p)
        {
            var req = PropertyValidator.GetRangeValidation(p);
            if (req.IsHasErrors)
            {
                foreach (var t in req.ListErrors)
                {
                    var vf = new ValidationFailure(nameof(p.RangeValuesRequirementStr), t);
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
        }
        public PropertyValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Custom((name, cntx) =>
            {
                if (string.IsNullOrEmpty(name))
                    return;
                var p = (Property)cntx.InstanceToValidate;
                //if (p.isSpecialItself) 
                //    return;
                if (p.Parent == null)
                    return;
                //if (p.Parent is Property) // no need validate extended property 
                //    return;
                if (!(p.Parent is GroupListProperties)) // no need validate
                    return;
                var pg = p.ParentGroupListProperties;
                GroupListProperties? pgs = null;
                var model = pg.Cfg.Model;
                if (name == model.PKeyName)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Model is is configured to use {model.PKeyName} as primary key name. Property name {model.PKeyName} is reserved for primary key property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
                if (name == model.RecordVersionFieldName)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Model is is configured to use {model.RecordVersionFieldName} as record version name. Property name {model.RecordVersionFieldName} is reserved for record version property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
                if (pg.Parent is Catalog c)
                {
                    Debug.Assert(c.Parent != null);
                    var gc = (IGroupListCatalogs)c.Parent;
                    if (!p.isSpecialItself)
                        ValidateSpecialProperties(name, cntx, p, c);
                    if (c.UseTree)
                    {
                        if (c.UseSeparateTreeForFolders)
                        {
                            if (name == "RefParent")
                            {
                                var vf = new ValidationFailure(nameof(p.Name),
                                    $"Catalog parameter 'Use Tree' is set to 'true' and 'Separate Folder' is set to 'true'. Property name 'RefParent' is reserved for auto generated property");
                                vf.Severity = Severity.Error;
                                cntx.AddFailure(vf);
                            }
                        }
                        else
                        {
                            if (name == "RefTreeParent")
                            {
                                var vf = new ValidationFailure(nameof(p.Name),
                                    $"Catalog parameter 'Use Tree' is set to 'true' and 'Separate Folder' is set to 'false'. Property name 'RefTreeParent' is reserved for auto generated property");
                                vf.Severity = Severity.Error;
                                cntx.AddFailure(vf);
                            }
                            if (c.UseTree && !c.UseSeparateTreeForFolders)
                            {
                                if (model.PropertyIsFolderName == name)
                                {
                                    var vf = new ValidationFailure(nameof(p.Name),
                                        $"Catalog parameter 'Explicit Folders' is set to 'true'. Property name '{model.PropertyIsFolderName}' is reserved for auto generated property");
                                    vf.Severity = Severity.Error;
                                    cntx.AddFailure(vf);
                                }
                            }
                        }
                    }
                    if (p.DataType.DataTypeEnum == EnumDataType.CATALOG)
                    {
                        if (!string.IsNullOrWhiteSpace(p.DataType.ObjectGuid) && p.DataType.RelationType == EnumRelationType.MANY_TO_MANY)
                        {
                            var ttt = p.DataType.ObjectGuid;
                            var c2 = (Catalog)model.Cfg.DicNodes[ttt];
                            bool found = false;
                            foreach (var t in c2.GroupProperties.ListProperties)
                            {
                                if (t.DataType.DataTypeEnum == EnumDataType.CATALOG)
                                {
                                    if (t.DataType.ObjectGuid == c.Guid)
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                                else if (t.DataType.DataTypeEnum == EnumDataType.CATALOGS)
                                {
                                    foreach (var tt in t.DataType.ListObjectGuids)
                                    {
                                        if (tt == c.Guid)
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                }
                                if (found)
                                    break;
                            }
                            if (!found)
                            {
                                var vf = new ValidationFailure(nameof(p.Name),
                                    $"Relation type with catalog '{c2.Name}' is set to 'Many to Many', but catalog '{c2.Name}' doesn't have 'Many to Many' property pointing to this '{c.Name}' catalog");
                                vf.Severity = Severity.Error;
                                cntx.AddFailure(vf);
                            }
                        }
                    }
                    else if (p.DataType.DataTypeEnum == EnumDataType.CATALOGS)
                    {
                        if (!string.IsNullOrWhiteSpace(p.DataType.ObjectGuid) && p.DataType.RelationType == EnumRelationType.MANY_TO_MANY)
                        {
                            foreach (var ttt in p.DataType.ListObjectGuids)
                            {
                                var c2 = (Catalog)model.Cfg.DicNodes[ttt];
                                bool found = false;
                                foreach (var t in c2.GroupProperties.ListProperties)
                                {
                                    if (t.DataType.DataTypeEnum == EnumDataType.CATALOG)
                                    {
                                        if (t.DataType.ObjectGuid == c.Guid)
                                        {
                                            found = true;
                                            break;
                                        }
                                    }
                                    else if (t.DataType.DataTypeEnum == EnumDataType.CATALOGS)
                                    {
                                        foreach (var tt in t.DataType.ListObjectGuids)
                                        {
                                            if (tt == c.Guid)
                                            {
                                                found = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (found)
                                        break;
                                }
                                if (!found)
                                {
                                    var vf = new ValidationFailure(nameof(p.Name),
                                        $"Relation type with catalog '{c2.Name}' is set to 'Many to Many', but catalog '{c2.Name}' doesn't have 'Many to Many' property pointing to this '{c.Name}' catalog");
                                    vf.Severity = Severity.Error;
                                    cntx.AddFailure(vf);
                                }
                            }
                        }
                    }
                    else if (p.DataType.DataTypeEnum == EnumDataType.DOCUMENT || p.DataType.DataTypeEnum == EnumDataType.DOCUMENTS)
                    {
                        ThrowHelper.ThrowInvalidOperationException();
                    }
                }
                else if (pg.Parent is CatalogFolder cf)
                {
                    var cc = cf.ParentCatalog;
                    if (!p.isSpecialItself)
                        ValidateSpecialProperties(name, cntx, p, cf);
                    if (cc.UseTree)
                    {
                        if (cc.UseSeparateTreeForFolders)
                        {
                            if (name == "RefTreeParent")
                            {
                                var vf = new ValidationFailure(nameof(p.Name),
                                    $"Catalog parameter 'Use Tree' is set to 'true' and 'Separate Folder' is set to 'true'. Property name 'RefTreeParent' is reserved for auto generated property");
                                vf.Severity = Severity.Error;
                                cntx.AddFailure(vf);
                            }
                        }
                    }
                }
                else if (pg.Parent is Detail dd)
                {
                    ValidateSpecialProperties(name, cntx, p, dd);
                    if (name == "RefParent")
                    {
                        var vf = new ValidationFailure(nameof(p.Name),
                            $"Property name 'RefParent' is reserved for auto generated property");
                        vf.Severity = Severity.Error;
                        cntx.AddFailure(vf);
                    }
                }
                else if (pg.Parent is Document d)
                {
                    ValidateSpecialProperties(name, cntx, p, d);
                    pgs = d.ParentGroupListDocuments.ParentGroupDocuments.GroupSharedProperties;
                }
                else if (pg.Parent is GroupDocuments gd)
                {
                    ValidateSpecialProperties(name, cntx, p, gd);
                }
                else if (pg.Parent is Register r)
                {
                    ValidateSpecialProperties(name, cntx, p, r);
                }
                else
                {
                    Debug.Assert(false);
                    throw new NotImplementedException();
                }
                if (pgs != null)
                {
                    foreach (var t in pgs.ListProperties.ToList())
                    {
                        if ((p.Guid != t.Guid) && (name == t.Name))
                        {
                            var vf = new ValidationFailure(nameof(p.Name),
                                $"Not unique property name '{name}'. Same as shared property");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
                    }
                }
                foreach (var t in pg.ListProperties.ToList())
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

            this.RuleFor(x => x.Length).Custom((name, cntx) =>
            {
                var p = (Property)cntx.InstanceToValidate;
                if (p.DataTypeEnum == EnumDataType.STRING)
                {
                    if (!string.IsNullOrWhiteSpace(p.MinLengthRequirement))
                    {
                        BigInteger vmin;
                        if (BigInteger.TryParse(p.MinLengthRequirement, out vmin))
                        {
                            if (p.Length <= vmin)
                            {
                                var vf = new ValidationFailure(nameof(p.Length),
                                    $"Value less or equal than {nameof(p.MinLengthRequirement)} property value");
                                vf.Severity = Severity.Error;
                                cntx.AddFailure(vf);
                            }
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(p.MaxLengthRequirement))
                    {
                        BigInteger vmax;
                        if (BigInteger.TryParse(p.MaxLengthRequirement, out vmax))
                        {
                            if (p.Length <= vmax)
                            {
                                var vf = new ValidationFailure(nameof(p.Length),
                                    $"Value less or equal than {nameof(p.MaxLengthRequirement)} property value");
                                vf.Severity = Severity.Error;
                                cntx.AddFailure(vf);
                            }
                        }
                    }
                }
                else if (p.DataTypeEnum == EnumDataType.NUMERICAL)
                {
                    if (p.Length <= p.Accuracy)
                    {
                        var vf = new ValidationFailure(nameof(p.Length),
                            $"Value less or equal than {nameof(p.Accuracy)} property value");
                        vf.Severity = Severity.Error;
                        cntx.AddFailure(vf);
                    }
                    if (p.Length > 28)
                    {
                        var vf = new ValidationFailure(nameof(p.Length),
                            $"Value greater than 28 is not supported");
                        vf.Severity = Severity.Error;
                        cntx.AddFailure(vf);
                    }
                }
            });
            this.RuleFor(x => x.Accuracy).Custom((name, cntx) =>
            {
                var p = (Property)cntx.InstanceToValidate;
                if (p.DataTypeEnum != EnumDataType.NUMERICAL)
                    return;
                if (p.IsPositive)
                    return;
                if (p.Accuracy >= p.Length)
                {
                    var vf = new ValidationFailure(nameof(p.Accuracy),
                        $"Value greater or equal than {nameof(p.Length)} property value");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });

            this.RuleFor(x => x.MinLengthRequirement).Custom((name, cntx) =>
            {
                var p = (Property)cntx.InstanceToValidate;
                if (p.DataTypeEnum != EnumDataType.STRING)
                    return;
                if (string.IsNullOrWhiteSpace(p.MinLengthRequirement))
                    return;
                BigInteger vmin;
                if (!BigInteger.TryParse(p.MinLengthRequirement, out vmin))
                {
                    var vf = new ValidationFailure(nameof(p.MinLengthRequirement),
                        $"Can't parse to INTEGER");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                    return;
                }
                if (vmin >= p.Length)
                {
                    var vf = new ValidationFailure(nameof(p.MinLengthRequirement),
                        $"Value greater or equal than {nameof(p.Length)} property value");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
                if (string.IsNullOrWhiteSpace(p.MaxLengthRequirement))
                    return;
                BigInteger vmax;
                if (BigInteger.TryParse(p.MaxLengthRequirement, out vmax))
                {
                    if (vmin >= vmax)
                    {
                        var vf = new ValidationFailure(nameof(p.MinLengthRequirement),
                            $"Value greater or equal than {nameof(p.MaxLengthRequirement)} property value");
                        vf.Severity = Severity.Error;
                        cntx.AddFailure(vf);
                    }
                }
            });
            this.RuleFor(x => x.MaxLengthRequirement).Custom((name, cntx) =>
            {
                var p = (Property)cntx.InstanceToValidate;
                if (p.DataTypeEnum != EnumDataType.STRING)
                    return;
                if (string.IsNullOrWhiteSpace(p.MaxLengthRequirement))
                    return;
                BigInteger vmax;
                if (!BigInteger.TryParse(p.MaxLengthRequirement, out vmax))
                {
                    var vf = new ValidationFailure(nameof(p.MaxLengthRequirement),
                        $"Can't parse to INTEGER");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                    return;
                }
                if (vmax >= p.Length)
                {
                    var vf = new ValidationFailure(nameof(p.MaxLengthRequirement),
                        $"Value greater or equal than {nameof(p.Length)} property value");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
                if (string.IsNullOrWhiteSpace(p.MinLengthRequirement))
                    return;
                BigInteger vmin;
                if (BigInteger.TryParse(p.MinLengthRequirement, out vmin))
                {
                    if (vmin >= vmax)
                    {
                        var vf = new ValidationFailure(nameof(p.MaxLengthRequirement),
                            $"Value less or equal than {nameof(p.MinLengthRequirement)} property value");
                        vf.Severity = Severity.Error;
                        cntx.AddFailure(vf);
                    }
                }
            });
            this.RuleFor(x => x.RangeValuesRequirementStr).Custom((x, cntx) =>
            {
                var p = (Property)cntx.InstanceToValidate;
                ValidateRangeValuesRequirements(cntx, p);
            });

            this.RuleFor(x => x.DefaultValue).Custom((x, cntx) =>
            {
                if (string.IsNullOrWhiteSpace(x))
                    return;
                var val = x.Trim();
                var p = (Property)cntx.InstanceToValidate;
                switch (p.DataType.DataTypeEnum)
                {
                    case EnumDataType.BOOL:
                        if (!bool.TryParse(val, out bool v))
                        {
                            var vf = new ValidationFailure(nameof(p.DefaultValue),
                                $"Can't parse by bool.Parse() default property value");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
                        break;
                    case EnumDataType.CHAR:
                        if (val[0] != '\'')
                        {
                            var vf = new ValidationFailure(nameof(p.DefaultValue),
                                $"Char value has to start with \' character");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
                        if (val[val.Length - 1] != '\'')
                        {
                            var vf = new ValidationFailure(nameof(p.DefaultValue),
                                $"Char value has to finish with \' character");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
                        if (val.Length != 3)
                        {
                            var vf = new ValidationFailure(nameof(p.DefaultValue),
                                $"Char value has to follow next format 'a'");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
                        break;
                    case EnumDataType.DATE:
#if !NET6_0
                        if (!DateTime.TryParse(val, out DateTime vd))
                        {
                            var vf = new ValidationFailure(nameof(p.DefaultValue),
                                $"Can't parse by DateOnly.Parse() default property value");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
#else
                        if (!DateOnly.TryParse(val, out DateOnly vd))
                        {
                            var vf = new ValidationFailure(nameof(p.DefaultValue),
                                $"Can't parse by DateOnly.Parse() default property value");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
#endif
                        break;
                    case EnumDataType.DATETIMELOCAL:
                    case EnumDataType.DATETIMEUTC:
                        //case EnumDataType.DATETIMEZ:
                        if (!DateTime.TryParse(val, out DateTime vdt))
                        {
                            var vf = new ValidationFailure(nameof(p.DefaultValue),
                                $"Can't parse by DateTime.Parse() default property value");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
                        break;
                    case EnumDataType.TIME:
#if !NET6_0
                        if (!DateTime.TryParse(val, out DateTime vt))
                        {
                            var vf = new ValidationFailure(nameof(p.DefaultValue),
                                $"Can't parse by DateOnly.Parse() default property value");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
#else
                        if (!TimeOnly.TryParse(val, out TimeOnly vt))
                        {
                            var vf = new ValidationFailure(nameof(p.DefaultValue),
                                $"Can't parse by TimeOnly.Parse() default property value");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
#endif
                        break;
                    case EnumDataType.STRING:
                        if (val[0] != '\"')
                        {
                            var vf = new ValidationFailure(nameof(p.DefaultValue),
                                $"String value has to start with \" character");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
                        if (val[val.Length - 1] != '\"')
                        {
                            var vf = new ValidationFailure(nameof(p.DefaultValue),
                                $"String value has to finish with \" character");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
                        break;
                    default:
                        break;
                }
            });

            #region ObjectGuid
            this.RuleFor(p => p.ObjectGuid).Must((p, y) =>
            {
                if (p.DataTypeEnum == EnumDataType.ENUMERATION && string.IsNullOrWhiteSpace(p.ObjectGuid))
                {
                    return false;
                }

                return true;
            }).WithMessage(Config.ValidationMessages.TYPE_EMPTY_ENUMERATION);
            this.RuleFor(p => p.ObjectGuid).Must((p, y) =>
            {
                if (p.DataTypeEnum == EnumDataType.CATALOG && string.IsNullOrWhiteSpace(p.ObjectGuid))
                {
                    return false;
                }

                return true;
            }).WithMessage(Config.ValidationMessages.TYPE_EMPTY_CATALOG);
            this.RuleFor(p => p.ObjectGuid).Must((p, y) =>
            {
                if (p.DataTypeEnum == EnumDataType.DOCUMENT && string.IsNullOrWhiteSpace(p.ObjectGuid))
                {
                    return false;
                }

                return true;
            }).WithMessage(Config.ValidationMessages.TYPE_EMPTY_DOCUMENT);
            this.RuleFor(p => p.ObjectGuid).Must((p, y) =>
            {
                if (p.DataTypeEnum != EnumDataType.ENUMERATION)
                {
                    return true;
                }
                //IParent n = (IParent)p;
                //while (true)
                //{
                //    if (n.Parent != null)
                //    {
                //        n = n.Parent;
                //        if (n is Config)
                //        {
                //            break;
                //        }
                //    }
                //    else
                //    {
                //        return true;
                //    }
                //}
                foreach (var t in p.Cfg.Model.GroupEnumerations.ListEnumerations)
                {
                    if (t.Guid == y)
                    {
                        return true;
                    }
                }
                return false;
            }).WithMessage(Config.ValidationMessages.TYPE_OBJECT_IS_NOT_FOUND);
            this.RuleFor(p => p.ObjectGuid).Must((p, y) =>
            {
                if (p.DataTypeEnum != EnumDataType.CATALOG)
                {
                    return true;
                }
                //IParent n = (IParent)p;
                //while (true)
                //{
                //    if (n.Parent != null)
                //    {
                //        n = n.Parent;
                //        if (n is Config)
                //        {
                //            break;
                //        }
                //    }
                //    else
                //    {
                //        return true;
                //    }
                //}
                foreach (var t in p.Cfg.Model.GroupCatalogs.ListCatalogs)
                {
                    if (t.Guid == y)
                    {
                        return true;
                    }
                }
                return false;
            }).WithMessage(Config.ValidationMessages.TYPE_OBJECT_IS_NOT_FOUND);
            this.RuleFor(p => p.ObjectGuid).Must((p, y) =>
            {
                if (p.DataTypeEnum != EnumDataType.DOCUMENT)
                {
                    return true;
                }
                //IParent n = (IParent)p;
                //while (true)
                //{
                //    if (n.Parent != null)
                //    {
                //        n = n.Parent;
                //        if (n is Config)
                //        {
                //            break;
                //        }
                //    }
                //    else
                //    {
                //        return true;
                //    }
                //}
                foreach (var t in p.Cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments)
                {
                    if (t.Guid == y)
                    {
                        return true;
                    }
                }
                return false;
            }).WithMessage(Config.ValidationMessages.TYPE_OBJECT_IS_NOT_FOUND);
            #endregion ObjectGuid

            #region Loose data
            this.RuleFor(x => x.DataTypeEnum).Custom((path, cntx) =>
            {
                var pg = ((Property)cntx.InstanceToValidate).DataType;
                var prev = pg.PrevCurrentVersion();
                var ver = "CURRENT";
                if (prev != null && pg.DataTypeEnum != prev.DataTypeEnum)
                {
                    var vf = new ValidationFailure(nameof(pg.DataTypeEnum),
                        $"Comparison with previous {ver} version. Data type was changed from '{Enum.GetName(typeof(EnumDataType), prev.DataTypeEnum)}' to '{Enum.GetName(typeof(EnumDataType), pg.DataTypeEnum)}'");
                    vf.Severity = Severity.Warning;
                    cntx.AddFailure(vf);
                }
                prev = pg.PrevStableVersion();
                ver = "STABLE";
                if (prev != null && pg.DataTypeEnum != prev.DataTypeEnum)
                {
                    var vf = new ValidationFailure(nameof(pg.DataTypeEnum),
                        $"Comparison with previous {ver} version. Data type was changed from '{Enum.GetName(typeof(EnumDataType), prev.DataTypeEnum)}' to '{Enum.GetName(typeof(EnumDataType), pg.DataTypeEnum)}'");
                    vf.Severity = Severity.Warning;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.Length).Custom((path, cntx) =>
            {
                var pg = ((Property)cntx.InstanceToValidate).DataType;
                var prev = pg.PrevCurrentVersion();
                var ver = "CURRENT";
                if (prev != null && pg.DataTypeEnum == prev.DataTypeEnum)
                {
                    if (pg.Length > 0 && prev.Length > 0 && pg.Length < prev.Length)
                    {
                        var vf = new ValidationFailure(nameof(pg.Length), $"Comparison with previous {ver} version. Length was reduced from '{prev.Length}' to '{pg.Length}'");
                        vf.Severity = Severity.Warning;
                        cntx.AddFailure(vf);
                    }
                    else if (pg.Length > 0 && prev.Length == 0)
                    {
                        var vf = new ValidationFailure(nameof(pg.Length), $"Comparison with previous {ver} version. Length was reduced from 'MAX' to '{pg.Length}'");
                        vf.Severity = Severity.Warning;
                        cntx.AddFailure(vf);
                    }
                }
                prev = pg.PrevStableVersion();
                ver = "STABLE";
                if (prev != null && pg.DataTypeEnum == prev.DataTypeEnum)
                {
                    if (pg.Length > 0 && prev.Length > 0 && pg.Length < prev.Length)
                    {
                        var vf = new ValidationFailure(nameof(pg.Length), $"Comparison with previous {ver} version. Length was reduced from '{prev.Length}' to '{pg.Length}'");
                        vf.Severity = Severity.Warning;
                        cntx.AddFailure(vf);
                    }
                    else if (pg.Length > 0 && prev.Length == 0)
                    {
                        var vf = new ValidationFailure(nameof(pg.Length), $"Comparison with previous {ver} version. Length was reduced from 'MAX' to '{pg.Length}'");
                        vf.Severity = Severity.Warning;
                        cntx.AddFailure(vf);
                        return;
                    }
                }
            });
            this.RuleFor(x => x.Accuracy).Custom((path, cntx) =>
            {
                var pg = ((Property)cntx.InstanceToValidate).DataType;
                var prev = pg.PrevCurrentVersion();
                var ver = "CURRENT";
                if (prev != null && pg.DataTypeEnum == prev.DataTypeEnum)
                {
                    if (pg.DataTypeEnum == EnumDataType.NUMERICAL)
                    {
                        if (pg.Accuracy < prev.Accuracy)
                        {
                            var vf = new ValidationFailure(nameof(pg.Accuracy), $"Comparison with previous {ver} version. Accuracy was reduced from '{prev.Accuracy}' to '{pg.Accuracy}'");
                            vf.Severity = Severity.Warning;
                            cntx.AddFailure(vf);
                        }
                    }
                }
                prev = pg.PrevStableVersion();
                ver = "STABLE";
                if (prev != null && pg.DataTypeEnum == prev.DataTypeEnum)
                {
                    if (pg.DataTypeEnum == EnumDataType.NUMERICAL)
                    {
                        if (pg.Accuracy < prev.Accuracy)
                        {
                            var vf = new ValidationFailure(nameof(pg.Accuracy), $"Comparison with previous {ver} version. Accuracy was reduced from '{prev.Accuracy}' to '{pg.Accuracy}'");
                            vf.Severity = Severity.Warning;
                            cntx.AddFailure(vf);
                        }
                    }
                }
            });
            #endregion Loose data

            #region Auto UI
            this.RuleFor(x => x.IsStopTabControl).Custom((isStopTabControl, cntx) =>
            {
                if (!isStopTabControl)
                    return;
                var p = (Property)cntx.InstanceToValidate;
                var indx = p.ParentGroupListProperties.ListProperties.IndexOf(p);
                if (indx == 0)
                {
                    var vf = new ValidationFailure(nameof(p.IsStopTabControl),
                        $"Can't stop using tab control when it is first field");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                    return;
                }
                var is_tab = false;
                for (int i = indx - 1; i >= 0; i--)
                {
                    p = p.ParentGroupListProperties.ListProperties[i];
                    if (p.IsStartNewTabControl || !string.IsNullOrWhiteSpace(p.TabName))
                    {
                        is_tab = true;
                        break;
                    }
                }
                if (!is_tab)
                {
                    var vf = new ValidationFailure(nameof(p.IsStopTabControl),
                        $"Can't stop using tab control when there are no current tab control");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                    return;
                }
            });
            this.RuleFor(x => x.TabName).Custom((tabName, cntx) =>
            {
                if (string.IsNullOrWhiteSpace(tabName))
                    return;
                var p = (Property)cntx.InstanceToValidate;
                if (p.IsTryAttach)
                {
                    var vf = new ValidationFailure(nameof(p.TabName),
                        $"Can't start new tab when attached to previous field");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                    return;
                }
            });
            this.RuleFor(x => x.IsStartNewRow).Custom((isStartNewRow, cntx) =>
            {
                if (!isStartNewRow)
                    return;
                var p = (Property)cntx.InstanceToValidate;
                var indx = p.ParentGroupListProperties.ListProperties.IndexOf(p);
                if (indx == 0)
                {
                    var vf = new ValidationFailure(nameof(p.IsStartNewRow),
                        $"Can't start new row when it is first field. It is new row anyway");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                    return;
                }
                if (p.IsTryAttach)
                {
                    var vf = new ValidationFailure(nameof(p.IsStartNewRow),
                        $"Can't start new row when attached to previous field");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                    return;
                }
            });
            this.RuleFor(x => x.IsStartNewTabControl).Custom((isStartNewTabControl, cntx) =>
            {
                if (!isStartNewTabControl)
                    return;
                var p = (Property)cntx.InstanceToValidate;
                if (p.IsTryAttach)
                {
                    var vf = new ValidationFailure(nameof(p.IsStartNewTabControl),
                        $"Can't start new tab control when property must be attached to previous field");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                    return;
                }
                if (p.IsStopTabControl)
                {
                    var vf = new ValidationFailure(nameof(p.IsStartNewTabControl),
                        $"Can't start new tab control and stop at the same time");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                    return;
                }
                if (p.IsStartNewRow)
                {
                    var vf = new ValidationFailure(nameof(p.IsStartNewTabControl),
                        $"Can't start new tab control and start new row the same time");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                    return;
                }
                if (string.IsNullOrWhiteSpace(p.TabName))
                {
                    var vf = new ValidationFailure(nameof(p.IsStartNewTabControl),
                        $"Can't start new tab control without tab name");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                    return;
                }
            });
            this.RuleFor(x => x.IsTryAttach).Custom((isTryAttach, cntx) =>
            {
                if (!isTryAttach)
                    return;
                var p = (Property)cntx.InstanceToValidate;
                var indx = p.ParentGroupListProperties.ListProperties.IndexOf(p);
                if (indx == 0)
                {
                    var vf = new ValidationFailure(nameof(p.IsTryAttach),
                        $"Can't be attached to previous property when it is a first field");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                    return;
                }
                if (p.IsStartNewTabControl)
                {
                    var vf = new ValidationFailure(nameof(p.IsTryAttach),
                        $"Can't be attached to previous property when must be placed on new tab control");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                    return;
                }
                if (p.IsStopTabControl)
                {
                    var vf = new ValidationFailure(nameof(p.IsTryAttach),
                        $"Can't be attached to previous property when must be placed without tab control which was used for previous field");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                    return;
                }
                if (p.IsStartNewRow)
                {
                    var vf = new ValidationFailure(nameof(p.IsTryAttach),
                        $"Can't be attached to previous property when must be placed on an next row");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                    return;
                }
                if (!string.IsNullOrWhiteSpace(p.TabName))
                {
                    var vf = new ValidationFailure(nameof(p.IsTryAttach),
                        $"Can't be attached to previous property when must be placed on new TAB page");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                    return;
                }
            });
            #endregion Auto UI

            this.RuleFor(x => x.IsMarkedForDeletion).Custom((name, cntx) =>
            {
                var p = (Property)cntx.InstanceToValidate;
                if (p.IsMarkedForDeletion)
                    return;
                if (p.DataTypeEnum == EnumDataType.CATALOG || p.DataTypeEnum == EnumDataType.ENUMERATION || p.DataTypeEnum == EnumDataType.DOCUMENT)
                {
                    if (string.IsNullOrWhiteSpace(p.ObjectGuid))
                    {
                        var vf = new ValidationFailure(nameof(p.ObjectGuid),
                            $"Property general type is {Enum.GetName<EnumDataType>(p.DataTypeEnum)}, but subtype is not selected");
                        vf.Severity = Severity.Error;
                        cntx.AddFailure(vf);
                        return;
                    }
                    var cfg = p.Cfg;
                    Debug.Assert(cfg.DicNodes.ContainsKey(p.ObjectGuid));
                    var refObj = cfg.DicNodes[p.ObjectGuid];
                    Debug.Assert(refObj != null);
                    var refObjEditable = refObj as IEditableNode;
                    Debug.Assert(refObjEditable != null);
                    if (refObjEditable.IsMarkedForDeletion)
                    {
                        if (p.Parent is IEditableNode pe)
                        {
                            if (!p.IsMarkedForDeletion && !pe.IsMarkedForDeletion)
                            {
                                var vf = new ValidationFailure(nameof(p.IsMarkedForDeletion),
                                    $"Property type is {refObj.GetType().Name}:'{refObj.Name}'. This type is marked for deletion, but this property or object '{p.Parent.Name}' is not marked for deletion");
                                vf.Severity = Severity.Error;
                                cntx.AddFailure(vf);
                            }
                        }
                    }
                }
                else if (p.DataTypeEnum == EnumDataType.CATALOGS || p.DataTypeEnum == EnumDataType.DOCUMENTS)
                {
                    if (p.ListObjectGuids.Count == 0)
                    {
                        var vf = new ValidationFailure(nameof(p.ObjectGuid),
                            $"Property general type is {Enum.GetName<EnumDataType>(p.DataTypeEnum)}, but subtypes are not selected");
                        vf.Severity = Severity.Error;
                        cntx.AddFailure(vf);
                        return;
                    }
                    var cfg = p.Cfg;
                    foreach (var t in p.ListObjectGuids)
                    {
                        Debug.Assert(cfg.DicNodes.ContainsKey(t));
                        var refObj = cfg.DicNodes[t];
                        Debug.Assert(refObj != null);
                        var refObjEditable = refObj as IEditableNode;
                        Debug.Assert(refObjEditable != null);
                        if (refObjEditable.IsMarkedForDeletion)
                        {
                            if (p.Parent is IEditableNode pe)
                            {
                                if (!p.IsMarkedForDeletion && !pe.IsMarkedForDeletion)
                                {
                                    var vf = new ValidationFailure(nameof(p.IsMarkedForDeletion),
                                        $"Property type is {refObj.GetType().Name}:'{refObj.Name}'. This type is marked for deletion, but this constant can use it as it's type");
                                    vf.Severity = Severity.Error;
                                    cntx.AddFailure(vf);
                                }
                            }
                        }

                    }
                }
            });
        }
        private static void ValidateSpecialProperties(string name, ValidationContext<Property> cntx, Property p, Catalog c)
        {
            var model = c.ParentGroupListCatalogs.ParentModel;
            if (c.GetUseCodeProperty())
            {
                if (model.PropertyCodeName == name)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Catalog parameter 'UseCodeProperty' is set to 'true'. Property name '{model.PropertyCodeName}' is reserved for auto generated property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
            if (c.GetUseNameProperty())
            {
                if (model.PropertyNameName == name)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Catalog parameter 'UseNameProperty' is set to 'true'. Property name '{model.PropertyNameName}' is reserved for auto generated property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
            if (c.GetUseDescriptionProperty())
            {
                if (model.PropertyDescriptionName == name)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Catalog parameter 'UseDescriptionProperty' is set to 'true'. Property name '{model.PropertyDescriptionName}' is reserved for auto generated property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
        }
        private static void ValidateSpecialProperties(string name, ValidationContext<Property> cntx, Property p, CatalogFolder cf)
        {
            var model = cf.ParentCatalog.ParentGroupListCatalogs.ParentModel;
            if (cf.GetUseCodeProperty())
            {
                if (model.PropertyCodeName == name)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Catalog folder parameter 'UseCodeProperty' is set to 'true'. Property name '{model.PropertyCodeName}' is reserved for auto generated property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
            if (cf.GetUseNameProperty())
            {
                if (model.PropertyNameName == name)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Catalog folder parameter 'UseNameProperty' is set to 'true'. Property name '{model.PropertyNameName}' is reserved for auto generated property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
            if (cf.GetUseDescriptionProperty())
            {
                if (model.PropertyDescriptionName == name)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Catalog folder parameter 'UseDescriptionProperty' is set to 'true'. Property name '{model.PropertyDescriptionName}' is reserved for auto generated property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
        }
        private static void ValidateSpecialProperties(string name, ValidationContext<Property> cntx, Property p, Detail dd)
        {
            var model = dd.Cfg.Model;
            if (dd.GetUseCodeProperty())
            {
                if (model.PropertyCodeName == name)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Detail parameter 'UseCodeProperty' is set to 'true'. Property name '{model.PropertyCodeName}' is reserved for auto generated property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
            if (dd.GetUseNameProperty())
            {
                if (model.PropertyNameName == name)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Detail parameter 'UseNameProperty' is set to 'true'. Property name '{model.PropertyNameName}' is reserved for auto generated property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
            if (dd.GetUseDescriptionProperty())
            {
                if (model.PropertyDescriptionName == name)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Detail parameter 'UseDescriptionProperty' is set to 'true'. Property name '{model.PropertyDescriptionName}' is reserved for auto generated property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
        }
        private static void ValidateSpecialProperties(string name, ValidationContext<Property> cntx, Property p, Document d)
        {
            var model = d.ParentGroupListDocuments.ParentGroupDocuments.ParentModel;
            if (d.GetUseDocNumberProperty())
            {
                if (model.PropertyDocNumberName == name)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Document parameter 'UseDocCodeProperty' is set to 'true'. Property name '{model.PropertyDocNumberName}' is reserved for auto generated property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
            if (d.GetUseDocDateProperty())
            {
                if (model.PropertyDocDateName == name)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Document parameter 'UseDocDateProperty' is set to 'true'. Property name '{model.PropertyDocDateName}' is reserved for auto generated property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
        }
        private static void ValidateSpecialProperties(string name, ValidationContext<Property> cntx, Property p, GroupDocuments gd)
        {
            var model = gd.ParentModel;
            if (gd.GetUseDocCodeProperty())
            {
                if (model.PropertyDocNumberName == name)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Document parameter 'UseDocCodeProperty' is set to 'true'. Property name '{model.PropertyDocNumberName}' is reserved for auto generated property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
            if (gd.GetUseDocDateProperty())
            {
                if (model.PropertyDocDateName == name)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Document parameter 'UseDocDateProperty' is set to 'true'. Property name '{model.PropertyDocDateName}' is reserved for auto generated property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
        }
        private static void ValidateSpecialProperties(string name, ValidationContext<Property> cntx, Property p, Register r)
        {
            var model = r.ParentGroupListRegisters.ParentModel;

            if (model.PropertyDocNumberName == name)
            {
                var vf = new ValidationFailure(nameof(p.Name),
                    $"Property name '{name}' is reserved for auto generated document number property");
                vf.Severity = Severity.Error;
                cntx.AddFailure(vf);
            }
            if (model.PropertyDocDateName == name)
            {
                var vf = new ValidationFailure(nameof(p.Name),
                    $"Property name '{name}' is reserved for auto generated document date property");
                vf.Severity = Severity.Error;
                cntx.AddFailure(vf);
            }
            if (r.PropertyMoneyAccumulatorName == name)
            {
                var vf = new ValidationFailure(nameof(p.Name),
                    $"Property name '{name}' is reserved for auto generated money accumulator property of this register");
                vf.Severity = Severity.Error;
                cntx.AddFailure(vf);
            }
            if (r.PropertyQtyAccumulatorName == name)
            {
                var vf = new ValidationFailure(nameof(p.Name),
                    $"Property name '{name}' is reserved for auto generated quantity accumulator property of this register");
                vf.Severity = Severity.Error;
                cntx.AddFailure(vf);
            }
            if (r.PropertyDocRefName == name)
            {
                var vf = new ValidationFailure(nameof(p.Name),
                    $"Property name '{name}' is reserved for auto generated document reference property of this register");
                vf.Severity = Severity.Error;
                cntx.AddFailure(vf);
            }
            if (r.PropertyDocRefGuidName == name)
            {
                var vf = new ValidationFailure(nameof(p.Name),
                    $"Property name '{name}' is reserved for auto generated document Guid property of this register");
                vf.Severity = Severity.Error;
                cntx.AddFailure(vf);
            }
            foreach (var t in r.GroupRegisterDimensions.ListDimensions)
            {
                if (t.Name == name)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Property name '{name}' is already used as register dimention name");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
        }
        private bool ParsableToBigInteger(string val)
        {
            if (string.IsNullOrWhiteSpace(val))
            {
                return true;
            }

            BigInteger v;
            return BigInteger.TryParse(val, out v);
        }
    }
}
