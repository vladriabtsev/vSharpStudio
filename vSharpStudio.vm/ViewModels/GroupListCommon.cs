using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListListCommon.Count,nq}")]
    public partial class GroupListCommon : ITreeModel, ICanGoRight
    {
        public IEnumerable<object> GetChildren(object parent) { return this.Children; }
        public bool HasChildren(object parent) { return this.Children.Count > 0; }
        partial void OnInit()
        {
            this.Name = "Common";
            this.IsEditable = false;
            this.Children = new SortedObservableCollection<ITreeConfigNode>();

            //this.GroupPlugins.Parent = this;
            //this.Children.Add(this.GroupPlugins, 0);
        }
        public SortedObservableCollection<ITreeConfigNode> Children { get; private set; }
    }
}
