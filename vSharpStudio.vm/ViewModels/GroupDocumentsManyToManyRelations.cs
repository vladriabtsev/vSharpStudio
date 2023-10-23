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
    public partial class GroupDocumentsManyToManyRelations : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup, IRoleGlobalSetting //, IRoleAccess
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{ListDocumentsManyToManyRelations.Count}";
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public GroupRelations ParentGroupRelations { get { Debug.Assert(this.Parent != null); return (GroupRelations)this.Parent; } }
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
            DocumentsManyToManyRelation node = null!;
            if (node_impl == null)
            {
                node = new DocumentsManyToManyRelation(this);
            }
            else
            {
                node = (DocumentsManyToManyRelation)node_impl;
            }
            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.CatalogMtmRelationName, node, this.ListDocumentsManyToManyRelations);
            }
            var model = this.ParentGroupRelations.ParentModel;
            node.ShortId = model.LastCatalogRelationShortId + 1;
            model.LastCatalogRelationShortId = node.ShortId;
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        public new ConfigNodesCollection<DocumentsManyToManyRelation> Children { get { return this.ListDocumentsManyToManyRelations; } }

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
            this.ListDocumentsManyToManyRelations.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            //this.ListDocumentsManyToManyRelations.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //    t.InitRoles();
            //};
            this.ListDocumentsManyToManyRelations.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListDocumentsManyToManyRelations.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
            this._Name = Defaults.DocumentMtmRelationsGroupName;
        }
        public int IndexOf(IDocumentsManyToManyRelation doc)
        {
            return this.ListDocumentsManyToManyRelations.IndexOf((doc as DocumentsManyToManyRelation)!);
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
        public void Add(DocumentsManyToManyRelation item) // D:\dev\vSharpStudio.pro\submodules\vSharpStudio\generators\GenFromProto\Property.tt Line:51
        {
            Debug.Assert(item != null);
            this.ListDocumentsManyToManyRelations.Add(item);
            item.Parent = this;
        }
        public DocumentsManyToManyRelation AddRelation()
        {
            var node = new DocumentsManyToManyRelation(this);
            this.NodeAddNewSubNode(node);
            return node;
        }
        public DocumentsManyToManyRelation AddRelation(string name, IDocument doc1, IDocument doc2, bool isUseHistory, string? guid = null)
        {
            var node = new DocumentsManyToManyRelation(this) { Name = name };
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
