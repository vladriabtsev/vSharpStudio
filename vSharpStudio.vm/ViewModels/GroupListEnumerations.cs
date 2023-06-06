using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListEnumerations.Count,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class GroupListEnumerations : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup
    {
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public Model ParentModel { get { Debug.Assert(this.Parent != null); return (Model)this.Parent; } }
        [Browsable(false)]
        public IModel ParentModelI { get { Debug.Assert(this.Parent != null); return (IModel)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentModel.Children;
        }
        #endregion ITree

        public new ConfigNodesCollection<Enumeration> Children { get { return this.ListEnumerations; } }

        partial void OnCreated()
        {
            this._Name = Defaults.EnumerationsGroupName;
            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this.ListEnumerations.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListEnumerations.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListEnumerations.OnRemovedAction = (t) => {
                this.OnRemoveChild();
            };
            this.ListEnumerations.OnClearedAction = () => {
                this.OnRemoveChild();
            };
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public Enumeration AddEnumeration(string name, string? guid = null)
        {
            Enumeration node = new Enumeration(this) { Name = name };
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

        public Enumeration AddEnumeration(string name, EnumEnumerationType type, string? guid = null)
        {
            Enumeration node = new Enumeration(this) { Name = name, DataTypeEnum = type };
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

        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            Enumeration node = null!;
            if (node_impl == null)
            {
                node = new Enumeration(this);
            }
            else
            {
                node = (Enumeration)node_impl;
            }
            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.EnumerationName, node, this.ListEnumerations);
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
