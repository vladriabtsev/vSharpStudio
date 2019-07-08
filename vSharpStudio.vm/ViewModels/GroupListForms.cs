﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListForms.Count,nq}")]
    public partial class GroupListForms : ICanAddSubNode, ICanGoRight, ICanGoLeft
    {
        partial void OnInit()
        {
            this.Name = "Forms";
            this.IsEditable = false;
        }

        #region Tree operations
        public void AddForm(Form node)
        {
            this.NodeAddNewSubNode(node);
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Form node = null;
            if (node_impl == null)
                node = new Form();
            else
                node = (Form)node_impl;
            this.Add(node);
            if (node_impl == null)
                GetUniqueName(Form.DefaultName, node, this.ListForms);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
