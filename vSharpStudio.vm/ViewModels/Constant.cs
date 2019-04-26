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
            GroupListConstants vm = (GroupListConstants)this.Parent;
            int icurr = vm.ListConstants.IndexOf(this);
            vm.ListConstants.Add(new Constant() { Parent = this.Parent });
        }
        #endregion IConfigObject

        #region ITreeNode
        //        public string NodeText { get { return this.Name; } }

        #endregion ITreeNode
        public static Proto.Attr.ClassData GetDicPropertyAttributes()
        {
            Constant t = new Constant();
            StringBuilder sb = new StringBuilder();
            Proto.Attr.ClassData res = new Proto.Attr.ClassData();
            t.PropertyNameAction(p => p.NameUi, (m) =>
            {
                res.DicByProperty[m] = sb.Clear().PropertyOrderAttribute(2).ToString();
            });
            t.PropertyNameAction(p => p.Description, (m) =>
            {
                res.DicByProperty[m] = sb.Clear().PropertyOrderAttribute(3).ToString();
            });
            t.PropertyNameAction(p => p.DataType, (m) =>
            {
                //res.DicByProperty[m] = sb.Clear().PropertyOrderAttribute(4).ToString();
                res.DicByProperty[m] = sb.Clear().ExpandableObjectAttribute().ToString();
            });
            return res;
        }
    }
}
