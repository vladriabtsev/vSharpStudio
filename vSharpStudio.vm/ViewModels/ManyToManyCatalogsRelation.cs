﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Xml.Linq;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class ManyToManyCatalogsRelation : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup,
        ITreeConfigNodeSortable
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            string cat1 = "<empty>", cat2 = "<empty>";
            if (this.GuidCat1 != null)
            {
                Debug.Assert(this.Cfg.DicNodes.ContainsKey(this.GuidCat1));
                cat1 = this.Cfg.DicNodes[this.GuidCat1].Name;
            }
            if (this.GuidCat2 != null)
            {
                Debug.Assert(this.Cfg.DicNodes.ContainsKey(this.GuidCat2));
                cat2 = this.Cfg.DicNodes[this.GuidCat2].Name;
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
        public ManyToManyGroupCatalogsRelations ParentManyToManyGroupCatalogRelations { get { Debug.Assert(this.Parent != null); return (ManyToManyGroupCatalogsRelations)this.Parent; } }
        [Browsable(false)]
        public IManyToManyGroupCatalogsRelations ParentManyToManyGroupCatalogRelationsI { get { Debug.Assert(this.Parent != null); return (IManyToManyGroupCatalogsRelations)this.Parent; } }

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
            this._RefCat1Guid = System.Guid.NewGuid().ToString();
            this._RefCat2Guid = System.Guid.NewGuid().ToString();
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
        partial void OnRefCat1GuidChanged()
        {
            if (this._RefCat2Guid != null)
            {
                this._Name = this.GetName();
            }
        }
        private string GetName()
        {
            Debug.Assert(this.Parent != null);
            var cfg = this.ParentManyToManyGroupCatalogRelations.ParentGroupRelations.ParentModel.Cfg;
            Debug.Assert(cfg.DicNodes.ContainsKey(this._RefCat1Guid));
            string name1 = ((Catalog)cfg.DicNodes[this._RefCat1Guid]).Name;
            Debug.Assert(cfg.DicNodes.ContainsKey(this._RefCat2Guid));
            string name2 = ((Catalog)cfg.DicNodes[this._RefCat2Guid]).Name;
            Debug.Assert(name1.CompareTo(name2) != 0);
            if (name1.CompareTo(name2) < 0)
                return $"Many_to_many_{name1}_{name2}";
            else
                return $"Many_to_many_{name2}_{name1}";
        }
        partial void OnRefCat2GuidChanged()
        {
            if (this._RefCat1Guid != null)
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

        public ManyToManyCatalogsRelation(ITreeConfigNode parent, string name)
            : this(parent)
        {
            this._Name = name;
        }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentManyToManyGroupCatalogRelations.ListCatalogsRelations.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Catalog?)this.ParentManyToManyGroupCatalogRelations.ListCatalogsRelations.GetPrev(this);
            if (prev == null)
                return;
            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentManyToManyGroupCatalogRelations.ListCatalogsRelations.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentManyToManyGroupCatalogRelations.ListCatalogsRelations.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (ManyToManyCatalogsRelation?)this.ParentManyToManyGroupCatalogRelations.ListCatalogsRelations.GetNext(this);
            if (next == null)
                return;
            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentManyToManyGroupCatalogRelations.ListCatalogsRelations.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = ManyToManyCatalogsRelation.Clone(this.ParentManyToManyGroupCatalogRelations, this, true, true);
            node.Parent = this.Parent;
            this.ParentManyToManyGroupCatalogRelations.ListCatalogsRelations.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new ManyToManyCatalogsRelation(this.Parent);
            this.ParentManyToManyGroupCatalogRelations.Add(node);
            this.GetUniqueName(Defaults.CatalogMtmRelationName, node, this.ParentManyToManyGroupCatalogRelations.ListCatalogsRelations);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentManyToManyGroupCatalogRelations.ListCatalogsRelations.Remove(this);
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
            if (this.GuidCat1 != null)
            {
                if (model.IsUseCompositeNames)
                    prp = model.GetPropertyRef(this.ParentManyToManyGroupCatalogRelations, this.RefCat1Guid, "Ref" + ((ICompositeName)this.Cfg.DicNodes[this.GuidCat1]).CompositeName, 1, false);
                else
                    prp = model.GetPropertyRef(this.ParentManyToManyGroupCatalogRelations, this.RefCat1Guid, "Ref" + this.Cfg.DicNodes[this.GuidCat1].Name, 1, false);
                res.Add(prp);
            }
            if (this.GuidCat2 != null)
            {
                if (model.IsUseCompositeNames)
                    prp = model.GetPropertyRef(this.ParentManyToManyGroupCatalogRelations, this.RefCat2Guid, "Ref" + ((ICompositeName)this.Cfg.DicNodes[this.GuidCat2]).CompositeName, 2, false);
                else
                    prp = model.GetPropertyRef(this.ParentManyToManyGroupCatalogRelations, this.RefCat2Guid, "Ref" + this.Cfg.DicNodes[this.GuidCat2].Name, 2, false);
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
                return new SortedObservableCollection<ITreeConfigNodeSortable>(this.Cfg.Model.GroupCatalogs.ListCatalogs);
            }
        }
    }
}