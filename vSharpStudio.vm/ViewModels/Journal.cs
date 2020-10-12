using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Journal : ICanAddNode, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, INewAndDeleteion
    {
        public static readonly string DefaultName = "Journal";
        [Browsable(false)]
        new public string IconName { get { return "iconCatalogProperty"; } }
        //protected override string GetNodeIconName() { return "iconCatalogProperty"; }
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
        public bool CanAddSubNode() { return true; }
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListJournals).ListJournals.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }
        public override void NodeUp()
        {
            var prev = (Journal)(this.Parent as GroupListJournals).ListJournals.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }
        public override void NodeMoveUp()
        {
            (this.Parent as GroupListJournals).ListJournals.MoveUp(this);
            this.SetSelected(this);
        }
        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListJournals).ListJournals.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }
        public override void NodeDown()
        {
            var next = (Journal)(this.Parent as GroupListJournals).ListJournals.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }
        public override void NodeMoveDown()
        {
            (this.Parent as GroupListJournals).ListJournals.MoveDown(this);
            this.SetSelected(this);
        }
        public override void NodeRemove(bool ask = true)
        {
            (this.Parent as GroupListJournals).Remove(this);
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
                var p = (this.Parent as GroupListJournals);
                bool isMarked = false;
                foreach (var t in p.ListJournals)
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
                var p = (this.Parent as GroupListJournals);
                bool isNew = false;
                foreach (var t in p.ListJournals)
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
            var node = Journal.Clone(this.Parent, this, true, true);
            (this.Parent as GroupListJournals).Add(node);
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Journal(this.Parent);
            (this.Parent as GroupListJournals).Add(node);
            this.GetUniqueName(Journal.DefaultName, node, (this.Parent as GroupListJournals).ListJournals);
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

    }
}
