using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListRoles.Count,nq}")]
    public partial class GroupListRoles : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNodeGroup
    {
        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as Config;
            return p.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree
        new public ConfigNodesCollection<Role> Children { get { return this.ListRoles; } }
        partial void OnInit()
        {
            this._Name = "Roles";
            this.IsEditable = false;
            this.ListRoles.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListRoles.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListRoles.OnRemovedAction = (t) => {
                this.OnRemoveChild();
            };
            this.ListRoles.OnClearedAction = () => {
                this.OnRemoveChild();
            };
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public void AddForm(Role node)
        {
            this.NodeAddNewSubNode(node);
        }

        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Role node = null;
            if (node_impl == null)
            {
                node = new Role(this);
            }
            else
            {
                node = (Role)node_impl;
            }

            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Role.DefaultName, node, this.ListRoles);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
