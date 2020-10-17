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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListProperties.Count,nq}")]
    public partial class GroupListProperties : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, INewAndDeleteion
    {
        public ConfigNodesCollection<Property> Children { get { return this.ListProperties; } }
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.ListProperties;
        }
        public override bool HasChildren(object parent)
        {
            return this.ListProperties.Count > 0;
        }
        partial void OnInit()
        {
            if (this.Parent is GroupDocuments)
            {
                this.Name = "Shared";
            }
            else
            {
                this.Name = "Properties";
            }

            this.IsEditable = false;
            this.ListProperties.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListProperties.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListProperties.OnRemovedAction = (t) => {
                this.GetIsHasMarkedForDeletion();
                this.GetIsHasNew();
            };
            this.ListProperties.OnClearedAction = () => {
                this.GetIsHasMarkedForDeletion();
                this.GetIsHasNew();
            };
        }

        protected override void OnParentChanged()
        {
            if (this.Parent is GroupDocuments)
            {
                this.Name = "Shared";
            }
            else
            {
                this.Name = "Properties";
            }
        }

        protected override void OnInitFromDto()
        {
            if (this.Parent is GroupDocuments)
            {
                this.Name = "Shared";
            }
            else
            {
                this.Name = "Properties";
            }
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public Property AddProperty(string name)
        {
            var node = new Property(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddProperty(string name, DataType type)
        {
            var node = new Property(this) { Name = name, DataType = type };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddProperty(string name, EnumDataType type, uint length, uint accuracy)
        {
            var node = new Property(this) { Name = name, DataType = new DataType() { DataTypeEnum = type, Length = length, Accuracy = accuracy } };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyString(string name, uint length)
        {
            var dt = new DataType() { DataTypeEnum = EnumDataType.STRING, Length = length };
            var node = new Property(this) { Name = name, DataType = dt };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyNumerical(string name, uint length, uint accuracy)
        {
            var dt = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL, Length = length, Accuracy = accuracy };
            var node = new Property(this) { Name = name, DataType = dt };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Property node = null;
            if (node_impl == null)
            {
                node = new Property(this);
            }
            else
            {
                node = (Property)node_impl;
            }

            this.Add(node);
            if (this.LastGenPosition == 0)
            {
                this.LastGenPosition = 1;
            }

            this.LastGenPosition++;
            node.Position = this.LastGenPosition;
            if (node_impl == null)
            {
                this.GetUniqueName(Property.DefaultName, node, this.ListProperties);
            }

            this.SetSelected(node);
            return node;
        }
        public bool IsNew { get { return false; } set { } }
        public bool IsMarkedForDeletion { get { return false; } set { } }
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
            foreach (var t in this.ListProperties)
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
            foreach (var t in this.ListProperties)
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
        public void RemoveMarkedForDeletionIfNewObjects()
        {
            var tlst = this.ListProperties.ToList();
            foreach (var t in tlst)
            {
                if (t.IsMarkedForDeletion && t.IsNew)
                {
                    this.ListProperties.Remove(t);
                    continue;
                }
                //t.RemoveMarkedForDeletionIfNewObjects();
            }
        }
        public void RemoveMarkedForDeletionAndNewFlags()
        {
            foreach (var t in this.ListProperties)
            {
                //t.RemoveMarkedForDeletionAndNewFlags();
                t.IsNew = false;
                t.IsMarkedForDeletion = false;
            }
            Debug.Assert(!this.IsHasMarkedForDeletion);
            Debug.Assert(!this.IsHasNew);
        }
    }
}
