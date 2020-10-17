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
    public partial class GroupListRoles : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, INewAndDeleteion
    {
        public ConfigNodesCollection<Role> Children { get { return this.ListRoles; } }
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.ListRoles;
        }
        public override bool HasChildren(object parent)
        {
            return this.ListRoles.Count > 0;
        }
        partial void OnInit()
        {
            this.Name = "Roles";
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
                this.GetIsHasMarkedForDeletion();
                this.GetIsHasNew();
            };
            this.ListRoles.OnClearedAction = () => {
                this.GetIsHasMarkedForDeletion();
                this.GetIsHasNew();
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
        public bool IsNew { get { return false; } set { } }
        public bool IsMarkedForDeletion { get { return false; } set { } }
        partial void OnIsHasMarkedForDeletionChanged()
        {
            if (this.IsNotNotifying)
                return;
            if (this.IsHasMarkedForDeletion)
            {
                (this.Parent as INewAndDeleteion).IsHasMarkedForDeletion = true;
            }
            else
            {
                var p = (this.Parent as INewAndDeleteion);
                p.GetIsHasMarkedForDeletion();
            }
        }
        partial void OnIsHasNewChanged()
        {
            if (this.IsNotNotifying)
                return;
            if (this.IsHasNew)
            {
                (this.Parent as INewAndDeleteion).IsHasNew = true;
            }
            else
            {
                var p = (this.Parent as INewAndDeleteion);
                p.GetIsHasNew();
            }
        }

        public bool GetIsHasMarkedForDeletion()
        {
            foreach (var t in this.ListRoles)
            {
                if (t.IsMarkedForDeletion || t.GetIsHasMarkedForDeletion())
                {
                    this.IsHasMarkedForDeletion = true;
                    return true;
                }
            }
            this.IsHasMarkedForDeletion = false;
            return false;
        }

        public bool GetIsHasNew()
        {
            foreach (var t in this.ListRoles)
            {
                if (t.IsNew || t.GetIsHasNew())
                {
                    this.IsHasNew = true;
                    return true;
                }
            }
            this.IsHasNew = false;
            return false;
        }
        #endregion Tree operations
        public void RemoveMarkedForDeletionIfNewObjects()
        {
            var tlst = this.ListRoles.ToList();
            foreach (var t in tlst)
            {
                if (t.IsMarkedForDeletion && t.IsNew)
                {
                    this.ListRoles.Remove(t);
                    continue;
                }
                t.RemoveMarkedForDeletionIfNewObjects();
            }
        }
        public void RemoveMarkedForDeletionAndNewFlags()
        {
            foreach (var t in this.ListRoles)
            {
                t.RemoveMarkedForDeletionAndNewFlags();
                t.IsNew = false;
                t.IsMarkedForDeletion = false;
            }
            Debug.Assert(!this.IsHasMarkedForDeletion);
            Debug.Assert(!this.IsHasNew);
        }
    }
}
