﻿using System;
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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListMainViewForms.Count,nq}")]
    public partial class GroupListMainViewForms : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup
    {
        [BrowsableAttribute(false)]
        public GroupListCommon ParentGroupListCommon { get { return (GroupListCommon)this.Parent; } }
        [BrowsableAttribute(false)]
        public IGroupListCommon ParentGroupListCommonI { get { return (IGroupListCommon)this.Parent; } }
        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
                return this.ParentGroupListCommon.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ConfigNodesCollection<MainViewForm> Children { get { return this.ListMainViewForms; } }
        partial void OnCreated()
        {
            this._Name = Defaults.ConstantsGroupName;
            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this.ListMainViewForms.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListMainViewForms.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListMainViewForms.OnRemovedAction = (t) => {
                this.OnRemoveChild();
            };
            this.ListMainViewForms.OnClearedAction = () => {
                this.OnRemoveChild();
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
            //if (node_impl == null)
            //{
            //    node = new Constant(this);
            //}
            //else
            //{
            //    node = (Constant)node_impl;
            //}

            //this.Add(node);
            //if (node_impl == null)
            //{
            //    this.GetUniqueName(Constant.DefaultName, node, this.ListConstants);
            //}

            //this.SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
