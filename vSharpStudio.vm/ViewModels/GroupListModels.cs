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
    [DebuggerDisplay("Group:{Name,nq} models:{ListModels.Count,nq}")]
    public partial class GroupListModels : ITreeModel, ICanAddSubNode, ICanGoRight
    {
        //[BrowsableAttribute(false)]
        //public SortedObservableCollection<ITreeConfigNode> Children { get; private set; }
        public IEnumerable<object> GetChildren(object parent) { return this.ListModels; }
        public bool HasChildren(object parent) { return this.ListModels.Count > 0; }
        partial void OnInit()
        {
            this.Name = "Models";
            this.IsEditable = false;
            //this.Children = new SortedObservableCollection<ITreeConfigNode>();
            //this.GroupSharedProperties.Parent = this;
            //Children.Add(this.GroupSharedProperties, 7);
            //this.GroupListDocuments.Parent = this;
            //Children.Add(this.GroupListDocuments, 8);
        }
        protected override void OnInitFromDto()
        {
            this.Name = "Models";
        }

        #region Tree operations
        public void AddBaseConfig(BaseConfig node)
        {
            this.NodeAddNewSubNode(node);
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Model node =null;
            if (node_impl == null)
                node = new Model();
            else
                node = (Model)node_impl;
            this.Add(node);
            if (node_impl == null)
                GetUniqueName(Model.DefaultName, node, this.ListModels);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
