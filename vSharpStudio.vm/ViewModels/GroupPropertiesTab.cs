using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} properties:{ListProperties.Count,nq} sub:{ListSubPropertiesGroups.Count,nq}")]
    public partial class GroupPropertiesTab : IChildren, ICanAddSubNode, ICanGoRight, ICanGoLeft
    {
        public static readonly string DefaultName = "Tab";
        [BrowsableAttribute(false)]
        public SortedObservableCollection<ITreeConfigNode> Children { get; private set; }
        partial void OnInit()
        {
            this.Children = new SortedObservableCollection<ITreeConfigNode>();
            this.GroupProperties.Parent = this;
            Children.Add(this.GroupProperties, 7);
            this.GroupPropertiesSubtabs.Parent = this;
            Children.Add(this.GroupPropertiesSubtabs, 9);
        }
    }
}
