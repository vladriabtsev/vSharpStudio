using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListPropertiesTabs.Count,nq}")]
    public partial class GroupListPropertiesTabs : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNodeGroup
    {
        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            if (this.Parent is Catalog)
            {
                var p = this.Parent as Catalog;
                return p.Children;
            }
            else if (this.Parent is Document)
            {
                var p = this.Parent as Document;
                return p.Children;
            }
            else if (this.Parent is PropertiesTab)
            {
                var p = this.Parent as PropertiesTab;
                return p.Children;
            }
            throw new NotImplementedException();
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        new public ConfigNodesCollection<PropertiesTab> Children { get { return this.ListPropertiesTabs; } }
        partial void OnInit()
        {
            this._Name = "Tabs";
            this.IsEditable = false;
            this.ListPropertiesTabs.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListPropertiesTabs.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListPropertiesTabs.OnRemovedAction = (t) => {
                this.OnRemoveChild();
            };
            this.ListPropertiesTabs.OnClearedAction = () => {
                this.OnRemoveChild();
            };
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public PropertiesTab AddPropertiesTab(string name)
        {
            var node = new PropertiesTab(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            PropertiesTab node = null;
            if (node_impl == null)
            {
                node = new PropertiesTab(this);
            }
            else
            {
                node = (PropertiesTab)node_impl;
            }

            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(PropertiesTab.DefaultName, node, this.ListPropertiesTabs);
            }

            this.SetSelected(node);
            return node;
        }
        public override void MarkForDeletion()
        {
            this.IsMarkedForDeletion = !this.IsMarkedForDeletion;
        }
        #endregion Tree operations

        public PropertiesTab AddTab(string name)
        {
            var node = new PropertiesTab(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }
    }
}
