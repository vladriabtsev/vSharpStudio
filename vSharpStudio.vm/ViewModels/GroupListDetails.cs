using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class GroupListDetails : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNodeGroup, IRoleAccess
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{ListDetails.Count}";
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            if (this.Parent is Catalog c)
            {
                return c.Children;
            }
            else if (this.Parent is CatalogFolder cf)
            {
                return cf.Children;
            }
            else if (this.Parent is Document d)
            {
                return d.Children;
            }
            else if (this.Parent is Detail dt)
            {
                return dt.Children;
            }
            throw new NotImplementedException();
        }
        #endregion ITree

        public new ConfigNodesCollection<Detail> Children { get { return this.ListDetails; } }
        partial void OnCreated()
        {
            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this.ListDetails.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListDetails.OnAddedAction = (t) =>
            {
                t.OnAdded();
                t.InitRoles();
            };
            this.ListDetails.OnRemovedAction = (t) => {
                this.OnRemoveChild();
            };
            this.ListDetails.OnClearedAction = () => {
                this.OnRemoveChild();
            };
            this._Name = Defaults.GroupDetailsName;
        }
        public int IndexOf(IDetail det)
        {
            return this.ListDetails.IndexOf((det as Detail)!);
        }
        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public Detail AddPropertiesTab(string name)
        {
            var node = new Detail(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            Detail node = null!;
            if (node_impl == null)
            {
                node = new Detail(this);
            }
            else
            {
                node = (Detail)node_impl;
            }
            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.DetailName, node, this.ListDetails);
            }
            var cfg = (Config)this.Cfg;
            node.ShortId = cfg.Model.LastDetailShortId + 1;
            cfg.Model.LastDetailShortId = node.ShortId;
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        public Detail AddTab(string name)
        {
            var node = new Detail(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                nameof(this.Description),
                nameof(this.Guid),
                nameof(this.NameUi),
                nameof(this.Parent),
                nameof(this.Children)
            };
            return lst.ToArray();
        }
        public bool GetIsGridSortable()
        {
            if (this.IsGridSortable == EnumUseType.Yes)
                return true;
            if (this.IsGridSortable == EnumUseType.No)
                return false;
            if (this.Parent is Catalog c)
                return c.IsGridSortableGet();
            if (this.Parent is Document d)
                return d.IsGridSortableGet();
            if (this.Parent is Detail dd)
                return dd.IsGridSortableGet();
            if (this.Parent is CatalogFolder cf)
                return cf.IsGridSortableGet();
            throw new NotImplementedException();
        }
        public bool GetIsGridFilterable()
        {
            if (this.IsGridFilterable == EnumUseType.Yes)
                return true;
            if (this.IsGridFilterable == EnumUseType.No)
                return false;
            if (this.Parent is Catalog c)
                return c.IsGridFilterableGet();
            if (this.Parent is Document d)
                return d.IsGridFilterableGet();
            if (this.Parent is Detail dd)
                return dd.IsGridFilterableGet();
            if (this.Parent is CatalogFolder cf)
                return cf.IsGridFilterableGet();
            throw new NotImplementedException();
        }
        public bool GetIsGridSortableCustom()
        {
            if (this.IsGridSortableCustom == EnumUseType.Yes)
                return true;
            if (this.IsGridSortableCustom == EnumUseType.No)
                return false;
            if (this.Parent is Catalog c)
                return c.IsGridSortableCustomGet();
            if (this.Parent is Document d)
                return d.IsGridSortableCustomGet();
            if (this.Parent is Detail dd)
                return dd.IsGridSortableCustomGet();
            if (this.Parent is CatalogFolder cf)
                return cf.IsGridSortableCustomGet();
            throw new NotImplementedException();
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
        internal Dictionary<string, RoleDetailAccess> dicDetailAccess = new();
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
        public EnumCatalogDetailAccess GetRoleDetailAccess(IRole role)
        {
            if (this.dicDetailAccess.TryGetValue(role.Guid, out var r) && r.EditAccess != EnumCatalogDetailAccess.C_BY_PARENT)
                return r.EditAccess;
            if (this.Parent is Detail dd)
                return dd.GetRoleDetailAccess(role);
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
                        return EnumCatalogDetailAccess.C_EDIT_ITEMS;
                    case EnumDocumentAccess.D_MARK_DEL:
                        return EnumCatalogDetailAccess.C_MARK_DEL;
                    case EnumDocumentAccess.D_POST:
                    case EnumDocumentAccess.D_UNPOST:
                        return EnumCatalogDetailAccess.C_EDIT_ITEMS;
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
            if (this.Parent is Detail dd)
                return dd.GetRoleDetailPrint(role);
            else if (this.Parent is Catalog c)
                return c.GetRoleCatalogPrint(role);
            else if (this.Parent is Document d)
                return d.GetRoleDocumentPrint(role);
            else if (this.Parent is CatalogFolder cf)
                return cf.ParentCatalog.GetRoleCatalogPrint(role);
            else
                throw new NotImplementedException();
        }
        //public IReadOnlyList<string> GetRolesByAccess(EnumCatalogDetailAccess access)
        //{
        //    var roles = new List<string>();
        //    foreach (var role in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
        //    {
        //        if (GetRoleDetailAccess(role.Guid) == access)
        //            roles.Add(role.Name);
        //    }
        //    return roles;
        //}
        #endregion Roles
    }
}
