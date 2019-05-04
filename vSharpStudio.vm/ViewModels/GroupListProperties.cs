using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} properties:{ListProperties.Count,nq}")]
    public partial class GroupListProperties
    {
        partial void OnInit()
        {
            this.Name = "Properties";
        }

        #region ITreeNode

        [BrowsableAttribute(false)]
        public new string NodeText { get { return this.Name + " " + this.ListProperties.Count; } }

        #endregion ITreeNode
    }
}
