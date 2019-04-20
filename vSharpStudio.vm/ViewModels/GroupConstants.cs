using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} constants:{ListConstants.Count,nq}")]
    public partial class GroupConstants
    {
        partial void OnInit()
        {
            this.Name = "Constants";
        }

        #region ITreeNode

        public new string NodeText { get { return this.Name; } }
        protected override bool OnNodeCanLeft()
        {
            return false;
        }
        protected override bool OnNodeCanAddNew()
        {
            return false;
        }
        protected override bool OnNodeCanAddNewSubNode()
        {
            return true;
        }
        protected override ITreeConfigNode OnNodeAddNewSubNode()
        {
            var res = new Constant();
            res.Parent = this.Parent;
            this.ListConstants.Add(res);
            GetUniqueName(Constant.DefaultName, res, this.ListConstants);
            (this.Parent as Config).SelectedNode = res;
            return res;
        }
        protected override bool OnNodeCanMoveDown()
        {
            return false;
        }
        protected override bool OnNodeCanMoveUp()
        {
            return false;
        }
        protected override bool OnNodeCanAddClone()
        {
            return false;
        }
        protected override bool OnNodeCanRemove()
        {
            return false;
        }

        #endregion ITreeNode
        public static Proto.Attr.DicPropAttrs GetDicPropertyAttributes()
        {
            DataType t = new DataType();
            StringBuilder sb = new StringBuilder();
            Proto.Attr.DicPropAttrs res = new Proto.Attr.DicPropAttrs();
            //t.PropertyNameAction(p => p.DataTypeEnum, (m) =>
            //{
            //    res[m] = sb.Clear().Category("kuku").ToString();
            //});
            return res;
        }
    }
}
