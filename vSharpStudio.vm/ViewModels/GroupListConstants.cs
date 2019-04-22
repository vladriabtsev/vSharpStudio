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
    public partial class GroupListConstants : IListNodes<Constant>, ISubCount
    {
        [BrowsableAttribute(false)]
        public SortedObservableCollection<Constant> ListNodes { get; private set; }
        [BrowsableAttribute(false)]
        public int Count { get { return ListNodes.Count; } }

        partial void OnInit()
        {
            this.Name = "Constants";
            this.ListNodes = this.ListConstants;
        }

        #region ITreeNode

        public new string NodeText { get { return this.Name; } }
        #endregion ITreeNode
        public static Proto.Attr.DicPropAttrs GetDicPropertyAttributes()
        {
            GroupListConstants t = new GroupListConstants();
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
