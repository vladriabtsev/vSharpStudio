using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Windows;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using static Proto.Config.proto_data_type.Types;

namespace vSharpStudio.vm.ViewModels
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?view=netframework-4.7.2
    //[DebuggerDisplay("SettingsForDbPlugin:{DataType.GetTypeDesc(this),nq}")]
    public partial class SettingsForDbPlugin
    {
        partial void OnInit()
        {
        }
        [BrowsableAttribute(false)]
        public ITreeConfigNode Parent { get; set; }
    }
}
