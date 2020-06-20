using System;
using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using ViewModelBase;

namespace vSharpStudio.common // ModelInterfaces.tt Line: 11
{
    
    public partial interface IGeneratorDbSchemaSettings // ModelInterfaces.tt Line: 29
    {
        bool IsSchemaParam1 { get; set; } // ModelInterfaces.tt Line: 47
        bool? IsSchemaParam2 { get; set; } // ModelInterfaces.tt Line: 47
        string SchemaParam3 { get; set; } // ModelInterfaces.tt Line: 47
    }
    
    public partial interface IGeneratorDbAccessSettings // ModelInterfaces.tt Line: 29
    {
        bool IsAccessParam1 { get; set; } // ModelInterfaces.tt Line: 47
        bool? IsAccessParam2 { get; set; } // ModelInterfaces.tt Line: 47
        string AccessParam3 { get; set; } // ModelInterfaces.tt Line: 47
        string AccessParam4 { get; set; } // ModelInterfaces.tt Line: 47
        bool IsGenerateNotValidCode { get; set; } // ModelInterfaces.tt Line: 47
    }
    
    public partial interface IGeneratorDbAccessNodeSettings // ModelInterfaces.tt Line: 29
    {
        bool IsParam1 { get; set; } // ModelInterfaces.tt Line: 47
        bool? IsIncluded { get; set; } // ModelInterfaces.tt Line: 47
    }
    
    public partial interface IGeneratorDbAccessNodePropertySettings // ModelInterfaces.tt Line: 29
    {
        bool IsPropertyParam1 { get; set; } // ModelInterfaces.tt Line: 47
    }
    
    public partial interface IGeneratorDbAccessNodeCatalogFormSettings // ModelInterfaces.tt Line: 29
    {
        bool IsCatalogFormParam1 { get; set; } // ModelInterfaces.tt Line: 47
    }
}