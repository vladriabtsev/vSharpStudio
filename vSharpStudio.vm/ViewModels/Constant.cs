using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Constant:{Name,nq} Type:{DataType.GetTypeDesc(this.DataType),nq}")]
    public partial class Constant
    {
        public static readonly string DefaultName = "Constant";
        partial void OnInit()
        {
        }

        #region IConfigObject
        public void Create()
        {
            GroupConstants vm = (GroupConstants)this.Parent;
            int icurr = vm.ListConstants.IndexOf(this);
            vm.ListConstants.Add(new Constant() { Parent = this.Parent });
        }
        #endregion IConfigObject

        #region ITreeNode
        //        public string NodeText { get { return this.Name; } }
        protected override bool OnNodeCanRight()
        {
            return false;
        }
        protected override bool OnNodeCanMoveUp()
        {
            return (this.Parent as GroupConstants).ListConstants.IndexOf(this) > 0;
        }
        protected override void OnNodeMoveUp()
        {
            var p = this.Parent as GroupConstants;
            var i = p.ListConstants.IndexOf(this);
            if (i > 0)
            {
                this.SortingValue = p.ListConstants[i - 1].SortingValue - 1;
            }
        }
        protected override bool OnNodeCanMoveDown()
        {
            return (this.Parent as GroupConstants).ListConstants.IndexOf(this) < ((this.Parent as GroupConstants).ListConstants.Count - 1);
        }
        protected override void OnNodeMoveDown()
        {
            var p = this.Parent as GroupConstants;
            var i = p.ListConstants.IndexOf(this);
            if (i < p.ListConstants.Count - 1)
            {
                this.SortingValue = p.ListConstants[i + 1].SortingValue + 1;
            }
        }
        protected override void OnNodeRemove()
        {
            (this.Parent as GroupConstants).ListConstants.Remove(this);
        }
        protected override ITreeConfigNode OnNodeAddNew()
        {
            var res = new Constant();
            res.Parent = this.Parent;
            (this.Parent as GroupConstants).ListConstants.Add(res);
            GetUniqueName(Constant.DefaultName, res, (this.Parent as GroupConstants).ListConstants);
            (this.Parent.Parent as Config).SelectedNode = res;
            return res;
        }
        protected override ITreeConfigNode OnNodeAddClone()
        {
            var res = Constant.Clone(this.Parent, this, true, true);
            res.Parent = this.Parent;
            (this.Parent as GroupConstants).ListConstants.Add(res);
            this.Name = this.Name + "2";
            (this.Parent.Parent as Config).SelectedNode = res;
            return res;
        }

        #endregion ITreeNode
    }
}
