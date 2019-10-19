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
    public partial class Report : ICanGoLeft, ICanAddNode
    {
        public static readonly string DefaultName = "Report";

        partial void OnInit()
        {
            this.IsIncludableInModels = true;
        }

        [PropertyOrderAttribute(11)]
        [Editor(typeof(EditorObjectModels), typeof(EditorObjectModels))]
        public bool Models { get; set; }
        #region Tree operations
        public override bool NodeCanUp()
        {
            if (NodeCanAddClone())
            {
                if ((this.Parent as GroupListReports).ListReports.CanUp(this))
                    return true;
            }
            return false;
        }
        public override void NodeUp()
        {
            var prev = (Report)(this.Parent as GroupListReports).ListReports.GetPrev(this);
            if (prev == null)
                return;
            SetSelected(prev);
        }
        public override void NodeMoveUp()
        {
            (this.Parent as GroupListReports).ListReports.MoveUp(this);
            SetSelected(this);
        }
        public override bool NodeCanDown()
        {
            if (NodeCanAddClone())
            {
                if ((this.Parent as GroupListReports).ListReports.CanDown(this))
                    return true;
            }
            return false;
        }
        public override void NodeDown()
        {
            var next = (Report)(this.Parent as GroupListReports).ListReports.GetNext(this);
            if (next == null)
                return;
            SetSelected(next);
        }
        public override void NodeMoveDown()
        {
            (this.Parent as GroupListReports).ListReports.MoveDown(this);
            SetSelected(this);
        }
        public override void NodeRemove()
        {
            (this.Parent as GroupListReports).Remove(this);
            this.Parent = null;
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = Report.Clone(this.Parent, this, true, true);
            (this.Parent as GroupListReports).Add(node);
            this.Name = this.Name + "2";
            SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Report(this.Parent);
            (this.Parent as GroupListReports).Add(node);
            GetUniqueName(Report.DefaultName, node, (this.Parent as GroupListReports).ListReports);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
