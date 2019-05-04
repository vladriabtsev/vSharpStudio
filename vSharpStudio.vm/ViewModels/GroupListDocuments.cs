using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} documents:{ListDocuments.Count,nq}")]
    public partial class GroupListDocuments : IChildren, ICanNotLeft
    {
        partial void OnInit()
        {
            this.Name = "Documents";
        }

        #region ITreeNode

        public new string NodeText { get { return this.Name; } }

        #endregion ITreeNode
    }
}
