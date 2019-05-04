using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} documents:{ListDocuments.Count,nq}")]
    public partial class GroupDocuments : IChildren, ICanNotLeft
    {
        [BrowsableAttribute(false)]
        public SortedObservableCollection<ITreeConfigNode> Children { get; private set; }
        partial void OnInit()
        {
            this.Name = "Documens";
            this.Children = new SortedObservableCollection<ITreeConfigNode>();
            Children.Add(this.GroupSharedProperties, 7);
            Children.Add(this.GroupListDocuments, 8);
        }
        #region ITreeNode
        //public string NodeText { get { return this.Name+" "+this.ListCatalogs.Count; } }
        //protected override bool OnNodeCanLeft()
        //{
        //    return false;
        //}
        //protected override bool OnNodeCanAddNew()
        //{
        //    return false;
        //}
        ////protected override ITreeConfigNode OnNodeAddNew()
        ////{
        ////    var res = new Catalogs();
        ////    (this.Parent as Config).ListCatalogsGroups.Add(res);
        ////    return res;
        ////}
        //protected override bool OnNodeCanAddNewSubNode()
        //{
        //    return true;
        //}
        //protected override ITreeConfigNode OnNodeAddNewSubNode()
        //{
        //    var res = new Document();
        //    res.Parent = this.Parent;
        //    this.ListDocuments.Add(res);
        //    GetUniqueName(Document.DefaultName, res, this.ListDocuments);
        //    (this.Parent as Config).SelectedNode = res;
        //    return res;
        //}
        //protected override bool OnNodeCanMoveDown()
        //{
        //    return false;
        //}
        //protected override bool OnNodeCanMoveUp()
        //{
        //    return false;
        //}
        //protected override bool OnNodeCanAddClone()
        //{
        //    return false;
        //}
        //protected override bool OnNodeCanRemove()
        //{
        //    return false;
        //}
        #endregion ITreeNode
    }
}
