using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public partial class EnumerationPair : ConfigObjectBase<EnumerationPair, EnumerationPair.EnumerationPairValidator>, IComparable<EnumerationPair>
    {
        partial void OnInit()
        {
        }
        public void OnInitFromDto()
        {
        }

        #region ITreeNode
        //        public string NodeText { get { return this.Name; } }
        protected override bool OnNodeCanRight()
        {
            return false;
        }

        #endregion ITreeNode
    }
}
