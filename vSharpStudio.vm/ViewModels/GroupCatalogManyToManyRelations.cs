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
    public partial class GroupCatalogManyToManyRelations : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup, IRoleGlobalSetting //, IRoleAccess
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{ListCatalogsManyToManyRelations.Count}";
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public GroupListCatalogs ParentGroupListCatalogs { get { Debug.Assert(this.Parent != null); return (GroupListCatalogs)this.Parent; } }
        [Browsable(false)]
        public IGroupListCatalogs ParentGroupListCatalogsI { get { Debug.Assert(this.Parent != null); return (IGroupListCatalogs)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListCatalogs.Children;
        }
        #endregion ITree

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            CatalogsManyToManyRelation node = null!;
            if (node_impl == null)
            {
                node = new CatalogsManyToManyRelation(this);
            }
            else
            {
                node = (CatalogsManyToManyRelation)node_impl;
            }
            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.CatalogMtmRelationName, node, this.ListCatalogsManyToManyRelations);
            }
            var model = this.ParentGroupListCatalogs.ParentModel;
            node.ShortId = model.LastCatalogRelationShortId + 1;
            model.LastCatalogRelationShortId = node.ShortId;
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        public new ConfigNodesCollection<CatalogsManyToManyRelation> Children { get { return this.ListCatalogsManyToManyRelations; } }

        partial void OnCreated()
        {
            this._Name = Defaults.CatalogMtmRelationsGroupName;
            this._PrefixForDbTables = "CtlgManyToMany";
            this.IsEditable = false;

            this._ShortIdTypeForCacheKey = "m";
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
            this.ListCatalogsManyToManyRelations.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            //this.ListCatalogsManyToManyRelations.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //    t.InitRoles();
            //};
            this.ListCatalogsManyToManyRelations.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListCatalogsManyToManyRelations.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
        }
        public int IndexOf(ICatalogsManyToManyRelation cat)
        {
            return this.ListCatalogsManyToManyRelations.IndexOf((cat as CatalogsManyToManyRelation)!);
        }
        //protected override string[]? OnGetWhatHideOnPropertyGrid()
        //{
        //    var lst = new List<string>
        //    {
        //        nameof(this.Description),
        //        nameof(this.Guid),
        //        nameof(this.NameUi),
        //        nameof(this.Parent),
        //        nameof(this.Children)
        //    };
        //    //if (!this.UseCodeProperty)
        //    //    lst.Add(nameof(this.PropertyCodeName));
        //    //if (!this.UseNameProperty)
        //    //    lst.Add(nameof(this.PropertyNameName));
        //    return lst.ToArray();
        //}
        public void Add(CatalogsManyToManyRelation item) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:51
        {
            Debug.Assert(item != null);
            this.ListCatalogsManyToManyRelations.Add(item);
            item.Parent = this;
        }
        public CatalogsManyToManyRelation AddRelation()
        {
            var node = new CatalogsManyToManyRelation(this);
            this.NodeAddNewSubNode(node);
            return node;
        }
        public CatalogsManyToManyRelation AddRelation(string name, ICatalog? cat1, ICatalog? cat2, bool isUseHistory, string? guid = null)
        {
            var node = new CatalogsManyToManyRelation(this) { Name = name };
            node.GuidCat1 = cat1.Guid;
            node.GuidCat2 = cat2.Guid;
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            this.NodeAddNewSubNode(node);
            return node;
        }

        //#region Roles
        //public EnumCatalogDetailAccess GetRoleCatalogAccess(IRole role)
        //{
        //    return role.DefaultCatalogEditAccessSettings;
        //}
        //public EnumPrintAccess GetRoleCatalogPrint(IRole role)
        //{
        //    return role.DefaultCatalogPrintAccessSettings;
        //}
        //#endregion Roles
    }
}
