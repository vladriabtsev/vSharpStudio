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
    [DebuggerDisplay("Sequence:{Name,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class DocNumberCodeSequence : ICanGoLeft, ICanAddNode, INodeGenSettings, IEditableNode
    {
        [Browsable(false)]
        public GroupDocNumberListSequences ParentGroupListSequences { get { Debug.Assert(this.Parent != null); return (GroupDocNumberListSequences)this.Parent; } }
        [Browsable(false)]
        public IGroupDocNumberListSequences ParentGroupListSequencesI { get { Debug.Assert(this.Parent != null); return (IGroupDocNumberListSequences)this.Parent; } }
        public static readonly string DefaultName = "Sequence";

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListSequences.Children;
        }
        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconLifeline"; } }
        //protected override string GetNodeIconName() { return "iconWindowsForm"; }
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
                if (this.ParentGroupListSequences.ListSequences.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (DocNumberCodeSequence?)this.ParentGroupListSequences.ListSequences.GetPrev(this);
            if (prev == null)
                return;
            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupListSequences.ListSequences.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListSequences.ListSequences.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (DocNumberCodeSequence?)this.ParentGroupListSequences.ListSequences.GetNext(this);
            if (next == null)
                return;
            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupListSequences.ListSequences.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            Debug.Assert(this.Parent != null);
            var node = DocNumberCodeSequence.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupListSequences.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new DocNumberCodeSequence(this.Parent);
            this.ParentGroupListSequences.Add(node);
            this.GetUniqueName(DocNumberCodeSequence.DefaultName, node, this.ParentGroupListSequences.ListSequences);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListSequences.ListSequences.Remove(this);
        }
        #endregion Tree operations
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                this.GetPropertyName(() => this.Parent),
                this.GetPropertyName(() => this.Children)
            };
            return lst.ToArray();
        }
    }
}
