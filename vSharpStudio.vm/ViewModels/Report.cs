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
    public partial class Report : ICanGoLeft, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup
    {
        public static readonly string DefaultName = "Report";

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as GroupListReports;
            return p.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }

        [Browsable(false)]
        new public string IconName { get { return "iconReport"; } }
        //protected override string GetNodeIconName() { return "iconReport"; }
        partial void OnCreated()
        {
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
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
        public override ITreeConfigNode NodeAddClone()
        {
            var node = Report.Clone(this.Parent, this, true, true);
            (this.Parent as GroupListReports).Add(node);
            this._Name = this._Name + "2";
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
        public void Remove()
        {
            var p = this.Parent as GroupListReports;
            p.ListReports.Remove(this);
        }
        #endregion Tree operations
    }
}
