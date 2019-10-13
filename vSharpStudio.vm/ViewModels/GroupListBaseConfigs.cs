using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} configs:{ListBaseConfigs.Count,nq}")]
    public partial class GroupListBaseConfigs : ITreeModel, ICanAddSubNode, ICanGoRight
    {
        public IEnumerable<object> GetChildren(object parent) { return this.ListBaseConfigs; }
        public bool HasChildren(object parent) { return this.ListBaseConfigs.Count > 0; }
        //[BrowsableAttribute(false)]
        //public SortedObservableCollection<ITreeConfigNode> Children { get; private set; }
        partial void OnInit()
        {
            this.Name = "BaseConfigs";
            this.IsEditable = false;
            //this.Children = new SortedObservableCollection<ITreeConfigNode>();
            //this.GroupSharedProperties.Parent = this;
            //Children.Add(this.GroupSharedProperties, 7);
            //this.GroupListDocuments.Parent = this;
            //Children.Add(this.GroupListDocuments, 8);
        }

        #region Tree operations
        public void AddBaseConfig(BaseConfig node)
        {
            this.NodeAddNewSubNode(node);
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            BaseConfig node=null;
            if (node_impl == null)
                node = new BaseConfig();
            else
                node = (BaseConfig)node_impl;
            this.Add(node);
            if (node_impl == null)
                GetUniqueName(BaseConfig.DefaultName, node, this.ListBaseConfigs);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
