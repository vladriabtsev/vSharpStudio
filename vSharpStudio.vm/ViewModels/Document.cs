using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Document : EntityObjectBaseWithGuid<Document, Document.DocumentValidator>, IEntityObject, ITreeNode
    {
        partial void OnInit()
        {
        }
        public void OnInitFromDto()
        {
            RecreateSubNodes();
        }
        #region ITreeNode
        public ITreeNode Parent { get; internal set; }
        public IEnumerable<ITreeNode> SubNodes
        {
            get { return this._SubNodes; }
            set
            {
                this._SubNodes = value;
                NotifyPropertyChanged();
            }
        }
        private IEnumerable<ITreeNode> _SubNodes;
        void RecreateSubNodes() { SubNodes = new ITreeNode[] { this.Properties }; }
        partial void OnPropertiesChanged() { RecreateSubNodes(); }

        #endregion ITreeNode
    }
}
