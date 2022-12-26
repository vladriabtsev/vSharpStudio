using System;
using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using ViewModelBase;

namespace vPlugin.Sample2 // ModelInterfaces.tt Line: 11
{
    
    public partial interface IDbConnectionStringSettings2 // ModelInterfaces.tt Line: 29
    {
    	string StringSettings { get; set; } // ModelInterfaces.tt Line: 51
    }
    
    public partial interface IGeneratorDbAccessSettings2 // ModelInterfaces.tt Line: 29
    {
    	bool IsAccessParam1 { get; set; } // ModelInterfaces.tt Line: 51
    	bool? IsAccessParam2 { get; set; } // ModelInterfaces.tt Line: 51
    	string AccessParam3 { get; set; } // ModelInterfaces.tt Line: 51
    	string AccessParam4 { get; set; } // ModelInterfaces.tt Line: 51
    	bool IsGenerateNotValidCode { get; set; } // ModelInterfaces.tt Line: 51
    }
}