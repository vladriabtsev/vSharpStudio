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
    public partial class ManyToManyGroupDocumentsRelations : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup, IRoleGlobalSetting //, IRoleAccess
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{ListDocumentsRelations.Count}";
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
            ManyToManyDocumentsRelation node = null!;
            if (node_impl == null)
            {
                node = new ManyToManyDocumentsRelation(this);
            }
            else
            {
                node = (ManyToManyDocumentsRelation)node_impl;
            }
            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.CatalogMtmRelationName, node, this.ListDocumentsRelations);
            }
            var model = this.ParentGroupRelations.ParentModel;
            node.ShortId = model.LastTypeShortIdForNode();
            node.ShortRefId = model.LastTypeShortRefIdForNode(node, node.ShortId);
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        public new ConfigNodesCollection<ManyToManyDocumentsRelation> Children { get { return this.ListDocumentsRelations; } }

        partial void OnCreated()
        {
            this._PrefixForDbTables = "DocManyToMany";
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
            this.ListDocumentsRelations.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            //this.ListDocumentsManyToManyRelations.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //    t.InitRoles();
            //};
            this.ListDocumentsRelations.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListDocumentsRelations.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
            this._Name = Defaults.DocumentMtmRelationsGroupName;
        }
        public int IndexOf(IManyToManyDocumentsRelation doc)
        {
            return this.ListDocumentsRelations.IndexOf((doc as ManyToManyDocumentsRelation)!);
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
        public void Add(ManyToManyDocumentsRelation item) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:51
        {
            Debug.Assert(item != null);
            this.ListDocumentsRelations.Add(item);
            item.Parent = this;
        }
        public ManyToManyDocumentsRelation AddRelation()
        {
            var node = new ManyToManyDocumentsRelation(this);
            this.NodeAddNewSubNode(node);
            return node;
        }
        public ManyToManyDocumentsRelation AddRelation(string name, IDocument doc1, IDocument doc2, bool isUseHistory, string? guid = null)
        {
            var node = new ManyToManyDocumentsRelation(this) { Name = name };
            node.GuidDoc1 = doc1.Guid;
            node.GuidDoc2 = doc2.Guid;
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
