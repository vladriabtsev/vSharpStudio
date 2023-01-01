using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Text;
using System.Linq;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Grouping:{Name,nq} props:{GroupProperties.ListProperties.Count,nq} HasChanged:{IsHasChanged}")]
    public partial class CatalogFolder : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNodeGroup, IDbTable, INodeWithProperties
    {
        [BrowsableAttribute(false)]
        public Catalog ParentCatalog { get { Debug.Assert(this.Parent != null); return (Catalog)this.Parent; } }
        [BrowsableAttribute(false)]
        public ICatalog ParentCatalogI { get { Debug.Assert(this.Parent != null); return (ICatalog)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentCatalog.Children;
        }
        #endregion ITree

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
            this.PropertyIsFolderGuid = System.Guid.NewGuid().ToString();
            this.PropertyIsOpenGuid = System.Guid.NewGuid().ToString();
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
            if (this.Children.Count > 0)
                return;
            VmBindable.IsNotifyingStatic = false;
            var children = (ConfigNodesCollection<ITreeConfigNodeSortable>)this.Children;
            children.Add(this.GroupProperties, 2);
            children.Add(this.GroupDetails, 3);
            children.Add(this.GroupForms, 4);
            children.Add(this.GroupReports, 5);
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
        public Property AddPropertyEnumeration(string name, Enumeration en, bool isNullable)
        {
            var node = new Property(this) { Name = name };
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.ENUMERATION, ObjectGuid = en.Guid };
            node.IsNullable = isNullable;
            this.NodeAddNewSubNode(node);
            return node;
        }

        #region Tree operations
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
        public void GetSpecialProperties(List<IProperty> res, bool isSupportVersion)
        {
            var model = this.ParentCatalog.ParentGroupListCatalogs.ParentModel;
            var prp = model.GetPropertyId(this.GroupProperties, this.PropertyIdGuid);
            res.Add(prp);
            prp = model.GetPropertyRefParent(this.GroupProperties, this.PropertyRefSelfGuid, "RefTreeParent", true);
            res.Add(prp);
            prp = model.GetPropertyIsOpen(this.GroupProperties, this.PropertyIsOpenGuid);
            res.Add(prp);
            if (this.ParentCatalog.UseFolderTypeExplicitly)
            {
                prp = model.GetPropertyIsFolder(this.GroupProperties, this.PropertyIsFolderGuid);
                res.Add(prp);
            }
            if (isSupportVersion)
            {
                prp = model.GetPropertyVersion(this.GroupProperties, this.PropertyVersionGuid);
                res.Add(prp);
            }
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
        }
        //private void GetSpecialProperties(List<IProperty> res)
        //{
        //    var ctlg = this.ParentCatalog;
        //    var model = ctlg.ParentGroupListCatalogs.ParentModel;
        //    var prp = model.GetPropertyId(this.GroupProperties, this.PropertyIdGuid);
        //    res.Add(prp);
        //    prp = model.GetPropertyRefParent(this.GroupProperties, this.PropertyRefSelfGuid, "RefTreeParent", true);
        //    (prp as Property)!.IsNullable = true;
        //    res.Add(prp);
        //    if (this.GetUseCodeProperty())
        //    {
        //        switch (this.CodePropertySettings.Type)
        //        {
        //            case EnumCodeType.AutoNumber:
        //                throw new NotImplementedException();
        //            case EnumCodeType.AutoText:
        //                throw new NotImplementedException();
        //            case EnumCodeType.Number:
        //                prp = model.GetPropertyCatalogCodeInt(this.GroupProperties, this.PropertyCodeGuid, this.CodePropertySettings.Length);
        //                break;
        //            case EnumCodeType.Text:
        //                prp = model.GetPropertyCatalogCode(this.GroupProperties, this.PropertyCodeGuid, this.CodePropertySettings.Length);
        //                break;
        //        }
        //        res.Add(prp);
        //    }
        //    if (this.GetUseNameProperty())
        //    {
        //        prp = model.GetPropertyCatalogName(this.GroupProperties, ctlg.PropertyNameGuid, ctlg.MaxNameLength);
        //        res.Add(prp);
        //    }
        //    if (this.GetUseDescriptionProperty())
        //    {
        //        prp = model.GetPropertyCatalogDescription(this.GroupProperties, ctlg.PropertyDescriptionGuid, ctlg.MaxDescriptionLength);
        //        res.Add(prp);
        //    }
        //}
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
