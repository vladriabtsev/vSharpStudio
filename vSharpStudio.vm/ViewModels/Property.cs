using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using static Proto.Config.proto_data_type.Types;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Property : EntityObjectBaseWithGuid<Property, Property.PropertyValidator>, IConfigObject, ITreeNode, IComparable<Property>
    {
        partial void OnInit()
        {
        }
        public void OnInitFromDto()
        {
        }
        public Property(string name, EnumDataType type, string guidOfType) : this()
        {
            this.Name = name;
            this.DataType = new DataType(type, guidOfType);
        }
        public Property(string name, EnumDataType type, uint? length = null, uint? accuracy = null) : this()
        {
            this.Name = name;
            this.DataType = new DataType(type, length, accuracy);
        }
        public int CompareTo(Property other) { return this.SortingValue.CompareTo(other.SortingValue); }
        public Type ClrType
        {
            get
            {
                // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/decimal
                switch (this.DataType.DataTypeEnum)
                {
                    case EnumDataType.Bool:
                        return typeof(bool);
                    case EnumDataType.String:
                        return typeof(string);
                    case EnumDataType.Numerical:
                        if (this.DataType.Accuracy == 0)
                        {
                            if (this.DataType.IsPositive)
                            {
                                if (this.DataType.MaxValue <= byte.MaxValue)
                                    return typeof(byte);
                                if (this.DataType.MaxValue <= ushort.MaxValue)
                                    return typeof(ushort);
                                if (this.DataType.MaxValue <= uint.MaxValue)
                                    return typeof(uint);
                                if (this.DataType.MaxValue <= ulong.MaxValue) // long, not ulong
                                    return typeof(ulong);
                                return typeof(BigInteger);
                            }
                            else
                            {
                                if (this.DataType.MinValue >= sbyte.MinValue && this.DataType.MaxValue <= sbyte.MaxValue)
                                    return typeof(sbyte);
                                if (this.DataType.MinValue >= short.MinValue && this.DataType.MaxValue <= short.MaxValue)
                                    return typeof(short);
                                if (this.DataType.MinValue >= int.MinValue && this.DataType.MaxValue <= int.MaxValue)
                                    return typeof(int);
                                if (this.DataType.MinValue >= long.MinValue && this.DataType.MaxValue <= long.MaxValue)
                                    return typeof(long);
                                return typeof(BigInteger);
                            }
                        }
                        else
                        {
                            // float   ±1.5 x 10−45   to ±3.4    x 10+38    ~6-9 digits
                            // double  ±5.0 × 10−324  to ±1.7    × 10+308   ~15-17 digits
                            // decimal ±1.0 x 10-28   to ±7.9228 x 10+28     28-29 significant digits
                            if (this.DataType.Length <= 6)
                                return typeof(float);
                            if (this.DataType.Length <= 15)
                                return typeof(double);
                            if (this.DataType.Length < 29)
                                return typeof(decimal);
                            return typeof(BigDecimal);
                        }
                    default:
                        throw new Exception("Not supported operation");
                }
            }
        }
        public string ProtoType
        {
            get
            {
                // https://developers.google.com/protocol-buffers/docs/proto3#scalar
                switch (this.DataType.DataTypeEnum)
                {
                    case EnumDataType.Bool:
                        return "bool";
                    case EnumDataType.String:
                        return "string";
                    case EnumDataType.Numerical:
                        if (this.DataType.Accuracy == 0)
                        {
                            if (this.DataType.IsPositive)
                            {
                                if (this.DataType.MaxValue <= uint.MaxValue)
                                    return "uint32";
                                if (this.DataType.MaxValue <= long.MaxValue) // long, not ulong
                                    return "uint64";
                                return "bytes"; // need conversions
                            }
                            else
                            {
                                if (this.DataType.MinValue >= int.MinValue && this.DataType.MaxValue <= int.MaxValue)
                                    return "int32";
                                if (this.DataType.MinValue >= long.MinValue && this.DataType.MaxValue <= long.MaxValue)
                                    return "int64";
                                return "bytes"; // need conversions
                            }
                        }
                        else
                        {
                            // float   ±1.5 x 10−45   to ±3.4    x 10+38    ~6-9 digits
                            // double  ±5.0 × 10−324  to ±1.7    × 10+308   ~15-17 digits
                            // decimal ±1.0 x 10-28   to ±7.9228 x 10+28     28-29 significant digits
                            if (this.DataType.Length <= 6)
                                return "float";
                            if (this.DataType.Length <= 15)
                                return "double";
                            return "bytes"; // need conversions
                        }
                    default:
                        throw new Exception("Not supported operation");
                }
            }
        }

        #region ITreeNode
        public ITreeNode Parent { get; internal set; }
        public IEnumerable<ITreeNode> SubNodes => this._SubNodes;
        private IEnumerable<ITreeNode> _SubNodes = new ITreeNode[] { };
        public bool IsSelected
        {
            get { return this._IsSelected; }
            set
            {
                this._IsSelected = value;
                NotifyPropertyChanged();
            }
        }
        private bool _IsSelected;
        public bool IsExpanded
        {
            get { return this._IsExpanded; }
            set
            {
                this._IsExpanded = value;
                NotifyPropertyChanged();
            }
        }
        private bool _IsExpanded;
        public string NodeText { get { return this.Name; } }

        #endregion ITreeNode
    }
}
