﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} configs:{ListBaseConfigLinks.Count,nq}")]
    public partial class GroupListBaseConfigLinks : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings
    {
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.ListBaseConfigLinks;
        }

        public override bool HasChildren(object parent)
        {
            return this.ListBaseConfigLinks.Count > 0;
        }

        // [BrowsableAttribute(false)]
        // public SortedObservableCollection<ITreeConfigNode> Children { get; private set; }
        partial void OnInit()
        {
            this.Name = "BaseConfigs";
            this.IsEditable = false;
            // this.Children = new SortedObservableCollection<ITreeConfigNode>();
            // this.GroupSharedProperties.Parent = this;
            // Children.Add(this.GroupSharedProperties, 7);
            // this.GroupListDocuments.Parent = this;
            // Children.Add(this.GroupListDocuments, 8);
            this.ListBaseConfigLinks.OnAddedAction = (t) =>
            {
                t.AddAllAppGenSettingsVmsToNode();
            };
        }

        #region Tree operations
        public void AddBaseConfig(BaseConfigLink node)
        {
            this.NodeAddNewSubNode(node);
        }
        public BaseConfigLink AddBaseConfig(string name, string baseConfigPath)
        {
            BaseConfigLink node = new BaseConfigLink(this)
            {
                RelativeConfigFilePath = this.GetRelativeToConfigDiskPath(baseConfigPath)
            };
            this.NodeAddNewSubNode(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            BaseConfigLink node = null;
            if (node_impl == null)
            {
                node = new BaseConfigLink(this);
            }
            else
            {
                node = (BaseConfigLink)node_impl;
            }

            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(BaseConfigLink.DefaultName, node, this.ListBaseConfigLinks);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        [DisplayName("Generators")]
        [Description("Expandable Attached Node Settings for App Project Generators")]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        [PropertyOrderAttribute(int.MaxValue)]
        public object GeneratorNodeSettings
        {
            get
            {
                if (!(this is INodeGenSettings))
                    return null;
                var res = SettingsTypeBuilder.CreateNewObject(this.ListNodeGeneratorsSettings);
                return res;
            }
        }
    }
}
