using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Document : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup, IDbTable, INodeWithProperties
    {
        [BrowsableAttribute(false)]
        public GroupListDocuments ParentGroupListDocuments { get { return (GroupListDocuments)this.Parent; } }
        [BrowsableAttribute(false)]
        public IGroupListDocuments ParentGroupListDocumentsI { get { return (IGroupListDocuments)this.Parent; } }
        public static readonly string DefaultName = "Document";

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            return this.ParentGroupListDocuments.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        [BrowsableAttribute(false)]
        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }
        [Browsable(false)]
        new public string IconName { get { return "iconDiagnosticesFile"; } }
        //protected override string GetNodeIconName() { return "iconDiagnosticesFile"; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            this.PropertyIdGuid = System.Guid.NewGuid().ToString();
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
#if DEBUG
            // SubNodes.Add(this.GroupConstants, 1);
#endif
            this.GroupProperties.Parent = this;
            this.GroupDetails.Parent = this;
            this.GroupForms.Parent = this;
            this.GroupReports.Parent = this;
            this.PropertyIdGuid = System.Guid.NewGuid().ToString();
            this.PropertyDocCodeGuid = System.Guid.NewGuid().ToString();
            this.PropertyDocDateGuid = System.Guid.NewGuid().ToString();
            this.PropertyVersionGuid = System.Guid.NewGuid().ToString();
            this.RefillChildren();
        }
        protected override void OnInitFromDto()
        {
            base.OnInitFromDto();
            this.RefillChildren();
        }
        void RefillChildren()
        {
            VmBindable.IsNotifyingStatic = false;
            this.Children.Clear();
            this.Children.Add(this.GroupProperties, 6);
            this.Children.Add(this.GroupDetails, 7);
            this.Children.Add(this.GroupForms, 8);
            this.Children.Add(this.GroupReports, 9);
            VmBindable.IsNotifyingStatic = true;
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
            this.GroupProperties.AddAllAppGenSettingsVmsToNode();
            this.GroupDetails.AddAllAppGenSettingsVmsToNode();
            this.GroupForms.AddAllAppGenSettingsVmsToNode();
            this.GroupReports.AddAllAppGenSettingsVmsToNode();
        }
        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListDocuments.ListDocuments.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Document)this.ParentGroupListDocuments.ListDocuments.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupListDocuments.ListDocuments.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListDocuments.ListDocuments.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Document)this.ParentGroupListDocuments.ListDocuments.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupListDocuments.ListDocuments.MoveDown(this);
            this.SetSelected(this);
        }

        public override ITreeConfigNode NodeAddClone()
        {
            var node = Document.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupListDocuments.Add(node);
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Document(this.Parent);
            this.ParentGroupListDocuments.Add(node);
            this.GetUniqueName(Document.DefaultName, node, this.ParentGroupListDocuments.ListDocuments);
            this.SetSelected(node);
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
            var node = new Property(this.GroupProperties) { Name = name, DataType = new DataType() { DataTypeEnum = type, Length = length, Accuracy = accuracy } };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyString(string name, uint length)
        {
            var dt = new DataType() { DataTypeEnum = EnumDataType.STRING, Length = length };
            var node = new Property(this.GroupProperties) { Name = name, DataType = dt };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyNumerical(string name, uint length, uint accuracy)
        {
            var dt = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL, Length = length, Accuracy = accuracy };
            var node = new Property(this.GroupProperties) { Name = name, DataType = dt };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Detail AddPropertiesTab(string name)
        {
            var node = new Detail(this.GroupDetails) { Name = name };
            this.GroupDetails.NodeAddNewSubNode(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListDocuments.ListDocuments.Remove(this);
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
        /// <summary>
        /// All included properties (shared and normal)
        /// Shared included first
        /// </summary>
        /// <param name="guidAppPrjGen"></param>
        /// <returns></returns>
        public IReadOnlyList<IProperty> GetIncludedPropertiesWithShared(string guidAppPrjGen, bool isSupportVersion)
        {
            var res = new List<IProperty>();
            var grd = (GroupDocuments)this.Parent.Parent;
            //var cfg = this.GetConfig();
            //var prp = cfg.Model.GetPropertyId(this.PropertyIdGuid);
            //res.Add(prp);
            GetSpecialProperties(res, isSupportVersion);
            foreach (var t in grd.GroupSharedProperties.ListProperties)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    t.ComplexObjectName = "SharedDocProperties";
                    res.Add(t);
                }
            }
            foreach (var t in this.GroupProperties.ListProperties)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    res.Add(t);
                }
            }
            return res;
        }
        public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isSupportVersion)
        {
            var res = new List<IProperty>();
            GetSpecialProperties(res, isSupportVersion);
            foreach (var t in this.GroupProperties.ListProperties)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    res.Add(t);
                }
            }
            return res;
        }
        public IReadOnlyList<IProperty> GetAllProperties(bool isSupportVersion)
        {
            var res = new List<IProperty>();
            GetSpecialProperties(res, isSupportVersion);
            foreach (var t in this.GroupProperties.ListProperties)
            {
                res.Add(t);
            }
            return res;
        }
        private void GetSpecialProperties(List<IProperty> res, bool isSupportVersion)
        {

            var grd = (GroupDocuments)this.Parent.Parent;
            var cfg = this.GetConfig();
            var prp = cfg.Model.GetPropertyId(this.PropertyIdGuid);
            res.Add(prp);
            if (isSupportVersion)
            {
                prp = cfg.Model.GetPropertyVersion(this.PropertyVersionGuid);
                res.Add(prp);
            }
            if (this.GetUseDocDateProperty())
            {
                prp = cfg.Model.GetPropertyDocumentDate(this.PropertyDocDateGuid);
                res.Add(prp);
            }
            if (this.GetUseDocCodeProperty())
            {
                switch (this.CodePropertySettings.Type)
                {
                    case EnumCodeType.AutoNumber:
                        throw new NotImplementedException();
                    case EnumCodeType.AutoText:
                        throw new NotImplementedException();
                    case EnumCodeType.Number:
                        prp = cfg.Model.GetPropertyDocumentCodeInt(this.PropertyDocCodeGuid, this.CodePropertySettings.Length);
                        break;
                    case EnumCodeType.Text:
                        prp = cfg.Model.GetPropertyDocumentCodeString(this.PropertyDocCodeGuid, this.CodePropertySettings.Length);
                        break;
                }
                res.Add(prp);
            }
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
        /// <summary>
        /// Only shared properties
        /// </summary>
        /// <param name="guidAppPrjGen"></param>
        /// <returns></returns>
        public IReadOnlyList<IProperty> GetIncludedSharedProperties(string guidAppPrjGen)
        {
            var res = new List<IProperty>();
            var grd = (GroupDocuments)this.Parent.Parent;
            foreach (var t in grd.GroupSharedProperties.ListProperties)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    t.ComplexObjectName = "SharedDocProperties";
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
            IProperty pId = null;
            pId = cfg.Model.GetPropertyId(this.PropertyIdGuid);
            List<IProperty> lst = null;
            viewListData = new ViewListData(pId);
            lst = SelectViewProperties(formType, this.GroupProperties.ListProperties, form.ListGuidViewProperties, guidAppPrjGen);
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
            return this.ParentGroupListDocuments.ParentGroupDocuments.IsGridSortableGet();
        }
        public bool IsGridFilterableGet()
        {
            if (this.IsGridFilterable == EnumUseType.Yes)
                return true;
            if (this.IsGridFilterable == EnumUseType.No)
                return false;
            return this.ParentGroupListDocuments.ParentGroupDocuments.IsGridFilterableGet();
        }
        public bool IsGridSortableCustomGet()
        {
            if (this.IsGridSortableCustom == EnumUseType.Yes)
                return true;
            if (this.IsGridSortableCustom == EnumUseType.No)
                return false;
            return this.ParentGroupListDocuments.ParentGroupDocuments.IsGridSortableCustomGet();
        }
        public bool GetUseDocCodeProperty()
        {
            if (this.UseDocCodeProperty == EnumUseType.Yes)
                return true;
            if (this.UseDocCodeProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListDocuments.ParentGroupDocuments.GetUseDocCodeProperty();
        }
        public bool GetUseDocDateProperty()
        {
            if (this.UseDocDateProperty == EnumUseType.Yes)
                return true;
            if (this.UseDocDateProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListDocuments.ParentGroupDocuments.GetUseDocDateProperty();
        }
    }
}
