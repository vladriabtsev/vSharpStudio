﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class FormGridSystemColumn : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNodeGroup
    {
        [Browsable(false)]
        public FormGridSystemRow ParentFormGridSystemRow { get { Debug.Assert(this.Parent != null); return (FormGridSystemRow)this.Parent; } }
        [Browsable(false)]
        public IFormGridSystemRow ParentFormGridSystemRowI { get { Debug.Assert(this.Parent != null); return (IFormGridSystemRow)this.Parent; } }

        #region ITree

        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentFormGridSystemRow.Children;
        }
        #region Tree operations
        #endregion Tree operations

        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconFolder"; } }
        partial void OnCreated()
        {
            this._Name = "Column";
            this._Description = "Grid System Column";
            this.IsIncludableInModels = true;
#if DEBUG
            // SubNodes.Add(this.GroupConstants, 1);
#endif
            this.RefillChildren();
            //Init();
        }
        //protected override void OnInitFromDto()
        //{
        //    Init();
        //}
        //private void Init()
        //{
        //    this.ListRoles.OnAddingAction = (t) =>
        //    {
        //        t.IsNew = true;
        //    };
        //    this.ListRoles.OnAddedAction = (t) =>
        //    {
        //        t.OnAdded();
        //    };
        //    this.ListRoles.OnRemovedAction = (t) => {
        //        this.OnRemoveChild();
        //    };
        //    this.ListRoles.OnClearedAction = () => {
        //        this.OnRemoveChild();
        //    };
        //}
        protected override void OnInitFromDto()
        {
            base.OnInitFromDto();
            this.RefillChildren();
        }
        void RefillChildren()
        {
            if (this.Children.Count > 0)
                return;
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
        }

        public FormGridSystemColumn(ITreeConfigNode parent, string name)
            : this(parent)
        {
            this.Name = name;
        }
        [ExpandableObjectAttribute()]
        public dynamic? Setting { get; set; }

        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            //lst.Add(this.GetPropertyName(() => this.Parent));
            //lst.Add(this.GetPropertyName(() => this.Children));
            return lst.ToArray();
        }
        public IFormAutoLayoutBlock AddAutoLayoutBlock(string name = "")
        {
            var node = new FormAutoLayoutBlock(this) { Name = name };
            this.FormBlock = node;
            return node;
        }
    }
}
