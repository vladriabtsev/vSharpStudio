using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Document : IListGroupNodes
    {
        public static readonly string DefaultName = "Document";
        [BrowsableAttribute(false)]
        public SortedObservableCollection<ITreeConfigNode> ListNodes { get; private set; }

        partial void OnInit()
        {
            this.ListNodes = new SortedObservableCollection<ITreeConfigNode>();
#if DEBUG
            //SubNodes.Add(this.GroupConstants, 1);
#endif
            ListNodes.Add(this.GroupPropertiesTree, 7);
            ListNodes.Add(this.GroupForms, 8);
            ListNodes.Add(this.GroupReports, 9);
        }

        #region ITreeNode

        //public string NodeText { get { return this.Name; } }
        //protected override bool OnNodeCanMoveUp()
        //{
        //    return (this.Parent as GroupDocuments).ListDocuments.IndexOf(this) > 0;
        //}
        //protected override void OnNodeMoveUp()
        //{
        //    var p = this.Parent as GroupDocuments;
        //    var i = p.ListDocuments.IndexOf(this);
        //    if (i > 0)
        //    {
        //        this.SortingValue = p.ListDocuments[i - 1].SortingValue - 1;
        //    }
        //}
        //protected override bool OnNodeCanMoveDown()
        //{
        //    return (this.Parent as GroupDocuments).ListDocuments.IndexOf(this) < ((this.Parent as GroupDocuments).ListDocuments.Count - 1);
        //}
        //protected override void OnNodeMoveDown()
        //{
        //    var p = this.Parent as GroupDocuments;
        //    var i = p.ListDocuments.IndexOf(this);
        //    if (i < p.ListDocuments.Count - 1)
        //    {
        //        this.SortingValue = p.ListDocuments[i + 1].SortingValue + 1;
        //    }
        //}
        //protected override void OnNodeRemove()
        //{
        //    (this.Parent as GroupDocuments).ListDocuments.Remove(this);
        //}
        //protected override ITreeConfigNode OnNodeAddNew()
        //{
        //    var res = new Document();
        //    res.Parent = this.Parent;
        //    (this.Parent as GroupDocuments).ListDocuments.Add(res);
        //    GetUniqueName(Enumeration.DefaultName, res, (this.Parent as GroupDocuments).ListDocuments);
        //    (this.Parent.Parent as Config).SelectedNode = res;
        //    return res;
        //}
        //protected override ITreeConfigNode OnNodeAddClone()
        //{
        //    var res = Document.Clone(this.Parent, this, true, true);
        //    res.Parent = this.Parent;
        //    (this.Parent as GroupDocuments).ListDocuments.Add(res);
        //    this.Name = this.Name + "2";
        //    (this.Parent.Parent as Config).SelectedNode = res;
        //    return res;
        //}

        #endregion ITreeNode
    }
}
