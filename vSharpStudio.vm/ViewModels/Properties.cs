using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Properties : EntityObjectBaseWithGuid<Properties, Properties.PropertiesValidator>, IEntityObject, ITreeNode
    {
        public void OnInitFromDto()
        {
        }

        #region ITreeNode
        public ITreeNode Parent { get; internal set; }
        public IEnumerable<ITreeNode> SubNodes => this.ListProperties;

        #endregion ITreeNode
    }
}
