using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} journals:{ListJournals.Count,nq}")]
    public partial class GroupJournals
    {
        partial void OnInit()
        {
            this.Name = "Journals";
        }
        #region ITreeNode
        //public string NodeText { get { return this.Name+" "+this.ListCatalogs.Count; } }
        protected override bool OnNodeCanLeft()
        {
            return false;
        }
        protected override bool OnNodeCanAddNew()
        {
            return false;
        }
        //protected override ITreeConfigNode OnNodeAddNew()
        //{
        //    var res = new Catalogs();
        //    (this.Parent as Config).ListCatalogsGroups.Add(res);
        //    return res;
        //}
        protected override bool OnNodeCanAddNewSubNode()
        {
            return true;
        }
        protected override ITreeConfigNode OnNodeAddNewSubNode()
        {
            var res = new Journal();
            res.Parent = this.Parent;
            this.ListJournals.Add(res);
            GetUniqueName(Journal.DefaultName, res, this.ListJournals);
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
