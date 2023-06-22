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
    public partial class GroupListEnumeratorSequences : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup
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

        public new ConfigNodesCollection<EnumeratorSequence> Children { get { return this.ListEnumeratorSequences; } }
        public IGroupListCommon IParent { get { return this.ParentGroupListCommonI; } }

        partial void OnCreated()
        {
            this._Name = "Code Sequences";
            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this.ListEnumeratorSequences.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListEnumeratorSequences.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListEnumeratorSequences.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListEnumeratorSequences.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
        }
        public EnumeratorSequence AddSequence(string name, string? guid = null)
        {
            var node = new EnumeratorSequence(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            this.NodeAddNewSubNode(node);
            return node;
        }
        public int IndexOf(IEnumeratorSequence docSequence)
        {
            return this.ListEnumeratorSequences.IndexOf((docSequence as EnumeratorSequence)!);
        }
        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            EnumeratorSequence node = null!;
            if (node_impl == null)
            {
                node = new EnumeratorSequence(this);
            }
            else
            {
                node = (EnumeratorSequence)node_impl;
            }
            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.SequenceName, node, this.ListEnumeratorSequences);
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
