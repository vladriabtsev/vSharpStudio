using System;
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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListForms.Count,nq}")]
    public partial class GroupListForms : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft
    {
        public IEnumerable<object> GetChildren(object parent) { return this.ListForms; }
        public bool HasChildren(object parent) { return this.ListForms.Count > 0; }
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
