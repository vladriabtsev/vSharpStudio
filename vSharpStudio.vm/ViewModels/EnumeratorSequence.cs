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
    public partial class EnumeratorSequence : ICanGoLeft, ICanAddNode, INodeGenSettings, IEditableNode
    {
        public string Text { get { return this.Name; } }
        public string Value { get { return this.Guid; } }
        [Browsable(false)]
        public GroupListEnumeratorSequences ParentGroupListSequences { get { Debug.Assert(this.Parent != null); return (GroupListEnumeratorSequences)this.Parent; } }
        [Browsable(false)]
        public IGroupListEnumeratorSequences ParentGroupListSequencesI { get { Debug.Assert(this.Parent != null); return (IGroupListEnumeratorSequences)this.Parent; } }
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
                if (this.ParentGroupListSequences.ListEnumeratorSequences.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (EnumeratorSequence?)this.ParentGroupListSequences.ListEnumeratorSequences.GetPrev(this);
            if (prev == null)
                return;
            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupListSequences.ListEnumeratorSequences.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListSequences.ListEnumeratorSequences.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (EnumeratorSequence?)this.ParentGroupListSequences.ListEnumeratorSequences.GetNext(this);
            if (next == null)
                return;
            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupListSequences.ListEnumeratorSequences.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            Debug.Assert(this.Parent != null);
            var node = EnumeratorSequence.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupListSequences.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new EnumeratorSequence(this.Parent);
            this.ParentGroupListSequences.Add(node);
            this.GetUniqueName(EnumeratorSequence.DefaultName, node, this.ParentGroupListSequences.ListEnumeratorSequences);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListSequences.ListEnumeratorSequences.Remove(this);
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
