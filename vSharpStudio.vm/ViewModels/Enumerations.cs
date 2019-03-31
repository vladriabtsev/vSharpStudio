using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Enumerations : EntityObjectBaseWithGuid<Enumerations, Enumerations.EnumerationsValidator>, IEntityObject, ITreeNode
    {
        public void OnInitFromDto()
        {
        }

        #region ITreeNode
        public ITreeNode Parent { get; internal set; }
        public IEnumerable<ITreeNode> SubNodes => this.ListEnumerations;

        #endregion ITreeNode
    }
}
