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
    public partial class GroupListForms : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNodeGroup
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{ListForms.Count}";
        }
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
            this._Name = Defaults.GroupFormsName;
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
                case FormType.ListComboBox:
                    form.Name = "ViewListDefault";
                    if (c.UseTree && c.UseSeparateTreeForFolders)
                    {
                        col = (FormGridSystemColumn)row.AddGridSystemColumn();
                        ablock = (FormAutoLayoutBlock)col.AddAutoLayoutBlock();
                        var tree = (FormTree)ablock.AddTree();
                        if (c.Folder.GetUseCodeProperty())
                            tree.ListGuidProperties.Add(c.Cfg.Model.PropertyCtlgCodeGuid);
                        if (c.Folder.GetUseNameProperty())
                            tree.ListGuidProperties.Add(c.Cfg.Model.PropertyCtlgNameGuid);
                    }
                    col = (FormGridSystemColumn)row.AddGridSystemColumn();
                    ablock = (FormAutoLayoutBlock)col.AddAutoLayoutBlock();
                    var dg = (FormDataGrid)ablock.AddDataGrid();
                    if (c.GetUseCodeProperty())
                        dg.ListGuidProperties.Add(c.Cfg.Model.PropertyCtlgCodeGuid);
                    if (c.GetUseNameProperty())
                        dg.ListGuidProperties.Add(c.Cfg.Model.PropertyCtlgNameGuid);
                    break;
                case FormType.ListDataGrid:
                    form.Name = "ViewListCustom";
                    if (c.UseTree && c.UseSeparateTreeForFolders)
                    {
                        col = (FormGridSystemColumn)row.AddGridSystemColumn();
                        ablock = (FormAutoLayoutBlock)col.AddAutoLayoutBlock();
                        var tree = (FormTree)ablock.AddTree();
                        if (c.Folder.GetUseCodeProperty())
                            tree.ListGuidProperties.Add(c.Cfg.Model.PropertyCtlgCodeGuid);
                        if (c.Folder.GetUseNameProperty())
                            tree.ListGuidProperties.Add(c.Cfg.Model.PropertyCtlgNameGuid);
                        if (c.Folder.GetUseDescriptionProperty())
                            tree.ListGuidProperties.Add(c.Cfg.Model.PropertyCtlgDescriptionGuid);
                    }
                    col = (FormGridSystemColumn)row.AddGridSystemColumn();
                    ablock = (FormAutoLayoutBlock)col.AddAutoLayoutBlock();
                    dg = (FormDataGrid)ablock.AddDataGrid();
                    if (c.GetUseCodeProperty())
                        dg.ListGuidProperties.Add(c.Cfg.Model.PropertyCtlgCodeGuid);
                    if (c.GetUseNameProperty())
                        dg.ListGuidProperties.Add(c.Cfg.Model.PropertyCtlgNameGuid);
                    if (c.GetUseDescriptionProperty())
                        dg.ListGuidProperties.Add(c.Cfg.Model.PropertyCtlgDescriptionGuid);
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
                nameof(this.Description),
                nameof(this.Guid),
                nameof(this.NameUi),
                nameof(this.Parent),
                nameof(this.Children)
            };
            return lst.ToArray();
        }
    }
}
