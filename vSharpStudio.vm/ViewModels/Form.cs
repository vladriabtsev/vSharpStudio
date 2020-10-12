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
    public partial class Form : ICanGoLeft, ICanAddNode, INodeGenSettings, INewAndDeleteion
    {
        public static readonly string DefaultName = "Form";
        [Browsable(false)]
        new public string IconName { get { return "iconWindowsForm"; } }
        //protected override string GetNodeIconName() { return "iconWindowsForm"; }
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

        public override void NodeRemove(bool ask = true)
        {
            (this.Parent as GroupListForms).Remove(this);
            this.Parent = null;
        }
        public override void MarkForDeletion()
        {
            this.IsMarkedForDeletion = !this.IsMarkedForDeletion;
        }
        partial void OnIsMarkedForDeletionChanged()
        {
            if (this.IsMarkedForDeletion)
            {
                (this.Parent as INewAndDeleteion).IsMarkedForDeletion = true;
            }
            else
            {
                var p = (this.Parent as GroupListForms);
                bool isMarked = false;
                foreach (var t in p.ListForms)
                {
                    if (t.IsMarkedForDeletion)
                    {
                        isMarked = true;
                        break;
                    }
                }
                p.IsMarkedForDeletion = isMarked;
            }
        }
        partial void OnIsNewChanged()
        {
            if (this.IsNew)
            {
                (this.Parent as INewAndDeleteion).IsNew = true;
            }
            else
            {
                var p = (this.Parent as GroupListForms);
                bool isNew = false;
                foreach (var t in p.ListForms)
                {
                    if (t.IsNew)
                    {
                        isNew = true;
                        break;
                    }
                }
                p.IsNew = isNew;
            }
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
