using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
            this.ListConstants.OnRemovedAction = (t) =>
            {
                var cfg = this.GetConfig();
                cfg.DicDeletedNodesInCurrentSession[t.Guid] = t;
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
        public override void MarkForDeletion()
        {
            this.IsMarkedForDeletion = !this.IsMarkedForDeletion;
        }
        partial void OnIsMarkedForDeletionChanged()
        {
            (this.Parent as INewAndDeleteion).IsMarkedForDeletion = this.IsMarkedForDeletion;
        }
        partial void OnIsNewChanged()
        {
            (this.Parent as INewAndDeleteion).IsNew = this.IsNew;
        }
        #endregion Tree operations

    }
}
