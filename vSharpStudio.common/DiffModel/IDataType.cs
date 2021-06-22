using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IDataType : IValidatableWithSeverity
    {
        ITreeConfigNode Parent { get; set; }
        string ProtoType { get; }
        string ClrTypeName { get; }
        string DefaultValue { get; }
        string DefaultNotNullValue { get; }
        string ClrTypeNameNotNull { get; }
        Type ClrType { get; }
        BigInteger MaxNumericalValue { get; }
        EnumEnumerationType EnumerationType { get; }
        int EnumerationStrFieldLength { get; }
        bool IsEnumStr();
        string GetProtoType(bool isGrpcService = false);
    }
}
