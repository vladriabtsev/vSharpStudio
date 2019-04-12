using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Constant : ConfigObjectBase<Constant, Constant.ConstantValidator>, ITreeConfigNode, IComparable<Constant>
    {
        partial void OnInit()
        {
        }
        public void OnInitFromDto()
        {
            //RecreateSubNodes();
        }

        public int CompareTo(Constant other) { return this.SortingValue.CompareTo(other.SortingValue); }

        #region IConfigObject
        public void Create()
        {
            Constants vm = (Constants)this.Parent;
            int icurr = vm.ListConstants.IndexOf(this);
            vm.ListConstants.Add(new Constant(this.Parent));
        }
        #endregion IConfigObject

        #region ITreeNode
        public ITreeConfigNode Parent { get; internal set; }

        public IEnumerable<ITreeConfigNode> SubNodes => null; // this._SubNodes;
        //private IEnumerable<ITreeNode> _SubNodes;
        //partial void OnPropertiesChanged()
        //{
        //    _SubNodes = new ITreeNode[] { this.Properties };
        //}
        public string NodeText { get { return this.Name; } }

        #endregion ITreeNode
    }
}
