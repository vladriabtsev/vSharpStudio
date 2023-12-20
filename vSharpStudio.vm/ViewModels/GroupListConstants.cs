﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class GroupListConstants : ITreeModel, ICanAddSubNode, ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNodeGroup, IEditableNode, IRoleAccess
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{ListConstants.Count}";
        }
        [Browsable(false)]
        public GroupConstantGroups ParentGroupConstantGroups { get { Debug.Assert(this.Parent != null); return (GroupConstantGroups)this.Parent; } }
        [Browsable(false)]
        public IGroupConstantGroups ParentGroupConstantGroupsI { get { Debug.Assert(this.Parent != null); return (IGroupConstantGroups)this.Parent; } }

        partial void OnCreated()
        {
            this.IsEditable = true;
            this._ShortIdTypeForCacheKey = "t";
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this.ListConstants.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListConstants.OnAddedAction = (t) =>
            {
                t.OnAdded();
                t.InitRoles();
            };
            this.ListConstants.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListConstants.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
        }
        public int IndexOf(IConstant cnst)
        {
            return this.ListConstants.IndexOf((Constant)cnst);
        }

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
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
        }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupConstantGroups.Children;
        }
        [Browsable(false)]
        public new ConfigNodesCollection<Constant> Children { get { return this.ListConstants; } }
        #endregion ITree

        #region Tree operations
        public void Remove()
        {
            this.ParentGroupConstantGroups.ListConstantGroups.Remove(this);
        }
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupConstantGroups.ListConstantGroups.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (GroupListConstants?)this.ParentGroupConstantGroups.ListConstantGroups.GetPrev(this);
            if (prev == null)
                return;
            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupConstantGroups.ListConstantGroups.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupConstantGroups.ListConstantGroups.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (GroupListConstants?)this.ParentGroupConstantGroups.ListConstantGroups.GetNext(this);
            if (next == null)
                return;
            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupConstantGroups.ListConstantGroups.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = GroupListConstants.Clone(this.ParentGroupConstantGroups, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupConstantGroups.ListConstantGroups.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new GroupListConstants(this.ParentGroupConstantGroups);
            this.ParentGroupConstantGroups.ListConstantGroups.Add(node);
            this.GetUniqueName(Defaults.ConstantsGroupName, node, this.ParentGroupConstantGroups.ListConstantGroups);
            this.SetSelected(node);
            return node;
        }
        public bool CanAddSubNode() { return true; }
        public Constant AddConstant(string name, string? guid = null)
        {
            Constant node = new Constant(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node);
            this.NodeAddNewSubNode(node);
            return node;
        }
        //public Constant AddConstant(string name, DataType type)
        //{
        //    Constant node = new Constant(this) { Name = name, DataType = type };
        //    this.NodeAddNewSubNode(node);
        //    return node;
        //}
        public Constant AddConstantString(string name, string? guid = null)
        {
            Constant node = new Constant(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node);
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Constant AddConstantEnumeration(string name, Enumeration en, string? guid = null)
        {
            var node = new Constant(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node);
            node.DataType.ObjectGuid = en.Guid;
            node.DataType.DataTypeEnum = EnumDataType.ENUMERATION;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Constant AddConstantTypeRefCatalog(string name, Catalog cat, string? guid = null)
        {
            var node = new Constant(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node);
            node.DataType.ObjectGuid = cat.Guid;
            node.DataType.DataTypeEnum = EnumDataType.CATALOG;
            node.IsNullable = true;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Constant AddConstantTypeRefDocument(string name, Document d, string? guid = null)
        {
            var node = new Constant(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node);
            node.DataType.ObjectGuid = d.Guid;
            node.DataType.DataTypeEnum = EnumDataType.DOCUMENT;
            node.IsNullable = true;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Constant AddConstantRefAnyCatalogOrDocument(string name, string? guid = null)
        {
            var node = new Constant(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node);
            node.DataType.DataTypeEnum = EnumDataType.ANY;
            node.IsNullable = true;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Constant AddConstantTypeRefCatalogs(string name, Catalog cat, Catalog? cat2 = null, string? guid = null)
        {
            var node = new Constant(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node);
            node.DataType.ListObjectGuids.Add(cat.Guid);
            if (cat2 != null)
                node.DataType.ListObjectGuids.Add(cat2.Guid);
            node.DataType.DataTypeEnum = EnumDataType.CATALOGS;
            node.IsNullable = true;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Constant AddConstantTypeRefDocuments(string name, Document d, Document? d2 = null, string? guid = null)
        {
            var node = new Constant(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node);
            node.DataType.ListObjectGuids.Add(d.Guid);
            if (d2 != null)
                node.DataType.ListObjectGuids.Add(d2.Guid);
            node.DataType.DataTypeEnum = EnumDataType.DOCUMENTS;
            node.IsNullable = true;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            Constant node = null!;
            if (node_impl == null)
            {
                node = new Constant(this);
            }
            else
            {
                node = (Constant)node_impl;
            }
            this.Add(node);
            node.DataType.Parent = node;
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.ConstantName, node, this.ListConstants);
            }
            var model = this.ParentGroupConstantGroups.ParentModel;
            node.ShortId = model.LastConstantShortId + 1;
            model.LastConstantShortId = node.ShortId;

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        public IReadOnlyList<IConstant> GetIncludedConstants(string guidAppPrjGen)
        {
            var res = new List<IConstant>();
            foreach (var t in this.ListConstants)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    res.Add(t);
                }
            }
            return res;
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                //lst.Add(nameof(this.Description));
                nameof(this.Guid),
                //lst.Add(nameof(this.NameUi));
                nameof(this.Parent),
                nameof(this.Children)
            };
            return lst.ToArray();
        }

        #region Roles
        public object GetRoleAccess(IRole role)
        {
            if (!this.dicConstantAccess.ContainsKey(role.Guid))
            {
                var rca = new RoleConstantAccess() { Guid = role.Guid };
                this.ListRoleConstantAccessSettings.Add(rca);
                this.dicConstantAccess[role.Guid] = rca;
            }
            return dicConstantAccess[role.Guid];
        }
        public void SetRoleAccess(IRole role, EnumConstantAccess? edit, EnumPrintAccess? print)
        {
            Debug.Assert(role != null);
            Debug.Assert(dicConstantAccess.ContainsKey(role.Guid));
            if (edit.HasValue)
                dicConstantAccess[role.Guid].EditAccess = edit.Value;
            if (print.HasValue)
                dicConstantAccess[role.Guid].PrintAccess = print.Value;
        }
        internal Dictionary<string, RoleConstantAccess> dicConstantAccess = new();
        public void InitRoles()
        {
            foreach (var tt in this.ListRoleConstantAccessSettings)
            {
                this.dicConstantAccess[tt.Guid] = tt;
            }
            foreach (var t in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (!this.dicConstantAccess.ContainsKey(t.Guid))
                {
                    var rca = new RoleConstantAccess() { Guid = t.Guid };
                    this.dicConstantAccess[t.Guid] = rca;
                }
            }
        }
        public void InitRoleAdd(IRole role)
        {
            var rca = new RoleConstantAccess() { Guid = role.Guid };
            this.ListRoleConstantAccessSettings.Add(rca);
            this.dicConstantAccess[rca.Guid] = rca;
        }
        public void InitRoleRemove(IRole role)
        {
            for (int i = 0; i < this.ListRoleConstantAccessSettings.Count; i++)
            {
                if (this.ListRoleConstantAccessSettings[i].Guid == role.Guid)
                {
                    this.ListRoleConstantAccessSettings.RemoveAt(i);
                    break;
                }
            }
            this.dicConstantAccess.Remove(role.Guid);
        }
        public EnumConstantAccess GetRoleConstantAccess(IRole role)
        {
            if (this.dicConstantAccess.TryGetValue(role.Guid, out var r) && r.EditAccess != EnumConstantAccess.CN_BY_PARENT)
                return r.EditAccess;
            return this.ParentGroupConstantGroups.GetRoleConstantAccess(role);
        }
        public EnumPrintAccess GetRoleConstantPrint(IRole role)
        {
            if (this.dicConstantAccess.TryGetValue(role.Guid, out var r) && r.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                return r.PrintAccess;
            return this.ParentGroupConstantGroups.GetRoleConstantPrint(role);
        }
        public IReadOnlyList<string> GetRolesByAccess(EnumConstantAccess access)
        {
            var roles = new List<string>();
            foreach (var role in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (GetRoleConstantAccess(role) == access)
                    roles.Add(role.Name);
            }
            return roles;
        }
        #endregion Roles
    }
}
