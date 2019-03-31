using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Constant : EntityObjectBaseWithGuid<Constant, Constant.ConstantValidator>, IEntityObject, ITreeNode
    {
        partial void OnInit()
        {
        }
        public void OnInitFromDto()
        {
            //RecreateSubNodes();
        }
        #region ITreeNode
        public ITreeNode Parent { get; internal set; }

        public IEnumerable<ITreeNode> SubNodes => null; // this._SubNodes;
        //private IEnumerable<ITreeNode> _SubNodes;
        //partial void OnPropertiesChanged()
        //{
        //    _SubNodes = new ITreeNode[] { this.Properties };
        //}

        #endregion ITreeNode
    }
}
