using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} catalogs:{ListCatalogs.Count,nq}")]
    public partial class GroupCatalogs : IListNodes<Catalog>
    {
        [BrowsableAttribute(false)]
        public SortedObservableCollection<Catalog> ListNodes { get; private set; }
        partial void OnInit()
        {
            this.Name = "Catalogs";
            this.ListNodes = this.ListCatalogs;
        }
        //[BrowsableAttribute(false)]
        //public SortedObservableCollection<ITreeConfigNode> SubNodes
        //{
        //    get { return this._SubNodes; }
        //    set
        //    {
        //        this._SubNodes = value;
        //        NotifyPropertyChanged();
        //    }
        //}
        //private SortedObservableCollection<ITreeConfigNode> _SubNodes;

        #region ITreeNode
        //public string NodeText { get { return this.Name+" "+this.ListCatalogs.Count; } }
        protected override bool OnNodeCanLeft()
        {
            return false;
        }
        //protected override ITreeConfigNode OnNodeAddNew()
        //{
        //    var res = new Catalogs();
        //    (this.Parent as Config).ListCatalogsGroups.Add(res);
        //    return res;
        //}
        protected override ITreeConfigNode OnNodeAddNewSubNode()
        {
            var res = new Catalog();
            res.Parent = this.Parent;
            this.ListCatalogs.Add(res);
            GetUniqueName(Catalog.DefaultName, res, this.ListCatalogs);
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
            GroupCatalogs t = new GroupCatalogs();
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
