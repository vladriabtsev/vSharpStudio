using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Documents : EntityObjectBaseWithGuid<Documents, Documents.DocumentsValidator>, IEntityObject, ITreeNode
    {
        partial void OnInit()
        {
            this.Name = "Documents";
        }
        public void OnInitFromDto()
        {
        }
        #region ITreeNode
        public ITreeNode Parent { get; internal set; }
        public IEnumerable<ITreeNode> SubNodes => this.ListDocuments;
        public bool IsSelected
        {
            get { return this._IsSelected; }
            set
            {
                this._IsSelected = value;
                NotifyPropertyChanged();
            }
        }
        private bool _IsSelected;
        public bool IsExpended
        {
            get { return this._IsExpended; }
            set
            {
                this._IsExpended = value;
                NotifyPropertyChanged();
            }
        }
        private bool _IsExpended;

        #endregion ITreeNode
    }
}
