using System;
using System.Collections.Generic;
using System.ComponentModel;
using Google.Protobuf.WellKnownTypes;
using ViewModelBase;
using vSharpStudio.common.ViewModels;

namespace vPlugin.Sample // ModelInterfaces.tt Line: 13
{
    
    public partial interface IDbConnectionStringSettings // ModelInterfaces.tt Line: 33
    {
    	string StringSettings { get; set; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IPluginsGroupSolutionSubSettings // ModelInterfaces.tt Line: 33
    {
    	bool IsSubParam1 { get; set; } // ModelInterfaces.tt Line: 55
    	bool IsSubParam2 { get; set; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IPluginsGroupSolutionSettings // ModelInterfaces.tt Line: 33
    {
    	bool IsGroupParam1 { get; set; } // ModelInterfaces.tt Line: 55
    	IPluginsGroupSolutionSubSettings SubSettings { get; } // ModelInterfaces.tt Line: 59
    }
    
    public partial interface IPluginsGroupProjectSettings // ModelInterfaces.tt Line: 33
    {
    	bool IsGroupProjectParam1 { get; set; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IGeneratorDbSchemaSettings // ModelInterfaces.tt Line: 33
    {
    	bool IsSchemaParam1 { get; set; } // ModelInterfaces.tt Line: 55
    	bool? IsSchemaParam2 { get; set; } // ModelInterfaces.tt Line: 55
    	string SchemaParam3 { get; set; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IGeneratorDbSchemaNodeSettings // ModelInterfaces.tt Line: 33
    {
    	bool IsParam1 { get; set; } // ModelInterfaces.tt Line: 55
    	bool? IsIncluded { get; set; } // ModelInterfaces.tt Line: 55
    	bool IsConstantParam1 { get; set; } // ModelInterfaces.tt Line: 55
    	bool IsCatalogFormParam1 { get; set; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IGeneratorDbAccessSettings // ModelInterfaces.tt Line: 33
    {
    	bool IsAccessParam1 { get; set; } // ModelInterfaces.tt Line: 55
    	bool? IsAccessParam2 { get; set; } // ModelInterfaces.tt Line: 55
    	string AccessParam3 { get; set; } // ModelInterfaces.tt Line: 55
    	string? AccessParam4 { get; set; } // ModelInterfaces.tt Line: 55
    	bool IsGenerateNotValidCode { get; set; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IGeneratorDbAccessNodeSettings // ModelInterfaces.tt Line: 33
    {
    	bool IsParam1 { get; set; } // ModelInterfaces.tt Line: 55
    	bool? IsIncluded { get; set; } // ModelInterfaces.tt Line: 55
    	bool IsPropertyParam1 { get; set; } // ModelInterfaces.tt Line: 55
    	bool IsCatalogFormParam1 { get; set; } // ModelInterfaces.tt Line: 55
    }
}