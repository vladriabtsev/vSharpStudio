using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public partial class EnumerationPair : ConfigObjectWithGuidBase<EnumerationPair, EnumerationPair.EnumerationPairValidator>, IConfigObject, ITreeConfigNode, IComparable<EnumerationPair>
    {
        public void OnInitFromDto()
        {
        }
        public int CompareTo(EnumerationPair other) { return this.SortingValue.CompareTo(other.SortingValue); }

        #region ITreeNode
        public ITreeConfigNode Parent { get; internal set; }

        public IEnumerable<ITreeConfigNode> SubNodes => this._SubNodes;
        private IEnumerable<ITreeConfigNode> _SubNodes = new ITreeConfigNode[] { };
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
