using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Journal
    {
        public static readonly string DefaultName = "Journal";

        #region ITreeNode
        //public string NodeText { get { return this.Name; } }
        protected override bool OnNodeCanMoveUp()
        {
            return (this.Parent as GroupJournals).ListJournals.IndexOf(this) > 0;
        }
        protected override void OnNodeMoveUp()
        {
            var p = this.Parent as GroupJournals;
            var i = p.ListJournals.IndexOf(this);
            if (i > 0)
            {
                this.SortingValue = p.ListJournals[i - 1].SortingValue - 1;
            }
        }
        protected override bool OnNodeCanMoveDown()
        {
            return (this.Parent as GroupJournals).ListJournals.IndexOf(this) < ((this.Parent as GroupJournals).ListJournals.Count - 1);
        }
        protected override void OnNodeMoveDown()
        {
            var p = this.Parent as GroupJournals;
            var i = p.ListJournals.IndexOf(this);
            if (i < p.ListJournals.Count - 1)
            {
                this.SortingValue = p.ListJournals[i + 1].SortingValue + 1;
            }
        }
        protected override void OnNodeRemove()
        {
            (this.Parent as GroupJournals).ListJournals.Remove(this);
        }
        protected override ITreeConfigNode OnNodeAddNew()
        {
            var res = new Journal();
            res.Parent = this.Parent;
            (this.Parent as GroupJournals).ListJournals.Add(res);
            GetUniqueName(Enumeration.DefaultName, res, (this.Parent as GroupJournals).ListJournals);
            (this.Parent.Parent as Config).SelectedNode = res;
            return res;
        }
        protected override ITreeConfigNode OnNodeAddClone()
        {
            var res = Journal.Clone(this.Parent, this, true, true);
            res.Parent = this.Parent;
            (this.Parent as GroupJournals).ListJournals.Add(res);
            this.Name = this.Name + "2";
            (this.Parent.Parent as Config).SelectedNode = res;
            return res;
        }

        #endregion ITreeNode
    }
    
}
