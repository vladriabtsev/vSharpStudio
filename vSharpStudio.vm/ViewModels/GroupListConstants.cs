using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} constants:{ListConstants.Count,nq}")]
    public partial class GroupListConstants : IListNodes<Constant>, IGroupListSubNodes
    {
        [BrowsableAttribute(false)]
        public SortedObservableCollection<Constant> ListNodes { get; private set; }

        partial void OnInit()
        {
            this.Name = "Constants";
            this.ListNodes = this.ListConstants;
        }

        #region ITreeNode

        public new string NodeText { get { return this.Name; } }

        [BrowsableAttribute(false)]
        int IGroupListSubNodes.Count => ListNodes.Count;
        int IGroupListSubNodes.IndexOf(ITreeConfigNode obj)
        {
            return this.ListConstants.IndexOf((Constant)obj);
        }

        ITreeConfigNode IGroupListSubNodes.GetNode(int index)
        {
            return this.ListConstants[index];
        }

        #endregion ITreeNode
    }
}
