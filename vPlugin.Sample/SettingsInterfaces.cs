using System;
using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using ViewModelBase;

namespace vSharpStudio.common // ModelInterfaces.tt Line: 11
{
    
    public partial interface IPluginsGroupSettings // ModelInterfaces.tt Line: 29
    {
        bool IsGroupParam1 { get; set; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IGeneratorDbSchemaSettings // ModelInterfaces.tt Line: 29
    {
        bool IsSchemaParam1 { get; set; } // ModelInterfaces.tt Line: 53
        bool? IsSchemaParam2 { get; set; } // ModelInterfaces.tt Line: 53
        string SchemaParam3 { get; set; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IGeneratorDbAccessSettings // ModelInterfaces.tt Line: 29
    {
        bool IsAccessParam1 { get; set; } // ModelInterfaces.tt Line: 53
        bool? IsAccessParam2 { get; set; } // ModelInterfaces.tt Line: 53
        string AccessParam3 { get; set; } // ModelInterfaces.tt Line: 53
        string AccessParam4 { get; set; } // ModelInterfaces.tt Line: 53
        bool IsGenerateNotValidCode { get; set; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IGeneratorDbAccessNodeSettings // ModelInterfaces.tt Line: 29
    {
        bool IsParam1 { get; set; } // ModelInterfaces.tt Line: 53
        bool? IsIncluded { get; set; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IGeneratorDbAccessNodePropertySettings // ModelInterfaces.tt Line: 29
    {
        bool IsPropertyParam1 { get; set; } // ModelInterfaces.tt Line: 53
    }
    
    public partial interface IGeneratorDbAccessNodeCatalogFormSettings // ModelInterfaces.tt Line: 29
    {
        bool IsCatalogFormParam1 { get; set; } // ModelInterfaces.tt Line: 53
    }
}