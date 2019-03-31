using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Constants : EntityObjectBaseWithGuid<Constants, Constants.ConstantsValidator>, IEntityObject, ITreeNode
    {
        public void OnInitFromDto()
        {
        }
        #region ITreeNode
        public ITreeNode Parent { get; private set; }

        public IEnumerable<ITreeNode> SubNodes => this.ListConstants;

        #endregion ITreeNode
    }
}
