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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Catalog:{Name,nq} props:{GroupProperties.ListProperties.Count,nq}")]
    public partial class Catalog : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup,
        IDbTable, ITreeConfigNode, INodeWithProperties, IViewList
    {
        [BrowsableAttribute(false)]
        public GroupListCatalogs ParentGroupListCatalogs { get { return (GroupListCatalogs)this.Parent; } }
        [BrowsableAttribute(false)]
        public IGroupListCatalogs ParentGroupListCatalogsI { get { return (IGroupListCatalogs)this.Parent; } }

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            return this.ParentGroupListCatalogs.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ObservableCollection<ITreeConfigNode> Children { get; private set; }
        [Browsable(false)]
        new public string IconName { get { return "iconCatalogProperty"; } }
        //protected override string GetNodeIconName() { return "iconCatalogProperty"; }
        partial void OnCreated()
        {
            //this.ListGuidViewProperties = new ObservableCollectionWithActions<string>();
            //this.ListGuidViewFolderProperties = new ObservableCollectionWithActions<string>();
            this.IsIncludableInModels = true;
            this.Children = new ObservableCollection<ITreeConfigNode>();
#if DEBUG
            // SubNodes.Add(this.GroupConstants, 1);
#endif
            this.Folder.Parent = this;
            this.GroupProperties.Parent = this;
            this.GroupDetails.Parent = this;
            this.GroupForms.Parent = this;
            this.GroupReports.Parent = this;
            this.ItemIconType = EnumCatalogTreeIcon.None;

            this.PropertyIdGuid = System.Guid.NewGuid().ToString();
            this.PropertyCodeGuid = System.Guid.NewGuid().ToString();
            this.PropertyNameGuid = System.Guid.NewGuid().ToString();
            this.PropertyDescriptionGuid = System.Guid.NewGuid().ToString();
            this.PropertyRefFolderGuid = System.Guid.NewGuid().ToString();
            this.PropertyRefSelfGuid = System.Guid.NewGuid().ToString();
            this.PropertyIsFolderGuid = System.Guid.NewGuid().ToString();
            this.PropertyIsOpenGuid = System.Guid.NewGuid().ToString();
            this.PropertyVersionGuid = System.Guid.NewGuid().ToString();
            this.ViewListWideGuid = System.Guid.NewGuid().ToString();
            this.ViewListNarrowGuid = System.Guid.NewGuid().ToString();

            this.MaxNameLength = 20;
            this.MaxDescriptionLength = 100;
            this.UseTree = false;
            this.MaxTreeLevels = 2;
            this.UseSeparateTreeForFolders = false;
            this.GroupIconType = EnumCatalogTreeIcon.Folder;
            this.UseCodeProperty = EnumUseType.Default;
            this.UseNameProperty = EnumUseType.Default;
            this.UseDescriptionProperty = EnumUseType.Default;
            this.RefillChildren();
        }
        protected override void OnInitFromDto()
        {
            base.OnInitFromDto();
            this.RefillChildren();
        }
        public void RefillChildren()
        {
            VmBindable.IsNotifyingStatic = false;
            this.Children.Clear();
            if (this.UseTree && this.UseSeparateTreeForFolders)
            {
                this.Children.Add(this.Folder);
            }
            this.Children.Add(this.GroupProperties);
            this.Children.Add(this.GroupDetails);
            this.Children.Add(this.GroupForms);
            this.Children.Add(this.GroupReports);
            this.CodePropertySettings.Parent = this;
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

        public Catalog(ITreeConfigNode parent, string name)
            : this(parent)
        {
            this.Name = name;
        }

        public Catalog(ITreeConfigNode parent, string name, List<Property> listProperties)
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

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListCatalogs.ListCatalogs.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Catalog)this.ParentGroupListCatalogs.ListCatalogs.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupListCatalogs.ListCatalogs.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListCatalogs.ListCatalogs.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Catalog)this.ParentGroupListCatalogs.ListCatalogs.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupListCatalogs.ListCatalogs.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = Catalog.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupListCatalogs.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Catalog(this.Parent);
            this.ParentGroupListCatalogs.Add(node);
            this.GetUniqueName(Defaults.CatalogName, node, this.ParentGroupListCatalogs.ListCatalogs);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListCatalogs.ListCatalogs.Remove(this);
        }
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
        public IReadOnlyList<IProperty> GetAllProperties(bool isUseRecordVersionField)
        {
            var res = new List<IProperty>();
            GetSpecialProperties(res, false, true, isUseRecordVersionField);
            foreach (var t in this.GroupProperties.ListProperties)
            {
                res.Add(t);
            }
            return res;
        }
        public IReadOnlyList<IProperty> GetAllFolderProperties(bool isUseRecordVersionField)
        {
            var res = new List<IProperty>();
            GetSpecialProperties(res, true, true, isUseRecordVersionField);
            foreach (var t in this.Folder.GroupProperties.ListProperties)
            {
                res.Add(t);
            }
            return res;
        }
        public void GetSpecialProperties(List<IProperty> res, bool isFolder, bool isAll, bool isSupportVersion)
        {
            var cfg = this.GetConfig();
            var prp = cfg.Model.GetPropertyId(this.PropertyIdGuid);
            if (isAll)
            {
                res.Add(prp);
            }
            if (isFolder)
            {
                if (isAll)
                {
                    prp = cfg.Model.GetPropertyRefParent(this.Folder.PropertyRefSelfGuid, "RefTreeParent", true);
                    res.Add(prp);
                    prp = cfg.Model.GetPropertyIsOpen(this.PropertyIsOpenGuid);
                    res.Add(prp);
                }
                if (this.UseFolderTypeExplicitly)
                {
                    if (isAll)
                    {
                        prp = cfg.Model.GetPropertyIsFolder(this.PropertyIsFolderGuid);
                        res.Add(prp);
                    }
                }
                if (isSupportVersion)
                {
                    prp = cfg.Model.GetPropertyVersion(this.Folder.PropertyVersionGuid);
                    res.Add(prp);
                }
            }
            else
            {
                if (this.UseTree)
                {
                    if (this.UseSeparateTreeForFolders)
                    {
                        if (isAll)
                        {
                            prp = cfg.Model.GetPropertyRefParent(this.PropertyRefFolderGuid, "Ref" + this.Folder.CompositeName);
                            res.Add(prp);
                        }
                    }
                    else
                    {
                        if (isAll)
                        {
                            prp = cfg.Model.GetPropertyRefParent(this.PropertyRefSelfGuid, "RefTreeParent", true);
                            res.Add(prp);
                            prp = cfg.Model.GetPropertyIsOpen(this.PropertyIsOpenGuid);
                            res.Add(prp);
                        }
                        if (this.UseFolderTypeExplicitly)
                        {
                            if (isAll)
                            {
                                prp = cfg.Model.GetPropertyIsFolder(this.PropertyIsFolderGuid);
                                res.Add(prp);
                            }
                        }
                    }
                }
                if (isSupportVersion)
                {
                    prp = cfg.Model.GetPropertyVersion(this.PropertyVersionGuid);
                    res.Add(prp);
                }
            }
            if (this.GetUseCodeProperty())
            {
                prp = this.GetCodeProperty(cfg);
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
        private IProperty GetCodeProperty(IConfig cfg)
        {
            IProperty prp = null!;
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
            return prp;
        }

        partial void OnUseCodePropertyChanged()
        {
            this.NotifyPropertyChanged(() => this.PropertyDefinitions);
        }
        partial void OnUseNamePropertyChanged()
        {
            this.NotifyPropertyChanged(() => this.PropertyDefinitions);
        }
        partial void OnUseDescriptionPropertyChanged()
        {
            this.NotifyPropertyChanged(() => this.PropertyDefinitions);
        }
        partial void OnUseFolderTypeExplicitlyChanged()
        {
            this.NotifyPropertyChanged(() => this.IsShowIsFolder);
        }
        partial void OnUseSeparateTreeForFoldersChanged()
        {
            this.RefillChildren();
            this.NotifyPropertyChanged(() => this.IsShowRefSelfTree);
            this.NotifyPropertyChanged(() => this.IsShowIsFolder);
        }
        partial void OnUseTreeChanged()
        {
            this.RefillChildren();
            if (!this.UseTree)
            {
                this.UseSeparateTreeForFolders = false;
                this.UseFolderTypeExplicitly = false;
            }
            this.NotifyPropertyChanged(() => this.PropertyDefinitions);
            this.NotifyPropertyChanged(() => this.IsShowRefSelfTree);
            this.NotifyPropertyChanged(() => this.IsShowIsFolder);
        }
        public bool IsShowRefSelfTree { get { if (this.UseTree && !this.UseSeparateTreeForFolders) return true; return false; } }
        public bool IsShowIsFolder { get { if (this.UseTree && !this.UseSeparateTreeForFolders && this.UseFolderTypeExplicitly) return true; return false; } }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            if (!this.UseTree)
            {
                lst.Add(this.GetPropertyName(() => this.GroupIconType));
                lst.Add(this.GetPropertyName(() => this.MaxTreeLevels));
                lst.Add(this.GetPropertyName(() => this.UseSeparateTreeForFolders));
                lst.Add(this.GetPropertyName(() => this.UseFolderTypeExplicitly));
            }
            else
            {
                if (this.UseSeparateTreeForFolders)
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
            return lst.ToArray();
        }
        //public IReadOnlyList<IProperty> GetIncludedViewProperties(string guidAppPrjDbGen)
        //{
        //    var res = new List<IProperty>();
        //    GetSpecialProperties(res, false, true, false);
        //    foreach (var t in this.GroupProperties.ListProperties)
        //    {
        //        if (t.IsIncluded(guidAppPrjDbGen))
        //        {
        //            foreach (var tt in this.ListGuidViewProperties)
        //            {
        //                if (tt == t.Guid)
        //                {
        //                    res.Add(t);
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    return res;
        //}
        //public IReadOnlyList<IProperty> GetIncludedFolderViewProperties(string guidAppPrjDbGen)
        //{
        //    var res = new List<IProperty>();
        //    GetSpecialProperties(res, true, true, false);
        //    foreach (var t in this.Folder.GroupProperties.ListProperties)
        //    {
        //        if (t.IsIncluded(guidAppPrjDbGen))
        //        {
        //            foreach (var tt in this.ListGuidViewFolderProperties)
        //            {
        //                if (tt == t.Guid)
        //                {
        //                    res.Add(t);
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    return res;
        //}
        public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isSupportVersion)
        {
            var res = new List<IProperty>();
            GetSpecialProperties(res, false, true, isSupportVersion);
            foreach (var t in this.GroupProperties.ListProperties)
            {
                if (t.IsIncluded(guidAppPrjDbGen))
                {
                    res.Add(t);
                }
            }
            return res;
        }
        public IReadOnlyList<IProperty> GetIncludedFolderProperties(string guidAppPrjDbGen, bool isSupportVersion)
        {
            var res = new List<IProperty>();
            GetSpecialProperties(res, true, true, isSupportVersion);
            foreach (var t in this.Folder.GroupProperties.ListProperties)
            {
                if (t.IsIncluded(guidAppPrjDbGen))
                {
                    res.Add(t);
                }
            }
            return res;
        }
        public IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjDbGen)
        {
            var res = new List<IDetail>();
            foreach (var t in this.GroupDetails.ListDetails)
            {
                if (t.IsIncluded(guidAppPrjDbGen))
                {
                    res.Add(t);
                }
            }
            return res;
        }
        public IReadOnlyList<IDetail> GetIncludedFolderDetails(string guidAppPrjDbGen)
        {
            var res = new List<IDetail>();
            foreach (var t in this.Folder.GroupDetails.ListDetails)
            {
                if (t.IsIncluded(guidAppPrjDbGen))
                {
                    res.Add(t);
                }
            }
            return res;
        }
        public ViewFormData GetFormViewData(FormType formType, string guidAppPrjGen)
        {
            ViewTreeData? viewTreeData = null;
            ViewListData? viewListData = null;
            var cfg = this.GetConfig();
            Form? form = (from p in this.GroupForms.ListForms where p.EnumFormType == formType select p).SingleOrDefault();
            IProperty pId = cfg.Model.GetPropertyId(this.PropertyIdGuid);
            IProperty? pRefTreeParent = null;
            IProperty? pRefParent = null;
            IProperty? pIsFolder = null;
            if (this.UseTree)
            {
                pRefTreeParent = cfg.Model.GetPropertyRefParent(this.Folder.PropertyRefSelfGuid, "RefTreeParent", true);
                if (this.UseFolderTypeExplicitly)
                {
                    pIsFolder = cfg.Model.GetPropertyIsFolder(this.PropertyIsFolderGuid);
                }
                if (this.UseSeparateTreeForFolders) // self tree and separate data grid for children
                {
                    viewTreeData = new ViewTreeData(pId, pRefTreeParent, pIsFolder);
                    var lst = SelectViewProperties(formType, this.Folder.GroupProperties.ListProperties, form.ListGuidViewFolderProperties, guidAppPrjGen);
                    viewTreeData.ListViewProperties.AddRange(lst);

                    viewListData = new ViewListData(pId, pRefParent, pIsFolder);
                    lst = SelectViewProperties(formType, this.GroupProperties.ListProperties, form.ListGuidViewProperties, guidAppPrjGen);
                    viewListData.ListViewProperties.AddRange(lst);
                }
                else // only self tree
                {
                    viewTreeData = new ViewTreeData(pId, pRefParent, pIsFolder);
                    var lst = SelectViewProperties(formType, this.Folder.GroupProperties.ListProperties, form.ListGuidViewFolderProperties, guidAppPrjGen);
                    viewTreeData.ListViewProperties.AddRange(lst);
                }
            }
            else // only data grid for children
            {
                viewListData = new ViewListData(pId);
                var lst = SelectViewProperties(formType, this.GroupProperties.ListProperties, form.ListGuidViewProperties, guidAppPrjGen);
                viewListData.ListViewProperties.AddRange(lst);
            }
            return new ViewFormData(viewTreeData, viewListData);
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
                if (!this.GetUseCodeProperty())
                {
                    var prp = this.GetCodeProperty(this.Cfg);
                    res.Add(prp);
                }
                if (!this.GetUseNameProperty())
                {
                    var prp = Cfg.Model.GetPropertyCatalogName(this.PropertyNameGuid, this.MaxNameLength);
                    res.Add(prp);
                }
                if (formType == FormType.ViewListWide)
                {
                    if (!this.GetUseDescriptionProperty())
                    {
                        var prp = Cfg.Model.GetPropertyCatalogDescription(this.PropertyDescriptionGuid, this.MaxDescriptionLength);
                        res.Add(prp);
                    }
                }
            }
            return res;
        }
        public bool IsGridSortableGet()
        {
            if (this.IsGridSortable == EnumUseType.Yes)
                return true;
            if (this.IsGridSortable == EnumUseType.No)
                return false;
            return this.ParentGroupListCatalogs.GetIsGridSortable();
        }
        public bool IsGridFilterableGet()
        {
            if (this.IsGridFilterable == EnumUseType.Yes)
                return true;
            if (this.IsGridFilterable == EnumUseType.No)
                return false;
            return this.ParentGroupListCatalogs.GetIsGridFilterable();
        }
        public bool IsGridSortableCustomGet()
        {
            if (this.IsGridSortableCustom == EnumUseType.Yes)
                return true;
            if (this.IsGridSortableCustom == EnumUseType.No)
                return false;
            return this.ParentGroupListCatalogs.GetIsGridSortableCustom();
        }
        public bool GetUseCodeProperty()
        {
            if (this.UseCodeProperty == EnumUseType.Yes)
                return true;
            if (this.UseCodeProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListCatalogs.GetUseCodeProperty();
        }
        public bool GetUseCodePropertySeparateFolder()
        {
            if (this.Folder.UseCodeProperty == EnumUseType.Yes)
                return true;
            if (this.Folder.UseCodeProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListCatalogs.UseCodePropertyInSeparateTree;
        }
        public bool GetUseNameProperty()
        {
            if (this.UseNameProperty == EnumUseType.Yes)
                return true;
            if (this.UseNameProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListCatalogs.GetUseNameProperty();
        }
        public bool GetUseNamePropertySeparateFolder()
        {
            if (this.Folder.UseNameProperty == EnumUseType.Yes)
                return true;
            if (this.Folder.UseNameProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListCatalogs.UseNamePropertyInSeparateTree;
        }
        public bool GetUseDescriptionProperty()
        {
            if (this.UseDescriptionProperty == EnumUseType.Yes)
                return true;
            if (this.UseDescriptionProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListCatalogs.GetUseDescriptionProperty();
        }
        public bool GetUseDescriptionPropertSeparateFoldery()
        {
            if (this.Folder.UseDescriptionProperty == EnumUseType.Yes)
                return true;
            if (this.Folder.UseDescriptionProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListCatalogs.UseDescriptionPropertyInSeparateTree;
        }
        public IForm GetForm(FormType formType)
        {
            var res = (from p in this.GroupForms.ListForms where p.EnumFormType == formType select p).SingleOrDefault();
            if (res == null)
            {
                res = new Form(this.GroupForms);
                switch (formType)
                {
                    case FormType.ViewListWide:
                    case FormType.ViewListNarrow:
                        if (this.GetUseCodeProperty())
                            res.ListGuidViewProperties.Add(this.PropertyCodeGuid);
                        if (this.GetUseNameProperty())
                            res.ListGuidViewProperties.Add(this.PropertyNameGuid);
                        if (formType == FormType.ViewListWide)
                        {
                            if (this.GetUseDescriptionProperty())
                                res.ListGuidViewProperties.Add(this.PropertyDescriptionGuid);
                        }
                        res.ListGuidViewProperties.Add(this.PropertyIdGuid);
                        res.ListGuidViewProperties.Add(this.PropertyVersionGuid);
                        if (this.UseTree && !this.UseSeparateTreeForFolders)
                        {
                            res.ListGuidViewProperties.Add(this.PropertyRefSelfGuid);
                            res.ListGuidViewProperties.Add(this.PropertyIsOpenGuid);
                            if (this.UseFolderTypeExplicitly)
                            {
                                res.ListGuidViewProperties.Add(this.PropertyIsFolderGuid);
                            }
                        }
                        if (this.UseTree && this.UseSeparateTreeForFolders)
                        {
                            res.ListGuidViewProperties.Add(this.PropertyRefFolderGuid);
                            if (this.GetUseCodeProperty())
                                res.ListGuidViewFolderProperties.Add(this.PropertyCodeGuid);
                            if (this.GetUseNameProperty())
                                res.ListGuidViewFolderProperties.Add(this.PropertyNameGuid);
                            if (formType == FormType.ViewListWide)
                            {
                                if (this.GetUseDescriptionProperty())
                                    res.ListGuidViewFolderProperties.Add(this.PropertyDescriptionGuid);
                            }
                            res.ListGuidViewFolderProperties.Add(this.PropertyIdGuid);
                            res.ListGuidViewFolderProperties.Add(this.PropertyRefSelfGuid);
                            res.ListGuidViewFolderProperties.Add(this.PropertyIsOpenGuid);
                            res.ListGuidViewFolderProperties.Add(this.PropertyVersionGuid);
                        }
                        break;
                    //case FormType.FolderEditForm:
                    //    break;
                    //case FormType.ItemEditForm:
                    //    break;
                    default:
                        throw new NotImplementedException();
                }
            }
            return res;
        }
    }
}
