using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class DataTypeValidator
    {
        //IDataType prev = null;
        public DataTypeValidator()
        {
            #region Length
            // RuleFor(x => x.LengthString).Must(ParsableToBigInteger).WithMessage(Config.ValidationMessages.TYPE_MINMAX_CANT_PARSE);
            this.RuleFor(p => p.Length).Must((p, y) =>
            {
                if (y < 0)
                {
                    return false;
                }

                return true;
            }).WithMessage(Config.ValidationMessages.TYPE_LENGTH_POSITIVE);
            this.RuleFor(p => p.Length).Must((p, y) =>
            {
                if (p.DataTypeEnum == EnumDataType.NUMERICAL && y < 1)
                {
                    return false;
                }

                return true;
            }).WithMessage(Config.ValidationMessages.TYPE_LENGTH_GREATER_THAN_ZERO);
            this.RuleFor(p => p.Length).Must((p, y) =>
            {
                if (p.Length == 0)
                {
                    return true;
                }

                if (p.DataTypeEnum == EnumDataType.NUMERICAL && y <= p.Accuracy)
                {
                    return false;
                }

                return true;
            }).WithMessage(Config.ValidationMessages.TYPE_LENGTH_LESS_THAN_ACCURACY);
            this.RuleFor(p => p.Length).Must((p, y) =>
            {
                if (p.Length == 0)
                {
                    return true;
                }

                if (p.DataTypeEnum == EnumDataType.NUMERICAL && y > 38)
                {
                    return false;
                }

                return true;
            }).WithMessage(string.Format(Config.ValidationMessages.TYPE_LENGTH_LIMIT, 38));
            #endregion Length

            #region MinValueString
            // RuleFor(x => x.MinValueString).Must(ParsableToBigInteger).WithMessage(Config.ValidationMessages.TYPE_MINMAX_CANT_PARSE);
            // RuleFor(p => p.MinValueString).Must((p, y) =>
            // {
            //    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.String && p.IsPositive && p.MinValue < 0)
            //        return false;
            //    return true;
            // }).WithMessage(Config.ValidationMessages.TYPE_MAXLENGTH_GREATER_LENGTH);
            #endregion MinValueString

            #region MaxValueString
            // RuleFor(x => x.MaxValueString).Must(ParsableToBigInteger).WithMessage(Config.ValidationMessages.TYPE_MINMAX_CANT_PARSE);
            // RuleFor(p => p.MaxValueString).Must((p, y) =>
            // {
            //    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.String && p.MaxValue > p.Length)
            //        return false;
            //    return true;
            // }).WithMessage(Config.ValidationMessages.TYPE_MAXLENGTH_GREATER_LENGTH);
            // RuleFor(p => p.MaxValueString).Must((p, y) =>
            // {
            //    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.Numerical)
            //        return false;
            //    return true;
            // }).WithMessage(Config.ValidationMessages.TYPE_MIN_EMPTY).WithSeverity(Severity.Warning);
            #endregion MaxValueString

            #region Accuracy
            this.RuleFor(p => p.Accuracy).Must((p, y) =>
            {
                if (p.Length == 0)
                {
                    return true;
                }

                if (p.DataTypeEnum == EnumDataType.NUMERICAL && y >= p.Length)
                {
                    return false;
                }

                return true;
            }).WithMessage(Config.ValidationMessages.TYPE_ACCURACY_GREATER_THAN_LENGTH);
            #endregion Accuracy

            #region ObjectGuid
            this.RuleFor(p => p.ObjectRef.ForeignObjectGuid).Must((p, y) =>
            {
                if (p.DataTypeEnum == EnumDataType.ENUMERATION && string.IsNullOrWhiteSpace(p.ObjectRef.ForeignObjectGuid))
                {
                    return false;
                }
                return true;
            }).WithMessage(Config.ValidationMessages.TYPE_EMPTY_ENUMERATION);
            this.RuleFor(p => p.ObjectRef.ForeignObjectGuid).Must((p, y) =>
            {
                if (p.DataTypeEnum == EnumDataType.CATALOG && string.IsNullOrWhiteSpace(p.ObjectRef.ForeignObjectGuid))
                {
                    return false;
                }
                return true;
            }).WithMessage(Config.ValidationMessages.TYPE_EMPTY_CATALOG);
            this.RuleFor(p => p.ObjectRef.ForeignObjectGuid).Must((p, y) =>
            {
                if (p.DataTypeEnum == EnumDataType.DOCUMENT && string.IsNullOrWhiteSpace(p.ObjectRef.ForeignObjectGuid))
                {
                    return false;
                }
                return true;
            }).WithMessage(Config.ValidationMessages.TYPE_EMPTY_DOCUMENT);
            this.RuleFor(p => p.ObjectRef.ForeignObjectGuid).Must((p, y) =>
            {
                if (p.DataTypeEnum != EnumDataType.ENUMERATION)
                {
                    return true;
                }
                if (p.Cfg == null)
                    return true;
                foreach (var t in p.Cfg.Model.GroupEnumerations.ListEnumerations)
                {
                    if (t.Guid == y)
                    {
                        return true;
                    }
                }
                return false;
            }).WithMessage(Config.ValidationMessages.TYPE_OBJECT_IS_NOT_FOUND);
            this.RuleFor(p => p.ObjectRef.ForeignObjectGuid).Must((p, y) =>
            {
                if (p.DataTypeEnum != EnumDataType.CATALOG)
                {
                    return true;
                }
                if (p.Cfg == null)
                    return true;
                foreach (var t in p.Cfg.Model.GroupCatalogs.ListCatalogs)
                {
                    if (t.Guid == y)
                    {
                        return true;
                    }
                }
                return false;
            }).WithMessage(Config.ValidationMessages.TYPE_OBJECT_IS_NOT_FOUND);
            this.RuleFor(p => p.ObjectRef.ForeignObjectGuid).Must((p, y) =>
            {
                if (p.DataTypeEnum != EnumDataType.DOCUMENT)
                {
                    return true;
                }
                if (p.Cfg == null)
                    return true;
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
            //TODO: Migration one DataTypeEnum to another need creation new property an made current OBSOLETE. Need data migration code. Deletion of OBSOLETE objects for next iteration.
            this.RuleFor(x => x.DataTypeEnum).Custom((path, cntx) =>
            {
                var pg = (DataType)cntx.InstanceToValidate;
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
                var pg = (DataType)cntx.InstanceToValidate;
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
                var pg = (DataType)cntx.InstanceToValidate;
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
