using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using CommunityToolkit.Diagnostics;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class RelationOneToOne : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup,
        ITreeConfigNodeSortable
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" {this.GetName(false)} History:{this.IsUseHistory}";
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
            this._PropertyDataTimeGuid = System.Guid.NewGuid().ToString();
            this._IsRelationReferenceNullable = true;
            var m = (Model)this.Cfg.Model;
            this._PropertyRefObj1 = (Property)m.GetPropertyRef(this, System.Guid.NewGuid().ToString(), "Ref1", 0, true);
            this._PropertyRefObj2 = (Property)m.GetPropertyRef(this, System.Guid.NewGuid().ToString(), "Ref2", 0, true);
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
        }

        #region OnChanged
        partial void OnIsRelationReferenceNullableChanged()
        {
            this.PropertyRefObj1.IsNullable = this.IsRelationReferenceNullable;
            this.PropertyRefObj2.IsNullable = this.IsRelationReferenceNullable;
        }
        partial void OnNameChanged()
        {
            this.OnGuidObj1Changed();
            this.OnGuidObj2Changed();
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
            this.PropertyRefObj1.DataType.ObjectRef.ForeignObjectGuid = this.GuidObj1 ?? "";
            this.PropertyRefObj1.Name = this.Name;
            this.PropertyRefObj1.IsNullable = this.IsRelationReferenceNullable;
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
            this.PropertyRefObj2.DataType.ObjectRef.ForeignObjectGuid = this.GuidObj2 ?? "";
            this.PropertyRefObj2.Name = this.Name;
            this.PropertyRefObj2.IsNullable = this.IsRelationReferenceNullable;
        }
        #endregion OnChanged

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
        //public void OnAdded()
        //{
        //    this.AddAllAppGenSettingsVmsToNode();
        //    this.GroupProperties.AddAllAppGenSettingsVmsToNode();
        //    this.GroupDetails.AddAllAppGenSettingsVmsToNode();
        //    this.GroupForms.AddAllAppGenSettingsVmsToNode();
        //    this.GroupReports.AddAllAppGenSettingsVmsToNode();
        //}

        public RelationOneToOne(ITreeConfigNode parent, string name)
            : this(parent)
        {
            this._Name = name;
        }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentOneToOneGroupRelations.ListRelations.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Catalog?)this.ParentOneToOneGroupRelations.ListRelations.GetPrev(this);
            if (prev == null)
                return;
            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentOneToOneGroupRelations.ListRelations.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentOneToOneGroupRelations.ListRelations.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (RelationOneToOne?)this.ParentOneToOneGroupRelations.ListRelations.GetNext(this);
            if (next == null)
                return;
            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentOneToOneGroupRelations.ListRelations.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = RelationOneToOne.Clone(this.ParentOneToOneGroupRelations, this, true, true);
            node.Parent = this.Parent;
            this.ParentOneToOneGroupRelations.ListRelations.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new RelationOneToOne(this.Parent);
            this.ParentOneToOneGroupRelations.Add(node);
            this.GetUniqueName(Defaults.OneToOneRelationName, node, this.ParentOneToOneGroupRelations.ListRelations);
            var model = this.ParentOneToOneGroupRelations.ParentGroupRelations.ParentModel;
            node.ShortId = model.LastTypeShortIdForNode();
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

        public void GetSpecialProperties(List<IProperty> res, bool isOptimistic)
        {
            var model = this.ParentOneToOneGroupRelations.ParentGroupRelations.ParentModel;
            var prp = model.GetPropertyPkId(this.ParentOneToOneGroupRelations, this.Cfg.Model.PropertyIdGuid); // position 6
            res.Add(prp);
            if (isOptimistic)
            {
                prp = model.GetPropertyVersion(this.ParentOneToOneGroupRelations, this.Cfg.Model.PropertyVersionGuid); // position 7
                res.Add(prp);
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
                prp = model.GetPropertyDateTimeUtc(this.ParentOneToOneGroupRelations, this.PropertyDataTimeGuid, "DataTimeUtc", 3, false);
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
        [Browsable(false)]
        public SortedObservableCollection<ITreeConfigNodeSortable>? ListObjectsNode1
        {
            get
            {
                Debug.Assert(this.Parent != null);
                if (this.RefObj1Type == EnumRelationConfigType.RelConfigTypeCatalogs)
                    return new SortedObservableCollection<ITreeConfigNodeSortable>(this.Parent.Cfg.Model.GroupCatalogs.ListCatalogs);
                else if (this.RefObj1Type == EnumRelationConfigType.RelConfigTypeDocuments)
                    return new SortedObservableCollection<ITreeConfigNodeSortable>(this.Parent.Cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments);
                else throw new NotImplementedException();
            }
        }
        [Browsable(false)]
        public SortedObservableCollection<ITreeConfigNodeSortable>? ListObjectsNode2
        {
            get
            {
                Debug.Assert(this.Parent != null);
                if (this.RefObj2Type == EnumRelationConfigType.RelConfigTypeCatalogs)
                    return new SortedObservableCollection<ITreeConfigNodeSortable>(this.Parent.Cfg.Model.GroupCatalogs.ListCatalogs);
                else if (this.RefObj2Type == EnumRelationConfigType.RelConfigTypeDocuments)
                    return new SortedObservableCollection<ITreeConfigNodeSortable>(this.Parent.Cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments);
                else throw new NotImplementedException();
            }
        }
    }
}
