using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class GroupListCatalogs : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup, IRoleGlobalSetting //, IRoleAccess
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{ListCatalogs.Count}";
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public Model ParentModel { get { Debug.Assert(this.Parent != null); return (Model)this.Parent; } }
        [Browsable(false)]
        public IModel ParentModelI { get { Debug.Assert(this.Parent != null); return (IModel)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentModel.Children;
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
            var model = this.ParentModel;
            node.ShortId = model.LastTypeShortIdForNode();
            node.ShortRefId = model.LastTypeShortRefIdForNode(node, node.ShortId);
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        public new ConfigNodesCollection<Catalog> Children { get { return this.ListCatalogs; } }

        partial void OnCreated()
        {
            this._PrefixForCompositionNames = "Ctlg";
            this.IsEditable = false;
            this._UseCodeProperty = EnumUseType.Default;
            this._UseNameProperty = EnumUseType.Default;
            this._UseDescriptionProperty = EnumUseType.Default;
            this._UseCodePropertyInSeparateTree = true;
            this._UseNamePropertyInSeparateTree = true;

            this._ShortIdTypeForCacheKey = "c";
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
                t.InitRoles();
            };
            this.ListCatalogs.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListCatalogs.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
            this._Name = Defaults.GroupCatalogsName;
        }
        public int IndexOf(ICatalog cat)
        {
            return this.ListCatalogs.IndexOf((Catalog)cat);
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

        #region Roles
        public EnumCatalogDetailAccess GetRoleCatalogAccess(IRole role)
        {
            return role.DefaultCatalogEditAccessSettings;
        }
        public EnumPrintAccess GetRoleCatalogPrint(IRole role)
        {
            return role.DefaultCatalogPrintAccessSettings;
        }
        #endregion Roles
    }
}
