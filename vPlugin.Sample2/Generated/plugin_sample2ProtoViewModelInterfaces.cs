using System;
using System.Collections.Generic;
using System.ComponentModel;
using Google.Protobuf.WellKnownTypes;
using vSharpStudio.common.ViewModels;

namespace vPlugin.Sample2 
{
    
    public partial interface IDbConnectionStringSettings2 
    {
    	string StringSettings { get; } 
    }
    
    public partial interface IGeneratorDbAccessSettings2 
    {
    	bool IsAccessParam1 { get; } 
    	bool? IsAccessParam2 { get; } 
    	string AccessParam3 { get; } 
    	string? AccessParam4 { get; } 
    	bool IsGenerateNotValidCode { get; } 
    }
}