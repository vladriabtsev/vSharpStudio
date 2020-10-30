using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListJournals.Count,nq}")]
    public partial class GroupListJournals : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup
    {
        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as ConfigModel;
            return p.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ConfigNodesCollection<Journal> Children { get { return this.ListJournals; } }
        partial void OnInit()
        {
            this._Name = "Journals";
            this.IsEditable = false;
            this.ListJournals.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListJournals.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListJournals.OnRemovedAction = (t) => {
                this.OnRemoveChild();
            };
            this.ListJournals.OnClearedAction = () => {
                this.OnRemoveChild();
            };
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
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
