using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} documents:{ListDocuments.Count,nq}")]
    public partial class GroupListDocuments : IListNodes<Document>, IGroupListSubNodes
    {
        [BrowsableAttribute(false)]
        public SortedObservableCollection<Document> ListNodes { get; private set; }

        partial void OnInit()
        {
            this.Name = "Documents";
            this.ListNodes = this.ListDocuments;
        }

        #region ITreeNode

        public new string NodeText { get { return this.Name; } }
        [BrowsableAttribute(false)]
        int IGroupListSubNodes.Count => ListNodes.Count;
        int IGroupListSubNodes.IndexOf(ITreeConfigNode obj)
        {
            return this.ListDocuments.IndexOf((Document)obj);
        }

        ITreeConfigNode IGroupListSubNodes.GetNode(int index)
        {
            return this.ListDocuments[index];
        }

        #endregion ITreeNode
        public static Proto.Attr.DicPropAttrs GetDicPropertyAttributes()
        {
            GroupListDocuments t = new GroupListDocuments();
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
