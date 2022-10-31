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
        [BrowsableAttribute(false)]
        public GroupListReports ParentGroupListReports { get { return (GroupListReports)this.Parent; } }
        [BrowsableAttribute(false)]
        public IGroupListReports ParentGroupListReportsI { get { return (IGroupListReports)this.Parent; } }
        public static readonly string DefaultName = "Report";

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            return this.ParentGroupListReports.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ConfigNodesCollection<ITreeConfigNodeSortable> Children { get; private set; }

        [Browsable(false)]
        new public string IconName { get { return "iconReport"; } }
        //protected override string GetNodeIconName() { return "iconReport"; }
        partial void OnCreated()
        {
            this.Children = new ConfigNodesCollection<ITreeConfigNodeSortable>(this);
            this.IsIncludableInModels = true;
        }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListReports.ListReports.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Report)this.ParentGroupListReports.ListReports.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupListReports.ListReports.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListReports.ListReports.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Report)this.ParentGroupListReports.ListReports.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupListReports.ListReports.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = Report.Clone(this.Parent, this, true, true);
            this.ParentGroupListReports.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Report(this.Parent);
            this.ParentGroupListReports.Add(node);
            this.GetUniqueName(Report.DefaultName, node, this.ParentGroupListReports.ListReports);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListReports.ListReports.Remove(this);
        }
        #endregion Tree operations
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            lst.Add(this.GetPropertyName(() => this.Parent));
            lst.Add(this.GetPropertyName(() => this.Children));
            return lst.ToArray();
        }
    }
}
