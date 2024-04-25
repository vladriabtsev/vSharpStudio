using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IDataType : IValidatableWithSeverity
    {
        ITreeConfigNode? Parent { get; set; }
        //string ProtoType { get; }
        string ClrTypeName { get; }
        //string DefaultValue { get; }
        //string DefaultNotNullValue { get; }
        //string ClrTypeNameNotNull { get; }
        Type ClrType { get; }
        string MinValue { get; }
        string MaxValue { get; }
        string ClrLiteralSuf { get; }
        string EnumerationName { get; }
        BigInteger? MaxNumericalValue { get; }
        EnumEnumerationType? EnumerationType { get; }
        int EnumerationStrFieldLength { get; }
        bool IsEnumStr();
        string EnumerationDefault { get; }
        bool IsComplex { get; }
        bool IsComplexOne { get; }
        bool IsComplexMany { get; }
        IComplexRef ObjectRef { get; }
    }
}
