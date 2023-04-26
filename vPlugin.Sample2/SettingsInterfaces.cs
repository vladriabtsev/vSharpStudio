using System;
using System.Collections.Generic;
using System.ComponentModel;
using Google.Protobuf.WellKnownTypes;
using ViewModelBase;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace vPlugin.Sample2 // ModelInterfaces.tt Line: 13
{
    
    public partial interface IDbConnectionStringSettings2 // ModelInterfaces.tt Line: 33
    {
    	string StringSettings { get; set; } // ModelInterfaces.tt Line: 55
    }
    
    public partial interface IGeneratorDbAccessSettings2 // ModelInterfaces.tt Line: 33
    {
    	bool IsAccessParam1 { get; set; } // ModelInterfaces.tt Line: 55
    	bool? IsAccessParam2 { get; set; } // ModelInterfaces.tt Line: 55
    	string AccessParam3 { get; set; } // ModelInterfaces.tt Line: 55
    	string AccessParam4 { get; set; } // ModelInterfaces.tt Line: 55
    	bool IsGenerateNotValidCode { get; set; } // ModelInterfaces.tt Line: 55
    }
}