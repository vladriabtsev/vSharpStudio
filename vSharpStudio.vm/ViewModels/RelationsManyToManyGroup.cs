﻿using System;
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
    public partial class RelationsManyToManyGroup : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup, IRoleGlobalSetting //, IRoleAccess
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{ListCatalogsRelations.Count}";
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public RelationsGroup ParentGroupRelations { get { Debug.Assert(this.Parent != null); return (RelationsGroup)this.Parent; } }
        [Browsable(false)]
        public IRelationsGroup ParentGroupRelationsI { get { Debug.Assert(this.Parent != null); return (IRelationsGroup)this.Parent; } }

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
            RelationManyToMany node = null!;
            if (node_impl == null)
            {
                node = new RelationManyToMany(this);
            }
            else
            {
                node = (RelationManyToMany)node_impl;
            }
            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.ManyToManyRelationName, node, this.ListCatalogsRelations);
            }
            var model = this.ParentGroupRelations.ParentModel;
            node.ShortId = model.LastTypeShortIdForNode();
            node.ShortRefId = model.LastTypeShortRefIdForNode(node, node.ShortId);
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        public new ConfigNodesCollection<RelationManyToMany> Children { get { return this.ListCatalogsRelations; } }

        partial void OnCreated()
        {
            this._PrefixForCompositionNames = "CtlgManyToMany";
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
            this._Name = Defaults.ManyToManyRelationsGroupName;
        }
        public int IndexOf(IRelationManyToMany cat)
        {
            return this.ListCatalogsRelations.IndexOf((cat as RelationManyToMany)!);
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
        public void Add(RelationManyToMany item) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:51
        {
            Debug.Assert(item != null);
            this.ListCatalogsRelations.Add(item);
            item.Parent = this;
        }
        public RelationManyToMany AddRelation()
        {
            var node = new RelationManyToMany(this);
            this.NodeAddNewSubNode(node);
            return node;
        }
        public RelationManyToMany AddRelation(string name, ICatalog cat1, ICatalog cat2, bool isUseHistory, string? guid = null)
        {
            var node = new RelationManyToMany(this) { Name = name };
            node.RefObj1Type = EnumRelationConfigType.RelConfigTypeCatalogs;
            node.GuidObj1 = cat1.Guid;
            node.RefObj2Type = EnumRelationConfigType.RelConfigTypeCatalogs;
            node.GuidObj2 = cat2.Guid;
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
        public RelationManyToMany AddRelation(string name, IDocument doc1, ICatalog cat2, bool isUseHistory, string? guid = null)
        {
            var node = new RelationManyToMany(this) { Name = name };
            node.RefObj1Type = EnumRelationConfigType.RelConfigTypeDocuments;
            node.GuidObj1 = doc1.Guid;
            node.RefObj2Type = EnumRelationConfigType.RelConfigTypeCatalogs;
            node.GuidObj2 = cat2.Guid;
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
        public RelationManyToMany AddRelation(string name, IDocument doc1, IDocument doc2, bool isUseHistory, string? guid = null)
        {
            var node = new RelationManyToMany(this) { Name = name };
            node.RefObj1Type = EnumRelationConfigType.RelConfigTypeDocuments;
            node.GuidObj1 = doc1.Guid;
            node.RefObj2Type = EnumRelationConfigType.RelConfigTypeDocuments;
            node.GuidObj2 = doc2.Guid;
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