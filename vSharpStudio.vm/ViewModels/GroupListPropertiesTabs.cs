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
    public partial class GroupListPropertiesTabs : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, INewAndDeleteion
    {
        public ConfigNodesCollection<PropertiesTab> Children { get { return this.ListPropertiesTabs; } }
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.ListPropertiesTabs;
        }
        public override bool HasChildren(object parent)
        {
            return this.ListPropertiesTabs.Count > 0;
        }
        partial void OnInit()
        {
            this.Name = "Tabs";
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
                this.GetIsHasMarkedForDeletion();
                this.GetIsHasNew();
            };
            this.ListPropertiesTabs.OnClearedAction = () => {
                this.GetIsHasMarkedForDeletion();
                this.GetIsHasNew();
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
        partial void OnIsMarkedForDeletionChanged()
        {
            if (this.IsNotNotifying)
                return;
            if (this.IsMarkedForDeletion)
            {
                (this.Parent as INewAndDeleteion).IsHasMarkedForDeletion = true;
            }
            else
            {
                var p = (this.Parent as INewAndDeleteion);
                p.GetIsHasMarkedForDeletion();
            }
        }
        partial void OnIsNewChanged()
        {
            if (this.IsNotNotifying)
                return;
            if (this.IsNew)
            {
                (this.Parent as INewAndDeleteion).IsHasNew = true;
            }
            else
            {
                var p = (this.Parent as INewAndDeleteion);
                p.GetIsHasNew();
            }
        }
        partial void OnIsHasMarkedForDeletionChanged()
        {
            if (this.IsNotNotifying)
                return;
            if (this.IsHasMarkedForDeletion)
            {
                (this.Parent as INewAndDeleteion).IsHasMarkedForDeletion = true;
            }
            else
            {
                var p = (this.Parent as INewAndDeleteion);
                p.GetIsHasMarkedForDeletion();
            }
        }
        partial void OnIsHasNewChanged()
        {
            if (this.IsNotNotifying)
                return;
            if (this.IsHasNew)
            {
                (this.Parent as INewAndDeleteion).IsHasNew = true;
            }
            else
            {
                var p = (this.Parent as INewAndDeleteion);
                p.GetIsHasNew();
            }
        }
        public bool GetIsHasMarkedForDeletion()
        {
            foreach (var t in this.ListPropertiesTabs)
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
            foreach (var t in this.ListPropertiesTabs)
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
        #endregion Tree operations

        public PropertiesTab AddTab(string name)
        {
            var node = new PropertiesTab(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public void RemoveMarkedForDeletionIfNewObjects()
        {
            var tlst = this.ListPropertiesTabs.ToList();
            foreach (var t in tlst)
            {
                t.GroupProperties.RemoveMarkedForDeletionIfNewObjects();
                if (t.IsMarkedForDeletion && t.IsNew)
                {
                    this.ListPropertiesTabs.Remove(t);
                    continue;
                }
                t.RemoveMarkedForDeletionIfNewObjects();
            }
        }
        public void RemoveMarkedForDeletionAndNewFlags()
        {
            foreach (var t in this.ListPropertiesTabs)
            {
                t.GroupProperties.RemoveMarkedForDeletionAndNewFlags();
                t.RemoveMarkedForDeletionAndNewFlags();
                t.IsNew = false;
                t.IsMarkedForDeletion = false;
            }
            Debug.Assert(!this.IsHasMarkedForDeletion);
            Debug.Assert(!this.IsHasNew);
        }
    }
}
