using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} enumerations:{ListEnumerations.Count,nq}")]
    public partial class GroupEnumerations : IListNodes<Enumeration>
    {
        public SortedObservableCollection<Enumeration> ListNodes { get; private set; }
        partial void OnInit()
        {
            this.Name = "Enumerations";
            this.ListNodes = this.ListEnumerations;
        }

        #region ITreeNode
        public new string NodeText { get { return this.Name; } }

        #endregion ITreeNode
        public static Proto.Attr.DicPropAttrs GetDicPropertyAttributes()
        {
            GroupEnumerations t = new GroupEnumerations();
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
