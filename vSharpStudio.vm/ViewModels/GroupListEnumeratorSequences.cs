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
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class GroupListEnumeratorSequences : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{this.ListEnumeratorSequences.Count}";
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public GroupDocuments ParentGroupDocuments { get { Debug.Assert(this.Parent != null); return (GroupDocuments)this.Parent; } }
        [Browsable(false)]
        public IGroupDocuments ParentGroupDocumentsI { get { Debug.Assert(this.Parent != null); return (IGroupDocuments)this.Parent; } }
        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupDocuments.Children;
        }
        #endregion ITree

        public new ConfigNodesCollection<DocumentEnumeratorSequence> Children { get { return this.ListEnumeratorSequences; } }
        public IGroupDocuments IParent { get { return this.ParentGroupDocumentsI; } }

        partial void OnCreated()
        {
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
            this._Name = Defaults.GroupSequenceName;
        }
        public DocumentEnumeratorSequence AddSequence(string name, string? guid = null)
        {
            var node = new DocumentEnumeratorSequence(this) { Name = name };
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
        public int IndexOf(IDocumentEnumeratorSequence docSequence)
        {
            return this.ListEnumeratorSequences.IndexOf((docSequence as DocumentEnumeratorSequence)!);
        }
        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            DocumentEnumeratorSequence node = null!;
            if (node_impl == null)
            {
                node = new DocumentEnumeratorSequence(this);
            }
            else
            {
                node = (DocumentEnumeratorSequence)node_impl;
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
                nameof(this.Description),
                nameof(this.Guid),
                nameof(this.NameUi),
                nameof(this.Parent),
                nameof(this.Children)
            };
            return lst.ToArray();
        }
    }
}
