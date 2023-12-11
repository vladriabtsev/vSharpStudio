using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.DirectoryServices;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using FluentValidation;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Proto.Config;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class Register : ICanAddNode, ICanGoLeft, INodeGenSettings, ITreeConfigNodeSortable, IEditableNode //, IRoleAccess, IPropertyAccessRoles
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Docs:{this.ListDocGuids.Count} Dims:{this.GroupRegisterDimensions.ListDimensions.Count} Atchs:{this.GroupAttachedProperties.ListProperties.Count} Maps:{this.ListDocMappings.Count}";
        }
        public string GetDebuggerDisplay(bool isOptimistic)
        {
            var sb = new StringBuilder();
            sb.Append("REG ");
            sb.Append(this.Name);
            sb.Append(", ");
            sb.Append(this.ParentGroupListRegisters.ParentModel.PKeyName);
            sb.Append(":{");
            sb.Append(this.ParentGroupListRegisters.ParentModel.PKeyName);
            sb.Append(",nq}");
            sb.Append(", Doc:{DocDescr,nq}");
            sb.Append(", Turnovers:{ListTurnovers.Count}");
            if (isOptimistic)
            {
                sb.Append(" RecVer:{");
                sb.Append(this.ParentGroupListRegisters.ParentModel.RecordVersionFieldName);
                sb.Append(",nq}");
            }
            return sb.ToString();
        }
        public string GetDebuggerDisplayTurnover(bool isOptimistic)
        {
            var sb = new StringBuilder();
            sb.Append("POST ");
            sb.Append(this.Name);
            sb.Append(", ");
            sb.Append(this.ParentGroupListRegisters.ParentModel.PKeyName);
            sb.Append(":{");
            sb.Append(this.ParentGroupListRegisters.ParentModel.PKeyName);
            sb.Append(",nq}");
            foreach (var t in this.GroupRegisterDimensions.ListDimensions)
            {
                sb.Append(", ");
                sb.Append(t.Name);
                sb.Append(":{");
                sb.Append(t.Name);
                sb.Append("Descr ,nq}");
            }
            if (this.UseQtyAccumulator)
                sb.Append(", Qty:{AccumulatedQty,nq}");
            if (this.UseMoneyAccumulator)
                sb.Append(", Money:{AccumulatedMoney,nq}");
            if (isOptimistic)
            {
                sb.Append(" RecVer:{");
                sb.Append(this.ParentGroupListRegisters.ParentModel.RecordVersionFieldName);
                sb.Append(",nq}");
            }
            return sb.ToString();
        }
        public string GetDebuggerDisplayBalance(bool isOptimistic)
        {
            return "";
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
            this._TableTurnoverPropertyMoneyAccumulatorName = "AccumulatedMoney";
            this._TableTurnoverPropertyMoneyAccumulatorAccuracy = 2;
            this._TableTurnoverPropertyMoneyAccumulatorLength = 28;
            this._UseQtyAccumulator = true;
            this._TableTurnoverPropertyQtyAccumulatorName = "AccumulatedQty";
            this._TableTurnoverPropertyQtyAccumulatorAccuracy = 4;
            this._TableTurnoverPropertyQtyAccumulatorLength = 28;
            this._TableTurnoverPropertyMoneyAccumulatorGuid = System.Guid.NewGuid().ToString();
            this._TableTurnoverPropertyQtyAccumulatorGuid = System.Guid.NewGuid().ToString();
            this._PropertyPostDateGuid = System.Guid.NewGuid().ToString();
            this._PropertyDocRefGuid = System.Guid.NewGuid().ToString();
            this._PropertyDocGuidGuid = System.Guid.NewGuid().ToString();
            this._PropertyDocDateGuid = System.Guid.NewGuid().ToString();
            this._IndexDocDateGuid = System.Guid.NewGuid().ToString();
            this._PropertyDocNumberGuid = System.Guid.NewGuid().ToString();
            this._IndexDocIdTypeGuid = System.Guid.NewGuid().ToString();
            this._PropertyIdGuid = System.Guid.NewGuid().ToString();
            this._PropertyVersionGuid = System.Guid.NewGuid().ToString();
            this._TableTurnoverGuid = System.Guid.NewGuid().ToString();
            this._TableTurnoverPropertyIdGuid = System.Guid.NewGuid().ToString();
            this._TableTurnoverPropertyVersionGuid = System.Guid.NewGuid().ToString();
            this._TableBalanceGuid = System.Guid.NewGuid().ToString();
            this._TableBalancePropertyIdGuid = System.Guid.NewGuid().ToString();
            this._TableBalancePropertyVersionGuid = System.Guid.NewGuid().ToString();
            this._TableTurnoverPropertyIsStartingBalanceGuid = System.Guid.NewGuid().ToString();
            this._TableTurnoverPropertyPostDateGuid = System.Guid.NewGuid().ToString();
            this._LastGenPosition = 20;
            this._PropertyDocRefGuidName = "DocGuid";
            this._PropertyDocRefName = "DocRef";
            this._RegisterType = EnumRegisterType.TURNOVER;
            this._RegisterPeriodicity = EnumRegisterPeriodicity.REGISTER_PERIOD_MONTH;
            //this._ListNotSelectedDocuments.CollectionChanged += _ListNotSelectedDocuments_CollectionChanged;
            this._ListSelectedDocuments = new SortedObservableCollection<ISortingValue>();
            this._ListSelectedDocuments.CollectionChanged += _ListSelectedDocuments_CollectionChanged;
            Init();
        }

        protected override void OnInitFromDto()
        {
            Init();
            for (var i = this.ListDocMappings.Count - 1; i > -1; i--)
            {
                if (string.IsNullOrEmpty(this.ListDocMappings[i].DocGuid))
                    this.ListDocMappings.RemoveAt(i);
            }
            foreach (var t in this.ListDocMappings)
            {
                Debug.Assert(!string.IsNullOrWhiteSpace(t.DocGuid));
                var mappingDocDic = new Dictionary<string, string>();
                foreach (var tt in t.ListMappings)
                {
                    Debug.Assert(!mappingDocDic.ContainsKey(tt.RegPropGuid));
                    mappingDocDic[tt.RegPropGuid] = tt.DocPropGuid;
                }
                Debug.Assert(!mappingDic.ContainsKey(t.DocGuid));
                mappingDic[t.DocGuid] = mappingDocDic;
            }
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
        // doc guid, reg prop guid, doc prop guid
        internal Dictionary<string, Dictionary<string, string>> mappingDic = new();

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

        public RegisterDimension AddDimension(string name)
        {
            var d = new RegisterDimension(this.GroupRegisterDimensions) { Name = name };
            this.LastGenPosition++;
            d.Position = this.LastGenPosition;
            this.GroupRegisterDimensions.ListDimensions.Add(d);
            return d;
        }
        public RegisterDimension AddDimension(string name, ICatalog c)
        {
            var d = new RegisterDimension(this.GroupRegisterDimensions) { Name = name, DimensionCatalogGuid = c.Guid };
            this.LastGenPosition++;
            d.Position = this.LastGenPosition;
            this.GroupRegisterDimensions.ListDimensions.Add(d);
            return d;
        }
        public Property AddAttachedProperty(string name, EnumDataType type = EnumDataType.STRING, uint length = 0, uint accuracy = 0, string? guid = null)
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

            //if (this.ListDocGuids.Count > 0)
            //{
            //    //if (this.ListDocGuids.Count == 1)
            //    //{
            //    //    var pDoc = (Property)m.GetPropertyDocument(this, this.PropertyDocRefGuid, "DocRef", this.ListDocGuids[0], 9, false);
            //    //    lst.Add(pDoc);
            //    //    // Guid of document
            //    //    var pDocGuid = (Property)m.GetPropertyGuid(this, this.PropertyDocGuidGuid, this.PropertyDocRefGuidName, false);
            //    //    pDocGuid.Position = 13;
            //    //    pDocGuid.TagInList = "dg";
            //    //    lst.Add(pDocGuid);
            //    //}
            //    //else
            //    //{
            //    var pDoc = (Property)m.GetPropertyDocuments(this, this.PropertyDocRefGuid, "Doc", this.ListDocGuids, 10, true);
            //    lst.Add(pDoc);
            //    //}
            //}

            //// Post date
            //var pPostDate = m.GetPropertyDateTimeUtc(this, this.PropertyPostDateGuid, "PostDate", 9, false); // position 9
            //pPostDate.TagInList = "pd";
            //lst.Add(pPostDate);

            var pDoc = (Property)m.GetPropertyDocuments(this, this.PropertyDocRefGuid, "Doc", this.ListDocGuids, 10, true);
            lst.Add(pDoc);

            //// Reference to document
            //var pDocRef = (Property)m.GetPropertyId(this, this.PropertyDocRefGuid, this.PropertyDocRefName, false);
            //pDocRef.Position = 11;
            //pDocRef.TagInList = "dr";
            //lst.Add(pDocRef);

            //// Guid of document
            //var pDocGuid = (Property)m.GetPropertyGuid(this, this.PropertyDocGuidGuid, this.PropertyDocRefGuidName, false);
            //pDocGuid.Position = 13;
            //pDocGuid.TagInList = "dg";
            //lst.Add(pDocGuid);

            // Document date
            var pDocDate = m.GetPropertyDocumentDate(this, this.PropertyDocDateGuid); // position 8
            //var pDocDate = m.GetPropertyDocumentDate(this, this.PropertyDocDateGuid, true);
            pDocDate.TagInList = "dd";
            lst.Add(pDocDate);

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
        public IReadOnlyList<IProperty> GetIncludedTurnoverProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial)
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

            //// Post date
            //var pPostDate = m.GetPropertyDateTimeUtc(this, this.TableTurnoverPropertyPostDateGuid, "PostDate", 9, false); // position 9
            //pPostDate.TagInList = "pd";
            //lst.Add(pPostDate);

            // Money accumulator
            var pMoney = (Property)m.GetPropertyNumber(this, this.TableTurnoverPropertyMoneyAccumulatorGuid, this.TableTurnoverPropertyMoneyAccumulatorName, this.TableTurnoverPropertyMoneyAccumulatorLength, this.TableTurnoverPropertyMoneyAccumulatorAccuracy, false);
            pMoney.Position = 11;
            pMoney.TagInList = "ma";
            lst.Add(pMoney);

            // Qty accumulator
            var pQty = (Property)m.GetPropertyNumber(this, this.TableTurnoverPropertyQtyAccumulatorGuid, this.TableTurnoverPropertyQtyAccumulatorName, this.TableTurnoverPropertyQtyAccumulatorLength, this.TableTurnoverPropertyQtyAccumulatorAccuracy, false);
            pQty.Position = 12;
            pQty.TagInList = "qa";
            lst.Add(pQty);

            // Reference to register header
            var pRegRef = (Property)m.GetPropertyRef(this, this.Guid, "Ref" + this.CompositeName, 13);
            pRegRef.TagInList = "rr";
            lst.Add(pRegRef);

            if (this.RegisterType != EnumRegisterType.TURNOVER)
            {
                // Register starting balances on period
                var pIsStartBalance = (Property)m.GetPropertyBool(this, this.TableTurnoverPropertyIsStartingBalanceGuid, "IsStartingBalance", 14, true);
                pRegRef.TagInList = "ib";
                lst.Add(pRegRef);
            }

            // Positions for dimentsions and attached properties are starting from 21. They are using same position sequence.
            // For all dimensions (catalogs).
            foreach (var t in this.GroupRegisterDimensions.ListDimensions)
            {
                if (!string.IsNullOrEmpty(t.DimensionCatalogGuid))
                {
                    if (m.ParentConfig.DicNodes.TryGetValue(t.DimensionCatalogGuid, out var node))
                    {
                        if (node is Catalog c)
                        {
                            var pCat = (Property)m.GetPropertyCatalog(this, t.Guid, t.Name, c.Guid, t.Position, false);
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
        public IReadOnlyList<IProperty> GetIncludedBalanceProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial)
        {
            throw new NotImplementedException();

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
            var pMoney = (Property)m.GetPropertyNumber(this, this.TableTurnoverPropertyMoneyAccumulatorGuid, this.TableTurnoverPropertyMoneyAccumulatorName, this.TableTurnoverPropertyMoneyAccumulatorLength, this.TableTurnoverPropertyMoneyAccumulatorAccuracy, false);
            pMoney.Position = 11;
            pMoney.TagInList = "ma";
            lst.Add(pMoney);

            // Qty accumulator
            var pQty = (Property)m.GetPropertyNumber(this, this.TableTurnoverPropertyQtyAccumulatorGuid, this.TableTurnoverPropertyQtyAccumulatorName, this.TableTurnoverPropertyQtyAccumulatorLength, this.TableTurnoverPropertyQtyAccumulatorAccuracy, false);
            pQty.Position = 12;
            pQty.TagInList = "qa";
            lst.Add(pQty);

            // Reference to register header
            var pRegRef = (Property)m.GetPropertyRef(this, this.Guid, "Ref" + this.CompositeName, 13);
            pRegRef.TagInList = "rr";
            lst.Add(pRegRef);

            if (this.RegisterType != EnumRegisterType.TURNOVER)
            {
                // Register starting balances on period
                var pIsStartBalance = (Property)m.GetPropertyBool(this, this.TableTurnoverPropertyIsStartingBalanceGuid, "IsStartingBalance", 14, true);
                pRegRef.TagInList = "ib";
                lst.Add(pRegRef);
            }

            // Positions for dimentsions and attached properties are starting from 21. They are using same position sequence.
            // For all dimensions (catalogs).
            foreach (var t in this.GroupRegisterDimensions.ListDimensions)
            {
                if (!string.IsNullOrEmpty(t.DimensionCatalogGuid))
                {
                    if (m.ParentConfig.DicNodes.TryGetValue(t.DimensionCatalogGuid, out var node))
                    {
                        if (node is Catalog c)
                        {
                            var pCat = (Property)m.GetPropertyCatalog(this, t.Guid, t.Name, c.Guid, t.Position, false);
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
        public IReadOnlyList<IItemWithSubItems> GetIncludedSubItems(string guidAppPrjDbGen)
        {
            var res = new List<IItemWithSubItems>();
            foreach (var t in this.GroupRegisterDimensions.ListDimensions)
            {
                res.Add(t);
            }
            return res;
        }
        public IReadOnlyList<IForm> GetListForms(string guidAppPrjDbGen)
        {
            throw new NotImplementedException();
        }
        public IForm GetForm(FormType ftype, string guidAppPrjGen)
        {
            throw new NotImplementedException();
        }

        #region Editor

        private bool isOnOpeningEditor = false;
        public override void OnOpeningEditor()
        {
            this.isOnOpeningEditor = true;

            #region ListNotSelectedDocuments
            this.ListNotSelectedDocuments.Clear();
            foreach (var t in this.Cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments)
            {
                bool found = false;
                foreach (var tt in this.ListDocGuids)
                {
                    if (t.Guid == tt)
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                    continue;
                this.ListNotSelectedDocuments.Add(t);
            }
            #endregion ListNotSelectedDocuments

            #region ListSelectedDocuments
            this.ListSelectedDocuments.Clear();
            foreach (var t in this.Cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments)
            {
                bool found = false;
                foreach (var tt in this.ListDocGuids)
                {
                    if (t.Guid == tt)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    continue;
                this.ListSelectedDocuments.Add(t);
            }
            #endregion ListSelectedDocuments

            #region ListMappings
            UpdateListMappings();
            #endregion ListMappings

            this.isOnOpeningEditor = false;
        }
        public void MappingRegDocRemove(string docGuid)
        {
            Guard.IsNotNullOrWhiteSpace(docGuid);
            int iat = -1;
            bool found = false;
            foreach (var t in this.ListDocMappings)
            {
                Debug.Assert(!string.IsNullOrWhiteSpace(t.DocGuid));
                iat++;
                if (t.DocGuid == docGuid)
                {
                    found = true;
                    break;
                }
            }
            Debug.Assert(found);
            this.ListDocMappings.RemoveAt(iat);
            Debug.Assert(mappingDic.ContainsKey(docGuid));
            this.mappingDic.Remove(docGuid);
            this.IsChanged = true;
        }
        public void MappingRegDocAdd(string docGuid)
        {
            Guard.IsNotNullOrWhiteSpace(docGuid);
#if DEBUG
            bool found = false;
            foreach (var t in this.ListDocMappings)
            {
                Debug.Assert(!string.IsNullOrWhiteSpace(t.DocGuid));
                if (t.DocGuid == docGuid)
                {
                    found = true;
                    break;
                }
            }
            Debug.Assert(!found);
#endif
            this.ListDocMappings.Add(new RegisterDocToReg() { DocGuid = docGuid });
            this.IsChanged = true;
        }
        public void MappingRegPropertyRemove(string docGuid, string regPropertyGuid)
        {
            Guard.IsNotNullOrWhiteSpace(docGuid);
            Guard.IsNotNullOrWhiteSpace(regPropertyGuid);
            RegisterDocToReg? rec = null;
            foreach (var t in this.ListDocMappings)
            {
                Debug.Assert(!string.IsNullOrWhiteSpace(t.DocGuid));
                if (t.DocGuid == docGuid)
                {
                    rec = t;
                    break;
                }
            }
            Debug.Assert(rec != null);
            var iat = -1;
            bool found = false;
            foreach (var t in rec.ListMappings)
            {
                iat++;
                if (t.RegPropGuid == regPropertyGuid)
                {
                    found = true;
                    break;
                }
            }
            Debug.Assert(found);
            rec.ListMappings.RemoveAt(iat);
            Debug.Assert(mappingDic.ContainsKey(docGuid));
            var dic = this.mappingDic[docGuid];
            Debug.Assert(dic.ContainsKey(regPropertyGuid));
            dic.Remove(regPropertyGuid);
            this.IsChanged = true;
        }
        public void MappingRegPropertyAdd(string docGuid, string regPropertyGuid, string docPropertyGuid)
        {
            Guard.IsNotNullOrWhiteSpace(docGuid);
            Guard.IsNotNullOrWhiteSpace(regPropertyGuid);
            Guard.IsNotNullOrWhiteSpace(docPropertyGuid);
            RegisterDocToReg? rec = null;
            foreach (var t in this.ListDocMappings)
            {
                Debug.Assert(!string.IsNullOrWhiteSpace(t.DocGuid));
                if (t.DocGuid == docGuid)
                {
                    rec = t;
                    break;
                }
            }
            if (rec == null)
            {
                rec = new RegisterDocToReg() { DocGuid = docGuid };
                this.ListDocMappings.Add(rec);
            }
            RegisterRegPropToDocProp? mapToProp = null;
            foreach (var t in rec.ListMappings)
            {
                if (t.RegPropGuid == regPropertyGuid)
                {
                    mapToProp = t; break;
                }
            }
            if (mapToProp == null)
            {
                rec.ListMappings.Add(new RegisterRegPropToDocProp() { DocPropGuid = docPropertyGuid, RegPropGuid = regPropertyGuid });
                this.IsChanged = true;
            }
            else
            {
                if (mapToProp.DocPropGuid != docPropertyGuid)
                {
                    mapToProp.DocPropGuid = docPropertyGuid;
                    this.IsChanged = true;
                }
            }
            if (!mappingDic.ContainsKey(docGuid))
                this.mappingDic[docGuid] = new Dictionary<string, string>();
            var dic = this.mappingDic[docGuid];
            dic[regPropertyGuid] = docPropertyGuid;
        }
        public string GetMappingToDocPropertyGuid(string docGuid, string regPropertyGuid)
        {
            var res = string.Empty;

            return res;
        }
        public void UpdateListMappings()
        {
            this.fulListToMap.Clear();
            this.ListMappings.Clear();
            if (this.SelectedDoc != null)
            {
                var doc = (Document)this.SelectedDoc;
                foreach (var t in this.GroupRegisterDimensions.ListDimensions)
                {
                    var row = new MappingRow(doc, this, t);
                    this.ListMappings.Add(row);
                }
                if (this.UseQtyAccumulator)
                {
                    var row = new MappingRow(doc, this, this.TableTurnoverPropertyQtyAccumulatorGuid, this.TableTurnoverPropertyQtyAccumulatorName);
                    this.ListMappings.Add(row);
                }
                if (this.UseMoneyAccumulator)
                {
                    var row = new MappingRow(doc, this, this.TableTurnoverPropertyMoneyAccumulatorGuid, this.TableTurnoverPropertyMoneyAccumulatorName);
                    this.ListMappings.Add(row);
                }
                foreach (var t in this.GroupAttachedProperties.ListProperties)
                {
                    var row = new MappingRow(doc, this, t.Guid, t.Name);
                    this.ListMappings.Add(row);
                }
                foreach (var t in doc.ParentGroupListDocuments.ParentGroupDocuments.GroupSharedProperties.ListProperties)
                {
                    this.fulListToMap.Add(t);
                }
                foreach (var t in doc.GroupProperties.ListProperties)
                {
                    this.fulListToMap.Add(t);
                }
                foreach (var tt in doc.GroupDetails.ListDetails)
                {
                    this.AddDetailProperties(tt);
                }
                if (!this.mappingDic.ContainsKey(doc.Guid))
                    this.mappingDic[doc.Guid] = new Dictionary<string, string>();
                var dicRegPropToDocProp = this.mappingDic[doc.Guid];
                // select properties for mapping
                foreach (var t in this.ListMappings)
                {
                    Property? selected = null;
                    int cnt = 0;
                    foreach (var tt in this.fulListToMap)
                    {
                        this.AddCompatibleProperty(t, tt);
                        if (t.Dimension != null && dicRegPropToDocProp.ContainsKey(t.Dimension.Guid) && tt.Guid == dicRegPropToDocProp[t.Dimension.Guid])
                        {
                            selected = tt;
                            cnt++;
                        }
                        else if (t.AttachedProperty != null && dicRegPropToDocProp.ContainsKey(t.AttachedProperty.Guid) && tt.Guid == dicRegPropToDocProp[t.AttachedProperty.Guid])
                        {
                            selected = tt;
                            cnt++;
                        }
                        else if (!string.IsNullOrEmpty(t.RegPropertyGuid))
                        {
                            if (this.TableTurnoverPropertyMoneyAccumulatorGuid == t.RegPropertyGuid && dicRegPropToDocProp.ContainsKey(t.RegPropertyGuid) && tt.Guid == dicRegPropToDocProp[t.RegPropertyGuid])
                            {
                                selected = tt;
                                cnt++;
                            }
                            else if (this.TableTurnoverPropertyQtyAccumulatorGuid == t.RegPropertyGuid && dicRegPropToDocProp.ContainsKey(t.RegPropertyGuid) && tt.Guid == dicRegPropToDocProp[t.RegPropertyGuid])
                            {
                                selected = tt;
                                cnt++;
                            }
                        }
                    }
                    Debug.Assert(cnt < 2);
                    t.Selected = selected;
                }
            }
        }
        private void AddDetailProperties(Detail d)
        {
            foreach (var t in d.GroupProperties.ListProperties)
            {
                this.fulListToMap.Add(t);
            }
            foreach (var tt in d.GroupDetails.ListDetails)
            {
                this.AddDetailProperties(tt);
            }
        }
        private void AddCompatibleProperty(MappingRow row, Property p)
        {
            if (this.IsShowCompatible)
            {
                if (row.Dimension != null)
                {
                    if (string.IsNullOrEmpty(row.Dimension.DimensionCatalogGuid))
                        return;
                    var cat = this.Cfg.DicNodes[row.Dimension.DimensionCatalogGuid];
                    if (p.DataType.DataTypeEnum != EnumDataType.CATALOG || cat.Guid != p.DataType.ObjectGuid)
                        return;
                }
                else if (row.AttachedProperty != null)
                {
                    if (p.IsNullable && !row.AttachedProperty.IsNullable)
                        return;
                    switch (row.AttachedProperty.DataType.DataTypeEnum)
                    {
                        case EnumDataType.CATALOG:
                            if (p.DataType.DataTypeEnum != row.AttachedProperty.DataType.DataTypeEnum)
                                return;
                            if (p.DataType.ObjectGuid != row.AttachedProperty.DataType.ObjectGuid)
                                return;
                            break;
                        case EnumDataType.CATALOGS:
                            return;
                        case EnumDataType.CHAR:
                            if (p.DataType.DataTypeEnum != EnumDataType.CHAR && p.DataType.DataTypeEnum != EnumDataType.STRING)
                                return;
                            break;
                        case EnumDataType.DATE:
                            if (p.DataType.DataTypeEnum != row.AttachedProperty.DataType.DataTypeEnum)
                                return;
                            break;
                        case EnumDataType.DATETIMELOCAL:
                            if (p.DataType.DataTypeEnum != row.AttachedProperty.DataType.DataTypeEnum)
                                return;
                            break;
                        case EnumDataType.DATETIMEOFFSET:
                            if (p.DataType.DataTypeEnum != row.AttachedProperty.DataType.DataTypeEnum)
                                return;
                            break;
                        case EnumDataType.DATETIMEUTC:
                            if (p.DataType.DataTypeEnum != row.AttachedProperty.DataType.DataTypeEnum)
                                return;
                            break;
                        case EnumDataType.DATETIMEZ:
                            if (p.DataType.DataTypeEnum != row.AttachedProperty.DataType.DataTypeEnum)
                                return;
                            break;
                        case EnumDataType.DOCUMENT:
                            if (p.DataType.DataTypeEnum != row.AttachedProperty.DataType.DataTypeEnum)
                                return;
                            break;
                        case EnumDataType.DOCUMENTS:
                            return;
                        case EnumDataType.ENUMERATION:
                            if (p.DataType.DataTypeEnum != row.AttachedProperty.DataType.DataTypeEnum)
                                return;
                            if (p.DataType.EnumerationType != row.AttachedProperty.DataType.EnumerationType)
                                return;
                            break;
                        case EnumDataType.NUMERICAL:
                            if (p.DataType.Accuracy > 0 && p.DataType.Accuracy > row.AttachedProperty.DataType.Accuracy)
                                return;
                            if (p.DataType.Length > row.AttachedProperty.DataType.Length)
                                return;
                            break;
                        case EnumDataType.STRING:
                            if (p.DataType.DataTypeEnum != row.AttachedProperty.DataType.DataTypeEnum)
                                return;
                            break;
                        case EnumDataType.TIME:
                            if (p.DataType.DataTypeEnum != row.AttachedProperty.DataType.DataTypeEnum)
                                return;
                            break;
                        case EnumDataType.TIMESPAN:
                            if (p.DataType.DataTypeEnum != row.AttachedProperty.DataType.DataTypeEnum)
                                return;
                            break;
                        case EnumDataType.TIMESPAN_TIME_ONLY:
                            if (p.DataType.DataTypeEnum != row.AttachedProperty.DataType.DataTypeEnum)
                                return;
                            break;
                        case EnumDataType.TIMEZ:
                            break;
                        default:
                            if (p.DataType.DataTypeEnum != row.AttachedProperty.DataType.DataTypeEnum)
                                return;
                            break;
                    }
                }
                else if (row.RegPropertyGuid != null)
                {
                    if (p.DataType.DataTypeEnum != EnumDataType.NUMERICAL)
                        return;
                    if (row.RegPropertyGuid == this.TableTurnoverPropertyMoneyAccumulatorGuid)
                    {
                        if (p.DataType.Accuracy > 0 && p.DataType.Accuracy > this.TableTurnoverPropertyMoneyAccumulatorAccuracy)
                            return;
                        if (p.DataType.Length > this.TableTurnoverPropertyMoneyAccumulatorLength)
                            return;
                    }
                    else if (row.RegPropertyGuid == this.TableTurnoverPropertyQtyAccumulatorGuid)
                    {
                        if (p.DataType.Accuracy > 0 && p.DataType.Accuracy > this.TableTurnoverPropertyQtyAccumulatorAccuracy)
                            return;
                        if (p.DataType.Length > this.TableTurnoverPropertyQtyAccumulatorLength)
                            return;
                    }
                    else
                        throw new NotImplementedException();
                }
            }
            row.ListToMap.Add(p);
        }
        #region Documents
        [Browsable(false)]
        public SortedObservableCollection<ISortingValue> ListNotSelectedDocuments
        {
            get => _ListNotSelectedDocuments;
            set => SetProperty(ref _ListNotSelectedDocuments, value);
        }
        private SortedObservableCollection<ISortingValue> _ListNotSelectedDocuments = new SortedObservableCollection<ISortingValue>();
        [Browsable(false)]
        public SortedObservableCollection<ISortingValue> ListSelectedDocuments
        {
            get => _ListSelectedDocuments;
            set => SetProperty(ref _ListSelectedDocuments, value);
        }
        private void _ListSelectedDocuments_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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
                            var guid = ((IGuid)t).Guid;
                            var j = -1;
                            for (int i = 0; i < this.ListDocGuids.Count; i++)
                            {
                                if (this.ListDocGuids[i] == guid)
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
                            this.ListDocGuids.Add(((IGuid)t).Guid);
                        }
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                    {
                        foreach (var t in e.OldItems)
                        {
                            var guid = ((IGuid)t).Guid;
                            var j = -1;
                            for (int i = 0; i < this.ListDocGuids.Count; i++)
                            {
                                if (this.ListDocGuids[i] == guid)
                                {
                                    j = i;
                                    break;
                                }
                            }
                            Debug.Assert(j >= 0);
                            this.ListDocGuids.RemoveAt(j);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        private SortedObservableCollection<ISortingValue> _ListSelectedDocuments = new SortedObservableCollection<ISortingValue>();
        #endregion Documents

        #region Mapping
        [Browsable(false)]
        public bool IsShowCompatible
        {
            get => _IsShowCompatible;
            set => SetProperty(ref _IsShowCompatible, value);
        }
        private bool _IsShowCompatible = true;
        [Browsable(false)]
        public ISortingValue? SelectedDoc
        {
            get => _SelectedDoc;
            set
            {
                if (SetProperty(ref _SelectedDoc, value))
                {
                    if (_SelectedDoc == null)
                    {
                        this.VisibilityTextDocNotSelected = Visibility.Visible;
                        this.VisibilityTextDocSelected = Visibility.Hidden;
                    }
                    else
                    {
                        this.VisibilityTextDocNotSelected = Visibility.Hidden;
                        this.VisibilityTextDocSelected = Visibility.Visible;
                        this.TextDocSelected = $"Mapping register to '{((IName)_SelectedDoc).Name}' document properties";
                    }
                    UpdateListMappings();
                }
            }
        }
        private ISortingValue? _SelectedDoc;
        private ObservableCollection<Property> fulListToMap = new ObservableCollection<Property>();
        [Browsable(false)]
        public Visibility VisibilityTextDocNotSelected
        {
            get => _VisibilityTextDocNotSelected;
            set => SetProperty(ref _VisibilityTextDocNotSelected, value);
        }
        private Visibility _VisibilityTextDocNotSelected = Visibility.Visible;
        [Browsable(false)]
        public string TextDocSelected
        {
            get => _TextDocSelected;
            set => SetProperty(ref _TextDocSelected, value);
        }
        private string _TextDocSelected = string.Empty;
        [Browsable(false)]
        public Visibility VisibilityTextDocSelected
        {
            get => _VisibilityTextDocSelected;
            set => SetProperty(ref _VisibilityTextDocSelected, value);
        }
        private Visibility _VisibilityTextDocSelected = Visibility.Hidden;
        [Browsable(false)]
        public ObservableCollection<MappingRow> ListMappings
        {
            get => _ListMappings;
            set => SetProperty(ref _ListMappings, value);
        }
        private ObservableCollection<MappingRow> _ListMappings = new ObservableCollection<MappingRow>();
        #endregion Mapping

        #endregion Editor
    }
    public class MappingRow : ObservableObject
    {
        public Document Doc { get; private set; }
        public Register Reg { get; private set; }
        public RegisterDimension? Dimension { get; private set; }
        public Property? AttachedProperty { get; private set; }
        public string RegPropertyGuid { get; private set; }
        public MappingRow(Document doc, Register reg, RegisterDimension dim)
        {
            this.Doc = doc;
            this.Reg = reg;
            this.Dimension = dim;
            this.RegPropertyGuid = dim.Guid;
            this.Name = dim.Name;
        }
        public MappingRow(Document doc, Register reg, string regPropertyGuid, string accumulatorName)
        {
            this.Doc = doc;
            this.Reg = reg;
            this.RegPropertyGuid = regPropertyGuid;
            this.Name = accumulatorName;
        }
        public string Name { get; private set; }
        public Property? Selected
        {
            get => _Selected;
            set
            {
                if (SetProperty(ref _Selected, value))
                {
                    if (_Selected != null)
                        this.Reg.MappingRegPropertyAdd(this.Doc.Guid, this.RegPropertyGuid, _Selected.Guid);
                    else
                        this.Reg.MappingRegPropertyRemove(this.Doc.Guid, this.RegPropertyGuid);
                }
            }
        }
        private Property? _Selected;
        public ObservableCollection<Property> ListToMap
        {
            get => _ListToMap;
            set => SetProperty(ref _ListToMap, value);
        }
        private ObservableCollection<Property> _ListToMap = new ObservableCollection<Property>();
    }
}
