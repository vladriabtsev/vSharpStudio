using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;
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
                    ValidateSpecialProperties(name, cntx, p, c, gc);
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
                            if (gc.PropertyIsOpenName == name)
                            {
                                var vf = new ValidationFailure(nameof(p.Name),
                                    $"Catalog parameter 'Use Tree' is set to 'true' and 'Separate Folder' is set to 'false'. Property name '{gc.PropertyIsOpenName}' is reserved for auto generated property");
                                vf.Severity = Severity.Error;
                                cntx.AddFailure(vf);
                            }
                            if (c.UseFolderTypeExplicitly)
                            {
                                if (gc.PropertyIsFolderName == name)
                                {
                                    var vf = new ValidationFailure(nameof(p.Name),
                                        $"Catalog parameter 'Explicit Folders' is set to 'true'. Property name '{gc.PropertyIsFolderName}' is reserved for auto generated property");
                                    vf.Severity = Severity.Error;
                                    cntx.AddFailure(vf);
                                }
                            }
                        }
                    }
                }
                else if (pg.Parent is CatalogFolder)
                {
                    var c = (Catalog)pg.Parent.Parent;
                    var gc = (IGroupListCatalogs)c.Parent;
                    var cfg = pg.GetConfig();
                    ValidateSpecialProperties(name, cntx, p, c, gc);
                    if (c.UseTree)
                    {
                        if (c.UseSeparateTreeForFolders)
                        {
                            if (name == "RefTreeParent")
                            {
                                var vf = new ValidationFailure(nameof(p.Name),
                                    $"Catalog parameter 'Use Tree' is set to 'true' and 'Separate Folder' is set to 'true'. Property name 'RefTreeParent' is reserved for auto generated property");
                                vf.Severity = Severity.Error;
                                cntx.AddFailure(vf);
                            }
                            if (c.UseFolderTypeExplicitly)
                            {
                                if (gc.PropertyIsOpenName == name)
                                {
                                    var vf = new ValidationFailure(nameof(p.Name),
                                        $"Catalog parameter 'Explicit Folders' is set to 'true'. Property name '{gc.PropertyIsOpenName}' is reserved for auto generated property");
                                    vf.Severity = Severity.Error;
                                    cntx.AddFailure(vf);
                                }
                            }
                        }
                    }
                }
                else if (pg.Parent is Detail)
                {
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
                    if (p.Length > 38)
                    {
                        var vf = new ValidationFailure(nameof(p.Length),
                            $"Value greater than 38 is not supported");
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

                IParent n = (IParent)p;
                while (true)
                {
                    if (n.Parent != null)
                    {
                        n = n.Parent;
                        if (n is Config)
                        {
                            break;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                foreach (var t in (n as Config).Model.GroupEnumerations.ListEnumerations)
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

                IParent n = (IParent)p;
                while (true)
                {
                    if (n.Parent != null)
                    {
                        n = n.Parent;
                        if (n is Config)
                        {
                            break;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                foreach (var t in (n as Config).Model.GroupCatalogs.ListCatalogs)
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

                IParent n = (IParent)p;
                while (true)
                {
                    if (n.Parent != null)
                    {
                        n = n.Parent;
                        if (n is Config)
                        {
                            break;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                foreach (var t in (n as Config).Model.GroupDocuments.GroupListDocuments.ListDocuments)
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
                var grp = p.Parent as GroupListProperties;
                var indx = grp.ListProperties.IndexOf(p);
                if (indx == 0)
                {
                    var vf = new ValidationFailure(nameof(p.IsStopTabControl),
                        $"Can't stop using tab control when it is first field");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                    return;
                }
                var is_tab = false;
                for (int i = indx-1; i>=0; i--)
                {
                    p = grp.ListProperties[i];
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
                var grp = p.Parent as GroupListProperties;
                var indx = grp.ListProperties.IndexOf(p);
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
                var grp = p.Parent as GroupListProperties;
                var indx = grp.ListProperties.IndexOf(p);
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
        }
        private static void ValidateSpecialProperties(string name, ValidationContext<Property> cntx, Property p, Catalog c, IGroupListCatalogs gc)
        {
            if (c.GetUseCodeProperty())
            {
                if (gc.PropertyCodeName == name)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Catalog parameter 'UseCodeProperty' is set to 'true'. Property name '{gc.PropertyCodeName}' is reserved for auto generated property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
            if (c.GetUseNameProperty())
            {
                if (gc.PropertyNameName == name)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Catalog parameter 'UseNameProperty' is set to 'true'. Property name '{gc.PropertyNameName}' is reserved for auto generated property");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            }
            if (c.GetUseDescriptionProperty())
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
