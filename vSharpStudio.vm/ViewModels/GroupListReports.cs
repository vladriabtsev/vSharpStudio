using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListReports.Count,nq}")]
    public partial class GroupListReports : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings
    {
        [DisplayName("Generators")]
        [Description("Expandable Attached Node Settings for App Project Generators")]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        public object GenSettings { get; set; }
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.ListReports;
        }
        public override bool HasChildren(object parent)
        {
            return this.ListReports.Count > 0;
        }
        partial void OnInit()
        {
            this.Name = "Reports";
            this.IsEditable = false;
            this.AddAllAppGenSettingsVmsToNewNode();
            this.ListReports.CollectionChanged += ListReports_CollectionChanged;
        }
        private void ListReports_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnAddRemoveNode(e);
        }

        #region Tree operations
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
