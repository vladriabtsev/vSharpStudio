using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Constants : EntityObjectBaseWithGuid<Constants, Constants.ConstantsValidator>, IEntityObject, ITreeNode
    {
        partial void OnInit()
        {
            this.Name = "Constants";
        }
        public void OnInitFromDto()
        {
        }
        #region ITreeNode
        public ITreeNode Parent { get; private set; }

        public IEnumerable<ITreeNode> SubNodes => this.ListConstants;
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
        public bool IsExpanded
        {
            get { return this._IsExpanded; }
            set
            {
                this._IsExpanded = value;
                NotifyPropertyChanged();
            }
        }
        private bool _IsExpanded;
        public string NodeText { get { return this.Name + " " + this.ListConstants.Count; } }

        #endregion ITreeNode
    }
}
