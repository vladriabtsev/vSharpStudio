﻿using System;
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
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class Register : ICanAddNode, ICanGoLeft, INodeGenSettings, ITreeConfigNodeSortable, IEditableNode //, IRoleAccess, IPropertyAccessRoles
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Docs:{this.ListDocGuids.Count} Dims:{this.GroupRegisterDimensions.ListDimensions.Count} Atchs:{this.GroupAttachedProperties.ListProperties.Count} Maps:{this.ListDocMappings.Count}";
        }
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

        [Browsable(false)]
        public new string IconName { get { return "iconCube"; } }
        //protected override string GetNodeIconName() { return "iconProperty"; }
        [Browsable(false)]
        public string? ComplexObjectName { get; set; }
        public string ComplexObjectNameWithDot() { if (!string.IsNullOrEmpty(this.ComplexObjectName)) return $"{this.ComplexObjectName}."; return ""; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            this._UseMoneyAccumulator = true;
            this._TableDimensionPropertyMoneyAccumulatorName = "AccumulatedMoney";
            this._TableDimensionPropertyMoneyAccumulatorAccuracy = 2;
            this._TableDimensionPropertyMoneyAccumulatorLength = 28;
            this._UseQtyAccumulator = true;
            this._TableDimensionPropertyQtyAccumulatorName = "AccumulatedQty";
            this._TableDimensionPropertyQtyAccumulatorAccuracy = 4;
            this._TableDimensionPropertyQtyAccumulatorLength = 28;
            this._TableDimensionPropertyMoneyAccumulatorGuid = System.Guid.NewGuid().ToString();
            this._TableDimensionPropertyQtyAccumulatorGuid = System.Guid.NewGuid().ToString();
            this._PropertyDocRefGuid = System.Guid.NewGuid().ToString();
            this._PropertyDocGuidGuid = System.Guid.NewGuid().ToString();
            this._PropertyDocDateGuid = System.Guid.NewGuid().ToString();
            this._IndexDocDateGuid = System.Guid.NewGuid().ToString();
            this._PropertyDocNumberGuid = System.Guid.NewGuid().ToString();
            this._IndexDocIdTypeGuid = System.Guid.NewGuid().ToString();
            this._PropertyIdGuid = System.Guid.NewGuid().ToString();
            this._PropertyVersionGuid = System.Guid.NewGuid().ToString();
            this._TableDimensionGuid = System.Guid.NewGuid().ToString();
            this._TableDimensionPropertyIdGuid = System.Guid.NewGuid().ToString();
            this._TableDimensionPropertyVersionGuid = System.Guid.NewGuid().ToString();
            this._LastGenPosition = 20;
            this._PropertyDocRefGuidName = "DocGuid";
            this._PropertyDocRefName = "DocRef";
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
            children.Add(this.GroupRegisterDimensions, 0);
            children.Add(this.GroupAttachedProperties, 1);
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
        public uint GetNextPosition()
        {
            if (this.LastGenPosition < 20)
                this.LastGenPosition = 20;
            return ++this.LastGenPosition;
        }
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
            var node = new Register(this.Parent);
            this.ParentGroupListRegisters.Add(node);
            this.GetUniqueName(Defaults.RegisterName, node, this.ParentGroupListRegisters.ListRegisters);
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

        public IRegisterDimension AddDimension(string name, ICatalog c)
        {
            var d = new RegisterDimension(c) { Name = name, CatalogGuid = c.Guid };
            this.LastGenPosition++;
            d.Position = this.LastGenPosition;
            this.GroupRegisterDimensions.ListDimensions.Add(d);
            return d;
        }
        public IProperty AddAttachedProperty(string name, EnumDataType type = EnumDataType.STRING, uint length = 0, uint accuracy = 0, string? guid = null)
        {
            var node = new Property(this.GroupAttachedProperties) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = type, Length = length, Accuracy = accuracy };
            this.GroupAttachedProperties.NodeAddNewSubNode(node);
            return node;
        }
        public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial)
        {
            Debug.Assert(!isExcludeSpecial, "not implemented yet");

            var lst = new List<IProperty>();
            var m = this.ParentGroupListRegisters.ParentModel;

            // Id
            var pId = m.GetPropertyPkId(this, this.Guid); // position 6
            pId.TagInList = "id";
            lst.Add(pId);

            if (isOptimistic)
            {
                // Version
                var pVer = m.GetPropertyVersion(this, this.PropertyVersionGuid); // position 7
                pVer.TagInList = "vr";
                lst.Add(pVer);
            }

            // Document date
            var pDocDate = m.GetPropertyDocumentDate(this, this.PropertyDocDateGuid); // position 8
            //var pDocDate = m.GetPropertyDocumentDate(this, this.PropertyDocDateGuid, true);
            pDocDate.TagInList = "dd";
            lst.Add(pDocDate);

            // Reference to document
            var pDocRef = (Property)m.GetPropertyId(this, this.PropertyDocRefGuid, this.PropertyDocRefName, false);
            pDocRef.Position = 11;
            pDocRef.TagInList = "dr";
            lst.Add(pDocRef);

            // Guid of document
            var pDocGuid = (Property)m.GetPropertyGuid(this, this.PropertyDocGuidGuid, this.PropertyDocRefGuidName, false);
            pDocGuid.Position = 13;
            pDocGuid.TagInList = "dg";
            lst.Add(pDocGuid);

            // Document number
            var pDocNumber = (Property)m.GetPropertyDocNumberString(this, this.PropertyDocNumberGuid, 50);
            pDocNumber.Position = 15;
            pDocNumber.TagInList = "dn";
            lst.Add(pDocNumber);

            // For all attached properties.
            foreach (var t in this.GroupAttachedProperties.ListProperties)
            {
                lst.Add(t);
            }
            return lst;
        }
        public IReadOnlyList<IProperty> GetIncludedDimensionsProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial)
        {
            Debug.Assert(!isExcludeSpecial, "not implemented yet");

            var lst = new List<IProperty>();
            var m = this.ParentGroupListRegisters.ParentModel;

            // Id
            var pId = m.GetPropertyPkId(this, this.PropertyIdGuid); // position 6
            pId.TagInList = "id";
            lst.Add(pId);

            if (isOptimistic)
            {
                // Version
                var pVer = m.GetPropertyVersion(this, this.PropertyVersionGuid); // position 7
                pVer.TagInList = "vr";
                lst.Add(pVer);
            }

            // Money accumulator
            var pMoney = (Property)m.GetPropertyNumber(this, this.TableDimensionPropertyMoneyAccumulatorGuid, this.TableDimensionPropertyMoneyAccumulatorName, this.TableDimensionPropertyMoneyAccumulatorLength, this.TableDimensionPropertyMoneyAccumulatorAccuracy, false);
            pMoney.Position = 11;
            pMoney.TagInList = "ma";
            lst.Add(pMoney);

            // Qty accumulator
            var pQty = (Property)m.GetPropertyNumber(this, this.TableDimensionPropertyQtyAccumulatorGuid, this.TableDimensionPropertyQtyAccumulatorName, this.TableDimensionPropertyQtyAccumulatorLength, this.TableDimensionPropertyQtyAccumulatorAccuracy, false);
            pQty.Position = 12;
            pQty.TagInList = "qa";
            lst.Add(pQty);

            // Reference to register header
            var pRegRef = (Property)m.GetPropertyRef(this, this.Guid, "Ref" + this.CompositeName, 13);
            pRegRef.TagInList = "rr";
            lst.Add(pRegRef);

            // Positions for dimentsions and attached properties are starting from 21. They are using same position sequence.
            // For all dimensions (catalogs).
            foreach (var t in this.GroupRegisterDimensions.ListDimensions)
            {
                if (!string.IsNullOrEmpty(t.CatalogGuid))
                {
                    if (m.ParentConfig.DicNodes.TryGetValue(t.CatalogGuid, out var node))
                    {
                        if (node is Catalog c)
                        {
                            var pCat = (Property)m.GetPropertyRefCatalog(this, t.Guid, c, t.Position, false);
                            lst.Add(pCat);
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

            // For all attached properties.
            foreach (var t in this.GroupAttachedProperties.ListProperties)
            {
                lst.Add(t);
            }
            return lst;
        }

        #region Editor

        #region Documents
        //[Browsable(false)]
        //public SortedObservableCollection<ISortingValue> ListSeparateTreeSelectedNotSpecialProperties
        //{
        //    get
        //    {
        //        if (this.listSeparateTreeSelectedNotSpecialProperties == null)
        //        {
        //            this.listSeparateTreeSelectedNotSpecialProperties = new SortedObservableCollection<ISortingValue>();
        //            if (this.ParentGroupListForms.Parent is Catalog c)
        //            {
        //                if (c.UseTree && c.UseSeparateTreeForFolders)
        //                {
        //                    var res = new List<IProperty>();
        //                    c.Folder.GetNormalProperties(res);
        //                    foreach (var t in res)
        //                    {
        //                        foreach (var tt in this.ListGuidViewFolderProperties)
        //                        {
        //                            if (tt == t.Guid)
        //                            {
        //                                this.listSeparateTreeSelectedNotSpecialProperties.Add(t);
        //                                break;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        return this.listSeparateTreeSelectedNotSpecialProperties;
        //    }
        //}
        //private SortedObservableCollection<ISortingValue>? listSeparateTreeSelectedNotSpecialProperties;
        #endregion Documents

        #endregion Editor
    }
}
