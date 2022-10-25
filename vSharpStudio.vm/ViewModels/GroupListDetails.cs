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
            if (this.Parent is Catalog c)
            {
                return c.Children;
            }
            else if (this.Parent is CatalogFolder cf)
            {
                return cf.Children;
            }
            else if (this.Parent is Document d)
            {
                return d.Children;
            }
            else if (this.Parent is Detail dt)
            {
                return dt.Children;
            }
            throw new NotImplementedException();
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ConfigNodesCollection<Detail> Children { get { return this.ListDetails; } }
        partial void OnCreated()
        {
            this._Name = "Details";
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
        public int IndexOf(IDetail det)
        {
            return this.ListDetails.IndexOf(det as Detail);
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
            var cfg = (Config)this.GetConfig();
            node.ShortId = cfg.Model.LastDetailShortId + 1;
            cfg.Model.LastDetailShortId = node.ShortId;
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
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            lst.Add(this.GetPropertyName(() => this.Description));
            lst.Add(this.GetPropertyName(() => this.Guid));
            lst.Add(this.GetPropertyName(() => this.NameUi));
            lst.Add(this.GetPropertyName(() => this.Parent));
            lst.Add(this.GetPropertyName(() => this.Children));
            return lst.ToArray();
        }
    }
}
