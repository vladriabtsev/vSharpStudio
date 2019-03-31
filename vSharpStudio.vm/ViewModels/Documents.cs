using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Documents : EntityObjectBaseWithGuid<Documents, Documents.DocumentsValidator>, IEntityObject, ITreeNode
    {
        public void OnInitFromDto()
        {
        }
        #region ITreeNode
        public ITreeNode Parent { get; internal set; }
        public IEnumerable<ITreeNode> SubNodes => this.ListDocuments;

        #endregion ITreeNode
    }
}
