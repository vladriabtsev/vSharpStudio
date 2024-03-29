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
    [DebuggerDisplay("Role:{Name,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class Role : ICanGoLeft, ICanAddNode, INodeGenSettings, IEditableNode
    {
        [Browsable(false)]
        public GroupListRoles ParentGroupListRoles { get { Debug.Assert(this.Parent != null); return (GroupListRoles)this.Parent; } }
        [Browsable(false)]
        public IGroupListRoles ParentGroupListRolesI { get { Debug.Assert(this.Parent != null); return (IGroupListRoles)this.Parent; } }
        public static readonly string DefaultName = "Role";

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListRoles.Children;
        }
        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconWindowsForm"; } }
        //protected override string GetNodeIconName() { return "iconWindowsForm"; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            this.DefaultCatalogEditAccessSettings = EnumCatalogDetailAccess.C_MARK_DEL;
            this.DefaultCatalogPrintAccessSettings = EnumPrintAccess.PR_PRINT;
            this.DefaultConstantEditAccessSettings = EnumConstantAccess.CN_EDIT;
            this.DefaultConstantPrintAccessSettings = EnumPrintAccess.PR_PRINT;
            this.DefaultDocumentEditAccessSettings = EnumDocumentAccess.D_UNPOST;
            this.DefaultDocumentPrintAccessSettings = EnumPrintAccess.PR_PRINT;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            //this.ListMainViewForms.OnAddingAction = (t) =>
            //{
            //    t.IsNew = true;
            //};
            //this.ListMainViewForms.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            //this.ListMainViewForms.OnRemovedAction = (t) =>
            //{
            //    this.OnRemoveChild();
            //};
            //this.ListMainViewForms.OnClearedAction = () =>
            //{
            //    this.OnRemoveChild();
            //};
        }
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
                if (this.ParentGroupListRoles.ListRoles.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Role?)this.ParentGroupListRoles.ListRoles.GetPrev(this);
            if (prev == null)
                return;
            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupListRoles.ListRoles.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListRoles.ListRoles.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Role?)this.ParentGroupListRoles.ListRoles.GetNext(this);
            if (next == null)
                return;
            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupListRoles.ListRoles.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            Debug.Assert(this.Parent != null);
            var node = Role.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupListRoles.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Role(this.Parent);
            this.ParentGroupListRoles.Add(node);
            this.GetUniqueName(Role.DefaultName, node, this.ParentGroupListRoles.ListRoles);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListRoles.ListRoles.Remove(this);
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
