using System;
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
    [DebuggerDisplay("Group:{Name,nq} configs:{ListBaseConfigLinks.Count,nq}")]
    public partial class GroupListBaseConfigLinks : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, INewAndDeleteion
    {
        public ConfigNodesCollection<BaseConfigLink> Children { get { return this.ListBaseConfigLinks; } }
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
            this.ListBaseConfigLinks.OnAddingAction = (t) => {
                t.IsNew = true;
            };
            this.ListBaseConfigLinks.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListBaseConfigLinks.OnRemovedAction = (t) => {
                this.GetIsHasMarkedForDeletion();
                this.GetIsHasNew();
            };
            this.ListBaseConfigLinks.OnClearedAction = () => {
                this.GetIsHasMarkedForDeletion();
                this.GetIsHasNew();
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
        public bool IsNew { get { return false; } set { } }
        public bool IsMarkedForDeletion { get { return false; } set { } }
        public bool GetIsHasMarkedForDeletion()
        {
            foreach (var t in this.ListBaseConfigLinks)
            {
                if (t.IsMarkedForDeletion || t.GetIsHasMarkedForDeletion())
                {
                    this.IsHasMarkedForDeletion = true;
                    return true;
                }
            }
            this.IsHasMarkedForDeletion = false;
            return false;
        }
        public bool GetIsHasNew()
        {
            foreach (var t in this.ListBaseConfigLinks)
            {
                if (t.IsNew || t.GetIsHasNew())
                {
                    this.IsHasNew = true;
                    return true;
                }
            }
            this.IsHasNew = false;
            return false;
        }
        public void RemoveMarkedForDeletionIfNewObjects()
        {
            var tlst = this.ListBaseConfigLinks.ToList();
            foreach (var t in tlst)
            {
                if (t.IsMarkedForDeletion && t.IsNew)
                {
                    this.ListBaseConfigLinks.Remove(t);
                    continue;
                }
                //t.RemoveMarkedForDeletionIfNewObjects();
            }
        }
        public void RemoveMarkedForDeletionAndNewFlags()
        {
            foreach (var t in this.ListBaseConfigLinks)
            {
                //t.RemoveMarkedForDeletionAndNewFlags();
                t.IsNew = false;
                t.IsMarkedForDeletion = false;
            }
            Debug.Assert(!this.IsHasMarkedForDeletion);
            Debug.Assert(!this.IsHasNew);
        }
        #endregion Tree operations

    }
}
