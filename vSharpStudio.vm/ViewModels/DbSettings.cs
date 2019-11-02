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

namespace vSharpStudio.vm.ViewModels
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?view=netframework-4.7.2
    [DebuggerDisplay("DbSettings: Schema={DbSchema,nq}, {IdGenerator,nq}, PkType={KeyType,nq}, PkName={KeyName, nq}, Conn={ConnectionStringName, nq}")]
    public partial class DbSettings
    {
        partial void OnInit()
        {
        }
        [BrowsableAttribute(false)]
        public ITreeConfigNode Parent { get; set; }
    }
}
