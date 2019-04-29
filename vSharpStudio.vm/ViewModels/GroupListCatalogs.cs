﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} sub_catalogs:{ListCatalogs.Count,nq}")]
    public partial class GroupListCatalogs : IListNodes<Catalog>, IGroupListSubNodes
    {
        [BrowsableAttribute(false)]
        public SortedObservableCollection<Catalog> ListNodes { get; private set; }

        partial void OnInit()
        {
            this.Name = "Catalogs";
            this.ListNodes = this.ListCatalogs;
        }

        #region ITreeNode

        [BrowsableAttribute(false)]
        public new string NodeText { get { return this.Name; } }
        [BrowsableAttribute(false)]
        int IGroupListSubNodes.Count => ListNodes.Count;
        int IGroupListSubNodes.IndexOf(ITreeConfigNode obj)
        {
            return this.ListCatalogs.IndexOf((Catalog)obj);
        }

        ITreeConfigNode IGroupListSubNodes.GetNode(int index)
        {
            return this.ListCatalogs[index];
        }
        #endregion ITreeNode
    }
}
