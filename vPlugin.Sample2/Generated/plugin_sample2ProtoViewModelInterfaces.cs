using System;
using System.Collections.Generic;
using System.ComponentModel;
using Google.Protobuf.WellKnownTypes;
using vSharpStudio.common.ViewModels;

namespace vPlugin.Sample2 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:13
{
    
    public partial interface IDbConnectionStringSettings2 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	string StringSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
    
    public partial interface IGeneratorDbAccessSettings2 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:33
    {
    	bool IsAccessParam1 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool? IsAccessParam2 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string AccessParam3 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	string? AccessParam4 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    	bool IsGenerateNotValidCode { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenVmFromProto\ModelInterfaces.tt Line:55
    }
}