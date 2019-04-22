using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} journals:{ListJournals.Count,nq}")]
    public partial class GroupJournals : IListNodes<Journal>
    {
        [BrowsableAttribute(false)]
        public SortedObservableCollection<Journal> ListNodes { get; private set; }
        partial void OnInit()
        {
            this.Name = "Journals";
            this.ListNodes = this.ListJournals;
        }
        #region ITreeNode
        #endregion ITreeNode
        public static Proto.Attr.DicPropAttrs GetDicPropertyAttributes()
        {
            GroupJournals t = new GroupJournals();
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
