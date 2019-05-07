using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using ViewModelBase;
using FluentValidation;
using static Proto.Config.proto_data_type.Types;

namespace vSharpStudio.vm.ViewModels
{
    public partial class DataType
    {
        public partial class DataTypeValidator
        {
            public DataTypeValidator()
            {
                #region Length
                //RuleFor(x => x.LengthString).Must(ParsableToBigInteger).WithMessage(Config.ValidationMessages.TYPE_MINMAX_CANT_PARSE);
                RuleFor(p => p.Length).Must((p, y) =>
                {
                    if (y < 0) return false;
                    return true;
                }).WithMessage(Config.ValidationMessages.TYPE_LENGTH_POSITIVE);
                RuleFor(p => p.Length).Must((p, y) =>
                {
                    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.Numerical && y < 1)
                        return false;
                    return true;
                }).WithMessage(Config.ValidationMessages.TYPE_LENGTH_GREATER_THAN_ZERO);
                RuleFor(p => p.Length).Must((p, y) =>
                {
                    if (p.Length == 0)
                        return true;
                    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.Numerical && y <= p.Accuracy)
                        return false;
                    return true;
                }).WithMessage(Config.ValidationMessages.TYPE_LENGTH_LESS_THAN_ACCURACY);
                #endregion Length

                #region MinValueString
                //RuleFor(x => x.MinValueString).Must(ParsableToBigInteger).WithMessage(Config.ValidationMessages.TYPE_MINMAX_CANT_PARSE);
                //RuleFor(p => p.MinValueString).Must((p, y) =>
                //{
                //    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.String && p.IsPositive && p.MinValue < 0)
                //        return false;
                //    return true;
                //}).WithMessage(Config.ValidationMessages.TYPE_MAXLENGTH_GREATER_LENGTH);
                #endregion MinValueString

                #region MaxValueString
                //RuleFor(x => x.MaxValueString).Must(ParsableToBigInteger).WithMessage(Config.ValidationMessages.TYPE_MINMAX_CANT_PARSE);
                //RuleFor(p => p.MaxValueString).Must((p, y) =>
                //{
                //    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.String && p.MaxValue > p.Length)
                //        return false;
                //    return true;
                //}).WithMessage(Config.ValidationMessages.TYPE_MAXLENGTH_GREATER_LENGTH);
                //RuleFor(p => p.MaxValueString).Must((p, y) =>
                //{
                //    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.Numerical)
                //        return false;
                //    return true;
                //}).WithMessage(Config.ValidationMessages.TYPE_MIN_EMPTY).WithSeverity(Severity.Warning);
                #endregion MaxValueString

                #region Accuracy
                RuleFor(p => p.Accuracy).Must((p, y) =>
                {
                    if (p.Length == 0)
                        return true;
                    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.Numerical && y >= p.Length)
                        return false;
                    return true;
                }).WithMessage(Config.ValidationMessages.TYPE_ACCURACY_GREATER_THAN_LENGTH);
                #endregion Accuracy

                #region ObjectName
                RuleFor(p => p.ObjectName).Must((p, y) =>
                {
                    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.Enumeration && string.IsNullOrWhiteSpace(p.ObjectName))
                        return false;
                    return true;
                }).WithMessage(Config.ValidationMessages.TYPE_EMPTY_ENUMERATION_NAME);
                RuleFor(p => p.ObjectName).Must((p, y) =>
                {
                    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.Catalog && string.IsNullOrWhiteSpace(p.ObjectName))
                        return false;
                    return true;
                }).WithMessage(Config.ValidationMessages.TYPE_EMPTY_CATALOG_NAME);
                RuleFor(p => p.ObjectName).Must((p, y) =>
                {
                    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.Document && string.IsNullOrWhiteSpace(p.ObjectName))
                        return false;
                    return true;
                }).WithMessage(Config.ValidationMessages.TYPE_EMPTY_DOCUMENT_NAME);
                RuleFor(p => p.ObjectName).Must((p, y) =>
                {
                    if (p.DataTypeEnum != Proto.Config.proto_data_type.Types.EnumDataType.Enumeration)
                        return true;
                    IParent n = (IParent)p;
                    while (true)
                    {
                        if (n.Parent != null)
                        {
                            n = n.Parent;
                            if (n is Config)
                                break;
                        }
                        else
                            return true;
                    }
                    foreach (var t in (n as Config).GroupEnumerations.Children)
                    {
                        if (t.Name == y)
                            return true;
                    }
                    return false;
                }).WithMessage(Config.ValidationMessages.TYPE_WRONG_OBJECT_NAME);
                RuleFor(p => p.ObjectName).Must((p, y) =>
                {
                    if (p.DataTypeEnum != Proto.Config.proto_data_type.Types.EnumDataType.Catalog)
                        return true;
                    IParent n = (IParent)p;
                    while (true)
                    {
                        if (n.Parent != null)
                        {
                            n = n.Parent;
                            if (n is Config)
                                break;
                        }
                        else
                            return true;
                    }
                    foreach (var t in (n as Config).GroupCatalogs.Children)
                    {
                        if (t.Name == y)
                            return true;
                    }
                    return false;
                }).WithMessage(Config.ValidationMessages.TYPE_WRONG_OBJECT_NAME);
                RuleFor(p => p.ObjectName).Must((p, y) =>
                {
                    if (p.DataTypeEnum != Proto.Config.proto_data_type.Types.EnumDataType.Document)
                        return true;
                    IParent n = (IParent)p;
                    while (true)
                    {
                        if (n.Parent != null)
                        {
                            n = n.Parent;
                            if (n is Config)
                                break;
                        }
                        else
                            return true;
                    }
                    foreach (var t in (n as Config).GroupDocuments.Children)
                    {
                        if (t.Name == y)
                            return true;
                    }
                    return false;
                }).WithMessage(Config.ValidationMessages.TYPE_WRONG_OBJECT_NAME);
                #endregion ObjectName

                //RuleFor(x => x.MinValueString).NotEmpty().WithMessage(Config.ValidationMessages.TYPE_MIN_EMPTY).WithSeverity(Severity.Warning);
                //RuleFor(x => x.MaxValueString).NotEmpty().WithMessage(Config.ValidationMessages.TYPE_MAX_EMPTY).WithSeverity(Severity.Warning);
            }
            private bool ParsableToBigInteger(string val)
            {
                if (string.IsNullOrWhiteSpace(val))
                    return true;
                BigInteger v;
                return BigInteger.TryParse(val, out v);
            }
        }
    }
}
