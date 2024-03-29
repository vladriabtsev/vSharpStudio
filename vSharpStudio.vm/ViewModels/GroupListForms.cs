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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListForms.Count,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class GroupListForms : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNodeGroup
    {
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            if (this.Parent is Catalog c)
            {
                return c.Children;
            }
            else if (this.Parent is Document d)
            {
                return d.Children;
            }
            throw new NotImplementedException();
        }
        #endregion ITree

        public new ConfigNodesCollection<Form> Children { get { return this.ListForms; } }
        partial void OnCreated()
        {
            this._Name = "Forms";
            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this.ListForms.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListForms.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListForms.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListForms.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public void AddForm(Form node)
        {
            this.NodeAddNewSubNode(node);
        }

        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            Form node = null!;
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
                this.GetUniqueName(Defaults.FormName, node, this.ListForms);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        public IForm AddCatalogForm(FormType formType)
        {
            Debug.Assert(this.Parent is Catalog);
            var form = new Form(this) { EnumFormType = formType };
            var row = (FormGridSystemRow)form.GridSystem.AddGridRow();
            var c = this.Parent as Catalog;
            Debug.Assert(c != null);
            FormGridSystemColumn? col = null;
            FormAutoLayoutBlock? ablock = null;
            switch (formType)
            {
                case FormType.ListNarrow:
                    form.Name = "ViewListNarrow";
                    if (c.UseTree && c.UseSeparateTreeForFolders)
                    {
                        col = (FormGridSystemColumn)row.AddGridSystemColumn();
                        ablock = (FormAutoLayoutBlock)col.AddAutoLayoutBlock();
                        var tree = (FormTree)ablock.AddTree();
                        if (c.Folder.GetUseCodeProperty())
                            tree.ListGuidProperties.Add(c.PropertyCodeGuid);
                        if (c.Folder.GetUseNameProperty())
                            tree.ListGuidProperties.Add(c.PropertyNameGuid);
                    }
                    col = (FormGridSystemColumn)row.AddGridSystemColumn();
                    ablock = (FormAutoLayoutBlock)col.AddAutoLayoutBlock();
                    var dg = (FormDataGrid)ablock.AddDataGrid();
                    if (c.GetUseCodeProperty())
                        dg.ListGuidProperties.Add(c.PropertyCodeGuid);
                    if (c.GetUseNameProperty())
                        dg.ListGuidProperties.Add(c.PropertyNameGuid);
                    break;
                case FormType.ListWide:
                    form.Name = "ViewListWide";
                    if (c.UseTree && c.UseSeparateTreeForFolders)
                    {
                        col = (FormGridSystemColumn)row.AddGridSystemColumn();
                        ablock = (FormAutoLayoutBlock)col.AddAutoLayoutBlock();
                        var tree = (FormTree)ablock.AddTree();
                        if (c.Folder.GetUseCodeProperty())
                            tree.ListGuidProperties.Add(c.PropertyCodeGuid);
                        if (c.Folder.GetUseNameProperty())
                            tree.ListGuidProperties.Add(c.PropertyNameGuid);
                        if (c.Folder.GetUseDescriptionProperty())
                            tree.ListGuidProperties.Add(c.PropertyDescriptionGuid);
                    }
                    col = (FormGridSystemColumn)row.AddGridSystemColumn();
                    ablock = (FormAutoLayoutBlock)col.AddAutoLayoutBlock();
                    dg = (FormDataGrid)ablock.AddDataGrid();
                    if (c.GetUseCodeProperty())
                        dg.ListGuidProperties.Add(c.PropertyCodeGuid);
                    if (c.GetUseNameProperty())
                        dg.ListGuidProperties.Add(c.PropertyNameGuid);
                    if (c.GetUseDescriptionProperty())
                        dg.ListGuidProperties.Add(c.PropertyDescriptionGuid);
                    break;
                default:
                    throw new NotImplementedException();
            }
            this.ListForms.Add(form);
            return form;
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                this.GetPropertyName(() => this.Description),
                this.GetPropertyName(() => this.Guid),
                this.GetPropertyName(() => this.NameUi),
                this.GetPropertyName(() => this.Parent),
                this.GetPropertyName(() => this.Children)
            };
            return lst.ToArray();
        }
    }
}
