using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Catalog:{Name,nq} props:{listProperties.Count,nq}")]
    public partial class Catalog : IChildren, ICanGoLeft, ICanGoRight, ICanAddNode, ICanAddSubNode
    {
        public static readonly string DefaultName = "Catalog";
        public SortedObservableCollection<ITreeConfigNode> Children { get; private set; }
        partial void OnInit()
        {
            this.Children = new SortedObservableCollection<ITreeConfigNode>();
#if DEBUG
            //SubNodes.Add(this.GroupConstants, 1);
#endif
            this.GroupProperties.Parent = this;
            Children.Add(this.GroupProperties, 3);
            this.GroupForms.Parent = this;
            Children.Add(this.GroupForms, 4);
            this.GroupReports.Parent = this;
            Children.Add(this.GroupReports, 5);
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
                this.GroupProperties.Children.Add(t);
            }
        }

        #region ITreeConfigNode

        #endregion ITreeConfigNode

        #region ITreeNode

        #endregion ITreeNode
    }
}
