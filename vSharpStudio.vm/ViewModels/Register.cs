using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.DirectoryServices;
using System.Numerics;
using System.Text;
using System.Xml.Linq;
using CommunityToolkit.Diagnostics;
using FluentValidation;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Proto.Config;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Register:{Name,nq} Type:{DataType.GetTypeDesc(this.DataType),nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class Register : ICanAddNode, ICanGoLeft, INodeGenSettings, ITreeConfigNodeSortable, IEditableNode //, IRoleAccess, IPropertyAccessRoles
    {
        [Browsable(false)]
        public GroupListRegisters ParentGroupListRegisters { get { Debug.Assert(this.Parent != null); return (GroupListRegisters)this.Parent; } }
        [Browsable(false)]
        public IGroupListRegisters ParentGroupListRegistersI { get { Debug.Assert(this.Parent != null); return (IGroupListRegisters)this.Parent; } }
        [Browsable(false)]
        // Can be used by a generator to keep calculated property data
        public object? Tag { get; set; }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return new ConfigNodesCollection<ITreeConfigNodeSortable>(this);
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListRegisters.Children;
        }
        public override bool HasChildren()
        {
            return false;
        }
        #endregion ITree

        public static readonly string DefaultName = "Register";
        [Browsable(false)]
        public new string IconName { get { return "iconRegister"; } }
        //protected override string GetNodeIconName() { return "iconProperty"; }
        [Browsable(false)]
        public string? ComplexObjectName { get; set; }
        public string ComplexObjectNameWithDot() { if (!string.IsNullOrEmpty(this.ComplexObjectName)) return $"{this.ComplexObjectName}."; return ""; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            this.UseMoneyAccumulator = true;
            this.PropertyMoneyAccumulatorName = "AccumulatedMoney";
            this.PropertyMoneyAccumulatorAccuracy = 2;
            this.PropertyMoneyAccumulatorLength = 28;
            this.PropertyMoneyAccumulatorGuid = System.Guid.NewGuid().ToString();
            this.UseQtyAccumulator = true;
            this.PropertyQtyAccumulatorName = "AccumulatedQty";
            this.PropertyQtyAccumulatorAccuracy = 4;
            this.PropertyQtyAccumulatorLength = 28;
            this.PropertyQtyAccumulatorGuid = System.Guid.NewGuid().ToString();
            this.PropertyDocRefGuid = System.Guid.NewGuid().ToString();
            this.PropertyDocGuidGuid = System.Guid.NewGuid().ToString();
            this.PropertyDocDateGuid = System.Guid.NewGuid().ToString();
            this.IndexDocDateDimentionsGuid = System.Guid.NewGuid().ToString();
            this.PropertyDocNumberGuid = System.Guid.NewGuid().ToString();
            this.IndexDocIdTypeGuid = System.Guid.NewGuid().ToString();
            this.PropertyVersionGuid = System.Guid.NewGuid().ToString();
            this.LastGenPosition = 15;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            //this.ListMainViewForms.OnAddingAction = (t) =>
            //{
            //    t.IsNew = true;
            //};
            //this.ListMainViewForms.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            //this.ListMainViewForms.OnRemovedAction = (t) =>
            //{
            //    this.OnRemoveChild();
            //};
            //this.ListMainViewForms.OnClearedAction = () =>
            //{
            //    this.OnRemoveChild();
            //};
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
        }
        internal bool isSpecialItself;

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListRegisters.ListRegisters.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Register?)this.ParentGroupListRegisters.ListRegisters.GetPrev(this);
            if (prev == null)
                return;
            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupListRegisters.ListRegisters.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListRegisters.ListRegisters.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Register?)this.ParentGroupListRegisters.ListRegisters.GetNext(this);
            if (next == null)
                return;
            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupListRegisters.ListRegisters.MoveDown(this);
            this.SetSelected(this);
        }

        public void NodeRemove(bool ask = true)
        {
            this.ParentGroupListRegisters.Remove(this);
            this.Parent = null;
        }
        public override ITreeConfigNode NodeAddClone()
        {
            Debug.Assert(this.Parent != null);
            var node = Register.Clone(this.Parent, this, true, true);
            this.ParentGroupListRegisters.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            if (!(this.Parent is GroupListProperties))
            {
                throw new Exception();
            }

            var node = new Register(this.Parent);
            this.ParentGroupListRegisters.Add(node);
            this.GetUniqueName(Register.DefaultName, node, this.ParentGroupListRegisters.ListRegisters);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListRegisters.ListRegisters.Remove(this);
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

        #region Editing logic
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            //if (this.DataType.DataTypeEnum != EnumDataType.STRING)
            //{
            //    lst.Add(this.GetPropertyName(() => this.MinLengthRequirement));
            //    lst.Add(this.GetPropertyName(() => this.MaxLengthRequirement));
            //}
            //if (this.DataType.DataTypeEnum != EnumDataType.NUMERICAL)
            //{
            //    lst.Add(this.GetPropertyName(() => this.Accuracy));
            //    lst.Add(this.GetPropertyName(() => this.IsPositive));
            //}
            //if (this.DataType.DataTypeEnum != EnumDataType.STRING && this.DataType.DataTypeEnum != EnumDataType.NUMERICAL)
            //{
            //    lst.Add(this.GetPropertyName(() => this.Length));
            //}
            //if (this.DataType.DataTypeEnum != EnumDataType.TIME &&
            //    this.DataType.DataTypeEnum != EnumDataType.DATETIMELOCAL &&
            //    this.DataType.DataTypeEnum != EnumDataType.DATETIMEUTC) // &&
            //                                                            //this.DataType.DataTypeEnum != EnumDataType.DATETIME &&
            //                                                            //this.DataType.DataTypeEnum != EnumDataType.DATETIMEZ)
            //{
            //    lst.Add(this.GetPropertyName(() => this.AccuracyForTime));
            //}
            //if (this.DataType.DataTypeEnum != EnumDataType.CATALOGS &&
            //    this.DataType.DataTypeEnum != EnumDataType.DOCUMENTS)
            //{
            //    lst.Add(this.GetPropertyName(() => this.ListObjectGuids));
            //    lst.Add(this.GetPropertyName(() => this.DefaultValue));
            //}
            //if (this.DataType.DataTypeEnum != EnumDataType.CATALOG &&
            //    this.DataType.DataTypeEnum != EnumDataType.DOCUMENT &&
            //    this.DataType.DataTypeEnum != EnumDataType.ENUMERATION &&
            //    this.DataType.DataTypeEnum != EnumDataType.ANY)
            //{
            //    lst.Add(this.GetPropertyName(() => this.ObjectGuid));
            //    lst.Add(this.GetPropertyName(() => this.DefaultValue));
            //}
            //if (this.Accuracy != 0)
            //{
            //    lst.Add(this.GetPropertyName(() => this.IsPositive));
            //}
            //if (this.DataType.DataTypeEnum != EnumDataType.STRING &&
            //    this.DataType.DataTypeEnum != EnumDataType.CHAR &&
            //    this.DataType.DataTypeEnum != EnumDataType.DATE &&
            //    this.DataType.DataTypeEnum != EnumDataType.DATETIMELOCAL &&
            //    this.DataType.DataTypeEnum != EnumDataType.DATETIMEUTC &&
            //    //this.DataType.DataTypeEnum != EnumDataType.DATETIME &&
            //    //this.DataType.DataTypeEnum != EnumDataType.DATETIMEZ &&
            //    this.DataType.DataTypeEnum != EnumDataType.NUMERICAL)
            //{
            //    lst.Add(this.GetPropertyName(() => this.RangeValuesRequirements));
            //}
            return lst.ToArray();
        }
        #endregion Editing logic

        #region Roles
        //public object GetRoleAccess(IRole role)
        //{
        //    if (!this.dicPropertyAccess.ContainsKey(role.Guid))
        //    {
        //        var rca = new RolePropertyAccess() { Guid = role.Guid };
        //        this.ListRolePropertyAccessSettings.Add(rca);
        //        this.dicPropertyAccess[role.Guid] = rca;
        //    }
        //    return dicPropertyAccess[role.Guid];
        //}
        //public void SetRoleAccess(IRole role, EnumPropertyAccess? edit, EnumPrintAccess? print)
        //{
        //    Debug.Assert(role != null);
        //    Debug.Assert(dicPropertyAccess.ContainsKey(role.Guid));
        //    if (edit.HasValue)
        //        dicPropertyAccess[role.Guid].EditAccess = edit.Value;
        //    if (print.HasValue)
        //        dicPropertyAccess[role.Guid].PrintAccess = print.Value;
        //}
        //internal Dictionary<string, RolePropertyAccess> dicPropertyAccess = new();
        //public void InitRoles()
        //{
        //    foreach (var tt in this.ListRolePropertyAccessSettings)
        //    {
        //        this.dicPropertyAccess[tt.Guid] = tt;
        //    }
        //    foreach (var t in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
        //    {
        //        if (!this.dicPropertyAccess.ContainsKey(t.Guid))
        //        {
        //            var rca = new RolePropertyAccess() { Guid = t.Guid };
        //            this.dicPropertyAccess[t.Guid] = rca;
        //        }
        //    }
        //}
        //public void InitRoleAdd(IRole role)
        //{
        //    var rca = new RolePropertyAccess() { Guid = role.Guid };
        //    this.ListRolePropertyAccessSettings.Add(rca);
        //    this.dicPropertyAccess[rca.Guid] = rca;
        //}
        //public void InitRoleRemove(IRole role)
        //{
        //    for (int i = 0; i < this.ListRolePropertyAccessSettings.Count; i++)
        //    {
        //        if (this.ListRolePropertyAccessSettings[i].Guid == role.Guid)
        //        {
        //            this.ListRolePropertyAccessSettings.RemoveAt(i);
        //            break;
        //        }
        //    }
        //    this.dicPropertyAccess.Remove(role.Guid);
        //}
        //public EnumPropertyAccess GetRolePropertyAccess(IRole role)
        //{
        //    if (this.dicPropertyAccess.TryGetValue(role.Guid, out var r) && r.EditAccess != EnumPropertyAccess.P_BY_PARENT)
        //        return r.EditAccess;
        //    return this.ParentGroupListProperties.GetRolePropertyAccess(role);
        //}
        //public EnumPrintAccess GetRolePropertyPrint(IRole role)
        //{
        //    if (this.dicPropertyAccess.TryGetValue(role.Guid, out var r) && r.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
        //        return r.PrintAccess;
        //    return this.ParentGroupListProperties.GetRolePropertyPrint(role);
        //}
        //public IReadOnlyList<string> GetRolesByAccess(EnumPropertyAccess access)
        //{
        //    var roles = new List<string>();
        //    foreach (var role in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
        //    {
        //        if (GetRolePropertyAccess(role) == access)
        //            roles.Add(role.Name);
        //    }
        //    return roles;
        //}
        //public IReadOnlyList<string> GetRolesByAccess(EnumPrintAccess access)
        //{
        //    var roles = new List<string>();
        //    foreach (var role in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
        //    {
        //        if (GetRolePropertyPrint(role) == access)
        //            roles.Add(role.Name);
        //    }
        //    return roles;
        //}
        #endregion Roles

        public IRegisterDimention AddDimention(string name, ICatalog c)
        {
            var d = new RegisterDimention(c) { Name = name, DimentionCatalogGuid = c.Guid };
            this.LastGenPosition++;
            d.Position = this.LastGenPosition;
            this.ListRegisterDimensions.Add(d);
            return d;
        }
        public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isOptimistic)
        {
            var lst = new List<IProperty>();
            var m = this.ParentGroupListRegisters.ParentGroupDocuments.ParentModel;
            var pId = m.GetPropertyPkId(this, this.Guid);
            pId.TagInList = "id";
            lst.Add(pId);

            if (isOptimistic)
            {
                var pVer = m.GetPropertyVersion(this, this.PropertyVersionGuid);
                pVer.TagInList = "vr";
                lst.Add(pVer);
            }
            var pDocDate = m.GetPropertyDocumentDate(this, this.PropertyDocDateGuid);
            pDocDate.TagInList = "dd";
            lst.Add(pDocDate);

            // Document number
            var pDocNumber = m.GetPropertyDocNumberString(this, this.PropertyDocNumberGuid, 50);
            pDocNumber.TagInList = "dn";
            lst.Add(pDocNumber);

            // Money accumulator
            var pMoney = (Property)m.GetPropertyNumber(this, this.PropertyMoneyAccumulatorGuid, this.PropertyMoneyAccumulatorName, this.PropertyMoneyAccumulatorLength, this.PropertyMoneyAccumulatorAccuracy, false);
            pMoney.Position = 11;
            pMoney.TagInList = "ma";
            lst.Add(pMoney);

            // Qty accumulator
            var pQty = (Property)m.GetPropertyNumber(this, this.PropertyQtyAccumulatorGuid, this.PropertyQtyAccumulatorName, this.PropertyQtyAccumulatorLength, this.PropertyQtyAccumulatorAccuracy, false);
            pQty.Position = 12;
            pQty.TagInList = "qa";
            lst.Add(pQty);

            // Reference to document
            var pDocRef = (Property)m.GetPropertyId(this, this.PropertyDocRefGuid, "DocRef", false);
            pDocRef.Position = 13;
            pDocRef.TagInList = "dr";
            lst.Add(pDocRef);

            // Guid of document
            var pDocGuid = (Property)m.GetPropertyGuid(this, this.PropertyDocGuidGuid, "DocGuid", false);
            pDocGuid.Position = 14;
            pDocGuid.TagInList = "dg";
            lst.Add(pDocGuid);

            // For all dimensions (catalogs)
            int i = 1;
            foreach (var t in this.ListRegisterDimensions)
            {
                if (!string.IsNullOrEmpty(t.DimentionCatalogGuid))
                {
                    if (m.ParentConfig.DicNodes.TryGetValue(t.DimentionCatalogGuid, out var node))
                    {
                        if (node is Catalog c)
                        {
                            var pRef = (Property)m.GetPropertyRefDimention(this, t.Guid, "Ref" + c.CompositeName, false);
                            pRef.Position = t.Position + 16;
                            pRef.TagInList = i.ToString();
                            lst.Add(pRef);
                            i++;
                        }
                        else
                            ThrowHelper.ThrowNotSupportedException();
                    }
                    else
                        ThrowHelper.ThrowNotSupportedException();
                }
                else
                    ThrowHelper.ThrowNotSupportedException();
            }
            return lst;
        }
    }
}
