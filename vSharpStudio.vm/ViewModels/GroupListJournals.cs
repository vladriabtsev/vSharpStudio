using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} journals:{ListJournals.Count,nq}")]
    public partial class GroupListJournals : IListNodes<Journal>, IGroupListSubNodes
    {
        [BrowsableAttribute(false)]
        public SortedObservableCollection<Journal> ListNodes { get; private set; }
        partial void OnInit()
        {
            this.Name = "Journals";
            this.ListNodes = this.ListJournals;
        }
        
        #region ITreeNode

        [BrowsableAttribute(false)]
        public new string NodeText { get { return this.Name; } }
        [BrowsableAttribute(false)]
        int IGroupListSubNodes.Count => ListNodes.Count;
        int IGroupListSubNodes.IndexOf(ITreeConfigNode obj)
        {
            return this.ListJournals.IndexOf((Journal)obj);
        }

        ITreeConfigNode IGroupListSubNodes.GetNode(int index)
        {
            return this.ListJournals[index];
        }

        #endregion ITreeNode

        public static Proto.Attr.ClassData GetDicPropertyAttributes()
        {
            GroupListJournals t = new GroupListJournals();
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
            return res;
        }
    }
}
