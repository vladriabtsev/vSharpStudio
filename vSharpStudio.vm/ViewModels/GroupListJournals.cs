using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListJournals.Count,nq}")]
    public partial class GroupListJournals : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings
    {
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.ListJournals;
        }
        public override bool HasChildren(object parent)
        {
            return this.ListJournals.Count > 0;
        }
        partial void OnInit()
        {
            this.Name = "Journals";
            this.IsEditable = false;
            this.ListJournals.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
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
            {
                node = new Journal(this);
            }
            else
            {
                node = (Journal)node_impl;
            }

            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Journal.DefaultName, node, this.ListJournals);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

    }
}
