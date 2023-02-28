using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListDetails.Count,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class GroupListDetails : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNodeGroup
    {
        [BrowsableAttribute(false)]
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
            else if (this.Parent is CatalogFolder cf)
            {
                return cf.Children;
            }
            else if (this.Parent is Document d)
            {
                return d.Children;
            }
            else if (this.Parent is Detail dt)
            {
                return dt.Children;
            }
            throw new NotImplementedException();
        }
        #endregion ITree

        new public ConfigNodesCollection<Detail> Children { get { return this.ListDetails; } }
        partial void OnCreated()
        {
            this._Name = "Details";
            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this.ListDetails.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListDetails.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListDetails.OnRemovedAction = (t) => {
                this.OnRemoveChild();
            };
            this.ListDetails.OnClearedAction = () => {
                this.OnRemoveChild();
            };
        }
        public int IndexOf(IDetail det)
        {
            return this.ListDetails.IndexOf((det as Detail)!);
        }
        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public Detail AddPropertiesTab(string name)
        {
            var node = new Detail(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            Detail node = null!;
            if (node_impl == null)
            {
                node = new Detail(this);
            }
            else
            {
                node = (Detail)node_impl;
            }
            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.DetailName, node, this.ListDetails);
            }
            var cfg = (Config)this.Cfg;
            node.ShortId = cfg.Model.LastDetailShortId + 1;
            cfg.Model.LastDetailShortId = node.ShortId;
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        public Detail AddTab(string name)
        {
            var node = new Detail(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            lst.Add(this.GetPropertyName(() => this.Description));
            lst.Add(this.GetPropertyName(() => this.Guid));
            lst.Add(this.GetPropertyName(() => this.NameUi));
            lst.Add(this.GetPropertyName(() => this.Parent));
            lst.Add(this.GetPropertyName(() => this.Children));
            return lst.ToArray();
        }
        public bool GetIsGridSortable()
        {
            if (this.IsGridSortable == EnumUseType.Yes)
                return true;
            if (this.IsGridSortable == EnumUseType.No)
                return false;
            if (this.Parent is Catalog c)
                return c.IsGridSortableGet();
            if (this.Parent is Document d)
                return d.IsGridSortableGet();
            if (this.Parent is Detail dd)
                return dd.IsGridSortableGet();
            if (this.Parent is CatalogFolder cf)
                return cf.IsGridSortableGet();
            throw new NotImplementedException();
        }
        public bool GetIsGridFilterable()
        {
            if (this.IsGridFilterable == EnumUseType.Yes)
                return true;
            if (this.IsGridFilterable == EnumUseType.No)
                return false;
            if (this.Parent is Catalog c)
                return c.IsGridFilterableGet();
            if (this.Parent is Document d)
                return d.IsGridFilterableGet();
            if (this.Parent is Detail dd)
                return dd.IsGridFilterableGet();
            if (this.Parent is CatalogFolder cf)
                return cf.IsGridFilterableGet();
            throw new NotImplementedException();
        }
        public bool GetIsGridSortableCustom()
        {
            if (this.IsGridSortableCustom == EnumUseType.Yes)
                return true;
            if (this.IsGridSortableCustom == EnumUseType.No)
                return false;
            if (this.Parent is Catalog c)
                return c.IsGridSortableCustomGet();
            if (this.Parent is Document d)
                return d.IsGridSortableCustomGet();
            if (this.Parent is Detail dd)
                return dd.IsGridSortableCustomGet();
            if (this.Parent is CatalogFolder cf)
                return cf.IsGridSortableCustomGet();
            throw new NotImplementedException();
        }
        public bool GetUseCodeProperty()
        {
            if (this.UseCodeProperty == EnumUseType.Yes)
                return true;
            if (this.UseCodeProperty == EnumUseType.No)
                return false;
            if (this.Parent is Catalog c)
                return c.GetUseCodeProperty();
            if (this.Parent is Document d)
                return d.ParentGroupListDocuments.ParentGroupDocuments.ParentModel.UseCodeProperty;
            if (this.Parent is Detail dd)
                return dd.GetUseCodeProperty();
            if (this.Parent is CatalogFolder cf)
                return cf.GetUseCodeProperty();
            throw new NotImplementedException();
        }
        public bool GetUseNameProperty()
        {
            if (this.UseNameProperty == EnumUseType.Yes)
                return true;
            if (this.UseNameProperty == EnumUseType.No)
                return false;
            if (this.Parent is Catalog c)
                return c.GetUseNameProperty();
            if (this.Parent is Document d)
                return d.ParentGroupListDocuments.ParentGroupDocuments.ParentModel.UseNameProperty;
            if (this.Parent is Detail dd)
                return dd.GetUseNameProperty();
            if (this.Parent is CatalogFolder cf)
                return cf.GetUseNameProperty();
            throw new NotImplementedException();
        }
        public bool GetUseDescriptionProperty()
        {
            if (this.UseDescriptionProperty == EnumUseType.Yes)
                return true;
            if (this.UseDescriptionProperty == EnumUseType.No)
                return false;
            if (this.Parent is Catalog c)
                return c.GetUseDescriptionProperty();
            if (this.Parent is Document d)
                return d.ParentGroupListDocuments.ParentGroupDocuments.ParentModel.UseDescriptionProperty;
            if (this.Parent is Detail dd)
                return dd.GetUseDescriptionProperty();
            if (this.Parent is CatalogFolder cf)
                return cf.GetUseDescriptionProperty();
            throw new NotImplementedException();
        }
    }
}
