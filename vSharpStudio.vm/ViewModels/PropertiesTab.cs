using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} properties:{ListProperties.Count,nq} sub:{ListSubPropertiesGroups.Count,nq}")]
    public partial class PropertiesTab : ICanAddSubNode, ICanGoRight, ICanGoLeft
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

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (NodeCanAddClone())
            {
                if ((this.Parent as GroupListPropertiesTabs).ListPropertiesTabs.CanUp(this))
                    return true;
            }
            return false;
        }
        public override void NodeUp()
        {
            var prev = (PropertiesTab)(this.Parent as GroupListPropertiesTabs).ListPropertiesTabs.GetPrev(this);
            if (prev == null)
                return;
            SetSelected(prev);
        }
        public override void NodeMoveUp()
        {
            (this.Parent as GroupListPropertiesTabs).ListPropertiesTabs.MoveUp(this);
            SetSelected(this);
        }
        public override bool NodeCanDown()
        {
            if (NodeCanAddClone())
            {
                if ((this.Parent as GroupListPropertiesTabs).ListPropertiesTabs.CanDown(this))
                    return true;
            }
            return false;
        }
        public override void NodeDown()
        {
            var next = (PropertiesTab)(this.Parent as GroupListPropertiesTabs).ListPropertiesTabs.GetNext(this);
            if (next == null)
                return;
            SetSelected(next);
        }
        public override void NodeMoveDown()
        {
            (this.Parent as GroupListPropertiesTabs).ListPropertiesTabs.MoveDown(this);
            SetSelected(this);
        }
        public override void NodeRemove()
        {
            (this.Parent as GroupListPropertiesTabs).Remove(this);
            this.Parent = null;
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = PropertiesTab.Clone(this, true, true);
            node.Parent = this.Parent;
            (this.Parent as GroupListPropertiesTabs).Add(node);
            this.Name = this.Name + "2";
            SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new PropertiesTab();
            (this.Parent as GroupListPropertiesTabs).Add(node);
            GetUniqueName(PropertiesTab.DefaultName, node, (this.Parent as GroupListPropertiesTabs).ListPropertiesTabs);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations

    }
}
