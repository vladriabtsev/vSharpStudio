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
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class GroupListMainViewForms : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{ListMainViewForms.Count}";
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public GroupListCommon ParentGroupListCommon { get { Debug.Assert(this.Parent != null); return (GroupListCommon)this.Parent; } }
        [Browsable(false)]
        public IGroupListCommon ParentGroupListCommonI { get { Debug.Assert(this.Parent != null); return (IGroupListCommon)this.Parent; } }
        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListCommon.Children;
        }
        #endregion ITree

        public new ConfigNodesCollection<MainViewForm> Children { get { return this.ListMainViewForms; } }
        partial void OnCreated()
        {
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
            this._Name = Defaults.GroupViewFormsName;
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        //public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        //{
        //    Constant node = null;
        //    //if (node_impl == null)
        //    //{
        //    //    node = new Constant(this);
        //    //}
        //    //else
        //    //{
        //    //    node = (Constant)node_impl;
        //    //}

        //    //this.Add(node);
        //    //if (node_impl == null)
        //    //{
        //    //    this.GetUniqueName(Constant.DefaultName, node, this.ListConstants);
        //    //}

        //    //this.SetSelected(node);
        //    return node;
        //}
        #endregion Tree operations
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                nameof(this.Parent),
                nameof(this.Children)
            };
            return lst.ToArray();
        }
    }
}
