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
    [DebuggerDisplay("Property:{Name,nq} Type:{DataType.GetTypeDesc(this.DataType),nq}")]
    public partial class Property : ICanAddNode, ICanGoLeft
    {
        public static readonly string DefaultName = "Property";
        partial void OnInit()
        {
        }
        public Property(string name, EnumDataType type, string guidOfType) : this()
        {
            this.Name = name;
            this.DataType = new DataType(type, guidOfType);
        }
        public Property(string name, EnumDataType type, uint? length = null, uint? accuracy = null, bool? isPositive = null) : this()
        {
            this.Name = name;
            this.DataType = new DataType(type, length, accuracy);
        }
        public string ClrType { get { return this.DataType.ClrType; } }
        public string ProtoType { get { return this.DataType.ProtoType; } }
    }
}
