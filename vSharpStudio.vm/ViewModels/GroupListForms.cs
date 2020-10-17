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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListForms.Count,nq}")]
    public partial class GroupListForms : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, INewAndDeleteion
    {
        public ConfigNodesCollection<Form> Children { get { return this.ListForms; } }
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.ListForms;
        }
        public override bool HasChildren(object parent)
        {
            return this.ListForms.Count > 0;
        }
        partial void OnInit()
        {
            this.Name = "Forms";
            this.IsEditable = false;
            this.ListForms.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListForms.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListForms.OnRemovedAction = (t) => {
                this.GetIsHasMarkedForDeletion();
                this.GetIsHasNew();
            };
            this.ListForms.OnClearedAction = () => {
                this.GetIsHasMarkedForDeletion();
                this.GetIsHasNew();
            };
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public void AddForm(Form node)
        {
            this.NodeAddNewSubNode(node);
        }

        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Form node = null;
            if (node_impl == null)
            {
                node = new Form(this);
            }
            else
            {
                node = (Form)node_impl;
            }

            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Form.DefaultName, node, this.ListForms);
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
            foreach (var t in this.ListForms)
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
            foreach (var t in this.ListForms)
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
            var tlst = this.ListForms.ToList();
            foreach (var t in tlst)
            {
                if (t.IsMarkedForDeletion && t.IsNew)
                {
                    this.ListForms.Remove(t);
                    continue;
                }
                t.RemoveMarkedForDeletionIfNewObjects();
            }
        }
        public void RemoveMarkedForDeletionAndNewFlags()
        {
            foreach (var t in this.ListForms)
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
