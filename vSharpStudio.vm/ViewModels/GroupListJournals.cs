using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListJournals.Count,nq}")]
    public partial class GroupListJournals : ICanAddSubNode, ICanGoRight
    {
        partial void OnInit()
        {
            this.Name = "Journals";
            this.IsEditable = false;
        }

        #region Tree operations
        public override ITreeConfigNode NodeAddNewSubNode()
        {
            var node = new Journal();
            this.Add(node);
            GetUniqueName(Journal.DefaultName, node, this.ListJournals);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
