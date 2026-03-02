using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Google.Protobuf.WellKnownTypes;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugInfo(),nq}")]
    public partial class GroupListDocuments : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup
    {
        partial void OnDebugStringExtend(StringBuilder sb)
        {
            sb.Append(" Count:");
            sb.Append(this.ListDocuments.Count);
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public GroupDocuments ParentGroupDocuments { get { Debug.Assert(this.Parent != null); return (GroupDocuments)this.Parent; } }
        [Browsable(false)]
        public IGroupDocuments ParentGroupDocumentsI { get { Debug.Assert(this.Parent != null); return (IGroupDocuments)this.Parent; } }
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
            return this.ParentGroupDocuments.Children;
        }
        #endregion ITree

        public new ConfigNodesCollection<Document> Children { get { return this.ListDocuments; } }
        public IGroupDocuments IParent { get { return this.ParentGroupDocuments; } }

        partial void OnCreated()
        {
            this._MondayBeforeFirstDocDate = Timestamp.FromDateTime(new DateTime(1000, 1, 6, 0, 0, 0, DateTimeKind.Utc));
            this._UseDocNumberProperty = true;
            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        partial void OnSortTypeChanged() { this.ListDocuments.Sort(this.SortType); }
        private void Init()
        {
            OnSortTypeChanged();
            this._Name = Defaults.DocumentsListName;
            if (string.IsNullOrWhiteSpace(this._PrefixForCompositionNames)) this._PrefixForCompositionNames = "Doc";
            if (string.IsNullOrWhiteSpace(this._PropertyDocNumberName)) this._PropertyDocNumberName = "DocNumber";
            if (string.IsNullOrWhiteSpace(this._PropertyIsPostedName)) this._PropertyIsPostedName = "IsPosted";
            if (string.IsNullOrWhiteSpace(this._PropertyDocShortTypeIdName)) this._PropertyDocShortTypeIdName = "DocShortTypeId";
            this.ListDocuments.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListDocuments.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListDocuments.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListDocuments.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
        }
        public Document AddDocument(string name)
        {
            var node = new Document(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public int IndexOf(IDocument doc)
        {
            return this.ListDocuments.IndexOf((doc as Document)!);
        }
        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            Document node = null!;
            if (node_impl == null)
            {
                node = new Document(this);
            }
            else
            {
                node = (Document)node_impl;
            }
            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.DocumentName, node, this.ListDocuments);
            }
            var cfg = (Config)this.Cfg;
            node.ShortId = ++this.LastShortId;
            node.ShortRefId = cfg.Model.LastTypeShortRefIdForNode(node, node.ShortId);
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

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
    }
}
