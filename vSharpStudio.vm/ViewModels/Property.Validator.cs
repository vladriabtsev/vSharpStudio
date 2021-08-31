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
            this.RuleFor(x => x.MinDateRequirement).Must((p, y) =>
            {
                if (string.IsNullOrWhiteSpace(y))
                    return true;
                DateTime res;
                if (p.DataTypeEnum == EnumDataType.DATE || p.DataTypeEnum == EnumDataType.DATETIME || p.DataTypeEnum == EnumDataType.DATETIMEZ)
                {
                    // https://docs.microsoft.com/en-us/dotnet/standard/base-types/parsing-datetime
                    if (!DateTime.TryParse(y, out res))
                    {
                        return false;
                    }
                }
                return true;
            }).WithMessage("Can't parse by DateTime.TryParse()");
            this.RuleFor(x => x.MaxDateRequirement).Must((p, y) =>
            {
                if (string.IsNullOrWhiteSpace(y))
                    return true;
                DateTime res;
                if (p.DataTypeEnum == EnumDataType.DATE || p.DataTypeEnum == EnumDataType.DATETIME || p.DataTypeEnum == EnumDataType.DATETIMEZ)
                {
                    // https://docs.microsoft.com/en-us/dotnet/standard/base-types/parsing-datetime
                    if (!DateTime.TryParse(y, out res))
                    {
                        return false;
                    }
                }
                return true;
            }).WithMessage("Can't parse by DateTime.TryParse()");
            this.RuleFor(x => x.MinValueRequirement).Custom((x, cntx) =>
            {
                var p = (Property)cntx.InstanceToValidate;
                if (string.IsNullOrWhiteSpace(x))
                    return;
                if (p.DataTypeEnum != EnumDataType.NUMERICAL)
                    return;
                var mes = CanParse(x, p);
                if (!string.IsNullOrWhiteSpace(mes))
                {
                    var vf = new ValidationFailure(nameof(p.MinValueRequirement), mes);
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.MaxValueRequirement).Custom((x, cntx) =>
            {
                var p = (Property)cntx.InstanceToValidate;
                if (string.IsNullOrWhiteSpace(x))
                    return;
                if (p.DataTypeEnum != EnumDataType.NUMERICAL)
                    return;
                var mes = CanParse(x, p);
                if (!string.IsNullOrWhiteSpace(mes))
                {
                    var vf = new ValidationFailure(nameof(p.MaxValueRequirement), mes);
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
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
        }

        private static string CanParse(string x, Property p)
        {
            var dt = p.DataType;
            if (p.Accuracy == 0)
            {
                if (p.IsPositive)
                {
                    if (dt.MaxNumericalValue <= byte.MaxValue)
                    {
                        byte res;
                        if (!byte.TryParse(x, out res))
                            return $"Can't parse value by byte.TryParse()";
                    }
                    else if (dt.MaxNumericalValue <= ushort.MaxValue)
                    {
                        ushort res;
                        if (!ushort.TryParse(x, out res))
                            return $"Can't parse value by ushort.TryParse()";
                    }
                    else if (dt.MaxNumericalValue <= uint.MaxValue)
                    {
                        uint res;
                        if (!uint.TryParse(x, out res))
                            return $"Can't parse value by uint.TryParse()";
                    }
                    else if (dt.MaxNumericalValue <= ulong.MaxValue) // long, not ulong
                    {
                        ulong res;
                        if (!ulong.TryParse(x, out res))
                            return $"Can't parse value by ulong.TryParse()";
                    }
                    else if (dt.Length <= 28)
                    {
                        decimal res;
                        if (!decimal.TryParse(x, out res))
                            return $"Can't parse value by decimal.TryParse()";
                    }
                    throw new Exception("Not supported operation");
                    // return "BigInteger" + sn;
                }
                else
                {
                    if (dt.MaxNumericalValue <= sbyte.MaxValue)
                    {
                        sbyte res;
                        if (!sbyte.TryParse(x, out res))
                            return $"Can't parse value by sbyte.TryParse()";
                    }
                    else if (dt.MaxNumericalValue <= short.MaxValue)
                    {
                        short res;
                        if (!short.TryParse(x, out res))
                            return $"Can't parse value by short.TryParse()";
                    }
                    else if (dt.MaxNumericalValue <= int.MaxValue)
                    {
                        int res;
                        if (!int.TryParse(x, out res))
                            return $"Can't parse value by int.TryParse()";
                    }
                    else if (dt.MaxNumericalValue <= long.MaxValue)
                    {
                        long res;
                        if (!long.TryParse(x, out res))
                            return $"Can't parse value by long.TryParse()";
                    }
                    else if (dt.Length <= 28)
                    {
                        decimal res;
                        if (!decimal.TryParse(x, out res))
                            return $"Can't parse value by decimal.TryParse()";
                    }
                    throw new Exception("Not supported operation");
                    // return "BigInteger" + sn;
                }
            }
            else
            {
                // float   ±1.5 x 10−45   to ±3.4    x 10+38    ~6-9 digits
                // double  ±5.0 × 10−324  to ±1.7    × 10+308   ~15-17 digits
                // decimal ±1.0 x 10-28   to ±7.9228 x 10+28     28-29 significant digits
                if (dt.Length == 0)
                {
                    //BigDecimal res;
                    //if (!BigDecimal.TryParse(x, out res))
                    //    return $"Can't parse value by BigDecimal.TryParse()";
                }
                else if (dt.Length <= 6)
                {
                    float res;
                    if (!float.TryParse(x, out res))
                        return $"Can't parse value by float.TryParse()";
                }
                else if (dt.Length <= 15)
                {
                    double res;
                    if (!double.TryParse(x, out res))
                        return $"Can't parse value by double.TryParse()";
                }
                else if (dt.Length < 29)
                {
                    decimal res;
                    if (!decimal.TryParse(x, out res))
                        return $"Can't parse value by decimal.TryParse()";
                }
                throw new Exception("Not supported operation");
                // return "BigDecimal";
            }
            return null;
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
