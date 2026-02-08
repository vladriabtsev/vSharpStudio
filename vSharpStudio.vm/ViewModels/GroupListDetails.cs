using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class GroupListDetails : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNodeGroup
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{ListDetails.Count}";
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

        public new ConfigNodesCollection<Detail> Children { get { return this.ListDetails; } }
        partial void OnCreated()
        {
            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        partial void OnSortTypeChanged() { this.ListDetails.Sort(this.SortType); }
        private void Init()
        {
            OnSortTypeChanged();
            this.ListDetails.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListDetails.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListDetails.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListDetails.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
            this._Name = Defaults.DetailsGroupName;
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
            node.ShortId = ++this.LastShortId;
            node.ShortRefId = cfg.Model.LastTypeShortRefIdForNode(node, node.ShortId);
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
            var lst = new List<string>
            {
                nameof(this.Description),
                nameof(this.Guid),
                nameof(this.NameUi),
                nameof(this.Parent),
                nameof(this.Children)
            };
            return [.. lst];
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
    }
}
