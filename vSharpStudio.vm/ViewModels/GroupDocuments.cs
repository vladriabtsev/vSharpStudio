using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} documents:{ListDocuments.Count,nq}")]
    public partial class GroupDocuments : IChildren, ICanAddNode, ICanGoRight, ICanGoLeft
    {
        [BrowsableAttribute(false)]
        public SortedObservableCollection<ITreeConfigNode> Children { get; private set; }
        partial void OnInit()
        {
            this.Name = "Documens";
            this.Children = new SortedObservableCollection<ITreeConfigNode>();
            this.GroupSharedProperties.Parent = this;
            Children.Add(this.GroupSharedProperties, 7);
            this.GroupListDocuments.Parent = this;
            Children.Add(this.GroupListDocuments, 8);
        }
    }
}
