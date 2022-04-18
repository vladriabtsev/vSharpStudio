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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListDetails.Count,nq}")]
    public partial class GroupListDetails : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNodeGroup
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
            else if (this.Parent is CatalogFolder)
            {
                var p = this.Parent as CatalogFolder;
                return p.Children;
            }
            else if (this.Parent is Document)
            {
                var p = this.Parent as Document;
                return p.Children;
            }
            else if (this.Parent is Detail)
            {
                var p = this.Parent as Detail;
                return p.Children;
            }
            throw new NotImplementedException();
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ConfigNodesCollection<Detail> Children { get { return this.ListDetails; } }
        partial void OnInit()
        {
            this._Name = "Tabs";
            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this.ListDetails.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListDetails.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListDetails.OnRemovedAction = (t) => {
                this.OnRemoveChild();
            };
            this.ListDetails.OnClearedAction = () => {
                this.OnRemoveChild();
            };
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public Detail AddPropertiesTab(string name)
        {
            var node = new Detail(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Detail node = null;
            if (node_impl == null)
            {
                node = new Detail(this);
            }
            else
            {
                node = (Detail)node_impl;
            }

            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Detail.DefaultName, node, this.ListDetails);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        public Detail AddTab(string name)
        {
            var node = new Detail(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }
    }
}
