using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IDataType
    {
        string ClrTypeName { get; }
        string ClrTypeNameNotNull { get; }
        Type ClrType { get; }
        BigInteger MaxNumericalValue { get; }
        EnumEnumerationType EnumerationType { get; }
        int EnumerationStrFieldLength { get; }
        bool IsEnumStr();
    }
}
