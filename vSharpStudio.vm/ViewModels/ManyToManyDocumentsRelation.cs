using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class ManyToManyDocumentsRelation : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup,
        ITreeConfigNodeSortable
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            string cat1 = "<empty>", cat2 = "<empty>";
            if (this.GuidDoc1 != null)
            {
                Debug.Assert(this.Cfg.DicNodes.ContainsKey(this.GuidDoc1));
                cat1 = this.Cfg.DicNodes[this.GuidDoc1].Name;
            }
            if (this.GuidDoc2 != null)
            {
                Debug.Assert(this.Cfg.DicNodes.ContainsKey(this.GuidDoc2));
                cat2 = this.Cfg.DicNodes[this.GuidDoc2].Name;
            }
            mes = mes + $" {cat1}<->{cat2} History:{this.IsUseHistory}";
        }
        public string GetDebuggerDisplay(bool isOptimistic)
        {
            var sb = new StringBuilder();
            //sb.Append("CAT ");
            //sb.Append(this.Name);
            //sb.Append(", ");
            //sb.Append(this.ParentGroupListCatalogs.ParentModel.PKeyName);
            //sb.Append(":{");
            //sb.Append(this.ParentGroupListCatalogs.ParentModel.PKeyName);
            //sb.Append(",nq}");
            //if (this.UseTree)
            //{
            //    if (this.UseSeparateTreeForFolders)
            //    {
            //        sb.Append(" Ref");
            //        sb.Append(this.Folder.CompositeName);
            //        sb.Append(":{Ref");
            //        sb.Append(this.Folder.CompositeName);
            //        sb.Append(",nq}");
            //    }
            //    else
            //    {
            //        sb.Append(" RefTreeParent:{RefTreeParent,nq}");
            //        //prp = model.GetPropertyIsFolder(this.GroupProperties, this.PropertyIsFolderGuid);
            //        //res.Add(prp);
            //    }
            //}
            //if (isOptimistic)
            //{
            //    sb.Append(" RecVer:{");
            //    sb.Append(this.ParentGroupListCatalogs.ParentModel.RecordVersionFieldName);
            //    sb.Append(",nq}");
            //}
            return sb.ToString();
        }

        [Browsable(false)]
        public ManyToManyGroupDocumentsRelations ParentManyToManyGroupCatalogRelations { get { Debug.Assert(this.Parent != null); return (ManyToManyGroupDocumentsRelations)this.Parent; } }
        [Browsable(false)]
        public IManyToManyGroupDocumentsRelations ParentManyToManyGroupCatalogRelationsI { get { Debug.Assert(this.Parent != null); return (IManyToManyGroupDocumentsRelations)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentManyToManyGroupCatalogRelations.Children;
        }
        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconCatalogProperty"; } }
        //protected override string GetNodeIconName() { return "iconCatalogProperty"; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            this._Guid = System.Guid.NewGuid().ToString();
            this._PropertyIdGuid = System.Guid.NewGuid().ToString();
            this._PropertyDataTimeGuid = System.Guid.NewGuid().ToString();
            this._PropertyVersionGuid = System.Guid.NewGuid().ToString();
            this._RefDoc1Guid = System.Guid.NewGuid().ToString();
            this._RefDoc2Guid = System.Guid.NewGuid().ToString();
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
        partial void OnRefDoc1GuidChanged()
        {
            if (this._RefDoc2Guid != null)
            {
                this._Name = this.GetName();
            }
        }
        private string GetName()
        {
            Debug.Assert(this.Parent != null);
            var cfg = this.ParentManyToManyGroupCatalogRelations.ParentGroupRelations.ParentModel.Cfg;
            Debug.Assert(cfg.DicNodes.ContainsKey(this._RefDoc1Guid));
            string name1 = ((Document)cfg.DicNodes[this._RefDoc1Guid]).Name;
            Debug.Assert(cfg.DicNodes.ContainsKey(this._RefDoc2Guid));
            string name2 = ((Document)cfg.DicNodes[this._RefDoc2Guid]).Name;
            Debug.Assert(name1.CompareTo(name2) != 0);
            if (name1.CompareTo(name2) < 0)
                return $"Many_to_many_{name1}_{name2}";
            else
                return $"Many_to_many_{name2}_{name1}";
        }
        partial void OnRefDoc2GuidChanged()
        {
            if (this._RefDoc1Guid != null)
            {
                this._Name = this.GetName();
            }
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

        public ManyToManyDocumentsRelation(ITreeConfigNode parent, string name)
            : this(parent)
        {
            this._Name = name;
        }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentManyToManyGroupCatalogRelations.ListDocumentsRelations.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Catalog?)this.ParentManyToManyGroupCatalogRelations.ListDocumentsRelations.GetPrev(this);
            if (prev == null)
                return;
            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentManyToManyGroupCatalogRelations.ListDocumentsRelations.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentManyToManyGroupCatalogRelations.ListDocumentsRelations.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (ManyToManyDocumentsRelation?)this.ParentManyToManyGroupCatalogRelations.ListDocumentsRelations.GetNext(this);
            if (next == null)
                return;
            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentManyToManyGroupCatalogRelations.ListDocumentsRelations.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = ManyToManyDocumentsRelation.Clone(this.ParentManyToManyGroupCatalogRelations, this, true, true);
            node.Parent = this.Parent;
            this.ParentManyToManyGroupCatalogRelations.ListDocumentsRelations.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new ManyToManyDocumentsRelation(this.Parent);
            this.ParentManyToManyGroupCatalogRelations.Add(node);
            this.GetUniqueName(Defaults.DocumentMtmRelationName, node, this.ParentManyToManyGroupCatalogRelations.ListDocumentsRelations);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentManyToManyGroupCatalogRelations.ListDocumentsRelations.Remove(this);
        }
        #endregion Tree operations

        [ExpandableObjectAttribute()]
        public dynamic? Setting { get; set; }

        [PropertyOrder(100)]
        [ReadOnly(true)]
        [DisplayName("Composite")]
        [Description("Composite name based on IsCompositeNames and IsUseGroupPrefix model parameters")]
        public string CompositeName
        {
            get
            {
                return GetCompositeName();
            }
        }

        #region Get Properties and Details

        #region OnChanged
        #endregion OnChanged

        public void GetSpecialProperties(List<IProperty> res, bool isOptimistic)
        {
            var model = this.ParentManyToManyGroupCatalogRelations.ParentGroupRelations.ParentModel;
            var prp = model.GetPropertyPkId(this.ParentManyToManyGroupCatalogRelations, this.PropertyIdGuid); // position 6
            res.Add(prp);
            if (isOptimistic)
            {
                prp = model.GetPropertyVersion(this.ParentManyToManyGroupCatalogRelations, this.PropertyVersionGuid); // position 7
                res.Add(prp);
            }
            if (this.GuidDoc1 != null)
            {
                if (model.IsUseCompositeNames)
                    prp = model.GetPropertyRef(this.ParentManyToManyGroupCatalogRelations, this.RefDoc1Guid, "Ref" + ((ICompositeName)this.Cfg.DicNodes[this.GuidDoc1]).CompositeName, 1, false);
                else
                    prp = model.GetPropertyRef(this.ParentManyToManyGroupCatalogRelations, this.RefDoc1Guid, "Ref" + this.Cfg.DicNodes[this.GuidDoc1].Name, 1, false);
                res.Add(prp);
            }
            if (this.GuidDoc2 != null)
            {
                if (model.IsUseCompositeNames)
                    prp = model.GetPropertyRef(this.ParentManyToManyGroupCatalogRelations, this.RefDoc2Guid, "Ref" + ((ICompositeName)this.Cfg.DicNodes[this.GuidDoc2]).CompositeName, 2, false);
                else
                    prp = model.GetPropertyRef(this.ParentManyToManyGroupCatalogRelations, this.RefDoc2Guid, "Ref" + this.Cfg.DicNodes[this.GuidDoc2].Name, 2, false);
                res.Add(prp);
            }
            if (this.IsUseHistory)
            {
                prp = model.GetPropertyDateTimeUtc(this.ParentManyToManyGroupCatalogRelations, this.PropertyDataTimeGuid, "DataTimeUtc", 3, false);
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
            if (!isExcludeSpecial)
                this.GetSpecialProperties(res, isOptimistic);
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
        public SortedObservableCollection<ITreeConfigNodeSortable>? ListObjects
        {
            get
            {
                Debug.Assert(this.Cfg != null);
                return new SortedObservableCollection<ITreeConfigNodeSortable>(this.Cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments);
            }
        }
    }
}
