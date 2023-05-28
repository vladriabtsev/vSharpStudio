using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListDocuments.Count,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class GroupListSequences : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup
    {
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public GroupListCommon ParentGroupListCommon { get { Debug.Assert(this.Parent != null); return (GroupListCommon)this.Parent; } }
        [Browsable(false)]
        public IGroupListCommon ParentGroupListCommonI { get { Debug.Assert(this.Parent != null); return (IGroupListCommon)this.Parent; } }
        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListCommon.Children;
        }
        #endregion ITree

        public new ConfigNodesCollection<CodeSequence> Children { get { return this.ListSequences; } }
        public IGroupListCommon IParent { get { return this.ParentGroupListCommonI; } }

        partial void OnCreated()
        {
            this._Name = "Sequences";
            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this.ListSequences.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListSequences.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListSequences.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListSequences.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
        }
        public Document AddDocument(string name)
        {
            var node = new Document(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public int IndexOf(ICodeSequence docSequence)
        {
            return this.ListSequences.IndexOf((docSequence as CodeSequence)!);
        }
        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            CodeSequence node = null!;
            if (node_impl == null)
            {
                node = new CodeSequence(this);
            }
            else
            {
                node = (CodeSequence)node_impl;
            }
            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.SequenceName, node, this.ListSequences);
            }
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                this.GetPropertyName(() => this.Description),
                this.GetPropertyName(() => this.Guid),
                this.GetPropertyName(() => this.NameUi),
                this.GetPropertyName(() => this.Parent),
                this.GetPropertyName(() => this.Children)
            };
            return lst.ToArray();
        }
    }
}
