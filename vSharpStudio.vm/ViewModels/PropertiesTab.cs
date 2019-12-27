using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} properties:{ListProperties.Count,nq} sub:{ListSubPropertiesGroups.Count,nq}")]
    public partial class PropertiesTab : 
        ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings
    {
        [DisplayName("Generators")]
        [Description("Expandable Attached Node Settings for App Project Generators")]
        [ExpandableObjectAttribute()]
        [ReadOnly(true)]
        public object GenSettings { get; set; }
        public static readonly string DefaultName = "Tab";
        [BrowsableAttribute(false)]
        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }
        partial void OnInit()
        {
            this.IsIncludableInModels = true;
            this.IsIndexFk = true;
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
            this.GroupProperties.Parent = this;
            this.Children.Add(this.GroupProperties, 7);
            this.GroupPropertiesTabs.Parent = this;
            this.Children.Add(this.GroupPropertiesTabs, 9);
            this.AddAllAppGenSettingsVmsToNewNode();
        }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListPropertiesTabs).ListPropertiesTabs.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (PropertiesTab)(this.Parent as GroupListPropertiesTabs).ListPropertiesTabs.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            (this.Parent as GroupListPropertiesTabs).ListPropertiesTabs.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListPropertiesTabs).ListPropertiesTabs.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (PropertiesTab)(this.Parent as GroupListPropertiesTabs).ListPropertiesTabs.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            (this.Parent as GroupListPropertiesTabs).ListPropertiesTabs.MoveDown(this);
            this.SetSelected(this);
        }

        public override void NodeRemove()
        {
            (this.Parent as GroupListPropertiesTabs).Remove(this);
            this.Parent = null;
        }

        public override ITreeConfigNode NodeAddClone()
        {
            var node = PropertiesTab.Clone(this.Parent, this, true, true);
            (this.Parent as GroupListPropertiesTabs).Add(node);
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new PropertiesTab(this.Parent);
            (this.Parent as GroupListPropertiesTabs).Add(node);
            this.GetUniqueName(PropertiesTab.DefaultName, node, (this.Parent as GroupListPropertiesTabs).ListPropertiesTabs);
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

    }
}
