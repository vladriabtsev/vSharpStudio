using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Document
    {
        public static readonly string DefaultName = "Document";

        #region ITreeNode
        //public string NodeText { get { return this.Name; } }
        protected override bool OnNodeCanMoveUp()
        {
            return (this.Parent as GroupDocuments).ListDocuments.IndexOf(this) > 0;
        }
        protected override void OnNodeMoveUp()
        {
            var p = this.Parent as GroupDocuments;
            var i = p.ListDocuments.IndexOf(this);
            if (i > 0)
            {
                this.SortingValue = p.ListDocuments[i - 1].SortingValue - 1;
            }
        }
        protected override bool OnNodeCanMoveDown()
        {
            return (this.Parent as GroupDocuments).ListDocuments.IndexOf(this) < ((this.Parent as GroupDocuments).ListDocuments.Count - 1);
        }
        protected override void OnNodeMoveDown()
        {
            var p = this.Parent as GroupDocuments;
            var i = p.ListDocuments.IndexOf(this);
            if (i < p.ListDocuments.Count - 1)
            {
                this.SortingValue = p.ListDocuments[i + 1].SortingValue + 1;
            }
        }
        protected override void OnNodeRemove()
        {
            (this.Parent as GroupDocuments).ListDocuments.Remove(this);
        }
        protected override ITreeConfigNode OnNodeAddNew()
        {
            var res = new Document();
            res.Parent = this.Parent;
            (this.Parent as GroupDocuments).ListDocuments.Add(res);
            GetUniqueName(Enumeration.DefaultName, res, (this.Parent as GroupDocuments).ListDocuments);
            (this.Parent.Parent as Config).SelectedNode = res;
            return res;
        }
        protected override ITreeConfigNode OnNodeAddClone()
        {
            var res = Document.Clone(this.Parent, this, true, true);
            res.Parent = this.Parent;
            (this.Parent as GroupDocuments).ListDocuments.Add(res);
            this.Name = this.Name + "2";
            (this.Parent.Parent as Config).SelectedNode = res;
            return res;
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
