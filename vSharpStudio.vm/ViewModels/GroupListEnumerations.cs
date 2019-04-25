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
    public partial class GroupListEnumerations : IListNodes<Enumeration>, IGroupListSubNodes
    {
        [BrowsableAttribute(false)]
        public SortedObservableCollection<Enumeration> ListNodes { get; private set; }
        partial void OnInit()
        {
            this.Name = "Enumerations";
            this.ListNodes = this.ListEnumerations;
        }

        #region ITreeNode
        [BrowsableAttribute(false)]
        public new string NodeText { get { return this.Name; } }
        [BrowsableAttribute(false)]
        int IGroupListSubNodes.Count => ListNodes.Count;
        int IGroupListSubNodes.IndexOf(ITreeConfigNode obj)
        {
            return this.ListEnumerations.IndexOf((Enumeration)obj);
        }

        ITreeConfigNode IGroupListSubNodes.GetNode(int index)
        {
            return this.ListEnumerations[index];
        }

        #endregion ITreeNode
        public static Proto.Attr.DicPropAttrs GetDicPropertyAttributes()
        {
            GroupListEnumerations t = new GroupListEnumerations();
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
