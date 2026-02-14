using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CommunityToolkit.Diagnostics;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class Detail : ICanGoRight, ICanGoLeft, INodeGenSettings, ICanAddNode, IEditableNode, IEditableNodeGroup, INodeWithProperties,
        ILayoutParameters
    {
        public override string NameShortId
        {
            get
            {
                var sb = new StringBuilder();
                this.GetNodeShortId(sb, this);
                return sb.ToString();
            }
        }
        private void GetNodeShortId(StringBuilder sb, ITreeConfigNode n)
        {
            ITreeConfigNode? p;
            if (n is Detail t)
            {
                p = t.ParentGroupListDetails.Parent;
                Debug.Assert(p != null);
                this.GetNodeShortId(sb, p);
                sb.Append("t");
                sb.Append(t.ShortId);
            }
            else if (n is Catalog c)
            {
                sb.Append(c.NameShortId);
                return;
            }
            else if (n is Document d)
            {
                sb.Append(d.NameShortId);
                return;
            }
            else if (n is CatalogFolder cf)
            {
                sb.Append(cf.NameShortId);
                return;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" props:{GroupProperties.ListProperties.Count} details:{GroupDetails.ListDetails.Count}";
        }
        public string GetDebuggerDisplay(bool isOptimistic)
        {
            var model = this.Cfg.Model;
            var sb = new StringBuilder();
            sb.Append(this.Name);
            sb.Append(", ");
            sb.Append(model.PKeyName);
            sb.Append(":{");
            sb.Append(model.PKeyName);
            sb.Append(",nq}");
            if (isOptimistic)
            {
                sb.Append(" RecVer:{");
                sb.Append(model.RecordVersionFieldName);
                sb.Append(",nq}");
            }
            sb.Append(" Ref");
            Debug.Assert(this.ParentGroupListDetails.Parent != null);
            var compName = ((ICompositeName)this.ParentGroupListDetails.Parent).CompositeName;
            sb.Append(compName);
            sb.Append(":{Ref");
            sb.Append(compName);
            sb.Append(",nq}");
            return sb.ToString();
        }
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
        public new string IconName { get { return "iconFolder"; } }
        //protected override string GetNodeIconName() { return "iconFolder"; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            this._IsIndexFk = true;
            this._ViewListDatagridGuid = System.Guid.NewGuid().ToString();
            this._ViewListComboBoxGuid = System.Guid.NewGuid().ToString();
            var glp = (this.ParentGroupListDetails.Parent as INodeWithProperties);
            Debug.Assert(glp != null);
            this._Position = glp.GroupProperties.GetNextPosition();

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
        protected override ConfigNodesCollection<Detail>? GetParentCollection() { return this.ParentGroupListDetails.ListDetails; }
        public void OnAdded()
        {
            this.AddOrRestoreAllAppGenSettingsVmsToNode();
            this.GroupProperties.AddOrRestoreAllAppGenSettingsVmsToNode();
            this.GroupDetails.AddOrRestoreAllAppGenSettingsVmsToNode();
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override ITreeConfigNode NodeAddClone()
        {
            Debug.Assert(this.Parent != null);
            var node = Detail.Clone(this.Parent, this, true, true);
            this.ParentGroupListDetails.ListDetails.Add(node, this);
            var glp = (this.ParentGroupListDetails.Parent as INodeWithProperties);
            Debug.Assert(glp != null);
            node.Position = glp.GroupProperties.GetNextPosition();
            this.Name = this.Name + "2";
            var model = this.Cfg.Model;
            node.ShortId = ++this.ParentGroupListDetails.LastShortId;
            node.ShortRefId = model.LastTypeShortRefIdForNode(node, node.ShortId);
            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            Debug.Assert(this.Parent != null);
            var node = new Detail(this.Parent);
            this.ParentGroupListDetails.ListDetails.Add(node, this);
            var glp = (this.ParentGroupListDetails.Parent as INodeWithProperties);
            Debug.Assert(glp != null);
            node.Position = glp.GroupProperties.GetNextPosition();
            this.GetUniqueName(Defaults.DetailName, node, this.ParentGroupListDetails.ListDetails);
            var model = this.Cfg.Model;
            node.ShortId = ++this.ParentGroupListDetails.LastShortId;
            node.ShortRefId = model.LastTypeShortRefIdForNode(node, node.ShortId);
            this.SetSelected(node);
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
            var glp = (this.ParentGroupListDetails.Parent as INodeWithProperties);
            Debug.Assert(glp != null);
            node.Position = glp.GroupProperties.GetNextPosition();
            var model = this.Cfg.Model;
            node.ShortId = ++this.ParentGroupListDetails.LastShortId;
            node.ShortRefId = model.LastTypeShortRefIdForNode(node, node.ShortId);
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
        public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isOptimistic, bool isExcludeSpecial = false)
        {
            var res = new List<IProperty>();
            if (!isExcludeSpecial)
                this.GetSpecialProperties(res, isOptimistic);
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
            var model = this.Cfg.Model;
            var prp = model.GetPropertySpecial(this, EnumSpecialPropertyType.RECORD_ID);
            res.Add(prp);

            if (this.ParentGroupListDetails.Parent is Catalog c)
                prp = model.GetPropertySpecial(this, EnumSpecialPropertyType.REF_DETAIL_TO_PARENT_CATALOG, false, c);
            else if (this.ParentGroupListDetails.Parent is Detail dt)
                prp = model.GetPropertySpecial(this, EnumSpecialPropertyType.REF_DETAIL_TO_PARENT_DETAIL, false, dt);
            else if (this.ParentGroupListDetails.Parent is Document d) // Timeline is parent record
                prp = model.GetPropertySpecial(this, EnumSpecialPropertyType.REF_DETAIL_TO_PARENT_DOCUMENT, false, d);
            else if (this.ParentGroupListDetails.Parent is CatalogFolder cf)
                prp = model.GetPropertySpecial(this, EnumSpecialPropertyType.REF_DETAIL_TO_PARENT_CATALOG_FOLDER, false, cf);
            else
                ThrowHelper.ThrowNotSupportedException();
            res.Add(prp);

            if (isOptimistic)
            {
                prp = model.GetPropertyVersion(this);
                res.Add(prp);
            }
        }
        public void GetNormalProperties(List<IProperty> res)
        {
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
            var model = this.Cfg.Model;
            var prp = model.GetPropertySpecial(this, EnumSpecialPropertyType.RECORD_ID);
            viewListData = new ViewListData(prp);
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

        #region Roles
        public IRoleDetailsSettings GetRoleSettings(IRole role)
        {
            var roles = this.Cfg.Model.GroupCommon.GroupRoles;
            var nodeRoleDic = roles.DicRoles[role.Guid];
            nodeRoleDic.DicNodeRules.TryGetValue(this.Guid, out var roleFromNode);
            var res = new RoleDetailsSettings(this);
            if (this.ParentGroupListDetails.Parent is Catalog c)
            {
                res.CanEdit = roleFromNode?.DetailSettings.CanEdit ?? c.GetRoleSettings(role).CanEditDetails;
                res.CanEditDetails = roleFromNode?.DetailSettings.CanEditDetails ?? c.GetRoleSettings(role).CanEditDetails;
                res.CanEditFields = roleFromNode?.DetailSettings.CanEditFields ?? c.GetRoleSettings(role).CanEditFields;
                res.CanMarkDel = roleFromNode?.DetailSettings.CanMarkDel ?? c.GetRoleSettings(role).CanMarkDel;
                res.CanPrint = roleFromNode?.DetailSettings.CanPrint ?? c.GetRoleSettings(role).CanPrint;
                res.CanView = roleFromNode?.DetailSettings.CanView ?? c.GetRoleSettings(role).CanView;
                res.CanViewDetails = roleFromNode?.DetailSettings.CanViewDetails ?? c.GetRoleSettings(role).CanViewDetails;
                res.CanViewFields = roleFromNode?.DetailSettings.CanViewFields ?? c.GetRoleSettings(role).CanViewFields;
            }
            else if (this.ParentGroupListDetails.Parent is Document d)
            {
                res.CanEdit = roleFromNode?.DetailSettings.CanEdit ?? d.GetRoleSettings(role).CanEditDetails;
                res.CanEditDetails = roleFromNode?.DetailSettings.CanEditDetails ?? d.GetRoleSettings(role).CanEditDetails;
                res.CanEditFields = roleFromNode?.DetailSettings.CanEditFields ?? d.GetRoleSettings(role).CanEditFields;
                res.CanMarkDel = roleFromNode?.DetailSettings.CanMarkDel ?? d.GetRoleSettings(role).CanMarkDel;
                res.CanPrint = roleFromNode?.DetailSettings.CanPrint ?? d.GetRoleSettings(role).CanPrint;
                res.CanView = roleFromNode?.DetailSettings.CanView ?? d.GetRoleSettings(role).CanView;
                res.CanViewDetails = roleFromNode?.DetailSettings.CanViewDetails ?? d.GetRoleSettings(role).CanViewDetails;
                res.CanViewFields = roleFromNode?.DetailSettings.CanViewFields ?? d.GetRoleSettings(role).CanViewFields;
            }
            else if (this.ParentGroupListDetails.Parent is Detail t)
            {
                res.CanEdit = roleFromNode?.DetailSettings.CanEdit ?? t.GetRoleSettings(role).CanEditDetails;
                res.CanEditDetails = roleFromNode?.DetailSettings.CanEditDetails ?? t.GetRoleSettings(role).CanEditDetails;
                res.CanEditFields = roleFromNode?.DetailSettings.CanEditFields ?? t.GetRoleSettings(role).CanEditFields;
                res.CanMarkDel = roleFromNode?.DetailSettings.CanMarkDel ?? t.GetRoleSettings(role).CanMarkDel;
                res.CanPrint = roleFromNode?.DetailSettings.CanPrint ?? t.GetRoleSettings(role).CanPrint;
                res.CanView = roleFromNode?.DetailSettings.CanView ?? t.GetRoleSettings(role).CanView;
                res.CanViewDetails = roleFromNode?.DetailSettings.CanViewDetails ?? t.GetRoleSettings(role).CanViewDetails;
                res.CanViewFields = roleFromNode?.DetailSettings.CanViewFields ?? t.GetRoleSettings(role).CanViewFields;
            }
            else
            {
                Debug.Assert(false, "Not supported");
            }
            return res;
        }
        #endregion Roles
    }
}
