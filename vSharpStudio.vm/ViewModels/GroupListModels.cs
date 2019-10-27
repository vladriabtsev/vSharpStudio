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
        public override IEnumerable<object> GetChildren(object parent) { return this.ListModels; }
        public override bool HasChildren(object parent) { return this.ListModels.Count > 0; }
        private ConfigModel cm = null;
        private void Init()
        {
            this.Name = "Models";
            this.ListModels.OnAddAction = (m) =>
            {
                foreach (var t in cm.GetAllNodes())
                    t.ListInModels.Add(new ModelRow());
            };
            this.ListModels.OnRemoveAction = (indx) =>
            {
                foreach (var t in cm.GetAllNodes())
                    t.ListInModels.RemoveAt(indx);
            };
            this.ListModels.OnSortAction = (ifrom, ito) =>
            {
                if (ifrom == ito || ifrom == -1)
                    return;
                foreach (var t in cm.GetAllNodes())
                    MoveObject(ifrom, ito, t);
            };
            cm = (this.Parent as Config).Model;
        }
        private static void MoveObject(int ifrom, int ito, ITreeConfigNode t)
        {
            var obj = t.ListInModels[ifrom];
            t.ListInModels.RemoveAt(ifrom);
            t.ListInModels.Insert(ito, obj);
        }
        partial void OnInit()
        {
            this.IsEditable = false;
            //this.Children = new SortedObservableCollection<ITreeConfigNode>();
            //this.GroupSharedProperties.Parent = this;
            //Children.Add(this.GroupSharedProperties, 7);
            //this.GroupListDocuments.Parent = this;
            //Children.Add(this.GroupListDocuments, 8);
            this.Init();
        }
        protected override void OnInitFromDto()
        {
            this.Init();
        }

        #region Tree operations
        public Model AddModel(string name)
        {
            var node = new Model(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Model node = null;
            if (node_impl == null)
                node = new Model(this);
            else
                node = (Model)node_impl;
            node.DicInclusionRecordObjectGuids.IsActivateActions = true;
            this.Add(node);
            if (node_impl == null)
                GetUniqueName(Model.DefaultName, node, this.ListModels);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
