using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Linq;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Diagnostics.Contracts;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using FluentValidation.Results;

namespace vSharpStudio.vm.ViewModels
{
    //[DebuggerDisplay("RegisterDocToReg: Doc:{RelativeAppProjectPath,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class RegisterRegPropToDocProp
    {
        partial void OnCreated()
        {
            this.Guid = System.Guid.NewGuid().ToString();
        }
    }
}
