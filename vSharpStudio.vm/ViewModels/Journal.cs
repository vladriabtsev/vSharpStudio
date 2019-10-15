using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Journal : ICanAddNode, ICanAddSubNode, ICanGoRight, ICanGoLeft
    {
        public static readonly string DefaultName = "Journal";

        [PropertyOrderAttribute(11)]
        [Editor(typeof(EditorObjectModels), typeof(EditorObjectModels))]
        public bool Models { get; set; }
        #region Tree operations
        public override bool NodeCanUp()
        {
            if (NodeCanAddClone())
            {
                if ((this.Parent as GroupListJournals).ListJournals.CanUp(this))
                    return true;
            }
            return false;
        }
        public override void NodeUp()
        {
            var prev = (Journal)(this.Parent as GroupListJournals).ListJournals.GetPrev(this);
            if (prev == null)
                return;
            SetSelected(prev);
        }
        public override void NodeMoveUp()
        {
            (this.Parent as GroupListJournals).ListJournals.MoveUp(this);
            SetSelected(this);
        }
        public override bool NodeCanDown()
        {
            if (NodeCanAddClone())
            {
                if ((this.Parent as GroupListJournals).ListJournals.CanDown(this))
                    return true;
            }
            return false;
        }
        public override void NodeDown()
        {
            var next = (Journal)(this.Parent as GroupListJournals).ListJournals.GetNext(this);
            if (next == null)
                return;
            SetSelected(next);
        }
        public override void NodeMoveDown()
        {
            (this.Parent as GroupListJournals).ListJournals.MoveDown(this);
            SetSelected(this);
        }
        public override void NodeRemove()
        {
            (this.Parent as GroupListJournals).Remove(this);
            this.Parent = null;
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = Journal.Clone(this.Parent, this, true, true);
            (this.Parent as GroupListJournals).Add(node);
            this.Name = this.Name + "2";
            SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Journal(this.Parent);
            (this.Parent as GroupListJournals).Add(node);
            GetUniqueName(Journal.DefaultName, node, (this.Parent as GroupListJournals).ListJournals);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
