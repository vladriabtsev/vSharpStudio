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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListDocuments.Count,nq}")]
    public partial class GroupListDocuments : ICanAddSubNode, ICanGoRight
    {
        partial void OnInit()
        {
            this.Name = "Documents";
            this.IsEditable = false;
        }

        #region Tree operations
        public void AddDocument(Document node)
        {
            this.NodeAddNewSubNode(node);
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Document node = null;
            if (node_impl == null)
                node = new Document();
            else
                node = (Document)node_impl;
            this.Add(node);
            if (node_impl == null)
                GetUniqueName(Document.DefaultName, node, this.ListDocuments);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
