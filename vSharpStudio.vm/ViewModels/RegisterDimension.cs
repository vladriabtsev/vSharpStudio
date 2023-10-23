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
    public partial class RegisterDimension : ICanAddNode, ICanGoLeft, INodeGenSettings, ITreeConfigNodeSortable, IEditableNode //, IRoleAccess, IPropertyAccessRoles
    {
        //partial void OnDebugStringExtend(ref string mes)
        //{
        //    mes = mes + $" Type:{this.}";
        //}
        [Browsable(false)]
        public GroupListDimensions ParentGroupListDimensions { get { Debug.Assert(this.Parent != null); return (GroupListDimensions)this.Parent; } }
        [Browsable(false)]
        public IGroupListDimensions ParentGroupListDimensionsI { get { Debug.Assert(this.Parent != null); return (IGroupListDimensions)this.Parent; } }
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
            return this.ParentGroupListDimensions.Children;
        }
        public override bool HasChildren()
        {
            return false;
        }
        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconDimension"; } }
        //protected override string GetNodeIconName() { return "iconProperty"; }
        //[Browsable(false)]
        //public string? ComplexObjectName { get; set; }
        //public string ComplexObjectNameWithDot() { if (!string.IsNullOrEmpty(this.ComplexObjectName)) return $"{this.ComplexObjectName}."; return ""; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            this._PropertyDimensionGuid = System.Guid.NewGuid().ToString();
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
                if (this.ParentGroupListDimensions.ListDimensions.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Register?)this.ParentGroupListDimensions.ListDimensions.GetPrev(this);
            if (prev == null)
                return;
            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupListDimensions.ListDimensions.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListDimensions.ListDimensions.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Register?)this.ParentGroupListDimensions.ListDimensions.GetNext(this);
            if (next == null)
                return;
            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupListDimensions.ListDimensions.MoveDown(this);
            this.SetSelected(this);
        }

        public void NodeRemove(bool ask = true)
        {
            this.ParentGroupListDimensions.ListDimensions.Remove(this);
            this.Parent = null;
        }
        public override ITreeConfigNode NodeAddClone()
        {
            Debug.Assert(this.Parent != null);
            var node = RegisterDimension.Clone(this.Parent, this, true, true);
            this.ParentGroupListDimensions.ListDimensions.Add(node);
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
            this.ParentGroupListDimensions.ListDimensions.Add(node);
            this.GetUniqueName(Defaults.RegisterDimensionName, node, this.ParentGroupListDimensions.ListDimensions);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListDimensions.ListDimensions.Remove(this);
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

        [Browsable(false)]
        public SortedObservableCollection<ITreeConfigNodeSortable>? ListCatalogs
        {
            get
            {
                Debug.Assert(this.Cfg != null);
                var lst = new List<ICatalog>();
                var hs = new HashSet<string>();
                foreach(var t in this.ParentGroupListDimensions.ListDimensions)
                {
                    if (t.Guid == this.Guid)
                        continue;
                   if (!string.IsNullOrWhiteSpace(t.CatalogGuid))
                        hs.Add(t.CatalogGuid);
                }
                foreach(var t in this.Cfg.Model.GroupCatalogs.ListCatalogs)
                {
                    if (hs.Contains(t.Guid))
                        continue;
                    lst.Add(t);
                }
                return new SortedObservableCollection<ITreeConfigNodeSortable>(lst);
            }
        }
    }
}
