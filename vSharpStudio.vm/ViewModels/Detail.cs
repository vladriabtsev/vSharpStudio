using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} properties:{GroupProperties.ListProperties.Count,nq} details:{GroupDetails.ListDetails.Count,nq}")]
    public partial class Detail : ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNode, IEditableNodeGroup, IDbTable, INodeWithProperties
    {
        [BrowsableAttribute(false)]
        public GroupListDetails ParentGroupListDetails { get { Debug.Assert(this.Parent != null); return (GroupListDetails)this.Parent; } }
        [BrowsableAttribute(false)]
        public IGroupListDetails ParentGroupListDetailsI { get { Debug.Assert(this.Parent != null); return (IGroupListDetails)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListDetails.Children;
        }
        #endregion ITree

        [Browsable(false)]
        new public string IconName { get { return "iconFolder"; } }
        //protected override string GetNodeIconName() { return "iconFolder"; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            this.IsIndexFk = true;

            this.PropertyIdGuid = System.Guid.NewGuid().ToString();
            this.PropertyRefParentGuid = System.Guid.NewGuid().ToString();
            this.PropertyCodeGuid = System.Guid.NewGuid().ToString();
            this.PropertyNameGuid = System.Guid.NewGuid().ToString();
            this.PropertyDescriptionGuid = System.Guid.NewGuid().ToString();
            this.PropertyVersionGuid = System.Guid.NewGuid().ToString();
            this.ViewListWideGuid = System.Guid.NewGuid().ToString();
            this.ViewListNarrowGuid = System.Guid.NewGuid().ToString();
            this.PropertyVersionGuid = System.Guid.NewGuid().ToString();
            var glp = (this.ParentGroupListDetails.Parent as INodeWithProperties);
            Debug.Assert(glp != null);
            this.Position = glp.GroupProperties.GetNextPosition();
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            if (this.Children.Count > 0)
                return;
            VmBindable.IsNotifyingStatic = false;
            this.Children.Add(this.GroupProperties);
            this.Children.Add(this.GroupDetails);
            VmBindable.IsNotifyingStatic = true;
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
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
            this.GroupProperties.AddAllAppGenSettingsVmsToNode();
            this.GroupDetails.AddAllAppGenSettingsVmsToNode();
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListDetails.ListDetails.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }
        public override void NodeUp()
        {
            var prev = (Detail)this.ParentGroupListDetails.ListDetails.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }
        public override void NodeMoveUp()
        {
            this.ParentGroupListDetails.ListDetails.MoveUp(this);
            this.SetSelected(this);
        }
        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListDetails.ListDetails.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }
        public override void NodeDown()
        {
            var next = (Detail)this.ParentGroupListDetails.ListDetails.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }
        public override void NodeMoveDown()
        {
            this.ParentGroupListDetails.ListDetails.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            Debug.Assert(this.Parent != null);
            var node = Detail.Clone(this.Parent, this, true, true);
            this.ParentGroupListDetails.Add(node);
            var glp = (this.ParentGroupListDetails.Parent as INodeWithProperties);
            Debug.Assert(glp != null);
            node.Position = glp.GroupProperties.GetNextPosition();
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            Debug.Assert(this.Parent != null);
            var node = new Detail(this.Parent);
            this.ParentGroupListDetails.Add(node);
            var glp = (this.ParentGroupListDetails.Parent as INodeWithProperties);
            Debug.Assert(glp != null);
            node.Position = glp.GroupProperties.GetNextPosition();
            this.GetUniqueName(Defaults.DetailName, node, this.ParentGroupListDetails.ListDetails);
            this.SetSelected(node);
            return node;
        }
        public Detail AddPropertiesTab(string name)
        {
            var node = new Detail(this.GroupDetails) { Name = name };
            this.GroupDetails.NodeAddNewSubNode(node);
            var glp = (this.ParentGroupListDetails.Parent as INodeWithProperties);
            Debug.Assert(glp != null);
            node.Position = glp.GroupProperties.GetNextPosition();
            return node;
        }
        public Property AddProperty(string name)
        {
            var node = new Property(this.GroupProperties) { Name = name };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddProperty(string name, DataType type)
        {
            var node = new Property(this.GroupProperties) { Name = name, DataType = type };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddProperty(string name, EnumDataType type, uint length, uint accuracy)
        {
            var node = new Property(this.GroupProperties) { Name = name };
            node.DataType = new DataType(node) { DataTypeEnum = type, Length = length, Accuracy = accuracy };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyString(string name, uint length)
        {
            var node = new Property(this.GroupProperties) { Name = name };
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.STRING, Length = length };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyNumerical(string name, uint length, uint accuracy)
        {
            var node = new Property(this.GroupProperties) { Name = name };
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.NUMERICAL, Length = length, Accuracy = accuracy };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public IEnumerable<ITreeConfigNode> GetParentList()
        {
            return this.ParentGroupListDetails.ListDetails;
        }
        public void Remove()
        {
            this.ParentGroupListDetails.ListDetails.Remove(this);
        }
        #endregion Tree operations

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
        public IForm GetForm(FormType ftype)
        {
            var f = (from tf in this.GroupForms.ListForms where tf.EnumFormType == ftype select tf).SingleOrDefault();
            if (f == null)
            {
                var lstp = new List<IProperty>();
                int i = 0;
                foreach (var t in this.GroupProperties.ListProperties)
                {
                    i++;
                    if (i > 1)
                        break;
                    lstp.Add(t);
                }
                this.GetSpecialProperties(lstp, false);
                f = new Form(this.GroupForms, lstp);
                f.Name = $"View{Enum.GetName(typeof(FormType), ftype)}";
                f.EnumFormType = ftype;
            }
            return f;
        }
        public IReadOnlyList<IForm> GetListForms()
        {
            var res = new List<IForm>();
            res.Add(this.GetForm(FormType.ListNarrow));
            res.Add(this.GetForm(FormType.ListWide));
            return res;
        }
        public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isSupportVersion)
        {
            var res = new List<IProperty>();
            this.GetSpecialProperties(res, isSupportVersion);
            foreach (var t in this.GroupProperties.ListProperties)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    res.Add(t);
                }
            }
            return res;
        }
        public void GetSpecialProperties(List<IProperty> res, bool isSupportVersion)
        {
            string parentTable = "";
            if (this.ParentGroupListDetails.Parent is Detail dt)
            {
                parentTable = dt.CompositeName;
            }
            else if (this.ParentGroupListDetails.Parent is Catalog c)
            {
                parentTable = c.CompositeName;
            }
            else if (this.ParentGroupListDetails.Parent is CatalogFolder cf)
            {
                parentTable = cf.CompositeName;
            }
            else if (this.ParentGroupListDetails.Parent is Document d)
            {
                parentTable = d.CompositeName;
            }
            else
                throw new NotImplementedException();
            var cfg = this.GetConfig();
            var prp = cfg.Model.GetPropertyId(this.GroupProperties, this.PropertyIdGuid);
            res.Add(prp);
            prp = cfg.Model.GetPropertyRefParent(this.GroupProperties, this.PropertyRefParentGuid, "Ref" + parentTable);
            res.Add(prp);
            if (isSupportVersion)
            {
                prp = cfg.Model.GetPropertyVersion(this.GroupProperties, this.PropertyVersionGuid);
                res.Add(prp);
            }


            //if (this.GetUseCodeProperty())
            //{
            //    switch (this.CodePropertySettings.Type)
            //    {
            //        case EnumCodeType.AutoNumber:
            //            throw new NotImplementedException();
            //        case EnumCodeType.AutoText:
            //            throw new NotImplementedException();
            //        case EnumCodeType.Number:
            //            prp = cfg.Model.GetPropertyCatalogCodeInt(this.PropertyCodeGuid, this.CodePropertySettings.Length);
            //            break;
            //        case EnumCodeType.Text:
            //            prp = cfg.Model.GetPropertyCatalogCode(this.PropertyCodeGuid, this.CodePropertySettings.Length);
            //            break;
            //    }
            //    res.Add(prp);
            //}
            //if (this.GetUseNameProperty())
            //{
            //    prp = cfg.Model.GetPropertyCatalogName(this.PropertyNameGuid, this.MaxNameLength);
            //    res.Add(prp);
            //}
            //if (this.GetUseDescriptionProperty())
            //{
            //    prp = cfg.Model.GetPropertyCatalogDescription(this.PropertyDescriptionGuid, this.MaxDescriptionLength);
            //    res.Add(prp);
            //}
        }
        public IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjGen)
        {
            var res = new List<IDetail>();
            foreach (var t in this.GroupDetails.ListDetails)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    res.Add(t);
                }
            }
            return res;
        }
        public ViewFormData GetFormViewData(FormType formType, string guidAppPrjGen)
        {
            ViewListData? viewListData = null;
            var cfg = this.GetConfig();
            Form? form = (from p in this.GroupForms.ListForms where p.EnumFormType == formType select p).SingleOrDefault();
            var pId = cfg.Model.GetPropertyId(this.GroupProperties, this.PropertyIdGuid);
            viewListData = new ViewListData(pId);
            var lst = this.SelectViewProperties(formType, this.GroupProperties.ListProperties, form.ListGuidViewProperties, guidAppPrjGen);
            viewListData.ListViewProperties.AddRange(lst);
            return new ViewFormData(null, viewListData);
        }
        private List<IProperty> SelectViewProperties(FormType formType, ConfigNodesCollection<Property> fromPropertiesList, ObservableCollection<string> viewPropertiesGuids, string guidAppPrjGen)
        {
            var res = new List<IProperty>();
            if (viewPropertiesGuids.Count > 0)
            {
                foreach (var t in fromPropertiesList)
                {
                    if (guidAppPrjGen == null || t.IsIncluded(guidAppPrjGen))
                    {
                        foreach (var tguid in viewPropertiesGuids)
                        {
                            if (t.Guid == tguid)
                            {
                                res.Add(t);
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                var len = 3;
                foreach (var t in fromPropertiesList)
                {
                    if (guidAppPrjGen == null || t.IsIncluded(guidAppPrjGen))
                    {
                        len--;
                        res.Add(t);
                    }
                    if (len == 0)
                        break;
                }
            }
            return res;
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            lst.Add(this.GetPropertyName(() => this.Parent));
            lst.Add(this.GetPropertyName(() => this.Children));
            return lst.ToArray();
        }
        public bool IsGridSortableGet()
        {
            if (this.IsGridSortable == EnumUseType.Yes)
                return true;
            if (this.IsGridSortable == EnumUseType.No)
                return false;
            return this.ParentGroupListDetails.GetIsGridSortable();
        }
        public bool IsGridFilterableGet()
        {
            if (this.IsGridFilterable == EnumUseType.Yes)
                return true;
            if (this.IsGridFilterable == EnumUseType.No)
                return false;
            return this.ParentGroupListDetails.GetIsGridFilterable();
        }
        public bool IsGridSortableCustomGet()
        {
            if (this.IsGridSortableCustom == EnumUseType.Yes)
                return true;
            if (this.IsGridSortableCustom == EnumUseType.No)
                return false;
            return this.ParentGroupListDetails.GetIsGridSortableCustom();
        }
        public bool GetUseCodeProperty()
        {
            if (this.UseCodeProperty == EnumUseType.Yes)
                return true;
            if (this.UseCodeProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListDetails.GetUseCodeProperty();
        }
        public bool GetUseNameProperty()
        {
            if (this.UseNameProperty == EnumUseType.Yes)
                return true;
            if (this.UseNameProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListDetails.GetUseNameProperty();
        }
        public bool GetUseDescriptionProperty()
        {
            if (this.UseDescriptionProperty == EnumUseType.Yes)
                return true;
            if (this.UseDescriptionProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListDetails.GetUseDescriptionProperty();
        }
    }
}
