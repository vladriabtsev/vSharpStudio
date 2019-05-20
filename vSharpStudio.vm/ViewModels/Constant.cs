using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using static Proto.Config.proto_data_type.Types;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Constant:{Name,nq} Type:{DataType.GetTypeDesc(this.DataType),nq}")]
    public partial class Constant : ICanGoLeft, ICanAddNode, ISetParent
    {
        public static readonly string DefaultName = "Constant";

        partial void OnInit()
        {
        }
        public Constant(string name, EnumDataType type, string guidOfType) : this()
        {
            this.Name = name;
            this.DataType = new DataType(type, guidOfType);
        }
        public Constant(string name, EnumDataType type, uint? length = null, uint? accuracy = null, bool? isPositive = null) : this()
        {
            this.Name = name;
            this.DataType = new DataType(type, length, accuracy);
        }

        #region IConfigObject
        public void Create()
        {
            GroupListConstants vm = (GroupListConstants)this.Parent;
            int icurr = vm.Children.IndexOf(this);
            vm.Children.Add(new Constant() { Parent = this.Parent });
        }

        public void SetParent(object parent)
        {
            this.Parent = (GroupListConstants)parent;
        }
        #endregion IConfigObject
    }
}
