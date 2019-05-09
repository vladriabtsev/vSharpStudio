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
    [DebuggerDisplay("Group:{Name,nq} Count:{Children.Count,nq}")]
    public partial class GroupListProperties : IChildren, ICanAddSubNode, ICanGoRight, ICanGoLeft
    {
        partial void OnInit()
        {
            if (this.Parent is GroupDocuments)
                this.Name = "Shared";
            else
                this.Name = "Properties";
        }

        protected override void OnParentChanged()
        {
            if (this.Parent is GroupDocuments)
                this.Name = "Shared";
            else
                this.Name = "Properties";
        }
        protected override void OnInitFromDto()
        {
            if (this.Parent is GroupDocuments)
                this.Name = "Shared";
            else
                this.Name = "Properties";
        }

        #region ITreeNode


        #endregion ITreeNode
    }
}
