using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListListCommon.Count,nq}")]
    public partial class GroupListCommon : ITreeModel, ICanGoRight, INodeGenSettings
    {
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.Children;
        }

        public override bool HasChildren(object parent)
        {
            return this.Children.Count > 0;
        }

        partial void OnInit()
        {
            this.Name = "Common";
            this.IsEditable = false;
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);

            // this.GroupPlugins.Parent = this;
            // this.Children.Add(this.GroupPlugins, 0);
        }

        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }

    }
}
