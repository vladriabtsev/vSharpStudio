﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Constant : EntityObjectBaseWithGuid<Constant, Constant.ConstantValidator>, IEntityObject, ITreeNode, ISortingValue, IComparable<Constant>
    {
        partial void OnInit()
        {
        }
        public void OnInitFromDto()
        {
            //RecreateSubNodes();
        }

        public int CompareTo(Constant other) { return this.SortingValue.CompareTo(other.SortingValue); }

        #region IEntityObject
        //void IEntityObject.Create()
        //{
        //    Constants vm = (Constants)this.Parent;
        //    int icurr=vm.ListConstants.IndexOf(this);
        //    vm.ListConstants.Add(new Constant(this.Parent));

        //}
        #endregion IEntityObject

        #region ITreeNode
        public ITreeNode Parent { get; internal set; }

        public IEnumerable<ITreeNode> SubNodes => null; // this._SubNodes;
        //private IEnumerable<ITreeNode> _SubNodes;
        //partial void OnPropertiesChanged()
        //{
        //    _SubNodes = new ITreeNode[] { this.Properties };
        //}
        public bool IsSelected
        {
            get { return this._IsSelected; }
            set
            {
                this._IsSelected = value;
                NotifyPropertyChanged();
            }
        }
        private bool _IsSelected;
        public bool IsExpanded
        {
            get { return this._IsExpanded; }
            set
            {
                this._IsExpanded = value;
                NotifyPropertyChanged();
            }
        }
        private bool _IsExpanded;
        public string NodeText { get { return this.Name; } }

        #endregion ITreeNode
    }
}
