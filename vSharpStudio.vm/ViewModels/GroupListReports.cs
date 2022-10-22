using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentValidation;
using JetBrains.Annotations;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListReports.Count,nq}")]
    public partial class GroupListReports : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNodeGroup
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
            throw new NotImplementedException();
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ConfigNodesCollection<Report> Children { get { return this.ListReports; } }
        partial void OnCreated()
        {
            this._Name = "Reports";
            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this.ListReports.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListReports.OnAddedAction = (t) =>
            {
                t.AddAllAppGenSettingsVmsToNode();
            };
            this.ListReports.OnRemovedAction = (t) => {
                this.OnRemoveChild();
            };
            this.ListReports.OnClearedAction = () => {
                this.OnRemoveChild();
            };
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public void AddReport(Report node)
        {
            this.NodeAddNewSubNode(node);
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Report node = null;
            if (node_impl == null)
            {
                node = new Report(this);
            }
            else
            {
                node = (Report)node_impl;
            }

            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Report.DefaultName, node, this.ListReports);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
