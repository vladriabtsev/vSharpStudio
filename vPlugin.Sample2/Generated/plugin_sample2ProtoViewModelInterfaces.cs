using System;
using System.Collections.Generic;
using System.ComponentModel;
using Google.Protobuf.WellKnownTypes;
using vSharpStudio.common.ViewModels;

namespace vPlugin.Sample2 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:14
{
    
    public partial interface IDbConnectionStringSettings2 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    {
    	string StringSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:65
    }
    
    public partial interface IGeneratorDbAccessSettings2 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:36
    {
    	bool IsAccessParam1 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:65
    	bool? IsAccessParam2 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:65
    	string AccessParam3 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:65
    	string? AccessParam4 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:65
    	bool IsGenerateNotValidCode { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:65
    }
}