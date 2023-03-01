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
    [DebuggerDisplay("Catalog:{Name,nq} props:{GroupProperties.ListProperties.Count,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class Catalog : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup,
        IDbTable, INodeWithProperties, IViewList, ITreeConfigNodeSortable
    {
        [BrowsableAttribute(false)]
        public GroupListCatalogs ParentGroupListCatalogs { get { Debug.Assert(this.Parent != null); return (GroupListCatalogs)this.Parent; } }
        [BrowsableAttribute(false)]
        public IGroupListCatalogs ParentGroupListCatalogsI { get { Debug.Assert(this.Parent != null); return (IGroupListCatalogs)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListCatalogs.Children;
        }
        #endregion ITree

        [Browsable(false)]
        new public string IconName { get { return "iconCatalogProperty"; } }
        //protected override string GetNodeIconName() { return "iconCatalogProperty"; }
        partial void OnCreated()
        {
            //this.ListGuidViewProperties = new ObservableCollectionWithActions<string>();
            //this.ListGuidViewFolderProperties = new ObservableCollectionWithActions<string>();
            this.IsIncludableInModels = true;
#if DEBUG
            // SubNodes.Add(this.GroupConstants, 1);
#endif
            //this.Folder.Parent = this;
            //this.GroupProperties.Parent = this;
            //this.GroupDetails.Parent = this;
            //this.GroupForms.Parent = this;
            //this.GroupReports.Parent = this;
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
        public void RefillChildren()
        {
            //if (this.Children.Count > 0)
            //    return;
            VmBindable.IsNotifyingStatic = false;
            var children = (ConfigNodesCollection<ITreeConfigNodeSortable>)this.Children;
            children.Clear();
            if (this.UseTree && this.UseSeparateTreeForFolders)
            {
                children.Add(this.Folder, 1);
            }
            children.Add(this.GroupProperties, 2);
            children.Add(this.GroupDetails, 3);
            children.Add(this.GroupForms, 4);
            children.Add(this.GroupReports, 5);
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
        public Detail AddDetails(string name)
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
            var node = Catalog.Clone(this.ParentGroupListCatalogs, this, true, true);
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
        public IReadOnlyList<IProperty> GetAllProperties(bool isUseRecordVersionField)
        {
            var res = new List<IProperty>();
            this.GetSpecialProperties(res, isUseRecordVersionField);
            this.GetNormalProperties(res);
            return res;
        }
        public IReadOnlyList<IProperty> GetAllFolderProperties(bool isUseRecordVersionField)
        {
            var res = new List<IProperty>();
            this.Folder.GetSpecialProperties(res, isUseRecordVersionField);
            this.Folder.GetNormalProperties(res);
            return res;
        }
        public void GetSpecialProperties(List<IProperty> res, bool isSupportVersion)
        {
            var model = this.ParentGroupListCatalogs.ParentModel;
            var prp = model.GetPropertyId(this.GroupProperties, this.PropertyIdGuid);
            res.Add(prp);
            if (this.UseTree)
            {
                if (this.UseSeparateTreeForFolders)
                {
                    prp = model.GetPropertyRefParent(this.GroupProperties, this.PropertyRefFolderGuid, "Ref" + this.Folder.CompositeName);
                    res.Add(prp);
                }
                else
                {
                    prp = model.GetPropertyRefParent(this.GroupProperties, this.PropertyRefSelfGuid, "RefTreeParent", true);
                    res.Add(prp);
                    prp = model.GetPropertyIsOpen(this.GroupProperties, this.PropertyIsOpenGuid);
                    res.Add(prp);
                    if (this.UseFolderTypeExplicitly)
                    {
                        prp = model.GetPropertyIsFolder(this.GroupProperties, this.PropertyIsFolderGuid);
                        res.Add(prp);
                    }
                }
            }
            if (isSupportVersion)
            {
                prp = model.GetPropertyVersion(this.GroupProperties, this.Folder.PropertyVersionGuid);
                res.Add(prp);
            }
        }
        public void GetNormalProperties(List<IProperty> res)
        {
            var model = this.ParentGroupListCatalogs.ParentModel;
            IProperty prp = null!;
            if (this.GetUseCodeProperty())
            {
                prp = this.GetCodeProperty(model.ParentConfig);
                res.Add(prp);
            }
            if (this.GetUseNameProperty())
            {
                prp = model.GetPropertyCatalogName(this.GroupProperties, this.PropertyNameGuid, this.MaxNameLength);
                res.Add(prp);
            }
            if (this.GetUseDescriptionProperty())
            {
                prp = model.GetPropertyCatalogDescription(this.GroupProperties, this.PropertyDescriptionGuid, this.MaxDescriptionLength);
                res.Add(prp);
            }
            foreach (var t in this.GroupProperties.ListProperties)
            {
                res.Add(t);
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
                    prp = cfg.Model.GetPropertyCatalogCodeInt(this.GroupProperties, this.PropertyCodeGuid, this.CodePropertySettings.Length);
                    break;
                case EnumCodeType.Text:
                    prp = cfg.Model.GetPropertyCatalogCode(this.GroupProperties, this.PropertyCodeGuid, this.CodePropertySettings.Length);
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
            this.NotifyPropertyChanged(() => this.Children);
            this.NotifyPropertyChanged(() => this.IsShowRefSelfTree);
            this.NotifyPropertyChanged(() => this.IsShowIsFolder);
        }
        partial void OnUseTreeChanged()
        {
            if (!this.UseTree)
            {
                this.UseSeparateTreeForFolders = false;
                this.UseFolderTypeExplicitly = false;
            }
            this.RefillChildren();
            this.NotifyPropertyChanged(() => this.Children);
            this.NotifyPropertyChanged(() => this.PropertyDefinitions);
            this.NotifyPropertyChanged(() => this.IsShowRefSelfTree);
            this.NotifyPropertyChanged(() => this.IsShowIsFolder);
        }
        [BrowsableAttribute(false)]
        public bool IsShowRefSelfTree { get { if (this.UseTree && !this.UseSeparateTreeForFolders) return true; return false; } }
        [BrowsableAttribute(false)]
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
            //else
            //{
            //    if (this.UseSeparateTreeForFolders)
            //    {
            //        lst.Add(this.GetPropertyName(() => this.UseFolderTypeExplicitly));
            //    }
            //}
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
        public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isSupportVersion)
        {
            var res = new List<IProperty>();
            this.GetSpecialProperties(res, isSupportVersion);
            var model = this.ParentGroupListCatalogs.ParentModel;
            IProperty prp = null!;
            if (this.GetUseCodeProperty())
            {
                prp = this.GetCodeProperty(model.ParentConfig);
                res.Add(prp);
            }
            if (this.GetUseNameProperty())
            {
                prp = model.GetPropertyCatalogName(this.GroupProperties, this.PropertyNameGuid, this.MaxNameLength);
                res.Add(prp);
            }
            if (this.GetUseDescriptionProperty())
            {
                prp = model.GetPropertyCatalogDescription(this.GroupProperties, this.PropertyDescriptionGuid, this.MaxDescriptionLength);
                res.Add(prp);
            }
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
            this.Folder.GetSpecialProperties(res, isSupportVersion);
            var model = this.ParentGroupListCatalogs.ParentModel;
            IProperty prp = null!;
            if (this.GetUseCodeProperty())
            {
                prp = this.GetCodeProperty(model.ParentConfig);
                res.Add(prp);
            }
            if (this.GetUseNameProperty())
            {
                prp = model.GetPropertyCatalogName(this.GroupProperties, this.PropertyNameGuid, this.MaxNameLength);
                res.Add(prp);
            }
            if (this.GetUseDescriptionProperty())
            {
                prp = model.GetPropertyCatalogDescription(this.GroupProperties, this.PropertyDescriptionGuid, this.MaxDescriptionLength);
                res.Add(prp);
            }
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
            var model = this.ParentGroupListCatalogs.ParentModel;
            Form? form = (from p in this.GroupForms.ListForms where p.EnumFormType == formType select p).SingleOrDefault();
            IProperty pId = model.GetPropertyId(this.GroupProperties, this.PropertyIdGuid);
            IProperty? pRefTreeParent = null;
            IProperty? pRefParent = null;
            IProperty? pIsFolder = null;
            if (this.UseTree)
            {
                pRefTreeParent = model.GetPropertyRefParent(this.GroupProperties, this.Folder.PropertyRefSelfGuid, "RefTreeParent", true);
                if (this.UseFolderTypeExplicitly)
                {
                    pIsFolder = model.GetPropertyIsFolder(this.GroupProperties, this.PropertyIsFolderGuid);
                }
                if (this.UseSeparateTreeForFolders) // self tree and separate data grid for children
                {
                    viewTreeData = new ViewTreeData(pId, pRefTreeParent, pIsFolder);
                    var lst = this.SelectViewProperties(formType, this.Folder.GroupProperties.ListProperties, form.ListGuidViewFolderProperties, guidAppPrjGen);
                    viewTreeData.ListViewProperties.AddRange(lst);

                    viewListData = new ViewListData(pId, pRefParent, pIsFolder);
                    lst = this.SelectViewProperties(formType, this.GroupProperties.ListProperties, form.ListGuidViewProperties, guidAppPrjGen);
                    viewListData.ListViewProperties.AddRange(lst);
                }
                else // only self tree
                {
                    viewTreeData = new ViewTreeData(pId, pRefParent, pIsFolder);
                    var lst = this.SelectViewProperties(formType, this.Folder.GroupProperties.ListProperties, form.ListGuidViewFolderProperties, guidAppPrjGen);
                    viewTreeData.ListViewProperties.AddRange(lst);
                }
            }
            else // only data grid for children
            {
                viewListData = new ViewListData(pId);
                var lst = this.SelectViewProperties(formType, this.GroupProperties.ListProperties, form.ListGuidViewProperties, guidAppPrjGen);
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
                    var prp = Cfg.Model.GetPropertyCatalogName(this.GroupProperties, this.PropertyNameGuid, this.MaxNameLength);
                    res.Add(prp);
                }
                if (formType == FormType.ListWide)
                {
                    if (!this.GetUseDescriptionProperty())
                    {
                        var prp = Cfg.Model.GetPropertyCatalogDescription(this.GroupProperties, this.PropertyDescriptionGuid, this.MaxDescriptionLength);
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
            if (this.ParentGroupListCatalogs.UseCodeProperty == EnumUseType.Yes)
                return true;
            if (this.ParentGroupListCatalogs.UseCodeProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListCatalogs.ParentModel.UseCodeProperty;
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
            if (this.ParentGroupListCatalogs.UseNameProperty == EnumUseType.Yes)
                return true;
            if (this.ParentGroupListCatalogs.UseNameProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListCatalogs.ParentModel.UseNameProperty;
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
            if (this.ParentGroupListCatalogs.UseDescriptionProperty == EnumUseType.Yes)
                return true;
            if (this.ParentGroupListCatalogs.UseDescriptionProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListCatalogs.ParentModel.UseDescriptionProperty;
        }
        public bool GetUseDescriptionPropertSeparateFoldery()
        {
            if (this.Folder.UseDescriptionProperty == EnumUseType.Yes)
                return true;
            if (this.Folder.UseDescriptionProperty == EnumUseType.No)
                return false;
            if (this.ParentGroupListCatalogs.UseDescriptionProperty == EnumUseType.Yes)
                return true;
            if (this.ParentGroupListCatalogs.UseDescriptionProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListCatalogs.UseDescriptionPropertyInSeparateTree;
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
            if (this.GetUseNameProperty())
            {
                res.Add(this.GetForm(FormType.ListNarrow));
                if (this.GetUseDescriptionProperty())
                {
                    res.Add(this.GetForm(FormType.ListWide));
                }
            }
            return res;
        }
        //public IForm GetForm(FormType formType)
        //{
        //    var res = (from p in this.GroupForms.ListForms where p.EnumFormType == formType select p).SingleOrDefault();
        //    if (res == null)
        //    {
        //        res = new Form(this.GroupForms);
        //        switch (formType)
        //        {
        //            case FormType.ViewListWide:
        //            case FormType.ViewListNarrow:
        //                if (this.GetUseCodeProperty())
        //                    res.ListGuidViewProperties.Add(this.PropertyCodeGuid);
        //                if (this.GetUseNameProperty())
        //                    res.ListGuidViewProperties.Add(this.PropertyNameGuid);
        //                if (formType == FormType.ViewListWide)
        //                {
        //                    if (this.GetUseDescriptionProperty())
        //                        res.ListGuidViewProperties.Add(this.PropertyDescriptionGuid);
        //                }
        //                res.ListGuidViewProperties.Add(this.PropertyIdGuid);
        //                res.ListGuidViewProperties.Add(this.PropertyVersionGuid);
        //                if (this.UseTree && !this.UseSeparateTreeForFolders)
        //                {
        //                    res.ListGuidViewProperties.Add(this.PropertyRefSelfGuid);
        //                    res.ListGuidViewProperties.Add(this.PropertyIsOpenGuid);
        //                    if (this.UseFolderTypeExplicitly)
        //                    {
        //                        res.ListGuidViewProperties.Add(this.PropertyIsFolderGuid);
        //                    }
        //                }
        //                if (this.UseTree && this.UseSeparateTreeForFolders)
        //                {
        //                    res.ListGuidViewProperties.Add(this.PropertyRefFolderGuid);
        //                    if (this.GetUseCodeProperty())
        //                        res.ListGuidViewFolderProperties.Add(this.PropertyCodeGuid);
        //                    if (this.GetUseNameProperty())
        //                        res.ListGuidViewFolderProperties.Add(this.PropertyNameGuid);
        //                    if (formType == FormType.ViewListWide)
        //                    {
        //                        if (this.GetUseDescriptionProperty())
        //                            res.ListGuidViewFolderProperties.Add(this.PropertyDescriptionGuid);
        //                    }
        //                    res.ListGuidViewFolderProperties.Add(this.PropertyIdGuid);
        //                    res.ListGuidViewFolderProperties.Add(this.PropertyRefSelfGuid);
        //                    res.ListGuidViewFolderProperties.Add(this.PropertyIsOpenGuid);
        //                    res.ListGuidViewFolderProperties.Add(this.PropertyVersionGuid);
        //                }
        //                break;
        //            //case FormType.FolderEditForm:
        //            //    break;
        //            //case FormType.ItemEditForm:
        //            //    break;
        //            default:
        //                throw new NotImplementedException();
        //        }
        //    }
        //    return res;
        //}
    }
}
