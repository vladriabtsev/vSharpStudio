using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public partial class EnumerationPair : EntityObjectBaseWithGuid<EnumerationPair, EnumerationPair.EnumerationPairValidator>, IConfigObject, ITreeNode, IComparable<EnumerationPair>
    {
        public void OnInitFromDto()
        {
        }
        public int CompareTo(EnumerationPair other) { return this.SortingValue.CompareTo(other.SortingValue); }

        #region ITreeNode
        public ITreeNode Parent { get; internal set; }

        public IEnumerable<ITreeNode> SubNodes => this._SubNodes;
        private IEnumerable<ITreeNode> _SubNodes = new ITreeNode[] { };
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
        public string NodeText { get { return this.Name; } }

        #endregion ITreeNode
    }
}
