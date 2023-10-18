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
using vSharpStudio.common.DiffModel;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class CatalogFolder : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNodeGroup, IDbTable, INodeWithProperties, IRoleAccess, ICatalogDetailAccessRoles
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" props:{GroupProperties.ListProperties.Count}";
        }
        [Browsable(false)]
        public Catalog ParentCatalog { get { Debug.Assert(this.Parent != null); return (Catalog)this.Parent; } }
        [Browsable(false)]
        public ICatalog ParentCatalogI { get { Debug.Assert(this.Parent != null); return (ICatalog)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            Debug.Assert(this.Children != null);
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            Debug.Assert(this.ParentCatalog.Children != null);
            return this.ParentCatalog.Children;
        }
        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconFolder"; } }
        partial void OnCreated()
        {
            this._Name = "Folder";
            this._Description = "Catalog items groups";
            this.IsIncludableInModels = true;

            this._PropertyIdGuid = System.Guid.NewGuid().ToString();
            this._PropertyCodeGuid = System.Guid.NewGuid().ToString();
            this._PropertyNameGuid = System.Guid.NewGuid().ToString();
            this._PropertyDescriptionGuid = System.Guid.NewGuid().ToString();
            this._PropertyRefSelfGuid = System.Guid.NewGuid().ToString();
            this._PropertyVersionGuid = System.Guid.NewGuid().ToString();
            this._PropertyIsFolderGuid = System.Guid.NewGuid().ToString();
            this._ViewListWideGuid = System.Guid.NewGuid().ToString();
            this._ViewListNarrowGuid = System.Guid.NewGuid().ToString();

            this._IndexUniqueCodeGuid = System.Guid.NewGuid().ToString();
            this._IndexRefTreeParentCodeGuid = System.Guid.NewGuid().ToString();
            this._IndexNotUniqueCodeGuid = System.Guid.NewGuid().ToString();

            this._MaxNameLength = 20;
            this._MaxDescriptionLength = 100;

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
            var children = (ConfigNodesCollection<ITreeConfigNodeSortable>)this.Children;
            children.Add(this.GroupProperties, 2);
            children.Add(this.GroupDetails, 3);
            children.Add(this.GroupForms, 4);
            children.Add(this.GroupReports, 5);
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
            this._Name = name;
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
        public Detail AddPropertiesTab(string name, string? guid = null)
        {
            var node = new Detail(this.GroupDetails) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            this.GroupDetails.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddProperty(string name, string? guid = null)
        {
            var node = new Property(this.GroupProperties) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddProperty(string name, DataType type, string? guid = null)
        {
            var node = new Property(this.GroupProperties) { Name = name, DataType = type };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddProperty(string name, EnumDataType type, uint length, uint accuracy, string? guid = null)
        {
            var node = new Property(this.GroupProperties) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = type, Length = length, Accuracy = accuracy };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyString(string name, uint length, string? guid = null)
        {
            var node = new Property(this.GroupProperties) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.STRING, Length = length };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyNumerical(string name, uint length, uint accuracy, string? guid = null)
        {
            var node = new Property(this.GroupProperties) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.NUMERICAL, Length = length, Accuracy = accuracy };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyEnumeration(string name, Enumeration en, bool isNullable, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
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

        #region Get Properties and Details
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
        public string GetDebuggerDisplay(bool isOptimistic)
        {
            var sb = new StringBuilder();
            sb.Append("FOL ");
            sb.Append(this.Name);
            sb.Append(", ");
            sb.Append(this.ParentCatalog.ParentGroupListCatalogs.ParentModel.PKeyName);
            sb.Append(":{");
            sb.Append(this.ParentCatalog.ParentGroupListCatalogs.ParentModel.PKeyName);
            sb.Append(",nq} RefTreeParent:{RefTreeParent,nq}");
            if (isOptimistic)
            {
                sb.Append(" RecVer:{");
                sb.Append(this.ParentCatalog.ParentGroupListCatalogs.ParentModel.RecordVersionFieldName);
                sb.Append(",nq}");
            }
            return sb.ToString();
        }
        public void GetSpecialProperties(List<IProperty> res, bool isOptimistic)
        {
            var model = this.ParentCatalog.ParentGroupListCatalogs.ParentModel;
            var prp = model.GetPropertyPkId(this.GroupProperties, this.PropertyIdGuid);
            res.Add(prp);
            prp = model.GetPropertyRefParent(this.GroupProperties, this.PropertyRefSelfGuid, "RefTreeParent", true);
            res.Add(prp);
            if (this.ParentCatalog.UseTree && !this.ParentCatalog.UseSeparateTreeForFolders)
            {
                prp = model.GetPropertyIsFolder(this.GroupProperties, this.PropertyIsFolderGuid, false);
                res.Add(prp);
            }
            if (isOptimistic)
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
                prp = model.GetPropertyCatalogName(this.GroupProperties, this.PropertyNameGuid, this.MaxNameLength, false);
                res.Add(prp);
            }
            if (this.GetUseDescriptionProperty())
            {
                prp = model.GetPropertyCatalogDescription(this.GroupProperties, this.PropertyDescriptionGuid, this.MaxDescriptionLength, true);
                res.Add(prp);
            }
            foreach (var t in this.GroupProperties.ListProperties)
            {
                res.Add(t);
            }
        }
        private IProperty GetCodeProperty(List<IProperty> lst)
        {
            IProperty prp = null!;
            if (this.GetUseCodeProperty())
            {
                switch (this.CodePropertySettings.SequenceType)
                {
                    case EnumCodeType.Number:
                        prp = this.Cfg.Model.GetPropertyCatalogCodeInt(this.GroupProperties, this.PropertyCodeGuid,
                            this.CodePropertySettings.MaxSequenceLength, false);
                        break;
                    case EnumCodeType.Text:
                        prp = this.Cfg.Model.GetPropertyCatalogCode(this.GroupProperties, this.PropertyCodeGuid,
                            this.CodePropertySettings.MaxSequenceLength + (uint)this.CodePropertySettings.Prefix.Length, false);
                        break;
                    default:
                        throw new NotImplementedException();
                }
                lst.Add(prp);
            }
            return prp;
        }
        private IProperty GetNameProperty(List<IProperty> lst)
        {
            IProperty prp = null!;
            if (this.GetUseNameProperty())
            {
                prp = this.Cfg.Model.GetPropertyCatalogName(this.GroupProperties, this.PropertyNameGuid, this.MaxNameLength, false);
                lst.Add(prp);
            }
            return prp;
        }
        private IProperty GetDescriptionProperty(List<IProperty> lst)
        {
            IProperty prp = null!;
            if (this.GetUseDescriptionProperty())
            {
                prp = this.Cfg.Model.GetPropertyCatalogDescription(this.GroupProperties, this.PropertyDescriptionGuid, this.MaxDescriptionLength, true);
                lst.Add(prp);
            }
            return prp;
        }
        public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial = false)
        {
            var res = new List<IProperty>();
            if (!isExcludeSpecial)
                this.GetSpecialProperties(res, isOptimistic);
            //var model = this.ParentGroupListCatalogs.ParentModel;
            this.GetCodeProperty(res);
            this.GetNameProperty(res);
            this.GetDescriptionProperty(res);
            foreach (var t in this.GroupProperties.ListProperties)
            {
                if (t.IsIncluded(guidAppPrjDbGen))
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
        #endregion Get Properties and Details

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
                foreach (var t in f.ListAllNotSpecialProperties)
                {
                    lstp.Add((IProperty)t);
                }
                this.GetSpecialProperties(lstp, false);
                f = new Form(this.GroupForms, ftype, lstp);
            }
            return f;
        }
        public IReadOnlyList<IForm> GetListForms(string guidAppPrjGen)
        {
            var res = new List<IForm>
            {
                this.GetForm(FormType.ListNarrow, guidAppPrjGen),
                this.GetForm(FormType.ListWide, guidAppPrjGen)
            };
            return res;
        }

        private IProperty GetCodeProperty(IConfig cfg)
        {
            IProperty prp = null!;
            switch (this.CodePropertySettings.SequenceType)
            {
                case EnumCodeType.Number:
                    prp = this.Cfg.Model.GetPropertyCatalogCodeInt(this.GroupProperties, this.PropertyCodeGuid,
                        this.CodePropertySettings.MaxSequenceLength, false);
                    break;
                case EnumCodeType.Text:
                    prp = this.Cfg.Model.GetPropertyCatalogCode(this.GroupProperties, this.PropertyCodeGuid,
                        this.CodePropertySettings.MaxSequenceLength + (uint)this.CodePropertySettings.Prefix.Length, false);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return prp;
        }
        [Browsable(false)]
        public string CodePropertySettingsText { get { return this.CodePropertySettings.ToString(); } }
        public void NotifyCodePropertySettingsChanged()
        {
            this.NotifyPropertyChanged(nameof(this.CodePropertySettingsText));
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                nameof(this.Parent),
                nameof(this.Children)
            };
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
        public object GetRoleAccess(IRole role)
        {
            if (!this.dicCatalogAccess.ContainsKey(role.Guid))
            {
                var rca = new RoleCatalogAccess() { Guid = role.Guid };
                this.ListRoleCatalogAccessSettings.Add(rca);
                this.dicCatalogAccess[role.Guid] = rca;
            }
            return dicCatalogAccess[role.Guid];
        }
        public void SetRoleAccess(IRole role, EnumCatalogDetailAccess? edit, EnumPrintAccess? print)
        {
            Debug.Assert(role != null);
            Debug.Assert(dicCatalogAccess.ContainsKey(role.Guid));
            if (edit.HasValue)
                dicCatalogAccess[role.Guid].EditAccess = edit.Value;
            if (print.HasValue)
                dicCatalogAccess[role.Guid].PrintAccess = print.Value;
        }
        internal Dictionary<string, RoleCatalogAccess> dicCatalogAccess = new();
        public void InitRoles()
        {
            foreach (var tt in this.ListRoleCatalogAccessSettings)
            {
                this.dicCatalogAccess[tt.Guid] = tt;
            }
            foreach (var t in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (!this.dicCatalogAccess.ContainsKey(t.Guid))
                {
                    var rca = new RoleCatalogAccess() { Guid = t.Guid };
                    this.dicCatalogAccess[t.Guid] = rca;
                }
            }
        }
        public void InitRoleAdd(IRole role)
        {
            var rca = new RoleCatalogAccess() { Guid = role.Guid };
            this.ListRoleCatalogAccessSettings.Add(rca);
            this.dicCatalogAccess[rca.Guid] = rca;
        }
        public void InitRoleRemove(IRole role)
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
        public EnumPropertyAccess GetRolePropertyAccess(IRole role)
        {
            EnumCatalogDetailAccess r = EnumCatalogDetailAccess.C_BY_PARENT;
            if (!this.dicCatalogAccess.TryGetValue(role.Guid, out var rr) || rr.EditAccess == EnumCatalogDetailAccess.C_BY_PARENT)
            {
                r = this.ParentCatalog.GetRoleCatalogAccess(role);
            }
            Debug.Assert(r != EnumCatalogDetailAccess.C_BY_PARENT);
            switch (r)
            {
                case EnumCatalogDetailAccess.C_HIDE:
                    return EnumPropertyAccess.P_HIDE;
                case EnumCatalogDetailAccess.C_VIEW:
                    return EnumPropertyAccess.P_VIEW;
                case EnumCatalogDetailAccess.C_EDIT_ITEMS:
                case EnumCatalogDetailAccess.C_EDIT_FOLDERS:
                case EnumCatalogDetailAccess.C_MARK_DEL:
                    return EnumPropertyAccess.P_EDIT;
                default:
                    throw new NotImplementedException();
            }
        }
        public EnumPrintAccess GetRolePropertyPrint(IRole role)
        {
            EnumPrintAccess r = EnumPrintAccess.PR_BY_PARENT;
            if (!this.dicCatalogAccess.TryGetValue(role.Guid, out var rr) || rr.PrintAccess == EnumPrintAccess.PR_BY_PARENT)
            {
                r = this.ParentCatalog.GetRoleCatalogPrint(role);
            }
            Debug.Assert(r != EnumPrintAccess.PR_BY_PARENT);
            return r;
        }
        public EnumCatalogDetailAccess GetRoleCatalogAccess(IRole role)
        {
            if (this.dicCatalogAccess.TryGetValue(role.Guid, out var r) && r.EditAccess != EnumCatalogDetailAccess.C_BY_PARENT)
                return r.EditAccess;
            return this.ParentCatalog.GetRoleCatalogAccess(role);
        }
        public EnumPrintAccess GetRoleCatalogPrint(IRole role)
        {
            if (this.dicCatalogAccess.TryGetValue(role.Guid, out var r) && r.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                return r.PrintAccess;
            return this.ParentCatalog.GetRoleCatalogPrint(role);
        }
        public IReadOnlyList<string> GetRolesByAccess(EnumCatalogDetailAccess access)
        {
            var roles = new List<string>();
            foreach (var role in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (GetRoleCatalogAccess(role) == access)
                    roles.Add(role.Name);
            }
            return roles;
        }
        public IReadOnlyList<string> GetRolesByAccess(EnumPrintAccess access)
        {
            var roles = new List<string>();
            foreach (var role in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (GetRoleCatalogPrint(role) == access)
                    roles.Add(role.Name);
            }
            return roles;
        }
        #endregion Roles
    }
}
