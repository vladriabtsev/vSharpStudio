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
    public partial class PropertiesTab : ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNode, IEditableNodeGroup
    {
        public static readonly string DefaultName = "Tab";

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as GroupListPropertiesTabs;
            return p.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        [BrowsableAttribute(false)]
        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }
        [Browsable(false)]
        new public string IconName { get { return "iconFolder"; } }
        //protected override string GetNodeIconName() { return "iconFolder"; }
        partial void OnInit()
        {
            this.IsIncludableInModels = true;
            this.IsIndexFk = true;
            this.Children = new ConfigNodesCollection<ITreeConfigNode>(this);
            this.GroupProperties.Parent = this;
            this.GroupProperties.ListProperties.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.GroupProperties.ListProperties.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.Children.Add(this.GroupProperties, 7);
            this.GroupPropertiesTabs.Parent = this;
            this.GroupProperties.ListProperties.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.GroupPropertiesTabs.ListPropertiesTabs.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.Children.Add(this.GroupPropertiesTabs, 9);
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
            this.GroupProperties.AddAllAppGenSettingsVmsToNode();
            this.GroupPropertiesTabs.AddAllAppGenSettingsVmsToNode();
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
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
        public Property AddProperty(string name)
        {
            var node = new Property(this.GroupProperties) { Name = name };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddProperty(string name, DataType type)
        {
            var node = new Property(this.GroupProperties) { Name = name, DataType = type };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddProperty(string name, EnumDataType type, uint length, uint accuracy)
        {
            var node = new Property(this.GroupProperties) { Name = name, DataType = new DataType() { DataTypeEnum = type, Length = length, Accuracy = accuracy } };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyString(string name, uint length)
        {
            var dt = new DataType() { DataTypeEnum = EnumDataType.STRING, Length = length };
            var node = new Property(this.GroupProperties) { Name = name, DataType = dt };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyNumerical(string name, uint length, uint accuracy)
        {
            var dt = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL, Length = length, Accuracy = accuracy };
            var node = new Property(this.GroupProperties) { Name = name, DataType = dt };
            this.GroupProperties.NodeAddNewSubNode(node);
            return node;
        }
        public IEnumerable<ITreeConfigNode> GetParentList()
        {
            var p = this.Parent as GroupListPropertiesTabs;
            return p.ListPropertiesTabs;
        }
        public void Remove()
        {
            var p = this.Parent as GroupListPropertiesTabs;
            p.ListPropertiesTabs.Remove(this);
        }
        #endregion Tree operations

        [PropertyOrder(1)]
        [ReadOnly(true)]
        [DisplayName("Composite")]
        [Description("Composite name based on IsCompositeNames and IsUseGroupPrefix model parameters")]
        public string CompositeName
        {
            get
            {
                return GetCompositeName();
            }
        }
    }
}
