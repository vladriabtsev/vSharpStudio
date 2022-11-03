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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListCatalogs.Count,nq}")]
    public partial class GroupListCatalogs : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup
    {
        [BrowsableAttribute(false)]
        public bool IsNew { get { return false; } }
        [BrowsableAttribute(false)]
        public Model ParentModel { get { return (Model)this.Parent; } }
        [BrowsableAttribute(false)]
        public IModel ParentModelI { get { return (IModel)this.Parent; } }

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            return this.ParentModel.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Catalog node = null;
            if (node_impl == null)
            {
                node = new Catalog(this);
            }
            else
            {
                node = (Catalog)node_impl;
            }

            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.CatalogName, node, this.ListCatalogs);
            }
            var cfg = (Config)this.GetConfig();
            node.ShortId = cfg.Model.LastCatalogShortId + 1;
            cfg.Model.LastCatalogShortId = node.ShortId;
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        public ConfigNodesCollection<Catalog> Children { get { return this.ListCatalogs; } }

        partial void OnCreated()
        {
            this._Name = Defaults.CatalogsGroupName;
            this.PrefixForDbTables = "Ctlg";
            this.IsEditable = false;
            this.UseCodeProperty = EnumUseType.Default;
            this.UseNameProperty = EnumUseType.Default;
            this.UseDescriptionProperty = EnumUseType.Default;
            this.UseCodePropertyInSeparateTree = true;
            this.UseNamePropertyInSeparateTree = true;

            this.ShortIdTypeForCacheKey = "c";
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }

        private void Init()
        {
            //if (this.Parent is Catalog)
            //{
            //    this.NameUi = "Sub Catalogs";
            //}
            this.ListCatalogs.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListCatalogs.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListCatalogs.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListCatalogs.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
        }
        public int IndexOf(ICatalog cat)
        {
            return this.ListCatalogs.IndexOf(cat as Catalog);
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            lst.Add(this.GetPropertyName(() => this.Description));
            lst.Add(this.GetPropertyName(() => this.Guid));
            lst.Add(this.GetPropertyName(() => this.NameUi));
            lst.Add(this.GetPropertyName(() => this.Parent));
            lst.Add(this.GetPropertyName(() => this.Children));
            //if (!this.UseCodeProperty)
            //    lst.Add(this.GetPropertyName(() => this.PropertyCodeName));
            //if (!this.UseNameProperty)
            //    lst.Add(this.GetPropertyName(() => this.PropertyNameName));
            return lst.ToArray();
        }
        public bool GetIsGridSortable()
        {
            if (this.IsGridSortable == EnumUseType.Yes)
                return true;
            if (this.IsGridSortable == EnumUseType.No)
                return false;
            return this.ParentModel.IsGridSortable;
        }
        public bool GetIsGridFilterable()
        {
            if (this.IsGridFilterable == EnumUseType.Yes)
                return true;
            if (this.IsGridFilterable == EnumUseType.No)
                return false;
            return this.ParentModel.IsGridFilterable;
        }
        public bool GetIsGridSortableCustom()
        {
            if (this.IsGridSortableCustom == EnumUseType.Yes)
                return true;
            if (this.IsGridSortableCustom == EnumUseType.No)
                return false;
            return this.ParentModel.IsGridSortableCustom;
        }
        public bool GetUseCodeProperty()
        {
            if (this.UseCodeProperty == EnumUseType.Yes)
                return true;
            if (this.UseCodeProperty == EnumUseType.No)
                return false;
            return this.ParentModel.UseCodeProperty;
        }
        public bool GetUseNameProperty()
        {
            if (this.UseNameProperty == EnumUseType.Yes)
                return true;
            if (this.UseNameProperty == EnumUseType.No)
                return false;
            return this.ParentModel.UseNameProperty;
        }
        public bool GetUseDescriptionProperty()
        {
            if (this.UseDescriptionProperty == EnumUseType.Yes)
                return true;
            if (this.UseDescriptionProperty == EnumUseType.No)
                return false;
            return this.ParentModel.UseDescriptionProperty;
        }
        //partial void OnUseCodePropertyChanged()
        //{
        //    this.NotifyPropertyChanged(() => this.PropertyDefinitions);
        //}
        //partial void OnUseNamePropertyChanged()
        //{
        //    this.NotifyPropertyChanged(() => this.PropertyDefinitions);
        //}
        public Catalog AddCatalog()
        {
            var node = new Catalog(this);
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Catalog AddCatalog(string name)
        {
            var node = new Catalog(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }

    }
}
