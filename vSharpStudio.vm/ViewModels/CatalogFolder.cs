using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentValidation;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugInfo(),nq}")]
    public partial class CatalogFolder : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNodeGroup,
        INodeWithProperties
    {
        public override string NameShortId { get { return $"f{this.ParentCatalog.ShortId}"; } }
        partial void OnDebugStringExtend(StringBuilder sb)
        {
            sb.Append(" prj:");
            sb.Append(GroupProperties.ListProperties.Count);
        }
        public string GetDebuggerDisplay(bool isOptimistic)
        {
            var sb = new StringBuilder();
            sb.Append("FOL ");
            sb.Append(this.Name);
            sb.Append(", ");
            sb.Append(this.Cfg.Model.PKeyName);
            sb.Append(":{");
            sb.Append(this.Cfg.Model.PKeyName);
            sb.Append(",nq} RefTreeParent:{RefTreeParent,nq}");
            sb.Append(" State:{");
            sb.Append(this.Cfg.Model.RecordStateFieldName);
            sb.Append(",nq}");
            if (isOptimistic)
            {
                sb.Append(" RecVer:{");
                sb.Append(this.Cfg.Model.RecordVersionFieldName);
                sb.Append(",nq}");
            }
            return sb.ToString();
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
        public uint ShortId { get { return this.ParentCatalog.ShortId; } }
        [Browsable(false)]
        public new string IconName { get { return "iconFolder"; } }
        partial void OnCreated()
        {
            this._LastPosition = IProperty.PositionReservation;
            this._Name = "Folder";
            this._Description = "Catalog items groups";
            this.IsIncludableInModels = true;

            this._ViewListDatagridGuid = System.Guid.NewGuid().ToString();
            this._ViewListComboBoxGuid = System.Guid.NewGuid().ToString();

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
            this.GetSpecialProperties(new List<IProperty>(), true); // position ang guids for special properties
        }
        public void OnAdded()
        {
            this.AddOrRestoreAllAppGenSettingsVmsToNode();
            this.GroupProperties.AddOrRestoreAllAppGenSettingsVmsToNode();
            this.GroupDetails.AddOrRestoreAllAppGenSettingsVmsToNode();
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
        public Detail AddDetails(string name, string? guid = null)
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
            var model = this.Cfg.Model;
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
        public Property AddPropertyCatalog(string name, string catGuid, bool isNullable = false, bool isCsNullable = true, string? guidProperty = null)
        {
            var node = new Property(this.GroupProperties) { Name = name, IsNullable = isNullable, IsCsNullable = isCsNullable };
#if DEBUG
            if (guidProperty != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guidProperty))
                    return node;
                node.Guid = guidProperty;
            }
#endif
            node.DataType = new DataType(node)
            {
                IsNullable = isNullable,
                DataTypeEnum = EnumDataType.CATALOG,
            };
            node.ConfigObjectGuid = catGuid;
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyCatalog(string name, Catalog cat, string? guid = null)
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
            node.DataType = new DataType(node);
            node.IsNullable = true;
            node.DataType.ObjectRef0.ForeignObjectGuid = cat.Guid;
            node.DataType.DataTypeEnum = EnumDataType.CATALOG;
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyCatalogs(string name, Catalog cat, Catalog? cat2 = null, string? guid = null)
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
            node.DataType = new DataType(node);
            node.IsNullable = true;
            node.DataType.ObjectRef0.ForeignObjectGuid = cat.Guid;
            if (cat2 != null)
            {
                node.DataType.ListObjectRefs.Add(new ComplexRef(node.Guid, cat2.Guid));
            }
            node.DataType.DataTypeEnum = EnumDataType.CATALOGS;
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
        public Property AddPropertyStringFixed(string name, uint length, string? guid = null)
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
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.STRING_FIXED, Length = length };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyUlid(string name, string? guid = null)
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
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.ULID };
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
        //        public Property AddPropertyEnumeration(string name, Enumeration en, bool isNullable, string? guid = null)
        //        {
        //            var node = new Property(this) { Name = name };
        //#if DEBUG
        //            if (guid != null) // for test model generation
        //            {
        //                if (this.Cfg.DicNodes.ContainsKey(guid))
        //                    return node;
        //                node.Guid = guid;
        //            }
        //#endif
        //            node.DataType = new DataType(node)
        //            {
        //                DataTypeEnum = EnumDataType.ENUMERATION,
        //            };
        //            node.ListObjectRefs.Add(new ComplexRef(node.Guid, en.Guid));
        //            node.IsNullable = isNullable;
        //            this.NodeAddNewSubNode(node);
        //            return node;
        //        }

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
        public uint GetNextFreePosition() { return ++this.LastPosition; }
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
        public void GetSpecialProperties(List<IProperty> res, bool isOptimistic)
        {
            var model = this.Cfg.Model;
            var prp = model.GetPropertySpecial(this, EnumSpecialPropertyType.RECORD_ID);
            res.Add(prp);

            prp = model.GetPropertySpecial(this, EnumSpecialPropertyType.REF_TO_SELF_TREE_CATALOG_FOLDER_PARENT, true);
            res.Add(prp);

            if (this.ParentCatalog.UseTree && !this.ParentCatalog.UseSeparateTreeForFolders)
            {
                prp = model.GetPropertyIsFolder(this, false);
                res.Add(prp);
            }
            if (isOptimistic)
            {
                prp = model.GetPropertyVersion(this);
                res.Add(prp);
                //prp = model.GetPropertyVersionPrev(this);
                //res.Add(prp);
            }
        }
        public void GetNormalProperties(List<IProperty> res)
        {
            this.GetCodeProperty(res);
            this.GetNameProperty(res);
            this.GetDescriptionProperty(res);
            foreach (var t in this.GroupProperties.ListProperties)
            {
                res.Add(t);
            }
        }
        public IProperty? GetCodeProperty(List<IProperty> lst)
        {
            var prp = GetCodeProperty();
            if (prp != null)
                lst.Add(prp);
            return prp;
        }
        public IProperty? GetCodeProperty()
        {
            IProperty? prp = null!;
            if (this.GetUseCodeProperty())
            {
                var model = this.Cfg.Model;
                prp = this.CodePropertySettings.SequenceType switch
                {
                    EnumCodeType.Number =>
                        model.GetPropertyCodeInt(this, false, this.CodePropertySettings.MaxSequenceLength),
                    EnumCodeType.Text =>
                        model.GetPropertyCodeStr(this, false, this.CodePropertySettings.MaxSequenceLength + (uint)this.CodePropertySettings.Prefix.Length),
                    _ => throw new NotImplementedException(),
                };
                prp.Parent = this.GroupProperties;
            }
            return prp;
        }
        public IProperty GetNameProperty(List<IProperty> lst)
        {
            IProperty prp = null!;
            if (this.GetUseNameProperty())
            {
                var model = this.Cfg.Model;
                prp = model.GetPropertyName(this, false, this.MaxNameLength);
                prp.Parent = this.GroupProperties;
                lst.Add(prp);
            }
            return prp;
        }
        public IProperty? GetDescriptionProperty(List<IProperty> lst)
        {
            IProperty? prp = null!;
            if (this.GetUseDescriptionProperty())
            {
                var model = this.Cfg.Model;
                prp = model.GetPropertyDescription(this, false, this.MaxDescriptionLength);
                prp.Parent = this.GroupProperties;
                lst.Add(prp);
            }
            return prp;
        }
        public IProperty? GetDateTimeUtcProperty(bool? isRegisterBalance = null)
        {
            return null;
        }
        public IReadOnlyList<IProperty> GetListIdPKeyProperties(bool? isRegisterBalance = null)
        {
            Debug.Assert(isRegisterBalance == null);
            var res = new List<IProperty>();
            var model = this.Cfg.Model;
            var prp = model.GetPropertySpecial(this, EnumSpecialPropertyType.RECORD_ID);
            res.Add(prp);
            return res;
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

        #region OnChanged
        partial void OnUseCodePropertyChanged()
        {
            this.OnPropertyChanged(nameof(this.PropertyDefinitions));
            this.GetSpecialProperties(new List<IProperty>(), true); // position ang guids for special properties
        }
        partial void OnUseNamePropertyChanged()
        {
            this.OnPropertyChanged(nameof(this.PropertyDefinitions));
            this.GetSpecialProperties(new List<IProperty>(), true); // position ang guids for special properties
        }
        partial void OnUseDescriptionPropertyChanged()
        {
            this.OnPropertyChanged(nameof(this.PropertyDefinitions));
            this.GetSpecialProperties(new List<IProperty>(), true); // position ang guids for special properties
        }
        #endregion OnChanged

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
                this.GetForm(FormType.ListComboBox, guidAppPrjGen),
                this.GetForm(FormType.ListDataGrid, guidAppPrjGen)
            };
            return res;
        }
        [Browsable(false)]
        public string CodePropertySettingsText { get { return this.CodePropertySettings.ToString(); } }
        public void NotifyCodePropertySettingsChanged()
        {
            this.OnPropertyChanged(nameof(this.CodePropertySettingsText));
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                nameof(this.Parent),
                nameof(this.Children)
            };
            return [.. lst];
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
