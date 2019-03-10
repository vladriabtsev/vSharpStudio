using System;
using System.Collections.Generic;
using System.Text;
using static Proto.Config.proto_data_type.Types;

namespace vSharpStudio.vm.ViewModels
{
    public partial class DataType
    {
        public DataType(EnumDataType type, int? length = null, int? accuracy = null) : base(DataTypeValidator.Validator)
        {
            this.EnumDataType = type;
            switch(this.EnumDataType)
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
                case EnumDataType.Enum:
                    break;
                case EnumDataType.String:
                    this.Length = length ?? 30;
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        public DataType(EnumDataType type, string guidOfType) : base(DataTypeValidator.Validator)
        {
            this.EnumDataType = type;
            this.Guid = guidOfType;
            switch (this.EnumDataType)
            {
                case EnumDataType.Catalog:
                    break;
                case EnumDataType.Constant:
                    break;
                case EnumDataType.Document:
                    break;
                case EnumDataType.Enum:
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
