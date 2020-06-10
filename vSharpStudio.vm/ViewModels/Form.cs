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
    [DebuggerDisplay("Form:{Name,nq}")]
    public partial class Form : ICanGoLeft, ICanAddNode, INodeGenSettings
    {
        public static readonly string DefaultName = "Form";
        partial void OnInit()
        {
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

        public override void NodeRemove()
        {
            (this.Parent as GroupListForms).Remove(this);
            this.Parent = null;
        }

        public override ITreeConfigNode NodeAddClone()
        {
            var node = Form.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            (this.Parent as GroupListForms).Add(node);
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Form(this.Parent);
            (this.Parent as GroupListForms).Add(node);
            this.GetUniqueName(Form.DefaultName, node, (this.Parent as GroupListForms).ListForms);
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

    }
}
