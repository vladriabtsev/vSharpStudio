﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListEnumerations.Count,nq}")]
    public partial class GroupListEnumerations : ITreeModel, ICanAddSubNode, ICanGoRight
    {
        public IEnumerable<object> GetChildren(object parent) { return this.ListEnumerations; }
        public bool HasChildren(object parent) { return this.ListEnumerations.Count > 0; }
        partial void OnInit()
        {
            this.Name = Defaults.EnumerationsGroupName;
            this.IsEditable = false;
        }

        #region Tree operations
        public void AddEnumeration(Enumeration node)
        {
            this.NodeAddNewSubNode(node);
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Enumeration node = null;
            if (node_impl == null)
                node = new Enumeration();
            else
                node = (Enumeration)node_impl;
            this.Add(node);
            if (node_impl == null)
                GetUniqueName(Enumeration.DefaultName, node, this.ListEnumerations);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
