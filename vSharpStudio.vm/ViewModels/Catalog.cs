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
    [DebuggerDisplay("Catalog:{Name,nq} props:{GroupProperties.ListProperties.Count,nq}")]
    public partial class Catalog : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup,
        IDbTable, ITreeConfigNode, INodeWithProperties
    {
        public static readonly string DefaultName = "Catalog";

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as GroupListCatalogs;
            return p.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }
        [Browsable(false)]
        new public string IconName { get { return "iconCatalogProperty"; } }
        //protected override string GetNodeIconName() { return "iconCatalogProperty"; }
        partial void OnInit()
        {
            this.ListGuidViewProperties = new ObservableCollection<string>();
            this.ListGuidViewFolderProperties = new ObservableCollection<string>();
            this.IsIncludableInModels = true;
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
#if DEBUG
            // SubNodes.Add(this.GroupConstants, 1);
#endif
            this.Folder.Parent = this;
            this.GroupProperties.Parent = this;
            this.GroupPropertiesTabs.Parent = this;
            this.GroupForms.Parent = this;
            this.GroupReports.Parent = this;
            this.ItemIconType = EnumCatalogTreeIcon.None;
            this.PropertyIdGuid = System.Guid.NewGuid().ToString();
            this.UseCodeProperty = true;
            this.PropertyCodeGuid = System.Guid.NewGuid().ToString();
            this.UseNameProperty = true;
            this.MaxNameLength = 20;
            this.PropertyNameGuid = System.Guid.NewGuid().ToString();
            this.UseDescriptionProperty = false;
            this.MaxDescriptionLength = 100;
            this.PropertyDescriptionGuid = System.Guid.NewGuid().ToString();
            this.UseTree = false;
            this.MaxTreeLevels = 2;
            this.UseSeparatePropertiesForGroups = false;
            this.GroupIconType = EnumCatalogTreeIcon.Folder;
            this.PropertyRefFolderGuid = System.Guid.NewGuid().ToString();
            this.PropertyRefSelfGuid = System.Guid.NewGuid().ToString();
            this.PropertyIsFolderGuid = System.Guid.NewGuid().ToString();
            this.PropertyIsOpenGuid = System.Guid.NewGuid().ToString();
            this.ViewDefaultGuid = System.Guid.NewGuid().ToString();
            this.RefillChildren();
            HideProperties();
        }
        protected override void OnInitFromDto()
        {
            base.OnInitFromDto();
            this.RefillChildren();
            HideProperties();
        }
        public void RefillChildren()
        {
            VmBindable.IsNotifyingStatic = false;
            this.Children.Clear();
            if (this.UseTree && this.UseSeparatePropertiesForGroups)
            {
                this.Children.Add(this.Folder, 2);
            }
            this.Children.Add(this.GroupProperties, 3);
            this.Children.Add(this.GroupPropertiesTabs, 4);
            this.Children.Add(this.GroupForms, 5);
            this.Children.Add(this.GroupReports, 6);
            this.CodePropertySettings.Parent = this;
            VmBindable.IsNotifyingStatic = true;
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
            this.GroupProperties.AddAllAppGenSettingsVmsToNode();
            this.GroupPropertiesTabs.AddAllAppGenSettingsVmsToNode();
            this.GroupForms.AddAllAppGenSettingsVmsToNode();
            this.GroupReports.AddAllAppGenSettingsVmsToNode();
        }

        public Catalog(ITreeConfigNode parent, string name)
            : this(parent)
        {
            this.Name = name;
        }

        public Catalog(ITreeConfigNode parent, string name, List<Property> listProperties)
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

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListCatalogs).ListCatalogs.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Catalog)(this.Parent as GroupListCatalogs).ListCatalogs.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            (this.Parent as GroupListCatalogs).ListCatalogs.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListCatalogs).ListCatalogs.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Catalog)(this.Parent as GroupListCatalogs).ListCatalogs.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            (this.Parent as GroupListCatalogs).ListCatalogs.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = Catalog.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            (this.Parent as GroupListCatalogs).Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Catalog(this.Parent);
            (this.Parent as GroupListCatalogs).Add(node);
            this.GetUniqueName(Catalog.DefaultName, node, (this.Parent as GroupListCatalogs).ListCatalogs);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            var p = this.Parent as GroupListCatalogs;
            p.ListCatalogs.Remove(this);
        }
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
        public bool GetUseCodeProperty()
        {
            bool res = false;
            if (this.UseCodeProperty.HasValue)
            {
                res = this.UseCodeProperty.Value;
            }
            else
            {
                res = (this.Parent as GroupListCatalogs).UseCodeProperty;
            }
            return res;
        }
        public bool GetUseNameProperty()
        {
            bool res = false;
            if (this.UseNameProperty.HasValue)
            {
                res = this.UseNameProperty.Value;
            }
            else
            {
                res = (this.Parent as GroupListCatalogs).UseNameProperty;
            }
            return res;
        }
        public bool GetUseDescriptionProperty()
        {
            bool res = false;
            if (this.UseDescriptionProperty.HasValue)
            {
                res = this.UseDescriptionProperty.Value;
            }
            else
            {
                res = (this.Parent as GroupListCatalogs).UseDescriptionProperty;
            }
            return res;
        }
        public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen)
        {
            var res = new List<IProperty>();
            GetSpecialProperties(res, false);
            foreach (var t in this.GroupProperties.ListProperties)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    res.Add(t);
                }
            }
            return res;
        }
        public IReadOnlyList<IProperty> GetAllProperties()
        {
            var res = new List<IProperty>();
            GetSpecialProperties(res, false);
            foreach (var t in this.GroupProperties.ListProperties)
            {
                res.Add(t);
            }
            return res;
        }
        public List<IProperty> ListAllProperties
        {
            get
            {
                var res = new List<IProperty>();
                foreach(var t in this.GetAllProperties())
                {
                    res.Add(t);
                }
                return res;
            }
        }
        public SortedObservableCollection<IProperty> ListViewProperties
        {
            get
            {
                var res = new SortedObservableCollection<IProperty>();
                if (this.UseTree && this.UseSeparatePropertiesForGroups)
                {
                    foreach (var t in this.GetAllProperties())
                    {
                        foreach (var tt in this.ListGuidViewProperties)
                        {
                            if (tt == t.Guid)
                            {
                                res.Add(t);
                                break;
                            }
                        }
                    }
                }
                return res;
            }
        }
        public IReadOnlyList<IProperty> GetAllFolderProperties()
        {
            var res = new List<IProperty>();
            GetSpecialProperties(res, true);
            foreach (var t in this.Folder.GroupProperties.ListProperties)
            {
                res.Add(t);
            }
            return res;
        }
        public List<IProperty> ListAllFolderProperties
        {
            get
            {
                var res = new List<IProperty>();
                foreach (var t in this.GetAllFolderProperties())
                {
                    res.Add(t);
                }
                return res;
            }
        }
        public SortedObservableCollection<IProperty> ListViewFolderProperties
        {
            get
            {
                var res = new SortedObservableCollection<IProperty>();
                foreach (var t in this.GetAllFolderProperties())
                {
                    foreach (var tt in this.ListGuidViewFolderProperties)
                    {
                        if (tt == t.Guid)
                        {
                            res.Add(t);
                            break;
                        }
                    }
                }
                return res;
            }
        }

        private void GetSpecialProperties(List<IProperty> res, bool isFolder)
        {
            var cfg = this.GetConfig();
            var prp = cfg.Model.GetPropertyId(this.PropertyIdGuid);
            res.Add(prp);
            if (isFolder)
            {
                prp = cfg.Model.GetPropertyRefParent(this.Folder.PropertyRefSelfGuid, "RefTreeParent", true);
                res.Add(prp);
            }
            else
            {
                if (this.UseTree)
                {
                    if (this.UseSeparatePropertiesForGroups)
                    {
                        prp = cfg.Model.GetPropertyRefParent(this.PropertyRefFolderGuid, "Ref" + this.Folder.CompositeName);
                        res.Add(prp);
                    }
                    else
                    {
                        prp = cfg.Model.GetPropertyRefParent(this.PropertyRefSelfGuid, "RefTreeParent", true);
                        res.Add(prp);
                        if (this.UseFolderTypeExplicitly)
                        {
                            prp = cfg.Model.GetPropertyIsFolder(this.PropertyIsFolderGuid);
                            res.Add(prp);
                        }
                    }
                }
            }
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
                prp = cfg.Model.GetPropertyCatalogName(this.PropertyNameGuid, this.MaxNameLength);
                res.Add(prp);
            }
            if (this.GetUseDescriptionProperty())
            {
                prp = cfg.Model.GetPropertyCatalogDescription(this.PropertyDescriptionGuid, this.MaxDescriptionLength);
                res.Add(prp);
            }
        }
        public IReadOnlyList<IProperty> GetIncludedFolderProperties(string guidAppPrjGen)
        {
            var res = new List<IProperty>();
            GetSpecialProperties(res, true);
            foreach (var t in this.Folder.GroupProperties.ListProperties)
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
        public IReadOnlyList<IPropertiesTab> GetIncludedFolderPropertiesTabs(string guidAppPrjGen)
        {
            var res = new List<IPropertiesTab>();
            foreach (var t in this.Folder.GroupPropertiesTabs.ListPropertiesTabs)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    res.Add(t);
                }
            }
            return res;
        }
        partial void OnUseCodePropertyChanged()
        {
            HideProperties();
        }
        partial void OnUseNamePropertyChanged()
        {
            HideProperties();
        }
        partial void OnUseDescriptionPropertyChanged()
        {
            HideProperties();
        }
        partial void OnUseSeparatePropertiesForGroupsChanged()
        {
            this.RefillChildren();
        }
        partial void OnUseTreeChanged()
        {
            this.RefillChildren();
            HideProperties();
        }
        private void HideProperties()
        {
            var lst = new List<string>();
            if (!this.UseTree)
            {
                lst.Add(this.GetPropertyName(() => this.GroupIconType));
                lst.Add(this.GetPropertyName(() => this.MaxTreeLevels));
                lst.Add(this.GetPropertyName(() => this.UseSeparatePropertiesForGroups));
                lst.Add(this.GetPropertyName(() => this.UseFolderTypeExplicitly));
            }
            else
            {
                if (this.UseSeparatePropertiesForGroups)
                {
                    lst.Add(this.GetPropertyName(() => this.UseFolderTypeExplicitly));
                }
            }
            if (!this.GetUseCodeProperty())
            {
                lst.Add(this.GetPropertyName(() => this.CodePropertySettings));
            }
            if (!this.GetUseNameProperty())
            {
                lst.Add(this.GetPropertyName(() => this.MaxNameLength));
            }
            if (!this.GetUseDescriptionProperty())
            {
                lst.Add(this.GetPropertyName(() => this.MaxDescriptionLength));
            }
            if (lst.Count == 0)
            {
                this.AutoGenerateProperties = true;
            }
            else
            {
                this.HidePropertiesForXceedPropertyGrid(lst.ToArray());
            }
        }
    }
}
