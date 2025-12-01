using System;
using System.Collections.Generic;
using System.ComponentModel;
using Google.Protobuf.WellKnownTypes;
using vSharpStudio.common.ViewModels;

namespace vPlugin.Sample 
{
    ///<summary>
    ///     Represents the caching modes that can be used when creating a new <see cref="SqliteConnection" />.
    /// </summary>
    /// <seealso href="http://sqlite.org/sharedcache.html">SQLite Shared-Cache Mode</seealso>
    
    public partial interface IDbConnectionStringSettings 
    {
    	// Represents the caching modes that can be used when creating a new <see cref="SqliteConnection" />.
    	string StringSettings { get; } 
    }
    
    public partial interface IPluginsGroupSolutionSubSettings 
    {
    	bool IsSubParam1 { get; } 
    	bool IsSubParam2 { get; } 
    }
    
    public partial interface IPluginsGroupSolutionSettings 
    {
    	bool IsGroupParam1 { get; } 
    	IPluginsGroupSolutionSubSettings SubSettings { get; } 
    }
    
    public partial interface IPluginsGroupProjectSettings 
    {
    	bool IsGroupProjectParam1 { get; } 
    }
    
    public partial interface IGeneratorDbSchemaSettings 
    {
    	bool IsSchemaParam1 { get; } 
    	bool? IsSchemaParam2 { get; } 
    	string SchemaParam3 { get; } 
    }
    
    public partial interface IGeneratorDbSchemaNodeSettings 
    {
    	bool IsParam1 { get; } 
    	bool? IsIncluded { get; } 
    	bool IsConstantParam1 { get; } 
    	bool IsCatalogFormParam1 { get; } 
    }
    
    public partial interface IGeneratorDbAccessSettings 
    {
    	bool IsAccessParam1 { get; } 
    	bool? IsAccessParam2 { get; } 
    	string AccessParam3 { get; } 
    	string? AccessParam4 { get; } 
    	bool IsGenerateNotValidCode { get; } 
    }
    
    public partial interface IGeneratorDbAccessNodeSettings 
    {
    	bool IsParam1 { get; } 
    	bool? IsIncluded { get; } 
    	bool IsPropertyParam1 { get; } 
    	bool IsCatalogFormParam1 { get; } 
    }
}