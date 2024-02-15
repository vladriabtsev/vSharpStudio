using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Google.Protobuf.WellKnownTypes;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class Document : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup, INodeWithProperties, IRoleAccess, IDocumentAccessRoles
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" props:{GroupProperties.ListProperties.Count} details:{GroupDetails.ListDetails.Count}";
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
            sb.Append(this.ParentGroupListDocuments.ParentGroupDocuments.ParentModel.PropertyDocNumberName);
            sb.Append(",nq}");
            sb.Append(" Date:{");
            sb.Append(this.ParentGroupListDocuments.ParentGroupDocuments.ParentModel.PropertyDocDateName);
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
        public new string IconName { get { return "iconDiagnosticesFile"; } }
        //protected override string GetNodeIconName() { return "iconDiagnosticesFile"; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            this._IndexUniqueDocNumberGuid = System.Guid.NewGuid().ToString();
            this._IndexYearDocNumberGuid = System.Guid.NewGuid().ToString();
            this._IndexQuaterDocNumberGuid = System.Guid.NewGuid().ToString();
            this._IndexMonthDocNumberGuid = System.Guid.NewGuid().ToString();
            this._IndexWeekDocNumberGuid = System.Guid.NewGuid().ToString();
            this._IndexDayDocNumberGuid = System.Guid.NewGuid().ToString();
            this._IndexNotUniqueDocNumberGuid = System.Guid.NewGuid().ToString();

            this._SequenceGuid = "";
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
            children.Add(this.GroupProperties, 0);
            children.Add(this.GroupDetails, 1);
            children.Add(this.GroupForms, 2);
            children.Add(this.GroupReports, 3);
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
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
            this.GroupProperties.AddAllAppGenSettingsVmsToNode();
            this.GroupDetails.AddAllAppGenSettingsVmsToNode();
            this.GroupForms.AddAllAppGenSettingsVmsToNode();
            this.GroupReports.AddAllAppGenSettingsVmsToNode();
        }
        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListDocuments.ListDocuments.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Document?)this.ParentGroupListDocuments.ListDocuments.GetPrev(this);
            if (prev == null)
                return;
            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupListDocuments.ListDocuments.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListDocuments.ListDocuments.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Document?)this.ParentGroupListDocuments.ListDocuments.GetNext(this);
            if (next == null)
                return;
            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupListDocuments.ListDocuments.MoveDown(this);
            this.SetSelected(this);
        }

        public override ITreeConfigNode NodeAddClone()
        {
            var node = Document.Clone(this.ParentGroupListDocuments, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupListDocuments.Add(node);
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Document(this.ParentGroupListDocuments);
            this.ParentGroupListDocuments.Add(node);
            this.GetUniqueName(Defaults.DocumentName, node, this.ParentGroupListDocuments.ListDocuments);
            var model = this.ParentGroupListDocuments.ParentGroupDocuments.ParentModel;
            node.ShortId = model.LastTypeShortIdForNode();
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
                DataTypeEnum = EnumDataType.CATALOG,
                IsNullable = isNullable,
                ObjectRef = new FkComplexRef(catGuid)
            };
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
                return this.ParentGroupListDocuments.ParentGroupDocuments.GroupSharedProperties.ListProperties.Count > 0;
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
                GetSpecialProperties(res, isOptimistic);
            foreach (var t in grd.GroupSharedProperties.ListProperties)
            {
                res.Add(t);
            }
            foreach (var t in this.GroupProperties.ListProperties)
            {
                res.Add(t);
            }
            return res;
        }
        public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isOptimistic, bool isExcludeSpecial = false)
        {
            var res = new List<IProperty>();
            if (!isExcludeSpecial)
                GetSpecialProperties(res, isOptimistic);
            foreach (var t in this.Cfg.Model.GroupRelations.GroupListOneToOneRelations.ListRelations)
            {
                if (t.GuidObj1 == this.Guid && (t.RefType == EnumOneToOneRefType.ONE_TO_ONE_REF_BOTH_DIRECTIONS || t.RefType == EnumOneToOneRefType.ONE_TO_ONE_REF_FROM_FIRST_TO_SECOND_ONLY))
                {
                    Debug.Assert(t.GuidObj2 != null);
                    if (t.RefObj2Type == EnumRelationConfigType.RelConfigTypeCatalogs)
                    {
                        res.Add(this.Cfg.Model.GetPropertyCatalog(this, t.RefObj2PropGuid, t.Name, t.GuidObj2, (uint)res.Count, t.IsRelationReferenceNullable));
                    }
                    else if (t.RefObj2Type == EnumRelationConfigType.RelConfigTypeDocuments)
                    {
                        res.Add(this.Cfg.Model.GetPropertyDocument(this, t.RefObj2PropGuid, t.Name, t.GuidObj2, (uint)res.Count, t.IsRelationReferenceNullable));
                    }
                    else
                        throw new NotImplementedException();
                }
                if (t.GuidObj2 == this.Guid && (t.RefType == EnumOneToOneRefType.ONE_TO_ONE_REF_BOTH_DIRECTIONS || t.RefType == EnumOneToOneRefType.ONE_TO_ONE_REF_FROM_SECOND_TO_FIRST_ONLY))
                {
                    Debug.Assert(t.GuidObj1 != null);
                    if (t.RefObj1Type == EnumRelationConfigType.RelConfigTypeCatalogs)
                    {
                        res.Add(this.Cfg.Model.GetPropertyCatalog(this, t.RefObj1PropGuid, t.Name, t.GuidObj1, (uint)res.Count, t.IsRelationReferenceNullable));
                    }
                    else if (t.RefObj1Type == EnumRelationConfigType.RelConfigTypeDocuments)
                    {
                        res.Add(this.Cfg.Model.GetPropertyDocument(this, t.RefObj1PropGuid, t.Name, t.GuidObj1, (uint)res.Count, t.IsRelationReferenceNullable));
                    }
                    else
                        throw new NotImplementedException();
                }
            }
            foreach (var t in this.ParentGroupListDocuments.ParentGroupDocuments.GroupSharedProperties.ListProperties)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    t.ComplexObjectName = "SharedDocProperties";
                    res.Add(t);
                }
            }
            foreach (var t in this.GroupProperties.ListProperties)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    res.Add(t);
                }
            }
            return res;
        }
        public IReadOnlyList<IProperty> GetIncludedPropertiesWithoutShared(string guidAppPrjGen, bool isOptimistic, bool isExcludeSpecial = false)
        {
            var res = new List<IProperty>();
            if (!isExcludeSpecial)
                GetSpecialProperties(res, isOptimistic);
            foreach (var t in this.Cfg.Model.GroupRelations.GroupListOneToOneRelations.ListRelations)
            {
                if (t.GuidObj1 == this.Guid && (t.RefType == EnumOneToOneRefType.ONE_TO_ONE_REF_BOTH_DIRECTIONS || t.RefType == EnumOneToOneRefType.ONE_TO_ONE_REF_FROM_FIRST_TO_SECOND_ONLY))
                {
                    Debug.Assert(t.GuidObj2 != null);
                    if (t.RefObj2Type == EnumRelationConfigType.RelConfigTypeCatalogs)
                    {
                        res.Add(this.Cfg.Model.GetPropertyCatalog(this, t.RefObj2PropGuid, t.Name, t.GuidObj2, (uint)res.Count, t.IsRelationReferenceNullable));
                    }
                    else if (t.RefObj2Type == EnumRelationConfigType.RelConfigTypeDocuments)
                    {
                        res.Add(this.Cfg.Model.GetPropertyDocument(this, t.RefObj2PropGuid, t.Name, t.GuidObj2, (uint)res.Count, t.IsRelationReferenceNullable));
                    }
                    else
                        throw new NotImplementedException();
                }
                if (t.GuidObj2 == this.Guid && (t.RefType == EnumOneToOneRefType.ONE_TO_ONE_REF_BOTH_DIRECTIONS || t.RefType == EnumOneToOneRefType.ONE_TO_ONE_REF_FROM_SECOND_TO_FIRST_ONLY))
                {
                    Debug.Assert(t.GuidObj1 != null);
                    if (t.RefObj1Type == EnumRelationConfigType.RelConfigTypeCatalogs)
                    {
                        res.Add(this.Cfg.Model.GetPropertyCatalog(this, t.RefObj1PropGuid, t.Name, t.GuidObj1, (uint)res.Count, t.IsRelationReferenceNullable));
                    }
                    else if (t.RefObj1Type == EnumRelationConfigType.RelConfigTypeDocuments)
                    {
                        res.Add(this.Cfg.Model.GetPropertyDocument(this, t.RefObj1PropGuid, t.Name, t.GuidObj1, (uint)res.Count, t.IsRelationReferenceNullable));
                    }
                    else
                        throw new NotImplementedException();
                }
            }
            foreach (var t in this.GroupProperties.ListProperties)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    res.Add(t);
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
            var prp = model.GetPropertyRef(this.GroupProperties, this.Cfg.Model.PropertyIdGuid, this.Cfg.Model.PKeyName, 0, false, true);
            res.Add(prp);
            if (isOptimistic)
            {
                prp = model.GetPropertyVersion(this.GroupProperties, this.Cfg.Model.PropertyVersionGuid);
                res.Add(prp);
            }
            //prp = model.GetPropertyDocumentDate(this.GroupProperties, this.Cfg.Model.PropertyDocDateGuid);
            //res.Add(prp);
            if (!string.IsNullOrWhiteSpace(this.SequenceGuid))
            {
                var seq = this.Sequence;
                switch (seq.SequenceType)
                {
                    case EnumCodeType.Number:
                        prp = model.GetPropertyDocNumberInt(this.GroupProperties, this.Cfg.Model.PropertyDocNumberGuid,
                            seq.MaxSequenceLength);
                        break;
                    case EnumCodeType.Text:
                        prp = model.GetPropertyDocNumberString(this.GroupProperties, this.Cfg.Model.PropertyDocNumberGuid,
                            seq.MaxSequenceLength + (uint)seq.Prefix.Length);
                        break;
                    default:
                        throw new NotImplementedException();
                }
                res.Add(prp);
                switch (seq.ScopeOfUnique)
                {
                    case EnumDocNumberUniqueScope.DOC_UNIQUE_YEAR:
                        prp = model.GetPropertyDocNumberUniqueScopeHelper(this.GroupProperties, this.IndexYearDocNumberGuid);
                        res.Add(prp);
                        break;
                    case EnumDocNumberUniqueScope.DOC_UNIQUE_QUATER:
                        prp = model.GetPropertyDocNumberUniqueScopeHelper(this.GroupProperties, this.IndexQuaterDocNumberGuid);
                        res.Add(prp);
                        break;
                    case EnumDocNumberUniqueScope.DOC_UNIQUE_MONTH:
                        prp = model.GetPropertyDocNumberUniqueScopeHelper(this.GroupProperties, this.IndexMonthDocNumberGuid);
                        res.Add(prp);
                        break;
                    case EnumDocNumberUniqueScope.DOC_UNIQUE_WEEK:
                        prp = model.GetPropertyDocNumberUniqueScopeHelper(this.GroupProperties, this.IndexWeekDocNumberGuid);
                        res.Add(prp);
                        break;
                    case EnumDocNumberUniqueScope.DOC_UNIQUE_DAY:
                        prp = model.GetPropertyDocNumberUniqueScopeHelper(this.GroupProperties, this.IndexDayDocNumberGuid);
                        res.Add(prp);
                        break;
                    case EnumDocNumberUniqueScope.DOC_UNIQUE_FOREVER:
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
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
        /// <summary>
        /// Only shared properties
        /// </summary>
        /// <param name="guidAppPrjGen"></param>
        /// <returns></returns>
        public IReadOnlyList<IProperty> GetIncludedSharedProperties(string guidAppPrjGen)
        {
            var res = new List<IProperty>();
            var grd = this.ParentGroupListDocuments.ParentGroupDocuments;
            foreach (var t in grd.GroupSharedProperties.ListProperties)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    t.ComplexObjectName = "SharedDocProperties";
                    res.Add(t);
                }
            }
            return res;
        }
        public ViewFormData GetFormViewData(FormType formType, string guidAppPrjGen)
        {
            ViewListData? viewListData = null;
            var model = this.ParentGroupListDocuments.ParentGroupDocuments.ParentModel;
            Form form = (from p in this.GroupForms.ListForms where p.EnumFormType == formType select p).Single();
            var pId = model.GetPropertyPkId(this.GroupProperties, this.Cfg.Model.PropertyIdGuid);
            viewListData = new ViewListData(pId);
            var lst = SelectViewProperties(formType, this.GroupProperties.ListProperties, form.ListGuidViewProperties, guidAppPrjGen);
            viewListData.ListViewProperties.AddRange(lst);
            return new ViewFormData(null, viewListData);
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
            return lst.ToArray();
        }
        public bool IsGridSortableGet()
        {
            if (this.IsGridSortable == EnumUseType.Yes)
                return true;
            if (this.IsGridSortable == EnumUseType.No)
                return false;
            return this.ParentGroupListDocuments.ParentGroupDocuments.IsGridSortableGet();
        }
        public bool IsGridFilterableGet()
        {
            if (this.IsGridFilterable == EnumUseType.Yes)
                return true;
            if (this.IsGridFilterable == EnumUseType.No)
                return false;
            return this.ParentGroupListDocuments.ParentGroupDocuments.IsGridFilterableGet();
        }
        public bool IsGridSortableCustomGet()
        {
            if (this.IsGridSortableCustom == EnumUseType.Yes)
                return true;
            if (this.IsGridSortableCustom == EnumUseType.No)
                return false;
            return this.ParentGroupListDocuments.ParentGroupDocuments.IsGridSortableCustomGet();
        }

        #region Roles
        public object GetRoleAccess(IRole role)
        {
            if (!this.dicDocumentAccess.ContainsKey(role.Guid))
            {
                var rca = new RoleDocumentAccess() { Guid = role.Guid };
                this.ListRoleDocumentAccessSettings.Add(rca);
                this.dicDocumentAccess[role.Guid] = rca;
            }
            return dicDocumentAccess[role.Guid];
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
        internal Dictionary<string, RoleDocumentAccess> dicDocumentAccess = new();
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
        public EnumPropertyAccess GetRolePropertyAccess(IRole role)
        {
            var ra = EnumDocumentAccess.D_BY_PARENT;
            if (this.dicDocumentAccess.TryGetValue(role.Guid, out var r))
                ra = r.EditAccess;
            if (ra == EnumDocumentAccess.D_BY_PARENT)
                ra = this.ParentGroupListDocuments.GetRoleDocumentAccess(role);
            Debug.Assert(ra != EnumDocumentAccess.D_BY_PARENT);
            switch (ra)
            {
                case EnumDocumentAccess.D_HIDE:
                    return EnumPropertyAccess.P_HIDE;
                case EnumDocumentAccess.D_VIEW:
                    return EnumPropertyAccess.P_VIEW;
                case EnumDocumentAccess.D_EDIT:
                case EnumDocumentAccess.D_MARK_DEL:
                case EnumDocumentAccess.D_UNPOST:
                case EnumDocumentAccess.D_POST:
                    return EnumPropertyAccess.P_EDIT;
                default:
                    throw new NotImplementedException();
            }
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

        public IDocumentEnumeratorSequence? Sequence { get { if (!this.Cfg.DicNodes.ContainsKey(this.SequenceGuid)) return null; return (IDocumentEnumeratorSequence)this.Cfg.DicNodes[this.SequenceGuid]; } }
    }
}
