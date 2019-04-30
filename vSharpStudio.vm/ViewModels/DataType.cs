using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Dummy;
using FluentValidation;
using ViewModelBase;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using static Proto.Config.proto_data_type.Types;

namespace vSharpStudio.vm.ViewModels
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?view=netframework-4.7.2
    [DebuggerDisplay("DataType:{DataType.GetTypeDesc(this),nq}")]
    public partial class DataType : IParent
    {
        partial void OnInit()
        {
        }
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
                case EnumDataType.Enumeration:
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
            this.ObjectGuid = guidOfType;
            switch (this.DataTypeEnum)
            {
                case EnumDataType.Catalog:
                    break;
                case EnumDataType.Constant:
                    break;
                case EnumDataType.Document:
                    break;
                case EnumDataType.Enumeration:
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        public override string ToString()
        {
            return DataType.GetTypeDesc(this);
        }
        public static string GetTypeDesc(DataType p)
        {
            string res = Enum.GetName(typeof(Proto.Config.proto_data_type.Types.EnumDataType), (int)p.DataTypeEnum);
            switch (p.DataTypeEnum)
            {
                case Proto.Config.proto_data_type.Types.EnumDataType.Any:
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Bool:
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Catalog:
                    res += ": " + p.ObjectName;
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Catalogs:
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Constant:
                    res += ": " + p.ObjectName;
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Document:
                    res += ": " + p.ObjectName;
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Documents:
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Enumeration:
                    res += ": " + p.ObjectName;
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Numerical:
                    res += ", " + (p.IsPositive ? "+" : "") + " " + p.Length + (p.Accuracy > 0 ? "." + p.Accuracy : "") + " clr:" + p.ClrType + " proto:" + p.ProtoType;
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.String:
                    res += ", Length:" + (p.Length > 0 ? p.Length.ToString() : " unlimited");
                    break;
                default:
                    res += " - Not supported";
                    break;
            }
            return res;
        }
        //public BigInteger MinValue
        //{
        //    set
        //    {
        //        if (_MinValue != value)
        //        {
        //            _MinValue = value;
        //            NotifyPropertyChanged();
        //            ValidateProperty();
        //            this.MinValueString = _MinValue.ToString();
        //        }
        //    }
        //    get
        //    {
        //        if (_MinValue == null)
        //        {
        //            if (BigInteger.TryParse(this.MinValueString, out var v))
        //                _MinValue = v;
        //        }
        //        return _MinValue;
        //    }
        //}
        //private BigInteger _MinValue;
        //public BigInteger MaxValue
        //{
        //    set
        //    {
        //        if (_MaxValue != value)
        //        {
        //            _MaxValue = value;
        //            NotifyPropertyChanged();
        //            ValidateProperty();
        //            this.MaxValueString = _MaxValue.ToString();
        //        }
        //    }
        //    get
        //    {
        //        if (_MaxValue == null)
        //        {
        //            if (BigInteger.TryParse(this.MaxValueString, out var v))
        //                _MaxValue = v;
        //        }
        //        return _MaxValue;
        //    }
        //}
        //private BigInteger _MaxValue;
        [PropertyOrderAttribute(13)]
        public BigInteger MaxNumericalValue
        {
            get
            {
                if (_MaxNumericalValue == 0)
                {
                    _MaxNumericalValue = 1;
                    for (int i = 0; i < this.Length; i++)
                        _MaxNumericalValue *= 10;
                }
                return _MaxNumericalValue;
            }
        }
        private BigInteger _MaxNumericalValue;
        partial void OnLengthChanged()
        {
            _MaxNumericalValue = 0;
        }
        [PropertyOrderAttribute(11)]
        public string ClrType
        {
            get
            {
                // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/decimal
                switch (this.DataTypeEnum)
                {
                    case EnumDataType.Bool:
                        return "bool";
                    case EnumDataType.String:
                        return "string";
                    case EnumDataType.Numerical:
                        if (this.Accuracy == 0)
                        {
                            if (this.IsPositive)
                            {
                                if (this.MaxNumericalValue <= byte.MaxValue)
                                    return "byte";
                                if (this.MaxNumericalValue <= ushort.MaxValue)
                                    return "ushort";
                                if (this.MaxNumericalValue <= uint.MaxValue)
                                    return "uint";
                                if (this.MaxNumericalValue <= ulong.MaxValue) // long, not ulong
                                    return "ulong";
                                return "BigInteger";
                            }
                            else
                            {
                                if (this.MaxNumericalValue <= sbyte.MaxValue)
                                    return "sbyte";
                                if (this.MaxNumericalValue <= short.MaxValue)
                                    return "short";
                                if (this.MaxNumericalValue <= int.MaxValue)
                                    return "int";
                                if (this.MaxNumericalValue <= long.MaxValue)
                                    return "long";
                                return "BigInteger";
                            }
                        }
                        else
                        {
                            // float   ±1.5 x 10−45   to ±3.4    x 10+38    ~6-9 digits
                            // double  ±5.0 × 10−324  to ±1.7    × 10+308   ~15-17 digits
                            // decimal ±1.0 x 10-28   to ±7.9228 x 10+28     28-29 significant digits
                            if (this.Length <= 6)
                                return "float";
                            if (this.Length <= 15)
                                return "double";
                            if (this.Length < 29)
                                return "decimal";
                            return "BigDecimal";
                        }
                    default:
                        throw new Exception("Not supported operation");
                }
            }
        }
        [PropertyOrderAttribute(12)]
        public string ProtoType
        {
            get
            {
                // https://developers.google.com/protocol-buffers/docs/proto3#scalar
                switch (this.DataTypeEnum)
                {
                    case EnumDataType.Bool:
                        return "bool";
                    case EnumDataType.String:
                        return "string";
                    case EnumDataType.Numerical:
                        if (this.Accuracy == 0)
                        {
                            if (this.IsPositive)
                            {
                                if (this.MaxNumericalValue <= uint.MaxValue)
                                    return "uint32";
                                if (this.MaxNumericalValue <= long.MaxValue) // long, not ulong
                                    return "uint64";
                                return "bytes"; // need conversions
                            }
                            else
                            {
                                if (this.MaxNumericalValue <= int.MaxValue)
                                    return "int32";
                                if (this.MaxNumericalValue <= long.MaxValue)
                                    return "int64";
                                return "bytes"; // need conversions
                            }
                        }
                        else
                        {
                            // float   ±1.5 x 10−45   to ±3.4    x 10+38    ~6-9 digits
                            // double  ±5.0 × 10−324  to ±1.7    × 10+308   ~15-17 digits
                            // decimal ±1.0 x 10-28   to ±7.9228 x 10+28     28-29 significant digits
                            if (this.Length <= 6)
                                return "float";
                            if (this.Length <= 15)
                                return "double";
                            return "bytes"; // need conversions
                        }
                    default:
                        throw new Exception("Not supported operation");
                }
            }
        }
        [BrowsableAttribute(false)]
        public ITreeConfigNode Parent { get; set; }

        #region Visibility

        partial void OnDataTypeEnumChanged()
        {
            switch (this.DataTypeEnum)
            {
                case EnumDataType.Any:
                case EnumDataType.Bool:
                case EnumDataType.Catalogs:
                case EnumDataType.Documents:
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Collapsed;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    this.VisibilityIsPositive = Visibility.Collapsed;
                    break;
                case EnumDataType.Catalog:
                case EnumDataType.Constant:
                case EnumDataType.Document:
                case EnumDataType.Enumeration:
                    this.VisibilityIsPositive = Visibility.Collapsed;
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Collapsed;
                    this.VisibilityObjectName = Visibility.Visible;
                    break;
                case EnumDataType.Numerical:
                    if (this.Accuracy == 0)
                        this.VisibilityIsPositive = Visibility.Visible;
                    else
                        this.VisibilityIsPositive = Visibility.Collapsed;
                    this.VisibilityAccuracy = Visibility.Visible;
                    this.VisibilityLength = Visibility.Visible;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    break;
                case EnumDataType.String:
                    this.VisibilityIsPositive = Visibility.Collapsed;
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Visible;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    break;
            }
        }
        partial void OnAccuracyChanged()
        {
            if (this.Accuracy == 0)
                this.VisibilityIsPositive = Visibility.Visible;
            else
                this.VisibilityIsPositive = Visibility.Collapsed;
        }

        [BrowsableAttribute(false)]
        public Visibility VisibilityLength
        {
            set
            {
                if (_VisibilityLength == value)
                    return;
                _VisibilityLength = value;
                NotifyPropertyChanged();
            }
            get { return _VisibilityLength; }
        }
        private Visibility _VisibilityLength = Visibility.Collapsed;
        [BrowsableAttribute(false)]
        public Visibility VisibilityAccuracy
        {
            set
            {
                if (_VisibilityAccuracy == value)
                    return;
                _VisibilityAccuracy = value;
                NotifyPropertyChanged();
            }
            get { return _VisibilityAccuracy; }
        }
        private Visibility _VisibilityAccuracy = Visibility.Collapsed;
        [BrowsableAttribute(false)]
        public Visibility VisibilityIsPositive
        {
            set
            {
                if (_VisibilityIsPositive == value)
                    return;
                _VisibilityIsPositive = value;
                NotifyPropertyChanged();
            }
            get { return _VisibilityIsPositive; }
        }
        private Visibility _VisibilityIsPositive = Visibility.Collapsed;
        [BrowsableAttribute(false)]
        public Visibility VisibilityObjectName
        {
            set
            {
                if (_VisibilityObjectName == value)
                    return;
                _VisibilityObjectName = value;
                NotifyPropertyChanged();
            }
            get { return _VisibilityObjectName; }
        }
        private Visibility _VisibilityObjectName = Visibility.Collapsed;

        #endregion Visibility

        #region ITreeNode
        //        public string NodeText { get { return this.Name; } }

        #endregion ITreeNode
    }
}
