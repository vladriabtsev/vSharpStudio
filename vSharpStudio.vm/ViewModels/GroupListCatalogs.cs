using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} sub_catalogs:{ListCatalogs.Count,nq}")]
    public partial class GroupListCatalogs
    {
        partial void OnInit()
        {
            this.Name = "Catalogs";
        }

        #region ITreeNode

        [BrowsableAttribute(false)]
        public new string NodeText { get { return this.Name; } }

        #endregion ITreeNode
    }
}
