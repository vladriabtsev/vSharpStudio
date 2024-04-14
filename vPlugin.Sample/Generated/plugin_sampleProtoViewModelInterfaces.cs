using System;
using System.Collections.Generic;
using System.ComponentModel;
using Google.Protobuf.WellKnownTypes;
using vSharpStudio.common.ViewModels;

namespace vPlugin.Sample // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:13
{
    ///<summary>
    ///     Represents the caching modes that can be used when creating a new <see cref="SqliteConnection" />.
    /// </summary>
    /// <seealso href="http://sqlite.org/sharedcache.html">SQLite Shared-Cache Mode</seealso>
    
    public partial interface IDbConnectionStringSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	// Represents the caching modes that can be used when creating a new <see cref="SqliteConnection" />.
    	string StringSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IPluginsGroupSolutionSubSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	bool IsSubParam1 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsSubParam2 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IPluginsGroupSolutionSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	bool IsGroupParam1 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	IPluginsGroupSolutionSubSettings SubSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:59
    }
    
    public partial interface IPluginsGroupProjectSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	bool IsGroupProjectParam1 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IGeneratorDbSchemaSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	bool IsSchemaParam1 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool? IsSchemaParam2 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string SchemaParam3 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IGeneratorDbSchemaNodeSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	bool IsParam1 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool? IsIncluded { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsConstantParam1 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsCatalogFormParam1 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IGeneratorDbAccessSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	bool IsAccessParam1 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool? IsAccessParam2 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string AccessParam3 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string? AccessParam4 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsGenerateNotValidCode { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IGeneratorDbAccessNodeSettings // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	bool IsParam1 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool? IsIncluded { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsPropertyParam1 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsCatalogFormParam1 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
}