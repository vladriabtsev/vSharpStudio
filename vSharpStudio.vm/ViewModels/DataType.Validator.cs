﻿using System;
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
                    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.Numerical && y <= p.Accuracy)
                        return false;
                    return true;
                }).WithMessage(Config.ValidationMessages.TYPE_LENGTH_GREATER_THAN_ACCURACY);
                #endregion Length

                #region MinValueString
                RuleFor(x => x.MinValueString).Must(ParsableToBigInteger).WithMessage(Config.ValidationMessages.TYPE_MINMAX_CANT_PARSE);
                RuleFor(p => p.MinValueString).Must((p, y) =>
                {
                    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.String && p.IsPositive && p.MinValue < 0)
                        return false;
                    return true;
                }).WithMessage(Config.ValidationMessages.TYPE_MAXLENGTH_GREATER_LENGTH);
                #endregion MinValueString

                #region MaxValueString
                RuleFor(x => x.MaxValueString).Must(ParsableToBigInteger).WithMessage(Config.ValidationMessages.TYPE_MINMAX_CANT_PARSE);
                RuleFor(p => p.MaxValueString).Must((p, y) =>
                {
                    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.String && p.MaxValue > p.Length)
                        return false;
                    return true;
                }).WithMessage(Config.ValidationMessages.TYPE_MAXLENGTH_GREATER_LENGTH);
                RuleFor(p => p.MaxValueString).Must((p, y) =>
                {
                    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.Numerical)
                        return false;
                    return true;
                }).WithMessage(Config.ValidationMessages.TYPE_MIN_EMPTY).WithSeverity(Severity.Warning);
                #endregion MaxValueString

                #region Accuracy
                RuleFor(p => p.Accuracy).Must((p, y) =>
                {
                    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.Numerical && y >= p.Length)
                        return false;
                    return true;
                }).WithMessage(Config.ValidationMessages.TYPE_LENGTH_GREATER_THAN_ACCURACY);
                #endregion Accuracy

                #region ObjectName
                RuleFor(p => p.ObjectName).Must((p, y) =>
                {
                    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.Constant && string.IsNullOrWhiteSpace(p.ObjectName))
                        return false;
                    return true;
                }).WithMessage(Config.ValidationMessages.TYPE_EMPTY_CONSTANT_NAME);
                //RuleFor(p => p.ObjectName).Must((p, y) =>
                //{
                //    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.Enum)
                //        return false;
                //    return true;
                //}).WithMessage(Config.ValidationMessages.TYPE_EMPTY_ENUMERATION_NAME);
                RuleFor(p => p.ObjectName).Must((p, y) =>
                {
                    if (p.DataTypeEnum == Proto.Config.proto_data_type.Types.EnumDataType.Catalog && string.IsNullOrWhiteSpace(p.ObjectName))
                        return false;
                    return true;
                }).WithMessage(Config.ValidationMessages.TYPE_EMPTY_CATALOG_NAME);
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
