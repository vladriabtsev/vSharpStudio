using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupDocuments
    {
        partial void OnInit()
        {
            this.Name = "Documens";
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
            var res = new Document();
            res.Parent = this.Parent;
            this.ListDocuments.Add(res);
            GetUniqueName(Document.DefaultName, res, this.ListDocuments);
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
