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
    public partial class GroupListEnumerations : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, INewAndDeleteion
    {
        public ConfigNodesCollection<Enumeration> Children { get { return this.ListEnumerations; } }
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
            this.ListEnumerations.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListEnumerations.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            //this.ListEnumerations.OnRemovedAction = (t) =>
            //{
            //    var cfg = this.GetConfig();
            //};
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
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
        public bool IsNew { get { return false; } set { } }
        public bool IsMarkedForDeletion { get { return false; } set { } }
        partial void OnIsHasMarkedForDeletionChanged()
        {
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
            foreach (var t in this.ListEnumerations)
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
            foreach (var t in this.ListEnumerations)
            {
                if (t.IsHasNew || t.GetIsHasNew())
                {
                    this.IsHasNew = true;
                    return true;
                }
            }
            this.IsHasNew = false;
            return false;
        }

        #endregion Tree operations
    }
}
