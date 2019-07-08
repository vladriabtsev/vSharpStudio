using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListConstants.Count,nq}")]
    public partial class GroupListConstants : ICanAddSubNode, ICanGoRight
    {
        partial void OnInit()
        {
            this.Name = "Constants";
            this.IsEditable = false;
        }

        #region Tree operations
        public void AddConstant(Constant node)
        {
            this.NodeAddNewSubNode(node);
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Constant node = null;
            if (node_impl == null)
                node = new Constant();
            else
                node = (Constant)node_impl;
            this.Add(node);
            if (node_impl == null)
                GetUniqueName(Constant.DefaultName, node, this.ListConstants);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
