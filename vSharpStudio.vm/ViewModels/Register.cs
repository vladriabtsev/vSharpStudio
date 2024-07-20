using System;
using System.Linq;
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
using System.Text.RegularExpressions;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class Register : ICanAddNode, ICanGoLeft, INodeGenSettings, ITreeConfigNodeSortable, IEditableNode //, IRoleAccess, IPropertyAccessRoles
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Docs:{this.ListObjectDocRefs.Count} Dims:{this.GroupRegisterDimensions.ListDimensions.Count} Atchs:{this.GroupProperties.ListProperties.Count} Maps:{this.ListDocMappings.Count}";
        }
        public string GetDebuggerDisplay(bool isOptimistic)
        {
            var sb = new StringBuilder();
            sb.Append("REG ");
            sb.Append(this.Name);
            sb.Append(", ");
            sb.Append(this.ParentGroupListRegisters.ParentGroupDocuments.ParentModel.PKeyName);
            sb.Append(":{");
            sb.Append(this.ParentGroupListRegisters.ParentGroupDocuments.ParentModel.PKeyName);
            sb.Append(",nq}");
            sb.Append(", Doc:{DocDescr,nq}");
            sb.Append(", Turnovers:{ListTurnovers.Count}");
            if (isOptimistic)
            {
                sb.Append(" RecVer:{");
                sb.Append(this.ParentGroupListRegisters.ParentGroupDocuments.ParentModel.RecordVersionFieldName);
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
            sb.Append(this.ParentGroupListRegisters.ParentGroupDocuments.ParentModel.PKeyName);
            sb.Append(":{");
            sb.Append(this.ParentGroupListRegisters.ParentGroupDocuments.ParentModel.PKeyName);
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
                sb.Append(this.ParentGroupListRegisters.ParentGroupDocuments.ParentModel.RecordVersionFieldName);
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
            this._PropertyRefTimeline = (Property)this.Cfg.Model.GetPropertyRef(this, this.Cfg.Model.GroupDocuments.DocumentTimeline, System.Guid.NewGuid().ToString(),
                                            "Ref" + this.Cfg.Model.GroupDocuments.DocumentTimeline.CompositeName, 0, false);
            this._IndexDocDateGuid = System.Guid.NewGuid().ToString();
            this._IndexDocIdTypeGuid = System.Guid.NewGuid().ToString();
            this._TableTurnoverGuid = System.Guid.NewGuid().ToString();
            this._TableTurnoverPropertyIdGuid = System.Guid.NewGuid().ToString();
            this._TableTurnoverPropertyVersionGuid = System.Guid.NewGuid().ToString();
            this._TableTurnoverPropertyPostDateGuid = System.Guid.NewGuid().ToString();
            this._TableBalanceGuid = System.Guid.NewGuid().ToString();
            this._TableBalancePropertyIdGuid = System.Guid.NewGuid().ToString();
            this._TableBalancePropertyVersionGuid = System.Guid.NewGuid().ToString();
            this._TableBalancePropertyDateGuid = System.Guid.NewGuid().ToString();
            this._TableBalancePropertyMoneyAccumulatorGuid = System.Guid.NewGuid().ToString();
            this._TableBalancePropertyQtyAccumulatorGuid = System.Guid.NewGuid().ToString();

            this._PropertyDocRefGuidName = "DocGuid";
            this._PropertyDocRefName = "Doc";
            this._RegisterType = EnumRegisterType.TURNOVER;
            this._RegisterBalancePeriodicity = EnumRegisterBalancePeriodicity.REGISTER_PERIOD_MONTH;
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
            children.Add(this.GroupProperties, 1);
            children.Add(this.GroupReports, 2);
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
            var model = this.ParentGroupListRegisters.ParentGroupDocuments.ParentModel;
            node.ShortId = model.LastTypeShortIdForNode();
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
        partial void OnRegisterTypeChanged()
        {
            this.OnPropertyChanged(nameof(this.PropertyDefinitions));
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            if (this.RegisterType != EnumRegisterType.BALANCE_AND_TURNOVER)
            {
                lst.Add(nameof(this.RegisterBalancePeriodicity));
                lst.Add(nameof(this.RegisterBalanceWeeklyStartDay));
            }
            else if (this.RegisterBalancePeriodicity != EnumRegisterBalancePeriodicity.REGISTER_PERIOD_WEEK)
            {
                lst.Add(nameof(this.RegisterBalanceWeeklyStartDay));
            }
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
            d.Position = this.GroupProperties.GetNextPosition();
            this.GroupRegisterDimensions.ListDimensions.Add(d);
            return d;
        }
        public RegisterDimension AddDimension(string name, ICatalog c)
        {
            var d = new RegisterDimension(this.GroupRegisterDimensions) { Name = name, DimensionCatalogGuid = c.Guid };
            d.Position = this.GroupProperties.GetNextPosition();
            this.GroupRegisterDimensions.ListDimensions.Add(d);
            return d;
        }
        public Property AddAttachedProperty(string name, EnumDataType type = EnumDataType.STRING, uint length = 0, uint accuracy = 0, string? guid = null)
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
        public void GetNormalBalanceProperties(List<IProperty> res)
        {
            var lst = this.GetIncludedBalanceProperties("", false, true);
            foreach (var t in lst)
            {
                res.Add(t);
            }
        }
        public void GetNormalTurnoverProperties(List<IProperty> res)
        {
            var lst = this.GetIncludedTurnoverProperties("", false, true);
            foreach (var t in lst)
            {
                res.Add(t);
            }
        }
        //public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial)
        //{
        //    var lst = new List<IProperty>();
        //    var m = this.ParentGroupListRegisters.ParentGroupDocuments.ParentModel;

        //    if (!isExcludeSpecial)
        //    {
        //        // Id
        //        var pId = m.GetPropertyPkId(this, this.Cfg.Model.PropertyIdGuid); // position 6
        //        pId.TagInList = "id";
        //        lst.Add(pId);

        //        //// RefTimeline
        //        //var timelineName = "Ref" + this.ParentGroupListRegisters.ParentGroupDocuments.DocumentTimeline.CompositeName;
        //        //var pRefTimeline = m.GetPropertyTimeline(this.GroupProperties, this.Cfg.Model.PropertyIdGuid, timelineName, 0, false, true);
        //        ////var pId = m.GetPropertyPkId(this, this.Guid); // position 6
        //        //pRefTimeline.TagInList = "id";
        //        //lst.Add(pRefTimeline);

        //        if (isOptimistic)
        //        {
        //            // Version
        //            var pVer = m.GetPropertyVersion(this, this.Cfg.Model.PropertyVersionGuid); // position 7
        //            pVer.TagInList = "vr";
        //            lst.Add(pVer);
        //        }
        //        //var pRegRef = (Property)m.GetPropertyRef(this, this.Guid, "Ref" + this.CompositeName, 11); // position 11
        //        //pRegRef.TagInList = "rr";
        //        //lst.Add(pRegRef);
        //    }
        //    // For all attached properties.
        //    foreach (var t in this.GroupProperties.ListProperties)
        //    {
        //        lst.Add(t);
        //    }
        //    return lst;
        //}
        public IReadOnlyList<IProperty> GetIncludedTurnoverProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial)
        {
            var lst = new List<IProperty>();
            var m = this.ParentGroupListRegisters.ParentGroupDocuments.ParentModel;

            // Id
            var pId = m.GetPropertyPkId(this, this.Cfg.Model.PropertyIdGuid); // position 6
            pId.TagInList = "id";
            lst.Add(pId);

            this.PropertyRefTimeline.Name = "Ref" + this.Cfg.Model.GroupDocuments.DocumentTimeline.CompositeName;
            var pRefTimeline = this.PropertyRefTimeline;
            pRefTimeline.Position = IProperty.PropertyRefParentPosition;
            pRefTimeline.IsRefTimeline = true;
            lst.Add(pRefTimeline);

            //if (isOptimistic) - not relevant for registers
            //{
            //    // Version
            //    var pVer = m.GetPropertyVersion(this, this.Cfg.Model.PropertyVersionGuid); // position 7
            //    pVer.TagInList = "vr";
            //    lst.Add(pVer);
            //}

            //// Post date
            //var pPostDate = m.GetPropertyDateTimeUtc(this, this.TableTurnoverPropertyPostDateGuid, "PostDate", 9, false); // position 9
            //pPostDate.TagInList = "pd";
            //lst.Add(pPostDate);

            // Money accumulator
            if (this.UseMoneyAccumulator)
            {
                var pMoney = (Property)m.GetPropertyNumber(this, this.TableTurnoverPropertyMoneyAccumulatorGuid, this.TableTurnoverPropertyMoneyAccumulatorName, this.TableTurnoverPropertyMoneyAccumulatorLength, this.TableTurnoverPropertyMoneyAccumulatorAccuracy, false);
                pMoney.Position = IProperty.PropertyMoneyAccumulatorPosition;
                pMoney.TagInList = "ma";
                lst.Add(pMoney);
            }

            // Qty accumulator
            if (this.UseQtyAccumulator)
            {
                var pQty = (Property)m.GetPropertyNumber(this, this.TableTurnoverPropertyQtyAccumulatorGuid, this.TableTurnoverPropertyQtyAccumulatorName, this.TableTurnoverPropertyQtyAccumulatorLength, this.TableTurnoverPropertyQtyAccumulatorAccuracy, false);
                pQty.Position = IProperty.PropertyQtyAccumulatorPosition;
                pQty.TagInList = "qa";
                lst.Add(pQty);
            }

            // Reference to register header
            //var pRegRef = (Property)m.GetPropertyRef(this, this.Guid, "Ref" + this.CompositeName, 13);
            //pRegRef.TagInList = "rr";
            //lst.Add(pRegRef);

            //var pDoc = (Property)m.GetPropertyDocuments(this, this.PropertyDocRefGuid, this.PropertyDocRefName, this.ListObjectDocRefs, 15, false);
            //lst.Add(pDoc);

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
                            var pCat = Property.Clone(t, t.PropertyRefDimensionCatalog, true);
                            pCat.Position = t.Position;
                            pCat.IsPKey = false;
                            pCat.IsComplex = true;
                            pCat.IsNullable = false;
                            pCat.IsCsNullable = true;
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
            foreach (var t in this.GroupProperties.ListProperties)
            {
                lst.Add(t);
            }
            return lst;
        }
        public IReadOnlyList<IProperty> GetIncludedBalanceProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial)
        {
            var lst = new List<IProperty>();
            var m = this.ParentGroupListRegisters.ParentGroupDocuments.ParentModel;

            //// Id
            //var pId = m.GetPropertyPkId(this, this.TableBalancePropertyIdGuid); // position 6
            //pId.TagInList = "id";
            //lst.Add(pId);

            if (this.RegisterType != EnumRegisterType.BALANCE)
            {

                // Balance date
                var pPostDate = (Property)m.GetPropertyDateTimeUtc(this, this.TableBalancePropertyDateGuid, "OnDateTime", 9, false); // position 9
                pPostDate.TagInList = "pd";
                pPostDate.DataType.IsPKey = true;
                //pPostDate.IsBalanceDate = true;
                lst.Add(pPostDate);
            }

            //if (isOptimistic) -not relevant for registers
            //{
            //    // Version
            //    var pVer = m.GetPropertyVersion(this, this.TableBalancePropertyVersionGuid); // position 7
            //    pVer.TagInList = "vr";
            //    lst.Add(pVer);
            //}

            // Money accumulator
            if (this.UseMoneyAccumulator)
            {
                var pMoney = (Property)m.GetPropertyNumber(this, this.TableBalancePropertyMoneyAccumulatorGuid, this.TableTurnoverPropertyMoneyAccumulatorName, this.TableTurnoverPropertyMoneyAccumulatorLength, this.TableTurnoverPropertyMoneyAccumulatorAccuracy, false);
                pMoney.Position = IProperty.PropertyMoneyAccumulatorPosition;
                pMoney.TagInList = "ma";
                lst.Add(pMoney);
            }

            // Qty accumulator
            if (this.UseQtyAccumulator)
            {
                var pQty = (Property)m.GetPropertyNumber(this, this.TableBalancePropertyQtyAccumulatorGuid, this.TableTurnoverPropertyQtyAccumulatorName, this.TableTurnoverPropertyQtyAccumulatorLength, this.TableTurnoverPropertyQtyAccumulatorAccuracy, false);
                pQty.Position = IProperty.PropertyQtyAccumulatorPosition;
                pQty.TagInList = "qa";
                lst.Add(pQty);
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
                            var pCat = Property.Clone(t, t.PropertyRefDimensionCatalog, true);
                            pCat.Position = t.Position;
                            pCat.IsPKey = true;
                            pCat.IsComplex = true;
                            pCat.IsNullable = false;
                            pCat.IsCsNullable = true;
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
            return lst;
        }
        //public IReadOnlyList<IDetail> GetIncludedSubItems(string guidAppPrjDbGen)
        //{
        //    var res = new List<IDetail>();
        //    foreach (var t in this.GroupRegisterDimensions.ListDimensions)
        //    {
        //        res.Add(t);
        //    }
        //    return res;
        //}
        //public IReadOnlyList<IForm> GetListForms(string guidAppPrjGen)
        //{
        //    var res = new List<IForm>
        //    {
        //        this.GetForm(FormType.ListComboBox, guidAppPrjGen),
        //        this.GetForm(FormType.ListDataGrid, guidAppPrjGen)
        //    };
        //    return res;
        //}
        //public IForm GetForm(FormType ftype, string guidAppPrjGen)
        //{
        //    var f = (from tf in this.GroupForms.ListForms where tf.EnumFormType == ftype select tf).SingleOrDefault();
        //    if (f == null)
        //    {
        //        var lstp = new List<IProperty>();
        //        if (this.RegisterType == EnumRegisterType.TURNOVER)
        //        {
        //            //lstp.AddRange(this.GetIncludedProperties(guidAppPrjGen, false, false));
        //            lstp.AddRange(this.GetIncludedTurnoverProperties(guidAppPrjGen, false, false));
        //        }
        //        else
        //            ThrowHelper.ThrowPlatformNotSupportedException();
        //        if (ftype == FormType.ListDataGrid)
        //        {
        //        }
        //        if (lstp.Count == 0)
        //        {
        //            int i = 0;
        //            foreach (var t in this.GroupProperties.ListProperties)
        //            {
        //                if (t.IsIncluded(guidAppPrjGen))
        //                {
        //                    i++;
        //                    if (i > 1)
        //                        break;
        //                    lstp.Add(t);
        //                }
        //            }
        //        }
        //        f = new Form(this.GroupForms, ftype, lstp);
        //    }
        //    else
        //    {
        //        var lstp = new List<IProperty>();
        //        foreach (var t in f.ListAllNotSpecialProperties)
        //        {
        //            lstp.Add((IProperty)t);
        //        }
        //        f = new Form(this.GroupForms, ftype, lstp);
        //    }
        //    return f;
        //}

        #region Mapping Editor

        private bool isOnOpeningEditor = false;
        public override void OnOpeningEditor()
        {
            this.isOnOpeningEditor = true;

            #region ListNotSelectedDocuments
            this.ListNotSelectedDocuments.Clear();
            foreach (var t in this.Cfg.Model.GroupDocuments.GroupListDocuments.ListDocuments)
            {
                bool found = false;
                foreach (var tt in this.ListObjectDocRefs)
                {
                    if (t.Guid == tt.ForeignObjectGuid)
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
                foreach (var tt in this.ListObjectDocRefs)
                {
                    if (t.Guid == tt.ForeignObjectGuid)
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
            Register.UpdateListMappings(this, this.SelectedDoc);
            #endregion ListMappings

            this.isOnOpeningEditor = false;
        }
        public static void MappingRegDocRemove(Register reg, string docGuid)
        {
            Guard.IsNotNullOrWhiteSpace(docGuid);
            int iat = -1;
            bool found = false;
            foreach (var t in reg.ListDocMappings)
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
            reg.ListDocMappings.RemoveAt(iat);
            Debug.Assert(reg.mappingDic.ContainsKey(docGuid));
            reg.mappingDic.Remove(docGuid);
            reg.IsChanged = true;
        }
        public static void MappingRegDocAdd(Register reg, string docGuid)
        {
            Guard.IsNotNullOrWhiteSpace(docGuid);
#if DEBUG
            bool found = false;
            foreach (var t in reg.ListDocMappings)
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
            reg.ListDocMappings.Add(new RegisterDocToReg() { DocGuid = docGuid });
            reg.IsChanged = true;
        }
        public static void MappingRegPropertyRemove(Register reg, string docGuid, string regPropertyGuid)
        {
            Guard.IsNotNullOrWhiteSpace(docGuid);
            Guard.IsNotNullOrWhiteSpace(regPropertyGuid);
            RegisterDocToReg? rec = null;
            foreach (var t in reg.ListDocMappings)
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
            Debug.Assert(reg.mappingDic.ContainsKey(docGuid));
            var dic = reg.mappingDic[docGuid];
            Debug.Assert(dic.ContainsKey(regPropertyGuid));
            dic.Remove(regPropertyGuid);
            reg.IsChanged = true;
        }
        public static void MappingRegPropertyAdd(Register reg, string docGuid, string regPropertyGuid, string docPropertyGuid)
        {
            Guard.IsNotNullOrWhiteSpace(docGuid);
            Guard.IsTrue(reg.Cfg.DicNodes.ContainsKey(docGuid));
            Guard.IsNotNullOrWhiteSpace(regPropertyGuid);
            //Guard.IsTrue(this.Cfg.DicNodes.ContainsKey(regPropertyGuid));
            Guard.IsNotNullOrWhiteSpace(docPropertyGuid);
            Guard.IsTrue(reg.Cfg.DicNodes.ContainsKey(docPropertyGuid));
            RegisterDocToReg? rec = null;
            foreach (var t in reg.ListDocMappings)
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
                reg.ListDocMappings.Add(rec);
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
                reg.IsChanged = true;
            }
            else
            {
                if (mapToProp.DocPropGuid != docPropertyGuid)
                {
                    mapToProp.DocPropGuid = docPropertyGuid;
                    reg.IsChanged = true;
                }
            }
            if (!reg.mappingDic.ContainsKey(docGuid))
                reg.mappingDic[docGuid] = new Dictionary<string, string>();
            var dic = reg.mappingDic[docGuid];
            dic[regPropertyGuid] = docPropertyGuid;
        }
        public static string GetMappingToDocPropertyGuid(Register reg, string docGuid, string regPropertyGuid)
        {
            var res = string.Empty;

            return res;
        }
        public static void UpdateListMappings(Register? reg, ISortingValue? selectedDoc)
        {
            if (reg == null)
                return;
            reg.fulListToMap.Clear();
            reg.ListMappings.Clear();
            if (selectedDoc != null)
            {
                var doc = (Document)selectedDoc;
                foreach (var t in reg.GroupRegisterDimensions.ListDimensions)
                {
                    var row = new RegisterMappingRow(doc, reg, t);
                    reg.ListMappings.Add(row);
                }
                if (reg.UseQtyAccumulator)
                {
                    var row = new RegisterMappingRow(doc, reg, reg.TableTurnoverPropertyQtyAccumulatorGuid, reg.TableTurnoverPropertyQtyAccumulatorName);
                    reg.ListMappings.Add(row);
                }
                if (reg.UseMoneyAccumulator)
                {
                    var row = new RegisterMappingRow(doc, reg, reg.TableTurnoverPropertyMoneyAccumulatorGuid, reg.TableTurnoverPropertyMoneyAccumulatorName);
                    reg.ListMappings.Add(row);
                }
                foreach (var t in reg.GroupProperties.ListProperties)
                {
                    var row = new RegisterMappingRow(doc, reg, t.Guid, t.Name);
                    reg.ListMappings.Add(row);
                }
                foreach (var t in doc.ParentGroupListDocuments.ParentGroupDocuments.DocumentTimeline.ListProperties)
                {
                    reg.fulListToMap.Add(t);
                }
                foreach (var t in doc.GroupProperties.ListProperties)
                {
                    reg.fulListToMap.Add(t);
                }
                foreach (var tt in doc.GroupDetails.ListDetails)
                {
                    Register.AddDetailProperties(reg, tt);
                }
                if (!reg.mappingDic.ContainsKey(doc.Guid))
                    reg.mappingDic[doc.Guid] = new Dictionary<string, string>();
                var dicRegPropToDocProp = reg.mappingDic[doc.Guid];
                // select properties for mapping
                foreach (var t in reg.ListMappings)
                {
                    Property? selected = null;
                    int cnt = 0;
                    foreach (var tt in reg.fulListToMap)
                    {
                        Register.AddCompatibleProperty(reg, t, tt);
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
                            if (reg.TableTurnoverPropertyMoneyAccumulatorGuid == t.RegPropertyGuid && dicRegPropToDocProp.ContainsKey(t.RegPropertyGuid) && tt.Guid == dicRegPropToDocProp[t.RegPropertyGuid])
                            {
                                selected = tt;
                                cnt++;
                            }
                            else if (reg.TableTurnoverPropertyQtyAccumulatorGuid == t.RegPropertyGuid && dicRegPropToDocProp.ContainsKey(t.RegPropertyGuid) && tt.Guid == dicRegPropToDocProp[t.RegPropertyGuid])
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
        public static void AddDetailProperties(Register reg, Detail d)
        {
            foreach (var t in d.GroupProperties.ListProperties)
            {
                reg.fulListToMap.Add(t);
            }
            foreach (var tt in d.GroupDetails.ListDetails)
            {
                Register.AddDetailProperties(reg, tt);
            }
        }
        public static void AddCompatibleProperty(Register reg, RegisterMappingRow row, Property p)
        {
            if (reg.IsShowCompatible)
            {
                if (row.Dimension != null)
                {
                    if (string.IsNullOrEmpty(row.Dimension.DimensionCatalogGuid))
                        return;
                    var cat = reg.Cfg.DicNodes[row.Dimension.DimensionCatalogGuid];
                    if (p.DataType.DataTypeEnum != EnumDataType.CATALOG || cat.Guid != p.DataType.ObjectRef.ForeignObjectGuid)
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
                            if (p.DataType.ObjectRef.ForeignObjectGuid != row.AttachedProperty.DataType.ObjectRef.ForeignObjectGuid)
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
                        //case EnumDataType.TIMEZ:
                        //    break;
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
                    if (row.RegPropertyGuid == reg.TableTurnoverPropertyMoneyAccumulatorGuid)
                    {
                        if (p.DataType.Accuracy > 0 && p.DataType.Accuracy > reg.TableTurnoverPropertyMoneyAccumulatorAccuracy)
                            return;
                        if (p.DataType.Length > reg.TableTurnoverPropertyMoneyAccumulatorLength)
                            return;
                    }
                    else if (row.RegPropertyGuid == reg.TableTurnoverPropertyQtyAccumulatorGuid)
                    {
                        if (p.DataType.Accuracy > 0 && p.DataType.Accuracy > reg.TableTurnoverPropertyQtyAccumulatorAccuracy)
                            return;
                        if (p.DataType.Length > reg.TableTurnoverPropertyQtyAccumulatorLength)
                            return;
                    }
                    else
                        return;
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
                            for (int i = 0; i < this.ListObjectDocRefs.Count; i++)
                            {
                                if (this.ListObjectDocRefs[i].ForeignObjectGuid == guid)
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
                            this.ListObjectDocRefs.Add(new ComplexRef("", ((Document)t).Guid));
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
                            for (int i = 0; i < this.ListObjectDocRefs.Count; i++)
                            {
                                if (this.ListObjectDocRefs[i].ForeignObjectGuid == guid)
                                {
                                    j = i;
                                    break;
                                }
                            }
                            Debug.Assert(j >= 0);
                            this.ListObjectDocRefs.RemoveAt(j);
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
        public RegisterDocToReg? RegisterDocToReg
        {
            get => _RegisterDocToReg;
            set => SetProperty(ref _RegisterDocToReg, value);
        }
        private RegisterDocToReg? _RegisterDocToReg;
        [Browsable(false)]
        public ISortingValue? SelectedDoc
        {
            get => _SelectedDoc;
            set
            {
                if (value == null)
                {
                    if (_SelectedDoc != null)
                    {
                        var guid = ((IGuid)_SelectedDoc).Guid;
                        foreach (var t in this.ListSelectedDocuments)
                        {
                            if (((IGuid)t).Guid == guid)
                            {
                                return;
                            }
                        }
                    }
                }
                if (SetProperty(ref _SelectedDoc, value))
                {
                    if (_SelectedDoc == null)
                    {
                        this.VisibilityTextDocNotSelected = Visibility.Visible;
                        this.VisibilityTextDocSelected = Visibility.Hidden;
                        this.RegisterDocToReg = null;
                    }
                    else
                    {
                        this.VisibilityTextDocNotSelected = Visibility.Hidden;
                        this.VisibilityTextDocSelected = Visibility.Visible;
                        this.TextDocSelected = $"Mapping register '{this.Name}' to '{((IName)_SelectedDoc).Name}' document properties";
                        var dGuid = ((Document)_SelectedDoc).Guid;
                        foreach (var t in this.ListDocMappings)
                        {
                            if (t.DocGuid == dGuid)
                            {
                                this.RegisterDocToReg = t;
                                break;
                            }
                        }

                    }
                    Register.UpdateListMappings(this, this.SelectedDoc);
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
        public ObservableCollection<RegisterMappingRow> ListMappings
        {
            get => _ListMappings;
            set => SetProperty(ref _ListMappings, value);
        }
        private ObservableCollection<RegisterMappingRow> _ListMappings = new ObservableCollection<RegisterMappingRow>();
        #endregion Mapping

        #endregion Mapping Editor
    }
}
