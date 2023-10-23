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
    public partial class CatalogsManyToManyRelation : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup,
        IDbTable, ITreeConfigNodeSortable
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

        [Browsable(false)]
        public GroupCatalogManyToManyRelations ParentGroupCatalogManyToManyRelations { get { Debug.Assert(this.Parent != null); return (GroupCatalogManyToManyRelations)this.Parent; } }
        [Browsable(false)]
        public IGroupCatalogManyToManyRelations ParentGroupCatalogManyToManyRelationsI { get { Debug.Assert(this.Parent != null); return (IGroupCatalogManyToManyRelations)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupCatalogManyToManyRelations.Children;
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
            Debug.Assert(this.Parent is GroupListCatalogs);
            var cfg = ((GroupListCatalogs)this.Parent).ParentModel.Cfg;
            Debug.Assert(cfg.DicNodes.ContainsKey(this._RefCat1Guid));
            string nameCat1 = ((Catalog)cfg.DicNodes[this._RefCat1Guid]).Name;
            Debug.Assert(cfg.DicNodes.ContainsKey(this._RefCat2Guid));
            string nameCat2 = ((Catalog)cfg.DicNodes[this._RefCat2Guid]).Name;
            Debug.Assert(nameCat1.CompareTo(nameCat2) != 0);
            if (nameCat1.CompareTo(nameCat2) < 0)
                return $"Many_to_many_{nameCat1}_{nameCat2}";
            else
                return $"Many_to_many_{nameCat2}_{nameCat1}";
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

        public CatalogsManyToManyRelation(ITreeConfigNode parent, string name)
            : this(parent)
        {
            this._Name = name;
        }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupCatalogManyToManyRelations.ListCatalogsManyToManyRelations.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Catalog?)this.ParentGroupCatalogManyToManyRelations.ListCatalogsManyToManyRelations.GetPrev(this);
            if (prev == null)
                return;
            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupCatalogManyToManyRelations.ListCatalogsManyToManyRelations.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupCatalogManyToManyRelations.ListCatalogsManyToManyRelations.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (CatalogsManyToManyRelation?)this.ParentGroupCatalogManyToManyRelations.ListCatalogsManyToManyRelations.GetNext(this);
            if (next == null)
                return;
            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupCatalogManyToManyRelations.ListCatalogsManyToManyRelations.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = CatalogsManyToManyRelation.Clone(this.ParentGroupCatalogManyToManyRelations, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupCatalogManyToManyRelations.ListCatalogsManyToManyRelations.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new CatalogsManyToManyRelation(this.Parent);
            this.ParentGroupCatalogManyToManyRelations.Add(node);
            this.GetUniqueName(Defaults.CatalogName, node, this.ParentGroupCatalogManyToManyRelations.ListCatalogsManyToManyRelations);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupCatalogManyToManyRelations.ListCatalogsManyToManyRelations.Remove(this);
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
            var model = this.ParentGroupCatalogManyToManyRelations.ParentGroupListCatalogs.ParentModel;
            var prp = model.GetPropertyPkId(this.ParentGroupCatalogManyToManyRelations, this.PropertyIdGuid);
            res.Add(prp);
            if (isOptimistic)
            {
                prp = model.GetPropertyVersion(this.ParentGroupCatalogManyToManyRelations, this.PropertyVersionGuid);
                res.Add(prp);
            }
            if (this.GuidCat1 != null)
            {
                if (model.IsUseCompositeNames)
                    prp = model.GetPropertyRef(this.ParentGroupCatalogManyToManyRelations, this.RefCat1Guid, "Ref" + ((ICompositeName)this.Cfg.DicNodes[this.GuidCat1]).CompositeName, false);
                else
                    prp = model.GetPropertyRef(this.ParentGroupCatalogManyToManyRelations, this.RefCat1Guid, "Ref" + this.Cfg.DicNodes[this.GuidCat1].Name, false);
                res.Add(prp);
            }
            if (this.GuidCat2 != null)
            {
                if (model.IsUseCompositeNames)
                    prp = model.GetPropertyRef(this.ParentGroupCatalogManyToManyRelations, this.RefCat1Guid, "Ref" + ((ICompositeName)this.Cfg.DicNodes[this.GuidCat2]).CompositeName, false);
                else
                    prp = model.GetPropertyRef(this.ParentGroupCatalogManyToManyRelations, this.RefCat1Guid, "Ref" + this.Cfg.DicNodes[this.GuidCat2].Name, false);
                res.Add(prp);
            }
            if (this.IsUseHistory)
            {
             //   ???
                    prp = model.GetPropertyRef(this.ParentGroupCatalogManyToManyRelations, this.RefCat1Guid, "Ref" + this.Cfg.DicNodes[this.GuidCat2].Name, false);
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
        #endregion Get Properties and Details
    }
}
