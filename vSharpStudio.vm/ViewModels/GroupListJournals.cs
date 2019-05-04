using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} journals:{ListJournals.Count,nq}")]
    public partial class GroupListJournals : IChildren, ICanNotLeft
    {
        partial void OnInit()
        {
            this.Name = "Journals";
        }
        
        #region ITreeNode

        [BrowsableAttribute(false)]
        public new string NodeText { get { return this.Name; } }

        #endregion ITreeNode
    }
}
