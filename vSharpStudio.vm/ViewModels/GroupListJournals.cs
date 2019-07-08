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
        public void AddJournal(Journal node)
        {
            this.NodeAddNewSubNode(node);
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Journal node = null;
            if (node_impl == null)
                node = new Journal();
            else
                node = (Journal)node_impl;
            this.Add(node);
            if (node_impl == null)
                GetUniqueName(Journal.DefaultName, node, this.ListJournals);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
