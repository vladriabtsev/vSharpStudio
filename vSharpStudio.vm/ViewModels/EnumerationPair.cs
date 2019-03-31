using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class EnumerationPair : EntityObjectBaseWithGuid<EnumerationPair, EnumerationPair.EnumerationPairValidator>, IEntityObject, ITreeNode
    {
        public void OnInitFromDto()
        {
        }
        #region ITreeNode
        public ITreeNode Parent { get; internal set; }

        public IEnumerable<ITreeNode> SubNodes => this._SubNodes;
        private IEnumerable<ITreeNode> _SubNodes = new ITreeNode[] { };

        #endregion ITreeNode
    }
}
