using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using CommunityToolkit.Diagnostics;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?view=netframework-4.7.2
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class DataType : IParent
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Type:{DataType.GetTypeDesc(this)}";
        }
        partial void OnCreating()
        {
            this._ListObjectRefs = new ObservableCollectionWithActions<ComplexRef>();
            this._ListObjectRefs.CollectionChanged += ListObjectRefs_CollectionChanged;
        }
        private void ListObjectRefs_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var t in e.NewItems)
                {
                    var cr = (ComplexRef)t;
                    this.GetPositions(cr);
                }
            }
        }
        private uint GetNextPosition()
        {
            if (this.Parent is Property tp)
            {
                if (tp.Parent is GroupListProperties)
                {
                    return tp.ParentGroupListProperties.GetNextPosition();
                }
                else if (tp.Parent is DocumentTimeline dt)
                {
                    return dt.GetNextPosition();
                }
                else if (tp.Parent is RelationManyToMany rm)
                {
                    //tp.PositionOfDescr = rm.PropertyRefObj1. dt.GetNextPosition();
                }
                else if (tp.Parent is RelationOneToOne ro)
                {
                    //tp.PositionOfDescr = dt.GetNextPosition();
                }
                else
                    ThrowHelper.ThrowInvalidOperationException();
            }
            else if (this.Parent is Constant tc)
            {
                //if (tc.PositionOfDescr == 0)
                //{
                //    tc.PositionOfDescr = tc.ParentGroupListProperties.GetNextPosition();
                //}
            }
            else
                ThrowHelper.ThrowInvalidOperationException();
            return 0;
        }
        private void GetPositions(ComplexRef cr)
        {
            if (cr.Position > 0)
                return;
            if (this.Parent is Property tp)
            {
                if (tp.PositionOfDescr == 0)
                    tp.PositionOfDescr = this.GetNextPosition();
                if (tp.PositionOfGd == 0)
                    tp.PositionOfGd = this.GetNextPosition();
                cr.Position = this.GetNextPosition();
            }
            else if (this.Parent is Constant tc)
            {
                if (tc.PositionOfDescr == 0)
                    tc.PositionOfDescr = tc.ParentGroupListConstants.GetNextPosition();
                if (tc.PositionOfGd == 0)
                    tc.PositionOfGd = tc.ParentGroupListConstants.GetNextPosition();
                cr.Position = tc.ParentGroupListConstants.GetNextPosition();
            }
            else
                ThrowHelper.ThrowInvalidOperationException();
        }

        partial void OnCreated()
        {
            this._Length = 10;
            this._DataTypeEnum = EnumDataType.STRING;
            //Init();
            //this.PropertyChanging += DataType_PropertyChanging;
            //this.PropertyChanged += DataType_PropertyChanged;
        }
        ///<summary>
        /// Guid of complex type. It can be Guid of Catalog or Document. 
        /// Numerical, string, bool, date and similar are simple types. For simple types this property is empty.
        /// If Guid of group types is assigned, then any type of such group of types is acceptable as type
        /// If Guid is empty, but EnumDataType is Any, then any complex type is acceptable as type
        /// </summary>
        [PropertyOrderAttribute(6)]
        [Editor(typeof(EditorDataTypeObjectName), typeof(EditorDataTypeObjectName))]
        public ComplexRef ObjectRef0
        {
            get
            {
                if (this.ListObjectRefs.Count == 0)
                {
                    this.ListObjectRefs.Add(new ComplexRef());
                }
                return this.ListObjectRefs[0];
            }
        }
        public IComplexRef ObjectRef { get { return this.ObjectRef0; } }
        partial void OnDataTypeEnumChanging(ref EnumDataType to, ref bool isCancel)
        {
            switch (this.DataTypeEnum)
            {
                case EnumDataType.CATALOG:
                    //this.ListObjectRefs.Clear();
                    //if (to == EnumDataType.CATALOGS && !string.IsNullOrWhiteSpace(this.ObjectRef0?.ForeignObjectGuid))
                    //{
                    //    this.ListObjectRefs.Add(this.ObjectRef);
                    //    this.ObjectRef.ForeignObjectGuid = string.Empty;
                    //}
                    break;
                case EnumDataType.DOCUMENT:
                    //this.ListObjectRefs.Clear();
                    //if (to == EnumDataType.DOCUMENTS && !string.IsNullOrWhiteSpace(this.ObjectRef.ForeignObjectGuid))
                    //{
                    //    this.ListObjectRefs.Add(this.ObjectRef);
                    //    this.ObjectRef.ForeignObjectGuid = string.Empty;
                    //}
                    break;
                case EnumDataType.CATALOGS:
                    if (this.ListObjectRefs.Count > 1)
                    {
                        var mes = "Can't change data type CATALOGS to CATALOG when list selected types for CATALOGS contains more than one type.";
                        if (VmBindable.isUnitTests)
                            throw new Exception(mes);
                        var res = Xceed.Wpf.Toolkit.MessageBox.Show(mes, "Error", System.Windows.MessageBoxButton.OK);
                        isCancel = true;
                    }
                    break;
                case EnumDataType.DOCUMENTS:
                    if (this.ListObjectRefs.Count > 1)
                    {
                        var mes = "Can't change data type DOCUMENTS to DOCUMENT when list selected types for DOCUMENTS contains more than one type.";
                        if (VmBindable.isUnitTests)
                            throw new Exception(mes);
                        var res = Xceed.Wpf.Toolkit.MessageBox.Show(mes, "Error", System.Windows.MessageBoxButton.OK);
                        isCancel = true;
                    }
                    break;
                case EnumDataType.ENUMERATION:
                    break;
            }
        }
        public DataType(ITreeConfigNode parent, BigInteger maxNumericalValue, bool isPositive = false) : this(parent)
        {
            BigInteger maxValue = maxNumericalValue;
            uint length = 0;
            maxValue /= 10;
            while (maxValue > 0)
            {
                length++;
                maxValue /= 10;
            }
            this._DataTypeEnum = EnumDataType.NUMERICAL;
            this._Length = length;
            this._Accuracy = 0;
            this._IsPositive = isPositive;
        }
        public DataType(ITreeConfigNode parent, EnumDataType type, uint? length = null, uint? accuracy = null, bool? isPositive = null) : this(parent)
        {
            this._DataTypeEnum = type;
            switch (this.DataTypeEnum)
            {
                case EnumDataType.BOOL:
                    break;
                case EnumDataType.CATALOGS:
                    break;
                case EnumDataType.DOCUMENTS:
                    break;
                case EnumDataType.ANY:
                    break;
                case EnumDataType.NUMERICAL:
                    // TODO revisit default length and accuracy for Numerical
                    this._Length = length ?? 16;
                    this._Accuracy = accuracy ?? 2;
                    this._IsPositive = isPositive ?? false;
                    break;
                case EnumDataType.ENUMERATION:
                    break;
                case EnumDataType.STRING:
                    this._Length = length ?? 30;
                    break;
                case EnumDataType.ULID:
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        public DataType(ITreeConfigNode parent, EnumDataType type, string guidOfType) : this(parent)
        {
            this._DataTypeEnum = type;
            this._ListObjectRefs = new ObservableCollectionWithActions<ComplexRef>();
            this._ListObjectRefs.Add(new ComplexRef() { ForeignObjectGuid = guidOfType });
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
            if (string.IsNullOrWhiteSpace(this.ObjectRef.ForeignObjectGuid))
                return false;
            Debug.Assert(this.Cfg != null);
            var en = (Enumeration)this.Cfg.DicNodes[this.ObjectRef.ForeignObjectGuid];
            if (en.DataTypeEnum == EnumEnumerationType.STRING_VALUE)
                return true;
            return false;
        }
        [Browsable(false)]
        public string EnumerationDefault
        {
            get
            {
                if (this.DataTypeEnum != EnumDataType.ENUMERATION)
                    throw new NotImplementedException();
                if (string.IsNullOrWhiteSpace(this.ObjectRef.ForeignObjectGuid))
                    throw new NotImplementedException();
                Debug.Assert(this.Cfg != null);
                var en = (Enumeration)this.Cfg.DicNodes[this.ObjectRef.ForeignObjectGuid];
                return en.ListEnumerationPairs[0].Value;
            }
        }
        [Browsable(false)]
        public EnumEnumerationType? EnumerationType
        {
            get
            {
                if (this.DataTypeEnum != EnumDataType.ENUMERATION)
                    return null;
                if (string.IsNullOrWhiteSpace(this.ObjectRef.ForeignObjectGuid))
                    throw new NotImplementedException();
                Debug.Assert(this.Cfg != null);
                var en = (Enumeration)this.Cfg.DicNodes[this.ObjectRef.ForeignObjectGuid];
                return en.DataTypeEnum;
            }
        }
        [Browsable(false)]
        public int EnumerationStrFieldLength
        {
            get
            {
                if (this.DataTypeEnum != EnumDataType.ENUMERATION)
                    return 0;
                if (string.IsNullOrWhiteSpace(this.ObjectRef.ForeignObjectGuid))
                    throw new NotImplementedException();
                Debug.Assert(this.Cfg != null);
                var en = (Enumeration)this.Cfg.DicNodes[this.ObjectRef.ForeignObjectGuid];
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
                if (string.IsNullOrWhiteSpace(this.ObjectRef.ForeignObjectGuid))
                    return "";
                Debug.Assert(this.Cfg != null);
                var en = (Enumeration)this.Cfg.DicNodes[this.ObjectRef.ForeignObjectGuid];
                return "Enum" + en.Name;
            }
        }
        #endregion Enumeration

        public static string? GetTypeDesc(DataType p, StringBuilder sb = null)
        {
            Debug.Assert(p != null);
            if (sb == null)
                sb = new StringBuilder();
            sb.Append(Enum.GetName(typeof(EnumDataType), (int)p.DataTypeEnum)!);
            Debug.Assert(p.Parent != null);
            ITreeConfigNode par = p.Parent;
            while (par != null && par.Parent != null)
            {
                par = par.Parent;
            }
            var config = (Config)par!;
            switch (p.DataTypeEnum)
            {
                case EnumDataType.CATALOG:
                    sb.Append(": ");
                    foreach (var t in config.Model.GroupCatalogs.ListCatalogs)
                    {
                        if (p.ObjectRef.ForeignObjectGuid == t.Guid)
                        {
                            sb.Append(t.Name);
                        }
                    }
                    break;
                case EnumDataType.DOCUMENT:
                    sb.Append(": ");
                    foreach (var t in config.Model.GroupDocuments.GroupListDocuments.ListDocuments)
                    {
                        if (p.ObjectRef.ForeignObjectGuid == t.Guid)
                        {
                            sb.Append(t.Name);
                        }
                    }
                    break;
                case EnumDataType.ANY:
                    break;
                case EnumDataType.CATALOGS:
                    sb.Append(": ");
                    sb.Append(p.ListObjectRefs.Count);
                    sb.Append(" types");
                    foreach (var tt in p.ListObjectRefs)
                    {
                        foreach (var t in config.Model.GroupCatalogs.ListCatalogs)
                        {
                            if (tt.ForeignObjectGuid == t.Guid)
                            {
                                sb.Append(", ");
                                sb.Append(t.Name);
                            }
                        }
                    }
                    break;
                case EnumDataType.DOCUMENTS:
                    sb.Append(": ");
                    sb.Append(p.ListObjectRefs.Count);
                    sb.Append(" types");
                    foreach (var tt in p.ListObjectRefs)
                    {
                        foreach (var t in config.Model.GroupDocuments.GroupListDocuments.ListDocuments)
                        {
                            if (tt.ForeignObjectGuid == t.Guid)
                            {
                                sb.Append(", ");
                                sb.Append(t.Name);
                            }
                        }
                    }
                    break;
                case EnumDataType.ENUMERATION:
                    sb.Append(": ");
                    foreach (var t in config.Model.GroupEnumerations.ListEnumerations)
                    {
                        if (p.ObjectRef.ForeignObjectGuid == t.Guid)
                        {
                            sb.Append(t.Name);
                        }
                    }
                    break;
                case EnumDataType.NUMERICAL:
                    sb.Append(", ");
                    if (p.IsPositive)
                        sb.Append("+ ");
                    sb.Append(p.Length);
                    if (p.Accuracy > 0)
                    {
                        sb.Append(".");
                        sb.Append(p.Accuracy);
                        sb.Append(" ");
                    }
                    sb.Append("clr:");
                    sb.Append(p.ClrTypeName);
                    break;
                case EnumDataType.STRING:
                    sb.Append(", Length: ");
                    if (p.Length > 0)
                        sb.Append(p.Length);
                    else
                        sb.Append("unlimited");
                    break;
                case EnumDataType.ULID:
                    break;
                case EnumDataType.BOOL:
                    break;
                default:
                    sb.Append(" - NOT SUPPORTED");
                    break;
            }
            return sb.ToString();
        }
        [PropertyOrderAttribute(14)]
        [DisplayName("Min Len")]
        [Description("Min value based on Length")]
        public string MinValue
        {
            get { if (_MinValue == null) MinValueCalc(); Debug.Assert(_MinValue != null); return _MinValue; }
            set
            {
                SetProperty(ref this._MinValue, value);
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
                SetProperty(ref this._MaxValue, value);
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
        [Browsable(false)]
        public BigInteger? MaxNumericalValue
        {
            get { if (_MaxNumericalValue == null) ClrTypeNameCalc(); return _MaxNumericalValue; }
            set
            {
                SetProperty(ref this._MaxNumericalValue, value);
            }
        }
        private BigInteger? _MaxNumericalValue = null;
        public static uint GetLengthFromMaxValue(BigInteger maxValue)
        {
            uint length = 0;
            BigInteger m = maxValue;
            while (m > 10)
            {
                m /= 10;
                length++;
            }
            return length;
        }
        [Browsable(false)]
        public Type ClrType
        {
            get
            {
                return GetClrType();
            }
        }
        private Type GetClrType()
        {
            return this.ClrTypeName switch
            {
                "DateTime" => typeof(DateTime),
                "DateTime?" => typeof(DateTime?),
                "bool" => typeof(bool),
                "bool?" => typeof(bool?),
                "string" => typeof(string),
                "byte" => typeof(byte),
                "byte?" => typeof(byte?),
                "ushort" => typeof(ushort),
                "ushort?" => typeof(ushort?),
                "uint" => typeof(uint),
                "uint?" => typeof(uint?),
                "ulong" => typeof(ulong),
                "ulong?" => typeof(ulong?),
                "BigInteger" => typeof(BigInteger),
                "BigInteger?" => typeof(BigInteger?),
                "sbyte" => typeof(sbyte),
                "sbyte?" => typeof(sbyte?),
                "short" => typeof(short),
                "short?" => typeof(short?),
                "int" => typeof(int),
                "int?" => typeof(int?),
                "long" => typeof(long),
                "long?" => typeof(long?),
                "BigDecimal" => typeof(BigDecimal),
                "BigDecimal?" => typeof(BigDecimal?),
                "float" => typeof(float),
                "float?" => typeof(float?),
                "double" => typeof(double),
                "double?" => typeof(double?),
                "decimal" => typeof(decimal),
                "decimal?" => typeof(decimal?),
                _ => throw new Exception("Not supported operation"),
            };
        }
        [Browsable(false)]
        public string? ComplexRefSuffix
        {
            get { return this._ComplexRefSuffix; }
            set { SetProperty(ref this._ComplexRefSuffix, value); }
        }
        private string? _ComplexRefSuffix = null;
        [PropertyOrderAttribute(11)]
        public string ClrTypeName
        {
            get { if (_ClrTypeName == null) ClrTypeNameCalc(); return _ClrTypeName!; }
            set
            {
                SetProperty(ref this._ClrTypeName, value);
            }
        }
        private string? _ClrTypeName = null;
        [Browsable(false)]
        public string ClrLiteralSuf
        {
            get { if (_ClrLiteralSuf == null) ClrTypeNameCalc(); Debug.Assert(_ClrLiteralSuf != null); return _ClrLiteralSuf!; }
            set
            {
                SetProperty(ref this._ClrLiteralSuf, value);
            }
        }
        private string? _ClrLiteralSuf = null;
        private void ClrTypeNameCalc()
        {
            this.ClrLiteralSuf = "";
            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/decimal
            switch (this.DataTypeEnum)
            {
                case EnumDataType.REF_CATALOG_TO_SEPARATE_CATALOG_FOLDER:
                case EnumDataType.REF_DETAIL_TO_PARENT_CATALOG:
                case EnumDataType.REF_DETAIL_TO_PARENT_CATALOG_FOLDER:
                case EnumDataType.REF_DETAIL_TO_PARENT_DETAIL:
                case EnumDataType.REF_DETAIL_TO_PARENT_DOCUMENT:
                case EnumDataType.REF_TO_SELF_TREE_CATALOG_FOLDER_PARENT:
                case EnumDataType.REF_TO_SELF_TREE_CATALOG_PARENT:
                case EnumDataType.REF_TIMELINE:
                    return;
                case EnumDataType.CATALOG:
                    Debug.Assert(this.Cfg != null);
                    if (!string.IsNullOrEmpty(this.ObjectRef.ForeignObjectGuid))
                    {
                        var en = (Catalog?)this.Cfg.DicNodes[this.ObjectRef.ForeignObjectGuid];
                        Debug.Assert(en != null);
                        this.ClrTypeName = en.Name;
                    }
                    else
                        this.ClrTypeName = string.Empty;
                    break;
                case EnumDataType.DOCUMENT:
                    Debug.Assert(this.Cfg != null);
                    if (!string.IsNullOrEmpty(this.ObjectRef.ForeignObjectGuid))
                    {
                        var en = (Document?)this.Cfg.DicNodes[this.ObjectRef.ForeignObjectGuid];
                        Debug.Assert(en != null);
                        this.ClrTypeName = en.Name;
                    }
                    else
                        this.ClrTypeName = string.Empty;
                    break;
                case EnumDataType.CATALOGS:
                    this.ClrTypeName = "object";
                    break;
                case EnumDataType.DOCUMENTS:
                    this.ClrTypeName = "object";
                    break;
                case EnumDataType.ANY:
                    this.ClrTypeName = "object";
                    break;
                case EnumDataType.ENUMERATION:
                    this.ClrTypeName = this.EnumerationName;
                    break;
                case EnumDataType.TIME:
                case EnumDataType.TIMEZ:
                    this.ClrTypeName = "TimeOnly";
                    break;
                case EnumDataType.DATE:
                    this.ClrTypeName = "DateOnly";
                    break;
                case EnumDataType.DATETIMEZ:
                    this.ClrTypeName = "DateTimeOffset";
                    break;
                case EnumDataType.DATETIMELOCAL:
                case EnumDataType.DATETIMEUTC:
                    //case EnumDataType.DATETIME:
                    this.ClrTypeName = "DateTime";
                    break;
                case EnumDataType.DATETIMEOFFSET:
                    this.ClrTypeName = "DateTimeOffset";
                    break;
                case EnumDataType.TIMESPAN_TIME_ONLY:
                case EnumDataType.TIMESPAN:
                    this.ClrTypeName = "TimeSpan";
                    break;
                case EnumDataType.BOOL:
                    this.ClrTypeName = "bool";
                    break;
                case EnumDataType.CHAR:
                    this.ClrTypeName = "char";
                    break;
                case EnumDataType.STRING:
                    this.ClrTypeName = "string";
                    break;
                case EnumDataType.ULID:
                    this.ClrTypeName = "Ulid";
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
                    this.MaxNumericalValue = mv - 1;
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
                                //this.ClrTypeName = "short";
                                this.ClrTypeName = "sbyte"; // problem with Dapper, writing wrong value in DB
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
        public string? CanLooseData(DataType to)
        {
            string? res = null;

            return res;
        }

        #region Visibility
        [Browsable(false)]
        public SortedObservableCollection<ITreeConfigNodeSortable>? ListObjects
        {
            get
            {
                Debug.Assert(this.Cfg != null);
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
        partial void OnDataTypeEnumChanged()
        {
            if (this.Cfg == null)
                return;
            switch (this.DataTypeEnum)
            {
                case EnumDataType.REF_CATALOG_TO_SEPARATE_CATALOG_FOLDER:
                case EnumDataType.REF_DETAIL_TO_PARENT_CATALOG:
                case EnumDataType.REF_DETAIL_TO_PARENT_CATALOG_FOLDER:
                case EnumDataType.REF_DETAIL_TO_PARENT_DETAIL:
                case EnumDataType.REF_DETAIL_TO_PARENT_DOCUMENT:
                case EnumDataType.REF_TO_SELF_TREE_CATALOG_FOLDER_PARENT:
                case EnumDataType.REF_TO_SELF_TREE_CATALOG_PARENT:
                case EnumDataType.REF_TIMELINE:
                    return;
                case EnumDataType.CHAR:
                case EnumDataType.BOOL:
                case EnumDataType.DATE:
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Collapsed;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    this.VisibilityIsPositive = Visibility.Collapsed;
                    this._Length = 0;
                    this._Accuracy = 0;
                    this._IsPositive = false;
                    break;
                case EnumDataType.DATETIMELOCAL:
                case EnumDataType.DATETIMEUTC:
                //case EnumDataType.DATETIME:
                case EnumDataType.DATETIMEZ:
                case EnumDataType.DATETIMEOFFSET:
                case EnumDataType.TIME:
                case EnumDataType.TIMEZ:
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Collapsed;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    this.VisibilityIsPositive = Visibility.Collapsed;
                    this._Length = 0;
                    this._Accuracy = 0;
                    this._IsPositive = false;
                    break;
                case EnumDataType.CATALOG:
                case EnumDataType.DOCUMENT:
                    this.VisibilityIsPositive = Visibility.Collapsed;
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Collapsed;
                    this.VisibilityObjectName = Visibility.Visible;
                    this._Length = 0;
                    this._Accuracy = 0;
                    this._IsPositive = false;
                    foreach (var t in this.ListObjectRefs)
                    {
                        GetPositions(t);
                    }
                    break;
                case EnumDataType.CATALOGS:
                case EnumDataType.DOCUMENTS:
                case EnumDataType.ANY:
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Collapsed;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    this.VisibilityIsPositive = Visibility.Collapsed;
                    this._Length = 0;
                    this._Accuracy = 0;
                    this._IsPositive = false;
                    foreach (var t in this.ListObjectRefs)
                    {
                        GetPositions(t);
                    }
                    break;
                case EnumDataType.ENUMERATION:
                    this.VisibilityIsPositive = Visibility.Collapsed;
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Collapsed;
                    this.VisibilityObjectName = Visibility.Visible;
                    this._Length = 0;
                    this._Accuracy = 0;
                    this._IsPositive = false;
                    break;
                case EnumDataType.TIMESPAN:
                case EnumDataType.TIMESPAN_TIME_ONLY:
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityIsPositive = Visibility.Visible;
                    this.VisibilityLength = Visibility.Visible;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    this._Length = 6;
                    this._Accuracy = 0;
                    this._IsPositive = false;
                    this.ObjectRef0.ForeignObjectGuid = string.Empty;
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
                    break;
                case EnumDataType.STRING:
                    this.VisibilityIsPositive = Visibility.Collapsed;
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Visible;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    this._Length = 25;
                    this._Accuracy = 0;
                    this._IsPositive = false;
                    break;
                case EnumDataType.ULID:
                    this.VisibilityIsPositive = Visibility.Collapsed;
                    this.VisibilityAccuracy = Visibility.Collapsed;
                    this.VisibilityLength = Visibility.Collapsed;
                    this.VisibilityObjectName = Visibility.Collapsed;
                    this._Length = 0;
                    this._Accuracy = 0;
                    this._IsPositive = false;
                    break;
                default:
                    throw new NotSupportedException();
            }
            ClrTypeNameCalc();
            MinValueCalc();
            MaxValueCalc();
            this.OnPropertyChanged(nameof(this.Length));
            this.OnPropertyChanged(nameof(this.Accuracy));
            this.OnPropertyChanged(nameof(this.IsPositive));
            this.OnPropertyChanged(nameof(this.ObjectRef));
            this.OnPropertyChanged(nameof(this.ListObjects));
        }
        partial void OnLengthChanged()
        {
            if (this.Cfg == null)
                return;
            this._MaxNumericalValue = 0;
            ClrTypeNameCalc();
            MaxValueCalc();
            this.ValidateProperty(nameof(this.Accuracy));
        }
        partial void OnAccuracyChanged()
        {
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
        [Browsable(false)]
        public Visibility VisibilityLength
        {
            get
            {
                return this._VisibilityLength;
            }

            set
            {
                SetProperty(ref this._VisibilityLength, value);
            }
        }
        private Visibility _VisibilityLength = Visibility.Collapsed;
        [Browsable(false)]
        public Visibility VisibilityAccuracy
        {
            get
            {
                return this._VisibilityAccuracy;
            }

            set
            {
                SetProperty(ref this._VisibilityAccuracy, value);
            }
        }
        private Visibility _VisibilityAccuracy = Visibility.Collapsed;
        [Browsable(false)]
        public Visibility VisibilityIsPositive
        {
            get
            {
                return this._VisibilityIsPositive;
            }

            set
            {
                SetProperty(ref this._VisibilityIsPositive, value);
            }
        }
        private Visibility _VisibilityIsPositive = Visibility.Collapsed;
        [Browsable(false)]
        public Visibility VisibilityObjectName
        {
            get
            {
                return this._VisibilityObjectName;
            }

            set
            {
                SetProperty(ref this._VisibilityObjectName, value);
            }
        }
        private Visibility _VisibilityObjectName = Visibility.Collapsed;
        [Browsable(false)]
        public Visibility VisibilityTimespan
        {
            get
            {
                return this._VisibilityTimespan;
            }

            set
            {
                SetProperty(ref this._VisibilityTimespan, value);
            }
        }
        private Visibility _VisibilityTimespan = Visibility.Collapsed;
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
            Debug.Assert(this.Cfg == null || this.Cfg.PrevStableConfig == null || this.Parent != null);
            IDataType? res = null;
            if (this.Cfg != null && this.Cfg.PrevStableConfig != null && this.Cfg.PrevStableConfig.DicNodes.ContainsKey(this.Parent.Guid))
            {
                res = (this.Cfg.PrevStableConfig.DicNodes[this.Parent.Guid] as IDataTypeObject)?.IDataType;
            }
            return res;
        }
        public IDataType? PrevCurrentVersion()
        {
            Debug.Assert(this.Cfg == null || this.Cfg.PrevCurrentConfig == null || this.Parent != null);
            //Debug.Assert(this.Cfg == null || this.Parent != null);
            IDataType? res = null;
            if (this.Cfg != null && this.Cfg.PrevCurrentConfig != null && this.Cfg.PrevCurrentConfig.DicNodes.ContainsKey(this.Parent.Guid))
            {
                res = (this.Cfg.PrevCurrentConfig.DicNodes[this.Parent.Guid] as IDataTypeObject)?.IDataType;
            }
            return res;
        }
        protected override void OnIsChangedChanged()
        {
            if (this.Parent != null && this.IsChanged)
                this.Parent.IsChanged = true;
        }
        public bool IsComplex
        {
            get
            {
                return this.DataTypeEnum == EnumDataType.CATALOG || this.DataTypeEnum == EnumDataType.DOCUMENT
                    || this.DataTypeEnum == EnumDataType.CATALOGS || this.DataTypeEnum == EnumDataType.DOCUMENTS || this.DataTypeEnum == EnumDataType.ANY
                    || this.DataTypeEnum == EnumDataType.REF_CATALOG_TO_SEPARATE_CATALOG_FOLDER || this.DataTypeEnum == EnumDataType.REF_DETAIL_TO_PARENT_CATALOG
                    || this.DataTypeEnum == EnumDataType.REF_DETAIL_TO_PARENT_CATALOG_FOLDER || this.DataTypeEnum == EnumDataType.REF_DETAIL_TO_PARENT_DETAIL
                    || this.DataTypeEnum == EnumDataType.REF_DETAIL_TO_PARENT_DOCUMENT || this.DataTypeEnum == EnumDataType.REF_TIMELINE
                    || this.DataTypeEnum == EnumDataType.REF_TO_SELF_TREE_CATALOG_FOLDER_PARENT || this.DataTypeEnum == EnumDataType.REF_TO_SELF_TREE_CATALOG_PARENT
                    ;
            }
        }
        public bool IsComplexOne
        {
            get
            {
                return this.DataTypeEnum == EnumDataType.CATALOG || this.DataTypeEnum == EnumDataType.DOCUMENT;
            }
        }
        public bool IsComplexMany
        {
            get
            {
                return this.DataTypeEnum == EnumDataType.CATALOGS || this.DataTypeEnum == EnumDataType.DOCUMENTS || this.DataTypeEnum == EnumDataType.ANY;
            }
        }
    }
}
