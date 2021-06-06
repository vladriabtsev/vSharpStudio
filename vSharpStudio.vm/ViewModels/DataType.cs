using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
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
            this.IsNullable = false;
            this.Length = 10;
            this.DataTypeEnum = EnumDataType.STRING;
        }
        public DataType(BigInteger maxNumericalValue, bool isPositive = false)
            : this()
        {
            BigInteger maxValue = maxNumericalValue;
            uint length = 0;
            maxValue = maxValue / 10;
            while (maxValue > 0)
            {
                length++;
                maxValue = maxValue / 10;
            }
            this.DataTypeEnum = EnumDataType.NUMERICAL;
            this.Length = length;
            this.Accuracy = 0;
            this.IsPositive = isPositive;
        }
        public DataType(EnumDataType type, uint? length = null, uint? accuracy = null, bool? isPositive = null)
        : this()
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
        public DataType(EnumDataType type, string guidOfType)
            : this()
        {
            this.DataTypeEnum = type;
            this.ObjectGuid = guidOfType;
            this.ListObjectGuids = new ObservableCollection<string>();
        }
        public override string ToString()
        {
            return DataType.GetTypeDesc(this);
        }

        #region Enumeration
        [Browsable(false)]
        public bool IsEnumStr()
        {
            if (this.DataTypeEnum != EnumDataType.ENUMERATION)
                return false;
            if (string.IsNullOrWhiteSpace(this.ObjectGuid))
                return false;
            var en = (Enumeration)this.Cfg.DicNodes[this.ObjectGuid];
            if (en.DataTypeEnum == EnumEnumerationType.STRING_VALUE)
                return true;
            return false;
        }
        [Browsable(false)]
        public EnumEnumerationType EnumerationType
        {
            get
            {
                if (this.DataTypeEnum != EnumDataType.ENUMERATION)
                    throw new NotImplementedException();
                if (string.IsNullOrWhiteSpace(this.ObjectGuid))
                    throw new NotImplementedException();
                var en = (Enumeration)this.Cfg.DicNodes[this.ObjectGuid];
                return en.DataTypeEnum;
            }
        }
        [Browsable(false)]
        public int EnumerationStrFieldLength
        {
            get
            {
                if (this.DataTypeEnum != EnumDataType.ENUMERATION)
                    throw new NotImplementedException();
                if (string.IsNullOrWhiteSpace(this.ObjectGuid))
                    throw new NotImplementedException();
                var en = (Enumeration)this.Cfg.DicNodes[this.ObjectGuid];
                int len = 0;
                if (en.DataTypeEnum == EnumEnumerationType.STRING_VALUE)
                {
                    foreach (var t in en.ListEnumerationPairs)
                    {
                        len = Math.Max(len, t.Value.Length);
                    }
                }
                return len;
            }
        }
        [Browsable(false)]
        public string EnumerationName
        {
            get
            {
                if (this.DataTypeEnum != EnumDataType.ENUMERATION)
                    throw new NotImplementedException();
                if (string.IsNullOrWhiteSpace(this.ObjectGuid))
                    return "";
                var en = (Enumeration)this.Cfg.DicNodes[this.ObjectGuid];
                return "Enum" + en.Name;
            }
        }
        #endregion Enumeration

        public static string GetTypeDesc(DataType p)
        {
            Contract.Requires(p != null);
            string res = Enum.GetName(typeof(EnumDataType), (int)p.DataTypeEnum);
            string objName = "Not found";
            ITreeConfigNode config = p.Parent;
            while (config != null && config.Parent != null)
            {
                config = config.Parent;
            }

            switch (p.DataTypeEnum)
            {
                case EnumDataType.ANY:
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
                    res += ", " + (p.IsPositive ? "+" : string.Empty) + " " + p.Length + (p.Accuracy > 0 ? "." + p.Accuracy : string.Empty) + " clr:" + p.ClrTypeName + " proto:" + p.ProtoType;
                    break;
                case EnumDataType.STRING:
                    res += ", Length:" + (p.Length > 0 ? p.Length.ToString() : " unlimited");
                    break;
                case EnumDataType.BOOL:
                    break;
                default:
                    res += " - Not supported";
                    break;
            }
            return res;
        }
        [PropertyOrderAttribute(13)]
        public string MaxValue
        {
            get
            {
                switch (this.DataTypeEnum)
                {
                    case EnumDataType.NUMERICAL:
                        if (this.Length > this.Accuracy)
                        {
                            return string.Empty.PadRight((int)(this.Length - this.Accuracy), '9');
                        }
                        else if (this.Length == this.Accuracy)
                        {
                            return "1";
                        }
                        else if (this.Length == 0)
                        {
                            return "unlimited";
                        }
                        else
                        {
                            return string.Empty;
                        }

                    case EnumDataType.STRING:
                        if (this.Length > 0)
                        {
                            return "max length " + this.Length;
                        }
                        else
                        {
                            return "unlimited";
                        }

                    default:
                        return string.Empty;
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
                        {
                            return "0.".PadRight((int)(this.Accuracy + 1), '0') + "1";
                        }
                        // if (this.Accuracy == 1)
                        //    return "0.1";
                        else
                        {
                            return "0";
                        }

                    default:
                        return string.Empty;
                }
            }
        }
        [BrowsableAttribute(false)]
        public BigInteger MaxNumericalValue
        {
            get
            {
                this._MaxNumericalValue = 1;
                if (this.Length > 0)
                {
                    for (int i = 0; i < this.Length; i++)
                    {
                        this._MaxNumericalValue *= 10;
                    }
                }
                return this._MaxNumericalValue;
            }
        }
        private BigInteger _MaxNumericalValue;
        static public uint GetLengthFromMaxValue(BigInteger maxValue)
        {
            uint length = 0;
            BigInteger m = maxValue;
            while (m > 10)
            {
                m = m / 10;
                length++;
            }
            return length;
        }
        [BrowsableAttribute(false)]
        public Type ClrType
        {
            get
            {
                return GetClrType();
            }
        }
        [BrowsableAttribute(false)]
        public string DefaultValue
        {
            get
            {
                if (this.IsNullable)
                    return "null";
                switch (this.DataTypeEnum)
                {
                    //case EnumDataType.CATALOG:
                    //    return "Catalog";
                    //case EnumDataType.CATALOGS:
                    //    return "Catalog";
                    //case EnumDataType.DOCUMENT:
                    //    return "Document";
                    //case EnumDataType.DOCUMENTS:
                    //    return "Documents";
                    //case EnumDataType.DATE:
                    //    return "Date" + sn;
                    //case EnumDataType.DATETIME:
                    //    return "DateTime" + sn;
                    //case EnumDataType.TIME:
                    //    return "Time" + sn;
                    //case EnumDataType.DATETIMEZ:
                    //    return "DateTimeZ" + sn;
                    //case EnumDataType.TIMEZ:
                    //    return "TimeZ" + sn;
                    case EnumDataType.ENUMERATION:
                        var en = (Enumeration)this.Cfg.DicNodes[this.ObjectGuid];
                        return en.DefaultValue;
                    case EnumDataType.BOOL:
                        return "false";
                    case EnumDataType.STRING:
                        return "null";
                    case EnumDataType.NUMERICAL:
                        return "0";
                    default:
                        return "null";
                }
            }
        }
        private Type GetClrType()
        {
            switch (this.ClrTypeName)
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
        [PropertyOrderAttribute(11)]
        public string ClrTypeName
        {
            get
            {
                return GetClrTypeName();
            }
        }
        [BrowsableAttribute(false)]
        public string ClrTypeNameNotNull
        {
            get
            {
                return GetClrTypeName(true);
            }
        }
        private string GetClrTypeName(bool isWithoutNull = false)
        {
            string sn = string.Empty;
            if (this.IsNullable)
            {
                if (isWithoutNull)
                    sn = "";
                else
                    sn = "?";
            }
            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/decimal
            switch (this.DataTypeEnum)
            {
                case EnumDataType.CATALOG:
                    var en = (Catalog)this.Cfg.DicNodes[this.ObjectGuid];
                    return en.Name;
                case EnumDataType.CATALOGS:
                    return "Catalog";
                case EnumDataType.DOCUMENT:
                    return "Document";
                case EnumDataType.DOCUMENTS:
                    return "Documents";
                case EnumDataType.ENUMERATION:
                    return this.EnumerationName + sn;
                case EnumDataType.DATE:
                    return "Date" + sn;
                case EnumDataType.DATETIME:
                    return "DateTime" + sn;
                case EnumDataType.TIME:
                    return "Time" + sn;
                case EnumDataType.DATETIMEZ:
                    return "DateTimeZ" + sn;
                case EnumDataType.TIMEZ:
                    return "TimeZ" + sn;
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
                            {
                                return "byte" + sn;
                            }

                            if (this.MaxNumericalValue <= ushort.MaxValue)
                            {
                                return "ushort" + sn;
                            }

                            if (this.MaxNumericalValue <= uint.MaxValue)
                            {
                                return "uint" + sn;
                            }

                            if (this.MaxNumericalValue <= ulong.MaxValue) // long, not ulong
                            {
                                return "ulong" + sn;
                            }

                            if (this.Length <= 28)
                            {
                                return "decimal" + sn;
                            }

                            throw new Exception("Not supported operation");
                            // return "BigInteger" + sn;
                        }
                        else
                        {
                            if (this.MaxNumericalValue <= sbyte.MaxValue)
                            {
                                return "sbyte" + sn;
                            }

                            if (this.MaxNumericalValue <= short.MaxValue)
                            {
                                return "short" + sn;
                            }

                            if (this.MaxNumericalValue <= int.MaxValue)
                            {
                                return "int" + sn;
                            }

                            if (this.MaxNumericalValue <= long.MaxValue)
                            {
                                return "long" + sn;
                            }

                            if (this.Length <= 28)
                            {
                                return "decimal" + sn;
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
                        if (this.Length == 0)
                        {
                            return "BigDecimal";
                        }

                        if (this.Length <= 6)
                        {
                            return "float" + sn;
                        }

                        if (this.Length <= 15)
                        {
                            return "double" + sn;
                        }

                        if (this.Length < 29)
                        {
                            return "decimal" + sn;
                        }

                        throw new Exception("Not supported operation");
                        // return "BigDecimal";
                    }
                default:
                    throw new Exception("Not supported operation");
            }
        }
        [PropertyOrderAttribute(12)]
        public string ProtoType
        {
            get
            {
                return GetProtoType();
            }
        }
        // https://docs.microsoft.com/en-us/aspnet/core/grpc/protobuf?view=aspnetcore-5.0
        public string GetProtoType(bool isGrpcService = false)
        {
            IConfig cfg = null;
            // https://developers.google.com/protocol-buffers/docs/proto3#scalar
            switch (this.DataTypeEnum)
            {
                case EnumDataType.CATALOG:
                    cfg = this.Parent.GetConfig();
                    var p = (ICompositeName)(cfg.DicNodes[this.ObjectGuid]);
                    return p.CompositeName.ToProtoName();
                case EnumDataType.CATALOGS:
                    return "catalogs";
                case EnumDataType.DOCUMENT:
                    cfg = this.Parent.GetConfig();
                    var d = (ICompositeName)(cfg.DicNodes[this.ObjectGuid]);
                    return d.CompositeName.ToProtoName();
                case EnumDataType.DOCUMENTS:
                    return "documents";
                case EnumDataType.ENUMERATION:
                    cfg = this.Parent.GetConfig();
                    var en = (IEnumeration)(cfg.DicNodes[this.ObjectGuid]);
                    if (isGrpcService)
                    {
                        switch (en.DataTypeEnum)
                        {
                            case EnumEnumerationType.BYTE_VALUE:
                            case EnumEnumerationType.INTEGER_VALUE:
                            case EnumEnumerationType.SHORT_VALUE:
                                return "int32";
                            case EnumEnumerationType.STRING_VALUE:
                                return "string";
                            default:
                                throw new NotImplementedException();
                        }
                    }
                    else
                    {
                        //if (en.DataTypeEnum == EnumEnumerationType.STRING_VALUE)
                        //    return "enum" + en.Name.ToProtoName();
                        //else
                            return en.Name.ToProtoName();
                    }
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
                            {
                                return "uint32";
                            }

                            if (this.MaxNumericalValue <= long.MaxValue) // long, not ulong
                            {
                                return "uint64";
                            }

                            return "bytes"; // need conversions
                        }
                        else
                        {
                            if (this.MaxNumericalValue <= int.MaxValue)
                            {
                                return "int32";
                            }

                            if (this.MaxNumericalValue <= long.MaxValue)
                            {
                                return "int64";
                            }

                            return "bytes"; // need conversions
                        }
                    }
                    else
                    {
                        // float   ±1.5 x 10−45   to ±3.4    x 10+38    ~6-9 digits
                        // double  ±5.0 × 10−324  to ±1.7    × 10+308   ~15-17 digits
                        // decimal ±1.0 x 10-28   to ±7.9228 x 10+28     28-29 significant digits
                        if (this.Length <= 6)
                        {
                            return "float";
                        }

                        if (this.Length <= 15)
                        {
                            return "double";
                        }

                        return "bytes"; // need conversions
                    }
                case EnumDataType.DATETIMEOFFSET:
                case EnumDataType.DATE:
                case EnumDataType.DATETIME:
                case EnumDataType.DATETIMEZ:
                case EnumDataType.TIME:
                case EnumDataType.TIMEZ:
                    return "google.protobuf.Timestamp";
                case EnumDataType.TIMESPAN:
                    return "google.protobuf.Duration";
                default:
                    throw new Exception("Not supported operation");
            }
        }
        [BrowsableAttribute(false)]
        public ITreeConfigNode Parent { get; set; }
        /// <summary>
        /// Potential data lost analysis
        /// </summary>
        /// <param name="to">New data type format</param>
        /// <returns>Description of problems. Null if there are no data lost</returns>
        public string CanLooseData(DataType to)
        {
            string res = null;

            return res;
        }

        #region Visibility
        [BrowsableAttribute(false)]
        public SortedObservableCollection<ITreeConfigNode> ListObjects
        {
            get
            {
                switch (this.DataTypeEnum)
                {
                    case EnumDataType.ENUMERATION:
                        return new SortedObservableCollection<ITreeConfigNode>(this.Cfg.Model.GroupEnumerations.ListEnumerations);
                    case EnumDataType.CATALOG:
                        return new SortedObservableCollection<ITreeConfigNode>(this.Cfg.Model.GroupCatalogs.ListCatalogs);
                    case EnumDataType.DOCUMENT:
                        return new SortedObservableCollection<ITreeConfigNode>(this.Cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments);
                    default:
                        break;
                }
                return null;
            }
        }
        partial void OnDataTypeEnumChanging(ref EnumDataType to)
        {
            switch (this.DataTypeEnum)
            {
                case EnumDataType.CATALOG:
                    this.ListObjectGuids.Clear();
                    break;
                case EnumDataType.CATALOGS:
                    this.ObjectGuid = string.Empty;
                    break;
                case EnumDataType.DOCUMENT:
                    this.ListObjectGuids.Clear();
                    break;
                case EnumDataType.DOCUMENTS:
                    this.ObjectGuid = string.Empty;
                    break;
                case EnumDataType.ENUMERATION:
                    break;
            }
        }
        partial void OnDataTypeEnumChanged()
        {
            this.NotifyPropertyChanged(nameof(this.ClrTypeName));
            this.NotifyPropertyChanged(nameof(this.ProtoType));
            switch (this.DataTypeEnum)
            {
                case EnumDataType.ANY:
                case EnumDataType.BOOL:
                case EnumDataType.DATETIME:
                case EnumDataType.DATE:
                case EnumDataType.TIME:
                case EnumDataType.DATETIMEZ:
                case EnumDataType.TIMEZ:
                case EnumDataType.CATALOGS:
                case EnumDataType.DOCUMENTS:
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Collapsed;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    this.VisibilityIsPositive = Visibility.Collapsed;
                    this.Length = 0;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ObjectGuid = string.Empty;
                    this.NotifyPropertyChanged(() => this.ListObjects);
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
                    this.ObjectGuid = string.Empty;
                    this.ListObjectGuids.Clear();
                    this.NotifyPropertyChanged(() => this.ListObjects);
                    break;
                case EnumDataType.NUMERICAL:
                    if (this.Accuracy == 0)
                    {
                        this.VisibilityIsPositive = Visibility.Visible;
                    }
                    else
                    {
                        this.VisibilityIsPositive = Visibility.Collapsed;
                    }

                    this.VisibilityAccuracy = Visibility.Visible;
                    this.VisibilityLength = Visibility.Visible;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    this.Length = 6;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ObjectGuid = string.Empty;
                    this.ListObjectGuids.Clear();
                    this.NotifyPropertyChanged(() => this.ListObjects);
                    break;
                case EnumDataType.STRING:
                    this.VisibilityIsPositive = Visibility.Collapsed;
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Visible;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    this.Length = 25;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ObjectGuid = string.Empty;
                    this.ListObjectGuids.Clear();
                    this.NotifyPropertyChanged(() => this.ListObjects);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
        partial void OnLengthChanged()
        {
            this._MaxNumericalValue = 0;
            this.NotifyPropertyChanged(nameof(this.ClrTypeName));
            this.NotifyPropertyChanged(nameof(this.ProtoType));
            this.NotifyPropertyChanged(nameof(this.MaxValue));
            this.NotifyPropertyChanged(nameof(this.MinValue));
            this.ValidateProperty(nameof(this.Accuracy));
        }
        partial void OnAccuracyChanged()
        {
            this.NotifyPropertyChanged(nameof(this.ClrTypeName));
            this.NotifyPropertyChanged(nameof(this.ProtoType));
            this.ValidateProperty(nameof(this.Length));
            this.NotifyPropertyChanged(nameof(this.MaxValue));
            this.NotifyPropertyChanged(nameof(this.MinValue));
            if (this.Accuracy == 0)
            {
                this.VisibilityIsPositive = Visibility.Visible;
            }
            else
            {
                this.VisibilityIsPositive = Visibility.Collapsed;
            }
        }
        partial void OnIsPositiveChanged()
        {
            this.NotifyPropertyChanged(nameof(this.ClrTypeName));
            this.NotifyPropertyChanged(nameof(this.ProtoType));
            this.NotifyPropertyChanged(nameof(this.MaxValue));
            this.NotifyPropertyChanged(nameof(this.MinValue));
        }
        [BrowsableAttribute(false)]
        public Visibility VisibilityLength
        {
            get
            {
                return this._VisibilityLength;
            }

            set
            {
                if (this._VisibilityLength == value)
                {
                    return;
                }
                this._VisibilityLength = value;
                this.NotifyPropertyChanged();
            }
        }
        private Visibility _VisibilityLength = Visibility.Collapsed;
        [BrowsableAttribute(false)]
        public Visibility VisibilityAccuracy
        {
            get
            {
                return this._VisibilityAccuracy;
            }

            set
            {
                if (this._VisibilityAccuracy == value)
                {
                    return;
                }
                this._VisibilityAccuracy = value;
                this.NotifyPropertyChanged();
            }
        }
        private Visibility _VisibilityAccuracy = Visibility.Collapsed;
        [BrowsableAttribute(false)]
        public Visibility VisibilityIsPositive
        {
            get
            {
                return this._VisibilityIsPositive;
            }

            set
            {
                if (this._VisibilityIsPositive == value)
                {
                    return;
                }
                this._VisibilityIsPositive = value;
                this.NotifyPropertyChanged();
            }
        }
        private Visibility _VisibilityIsPositive = Visibility.Collapsed;
        [BrowsableAttribute(false)]
        public Visibility VisibilityObjectName
        {
            get
            {
                return this._VisibilityObjectName;
            }

            set
            {
                if (this._VisibilityObjectName == value)
                {
                    return;
                }
                this._VisibilityObjectName = value;
                this.NotifyPropertyChanged();
            }
        }
        private Visibility _VisibilityObjectName = Visibility.Collapsed;
        #endregion Visibility

        public Config Cfg
        {
            get
            {
                if (this.cfg == null)
                {
                    var p = this.Parent;
                    if (p == null)
                    {
                        return null;
                    }

                    while (p.Parent != null)
                    {
                        p = p.Parent;
                    }

                    this.cfg = p as Config;
                }
                return this.cfg;
            }
        }
        private Config cfg = null;
        public IDataType PrevStableVersion()
        {
            IDataType res = null;
            if (this.Cfg != null && this.Cfg.PrevStableConfig != null && this.Cfg.PrevStableConfig.DicNodes.ContainsKey(this.Parent.Guid))
            {
                res = (this.Cfg.PrevStableConfig.DicNodes[this.Parent.Guid] as IDataTypeObject).IDataType;
            }
            return res;
        }
        public IDataType PrevCurrentVersion()
        {
            IDataType res = null;
            if (this.Cfg != null && this.Cfg.PrevCurrentConfig != null && this.Cfg.PrevCurrentConfig.DicNodes.ContainsKey(this.Parent.Guid))
            {
                res = (this.Cfg.PrevCurrentConfig.DicNodes[this.Parent.Guid] as IDataTypeObject).IDataType;
            }
            return res;
        }
    }
}
