using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class Document : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup, INodeWithProperties, IRoleAccess, IDocumentAccessRoles
    {
        public override string NameShortId { get { return $"d{this.ShortId}"; } }
        partial void OnDebugStringExtend(ref string mes)
        {
            mes += $" props:{GroupProperties.ListProperties.Count} details:{GroupDetails.ListDetails.Count} seq:{this.Sequence?.Name}";
        }
        public string GetDebuggerDisplay(bool isOptimistic)
        {
            var sb = new StringBuilder();
            sb.Append("DOC ");
            sb.Append(this.Name);
            sb.Append(", ");
            sb.Append(this.ParentGroupListDocuments.ParentGroupDocuments.ParentModel.PKeyName);
            sb.Append(":{");
            sb.Append(this.ParentGroupListDocuments.ParentGroupDocuments.ParentModel.PKeyName);
            sb.Append(",nq}");
            if (isOptimistic)
            {
                sb.Append(" RecVer:{");
                sb.Append(this.ParentGroupListDocuments.ParentGroupDocuments.ParentModel.RecordVersionFieldName);
                sb.Append(",nq}");
            }
            sb.Append(" Number:{");
            sb.Append(this.ParentGroupListDocuments.PropertyDocNumberName);
            sb.Append(",nq}");
            sb.Append(" Date:{");
            sb.Append(this.ParentGroupListDocuments.ParentGroupDocuments.DocumentTimeline.TimeLineDocDateTimePropertyName);
            sb.Append(",nq}");
            return sb.ToString();
        }
        [Browsable(false)]
        public GroupListDocuments ParentGroupListDocuments { get { Debug.Assert(this.Parent != null); return (GroupListDocuments)this.Parent; } }
        [Browsable(false)]
        public IGroupListDocuments ParentGroupListDocumentsI { get { Debug.Assert(this.Parent != null); return (IGroupListDocuments)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListDocuments.Children;
        }
        #endregion ITree

        [Browsable(false)]
        public static new string IconName { get { return "iconDiagnosticesFile"; } }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;

            this._SequenceGuid = "";
            this._ListSelectedRegisters = [];
            this._ListSelectedRegisters.CollectionChanged += _ListSelectedRegisters_CollectionChanged;
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
            children.Add(this.GroupProperties, 1);
            children.Add(this.GroupDetails, 2);
            children.Add(this.GroupForms, 3);
            children.Add(this.GroupReports, 4);
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
        protected override ConfigNodesCollection<Document>? GetParentCollection() { return this.ParentGroupListDocuments.ListDocuments; }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
            this.GroupProperties.AddAllAppGenSettingsVmsToNode();
            this.GroupDetails.AddAllAppGenSettingsVmsToNode();
            this.GroupForms.AddAllAppGenSettingsVmsToNode();
            this.GroupReports.AddAllAppGenSettingsVmsToNode();
        }

        #region Tree operations
        public override ITreeConfigNode NodeAddClone()
        {
            var node = Document.Clone(this.ParentGroupListDocuments, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupListDocuments.ListDocuments.Add(node, this);
            this.Name += "2";
            var model = (Model)this.Cfg.Model;
            node.ShortId = ++this.ParentGroupListDocuments.LastShortId;
            node.ShortRefId = model.LastTypeShortRefIdForNode(node, node.ShortId);
            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Document(this.ParentGroupListDocuments);
            this.ParentGroupListDocuments.ListDocuments.Add(node, this);
            this.GetUniqueName(Defaults.DocumentName, node, this.ParentGroupListDocuments.ListDocuments);
            var model = this.ParentGroupListDocuments.ParentGroupDocuments.ParentModel;
            node.ShortId = ++this.ParentGroupListDocuments.LastShortId;
            node.ShortRefId = model.LastTypeShortRefIdForNode(node, node.ShortId);
            this.SetSelected(node);
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
        public void Remove()
        {
            this.ParentGroupListDocuments.ListDocuments.Remove(this);
        }
        #endregion Tree operations

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
        public bool IsDocWithSharedProperties
        {
            get
            {
                return this.ParentGroupListDocuments.ParentGroupDocuments.DocumentTimeline.ListProperties.Count > 0;
            }
        }
        public void GetNormalProperties(List<IProperty> res)
        {
            foreach (var t in this.GroupProperties.ListProperties)
            {
                res.Add(t);
            }
        }
        /// <summary>
        /// All properties (shared and normal)
        /// Shared included first
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<IProperty> GetPropertiesForUI(bool isOptimistic, bool isExcludeSpecial = false)
        {
            var res = new List<IProperty>();
            var grd = this.ParentGroupListDocuments.ParentGroupDocuments;
            if (!isExcludeSpecial)
            {
                this.GetSpecialProperties(res, isOptimistic);
            }
            foreach (var t in grd.DocumentTimeline.ListProperties)
            {
                res.Add(t);
            }
            foreach (var t in this.GroupProperties.ListProperties)
            {
                res.Add(t);
            }
            return res;
        }
        public IProperty? GetDateTimeUtcProperty(bool? isRegisterBalance = null)
        {
            return null;
        }
        public IReadOnlyList<IProperty> GetListIdPKeyProperties(bool? isRegisterBalance = null)
        {
            Debug.Assert(isRegisterBalance == null);
            var res = new List<IProperty>();
            var prp = this.Cfg.Model.GetPropertyPkId(this.GroupProperties, this.Cfg.Model.PropertyIdGuid);
            res.Add(prp);
            return res;
        }
        public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isOptimistic, bool isExcludeSpecial = false, bool isOnlyShared = false, bool isOnlyNotShared = true)
        {
            var res = new List<IProperty>();
            if (!isExcludeSpecial)
            {
                this.GetSpecialProperties(res, isOptimistic);
            }
            this.GetDocNumberProperty(res);
            uint pos = this.GroupProperties.LastGenPosition;
            foreach (var t in this.Cfg.Model.GroupCatalogs.GroupRelations.GroupListOneToOneRelations.ListRelations)
            {
                if (t.GuidObj1 == this.Guid && (t.RefType == EnumOneToOneRefType.ONE_TO_ONE_REF_BOTH_DIRECTIONS || t.RefType == EnumOneToOneRefType.ONE_TO_ONE_REF_FROM_FIRST_TO_SECOND_ONLY))
                {
                    Debug.Assert(t.GuidObj2 != null);
                    if (t.RefObj2Type == EnumRelationConfigType.RelConfigTypeCatalogs)
                    {
                        var prp = (Property)t.PropertyRefObj2;
                        prp.Position = ++pos;
                        //var prp = this.Cfg.Model.GetPropertyCatalog(this, t.RefObj2PropGuid, t.Name, t.GuidObj2, (uint)res.Count, t.IsRelationReferenceNullable);
                        if (!isOnlyShared)
                            res.Add(prp);
                    }
                    else if (t.RefObj2Type == EnumRelationConfigType.RelConfigTypeDocuments)
                    {
                        var prp = (Property)t.PropertyRefObj2;
                        prp.Position = ++pos;
                        //var prp = this.Cfg.Model.GetPropertyDocument(this, t.RefObj2PropGuid, t.Name, t.GuidObj2, (uint)res.Count, t.IsRelationReferenceNullable);
                        if (!isOnlyShared)
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
                        //var prp = this.Cfg.Model.GetPropertyCatalog(this, t.RefObj1PropGuid, t.Name, t.GuidObj1, (uint)res.Count, t.IsRelationReferenceNullable);
                        if (!isOnlyShared)
                            res.Add(prp);
                    }
                    else if (t.RefObj1Type == EnumRelationConfigType.RelConfigTypeDocuments)
                    {
                        var prp = (Property)t.PropertyRefObj1;
                        prp.Position = ++pos;
                        //var prp = this.Cfg.Model.GetPropertyDocument(this, t.RefObj1PropGuid, t.Name, t.GuidObj1, (uint)res.Count, t.IsRelationReferenceNullable);
                        if (!isOnlyShared)
                            res.Add(prp);
                    }
                    else
                        throw new NotImplementedException();
                }
            }
            if (!isOnlyNotShared)
            {
                foreach (var t in this.ParentGroupListDocuments.ParentGroupDocuments.DocumentTimeline.ListProperties)
                {
                    if (t.IsIncluded(guidAppPrjGen))
                    {
                        t.ComplexObjectName = "SharedDocProperties";
                        res.Add(t);
                    }
                }
            }
            if (!isOnlyShared)
            {
                foreach (var t in this.GroupProperties.ListProperties)
                {
                    if (t.IsIncluded(guidAppPrjGen))
                    {
                        res.Add(t);
                    }
                }
            }
            return res;
        }
        public void GetSpecialProperties(List<IProperty> res, bool isOptimistic)
        {
            var model = this.ParentGroupListDocuments.ParentGroupDocuments.ParentModel;
            //var prp = model.GetPropertyPkId(this.GroupProperties, this.Cfg.Model.PropertyIdGuid);
            //string name = this.ParentGroupListDocuments.ParentGroupDocuments.GetTimelineCompositeName();
            //var prp = model.GetPropertyRef(this.GroupProperties, this.Cfg.Model.PropertyIdGuid, "Ref" + name, 0, false, true);
            IProperty prp = this.Cfg.Model.GetPropertyPkId(this.GroupProperties, this.Cfg.Model.PropertyIdGuid);
            //prp = model.GetPropertyRef(this.GroupProperties, this.Cfg.Model.PropertyIdGuid, this.Cfg.Model.PKeyName, 0, false, true);
            res.Add(prp);
            if (isOptimistic)
            {
                prp = model.GetPropertyVersion(this.GroupProperties, this.Cfg.Model.PropertyVersionGuid);
                res.Add(prp);
            }
            //prp = model.GetPropertyDocumentDate(this.GroupProperties, this.Cfg.Model.PropertyDocDateGuid);
            //res.Add(prp);
            //prp = model.GetPropertyBool(this.GroupProperties, this.Cfg.Model.PropertyDocIsPostedGuid, "IsPosted", 10, true);
            //res.Add(prp);
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
        //public ViewFormData GetFormViewData(FormType formType, string guidAppPrjGen)
        //{
        //    ViewListData? viewListData = null;
        //    var model = this.ParentGroupListDocuments.ParentGroupDocuments.ParentModel;
        //    Form form = (from p in this.GroupForms.ListForms where p.EnumFormType == formType select p).Single();
        //    var pId = model.GetPropertyPkId(this.GroupProperties, this.Cfg.Model.PropertyIdGuid);
        //    viewListData = new ViewListData(pId);
        //    var lst = SelectViewProperties(formType, this.GroupProperties.ListProperties, form.ListGuidViewProperties, guidAppPrjGen);
        //    viewListData.ListViewProperties.AddRange(lst);
        //    return new ViewFormData(null, viewListData);
        //}
        public IForm GetForm(FormType ftype, string guidAppPrjGen)
        {
            var f = (from tf in this.GroupForms.ListForms where tf.EnumFormType == ftype select tf).SingleOrDefault();
            if (f == null)
            {
                var lstp = new List<IProperty>();
                this.GetDocNumberProperty(lstp);
                var prp = this.Cfg.Model.GetPropertyPkId(this.GroupProperties, this.Cfg.Model.PropertyIdGuid);
                lstp.Add(prp);
                prp = this.Cfg.Model.GetPropertyDocumentDate(this.GroupProperties, this.Cfg.Model.PropertyDocDateGuid);
                prp.IsSimple = true;
                lstp.Add(prp);
                f = new Form(this.GroupForms, ftype, lstp);
            }
            else
            {
                var lstp = new List<IProperty>();
                foreach (var t in f.ListAllNotSpecialProperties)
                {
                    lstp.Add((IProperty)t);
                }
                var prp = this.Cfg.Model.GetPropertyPkId(this.GroupProperties, this.Cfg.Model.PropertyIdGuid);
                lstp.Add(prp);
                prp = this.Cfg.Model.GetPropertyDocumentDate(this.GroupProperties, this.Cfg.Model.PropertyDocDateGuid);
                prp.IsSimple = true;
                lstp.Add(prp);
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
        public IProperty GetDocNumberProperty(List<IProperty> lst)
        {
            Debug.Assert(this.Sequence != null);
            var prp = this.Sequence.SequenceType switch
            {
                EnumCodeType.Number => this.Cfg.Model.GetPropertyDocNumberInt(this.GroupProperties, this.Cfg.Model.PropertyDocNumberGuid,
                                        this.Sequence.MaxSequenceLength),
                EnumCodeType.Text => this.Cfg.Model.GetPropertyDocNumberString(this.GroupProperties, this.Cfg.Model.PropertyDocNumberGuid,
                                        this.Sequence.MaxSequenceLength + (uint)this.Sequence.Prefix.Length),
                _ => throw new NotImplementedException(),
            };
            lst.Add(prp);
            return prp;
        }
        //public IProperty GetDocDateProperty(List<IProperty> lst)
        //{
        //    IProperty prp = null!;
        //    lst.Add(prp);
        //    return prp;
        //}
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
                var len = 3;
                foreach (var t in fromPropertiesList)
                {
                    if (guidAppPrjGen == null || t.IsIncluded(guidAppPrjGen))
                    {
                        len--;
                        res.Add(t);
                    }
                    if (len == 0)
                        break;
                }
            }
            return res;
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
            return this.ParentGroupListDocuments.IsGridSortableGet();
        }
        public bool IsGridFilterableGet()
        {
            if (this.IsGridFilterable == EnumUseType.Yes)
                return true;
            if (this.IsGridFilterable == EnumUseType.No)
                return false;
            return this.ParentGroupListDocuments.IsGridFilterableGet();
        }
        public bool IsGridSortableCustomGet()
        {
            if (this.IsGridSortableCustom == EnumUseType.Yes)
                return true;
            if (this.IsGridSortableCustom == EnumUseType.No)
                return false;
            return this.ParentGroupListDocuments.IsGridSortableCustomGet();
        }

        #region Roles
        public object GetRoleAccess(IRole role)
        {
            if (!this.dicDocumentAccess.TryGetValue(role.Guid, out var value))
            {
                var rca = new RoleDocumentAccess() { Guid = role.Guid };
                this.ListRoleDocumentAccessSettings.Add(rca);
                value = rca;
                this.dicDocumentAccess[role.Guid] = value;
            }
            return value;
        }
        public void SetRoleAccess(IRole role, EnumDocumentAccess? edit, EnumPrintAccess? print)
        {
            Debug.Assert(role != null);
            Debug.Assert(dicDocumentAccess.ContainsKey(role.Guid));
            if (edit.HasValue)
                dicDocumentAccess[role.Guid].EditAccess = edit.Value;
            if (print.HasValue)
                dicDocumentAccess[role.Guid].PrintAccess = print.Value;
        }
        internal Dictionary<string, RoleDocumentAccess> dicDocumentAccess = [];
        public void InitRoles()
        {
            foreach (var tt in this.ListRoleDocumentAccessSettings)
            {
                this.dicDocumentAccess[tt.Guid] = tt;
            }
            foreach (var t in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (!this.dicDocumentAccess.ContainsKey(t.Guid))
                {
                    var rca = new RoleDocumentAccess() { Guid = t.Guid };
                    this.dicDocumentAccess[t.Guid] = rca;
                }
            }
        }
        public void InitRoleAdd(IRole role)
        {
            var rca = new RoleDocumentAccess() { Guid = role.Guid };
            this.ListRoleDocumentAccessSettings.Add(rca);
            this.dicDocumentAccess[rca.Guid] = rca;
        }
        public void InitRoleRemove(IRole role)
        {
            for (int i = 0; i < this.ListRoleDocumentAccessSettings.Count; i++)
            {
                if (this.ListRoleDocumentAccessSettings[i].Guid == role.Guid)
                {
                    this.ListRoleDocumentAccessSettings.RemoveAt(i);
                    break;
                }
            }
            this.dicDocumentAccess.Remove(role.Guid);
        }
        public EnumDocumentAccess GetRoleDocumentAccess(IRole role)
        {
            if (this.dicDocumentAccess.TryGetValue(role.Guid, out var r) && r.EditAccess != EnumDocumentAccess.D_BY_PARENT)
                return r.EditAccess;
            return this.ParentGroupListDocuments.GetRoleDocumentAccess(role);
        }
        public EnumPrintAccess GetRoleDocumentPrint(IRole role)
        {
            if (this.dicDocumentAccess.TryGetValue(role.Guid, out var r) && r.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                return r.PrintAccess;
            return this.ParentGroupListDocuments.GetRoleDocumentPrint(role);
        }
        public EnumPropertyAccess GetRolePropertyAccess(IRole role)
        {
            var ra = EnumDocumentAccess.D_BY_PARENT;
            if (this.dicDocumentAccess.TryGetValue(role.Guid, out var r))
                ra = r.EditAccess;
            if (ra == EnumDocumentAccess.D_BY_PARENT)
                ra = this.ParentGroupListDocuments.GetRoleDocumentAccess(role);
            Debug.Assert(ra != EnumDocumentAccess.D_BY_PARENT);
            return ra switch
            {
                EnumDocumentAccess.D_HIDE => EnumPropertyAccess.P_HIDE,
                EnumDocumentAccess.D_VIEW => EnumPropertyAccess.P_VIEW,
                EnumDocumentAccess.D_EDIT or EnumDocumentAccess.D_MARK_DEL or EnumDocumentAccess.D_UNPOST or EnumDocumentAccess.D_POST => EnumPropertyAccess.P_EDIT,
                _ => throw new NotImplementedException(),
            };
        }
        public EnumPrintAccess GetRolePropertyPrint(IRole role)
        {
            var ra = EnumPrintAccess.PR_BY_PARENT;
            if (this.dicDocumentAccess.TryGetValue(role.Guid, out var r))
                ra = r.PrintAccess;
            if (ra == EnumPrintAccess.PR_BY_PARENT)
                ra = this.ParentGroupListDocuments.GetRoleDocumentPrint(role);
            Debug.Assert(ra != EnumPrintAccess.PR_BY_PARENT);
            return ra;
        }
        public IReadOnlyList<string> GetRolesByAccess(EnumDocumentAccess access)
        {
            var roles = new List<string>();
            foreach (var role in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (GetRoleDocumentAccess(role) == access)
                    roles.Add(role.Name);
            }
            return roles;
        }
        public IReadOnlyList<string> GetRolesByAccess(EnumPrintAccess access)
        {
            var roles = new List<string>();
            foreach (var role in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (GetRoleDocumentPrint(role) == access)
                    roles.Add(role.Name);
            }
            return roles;
        }
        #endregion Roles

        [Browsable(false)]
        public IDocumentEnumeratorSequence? Sequence { get { if (!this.Cfg.DicNodes.ContainsKey(this.SequenceGuid)) return null; return (IDocumentEnumeratorSequence)this.Cfg.DicNodes[this.SequenceGuid]; } }

        #region Mapping Editor

        private bool isOnOpeningEditor = false;
        public override void OnOpeningEditor()
        {
            this.isOnOpeningEditor = true;

            #region ListNotSelectedRegisters
            this.ListNotSelectedRegisters.Clear();
            foreach (var t in this.Cfg.Model.GroupDocuments.GroupRegisters.ListRegisters)
            {
                bool found = false;
                foreach (var tt in t.ListObjectDocRefs)
                {
                    if (this.Guid == tt.ForeignObjectGuid)
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                    continue;
                this.ListNotSelectedRegisters.Add(t);
            }
            #endregion ListNotSelectedRegisters

            #region ListSelectedRegisters
            this.ListSelectedRegisters.Clear();
            foreach (var t in this.Cfg.Model.GroupDocuments.GroupRegisters.ListRegisters)
            {
                bool found = false;
                foreach (var tt in t.ListObjectDocRefs)
                {
                    if (this.Guid == tt.ForeignObjectGuid)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    continue;
                this.ListSelectedRegisters.Add(t);
            }
            #endregion ListSelectedRegisters

            #region ListMappings
            Register.UpdateListMappings((Register?)this.SelectedReg, this);
            #endregion ListMappings

            this.isOnOpeningEditor = false;
        }

        #region Registers
        [Browsable(false)]
        public SortedObservableCollection<ISortingValue> ListNotSelectedRegisters
        {
            get => _ListNotSelectedRegisters;
            set => SetProperty(ref _ListNotSelectedRegisters, value);
        }
        private SortedObservableCollection<ISortingValue> _ListNotSelectedRegisters = [];
        [Browsable(false)]
        public SortedObservableCollection<ISortingValue> ListSelectedRegisters
        {
            get => _ListSelectedRegisters;
            set => SetProperty(ref _ListSelectedRegisters, value);
        }
        private void _ListSelectedRegisters_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (this.isOnOpeningEditor)
                return;
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
#if DEBUG
                        // Chack new item is not added yet
                        foreach (var t in e.NewItems)
                        {
                            var r = (Register)t;
                            var guid = r.Guid;
                            var j = -1;
                            for (int i = 0; i < r.ListObjectDocRefs.Count; i++)
                            {
                                if (r.ListObjectDocRefs[i].ForeignObjectGuid == guid)
                                {
                                    j = i;
                                    break;
                                }
                            }
                            Debug.Assert(j == -1);
                        }
#endif
                        foreach (var t in e.NewItems)
                        {
                            var r = (Register)t;
                            r.ListObjectDocRefs.Add(new ComplexRef("", this.Guid));
                        }
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                    {
                        foreach (var t in e.OldItems)
                        {
                            var r = (Register)t;
                            var j = -1;
                            for (int i = 0; i < r.ListObjectDocRefs.Count; i++)
                            {
                                if (r.ListObjectDocRefs[i].ForeignObjectGuid == this.Guid)
                                {
                                    j = i;
                                    break;
                                }
                            }
                            Debug.Assert(j >= 0);
                            r.ListObjectDocRefs.RemoveAt(j);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        private SortedObservableCollection<ISortingValue> _ListSelectedRegisters = [];
        #endregion Registers

        #region Mapping
        [Browsable(false)]
        public bool IsShowCompatible
        {
            get => _IsShowCompatible;
            set => SetProperty(ref _IsShowCompatible, value);
        }
        private bool _IsShowCompatible = true;
        [Browsable(false)]
        public RegisterDocToReg? RegisterDocToReg
        {
            get => _RegisterDocToReg;
            set => SetProperty(ref _RegisterDocToReg, value);
        }
        private RegisterDocToReg? _RegisterDocToReg;
        [Browsable(false)]
        public ISortingValue? SelectedReg
        {
            get => _SelectedReg;
            set
            {
                if (value == null)
                {
                    if (_SelectedReg != null)
                    {
                        var guid = ((IGuid)_SelectedReg).Guid;
                        foreach (var t in this.ListSelectedRegisters)
                        {
                            if (((IGuid)t).Guid == guid)
                            {
                                return;
                            }
                        }
                    }
                }
                if (SetProperty(ref _SelectedReg, value))
                {
                    if (_SelectedReg == null)
                    {
                        this.VisibilityTextRegNotSelected = Visibility.Visible;
                        this.VisibilityTextRegSelected = Visibility.Hidden;
                        this.RegisterDocToReg = null;
                    }
                    else
                    {
                        this.VisibilityTextRegNotSelected = Visibility.Hidden;
                        this.VisibilityTextRegSelected = Visibility.Visible;
                        this.TextRegSelected = $"Mapping register '{((IName)_SelectedReg).Name}' to '{this.Name}' document properties";
                        var r = (Register)_SelectedReg;
                        foreach (var t in r.ListDocMappings)
                        {
                            if (t.DocGuid == this.Guid)
                            {
                                this.RegisterDocToReg = t;
                                break;
                            }
                        }
                        Register.UpdateListMappings(r, this);
                    }
                }
            }
        }
        private ISortingValue? _SelectedReg;
        private readonly ObservableCollection<Property> fulListToMap = [];
        [Browsable(false)]
        public Visibility VisibilityTextRegNotSelected
        {
            get => _VisibilityTextRegNotSelected;
            set => SetProperty(ref _VisibilityTextRegNotSelected, value);
        }
        private Visibility _VisibilityTextRegNotSelected = Visibility.Visible;
        [Browsable(false)]
        public string TextRegSelected
        {
            get => _TextRegSelected;
            set => SetProperty(ref _TextRegSelected, value);
        }
        private string _TextRegSelected = string.Empty;
        [Browsable(false)]
        public Visibility VisibilityTextRegSelected
        {
            get => _VisibilityTextRegSelected;
            set => SetProperty(ref _VisibilityTextRegSelected, value);
        }
        private Visibility _VisibilityTextRegSelected = Visibility.Hidden;
        [Browsable(false)]
        public ObservableCollection<RegisterMappingRow> ListMappings
        {
            get => _ListMappings;
            set => SetProperty(ref _ListMappings, value);
        }
        private ObservableCollection<RegisterMappingRow> _ListMappings = [];
        #endregion Mapping

        #endregion Mapping Editor
    }
}
