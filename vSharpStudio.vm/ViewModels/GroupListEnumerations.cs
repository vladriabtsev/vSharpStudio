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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListEnumerations.Count,nq}")]
    public partial class GroupListEnumerations : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings
    {
        [DisplayName("Generators")]
        [Description("Expandable Attached Node Settings for App Project Generators")]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        public object GenSettings { get; set; }
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.ListEnumerations;
        }
        public override bool HasChildren(object parent)
        {
            return this.ListEnumerations.Count > 0;
        }
        partial void OnInit()
        {
            this.Name = Defaults.EnumerationsGroupName;
            this.IsEditable = false;
            this.AddAllAppGenSettingsVmsToNewNode();
            this.ListEnumerations.CollectionChanged += ListEnumerations_CollectionChanged;
        }
        private void ListEnumerations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnAddRemoveNode(e);
        }

        #region Tree operations
        public Enumeration AddEnumeration(string name)
        {
            Enumeration node = new Enumeration(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }

        public Enumeration AddEnumeration(string name, EnumEnumerationType type)
        {
            Enumeration node = new Enumeration(this) { Name = name, DataTypeEnum = type };
            this.NodeAddNewSubNode(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Enumeration node = null;
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
                this.GetUniqueName(Enumeration.DefaultName, node, this.ListEnumerations);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
