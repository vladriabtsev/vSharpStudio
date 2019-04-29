using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} properties:{ListProperties.Count,nq}")]
    public partial class GroupListProperties : IListNodes<Property>, IGroupListSubNodes
    {
        [BrowsableAttribute(false)]
        public SortedObservableCollection<Property> ListNodes { get; private set; }
        partial void OnInit()
        {
            this.Name = "Properties";
            this.ListNodes = this.ListProperties;
        }

        #region ITreeNode

        [BrowsableAttribute(false)]
        public new string NodeText { get { return this.Name + " " + this.ListProperties.Count; } }
        [BrowsableAttribute(false)]
        int IGroupListSubNodes.Count => ListNodes.Count;
        int IGroupListSubNodes.IndexOf(ITreeConfigNode obj)
        {
            return this.ListProperties.IndexOf((Property)obj);
        }
        ITreeConfigNode IGroupListSubNodes.GetNode(int index)
        {
            return this.ListProperties[index];
        }
        //void IGroupListSubNodes.AddNew()
        //{
        //    var p = new Property();
        //    p.Parent = this.Parent;
        //    this.ListProperties.Add(p);
        //    GetUniqueName(Property.DefaultName, p, this.ListProperties);
        //    ITreeConfigNode config = this.Parent;
        //    while (config.Parent != null)
        //        config = config.Parent;
        //    (config as Config).SelectedNode = p;
        //}

        #endregion ITreeNode
    }
}
