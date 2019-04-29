﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Catalog:{Name,nq} props:{listProperties.Count,nq}")]
    public partial class Catalog : IListGroupNodes
    {
        public static readonly string DefaultName = "Catalog";
        [BrowsableAttribute(false)]
        public SortedObservableCollection<ITreeConfigNode> ListNodes { get; private set; }
        partial void OnInit()
        {
            this.ListNodes = new SortedObservableCollection<ITreeConfigNode>();
#if DEBUG
            //SubNodes.Add(this.GroupConstants, 1);
#endif
            ListNodes.Add(this.GroupProperties, 7);
            ListNodes.Add(this.GroupSubCatalogs, 8);
        }
        public Catalog(string name) : this()
        {
            (this as ITreeConfigNode).Name = name;
        }
        public Catalog(string name, List<Property> listProperties) : this()
        {
            (this as ITreeConfigNode).Name = name;
            foreach (var t in listProperties)
            {
                this.GroupProperties.ListProperties.Add(t);
            }
        }

        #region ITreeConfigNode

        #endregion ITreeConfigNode

        #region ITreeNode

        #endregion ITreeNode
    }
}
