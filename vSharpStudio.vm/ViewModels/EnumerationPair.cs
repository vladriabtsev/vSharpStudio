using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public partial class EnumerationPair : ConfigObjectBase<EnumerationPair, EnumerationPair.EnumerationPairValidator>, ITreeConfigNode, IComparable<EnumerationPair>
    {
        public void OnInitFromDto()
        {
        }
        public int CompareTo(EnumerationPair other) { return this.SortingValue.CompareTo(other.SortingValue); }

        #region ITreeNode
        public ITreeConfigNode Parent { get; internal set; }

        public IEnumerable<ITreeConfigNode> SubNodes => this._SubNodes;
        private IEnumerable<ITreeConfigNode> _SubNodes = new ITreeConfigNode[] { };
        public string NodeText { get { return this.Name; } }

        #endregion ITreeNode
    }
}
