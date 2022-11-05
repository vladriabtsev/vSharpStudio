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
        partial void OnCreating()
        {
            this.ListObjectGuids = new ObservableCollectionWithActions<string>();
        }
        partial void OnCreated()
        {
            this.Length = 10;
            this.DataTypeEnum = EnumDataType.STRING;
            //Init();
        }
        //protected override void OnInitFromDto()
        //{
        //    Init();
        //}
        //private void Init()
        //{
        //    this.ListRoles.OnAddingAction = (t) =>
        //    {
        //        t.IsNew = true;
        //    };
        //    this.ListRoles.OnAddedAction = (t) =>
        //    {
        //        t.OnAdded();
        //    };
        //    this.ListRoles.OnRemovedAction = (t) => {
        //        this.OnRemoveChild();
        //    };
        //    this.ListRoles.OnClearedAction = () => {
        //        this.OnRemoveChild();
        //    };
        //}
        public DataType(BigInteger maxNumericalValue, bool isPositive = false) : this()
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
            this.ListObjectGuids = new ObservableCollectionWithActions<string>();
        }
        public override string ToString()
        {
            return DataType.GetTypeDesc(this)!;
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

        public static string? GetTypeDesc(DataType p)
        {
            Debug.Assert(p != null);
            string res = Enum.GetName(typeof(EnumDataType), (int)p.DataTypeEnum)!;
            string objName = "Not found";
            ITreeConfigNode par = p.Parent;
            while (par != null && par.Parent != null)
            {
                par = par.Parent;
            }
            var config = (Config)par!;
            switch (p.DataTypeEnum)
            {
                case EnumDataType.ANY:
                    throw new NotImplementedException();
                case EnumDataType.CATALOG:
                    foreach (var t in config.Model.GroupCatalogs.ListCatalogs)
                    {
                        if (p.ObjectGuid == t.Guid)
                        {
                            objName = t.Name;
                        }
                    }
                    res += ": " + objName;
                    break;
                case EnumDataType.CATALOGS:
                    throw new NotImplementedException();
                case EnumDataType.DOCUMENT:
                    foreach (var t in config.Model.GroupDocuments.GroupListDocuments.ListDocuments)
                    {
                        if (p.ObjectGuid == t.Guid)
                        {
                            objName = t.Name;
                        }
                    }
                    res += ": " + objName;
                    break;
                case EnumDataType.DOCUMENTS:
                    break;
                case EnumDataType.ENUMERATION:
                    foreach (var t in config.Model.GroupEnumerations.ListEnumerations)
                    {
                        if (p.ObjectGuid == t.Guid)
                        {
                            objName = t.Name;
                        }
                    }
                    res += ": " + objName;
                    break;
                case EnumDataType.NUMERICAL:
                    res += ", " + (p.IsPositive ? "+" : string.Empty) + " " + p.Length + (p.Accuracy > 0 ? "." + p.Accuracy : string.Empty) + " clr:" + p.ClrTypeName; // + " proto:" + p.ProtoType;
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
        [PropertyOrderAttribute(14)]
        [DisplayName("Min Len")]
        [Description("Min value based on Length")]
        public string MinValue
        {
            get { if (_MinValue == null) MinValueCalc(); Debug.Assert(_MinValue != null); return _MinValue; }
            set
            {
                if (this._MinValue != value)
                {
                    this._MinValue = value;
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string? _MinValue = null;
        private void MinValueCalc()
        {
            switch (this.DataTypeEnum)
            {
                case EnumDataType.NUMERICAL:
                    if (this.Accuracy > 0)
                    {
                        this.MinValue = "0.".PadRight((int)(this.Accuracy + 1), '0') + "1";
                    }
                    // if (this.Accuracy == 1)
                    //    return "0.1";
                    else
                    {
                        this.MinValue = "0";
                    }
                    break;
                default:
                    this.MinValue = string.Empty;
                    break;
            }
        }
        [PropertyOrderAttribute(16)]
        [DisplayName("Max Len")]
        [Description("Max value based on Length")]
        public string MaxValue
        {
            get { if (_MaxValue == null) MaxValueCalc(); Debug.Assert(_MaxValue != null); return _MaxValue; }
            set
            {
                if (this._MaxValue != value)
                {
                    this._MaxValue = value;
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string? _MaxValue = null;
        private void MaxValueCalc()
        {
            switch (this.DataTypeEnum)
            {
                case EnumDataType.NUMERICAL:
                    if (this.Length > this.Accuracy)
                    {
                        this.MaxValue = string.Empty.PadRight((int)(this.Length - this.Accuracy), '9');
                        if (this.Accuracy > 0)
                            this.MaxValue += "." + string.Empty.PadRight((int)(this.Accuracy), '9');
                    }
                    else if (this.Length == this.Accuracy)
                    {
                        this.MaxValue = "1";
                    }
                    else if (this.Length == 0)
                    {
                        this.MaxValue = "unlimited";
                    }
                    else
                    {
                        this.MaxValue = string.Empty;
                    }
                    break;
                case EnumDataType.STRING:
                    if (this.Length > 0)
                    {
                        this.MaxValue = "max length " + this.Length;
                    }
                    else
                    {
                        this.MaxValue = "unlimited";
                    }
                    break;
                default:
                    this.MaxValue = string.Empty;
                    break;
            }
        }
        [BrowsableAttribute(false)]
        public BigInteger? MaxNumericalValue
        {
            get { if (_MaxNumericalValue == null) ClrTypeNameCalc(); return _MaxNumericalValue; }
            set
            {
                if (this._MaxNumericalValue != value)
                {
                    this._MaxNumericalValue = value;
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private BigInteger? _MaxNumericalValue = null;
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
            get { if (_ClrTypeName == null) ClrTypeNameCalc(); return _ClrTypeName!; }
            set
            {
                if (this._ClrTypeName != value)
                {
                    this._ClrTypeName = value;
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string? _ClrTypeName = null;
        [BrowsableAttribute(false)]
        public string ClrLiteralSuf
        {
            get { if (_ClrLiteralSuf == null) ClrTypeNameCalc(); Debug.Assert(_ClrLiteralSuf != null); return _ClrLiteralSuf!; }
            set
            {
                if (this._ClrLiteralSuf != value)
                {
                    this._ClrLiteralSuf = value;
                    this.NotifyPropertyChanged();
                    this.ValidateProperty();
                }
            }
        }
        private string? _ClrLiteralSuf = null;
        //[BrowsableAttribute(false)]
        //public string ClrTypeNameNotNull
        //{
        //    get { if (_ClrTypeNameNotNull == null) ClrTypeNameCalc(); return _ClrTypeNameNotNull; }
        //    set
        //    {
        //        if (this._ClrTypeNameNotNull != value)
        //        {
        //            this._ClrTypeNameNotNull = value;
        //            this.NotifyPropertyChanged();
        //            this.ValidateProperty();
        //        }
        //    }
        //}
        //private string _ClrTypeNameNotNull = null;
        private void ClrTypeNameCalc()
        {
            this.ClrLiteralSuf = "";
            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/decimal
            switch (this.DataTypeEnum)
            {
                case EnumDataType.CATALOG:
                    var en = (Catalog)this.Cfg.DicNodes[this.ObjectGuid];
                    this.ClrTypeName = en.Name;
                    break;
                case EnumDataType.CATALOGS:
                    this.ClrTypeName = "Catalog";
                    break;
                case EnumDataType.DOCUMENT:
                    this.ClrTypeName = "Document";
                    break;
                case EnumDataType.DOCUMENTS:
                    this.ClrTypeName = "Documents";
                    break;
                case EnumDataType.ENUMERATION:
                    this.ClrTypeName = this.EnumerationName;
                    break;
                case EnumDataType.TIME:
                    //case EnumDataType.TIMEZ:
                    this.ClrTypeName = "TimeOnly";
                    break;
                case EnumDataType.DATE:
                    this.ClrTypeName = "DateOnly";
                    break;
                //case EnumDataType.DATETIMEZ:
                //    this.ClrTypeName = "DateTimeOffset";
                //    break;
                case EnumDataType.DATETIMELOCAL:
                case EnumDataType.DATETIMEUTC:
                    //case EnumDataType.DATETIME:
                    this.ClrTypeName = "DateTime";
                    break;
                //case EnumDataType.DATETIMEOFFSET:
                //    return "DateTimeOffset" + sn;
                //case EnumDataType.TIMESPAN:
                //    return "TimeSpan" + sn;
                case EnumDataType.BOOL:
                    this.ClrTypeName = "bool";
                    break;
                case EnumDataType.CHAR:
                    this.ClrTypeName = "char";
                    break;
                case EnumDataType.STRING:
                    this.ClrTypeName = "string";
                    break;
                case EnumDataType.NUMERICAL:
                    BigInteger mv = 1;
                    if (this.Length > 0)
                    {
                        for (int i = 0; i < this.Length; i++)
                        {
                            mv *= 10;
                        }
                    }
                    this.MaxNumericalValue = mv;
                    if (this.Accuracy == 0)
                    {
                        if (this.IsPositive)
                        {
                            if (this.MaxNumericalValue <= byte.MaxValue)
                            {
                                this.ClrTypeName = "byte";
                                this.ClrLiteralSuf = "U";
                                break;
                            }
                            if (this.MaxNumericalValue <= ushort.MaxValue)
                            {
                                this.ClrTypeName = "ushort";
                                this.ClrLiteralSuf = "U";
                                break;
                            }
                            if (this.MaxNumericalValue <= uint.MaxValue)
                            {
                                this.ClrTypeName = "uint";
                                this.ClrLiteralSuf = "U";
                                break;
                            }
                            if (this.MaxNumericalValue <= ulong.MaxValue) // long, not ulong
                            {
                                this.ClrTypeName = "ulong";
                                this.ClrLiteralSuf = "LU";
                                break;
                            }
                            if (this.Length <= 28)
                            {
                                this.ClrTypeName = "decimal";
                                this.ClrLiteralSuf = "m";
                                break;
                            }
                            throw new Exception("Not supported operation");
                            // return "BigInteger" + sn;
                        }
                        else
                        {
                            if (this.MaxNumericalValue <= sbyte.MaxValue)
                            {
                                this.ClrTypeName = "sbyte";
                                break;
                            }
                            if (this.MaxNumericalValue <= short.MaxValue)
                            {
                                this.ClrTypeName = "short";
                                break;
                            }
                            if (this.MaxNumericalValue <= int.MaxValue)
                            {
                                this.ClrTypeName = "int";
                                break;
                            }
                            if (this.MaxNumericalValue <= long.MaxValue)
                            {
                                this.ClrTypeName = "long";
                                this.ClrLiteralSuf = "L";
                                break;
                            }
                            if (this.Length <= 28)
                            {
                                this.ClrTypeName = "decimal";
                                this.ClrLiteralSuf = "m";
                                break;
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
                            this.ClrTypeName = "BigDecimal";
                            break;
                        }
                        if (this.Length <= 6)
                        {
                            this.ClrTypeName = "float";
                            this.ClrLiteralSuf = "f";
                            break;
                        }
                        if (this.Length <= 15)
                        {
                            this.ClrTypeName = "double";
                            this.ClrLiteralSuf = "d";
                            break;
                        }
                        if (this.Length < 29)
                        {
                            this.ClrTypeName = "decimal";
                            this.ClrLiteralSuf = "m";
                            break;
                        }
                        throw new Exception("Not supported operation");
                        // return "BigDecimal";
                    }
                default:
                    throw new Exception("Not supported operation");
            }
        }
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
        public SortedObservableCollection<ITreeConfigNodeSortable>? ListObjects
        {
            get
            {
                switch (this.DataTypeEnum)
                {
                    case EnumDataType.ENUMERATION:
                        return new SortedObservableCollection<ITreeConfigNodeSortable>(this.Cfg.Model.GroupEnumerations.ListEnumerations);
                    case EnumDataType.CATALOG:
                        return new SortedObservableCollection<ITreeConfigNodeSortable>(this.Cfg.Model.GroupCatalogs.ListCatalogs);
                    case EnumDataType.DOCUMENT:
                        return new SortedObservableCollection<ITreeConfigNodeSortable>(this.Cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments);
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
            if (!this.IsNotifying)
                return;
            if (this.Cfg == null)
                return;
            switch (this.DataTypeEnum)
            {
                case EnumDataType.CHAR:
                case EnumDataType.ANY:
                case EnumDataType.BOOL:
                case EnumDataType.DATE:
                case EnumDataType.DATETIMELOCAL:
                case EnumDataType.DATETIMEUTC:
                //case EnumDataType.DATETIME:
                //case EnumDataType.DATETIMEZ:
                //case EnumDataType.DATETIMEOFFSET:
                case EnumDataType.TIME:
                //case EnumDataType.TIMEZ:
                //case EnumDataType.TIMESPAN:
                case EnumDataType.CATALOGS:
                case EnumDataType.DOCUMENTS:
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Collapsed;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    this.VisibilityIsPositive = Visibility.Collapsed;
                    this._Length = 0;
                    this._Accuracy = 0;
                    this._IsPositive = false;
                    this._ObjectGuid = string.Empty;
                    this.NotifyPropertyChanged(() => this.ListObjects);
                    break;
                case EnumDataType.CATALOG:
                case EnumDataType.DOCUMENT:
                case EnumDataType.ENUMERATION:
                    this.VisibilityIsPositive = Visibility.Collapsed;
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Collapsed;
                    this.VisibilityObjectName = Visibility.Visible;
                    this._Length = 0;
                    this._Accuracy = 0;
                    this._IsPositive = false;
                    this._ObjectGuid = string.Empty;
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
                    this._Length = 6;
                    this._Accuracy = 0;
                    this._IsPositive = false;
                    this._ObjectGuid = string.Empty;
                    this.ListObjectGuids.Clear();
                    this.NotifyPropertyChanged(() => this.ListObjects);
                    break;
                case EnumDataType.STRING:
                    this.VisibilityIsPositive = Visibility.Collapsed;
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Visible;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    this._Length = 25;
                    this._Accuracy = 0;
                    this._IsPositive = false;
                    this._ObjectGuid = string.Empty;
                    this.ListObjectGuids.Clear();
                    this.NotifyPropertyChanged(() => this.ListObjects);
                    break;
                default:
                    throw new NotSupportedException();
            }
            ClrTypeNameCalc();
            MinValueCalc();
            MaxValueCalc();
            this.NotifyPropertyChanged(() => this.Length);
            this.NotifyPropertyChanged(() => this.Accuracy);
            this.NotifyPropertyChanged(() => this.IsPositive);
            this.NotifyPropertyChanged(() => this.ObjectGuid);
        }
        partial void OnLengthChanged()
        {
            if (!this.IsNotifying)
                return;
            if (this.Cfg == null)
                return;
            this._MaxNumericalValue = 0;
            ClrTypeNameCalc();
            MaxValueCalc();
            this.ValidateProperty(nameof(this.Accuracy));
        }
        partial void OnAccuracyChanged()
        {
            if (!this.IsNotifying)
                return;
            if (this.Cfg == null)
                return;
            ClrTypeNameCalc();
            MaxValueCalc();
            MinValueCalc();
            this.ValidateProperty(nameof(this.Length));
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
            if (!this.IsNotifying)
                return;
            if (this.Cfg == null)
                return;
            ClrTypeNameCalc();
            MaxValueCalc();
            MinValueCalc();
        }
        //partial void OnIsNullableChanged()
        //{
        //    if (!this.IsNotifying)
        //        return;
        //    if (this.Cfg == null)
        //        return;
        //    ClrTypeNameCalc();
        //    MaxValueCalc();
        //    MinValueCalc();
        //}
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

        public Config? Cfg
        {
            get
            {
                if (this.cfg == null)
                {
                    var p = this.Parent;
                    //Debug.Assert(p != null);
                    while (p != null && p.Parent != null)
                        p = p.Parent;
                    if (p is Config c)
                    this.cfg = c;
                }
                return this.cfg;
            }
        }
        private Config? cfg = null;
        public IDataType? PrevStableVersion()
        {
            IDataType res = null;
            if (this.Cfg != null && this.Cfg.PrevStableConfig != null && this.Cfg.PrevStableConfig.DicNodes.ContainsKey(this.Parent.Guid))
            {
                res = (this.Cfg.PrevStableConfig.DicNodes[this.Parent.Guid] as IDataTypeObject)!.IDataType;
            }
            return res;
        }
        public IDataType? PrevCurrentVersion()
        {
            IDataType? res = null;
            if (this.Cfg != null && this.Cfg.PrevCurrentConfig != null && this.Cfg.PrevCurrentConfig.DicNodes.ContainsKey(this.Parent.Guid))
            {
                res = (this.Cfg.PrevCurrentConfig.DicNodes[this.Parent.Guid] as IDataTypeObject)!.IDataType;
            }
            return res;
        }
        protected override void OnIsChangedChanged()
        {
            if (this.Parent != null && this.IsChanged)
                this.Parent.IsChanged = true;
        }
    }
}
