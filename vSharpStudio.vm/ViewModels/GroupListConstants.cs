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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListConstants.Count,nq}")]
    public partial class GroupListConstants : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, INewAndDeleteion
    {
        public ConfigNodesCollection<Constant> Children { get { return this.ListConstants; } }
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.ListConstants;
        }
        public override bool HasChildren(object parent)
        {
            return this.ListConstants.Count > 0;
        }
        partial void OnInit()
        {
            this.Name = Defaults.ConstantsGroupName;
            this.IsEditable = false;
            this.ListConstants.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListConstants.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListConstants.OnRemovedAction = (t) => {
                this.GetIsHasMarkedForDeletion();
                this.GetIsHasNew();
            };
            this.ListConstants.OnClearedAction = () => {
                this.GetIsHasMarkedForDeletion();
                this.GetIsHasNew();
            };
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public Constant AddConstant(string name, DataType type = null)
        {
            Constant node = new Constant(this) { Name = name, DataType = new DataType() };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Constant node = null;
            if (node_impl == null)
            {
                node = new Constant(this);
            }
            else
            {
                node = (Constant)node_impl;
            }

            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Constant.DefaultName, node, this.ListConstants);
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
            foreach (var t in this.ListConstants)
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
            foreach (var t in this.ListConstants)
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
            var tlst = this.ListConstants.ToList();
            foreach (var t in tlst)
            {
                if (t.IsMarkedForDeletion && t.IsNew)
                {
                    this.ListConstants.Remove(t);
                    continue;
                }
                //t.RemoveMarkedForDeletionIfNewObjects();
            }
        }
        public void RemoveMarkedForDeletionAndNewFlags()
        {
            foreach (var t in this.ListConstants)
            {
                //t.RemoveMarkedForDeletionAndNewFlags();
                t.IsNew = false;
                t.IsMarkedForDeletion = false;
            }
            Debug.Assert(!this.IsHasMarkedForDeletion);
            Debug.Assert(!this.IsHasNew);
        }
    }
}
