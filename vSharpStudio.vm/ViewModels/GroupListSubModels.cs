using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} configs:{ListBaseConfigs.Count,nq}")]
    public partial class GroupListSubModels : ICanAddSubNode, ICanGoRight
    {
        //[BrowsableAttribute(false)]
        //public SortedObservableCollection<ITreeConfigNode> Children { get; private set; }
        partial void OnInit()
        {
            this.Name = "SubModels";
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
            SubModel node =null;
            if (node_impl == null)
                node = new SubModel();
            else
                node = (SubModel)node_impl;
            this.Add(node);
            if (node_impl == null)
                GetUniqueName(SubModel.DefaultName, node, this.ListSubModels);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
