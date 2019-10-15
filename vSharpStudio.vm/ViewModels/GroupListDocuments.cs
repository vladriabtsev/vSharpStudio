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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListDocuments.Count,nq}")]
    public partial class GroupListDocuments : ITreeModel, ICanAddSubNode, ICanGoRight
    {
        public override IEnumerable<object> GetChildren(object parent) { return this.ListDocuments; }
        public override bool HasChildren(object parent) { return this.ListDocuments.Count > 0; }
        partial void OnInit()
        {
            this.Name = "Documents";
            this.IsEditable = false;
        }

        #region Tree operations
        public Document AddDocument(string name)
        {
            var node = new Document(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Document node = null;
            if (node_impl == null)
                node = new Document(this);
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
