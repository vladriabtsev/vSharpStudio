using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Grouping:{Name,nq} props:{GroupProperties.ListProperties.Count,nq}")]
    public partial class CatalogFolder : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNodeGroup, IDbTable, INodeWithProperties
    {
        [BrowsableAttribute(false)]
        public Catalog ParentCatalog { get { Debug.Assert(this.Parent != null); return (Catalog)this.Parent; } }
        [BrowsableAttribute(false)]
        public ICatalog ParentCatalogI { get { Debug.Assert(this.Parent != null); return (ICatalog)this.Parent; } }

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            return this.ParentCatalog.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ObservableCollection<ITreeConfigNode> Children { get; private set; }
        [Browsable(false)]
        new public string IconName { get { return "iconFolder"; } }
        partial void OnCreated()
        {
            this._Name = "Folder";
            this._Description = "Catalog items groups";
            this.IsIncludableInModels = true;

            this.PropertyIdGuid = System.Guid.NewGuid().ToString();
            this.PropertyCodeGuid = System.Guid.NewGuid().ToString();
            this.PropertyNameGuid = System.Guid.NewGuid().ToString();
            this.PropertyDescriptionGuid = System.Guid.NewGuid().ToString();
            this.PropertyRefSelfGuid = System.Guid.NewGuid().ToString();
            this.PropertyVersionGuid = System.Guid.NewGuid().ToString();
            this.ViewListWideGuid = System.Guid.NewGuid().ToString();
            this.ViewListNarrowGuid = System.Guid.NewGuid().ToString();

            this.MaxNameLength = 20;
            this.MaxDescriptionLength = 100;

            //this.CodePropertySettings.Parent = this;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            VmBindable.IsNotifyingStatic = false;
            this.Children = new ObservableCollection<ITreeConfigNode>();
            this.Children.Add(this.GroupProperties);
            this.Children.Add(this.GroupDetails);
            VmBindable.IsNotifyingStatic = true;
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
            this.GroupProperties.AddAllAppGenSettingsVmsToNode();
            this.GroupDetails.AddAllAppGenSettingsVmsToNode();
        }

        public CatalogFolder(ITreeConfigNode parent, string name)
            : this(parent)
        {
            this.Name = name;
        }

        public CatalogFolder(ITreeConfigNode parent, string name, List<Property> listProperties)
            : this(parent)
        {
            Debug.Assert(listProperties != null);
            this.Name = name;
            foreach (var t in listProperties)
            {
                this.GroupProperties.ListProperties.Add(t);
            }
        }
        public Detail AddPropertiesTab(string name)
        {
            var node = new Detail(this.GroupDetails) { Name = name };
            this.GroupDetails.NodeAddNewSubNode(node);
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
        public Property AddPropertyEnumeration(string name, Enumeration en, bool isNullable)
        {
            var dt = new DataType() { DataTypeEnum = EnumDataType.ENUMERATION, ObjectGuid = en.Guid };
            var node = new Property(this) { Name = name, DataType = dt };
            node.IsNullable = isNullable;
            this.NodeAddNewSubNode(node);
            return node;
        }

        #region Tree operations
        #endregion Tree operations

        [ExpandableObjectAttribute()]
        public dynamic Setting { get; set; }

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
        public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen)
        {
            var res = new List<IProperty>();
            GetSpecialProperties(res);
            foreach (var t in this.GroupProperties.ListProperties)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    res.Add(t);
                }
            }
            return res;
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
        public bool GetUseCodeProperty()
        {
            bool res = false;
            if (this.UseCodeProperty == EnumUseType.Default)
                res = this.ParentCatalog.GetUseCodeProperty();
            else if (this.UseCodeProperty == EnumUseType.Yes)
                res = true;
            else
                res = false;
            return res;
        }
        public bool GetUseNameProperty()
        {
            bool res = false;
            if (this.UseNameProperty == EnumUseType.Default)
                res = this.ParentCatalog.GetUseNameProperty();
            else if (this.UseNameProperty == EnumUseType.Yes)
                res = true;
            else
                res = false;
            return res;
        }
        public bool GetUseDescriptionProperty()
        {
            bool res = false;
            if (this.UseDescriptionProperty == EnumUseType.Default)
                res = this.ParentCatalog.GetUseDescriptionProperty();
            else if (this.UseDescriptionProperty == EnumUseType.Yes)
                res = true;
            else
                res = false;
            return res;
        }
        private void GetSpecialProperties(List<IProperty> res)
        {
            var cfg = this.GetConfig();
            var ctlg = (Catalog)this.Parent;
            var prp = cfg.Model.GetPropertyId(this.PropertyIdGuid);
            res.Add(prp);
            prp = cfg.Model.GetPropertyRefParent(this.PropertyRefSelfGuid, "RefTreeParent", true);
            (prp as Property).IsNullable = true;
            res.Add(prp);
            if (this.GetUseCodeProperty())
            {
                    switch (this.CodePropertySettings.Type)
                    {
                        case EnumCodeType.AutoNumber:
                            throw new NotImplementedException();
                        case EnumCodeType.AutoText:
                            throw new NotImplementedException();
                        case EnumCodeType.Number:
                            prp = cfg.Model.GetPropertyCatalogCodeInt(this.PropertyCodeGuid, this.CodePropertySettings.Length);
                            break;
                        case EnumCodeType.Text:
                            prp = cfg.Model.GetPropertyCatalogCode(this.PropertyCodeGuid, this.CodePropertySettings.Length);
                            break;
                    }
                    res.Add(prp);
            }
            if (this.GetUseNameProperty())
            {
                prp = cfg.Model.GetPropertyCatalogName(ctlg.PropertyNameGuid, ctlg.MaxNameLength);
                res.Add(prp);
            }
            if (this.GetUseDescriptionProperty())
            {
                prp = cfg.Model.GetPropertyCatalogDescription(ctlg.PropertyDescriptionGuid, ctlg.MaxDescriptionLength);
                res.Add(prp);
            }
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
            return this.ParentCatalog.IsGridSortableGet();
        }
        public bool IsGridFilterableGet()
        {
            if (this.IsGridFilterable == EnumUseType.Yes)
                return true;
            if (this.IsGridFilterable == EnumUseType.No)
                return false;
            return this.ParentCatalog.IsGridFilterableGet();
        }
        public bool IsGridSortableCustomGet()
        {
            if (this.IsGridSortableCustom == EnumUseType.Yes)
                return true;
            if (this.IsGridSortableCustom == EnumUseType.No)
                return false;
                return this.ParentCatalog.IsGridSortableCustomGet();
        }
    }
}
