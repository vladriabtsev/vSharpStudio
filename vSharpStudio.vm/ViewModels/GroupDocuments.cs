using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq}")]
    public partial class GroupDocuments : IChildren, ICanGoRight, ICanGoLeft
    {
        [BrowsableAttribute(false)]
        public SortedObservableCollection<ITreeConfigNode> Children { get; private set; }
        partial void OnInit()
        {
            this.Name = "Documens";
            this.IsEditable = false;
            this.Children = new SortedObservableCollection<ITreeConfigNode>();
            this.GroupSharedProperties.Parent = this;
            Children.Add(this.GroupSharedProperties, 7);
            this.GroupListDocuments.Parent = this;
            Children.Add(this.GroupListDocuments, 8);
        }
    }
}
