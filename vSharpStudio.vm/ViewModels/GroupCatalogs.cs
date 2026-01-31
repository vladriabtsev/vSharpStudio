using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Google.Protobuf.WellKnownTypes;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class GroupCatalogs : ITreeModel, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNodeGroup, IRoleGlobalSetting //, IRoleAccess
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Code:{(this.UseCodeProperty ? this.PropertyCodeName : "No")} Desc:{(this.UseDescriptionProperty ? this.PropertyDescriptionName : "No")} SepTreeCode:{this.UseCodePropertyInSeparateTree} SepTreeName:{this.UseNamePropertyInSeparateTree} 1to1:{this.GroupRelations.GroupListOneToOneRelations.ListRelations.Count} MtoM:{this.GroupRelations.GroupListOneToOneRelations.ListRelations.Count} Cats:{this.GroupListCatalogs.ListCatalogs.Count}";
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

        [Browsable(false)]
        public new string IconName { get { return "iconFolder"; } }
        //protected override string GetNodeIconName() { return "iconFolder"; }
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
        private void Init()
        {
            this._Name = Defaults.CatalogsGroupName;
            if (string.IsNullOrWhiteSpace(this._PrefixForCompositionNames)) this._PrefixForCompositionNames = "Ctlg";
            if (string.IsNullOrWhiteSpace(this._PropertyCodeName)) this._PropertyCodeName = "Code";
            if (string.IsNullOrWhiteSpace(this._PropertyNameName)) this._PropertyNameName = "Name";
            if (string.IsNullOrWhiteSpace(this._PropertyDescriptionName)) this._PropertyDescriptionName = "Description";
            if (string.IsNullOrWhiteSpace(this._PropertyIsFolderName)) this._PropertyIsFolderName = "IsFolder";
            if (this.Children.Count > 0)
                return;
            var children = (ConfigNodesCollection<ITreeConfigNodeSortable>)this.Children;
            children.Add(this.GroupRelations, 2);
            children.Add(this.GroupListCatalogs, 3);

            //this.ListRoles.OnAddingAction = (t) =>
            //{
            //    t.IsNew = true;
            //};
            //this.ListRoles.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            //this.ListRoles.OnRemovedAction = (t) =>
            //{
            //    this.OnRemoveChild();
            //};
            //this.ListRoles.OnClearedAction = () =>
            //{
            //    this.OnRemoveChild();
            //};
        }
        public Catalog AddCatalog()
        {
            var node = new Catalog(this);
            this.GroupListCatalogs.NodeAddNewSubNode(node);
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
            this.GroupListCatalogs.NodeAddNewSubNode(node);
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

        #region Roles
        public EnumCatalogDetailAccess GetRoleCatalogAccess(IRole role)
        {
            return role.DefaultCatalogEditAccessSettings;
        }
        public EnumPrintAccess GetRoleCatalogPrint(IRole role)
        {
            return role.DefaultCatalogPrintAccessSettings;
        }
        public EnumPropertyAccess GetRolePropertyAccess(IRole role)
        {
            var pa = role.DefaultCatalogEditAccessSettings;
            switch (pa)
            {
                case EnumCatalogDetailAccess.C_HIDE:
                    return EnumPropertyAccess.P_HIDE;
                case EnumCatalogDetailAccess.C_VIEW:
                    return EnumPropertyAccess.P_VIEW;
                case EnumCatalogDetailAccess.C_EDIT_ITEMS:
                case EnumCatalogDetailAccess.C_MARK_DEL:
                case EnumCatalogDetailAccess.C_EDIT_FOLDERS:
                    return EnumPropertyAccess.P_EDIT;
                default:
                    throw new NotImplementedException();
            }
        }
        public EnumPrintAccess GetRolePropertyPrint(IRole role)
        {
            var pa = role.DefaultCatalogPrintAccessSettings;
            if (pa == EnumPrintAccess.PR_BY_PARENT)
                return EnumPrintAccess.PR_PRINT;
            return pa;
        }
        #endregion Roles
    }
}
