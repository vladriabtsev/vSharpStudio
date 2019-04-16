using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using static Proto.Config.proto_data_type.Types;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Property:{Name,nq} Type:{Property.GetTypeDesc(this),nq}")]
    public partial class Property : ConfigObjectBase<Property, Property.PropertyValidator>, IComparable<Property>
    {
        public static readonly string DefaultName = "Property";
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
        public static string GetTypeDesc(Property p)
        {
            string res = Enum.GetName(typeof(Proto.Config.proto_data_type.Types.EnumDataType), (int)p.DataType.DataTypeEnum);
            switch (p.DataType.DataTypeEnum)
            {
                case Proto.Config.proto_data_type.Types.EnumDataType.Any:
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Bool:
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Catalog:
                    res += " "+ p.DataType.ObjectName;
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Catalogs:
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Constant:
                    res += " " + p.DataType.ObjectName;
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Document:
                    res += " " + p.DataType.ObjectName;
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Documents:
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Enum:
                    res += " " + p.DataType.ObjectName;
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.Numerical:
                    res += " Length:" + p.DataType.Length + " Accuracy:" + p.DataType.Accuracy + " Min:" + p.DataType.MinValueString + " Max:" + p.DataType.MaxValueString;
                    break;
                case Proto.Config.proto_data_type.Types.EnumDataType.String:
                    res += " Length:" + p.DataType.Length + " Min:" + p.DataType.MinValueString;
                    break;
                default:
                    res += " - Not supported";
                    break;
            }
            return res;
        }
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
        //        public string NodeText { get { return this.Name; } }
        protected override bool OnNodeCanRight()
        {
            return false;
        }
        protected override bool OnNodeCanMoveUp()
        {
            return (this.Parent as IListProperties).ListProperties.IndexOf(this) > 0;
        }
        protected override void OnNodeMoveUp()
        {
            var pp = this.Parent as IListProperties;
            var i = pp.ListProperties.IndexOf(this);
            if (i > 0)
            {
                this.SortingValue = pp.ListProperties[i - 1].SortingValue - 1;
            }
        }
        protected override bool OnNodeCanMoveDown()
        {
            var pp = this.Parent as IListProperties;
            return pp.ListProperties.IndexOf(this) < (pp.ListProperties.Count - 1);
        }
        protected override void OnNodeMoveDown()
        {
            var pp = this.Parent as IListProperties;
            var i = pp.ListProperties.IndexOf(this);
            if (i < pp.ListProperties.Count - 1)
            {
                this.SortingValue = pp.ListProperties[i + 1].SortingValue + 1;
            }
        }
        protected override void OnNodeRemove()
        {
            (this.Parent as IListProperties).ListProperties.Remove(this);
        }
        protected override ITreeConfigNode OnNodeAddNew()
        {
            var pp = this.Parent as IListProperties;
            var res = new Property();
            res.Parent = this.Parent;
            pp.ListProperties.Add(res);
            GetUniqueName(Property.DefaultName, res, pp.ListProperties);
            ITreeConfigNode config = this.Parent;
            while (config.Parent != null)
                config = config.Parent;
            (config as Config).SelectedNode = res;
            return res;
        }
        protected override ITreeConfigNode OnNodeAddClone()
        {
            var pp = this.Parent as IListProperties;
            var res = Property.Clone(this.Parent, this, true, true);
            res.Parent = this.Parent;
            pp.ListProperties.Add(res);
            this.Name = this.Name + "2";
            ITreeConfigNode config = this.Parent;
            while (config.Parent != null)
                config = config.Parent;
            (config as Config).SelectedNode = res;
            return res;
        }

        #endregion ITreeNode
    }
}
