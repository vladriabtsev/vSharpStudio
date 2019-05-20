using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{Children.Count,nq}")]
    public partial class GroupListPropertiesTabs : IChildren, ICanAddSubNode, ICanGoRight, ICanGoLeft
    {
        partial void OnInit()
        {
                this.Name = "Tabs";
        }

        //protected override void OnParentChanged()
        //{
        //    if (this.Parent is GroupDocuments)
        //        this.NameUi = "Shared Properties";
        //    else
        //        this.NameUi = "Properties";
        //}
        //protected override void OnInitFromDto()
        //{
        //    if (this.Parent is GroupDocuments)
        //        this.NameUi = "Shared Properties";
        //    else
        //        this.NameUi = "Properties";
        //}

        #region ITreeNode


        #endregion ITreeNode
    }
}
