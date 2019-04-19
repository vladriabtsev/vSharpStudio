﻿using System;
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
    public partial class GroupCatalogs
    {
        partial void OnInit()
        {
            this.Name = "Catalogs";
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
    }
}
