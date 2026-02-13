using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class Catalog : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup,
        INodeWithProperties, ITreeConfigNodeSortable
    {
        public override string NameShortId { get { return $"c{this.ShortId}"; } }
        partial void OnDebugStringExtend(ref string mes)
        {
            mes += $" props:{GroupProperties.ListProperties.Count}";
        }
        public string GetDebuggerDisplay(bool isOptimistic)
        {
            var sb = new StringBuilder();
            sb.Append("CAT ");
            sb.Append(this.Name);
            sb.Append(", ");
            sb.Append(this.ParentGroupListCatalogs.ParentGroupCatalogs.ParentModel.PKeyName);
            sb.Append(":{");
            sb.Append(this.ParentGroupListCatalogs.ParentGroupCatalogs.ParentModel.PKeyName);
            sb.Append(",nq}");
            if (this.UseTree)
            {
                if (this.UseSeparateTreeForFolders)
                {
                    sb.Append(" Ref");
                    sb.Append(this.Folder.CompositeName);
                    sb.Append(":{Ref");
                    sb.Append(this.Folder.CompositeName);
                    sb.Append(",nq}");
                }
                else
                {
                    sb.Append(" RefTreeParent:{RefTreeParent,nq}");
                    //prp = model.GetPropertyIsFolder(this.GroupProperties, this.PropertyIsFolderGuid);
                    //res.Add(prp);
                }
            }
            if (isOptimistic)
            {
                sb.Append(" RecVer:{");
                sb.Append(this.ParentGroupListCatalogs.ParentGroupCatalogs.ParentModel.RecordVersionFieldName);
                sb.Append(",nq}");
            }
            return sb.ToString();
        }
        [Browsable(false)]
        public GroupListCatalogs ParentGroupListCatalogs { get { Debug.Assert(this.Parent != null); return (GroupListCatalogs)this.Parent; } }
        [Browsable(false)]
        public IGroupListCatalogs ParentGroupListCatalogsI { get { Debug.Assert(this.Parent != null); return (IGroupListCatalogs)this.Parent; } }

        [Browsable(false)]
        public static new string IconName { get { return "iconCatalogProperty"; } }
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
            this._ItemIconType = EnumCatalogTreeIcon.None;

            this._ViewListDatagridGuid = System.Guid.NewGuid().ToString();
            this._ViewListComboBoxGuid = System.Guid.NewGuid().ToString();

            this._IndexUniqueCodeGuid = System.Guid.NewGuid().ToString();
            this._IndexRefFolderCodeGuid = System.Guid.NewGuid().ToString();
            this._IndexRefTreeParentCodeGuid = System.Guid.NewGuid().ToString();
            this._IndexNotUniqueCodeGuid = System.Guid.NewGuid().ToString();

            this._MaxNameLength = 20;
            this._MaxDescriptionLength = 100;
            this._UseTree = false;
            this._MaxTreeLevels = 2;
            this._UseItemsAtRoot = true;
            this._UseSeparateTreeForFolders = false;
            this._GroupIconType = EnumCatalogTreeIcon.Folder;
            this._UseCodeProperty = EnumUseType.Default;
            this._UseNameProperty = EnumUseType.Default;
            this._IsUnicodeName = true;
            this._UseDescriptionProperty = EnumUseType.Default;
            this._IsUnicodeDescription = true;
            var m = this.Cfg.Model;
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
        protected override ConfigNodesCollection<Catalog>? GetParentCollection() { return this.ParentGroupListCatalogs.ListCatalogs; }
        public void RefillChildren()
        {
            //if (this.Children.Count > 0)
            //    return;
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
            this._Name = name;
        }
        public Catalog(ITreeConfigNode parent, string name, List<Property> listProperties)
            : this(parent)
        {
            Debug.Assert(listProperties != null);
            this._Name = name;
            foreach (var t in listProperties)
            {
                this.GroupProperties.ListProperties.Add(t);
            }
        }

        #region Tree operations
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListCatalogs.Children;
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = Catalog.Clone(this.ParentGroupListCatalogs, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupListCatalogs.ListCatalogs.Add(node, this);
            this._Name += "2";
            var model = this.ParentGroupListCatalogs.ParentGroupCatalogs.ParentModel;
            node.ShortId = ++this.ParentGroupListCatalogs.LastShortId;
            node.ShortRefId = model.LastTypeShortRefIdForNode(node, node.ShortId);
            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Catalog(this.Parent);
            this.ParentGroupListCatalogs.ListCatalogs.Add(node, this);
            this.GetUniqueName(Defaults.CatalogName, node, this.ParentGroupListCatalogs.ListCatalogs);
            var model = this.ParentGroupListCatalogs.ParentGroupCatalogs.ParentModel;
            node.ShortId = ++this.ParentGroupListCatalogs.LastShortId;
            node.ShortRefId = model.LastTypeShortRefIdForNode(node, node.ShortId);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListCatalogs.ListCatalogs.Remove(this);
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
        [Browsable(false)]
        public bool IsShowRefSelfTree { get { if (this.UseTree && !this.UseSeparateTreeForFolders) return true; return false; } }
        [Browsable(false)]
        public bool IsShowIsFolder { get { if (this.UseTree && !this.UseSeparateTreeForFolders) return true; return false; } }
        [Browsable(false)]
        public string CodePropertySettingsText { get { return this.CodePropertySettings.ToString(); } }
        public void NotifyCodePropertySettingsChanged()
        {
            this.OnPropertyChanged(nameof(this.CodePropertySettingsText));
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            if (!this.UseTree)
            {
                lst.Add(nameof(this.GroupIconType));
                lst.Add(nameof(this.MaxTreeLevels));
                lst.Add(nameof(this.UseSeparateTreeForFolders));
                lst.Add(nameof(this.UseItemsAtRoot));
            }
            else
            {
                if (!this.UseSeparateTreeForFolders)
                    lst.Add(nameof(this.UseItemsAtRoot));
            }
            if (!this.GetUseCodeProperty())
            {
                lst.Add(nameof(this.CodePropertySettings));
            }
            if (!this.GetUseNameProperty())
            {
                lst.Add(nameof(this.MaxNameLength));
            }
            if (!this.GetUseDescriptionProperty())
            {
                lst.Add(nameof(this.MaxDescriptionLength));
            }
            if (lst.Count == 0)
            {
                this.AutoGenerateProperties = true;
            }
            return [.. lst];
        }

        #region Get Properties and Details

        #region OnChanged
        partial void OnUseCodePropertyChanged()
        {
            this.OnPropertyChanged(nameof(this.PropertyDefinitions));
        }
        partial void OnUseNamePropertyChanged()
        {
            this.OnPropertyChanged(nameof(this.PropertyDefinitions));
        }
        partial void OnUseDescriptionPropertyChanged()
        {
            this.OnPropertyChanged(nameof(this.PropertyDefinitions));
        }
        //partial void OnUseItemsWithoutFolderInSeparateTreeForFoldersChanged()
        //{
        //    this.OnPropertyChanged(nameof(this.PropertyDefinitions));
        //}
        partial void OnUseSeparateTreeForFoldersChanged()
        {
            this.RefillChildren();
            this.OnPropertyChanged(nameof(this.Children));
            this.OnPropertyChanged(nameof(this.IsShowRefSelfTree));
            this.OnPropertyChanged(nameof(this.IsShowIsFolder));
            this.OnPropertyChanged(nameof(this.PropertyDefinitions));
        }
        partial void OnUseTreeChanged()
        {
            if (!this.UseTree)
            {
                this.UseSeparateTreeForFolders = false;
            }
            this.RefillChildren();
            this.OnPropertyChanged(nameof(this.Children));
            this.OnPropertyChanged(nameof(this.PropertyDefinitions));
            this.OnPropertyChanged(nameof(this.IsShowRefSelfTree));
            this.OnPropertyChanged(nameof(this.IsShowIsFolder));
        }
        #endregion OnChanged

        public bool GetUseCodeProperty()
        {
            if (this.UseCodeProperty == EnumUseType.Yes)
                return true;
            if (this.UseCodeProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListCatalogs.UseCodeProperty;
        }
        public bool GetUseNameProperty()
        {
            if (this.UseNameProperty == EnumUseType.Yes)
                return true;
            if (this.UseNameProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListCatalogs.UseNameProperty;
        }
        public bool GetUseDescriptionProperty()
        {
            if (this.UseDescriptionProperty == EnumUseType.Yes)
                return true;
            if (this.UseDescriptionProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListCatalogs.UseDescriptionProperty;
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
            }
            return prp;
        }
        public void GetSpecialProperties(List<IProperty> res, bool isOptimistic)
        {
            var model = this.Cfg.Model;
            var prp = model.GetPropertySpecial(this, EnumSpecialPropertyType.RECORD_ID);
            res.Add(prp);
            if (this.UseTree)
            {
                if (this.UseSeparateTreeForFolders)
                {
                    prp = model.GetPropertySpecial(this, EnumSpecialPropertyType.REF_CATALOG_TO_SEPARATE_CATALOG_FOLDER, true, this.Folder);
                    res.Add(prp);
                }
                else
                {
                    prp = model.GetPropertySpecial(this, EnumSpecialPropertyType.REF_TO_SELF_TREE_CATALOG_PARENT, true);
                    res.Add(prp);
                    prp = model.GetPropertyIsFolder(this, false);
                    res.Add(prp);
                }
            }
            if (isOptimistic)
            {
                prp = model.GetPropertyVersion(this);
                res.Add(prp);
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
        public IProperty GetNameProperty(List<IProperty> lst)
        {
            IProperty prp = null!;
            if (this.GetUseNameProperty())
            {
                var model = this.Cfg.Model;
                prp = model.GetPropertyName(this, false, this.MaxNameLength);
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
            uint pos = this.GroupProperties.LastGenPosition;
            var model = this.Cfg.Model;
            foreach (var t in model.GroupCatalogs.GroupRelations.GroupListOneToOneRelations.ListRelations)
            {
                if (t.GuidObj1 == this.Guid && (t.RefType == EnumOneToOneRefType.ONE_TO_ONE_REF_BOTH_DIRECTIONS || t.RefType == EnumOneToOneRefType.ONE_TO_ONE_REF_FROM_FIRST_TO_SECOND_ONLY))
                {
                    Debug.Assert(t.GuidObj2 != null);
                    if (t.RefObj2Type == EnumRelationConfigType.RelConfigTypeCatalogs)
                    {
                        var prp = (Property)t.PropertyRefObj2;
                        prp.Position = ++pos;
                        //prp.IsNullable = t.IsRelationReferenceNullable;
                        //var prp = model.GetPropertyCatalog(this, t.RefObj2PropGuid, t.Name, t.GuidObj2, (uint)res.Count, t.IsRelationReferenceNullable);
                        res.Add(prp);
                    }
                    else if (t.RefObj2Type == EnumRelationConfigType.RelConfigTypeDocuments)
                    {
                        var prp = (Property)t.PropertyRefObj2;
                        prp.Position = ++pos;
                        //prp.IsNullable = t.IsRelationReferenceNullable;
                        //var prp = model.GetPropertyDocument(this, t.RefObj2PropGuid, t.Name, t.GuidObj2, (uint)res.Count, t.IsRelationReferenceNullable);
                        res.Add(prp);
                    }
                    else
                        throw new NotImplementedException();
                }
                if (t.GuidObj2 == this.Guid && (t.RefType == EnumOneToOneRefType.ONE_TO_ONE_REF_BOTH_DIRECTIONS || t.RefType == EnumOneToOneRefType.ONE_TO_ONE_REF_FROM_SECOND_TO_FIRST_ONLY))
                {
                    Debug.Assert(t.GuidObj1 != null);
                    if (t.RefObj1Type == EnumRelationConfigType.RelConfigTypeCatalogs)
                    {
                        var prp = (Property)t.PropertyRefObj1;
                        prp.Position = ++pos;
                        //prp.IsNullable = t.IsRelationReferenceNullable;
                        //var prp = model.GetPropertyCatalog(this, t.RefObj1PropGuid, t.Name, t.GuidObj1, (uint)res.Count, t.IsRelationReferenceNullable);
                        res.Add(prp);
                    }
                    else if (t.RefObj1Type == EnumRelationConfigType.RelConfigTypeDocuments)
                    {
                        var prp = (Property)t.PropertyRefObj1;
                        prp.Position = ++pos;
                        //prp.IsNullable = t.IsRelationReferenceNullable;
                        //var prp = model.GetPropertyDocument(this, t.RefObj1PropGuid, t.Name, t.GuidObj1, (uint)res.Count, t.IsRelationReferenceNullable);
                        res.Add(prp);
                    }
                    else
                        throw new NotImplementedException();
                }
            }
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
        #endregion Get Properties and Details

        public IForm GetForm(FormType ftype, string guidAppPrjGen)
        {
            var f = (from tf in this.GroupForms.ListForms where tf.EnumFormType == ftype select tf).SingleOrDefault();
            if (f == null)
            {
                var lstp = new List<IProperty>();
                this.GetCodeProperty(lstp);
                this.GetNameProperty(lstp);
                if (ftype == FormType.ListDataGrid)
                {
                    this.GetDescriptionProperty(lstp);
                }
                if (lstp.Count == 0)
                {
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
        public ViewFormData GetFormViewData(FormType formType, string guidAppPrjGen)
        {
            var model = this.Cfg.Model;
            ViewTreeData? viewTreeData = null;
            ViewListData? viewListData = null;
            Form form = (from p in this.GroupForms.ListForms where p.EnumFormType == formType select p).Single();
            var prp = model.GetPropertySpecial(this, EnumSpecialPropertyType.RECORD_ID);
            IProperty? pRefTreeParent = null;
            IProperty? pRefParent = null;
            if (this.UseTree)
            {
                pRefTreeParent = model.GetPropertySpecial(this, EnumSpecialPropertyType.REF_TO_SELF_TREE_CATALOG_PARENT, true);
                if (this.UseSeparateTreeForFolders) // self tree and separate data grid for children
                {
                    viewTreeData = new ViewTreeData(prp, pRefTreeParent, null);
                    var lst = this.SelectViewProperties(formType, this.Folder.GroupProperties.ListProperties, form.ListGuidViewFolderProperties, guidAppPrjGen);
                    viewTreeData.ListViewProperties.AddRange(lst);

                    viewListData = new ViewListData(prp, pRefParent, null);
                    lst = this.SelectViewProperties(formType, this.GroupProperties.ListProperties, form.ListGuidViewProperties, guidAppPrjGen);
                    viewListData.ListViewProperties.AddRange(lst);
                }
                else // only self tree
                {
                    var pIsFolder = model.GetPropertyIsFolder(this, false);
                    viewTreeData = new ViewTreeData(prp, pRefParent, pIsFolder);
                    var lst = this.SelectViewProperties(formType, this.Folder.GroupProperties.ListProperties, form.ListGuidViewFolderProperties, guidAppPrjGen);
                    viewTreeData.ListViewProperties.AddRange(lst);
                }
            }
            else // only data grid for children
            {
                viewListData = new ViewListData(prp);
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
                this.GetCodeProperty(res);
                this.GetNameProperty(res);
                if (formType == FormType.ListDataGrid)
                {
                    this.GetDescriptionProperty(res);
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
            return this.ParentGroupListCatalogs.IsGridSortableGet();
        }
        public bool IsGridFilterableGet()
        {
            if (this.IsGridFilterable == EnumUseType.Yes)
                return true;
            if (this.IsGridFilterable == EnumUseType.No)
                return false;
            return this.ParentGroupListCatalogs.IsGridFilterableGet();
        }
        public bool IsGridSortableCustomGet()
        {
            if (this.IsGridSortableCustom == EnumUseType.Yes)
                return true;
            if (this.IsGridSortableCustom == EnumUseType.No)
                return false;
            return this.ParentGroupListCatalogs.IsGridSortableCustomGet();
        }

        #region Roles
        public IRoleCatalogsSettings GetRoleSettings(IRole role)
        {
            var roles = this.Cfg.Model.GroupCommon.GroupRoles;
            var nodeRoleDic = roles.DicRoles[role.Guid];
            nodeRoleDic.DicNodeRules.TryGetValue(this.Guid, out var roleFromNode);
            var res = new RoleCatalogsSettings(this);
            res.CanEditDetails = roleFromNode?.CatalogSettings.CanEditDetails ?? roles.DefaultCatalogsRoleSettings.CanEditDetails;
            res.CanEditFields = roleFromNode?.CatalogSettings.CanEditFields ?? roles.DefaultCatalogsRoleSettings.CanEditFields;
            res.CanEditFolders = roleFromNode?.CatalogSettings.CanEditFolders ?? roles.DefaultCatalogsRoleSettings.CanEditFolders;
            res.CanEditItems = roleFromNode?.CatalogSettings.CanEditItems ?? roles.DefaultCatalogsRoleSettings.CanEditItems;
            res.CanMarkDel = roleFromNode?.CatalogSettings.CanMarkDel ?? roles.DefaultCatalogsRoleSettings.CanMarkDel;
            res.CanMoveFolders = roleFromNode?.CatalogSettings.CanMoveFolders ?? roles.DefaultCatalogsRoleSettings.CanMoveFolders;
            res.CanMoveItems = roleFromNode?.CatalogSettings.CanMoveItems ?? roles.DefaultCatalogsRoleSettings.CanMoveItems;
            res.CanPrint = roleFromNode?.CatalogSettings.CanPrint ?? roles.DefaultCatalogsRoleSettings.CanPrint;
            res.CanView = roleFromNode?.CatalogSettings.CanView ?? roles.DefaultCatalogsRoleSettings.CanView;
            res.CanViewDetails = roleFromNode?.CatalogSettings.CanViewDetails ?? roles.DefaultCatalogsRoleSettings.CanViewDetails;
            res.CanViewFields = roleFromNode?.CatalogSettings.CanViewFields ?? roles.DefaultCatalogsRoleSettings.CanViewFields;
            return res;
        }
        #endregion Roles
    }
}
