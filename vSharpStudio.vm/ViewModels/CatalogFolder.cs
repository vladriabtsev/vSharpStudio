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
    [DebuggerDisplay("Grouping:{Name,nq} props:{GroupProperties.ListProperties.Count,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class CatalogFolder : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNodeGroup, IDbTable, INodeWithProperties
    {
        [Browsable(false)]
        public Catalog ParentCatalog { get { Debug.Assert(this.Parent != null); return (Catalog)this.Parent; } }
        [Browsable(false)]
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
        public IForm GetForm(FormType ftype, string guidAppPrjGen)
        {
            var f = (from tf in this.GroupForms.ListForms where tf.EnumFormType == ftype select tf).SingleOrDefault();
            if (f == null)
            {
                var lstp = new List<IProperty>();
                int i = 0;
                foreach (var t in this.GroupProperties.ListProperties)
                {
                    if (t.IsIncluded(guidAppPrjGen))
                    {
                        i++;
                        if (i > 1)
                            break;
                        lstp.Add(t);
                    }
                }
                this.GetSpecialProperties(lstp, false);
                f = new Form(this.GroupForms, ftype, lstp);
            }
            else
            {
                var lstp = new List<IProperty>();
                lstp.AddRange(f.ListAllNotSpecialProperties);
                this.GetSpecialProperties(lstp, false);
                f = new Form(this.GroupForms, ftype, lstp);
            }
            return f;
        }
        public IReadOnlyList<IForm> GetListForms(string guidAppPrjGen)
        {
            var res = new List<IForm>();
            res.Add(this.GetForm(FormType.ListNarrow, guidAppPrjGen));
            res.Add(this.GetForm(FormType.ListWide, guidAppPrjGen));
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
            if (this.ParentCatalog.UseTree && !this.ParentCatalog.UseSeparateTreeForFolders)
            {
                prp = model.GetPropertyIsFolder(this.GroupProperties, this.PropertyIsFolderGuid);
                res.Add(prp);
            }
            if (isSupportVersion)
            {
                prp = model.GetPropertyVersion(this.GroupProperties, this.PropertyVersionGuid);
                res.Add(prp);
            }
        }
        public void GetNormalProperties(List<IProperty> res)
        {
            var model = this.ParentCatalog.ParentGroupListCatalogs.ParentModel;
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

        #region Roles
        internal Dictionary<string, RoleCatalogAccess> dicCatalogAccess = new Dictionary<string, RoleCatalogAccess>();
        public void InitRoles()
        {
            foreach (var tt in this.ListRoleCatalogAccessSettings)
            {
                this.dicCatalogAccess[tt.Guid] = tt;
            }
        }
        public void InitRoleAdd(Role role)
        {
            var rca = new RoleCatalogAccess() { Guid = role.Guid };
            this.ListRoleCatalogAccessSettings.Add(rca);
            this.dicCatalogAccess[rca.Guid] = rca;
        }
        public void InitRoleRemove(Role role)
        {
            for (int i = 0; i < this.ListRoleCatalogAccessSettings.Count; i++)
            {
                if (this.ListRoleCatalogAccessSettings[i].Guid == role.Guid)
                {
                    this.ListRoleCatalogAccessSettings.RemoveAt(i);
                    break;
                }
            }
            this.dicCatalogAccess.Remove(role.Guid);
        }
        public EnumPropertyAccess GetRolePropertyAccess(string roleGuid)
        {
            EnumCatalogDetailAccess r = EnumCatalogDetailAccess.C_BY_PARENT;
            if (!this.dicCatalogAccess.TryGetValue(roleGuid, out var rr) || rr.EditAccess == EnumCatalogDetailAccess.C_BY_PARENT)
            {
                r = this.ParentCatalog.GetRoleCatalogAccess(roleGuid);
            }
            Debug.Assert(r != EnumCatalogDetailAccess.C_BY_PARENT);
            switch (r)
            {
                case EnumCatalogDetailAccess.C_HIDE:
                    return EnumPropertyAccess.P_HIDE;
                case EnumCatalogDetailAccess.C_VIEW:
                    return EnumPropertyAccess.P_VIEW;
                case EnumCatalogDetailAccess.C_EDIT:
                case EnumCatalogDetailAccess.C_MARK_DEL:
                    return EnumPropertyAccess.P_EDIT;
                default:
                    throw new NotImplementedException();
            }
        }
        public EnumPrintAccess GetRolePropertyPrint(string roleGuid)
        {
            EnumPrintAccess r = EnumPrintAccess.PR_BY_PARENT;
            if (!this.dicCatalogAccess.TryGetValue(roleGuid, out var rr) || rr.PrintAccess == EnumPrintAccess.PR_BY_PARENT)
            {
                r = this.ParentCatalog.GetRoleCatalogPrint(roleGuid);
            }
            Debug.Assert(r != EnumPrintAccess.PR_BY_PARENT);
            return r;
        }
        public EnumCatalogDetailAccess GetRoleCatalogAccess(string roleGuid)
        {
            if (this.dicCatalogAccess.TryGetValue(roleGuid, out var r) && r.EditAccess != EnumCatalogDetailAccess.C_BY_PARENT)
                return r.EditAccess;
            return this.ParentCatalog.GetRoleCatalogAccess(roleGuid);
        }
        public EnumPrintAccess GetRoleCatalogPrint(string roleGuid)
        {
            if (this.dicCatalogAccess.TryGetValue(roleGuid, out var r) && r.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                return r.PrintAccess;
            return this.ParentCatalog.GetRoleCatalogPrint(roleGuid);
        }
        public IReadOnlyList<string> GetRolesByAccess(EnumCatalogDetailAccess access)
        {
            var roles = new List<string>();
            foreach (var role in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (GetRoleCatalogAccess(role.Guid) == access)
                    roles.Add(role.Name);
            }
            return roles;
        }
        public IReadOnlyList<string> GetRolesByAccess(EnumPrintAccess access)
        {
            var roles = new List<string>();
            foreach (var role in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (GetRoleCatalogPrint(role.Guid) == access)
                    roles.Add(role.Name);
            }
            return roles;
        }
        #endregion Roles
    }
}
