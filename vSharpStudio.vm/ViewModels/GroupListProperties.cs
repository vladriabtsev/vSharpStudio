﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListProperties.Count,nq}")]
    public partial class GroupListProperties : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft
    {
        public override IEnumerable<object> GetChildren(object parent) { return this.ListProperties; }
        public override bool HasChildren(object parent) { return this.ListProperties.Count > 0; }
        partial void OnInit()
        {
            if (this.Parent is GroupDocuments)
                this.Name = "Shared";
            else
                this.Name = "Properties";
            this.IsEditable = false;
        }

        protected override void OnParentChanged()
        {
            if (this.Parent is GroupDocuments)
                this.Name = "Shared";
            else
                this.Name = "Properties";
        }
        protected override void OnInitFromDto()
        {
            if (this.Parent is GroupDocuments)
                this.Name = "Shared";
            else
                this.Name = "Properties";
        }

        #region Tree operations
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

        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Property node = null;
            if (node_impl == null)
                node = new Property(this);
            else
                node = (Property)node_impl;
            this.Add(node);
            //TODO can be more economical?
            if (this.LastGenPosition == 0)
                this.LastGenPosition = 1;
            this.LastGenPosition++;
            node.Position = this.LastGenPosition;
            if (node_impl == null)
                GetUniqueName(Property.DefaultName, node, this.ListProperties);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
