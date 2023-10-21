using System;
using System.Collections.Generic;
using System.ComponentModel;
using Google.Protobuf.WellKnownTypes;
using ViewModelBase;
using vSharpStudio.common.ViewModels;

namespace vPlugin.Sample //   7, ""  --- File: ModelInterfaces.tt Line: 14
{
    
    public partial interface IDbConnectionStringSettings //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	string StringSettings { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IPluginsGroupSolutionSubSettings //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	bool IsSubParam1 { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsSubParam2 { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IPluginsGroupSolutionSettings //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	bool IsGroupParam1 { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	IPluginsGroupSolutionSubSettings SubSettings { get; } //   7, ""  --- File: ModelInterfaces.tt Line: 60
    }
    
    public partial interface IPluginsGroupProjectSettings //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	bool IsGroupProjectParam1 { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IGeneratorDbSchemaSettings //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	bool IsSchemaParam1 { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool? IsSchemaParam2 { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string SchemaParam3 { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IGeneratorDbSchemaNodeSettings //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	bool IsParam1 { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool? IsIncluded { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsConstantParam1 { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsCatalogFormParam1 { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IGeneratorDbAccessSettings //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	bool IsAccessParam1 { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool? IsAccessParam2 { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string AccessParam3 { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	string? AccessParam4 { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsGenerateNotValidCode { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
    
    public partial interface IGeneratorDbAccessNodeSettings //   7, ""  --- File: ModelInterfaces.tt Line: 34
    {
    	bool IsParam1 { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool? IsIncluded { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsPropertyParam1 { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    	bool IsCatalogFormParam1 { get; set; } //   7, ""  --- File: ModelInterfaces.tt Line: 56
    }
}