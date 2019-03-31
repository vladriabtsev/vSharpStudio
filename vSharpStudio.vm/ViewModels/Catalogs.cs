using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Catalogs : EntityObjectBaseWithGuid<Catalogs, Catalogs.CatalogsValidator>, IEntityObject, ITreeNode
    {
        public void OnInitFromDto()
        {
        }
        #region ITreeNode
        public ITreeNode Parent { get; internal set; }
        public IEnumerable<ITreeNode> SubNodes => this.ListCatalogs;

        #endregion ITreeNode
    }
}
