using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Windows;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?view=netframework-4.7.2
    [DebuggerDisplay("DataType:{DataType.GetTypeDesc(this),nq}")]
    public partial class DataType : IParent
    {
        partial void OnInit()
        {
            this.IsNullable = true;
            this.Length = 10;
            this.DataTypeEnum = EnumDataType.STRING;
        }
        public DataType(EnumDataType type, uint? length = null, uint? accuracy = null, bool? isPositive = null) : this()
        {
            this.DataTypeEnum = type;
            switch (this.DataTypeEnum)
            {
                case EnumDataType.ANY:
                    break;
                case EnumDataType.BOOL:
                    break;
                case EnumDataType.CATALOGS:
                    break;
                case EnumDataType.DOCUMENTS:
                    break;
                case EnumDataType.NUMERICAL:
                    // TODO revisit default length and accuracy for Numerical
                    this.Length = length ?? 16;
                    this.Accuracy = accuracy ?? 2;
                    this.IsPositive = isPositive ?? false;
                    break;
                case EnumDataType.ENUMERATION:
                    break;
                case EnumDataType.STRING:
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
            this.ListObjectGuids = new ObservableCollection<string>();
        }
        public override string ToString()
        {
            return DataType.GetTypeDesc(this);
        }
        public static string GetTypeDesc(DataType p)
        {
            string res = Enum.GetName(typeof(EnumDataType), (int)p.DataTypeEnum);
            string objName = "Not found";
            ITreeConfigNode config = p.Parent;
            while (config != null && config.Parent != null)
                config = config.Parent;
            switch (p.DataTypeEnum)
            {
                case EnumDataType.ANY:
                    throw new NotImplementedException();
                case EnumDataType.BOOL:
                    throw new NotImplementedException();
                case EnumDataType.CATALOG:
                    if (config is Config)
                    {
                        foreach (var t in (config as Config).Model.GroupCatalogs.ListCatalogs)
                        {
                            if (p.ObjectGuid == t.Guid)
                            {
                                objName = t.Name;
                            }
                        }
                        res += ": " + objName;
                    }
                    break;
                case EnumDataType.CATALOGS:
                    throw new NotImplementedException();
                case EnumDataType.DOCUMENT:
                    if (config is Config)
                    {
                        foreach (var t in (config as Config).Model.GroupDocuments.GroupListDocuments.ListDocuments)
                        {
                            if (p.ObjectGuid == t.Guid)
                            {
                                objName = t.Name;
                            }
                        }
                        res += ": " + objName;
                    }
                    break;
                case EnumDataType.DOCUMENTS:
                    break;
                case EnumDataType.ENUMERATION:
                    if (config is Config)
                    {
                        foreach (var t in (config as Config).Model.GroupEnumerations.ListEnumerations)
                        {
                            if (p.ObjectGuid == t.Guid)
                            {
                                objName = t.Name;
                            }
                        }
                        res += ": " + objName;
                    }
                    break;
                case EnumDataType.NUMERICAL:
                    res += ", " + (p.IsPositive ? "+" : "") + " " + p.Length + (p.Accuracy > 0 ? "." + p.Accuracy : "") + " clr:" + p.ClrTypeName + " proto:" + p.ProtoType;
                    break;
                case EnumDataType.STRING:
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
        public string MaxValue
        {
            get
            {
                switch (this.DataTypeEnum)
                {
                    case EnumDataType.NUMERICAL:
                        if (this.Length > this.Accuracy)
                            return "".PadRight((int)(this.Length - this.Accuracy), '9');
                        else if (this.Length == this.Accuracy)
                            return "1";
                        else if (this.Length == 0)
                            return "unlimited";
                        else
                            return "";
                    case EnumDataType.STRING:
                        if (this.Length > 0)
                            return "max length " + this.Length;
                        else
                            return "unlimited";
                    default:
                        return "";
                }
            }
        }
        [PropertyOrderAttribute(14)]
        public string MinValue
        {
            get
            {
                switch (this.DataTypeEnum)
                {
                    case EnumDataType.NUMERICAL:
                        if (this.Accuracy > 0)
                            return "0.".PadRight((int)(this.Accuracy + 1), '0') + "1";
                        //if (this.Accuracy == 1)
                        //    return "0.1";
                        else
                            return "0";
                    default:
                        return "";
                }
            }
        }
        [BrowsableAttribute(false)]
        public BigInteger MaxNumericalValue
        {
            get
            {
                _MaxNumericalValue = 1;
                if (this.Length > 0)
                {
                    for (int i = 0; i < this.Length; i++)
                        _MaxNumericalValue *= 10;
                }
                return _MaxNumericalValue;
            }
        }
        private BigInteger _MaxNumericalValue;
        [BrowsableAttribute(false)]
        public Type ClrType
        {
            get
            {
                switch (ClrTypeName)
                {
                    case "DateTime":
                        return typeof(DateTime);
                    case "DateTime?":
                        return typeof(DateTime?);
                    case "bool":
                        return typeof(bool);
                    case "bool?":
                        return typeof(bool?);
                    case "string":
                        return typeof(string);
                    case "byte":
                        return typeof(byte);
                    case "byte?":
                        return typeof(byte?);
                    case "ushort":
                        return typeof(ushort);
                    case "ushort?":
                        return typeof(ushort?);
                    case "uint":
                        return typeof(uint);
                    case "uint?":
                        return typeof(uint?);
                    case "ulong":
                        return typeof(ulong);
                    case "ulong?":
                        return typeof(ulong?);
                    case "BigInteger":
                        return typeof(BigInteger);
                    case "BigInteger?":
                        return typeof(BigInteger?);
                    case "sbyte":
                        return typeof(sbyte);
                    case "sbyte?":
                        return typeof(sbyte?);
                    case "short":
                        return typeof(short);
                    case "short?":
                        return typeof(short?);
                    case "int":
                        return typeof(int);
                    case "int?":
                        return typeof(int?);
                    case "long":
                        return typeof(long);
                    case "long?":
                        return typeof(long?);
                    case "BigDecimal":
                        return typeof(BigDecimal);
                    case "BigDecimal?":
                        return typeof(BigDecimal?);
                    case "float":
                        return typeof(float);
                    case "float?":
                        return typeof(float?);
                    case "double":
                        return typeof(double);
                    case "double?":
                        return typeof(double?);
                    case "decimal":
                        return typeof(decimal);
                    case "decimal?":
                        return typeof(decimal?);
                    default:
                        throw new Exception("Not supported operation");
                }
            }
        }
        [PropertyOrderAttribute(11)]
        public string ClrTypeName
        {
            get
            {
                string sn = "";
                if (this.IsNullable)
                    sn = "?";
                // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/decimal
                switch (this.DataTypeEnum)
                {
                    case EnumDataType.CATALOG:
                        return "Catalog";
                    case EnumDataType.CATALOGS:
                        return "Catalog";
                    case EnumDataType.DOCUMENT:
                        return "Document";
                    case EnumDataType.DOCUMENTS:
                        return "Documents";
                    case EnumDataType.ENUMERATION:
                        return "Enumeration";
                    case EnumDataType.DATE:
                    case EnumDataType.DATETIME:
                    case EnumDataType.TIME:
                        return "DateTime" + sn;
                    case EnumDataType.BOOL:
                        return "bool" + sn;
                    case EnumDataType.STRING:
                        return "string";
                    case EnumDataType.NUMERICAL:
                        if (this.Accuracy == 0)
                        {
                            if (this.IsPositive)
                            {
                                if (this.MaxNumericalValue <= byte.MaxValue)
                                    return "byte" + sn;
                                if (this.MaxNumericalValue <= ushort.MaxValue)
                                    return "ushort" + sn;
                                if (this.MaxNumericalValue <= uint.MaxValue)
                                    return "uint" + sn;
                                if (this.MaxNumericalValue <= ulong.MaxValue) // long, not ulong
                                    return "ulong" + sn;
                                if (this.Length <= 28) 
                                    return "decimal" + sn;
                                throw new Exception("Not supported operation");
                                //return "BigInteger" + sn;
                            }
                            else
                            {
                                if (this.MaxNumericalValue <= sbyte.MaxValue)
                                    return "sbyte" + sn;
                                if (this.MaxNumericalValue <= short.MaxValue)
                                    return "short" + sn;
                                if (this.MaxNumericalValue <= int.MaxValue)
                                    return "int" + sn;
                                if (this.MaxNumericalValue <= long.MaxValue)
                                    return "long" + sn;
                                if (this.Length <= 28)
                                    return "decimal" + sn;
                                throw new Exception("Not supported operation");
                                //return "BigInteger" + sn;
                            }
                        }
                        else
                        {
                            // float   ±1.5 x 10−45   to ±3.4    x 10+38    ~6-9 digits
                            // double  ±5.0 × 10−324  to ±1.7    × 10+308   ~15-17 digits
                            // decimal ±1.0 x 10-28   to ±7.9228 x 10+28     28-29 significant digits
                            if (this.Length == 0)
                                return "BigDecimal";
                            if (this.Length <= 6)
                                return "float" + sn;
                            if (this.Length <= 15)
                                return "double" + sn;
                            if (this.Length < 29)
                                return "decimal" + sn;
                            throw new Exception("Not supported operation");
                            //return "BigDecimal";
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
                    case EnumDataType.CATALOG:
                        return "catalog";
                    case EnumDataType.CATALOGS:
                        return "catalogs";
                    case EnumDataType.DOCUMENT:
                        return "document";
                    case EnumDataType.DOCUMENTS:
                        return "documents";
                    case EnumDataType.ENUMERATION:
                        return "enumeration";
                    case EnumDataType.BOOL:
                        return "bool";
                    case EnumDataType.STRING:
                        return "string";
                    case EnumDataType.NUMERICAL:
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

        [BrowsableAttribute(false)]
        public SortedObservableCollection<ITreeConfigNode> ListObjects
        {
            set
            {
                if (_ListObjects != value)
                {
                    _ListObjects = value;
                    NotifyPropertyChanged();
                }
            }
            get
            {
                if (_ListObjects == null || _ListObjects.Count == 0)
                {
                    switch (this.DataTypeEnum)
                    {
                        case EnumDataType.CATALOG:
                        case EnumDataType.DOCUMENT:
                        case EnumDataType.ENUMERATION:
                            UpdateListObjects();
                            break;
                        default:
                            break;
                    }
                }
                return _ListObjects;
            }
        }
        private SortedObservableCollection<ITreeConfigNode> _ListObjects;
        partial void OnDataTypeEnumChanging()
        {
            switch (this.DataTypeEnum)
            {
                case EnumDataType.CATALOG:
                    this.ListObjectGuids.Clear();
                    break;
                case EnumDataType.CATALOGS:
                    this.ObjectGuid = "";
                    break;
                case EnumDataType.DOCUMENT:
                    this.ListObjectGuids.Clear();
                    break;
                case EnumDataType.DOCUMENTS:
                    this.ObjectGuid = "";
                    break;
                case EnumDataType.ENUMERATION:
                    break;
            }
        }
        partial void OnDataTypeEnumChanged()
        {
            this.NotifyPropertyChanged(p => p.ClrTypeName);
            this.NotifyPropertyChanged(p => p.ProtoType);
            switch (this.DataTypeEnum)
            {
                case EnumDataType.ANY:
                case EnumDataType.BOOL:
                case EnumDataType.DATETIME:
                case EnumDataType.DATE:
                case EnumDataType.TIME:
                case EnumDataType.CATALOGS:
                case EnumDataType.DOCUMENTS:
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Collapsed;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    this.VisibilityIsPositive = Visibility.Collapsed;
                    this.Length = 0;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ObjectGuid = "";
                    this.ListObjects = null;
                    break;
                case EnumDataType.CATALOG:
                case EnumDataType.DOCUMENT:
                case EnumDataType.ENUMERATION:
                    this.VisibilityIsPositive = Visibility.Collapsed;
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Collapsed;
                    this.VisibilityObjectName = Visibility.Visible;
                    this.Length = 0;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ObjectGuid = "";
                    this.ListObjects = null;
                    this.ListObjectGuids.Clear();
                    UpdateListObjects();
                    break;
                case EnumDataType.NUMERICAL:
                    if (this.Accuracy == 0)
                        this.VisibilityIsPositive = Visibility.Visible;
                    else
                        this.VisibilityIsPositive = Visibility.Collapsed;
                    this.VisibilityAccuracy = Visibility.Visible;
                    this.VisibilityLength = Visibility.Visible;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    this.Length = 6;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ObjectGuid = "";
                    this.ListObjects = null;
                    this.ListObjectGuids.Clear();
                    break;
                case EnumDataType.STRING:
                    this.VisibilityIsPositive = Visibility.Collapsed;
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Visible;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    this.Length = 25;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ObjectGuid = "";
                    this.ListObjects = null;
                    this.ListObjectGuids.Clear();
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        private void UpdateListObjects()
        {
            ITreeConfigNode config = this.Parent;
            while (config != null && config.Parent != null)
                config = config.Parent;
            if (config is Config)
            {
                if (this.ListObjects != null)
                    this.ListObjects.Clear();
                switch (this.DataTypeEnum)
                {
                    case EnumDataType.ENUMERATION:
                        this.ListObjects = new SortedObservableCollection<ITreeConfigNode>((config as Config).Model.GroupEnumerations.ListEnumerations);
                        break;
                    case EnumDataType.CATALOG:
                        this.ListObjects = new SortedObservableCollection<ITreeConfigNode>((config as Config).Model.GroupCatalogs.ListCatalogs);
                        break;
                    case EnumDataType.DOCUMENT:
                        this.ListObjects = new SortedObservableCollection<ITreeConfigNode>((config as Config).Model.GroupDocuments.GroupListDocuments.ListDocuments);
                        break;
                    default:
                        break;
                }
            }
        }

        partial void OnLengthChanged()
        {
            _MaxNumericalValue = 0;
            this.NotifyPropertyChanged(p => p.ClrTypeName);
            this.NotifyPropertyChanged(p => p.ProtoType);
            this.NotifyPropertyChanged(p => p.MaxValue);
            this.NotifyPropertyChanged(p => p.MinValue);
            this.ValidateProperty(p => p.Accuracy);
        }
        partial void OnAccuracyChanged()
        {
            this.NotifyPropertyChanged(p => p.ClrTypeName);
            this.NotifyPropertyChanged(p => p.ProtoType);
            this.ValidateProperty(p => p.Length);
            this.NotifyPropertyChanged(p => p.MaxValue);
            this.NotifyPropertyChanged(p => p.MinValue);
            if (this.Accuracy == 0)
                this.VisibilityIsPositive = Visibility.Visible;
            else
                this.VisibilityIsPositive = Visibility.Collapsed;
        }
        partial void OnIsPositiveChanged()
        {
            this.NotifyPropertyChanged(p => p.ClrTypeName);
            this.NotifyPropertyChanged(p => p.ProtoType);
            this.NotifyPropertyChanged(p => p.MaxValue);
            this.NotifyPropertyChanged(p => p.MinValue);
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
    }
}
