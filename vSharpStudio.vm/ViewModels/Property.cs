using System;
using System.Collections.Generic;
using System.Text;
using static Proto.Config.proto_data_type.Types;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Property : IEntityObject
    {
        partial void OnInit()
        {
            this.Guid = System.Guid.NewGuid().ToString();
        }
        public Property(string name, EnumDataType type, string guidOfType) : base(PropertyValidator.Validator)
        {
            this.Name = name;
            this.DataType = new DataType(type, guidOfType);
        }
        public Property(string name, EnumDataType type, int? length = null, int? accuracy = null) : base(PropertyValidator.Validator)
        {
            this.Name = name;
            this.DataType = new DataType(type, length, accuracy);
        }
    }
}
