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
    public partial class ManyToManyGroupCatalogsRelations : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup, IRoleGlobalSetting //, IRoleAccess
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{ListCatalogsRelations.Count}";
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public ManyToManyGroupRelations ParentGroupRelations { get { Debug.Assert(this.Parent != null); return (ManyToManyGroupRelations)this.Parent; } }
        [Browsable(false)]
        public IGroupRelations ParentGroupRelationsI { get { Debug.Assert(this.Parent != null); return (IGroupRelations)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupRelations.Children;
        }
        #endregion ITree

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            ManyToManyCatalogsRelation node = null!;
            if (node_impl == null)
            {
                node = new ManyToManyCatalogsRelation(this);
            }
            else
            {
                node = (ManyToManyCatalogsRelation)node_impl;
            }
            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.CatalogMtmRelationName, node, this.ListCatalogsRelations);
            }
            var model = this.ParentGroupRelations.ParentModel;
            node.ShortId = model.LastTypeShortRefIdForNode(node);
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        public new ConfigNodesCollection<ManyToManyCatalogsRelation> Children { get { return this.ListCatalogsRelations; } }

        partial void OnCreated()
        {
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
            this.ListCatalogsRelations.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            //this.ListCatalogsManyToManyRelations.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //    t.InitRoles();
            //};
            this.ListCatalogsRelations.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListCatalogsRelations.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
            this._Name = Defaults.CatalogMtmRelationsGroupName;
        }
        public int IndexOf(IManyToManyCatalogsRelation cat)
        {
            return this.ListCatalogsRelations.IndexOf((cat as ManyToManyCatalogsRelation)!);
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
        public void Add(ManyToManyCatalogsRelation item) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:51
        {
            Debug.Assert(item != null);
            this.ListCatalogsRelations.Add(item);
            item.Parent = this;
        }
        public ManyToManyCatalogsRelation AddRelation()
        {
            var node = new ManyToManyCatalogsRelation(this);
            this.NodeAddNewSubNode(node);
            return node;
        }
        public ManyToManyCatalogsRelation AddRelation(string name, ICatalog cat1, ICatalog cat2, bool isUseHistory, string? guid = null)
        {
            var node = new ManyToManyCatalogsRelation(this) { Name = name };
            node.GuidCat1 = cat1.Guid;
            node.GuidCat2 = cat2.Guid;
            node.IsUseHistory = isUseHistory;
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
