using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Dummy;
using FluentValidation;
using ViewModelBase;
using static Proto.Config.proto_data_type.Types;

namespace vSharpStudio.vm.ViewModels
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?view=netframework-4.7.2
    [DebuggerDisplay("DataType:{DataType.GetTypeDesc(this),nq}")]
    public partial class DataType
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
            this.TypeGuid = guidOfType;
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
                    res += " " + p.ObjectName;
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Catalogs:
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Constant:
                    res += " " + p.ObjectName;
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Document:
                    res += " " + p.ObjectName;
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Documents:
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Enumeration:
                    res += " " + p.ObjectName;
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Numerical:
                    res += " Length:" + p.Length + " Accuracy:" + p.Accuracy + " Min:" + p.MinValueString + " Max:" + p.MaxValueString;
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.String:
                    res += " Length:" + p.Length + " Min:" + p.MinValueString;
                    break;
                default:
                    res += " - Not supported";
                    break;
            }
            return res;
        }
        public BigInteger MinValue
        {
            set
            {
                if (_MinValue != value)
                {
                    _MinValue = value;
                    NotifyPropertyChanged();
                    ValidateProperty();
                    this.MinValueString = _MinValue.ToString();
                }
            }
            get
            {
                if (_MinValue == null)
                {
                    if (BigInteger.TryParse(this.MinValueString, out var v))
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
                    NotifyPropertyChanged();
                    ValidateProperty();
                    this.MaxValueString = _MaxValue.ToString();
                }
            }
            get
            {
                if (_MaxValue == null)
                {
                    if (BigInteger.TryParse(this.MaxValueString, out var v))
                        _MaxValue = v;
                }
                return _MaxValue;
            }
        }
        private BigInteger _MaxValue;

        #region Visibility

        partial void OnDataTypeEnumChanged()
        {
            switch(this.DataTypeEnum)
            {
                case EnumDataType.Any:
                case EnumDataType.Bool:
                case EnumDataType.Catalogs:
                case EnumDataType.Documents:
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Collapsed;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    break;
                case EnumDataType.Catalog:
                case EnumDataType.Constant:
                case EnumDataType.Document:
                case EnumDataType.Enumeration:
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Collapsed;
                    this.VisibilityObjectName = Visibility.Visible;
                    break;
                case EnumDataType.Numerical:
                    this.VisibilityAccuracy = Visibility.Visible;
                    this.VisibilityLength = Visibility.Visible;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    break;
                case EnumDataType.String:
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Visible;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    break;
            }
        }
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
        private Visibility _VisibilityLength= Visibility.Collapsed;
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
