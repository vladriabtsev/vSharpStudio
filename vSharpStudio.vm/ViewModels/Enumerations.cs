using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Enumerations : EntityObjectBaseWithGuid<Enumerations, Enumerations.EnumerationsValidator>, IEntityObject, ITreeNode
    {
        partial void OnInit()
        {
            this.Name = "Enumerations";
        }
        public void OnInitFromDto()
        {
        }

        #region ITreeNode
        public ITreeNode Parent { get; internal set; }
        public IEnumerable<ITreeNode> SubNodes => this.ListEnumerations;
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
        public string NodeText { get { return this.Name + " " + this.ListEnumerations.Count; } }

        #endregion ITreeNode
    }
}
