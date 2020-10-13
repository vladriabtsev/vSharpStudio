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
    [DebuggerDisplay("Report:{Name,nq}")]
    public partial class Report : ICanGoLeft, ICanAddNode, INodeGenSettings, INewAndDeleteion
    {
        public static readonly string DefaultName = "Report";
        [Browsable(false)]
        new public string IconName { get { return "iconReport"; } }
        //protected override string GetNodeIconName() { return "iconReport"; }
        partial void OnInit()
        {
            this.IsIncludableInModels = true;
        }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListReports).ListReports.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Report)(this.Parent as GroupListReports).ListReports.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            (this.Parent as GroupListReports).ListReports.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListReports).ListReports.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Report)(this.Parent as GroupListReports).ListReports.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            (this.Parent as GroupListReports).ListReports.MoveDown(this);
            this.SetSelected(this);
        }

        public override void NodeRemove(bool ask = true)
        {
            (this.Parent as GroupListReports).Remove(this);
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
                (this.Parent as INewAndDeleteion).IsHasMarkedForDeletion = true;
            }
            else
            {
                var p = (this.Parent as INewAndDeleteion);
                p.GetIsHasMarkedForDeletion();
            }
        }
        partial void OnIsNewChanged()
        {
            if (this.IsNew)
            {
                (this.Parent as INewAndDeleteion).IsHasNew = true;
            }
            else
            {
                var p = (this.Parent as INewAndDeleteion);
                p.GetIsHasNew();
            }
        }
        partial void OnIsHasMarkedForDeletionChanged()
        {
            if (this.IsHasMarkedForDeletion)
            {
                (this.Parent as INewAndDeleteion).IsHasMarkedForDeletion = true;
            }
            else
            {
                var p = (this.Parent as INewAndDeleteion);
                p.GetIsHasMarkedForDeletion();
            }
        }
        partial void OnIsHasNewChanged()
        {
            if (this.IsHasNew)
            {
                (this.Parent as INewAndDeleteion).IsHasNew = true;
            }
            else
            {
                var p = (this.Parent as INewAndDeleteion);
                p.GetIsHasNew();
            }
        }
        public bool GetIsHasMarkedForDeletion()
        {
            //foreach (var t in this.ListDocuments)
            //{
            //    if (t.IsMarkedForDeletion || t.GetIsHasMarkedForDeletion())
            //    {
            //        this.IsHasMarkedForDeletion = true;
            //        return true;
            //    }
            //}
            this.IsHasMarkedForDeletion = false;
            return false;
        }
        public bool GetIsHasNew()
        {
            //foreach (var t in this.ListDocuments)
            //{
            //    if (t.IsHasNew || t.GetIsHasNew())
            //    {
            //        this.IsHasNew = true;
            //        return true;
            //    }
            //}
            this.IsHasNew = false;
            return false;
        }


        public override ITreeConfigNode NodeAddClone()
        {
            var node = Report.Clone(this.Parent, this, true, true);
            (this.Parent as GroupListReports).Add(node);
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Report(this.Parent);
            (this.Parent as GroupListReports).Add(node);
            this.GetUniqueName(Report.DefaultName, node, (this.Parent as GroupListReports).ListReports);
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

    }
}
