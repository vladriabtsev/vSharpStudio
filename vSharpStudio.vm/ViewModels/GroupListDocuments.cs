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
        public override ITreeConfigNode NodeAddNewSubNode()
        {
            var node = new Document();
            this.Add(node);
            GetUniqueName(Document.DefaultName, node, this.ListDocuments);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
