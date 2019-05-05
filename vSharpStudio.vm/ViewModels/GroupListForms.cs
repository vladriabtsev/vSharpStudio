using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{Children.Count,nq}")]
    public partial class GroupListForms : IChildren, ICanAddSubNode, ICanGoRight, ICanGoLeft
    {
        partial void OnInit()
        {
            this.Name = "Forms";
        }
    }
}
