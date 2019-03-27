using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Enumeration : EntityObjectBaseWithGuid<Enumeration, Enumeration.EnumerationValidator>, IEntityObject, ITreeNode
    {
        partial void OnInit()
        {
        }
        public void OnInitFromDto()
        {
        }
        #region ITreeNode
        public ITreeNode Parent => throw new NotImplementedException();

        public IEnumerable<ITreeNode> SubNodes => this.ListValues;
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
