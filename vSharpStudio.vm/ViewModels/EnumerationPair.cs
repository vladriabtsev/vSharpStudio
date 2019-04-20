using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public partial class EnumerationPair
    {
        partial void OnInit()
        {
        }

        #region ITreeNode
        //        public string NodeText { get { return this.Name; } }
        protected override bool OnNodeCanRight()
        {
            return false;
        }

        #endregion ITreeNode
        public static Proto.Attr.DicPropAttrs GetDicPropertyAttributes()
        {
            EnumerationPair t = new EnumerationPair();
            StringBuilder sb = new StringBuilder();
            Proto.Attr.DicPropAttrs res = new Proto.Attr.DicPropAttrs();
            t.PropertyNameAction(p => p.NameUi, (m) =>
            {
                res.DicByProperty[m] = sb.Clear().PropertyOrderAttribute(2).ToString();
            });
            t.PropertyNameAction(p => p.Description, (m) =>
            {
                res.DicByProperty[m] = sb.Clear().PropertyOrderAttribute(3).ToString();
            });
            return res;
        }
    }
}
