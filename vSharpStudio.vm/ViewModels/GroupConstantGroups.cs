using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("ConstantGroups:{Name,nq} Count:{ListConstantGroups.Count,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class GroupConstantGroups : ITreeModel, ICanGoRight, ICanGoLeft, ICanAddSubNode, INodeGenSettings, IEditableNodeGroup, IRoleAccess
    {
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public Model ParentModel { get { Debug.Assert(this.Parent != null); return (Model)this.Parent; } }
        [Browsable(false)]
        public IModel ParentModelI { get { Debug.Assert(this.Parent != null); return (IModel)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentModel.Children;
        }
        #endregion ITree
        new public ConfigNodesCollection<GroupListConstants> Children { get { return this.ListConstantGroups; } }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            GroupListConstants node = null!;
            if (node_impl == null)
            {
                node = new GroupListConstants(this);
            }
            else
            {
                node = (GroupListConstants)node_impl;
            }
            this.ListConstantGroups.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.ConstantsGroupName, node, this.ListConstantGroups);
            }
            var model = this.ParentModel;
            node.ShortId = model.LastConstantGroupShortId + 1;
            model.LastConstantGroupShortId = node.ShortId;
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        //[Browsable(false)]
        //new public string IconName { get { return "iconFolder"; } }
        //protected override string GetNodeIconName() { return "iconFolder"; }
        partial void OnCreated()
        {
            this._Name = Defaults.GroupConstantGroupsName;
            this.PrefixForDbTables = "Cnst";
            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this.ListConstantGroups.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListConstantGroups.OnAddedAction = (t) =>
            {
                t.OnAdded();
                t.InitRoles();
            };
            this.ListConstantGroups.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListConstantGroups.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
        }
        public GroupListConstants AddGroupConstants(string name)
        {
            var node = new GroupListConstants(this) { Name = name };
            this.GetUniqueName(Defaults.ConstantsGroupName, node, this.ListConstantGroups);
            this.NodeAddNewSubNode(node);
            return node;
        }
        public IReadOnlyList<IGroupListConstants> GetIncludedConstantGroups(string guidAppPrjGen)
        {
            var res = new List<IGroupListConstants>();
            foreach (var t in this.ListConstantGroups)
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
            var lst = new List<string>();
            lst.Add(this.GetPropertyName(() => this.Parent));
            lst.Add(this.GetPropertyName(() => this.Children));
            return lst.ToArray();
        }


        #region Roles
        public object GetRoleAccess(IRole role)
        {
            Debug.Assert(role != null);
            Debug.Assert(dicConstantAccess.ContainsKey(role.Guid));
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
        internal Dictionary<string, RoleConstantAccess> dicConstantAccess = new Dictionary<string, RoleConstantAccess>();
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
        public EnumConstantAccess GetRoleConstantAccess(string roleGuid)
        {
            if (this.dicConstantAccess.TryGetValue(roleGuid, out var r) && r.EditAccess != EnumConstantAccess.CN_BY_PARENT)
                return r.EditAccess;
            return EnumConstantAccess.CN_EDIT;
            //return this.ParentModel.GetRoleConstantAccess(roleGuid);
        }
        public EnumPrintAccess GetRoleConstantPrint(string roleGuid)
        {
            if (this.dicConstantAccess.TryGetValue(roleGuid, out var r) && r.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                return r.PrintAccess;
            return EnumPrintAccess.PR_PRINT;
            //return this.ParentModel.GetRoleConstantPrint(roleGuid);
        }
        #endregion Roles
    }
}
