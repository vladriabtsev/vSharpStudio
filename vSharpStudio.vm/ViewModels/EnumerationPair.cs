using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class EnumerationPair : EntityObjectBaseWithGuid<EnumerationPair, EnumerationPair.EnumerationPairValidator>, IEntityObject, ITreeNode
    {
        public void OnInitFromDto()
        {
        }
        #region ITreeNode
        public ITreeNode Parent => throw new NotImplementedException();

        public IEnumerable<ITreeNode> SubNodes => this._SubNodes;
        private IEnumerable<ITreeNode> _SubNodes = new ITreeNode[] { };
        //partial void OnPropertiesChanged()
        //{
        //    _SubNodes = new ObservableCollection<ITreeNode>() { this.Properties };
        //}

        #region ITreeNodeWithValidation
        public int ValidationQty
        {
            set
            {
                if (_ValidationQty != value)
                {
                    _ValidationQty = value;
                    NotifyPropertyChanged();
                }
            }
            get { return _ValidationQty; }
        }
        private int _ValidationQty;

        public Severity ValidationSeverity
        {
            set
            {
                if (_ValidationSeverity != value)
                {
                    _ValidationSeverity = value;
                    NotifyPropertyChanged();
                }
            }
            get { return _ValidationSeverity; }
        }

        private Severity _ValidationSeverity;
        #endregion ITreeNodeWithValidation
        #endregion ITreeNode
    }
}
