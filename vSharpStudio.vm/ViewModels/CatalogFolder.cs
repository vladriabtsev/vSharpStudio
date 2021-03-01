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
    public partial class CatalogFolder : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNodeGroup, IDbTable
    {
        public static readonly string DefaultName = "Items Group";

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as Catalog;
            return p.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }
        [Browsable(false)]
        new public string IconName { get { return "iconFolder"; } }
        partial void OnInit()
        {
            this._Name = "Folder";
            this._Description = "Catalog items groups";
            this.IsIncludableInModels = true;

            this.PropertyIdGuid = System.Guid.NewGuid().ToString();
            this.PropertyCodeGuid = System.Guid.NewGuid().ToString();
            this.MaxNameLength = 20;
            this.PropertyNameGuid = System.Guid.NewGuid().ToString();
            this.MaxDescriptionLength = 100;
            this.PropertyDescriptionGuid = System.Guid.NewGuid().ToString();
            this.PropertyParentGuid = System.Guid.NewGuid().ToString();

            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
#if DEBUG
            // SubNodes.Add(this.GroupConstants, 1);
#endif
            this.GroupProperties.Parent = this;
            this.GroupPropertiesTabs.Parent = this;
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
            this.Children.Add(this.GroupProperties, 3);
            this.Children.Add(this.GroupPropertiesTabs, 4);
            VmBindable.IsNotifyingStatic = true;
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
            this.GroupProperties.AddAllAppGenSettingsVmsToNode();
            this.GroupPropertiesTabs.AddAllAppGenSettingsVmsToNode();
        }

        public CatalogFolder(ITreeConfigNode parent, string name)
            : this(parent)
        {
            this.Name = name;
        }

        public CatalogFolder(ITreeConfigNode parent, string name, List<Property> listProperties)
            : this(parent)
        {
            Contract.Requires(listProperties != null);
            this.Name = name;
            foreach (var t in listProperties)
            {
                this.GroupProperties.ListProperties.Add(t);
            }
        }
        public PropertiesTab AddPropertiesTab(string name)
        {
            var node = new PropertiesTab(this.GroupPropertiesTabs) { Name = name };
            this.GroupPropertiesTabs.NodeAddNewSubNode(node);
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
            dt.IsNullable = isNullable;
            var node = new Property(this) { Name = name, DataType = dt };
            this.NodeAddNewSubNode(node);
            return node;
        }

        #region Tree operations
        #endregion Tree operations

        [ExpandableObjectAttribute()]
        public dynamic Setting { get; set; }

        [PropertyOrder(1)]
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
        public IReadOnlyList<IPropertiesTab> GetIncludedPropertiesTabs(string guidAppPrjGen)
        {
            var res = new List<IPropertiesTab>();
            foreach (var t in this.GroupPropertiesTabs.ListPropertiesTabs)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    res.Add(t);
                }
            }
            return res;
        }
        private void GetSpecialProperties(List<IProperty> res)
        {
            var cfg = this.GetConfig();
            var ctlg = (Catalog)this.Parent;
            var prp = cfg.Model.GetPropertyId(this.PropertyIdGuid);
            res.Add(prp);
            prp = cfg.Model.GetPropertyRefParent(this.PropertyParentGuid, "RefTreeParent");
            (prp.DataType as DataType).IsNullable = true;
            res.Add(prp);
            if (this.UseCodeProperty.HasValue)
            {
                if (this.UseCodeProperty.Value)
                {
                    switch (this.CodePropertySettings.Type)
                    {
                        case EnumCatalogCodeType.AutoNumber:
                            throw new NotImplementedException();
                            break;
                        case EnumCatalogCodeType.AutoText:
                            throw new NotImplementedException();
                            break;
                        case EnumCatalogCodeType.Number:
                            prp = cfg.Model.GetPropertyInt(this.PropertyCodeGuid, this.CodePropertySettings.Length, cfg.Model.GroupCatalogs.PropertyCodeName);
                            break;
                        case EnumCatalogCodeType.Text:
                            prp = cfg.Model.GetPropertyString(this.PropertyCodeGuid, this.CodePropertySettings.Length, cfg.Model.GroupCatalogs.PropertyCodeName);
                            break;
                    }
                    res.Add(prp);
                }
            }
            else if (ctlg.UseCodeProperty)
            {
                switch (ctlg.CodePropertySettings.Type)
                {
                    case EnumCatalogCodeType.AutoNumber:
                        throw new NotImplementedException();
                        break;
                    case EnumCatalogCodeType.AutoText:
                        throw new NotImplementedException();
                        break;
                    case EnumCatalogCodeType.Number:
                        prp = cfg.Model.GetPropertyInt(ctlg.PropertyCodeGuid, ctlg.CodePropertySettings.Length, cfg.Model.GroupCatalogs.PropertyCodeName);
                        break;
                    case EnumCatalogCodeType.Text:
                        prp = cfg.Model.GetPropertyString(ctlg.PropertyCodeGuid, ctlg.CodePropertySettings.Length, cfg.Model.GroupCatalogs.PropertyCodeName);
                        break;
                }
                res.Add(prp);
            }
            if (this.UseNameProperty.HasValue)
            {
                if (this.UseCodeProperty.Value)
                {
                    prp = cfg.Model.GetPropertyString(this.PropertyNameGuid, this.MaxNameLength, cfg.Model.GroupCatalogs.PropertyNameName);
                    res.Add(prp);
                }
            }
            else if (ctlg.UseNameProperty)
            {
                prp = cfg.Model.GetPropertyString(ctlg.PropertyNameGuid, ctlg.MaxNameLength, cfg.Model.GroupCatalogs.PropertyNameName);
                res.Add(prp);
            }
            if (this.UseDescriptionProperty.HasValue)
            {
                if (this.UseDescriptionProperty.Value)
                {
                    prp = cfg.Model.GetPropertyString(this.PropertyDescriptionGuid, this.MaxDescriptionLength, cfg.Model.GroupCatalogs.PropertyDescriptionName);
                    res.Add(prp);
                }
            }
            else if (ctlg.UseDescriptionProperty)
            {
                prp = cfg.Model.GetPropertyString(ctlg.PropertyDescriptionGuid, ctlg.MaxDescriptionLength, cfg.Model.GroupCatalogs.PropertyDescriptionName);
                res.Add(prp);
            }
        }
    }
}
