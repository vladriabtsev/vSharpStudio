using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using CommunityToolkit.Diagnostics;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugInfo(),nq}")]
    public partial class RelationOneToOne : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup,
        ITreeConfigNodeSortable
    {
        public override string NameShortId { get { return $"o{this.ShortId}"; } }
        partial void OnDebugStringExtend(StringBuilder sb)
        {
            sb.Append(this.GetName(false));
            sb.Append(" History:");
            sb.Append(this.IsUseHistory);
        }
        public string GetDebuggerDisplay(bool isOptimistic)
        {
            var sb = new StringBuilder();
            sb.Append("OneToOne ");
            sb.Append(this.Name);
            sb.Append(", ");
            return sb.ToString();
        }

        [Browsable(false)]
        public RelationsOneToOneGroup ParentOneToOneGroupRelations { get { Debug.Assert(this.Parent != null); return (RelationsOneToOneGroup)this.Parent; } }
        [Browsable(false)]
        public IRelationsOneToOneGroup ParentOneToOneGroupRelationsI { get { Debug.Assert(this.Parent != null); return (IRelationsOneToOneGroup)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentOneToOneGroupRelations.Children;
        }
        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconCatalogProperty"; } }
        //protected override string GetNodeIconName() { return "iconCatalogProperty"; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            this._Guid = System.Guid.NewGuid().ToString();
            this._IsRelationReferenceNullable = true;
            var model = this.Cfg.Model;
            this._PropertyRefObj1 = (Property)model.GetPropertyRef(this, System.Guid.NewGuid().ToString(), "Ref1", 0, true);
            this._PropertyRefObj1.DataTypeEnum = EnumDataType.CATALOG;
            this._PropertyRefObj2 = (Property)model.GetPropertyRef(this, System.Guid.NewGuid().ToString(), "Ref2", 0, true);
            this._PropertyRefObj2.DataTypeEnum = EnumDataType.CATALOG;
            Init();
        }
        protected override void OnInitFromDto()
        {
            //base.OnInitFromDto();
            Init();
        }
        private void Init()
        {
            this.RefillChildren();
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
            //this.OnPropertyChanged(nameof(this.ListObjectsNode1));
            //this.OnPropertyChanged(nameof(this.ListObjectsNode2));
        }
        protected override ConfigNodesCollection<RelationOneToOne>? GetParentCollection() { return this.ParentOneToOneGroupRelations.ListRelations; }
        private string GetName(bool isComposite)
        {
            Debug.Assert(this.Parent != null);
            var cfg = this.ParentOneToOneGroupRelations.ParentGroupRelations.ParentModel.Cfg;
            string name1 = "<empty>";
            if (this.GuidObj1 != null)
            {
                if (isComposite)
                    name1 = ((ICompositeName)cfg.DicNodes[this.GuidObj1]).CompositeName;
                else
                    name1 = cfg.DicNodes[this.GuidObj1].Name;
            }
            string name2 = "<empty>";
            if (this.GuidObj2 != null)
            {
                if (isComposite)
                    name2 = ((ICompositeName)cfg.DicNodes[this.GuidObj2]).CompositeName;
                else
                    name2 = cfg.DicNodes[this.GuidObj2].Name;
            }
            if (name1.CompareTo(name2) < 1)
                return $"{name1}<->{name2}";
            else
                return $"{name2}<->{name1}";
        }
        public void RefillChildren()
        {
            //if (this.Children.Count > 0)
            //    return;

            //var children = (ConfigNodesCollection<ITreeConfigNodeSortable>)this.Children;
            //children.Clear();
            //if (this.UseTree && this.UseSeparateTreeForFolders)
            //{
            //    children.Add(this.Folder, 1);
            //}
            //children.Add(this.GroupProperties, 2);
            //children.Add(this.GroupDetails, 3);
            //children.Add(this.GroupForms, 4);
            //children.Add(this.GroupReports, 5);
            //this.CodePropertySettings.Parent = this;
        }
        public void OnAdded()
        {
            this.AddOrRestoreAllAppGenSettingsVmsToNode();
        }

        public RelationOneToOne(ITreeConfigNode parent, string name)
            : this(parent)
        {
            this._Name = name;
        }

        #region Tree operations
        public override ITreeConfigNode NodeAddClone()
        {
            var node = RelationOneToOne.Clone(this.ParentOneToOneGroupRelations, this, true, true);
            node.Parent = this.Parent;
            this.ParentOneToOneGroupRelations.ListRelations.Add(node, this);
            this._Name = this._Name + "2";
            var model = this.Cfg.Model;
            node.ShortId = ++this.ParentOneToOneGroupRelations.LastShortId;
            node.ShortRefId = model.LastTypeShortRefIdForNode(node, node.ShortId);
            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new RelationOneToOne(this.Parent);
            this.ParentOneToOneGroupRelations.ListRelations.Add(node, this);
            this.GetUniqueName(Defaults.RelationOneToOneName, node, this.ParentOneToOneGroupRelations.ListRelations);
            var model = this.ParentOneToOneGroupRelations.ParentGroupRelations.ParentModel;
            node.ShortId = ++this.ParentOneToOneGroupRelations.LastShortId;
            node.ShortRefId = model.LastTypeShortRefIdForNode(node, node.ShortId);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentOneToOneGroupRelations.ListRelations.Remove(this);
        }
        #endregion Tree operations

        [ExpandableObjectAttribute()]
        public dynamic? Setting { get; set; }

        #region Get Properties and Details
        public uint GetNextFreePosition() { return ++this.LastPosition; }
        public void GetSpecialProperties(List<IProperty> res, bool isOptimistic)
        {
            var model = this.Cfg.Model;
            var prp = model.GetPropertySpecial(this, EnumSpecialPropertyType.RECORD_ID);
            res.Add(prp);
            if (isOptimistic)
            {
                prp = model.GetPropertyVersion(this);
                res.Add(prp);
                //prp = model.GetPropertyVersionPrev(this);
                //res.Add(prp);
            }
            if (this.GuidObj1 != null)
            {
                if (model.IsUseNameComposition)
                    prp = model.GetPropertyRef(this.ParentOneToOneGroupRelations, this.GuidObj1, "Ref" + ((ICompositeName)this.Cfg.DicNodes[this.GuidObj1]).CompositeName, 1, false);
                else
                    prp = model.GetPropertyRef(this.ParentOneToOneGroupRelations, this.GuidObj1, "Ref" + this.Cfg.DicNodes[this.GuidObj1].Name, 1, false);
                res.Add(prp);
            }
            if (this.GuidObj2 != null)
            {
                if (model.IsUseNameComposition)
                    prp = model.GetPropertyRef(this.ParentOneToOneGroupRelations, this.GuidObj2, "Ref" + ((ICompositeName)this.Cfg.DicNodes[this.GuidObj2]).CompositeName, 2, false);
                else
                    prp = model.GetPropertyRef(this.ParentOneToOneGroupRelations, this.GuidObj2, "Ref" + this.Cfg.DicNodes[this.GuidObj2].Name, 2, false);
                res.Add(prp);
            }
            if (this.IsUseHistory)
            {
                prp = model.GetPropertySpecial(this, EnumSpecialPropertyType.HISTORY_DATATIMEUTC);
                //prp = model.GetPropertyDateTimeUtc(this.ParentOneToOneGroupRelations, this.PropertyDataTimeGuid, "DataTimeUtc", 3, false);
                res.Add(prp);
            }
        }
        //public void GetNormalProperties(List<IProperty> res)
        //{
        //    this.GetCodeProperty(res);
        //    this.GetNameProperty(res);
        //    this.GetDescriptionProperty(res);
        //    foreach (var t in this.GroupProperties.ListProperties)
        //    {
        //        res.Add(t);
        //    }
        //}
        public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial = false)
        {
            var res = new List<IProperty>();
            return res;
        }
        public IReadOnlyList<IForm> GetListForms(string guidAppPrjGen)
        {
            throw new NotImplementedException();
        }
        public IForm GetForm(FormType ftype, string guidAppPrjGen)
        {
            throw new NotImplementedException();
        }
        #endregion Get Properties and Details

        #region EDIT LOGIC
        partial void OnNameChanged()
        {
            this.OnGuidObj1Changed();
            this.OnGuidObj2Changed();
        }
        partial void OnIsRelationReferenceNullableChanged()
        {
            this.PropertyRefObj1.IsNullable = this.IsRelationReferenceNullable;
            this.PropertyRefObj2.IsNullable = this.IsRelationReferenceNullable;
        }
        partial void OnRefObj1TypeChanged()
        {
            this.GuidObj1 = null;
            this.OnPropertyChanged(nameof(this.ListObjectsNode1));
        }
        partial void OnGuidObj1Changed()
        {
            if (this.RefObj1Type == EnumRelationConfigType.RelConfigTypeCatalogs)
            {
                this.PropertyRefObj1.DataTypeEnum = EnumDataType.CATALOG;
            }
            else if (this.RefObj1Type == EnumRelationConfigType.RelConfigTypeDocuments)
            {
                this.PropertyRefObj1.DataTypeEnum = EnumDataType.DOCUMENT;
            }
            else
                ThrowHelper.ThrowInvalidOperationException();
            this.PropertyRefObj1.DataType.ObjectRef0.ForeignObjectGuid = this.GuidObj1 ?? "";
            this.PropertyRefObj1.Name = this.Name;
            this.PropertyRefObj1.IsNullable = this.IsRelationReferenceNullable;
            this.PropertyRefObj1.Position = 0;
            //this.PropertyRefObj1.PositionOfDescr = 0;
            //this.PropertyRefObj1.PositionOfGd = 0;
        }
        partial void OnRefObj2TypeChanged()
        {
            this.GuidObj2 = null;
            this.OnPropertyChanged(nameof(this.ListObjectsNode2));
        }
        partial void OnGuidObj2Changed()
        {
            if (this.RefObj2Type == EnumRelationConfigType.RelConfigTypeCatalogs)
            {
                this.PropertyRefObj2.DataTypeEnum = EnumDataType.CATALOG;
            }
            else if (this.RefObj2Type == EnumRelationConfigType.RelConfigTypeDocuments)
            {
                this.PropertyRefObj2.DataTypeEnum = EnumDataType.DOCUMENT;
            }
            else
                ThrowHelper.ThrowInvalidOperationException();
            this.PropertyRefObj2.DataType.ObjectRef0.ForeignObjectGuid = this.GuidObj2 ?? "";
            this.PropertyRefObj2.Name = this.Name;
            this.PropertyRefObj2.IsNullable = this.IsRelationReferenceNullable;
            this.PropertyRefObj2.Position = 0;
            //this.PropertyRefObj2.PositionOfDescr = 0;
            //this.PropertyRefObj2.PositionOfGd = 0;
        }
        [Browsable(false)]
        public SortedObservableCollection<ITreeConfigNodeSortable>? ListObjectsNode1
        {
            get
            {
                if (this.RefObj1Type == EnumRelationConfigType.RelConfigTypeCatalogs)
                    return new SortedObservableCollection<ITreeConfigNodeSortable>(this.Cfg.Model.GroupCatalogs.GroupListCatalogs.ListCatalogs);
                else if (this.RefObj1Type == EnumRelationConfigType.RelConfigTypeDocuments)
                    return new SortedObservableCollection<ITreeConfigNodeSortable>(this.Cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments);
                else throw new NotImplementedException();
            }
        }
        [Browsable(false)]
        public SortedObservableCollection<ITreeConfigNodeSortable>? ListObjectsNode2
        {
            get
            {
                if (this.RefObj2Type == EnumRelationConfigType.RelConfigTypeCatalogs)
                    return new SortedObservableCollection<ITreeConfigNodeSortable>(this.Cfg.Model.GroupCatalogs.GroupListCatalogs.ListCatalogs);
                else if (this.RefObj2Type == EnumRelationConfigType.RelConfigTypeDocuments)
                    return new SortedObservableCollection<ITreeConfigNodeSortable>(this.Cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments);
                else throw new NotImplementedException();
            }
        }
        #endregion EDIT LOGIC
    }
}
