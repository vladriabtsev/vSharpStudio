using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Document : IChildren, ICanGoLeft, ICanGoRight, ICanAddNode
    {
        public static readonly string DefaultName = "Document";
        [BrowsableAttribute(false)]
        public SortedObservableCollection<ITreeConfigNode> Children { get; private set; }

        partial void OnInit()
        {
            this.Children = new SortedObservableCollection<ITreeConfigNode>();
#if DEBUG
            //SubNodes.Add(this.GroupConstants, 1);
#endif
            this.GroupProperties.Parent = this;
            Children.Add(this.GroupProperties, 6);
            this.GroupPropertiesTabs.Parent = this;
            Children.Add(this.GroupPropertiesTabs, 7);
            this.GroupForms.Parent = this;
            Children.Add(this.GroupForms, 8);
            this.GroupReports.Parent = this;
            Children.Add(this.GroupReports, 9);
        }
    }
}
