using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{Children.Count,nq}")]
    public partial class GroupListJournals : IChildren, ICanAddSubNode, ICanGoRight
    {
        partial void OnInit()
        {
            this.Name = "Journals";
        }
    }
}
