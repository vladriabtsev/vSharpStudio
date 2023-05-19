using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} properties:{GroupProperties.ListProperties.Count,nq} details:{GroupDetails.ListDetails.Count,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class Detail : ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, ICanAddNode, IEditableNode, IEditableNodeGroup, IDbTable, INodeWithProperties, IRoleAccess, ICatalogDetailAccessRoles, ILayoutParameters
    {
        [Browsable(false)]
        public GroupListDetails ParentGroupListDetails { get { Debug.Assert(this.Parent != null); return (GroupListDetails)this.Parent; } }
        [Browsable(false)]
        public IGroupListDetails ParentGroupListDetailsI { get { Debug.Assert(this.Parent != null); return (IGroupListDetails)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListDetails.Children;
        }
        #endregion ITree

        [Browsable(false)]
        new public string IconName { get { return "iconFolder"; } }
        //protected override string GetNodeIconName() { return "iconFolder"; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            this.IsIndexFk = true;

            this.PropertyIdGuid = System.Guid.NewGuid().ToString();
            this.PropertyRefParentGuid = System.Guid.NewGuid().ToString();
            this.PropertyCodeGuid = System.Guid.NewGuid().ToString();
            this.PropertyNameGuid = System.Guid.NewGuid().ToString();
            this.PropertyDescriptionGuid = System.Guid.NewGuid().ToString();
            this.PropertyVersionGuid = System.Guid.NewGuid().ToString();
            this.ViewListWideGuid = System.Guid.NewGuid().ToString();
            this.ViewListNarrowGuid = System.Guid.NewGuid().ToString();
            this.PropertyVersionGuid = System.Guid.NewGuid().ToString();
            var glp = (this.ParentGroupListDetails.Parent as INodeWithProperties);
            Debug.Assert(glp != null);
            this.Position = glp.GroupProperties.GetNextPosition();
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
            children.Add(this.GroupProperties, 1);
            children.Add(this.GroupDetails, 2);
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
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListDetails.ListDetails.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }
        public override void NodeUp()
        {
            var prev = (Detail?)this.ParentGroupListDetails.ListDetails.GetPrev(this);
            if (prev == null)
                return;
            this.SetSelected(prev);
        }
        public override void NodeMoveUp()
        {
            this.ParentGroupListDetails.ListDetails.MoveUp(this);
            this.SetSelected(this);
        }
        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListDetails.ListDetails.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }
        public override void NodeDown()
        {
            var next = (Detail?)this.ParentGroupListDetails.ListDetails.GetNext(this);
            if (next == null)
                return;
            this.SetSelected(next);
        }
        public override void NodeMoveDown()
        {
            this.ParentGroupListDetails.ListDetails.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            Debug.Assert(this.Parent != null);
            var node = Detail.Clone(this.Parent, this, true, true);
            this.ParentGroupListDetails.Add(node);
            var glp = (this.ParentGroupListDetails.Parent as INodeWithProperties);
            Debug.Assert(glp != null);
            node.Position = glp.GroupProperties.GetNextPosition();
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            Debug.Assert(this.Parent != null);
            var node = new Detail(this.Parent);
            this.ParentGroupListDetails.Add(node);
            var glp = (this.ParentGroupListDetails.Parent as INodeWithProperties);
            Debug.Assert(glp != null);
            node.Position = glp.GroupProperties.GetNextPosition();
            this.GetUniqueName(Defaults.DetailName, node, this.ParentGroupListDetails.ListDetails);
            this.SetSelected(node);
            return node;
        }
        public Detail AddDetails(string name)
        {
            var node = new Detail(this.GroupDetails) { Name = name };
            this.GroupDetails.NodeAddNewSubNode(node);
            var glp = (this.ParentGroupListDetails.Parent as INodeWithProperties);
            Debug.Assert(glp != null);
            node.Position = glp.GroupProperties.GetNextPosition();
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
        public IEnumerable<ITreeConfigNode> GetParentList()
        {
            return this.ParentGroupListDetails.ListDetails;
        }
        public void Remove()
        {
            this.ParentGroupListDetails.ListDetails.Remove(this);
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
        public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isSupportVersion, bool isExcludeSpecial = false)
        {
            var res = new List<IProperty>();
            if (!isExcludeSpecial)
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
        public void GetSpecialProperties(List<IProperty> res, bool isSupportVersion)
        {
            string parentTable = "";
            if (this.ParentGroupListDetails.Parent is Detail dt)
            {
                parentTable = dt.CompositeName;
            }
            else if (this.ParentGroupListDetails.Parent is Catalog c)
            {
                parentTable = c.CompositeName;
            }
            else if (this.ParentGroupListDetails.Parent is CatalogFolder cf)
            {
                parentTable = cf.CompositeName;
            }
            else if (this.ParentGroupListDetails.Parent is Document d)
            {
                parentTable = d.CompositeName;
            }
            else
                throw new NotImplementedException();
            var prp = this.Cfg.Model.GetPropertyId(this.GroupProperties, this.PropertyIdGuid);
            res.Add(prp);
            prp = this.Cfg.Model.GetPropertyRefParent(this.GroupProperties, this.PropertyRefParentGuid, "Ref" + parentTable);
            res.Add(prp);
            if (isSupportVersion)
            {
                prp = this.Cfg.Model.GetPropertyVersion(this.GroupProperties, this.PropertyVersionGuid);
                res.Add(prp);
            }
        }
        public void GetNormalProperties(List<IProperty> res)
        {
            var model = this.Cfg.Model;
            foreach (var t in this.GroupProperties.ListProperties)
            {
                res.Add(t);
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
        public ViewFormData GetFormViewData(FormType formType, string guidAppPrjGen)
        {
            ViewListData? viewListData = null;
            Form form = (from p in this.GroupForms.ListForms where p.EnumFormType == formType select p).Single();
            var pId = this.Cfg.Model.GetPropertyId(this.GroupProperties, this.PropertyIdGuid);
            viewListData = new ViewListData(pId);
            var lst = this.SelectViewProperties(formType, this.GroupProperties.ListProperties, form.ListGuidViewProperties, guidAppPrjGen);
            viewListData.ListViewProperties.AddRange(lst);
            return new ViewFormData(null, viewListData);
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
            return this.ParentGroupListDetails.GetIsGridSortable();
        }
        public bool IsGridFilterableGet()
        {
            if (this.IsGridFilterable == EnumUseType.Yes)
                return true;
            if (this.IsGridFilterable == EnumUseType.No)
                return false;
            return this.ParentGroupListDetails.GetIsGridFilterable();
        }
        public bool IsGridSortableCustomGet()
        {
            if (this.IsGridSortableCustom == EnumUseType.Yes)
                return true;
            if (this.IsGridSortableCustom == EnumUseType.No)
                return false;
            return this.ParentGroupListDetails.GetIsGridSortableCustom();
        }
        public bool GetUseCodeProperty()
        {
            if (this.UseCodeProperty == EnumUseType.Yes)
                return true;
            if (this.UseCodeProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListDetails.GetUseCodeProperty();
        }
        public bool GetUseNameProperty()
        {
            if (this.UseNameProperty == EnumUseType.Yes)
                return true;
            if (this.UseNameProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListDetails.GetUseNameProperty();
        }
        public bool GetUseDescriptionProperty()
        {
            if (this.UseDescriptionProperty == EnumUseType.Yes)
                return true;
            if (this.UseDescriptionProperty == EnumUseType.No)
                return false;
            return this.ParentGroupListDetails.GetUseDescriptionProperty();
        }

        #region Roles
        public object GetRoleAccess(IRole role)
        {
            if (!this.dicDetailAccess.ContainsKey(role.Guid))
            {
                var rca = new RoleDetailAccess() { Guid = role.Guid };
                this.ListRoleDetailAccessSettings.Add(rca);
                this.dicDetailAccess[role.Guid] = rca;
            }
            return dicDetailAccess[role.Guid];
        }
        public void SetRoleAccess(IRole role, EnumCatalogDetailAccess? edit, EnumPrintAccess? print)
        {
            Debug.Assert(role != null);
            Debug.Assert(dicDetailAccess.ContainsKey(role.Guid));
            if (edit.HasValue)
                dicDetailAccess[role.Guid].EditAccess = edit.Value;
            if (print.HasValue)
                dicDetailAccess[role.Guid].PrintAccess = print.Value;
        }
        internal Dictionary<string, RoleDetailAccess> dicDetailAccess = new Dictionary<string, RoleDetailAccess>();
        public void InitRoles()
        {
            foreach (var tt in this.ListRoleDetailAccessSettings)
            {
                this.dicDetailAccess[tt.Guid] = tt;
            }
            foreach (var t in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (!this.dicDetailAccess.ContainsKey(t.Guid))
                {
                    var rca = new RoleDetailAccess() { Guid = t.Guid };
                    this.dicDetailAccess[t.Guid] = rca;
                }
            }
        }
        public void InitRoleAdd(IRole role)
        {
            var rca = new RoleDetailAccess() { Guid = role.Guid };
            this.ListRoleDetailAccessSettings.Add(rca);
            this.dicDetailAccess[rca.Guid] = rca;
        }
        public void InitRoleRemove(IRole role)
        {
            for (int i = 0; i < this.ListRoleDetailAccessSettings.Count; i++)
            {
                if (this.ListRoleDetailAccessSettings[i].Guid == role.Guid)
                {
                    this.ListRoleDetailAccessSettings.RemoveAt(i);
                    break;
                }
            }
            this.dicDetailAccess.Remove(role.Guid);
        }
        public EnumPropertyAccess GetRolePropertyAccess(IRole role)
        {
            EnumCatalogDetailAccess ra = this.dicDetailAccess[role.Guid].EditAccess;
            if (ra == EnumCatalogDetailAccess.C_BY_PARENT)
            {
                ra = this.ParentGroupListDetails.GetRoleDetailAccess(role);
            }
            switch (ra)
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
        public EnumPrintAccess GetRolePropertyPrint(IRole role)
        {
            EnumPrintAccess ra = EnumPrintAccess.PR_BY_PARENT;
            if (!this.dicDetailAccess.TryGetValue(role.Guid, out var r) || r.PrintAccess == EnumPrintAccess.PR_BY_PARENT)
            {
                if (this.Parent is GroupListDetails gd)
                {
                    ra = gd.GetRoleDetailPrint(role);
                }
                else if (this.Parent is Catalog c)
                {
                    ra = c.GetRoleCatalogPrint(role);
                }
                else if (this.Parent is Document d)
                {
                    ra = d.GetRoleDocumentPrint(role);
                }
                else
                    throw new NotImplementedException();
            }
            Debug.Assert(ra != EnumPrintAccess.PR_BY_PARENT);
            return ra;
        }
        public EnumCatalogDetailAccess GetRoleDetailAccess(IRole role)
        {
            if (this.dicDetailAccess.TryGetValue(role.Guid, out var r) && r.EditAccess != EnumCatalogDetailAccess.C_BY_PARENT)
                return r.EditAccess;
            if (this.Parent is Detail dd)
                return dd.GetRoleDetailAccess(role);
            else if (this.Parent is GroupListDetails gd)
                return gd.GetRoleDetailAccess(role);
            else if (this.Parent is Catalog c)
                return c.GetRoleCatalogAccess(role);
            else if (this.Parent is Document d)
            {
                var ra = d.GetRoleDocumentAccess(role);
                switch (ra)
                {
                    case EnumDocumentAccess.D_BY_PARENT:
                        throw new NotImplementedException();
                    case EnumDocumentAccess.D_HIDE:
                        return EnumCatalogDetailAccess.C_HIDE;
                    case EnumDocumentAccess.D_VIEW:
                        return EnumCatalogDetailAccess.C_VIEW;
                    case EnumDocumentAccess.D_EDIT:
                        return EnumCatalogDetailAccess.C_EDIT;
                    case EnumDocumentAccess.D_MARK_DEL:
                        return EnumCatalogDetailAccess.C_MARK_DEL;
                    case EnumDocumentAccess.D_POST:
                    case EnumDocumentAccess.D_UNPOST:
                        return EnumCatalogDetailAccess.C_EDIT;
                    default:
                        throw new NotImplementedException();
                }
            }
            else if (this.Parent is CatalogFolder cf)
                return cf.ParentCatalog.GetRoleCatalogAccess(role);
            else
                throw new NotImplementedException();
        }
        public EnumPrintAccess GetRoleDetailPrint(IRole role)
        {
            if (this.dicDetailAccess.TryGetValue(role.Guid, out var r) && r.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                return r.PrintAccess;
            if (this.Parent is GroupListDetails gd)
                return gd.GetRoleDetailPrint(role);
            else if (this.Parent is Catalog c)
                return c.GetRoleCatalogPrint(role);
            else if (this.Parent is Document d)
                return d.GetRoleDocumentPrint(role);
            else
                throw new NotImplementedException();
        }
        public IReadOnlyList<string> GetRolesByAccess(EnumCatalogDetailAccess access)
        {
            var roles = new List<string>();
            foreach (var role in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (GetRoleDetailAccess(role) == access)
                    roles.Add(role.Name);
            }
            return roles;
        }
        public IReadOnlyList<string> GetRolesByAccess(EnumPrintAccess access)
        {
            var roles = new List<string>();
            foreach (var role in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (GetRoleDetailPrint(role) == access)
                    roles.Add(role.Name);
            }
            return roles;
        }
        #endregion Roles
    }
}
