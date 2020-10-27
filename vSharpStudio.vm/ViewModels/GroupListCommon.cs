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
    public partial class GroupListCommon : ITreeModel, ICanGoRight, INodeGenSettings, IEditableNodeGroup
    {
        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as ConfigModel;
            return p.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        new public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }

        partial void OnInit()
        {
            this._Name = "Common";
            this.IsEditable = false;
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);

            // this.GroupPlugins.Parent = this;
            // this.Children.Add(this.GroupPlugins, 0);
        }
    }
}
