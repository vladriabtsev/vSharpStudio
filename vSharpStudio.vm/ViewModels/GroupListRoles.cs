using System;
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
    public partial class GroupListRoles : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNodeGroup
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{ListRoles.Count}";
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public GroupListCommon ParentGroupListCommon { get { Debug.Assert(this.Parent != null); return (GroupListCommon)this.Parent; } }
        [Browsable(false)]
        public IGroupListCommon ParentGroupListCommonI { get { Debug.Assert(this.Parent != null); return (IGroupListCommon)this.Parent; } }
        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListCommon.Children;
        }
        #endregion ITree
        public new ConfigNodesCollection<Role> Children { get { return this.ListRoles; } }
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
            //VmBindable.IsNotifyingStatic = false;
            //var children = (ConfigNodesCollection<ITreeConfigNodeSortable>)this.Children;
            //children.Add(this.GroupRoles, 6);
            ////children.Add(this.GroupViewForms, 7);
            //VmBindable.IsNotifyingStatic = true;

            this.ListRoles.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListRoles.OnAddedAction = (t) =>
            {
                var nvb = new ModelVisitorBase();
                nvb.RunFromRoot(this.Cfg, null, null, null, (p, n) =>
                {
                    if (n is IRoleAccess ra)
                        ra.InitRoleAdd(t);
                });
            };
            this.ListRoles.OnRemovedAction = (t) =>
            {
                var nvb = new ModelVisitorBase();
                nvb.RunFromRoot(this.Cfg, null, null, null, (p, n) =>
                {
                    if (n is IRoleAccess ra)
                        ra.InitRoleRemove(t);
                });
            };
            this.ListRoles.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
            this._Name = Defaults.GroupRolesName;
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public void AddForm(Role node)
        {
            this.NodeAddNewSubNode(node);
        }

        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            Role node = null!;
            if (node_impl == null)
            {
                node = new Role(this);
            }
            else
            {
                node = (Role)node_impl;
            }

            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.RoleName, node, this.ListRoles);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                nameof(this.Parent),
                nameof(this.Children)
            };
            return lst.ToArray();
        }
        public Role AddRole(string name)
        {
            var node = new Role(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }
    }
}
