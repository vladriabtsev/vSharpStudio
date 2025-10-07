using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class Report : ICanGoLeft, ICanAddNode, INodeGenSettings, IEditableNode, IEditableNodeGroup
    {
        [Browsable(false)]
        public GroupListReports ParentGroupListReports { get { Debug.Assert(this.Parent != null); return (GroupListReports)this.Parent; } }
        [Browsable(false)]
        public IGroupListReports ParentGroupListReportsI { get { Debug.Assert(this.Parent != null); return (IGroupListReports)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListReports.Children;
        }
        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconReport"; } }
        //protected override string GetNodeIconName() { return "iconReport"; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            //this.ListMainViewForms.OnAddingAction = (t) =>
            //{
            //    t.IsNew = true;
            //};
            //this.ListMainViewForms.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            //this.ListMainViewForms.OnRemovedAction = (t) =>
            //{
            //    this.OnRemoveChild();
            //};
            //this.ListMainViewForms.OnClearedAction = () =>
            //{
            //    this.OnRemoveChild();
            //};
        }
        protected override ConfigNodesCollection<Report>? GetParentCollection() { return this.ParentGroupListReports.ListReports; }

        #region Tree operations
        public override ITreeConfigNode NodeAddClone()
        {
            Debug.Assert(this.Parent != null);
            var node = Report.Clone(this.Parent, this, true, true);
            this.ParentGroupListReports.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Report(this.Parent);
            this.ParentGroupListReports.ListReports.Add(node, this);
            this.GetUniqueName(Defaults.ReportName, node, this.ParentGroupListReports.ListReports);
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
            var lst = new List<string>
            {
                nameof(this.Parent),
                nameof(this.Children)
            };
            return [.. lst];
        }
    }
}
