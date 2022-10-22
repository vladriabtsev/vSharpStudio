using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Role:{Name,nq}")]
    public partial class Role : ICanGoLeft, ICanAddNode, INodeGenSettings, IEditableNode
    {
        public static readonly string DefaultName = "Role";

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as GroupListRoles;
            return p.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }

        [Browsable(false)]
        new public string IconName { get { return "iconWindowsForm"; } }
        //protected override string GetNodeIconName() { return "iconWindowsForm"; }
        partial void OnCreated()
        {
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
            this.IsIncludableInModels = true;
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
            //this.GroupProperties.AddAllAppGenSettingsVmsToNode();
            //this.GroupForms.AddAllAppGenSettingsVmsToNode();
            //this.GroupReports.AddAllAppGenSettingsVmsToNode();
        }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListForms).ListForms.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Form)(this.Parent as GroupListForms).ListForms.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            (this.Parent as GroupListForms).ListForms.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListForms).ListForms.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Form)(this.Parent as GroupListForms).ListForms.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            (this.Parent as GroupListForms).ListForms.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = Role.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            (this.Parent as GroupListRoles).Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Role(this.Parent);
            (this.Parent as GroupListRoles).Add(node);
            this.GetUniqueName(Role.DefaultName, node, (this.Parent as GroupListRoles).ListRoles);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            var p = this.Parent as GroupListRoles;
            p.ListRoles.Remove(this);
        }
        #endregion Tree operations
    }
}
