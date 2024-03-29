﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("MainViewForm:{Name,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class MainViewForm : ICanGoLeft, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup
    {
        [Browsable(false)]
        public GroupListMainViewForms ParentGroupListMainViewForms { get { Debug.Assert(this.Parent != null); return (GroupListMainViewForms) this.Parent; } }
        [Browsable(false)]
        public IGroupListMainViewForms ParentGroupListMainViewFormsI { get { Debug.Assert(this.Parent != null); return (IGroupListMainViewForms)this.Parent; } }
        public static readonly string DefaultName = "MainViewForm";

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListMainViewForms.Children;
        }
        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconWindowsForm"; } }
        //protected override string GetNodeIconName() { return "iconWindowsForm"; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            //    Init();
        }
        //protected override void OnInitFromDto()
        //{
        //    Init();
        //}
        //private void Init()
        //{
        //    this.ListMainViewForms.OnAddingAction = (t) =>
        //    {
        //        t.IsNew = true;
        //    };
        //    this.ListMainViewForms.OnAddedAction = (t) =>
        //    {
        //        t.OnAdded();
        //    };
        //    this.ListMainViewForms.OnRemovedAction = (t) => {
        //        this.OnRemoveChild();
        //    };
        //    this.ListMainViewForms.OnClearedAction = () => {
        //        this.OnRemoveChild();
        //    };
        //}
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
            //this.GroupProperties.AddAllAppGenSettingsVmsToNode();
            //this.GroupForms.AddAllAppGenSettingsVmsToNode();
            //this.GroupReports.AddAllAppGenSettingsVmsToNode();
        }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListMainViewForms.ListMainViewForms.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Form?)this.ParentGroupListMainViewForms.ListMainViewForms.GetPrev(this);
            if (prev == null)
                return;
            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupListMainViewForms.ListMainViewForms.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListMainViewForms.ListMainViewForms.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Form?)this.ParentGroupListMainViewForms.ListMainViewForms.GetNext(this);
            if (next == null)
                return;
            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupListMainViewForms.ListMainViewForms.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            Debug.Assert(this.Parent != null);
            var node = MainViewForm.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupListMainViewForms.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new MainViewForm(this.Parent);
            this.ParentGroupListMainViewForms.Add(node);
            this.GetUniqueName(MainViewForm.DefaultName, node, this.ParentGroupListMainViewForms.ListMainViewForms);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListMainViewForms.ListMainViewForms.Remove(this);
        }
        #endregion Tree operations
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                this.GetPropertyName(() => this.Parent),
                this.GetPropertyName(() => this.Children)
            };
            return lst.ToArray();
        }
    }
}
