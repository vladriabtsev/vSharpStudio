﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} configs:{ListBaseConfigLinks.Count,nq} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class GroupListBaseConfigLinks : ITreeModel, ICanAddSubNode, ICanGoRight, IEditableNodeGroup // , INodeGenSettings
    {
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public Config ParentConfig { get { Debug.Assert(this.Parent != null); return (Config)this.Parent; } }
        [Browsable(false)]
        public IConfig ParentConfigI { get { Debug.Assert(this.Parent != null); return (IConfig)this.Parent; } }
        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentConfig.Children;
        }
        #endregion ITree

        public new ConfigNodesCollection<BaseConfigLink> Children { get { return this.ListBaseConfigLinks; } }

        // [BrowsableAttribute(false)]
        // public SortedObservableCollection<ITreeConfigNode> Children { get; private set; }
        partial void OnCreated()
        {
            this._Name = Defaults.BaseConfigLinksName;
            this.IsEditable = false;
            // this.Children = new SortedObservableCollection<ITreeConfigNode>();
            // this.GroupSharedProperties.Parent = this;
            // Children.Add(this.GroupSharedProperties, 7);
            // this.GroupListDocuments.Parent = this;
            // Children.Add(this.GroupListDocuments, 8);
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            //this.AddAllAppGenSettingsVmsToNode();
            this.ListBaseConfigLinks.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            //this.ListBaseConfigLinks.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            this.ListBaseConfigLinks.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListBaseConfigLinks.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
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

        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            BaseConfigLink node = null!;
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
                this.GetUniqueName(Defaults.BaseConfigName, node, this.ListBaseConfigLinks);
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
                //lst.Add(this.GetPropertyName(() => this.NameUi));
                this.GetPropertyName(() => this.Parent),
                this.GetPropertyName(() => this.Children)
            };
            return lst.ToArray();
        }
    }
}
