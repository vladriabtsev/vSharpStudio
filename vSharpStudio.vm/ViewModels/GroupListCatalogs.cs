using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class GroupListCatalogs : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Code:{(this.UseCodeProperty ? this.PropertyCodeName : "No")} Desc:{(this.UseDescriptionProperty ? this.PropertyDescriptionName : "No")} SepTreeCode:{this.UseCodePropertyInSeparateTree} SepTreeName:{this.UseNamePropertyInSeparateTree} Cats:{this.ListCatalogs.Count}";
            mes = mes + $" Count:{ListCatalogs.Count}";
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public GroupCatalogs ParentGroupCatalogs { get { Debug.Assert(this.Parent != null); return (GroupCatalogs)this.Parent; } }
        [Browsable(false)]
        public IGroupCatalogs ParentGroupCatalogsI { get { Debug.Assert(this.Parent != null); return (IGroupCatalogs)this.Parent; } }
        [Browsable(false)]
        public Model ParentModel { get { Debug.Assert(this.Parent != null && this.Parent.Parent != null); return (Model)this.Parent.Parent; } }
        [Browsable(false)]
        public IModel ParentModelI { get { Debug.Assert(this.Parent != null && this.Parent.Parent != null); return (IModel)this.Parent.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupCatalogs.Children;
        }
        #endregion ITree

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            Catalog node = null!;
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
            var model = this.ParentGroupCatalogs.ParentModel;
            node.ShortId = ++this.LastShortId;
            node.ShortRefId = model.LastTypeShortRefIdForNode(node, node.ShortId);
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        public new ConfigNodesCollection<Catalog> Children { get { return this.ListCatalogs; } }

        partial void OnCreated()
        {
            this.IsEditable = false;
            this._UseCodeProperty = true;
            this._UseNameProperty = true;
            this._UseDescriptionProperty = false;
            this._UseCodePropertyInSeparateTree = true;
            this._UseNamePropertyInSeparateTree = true;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        partial void OnSortTypeChanged() { this.ListCatalogs.Sort(this.SortType); }
        private void Init()
        {
            OnSortTypeChanged();
            this._Name = Defaults.CatalogsListName;
            if (string.IsNullOrWhiteSpace(this._PrefixForCompositionNames)) this._PrefixForCompositionNames = "Ctlg";
            if (string.IsNullOrWhiteSpace(this._PropertyCodeName)) this._PropertyCodeName = "Code";
            if (string.IsNullOrWhiteSpace(this._PropertyNameName)) this._PropertyNameName = "Name";
            if (string.IsNullOrWhiteSpace(this._PropertyDescriptionName)) this._PropertyDescriptionName = "Description";
            if (string.IsNullOrWhiteSpace(this._PropertyIsFolderName)) this._PropertyIsFolderName = "IsFolder";
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
            //if (!this.UseCodeProperty)
            //    lst.Add(nameof(this.PropertyCodeName));
            //if (!this.UseNameProperty)
            //    lst.Add(nameof(this.PropertyNameName));
            return [.. lst];
        }
        public int IndexOf(ICatalog cat)
        {
            return this.ListCatalogs.IndexOf((Catalog)cat);
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
        public Catalog AddCatalog(string name, string? guid = null, string? guidFolder = null)
        {
            var node = new Catalog(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
            if (guidFolder != null)
                node.Folder.Guid = guidFolder;
#endif
            this.NodeAddNewSubNode(node);
            return node;
        }

        #region View
        public bool IsGridSortableGet()
        {
            if (this.IsGridSortable == EnumUseType.Yes)
                return true;
            if (this.IsGridSortable == EnumUseType.No)
                return false;
            return this.ParentModel.IsGridSortable;
        }
        public bool IsGridFilterableGet()
        {
            if (this.IsGridFilterable == EnumUseType.Yes)
                return true;
            if (this.IsGridFilterable == EnumUseType.No)
                return false;
            return this.ParentModel.IsGridFilterable;
        }
        public bool IsGridSortableCustomGet()
        {
            if (this.IsGridSortableCustom == EnumUseType.Yes)
                return true;
            if (this.IsGridSortableCustom == EnumUseType.No)
                return false;
            return this.ParentModel.IsGridSortableCustom;
        }
        #endregion View
    }
}
