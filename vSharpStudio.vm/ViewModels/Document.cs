﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Document:{Name,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class Document : ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup, IDbTable, INodeWithProperties, IRoleAccess, IDocumentAccessRoles
    {
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
            this.PropertyIdGuid = System.Guid.NewGuid().ToString();
            this.PropertyIdGuid = System.Guid.NewGuid().ToString();
            this.PropertyDocCodeGuid = System.Guid.NewGuid().ToString();
            this.PropertyDocDateGuid = System.Guid.NewGuid().ToString();
            this.PropertyVersionGuid = System.Guid.NewGuid().ToString();
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
            children.Add(this.GroupProperties, 0);
            children.Add(this.GroupDetails, 1);
            children.Add(this.GroupForms, 2);
            children.Add(this.GroupReports, 3);
            VmBindable.IsNotifyingStatic = true;
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
            this.SetSelected(node);
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
        public Detail AddDetails(string name)
        {
            var node = new Detail(this.GroupDetails) { Name = name };
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
        /// <summary>
        /// All included properties (shared and normal)
        /// Shared included first
        /// </summary>
        /// <param name="guidAppPrjGen"></param>
        /// <returns></returns>
        public IReadOnlyList<IProperty> GetIncludedPropertiesWithShared(string guidAppPrjGen, bool isSupportVersion, bool isExcludeSpecial = false)
        {
            var res = new List<IProperty>();
            var grd = this.ParentGroupListDocuments.ParentGroupDocuments;
            //var cfg = this.GetConfig();
            //var prp = cfg.Model.GetPropertyId(this.PropertyIdGuid);
            //res.Add(prp);
            if (!isExcludeSpecial)
                GetSpecialProperties(res, isSupportVersion);
            foreach (var t in grd.GroupSharedProperties.ListProperties)
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
        /// <summary>
        /// All properties (shared and normal)
        /// Shared included first
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<IProperty> GetPropertiesWithShared(bool isSupportVersion, bool isExcludeSpecial = false)
        {
            var res = new List<IProperty>();
            var grd = this.ParentGroupListDocuments.ParentGroupDocuments;
            if (!isExcludeSpecial)
                GetSpecialProperties(res, isSupportVersion);
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
        public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isSupportVersion, bool isExcludeSpecial = false)
        {
            var res = new List<IProperty>();
            if (!isExcludeSpecial)
                GetSpecialProperties(res, isSupportVersion);
            foreach (var t in this.GroupProperties.ListProperties)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    res.Add(t);
                }
            }
            return res;
        }
        public IReadOnlyList<IProperty> GetAllProperties(bool isSupportVersion)
        {
            var res = new List<IProperty>();
            GetSpecialProperties(res, isSupportVersion);
            foreach (var t in this.GroupProperties.ListProperties)
            {
                res.Add(t);
            }
            return res;
        }
        public void GetSpecialProperties(List<IProperty> res, bool isSupportVersion)
        {

            var model = this.ParentGroupListDocuments.ParentGroupDocuments.ParentModel;
            var prp = model.GetPropertyId(this.GroupProperties, this.PropertyIdGuid);
            res.Add(prp);
            if (isSupportVersion)
            {
                prp = model.GetPropertyVersion(this.GroupProperties, this.PropertyVersionGuid);
                res.Add(prp);
            }
            if (this.GetUseDocDateProperty())
            {
                prp = model.GetPropertyDocumentDate(this.GroupProperties, this.PropertyDocDateGuid);
                res.Add(prp);
            }
            if (this.GetUseDocCodeProperty())
            {
                switch (this.CodePropertySettings.Type)
                {
                    case EnumCodeType.AutoNumber:
                        throw new NotImplementedException();
                    case EnumCodeType.AutoText:
                        throw new NotImplementedException();
                    case EnumCodeType.Number:
                        prp = model.GetPropertyDocumentCodeInt(this.GroupProperties, this.PropertyDocCodeGuid, this.CodePropertySettings.Length);
                        break;
                    case EnumCodeType.Text:
                        prp = model.GetPropertyDocumentCodeString(this.GroupProperties, this.PropertyDocCodeGuid, this.CodePropertySettings.Length);
                        break;
                }
                res.Add(prp);
            }
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
        //public void GetSpecialProperties(List<IProperty> res, bool isSupportVersion)
        //{
        //    var model = this.ParentGroupListDocuments.ParentGroupDocuments.ParentModel;
        //    var prp = model.GetPropertyId(this.GroupProperties, this.PropertyIdGuid);
        //    res.Add(prp);
        //    if (isSupportVersion)
        //    {
        //        prp = model.GetPropertyVersion(this.GroupProperties, this.PropertyVersionGuid);
        //        res.Add(prp);
        //    }
        //    //var model = this.ParentCatalog.ParentGroupListCatalogs.ParentModel;
        //    //var prp = model.GetPropertyId(this.GroupProperties, this.PropertyIdGuid);
        //    //res.Add(prp);
        //    //prp = model.GetPropertyRefParent(this.GroupProperties, this.PropertyRefSelfGuid, "RefTreeParent", true);
        //    //res.Add(prp);
        //    //prp = model.GetPropertyIsOpen(this.GroupProperties, this.PropertyIsOpenGuid);
        //    //res.Add(prp);
        //    //if (this.ParentCatalog.UseFolderTypeExplicitly)
        //    //{
        //    //    prp = model.GetPropertyIsFolder(this.GroupProperties, this.PropertyIsFolderGuid);
        //    //    res.Add(prp);
        //    //}
        //    ////if (isSupportVersion)
        //    ////{
        //    ////    prp = model.GetPropertyVersion(this.GroupProperties, this.Folder.PropertyVersionGuid);
        //    ////    res.Add(prp);
        //    ////}
        //    //if (this.GetUseCodeProperty())
        //    //{
        //    //    prp = this.GetCodeProperty(model.ParentConfig);
        //    //    res.Add(prp);
        //    //}
        //    //if (this.GetUseNameProperty())
        //    //{
        //    //    prp = model.GetPropertyCatalogName(this.GroupProperties, this.PropertyNameGuid, this.MaxNameLength);
        //    //    res.Add(prp);
        //    //}
        //    //if (this.GetUseDescriptionProperty())
        //    //{
        //    //    prp = model.GetPropertyCatalogDescription(this.GroupProperties, this.PropertyDescriptionGuid, this.MaxDescriptionLength);
        //    //    res.Add(prp);
        //    //}
        //}
        public ViewFormData GetFormViewData(FormType formType, string guidAppPrjGen)
        {
            ViewListData? viewListData = null;
            var model = this.ParentGroupListDocuments.ParentGroupDocuments.ParentModel;
            Form form = (from p in this.GroupForms.ListForms where p.EnumFormType == formType select p).Single();
            var pId = model.GetPropertyId(this.GroupProperties, this.PropertyIdGuid);
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
                this.GetForm(FormType.ListNarrow, guidAppPrjGen),
                this.GetForm(FormType.ListWide, guidAppPrjGen)
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
                this.GetPropertyName(() => this.Parent),
                this.GetPropertyName(() => this.Children)
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
        public bool GetUseDocCodeProperty()
        {
            if (this.UseDocCodeProperty == EnumUseType.Yes)
                return true;
            if (this.UseDocCodeProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListDocuments.ParentGroupDocuments.GetUseDocCodeProperty();
        }
        public bool GetUseDocDateProperty()
        {
            if (this.UseDocDateProperty == EnumUseType.Yes)
                return true;
            if (this.UseDocDateProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListDocuments.ParentGroupDocuments.GetUseDocDateProperty();
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
    }
}
