using System;
using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using ViewModelBase;

namespace vSharpStudio.common // ModelInterfaces.tt Line: 11
{
    
    public partial interface IGeneratorDbSchemaSettings // ModelInterfaces.tt Line: 29
    {
        bool IsSchemaParam1 { get; } // ModelInterfaces.tt Line: 47
        bool? IsSchemaParam2 { get; } // ModelInterfaces.tt Line: 47
        string SchemaParam3 { get; } // ModelInterfaces.tt Line: 47
    }
    
    public partial interface IGeneratorDbAccessSettings // ModelInterfaces.tt Line: 29
    {
        bool IsAccessParam1 { get; } // ModelInterfaces.tt Line: 47
        bool? IsAccessParam2 { get; } // ModelInterfaces.tt Line: 47
        string AccessParam3 { get; } // ModelInterfaces.tt Line: 47
    }
}