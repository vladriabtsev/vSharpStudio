using System;
using System.Collections.Generic;
using System.ComponentModel;
using Google.Protobuf.WellKnownTypes;
using ViewModelBase;
using vSharpStudio.common.ViewModels;

namespace vPlugin.Sample2 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt Line:14
{
    
    public partial interface IDbConnectionStringSettings2 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt Line:34
    {
    	string StringSettings { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt Line:56
    }
    
    public partial interface IGeneratorDbAccessSettings2 // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt Line:34
    {
    	bool IsAccessParam1 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt Line:56
    	bool? IsAccessParam2 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt Line:56
    	string AccessParam3 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt Line:56
    	string? AccessParam4 { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt Line:56
    	bool IsGenerateNotValidCode { get; } // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\ModelInterfaces.tt Line:56
    }
}