using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using static Proto.Config.proto_data_type.Types;

namespace vSharpStudio.vm.ViewModels
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?view=netframework-4.7.2
    public partial class DataType : ViewModelValidatable<DataType, DataType.DataTypeValidator>
    {
        public DataType(EnumDataType type, uint? length = null, uint? accuracy = null) : this()
        {
            this.DataTypeEnum = type;
            switch (this.DataTypeEnum)
            {
                case EnumDataType.Any:
                    break;
                case EnumDataType.Bool:
                    break;
                case EnumDataType.Catalogs:
                    break;
                case EnumDataType.Documents:
                    break;
                case EnumDataType.Numerical:
                    // TODO revisit default length and accuracy for Numerical
                    this.Length = length ?? 16;
                    this.Accuracy = accuracy ?? 2;
                    break;
                case EnumDataType.Enum:
                    break;
                case EnumDataType.String:
                    this.Length = length ?? 30;
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        public DataType(EnumDataType type, string guidOfType) : this()
        {
            this.DataTypeEnum = type;
            this.Guid = guidOfType;
            switch (this.DataTypeEnum)
            {
                case EnumDataType.Catalog:
                    break;
                case EnumDataType.Constant:
                    break;
                case EnumDataType.Document:
                    break;
                case EnumDataType.Enum:
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        public BigInteger MinValue
        {
            set
            {
                if (_MinValue != value)
                {
                    _MinValue = value;
                    _dto.MinValueString = _MinValue.ToString();
                    NotifyPropertyChanged();
                    ValidateProperty();
                }
            }
            get
            {
                if (_MinValue == null)
                {
                    BigInteger v;
                    if (BigInteger.TryParse(_dto.MinValueString, out v))
                        _MinValue = v;
                }
                return _MinValue;
            }
        }
        private BigInteger _MinValue;
        public BigInteger MaxValue
        {
            set
            {
                if (_MaxValue != value)
                {
                    _MaxValue = value;
                    _dto.MaxValueString = _MaxValue.ToString();
                    NotifyPropertyChanged();
                    ValidateProperty();
                }
            }
            get
            {
                if (_MaxValue == null)
                {
                    BigInteger v;
                    if (BigInteger.TryParse(_dto.MaxValueString, out v))
                        _MaxValue = v;
                }
                return _MaxValue;
            }
        }
        private BigInteger _MaxValue;
        public partial class DataTypeValidator
        {
            public DataTypeValidator()
            {
                RuleFor(x => x.MinValueString).NotEmpty().WithMessage("Please provide minimum value");
                RuleFor(x => x.MaxValueString).NotEmpty().WithMessage("Please provide maximum value");
                RuleFor(x => x.MinValueString).Must(ParsableToBigInteger).WithMessage("Can't parse to integer");
                RuleFor(x => x.MaxValueString).Must(ParsableToBigInteger).WithMessage("Can't parse to integer");
                RuleFor(x => x.Length).GreaterThan(0u);
                RuleFor(x => x.Accuracy).LessThan(x => x.Length);
                RuleFor(x => x.ObjectName).NotEmpty().When(x => x.DataTypeEnum == EnumDataType.Catalog).WithMessage("Please select catalog name");
                RuleFor(x => x.ObjectName).NotEmpty().When(x => x.DataTypeEnum == EnumDataType.Document).WithMessage("Please select document name");
            }
            private bool ParsableToBigInteger(string val)
            {
                BigInteger v;
                return BigInteger.TryParse(val, out v);
            }
        }
    }
}
