using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} constants:{ListConstants.Count,nq}")]
    public partial class GroupListConstants : IChildren, ICanNotLeft
    {
        partial void OnInit()
        {
            this.Name = "Constants";
        }

        #region ITreeNode

        public new string NodeText { get { return this.Name; } }

        #endregion ITreeNode
    }
}
