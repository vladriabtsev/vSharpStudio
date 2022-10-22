using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("GroupConstants:{Name,nq} Count:{ListConstants.Count,nq}")]
    public partial class GroupListConstants : ITreeModel, ICanAddSubNode, ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNodeGroup
    {
        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as GroupConstantGroups;
            return p.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ConfigNodesCollection<Constant> Children { get { return this.ListConstants; } }

        partial void OnCreated()
        {
            this._Name = Defaults.ConstantsGroupName;
            this.IsEditable = false;
            this.ShortIdTypeForCacheKey = "t";
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this.ListConstants.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListConstants.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListConstants.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListConstants.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
        }

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
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
        }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupConstantGroups).ListConstantGroups.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (GroupListConstants)(this.Parent as GroupConstantGroups).ListConstantGroups.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            (this.Parent as GroupConstantGroups).ListConstantGroups.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupConstantGroups).ListConstantGroups.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (GroupListConstants)(this.Parent as GroupConstantGroups).ListConstantGroups.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            (this.Parent as GroupConstantGroups).ListConstantGroups.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = GroupListConstants.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            (this.Parent as GroupConstantGroups).ListConstantGroups.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new GroupListConstants(this.Parent);
            (this.Parent as GroupConstantGroups).ListConstantGroups.Add(node);
            this.GetUniqueName(Defaults.ConstantsGroupName, node, (this.Parent as GroupConstantGroups).ListConstantGroups);
            this.SetSelected(node);
            return node;
        }
        public bool CanAddSubNode() { return true; }
        public Constant AddConstant(string name, DataType type = null)
        {
            Constant node = new Constant(this) { Name = name, DataType = new DataType() };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Constant AddConstantString(string name)
        {
            Constant node = new Constant(this) { Name = name, DataType = new DataType() {  } };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Constant AddConstantEnumeration(string name, Enumeration en)
        {
            var dt = new DataType() { DataTypeEnum = EnumDataType.ENUMERATION, ObjectGuid = en.Guid };
            var node = new Constant(this) { Name = name, DataType = dt };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Constant AddConstantCatalog(string name, Catalog cat)
        {
            var dt = new DataType() { DataTypeEnum = EnumDataType.CATALOG, ObjectGuid = cat.Guid };
            var node = new Constant(this) { Name = name, DataType = dt };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Constant node = null;
            if (node_impl == null)
            {
                node = new Constant(this);
            }
            else
            {
                node = (Constant)node_impl;
            }

            this.Add(node);
            node.DataType.Parent = node;
            if (node_impl == null)
            {
                this.GetUniqueName(Constant.DefaultName, node, this.ListConstants);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations
        public IReadOnlyList<IConstant> GetIncludedConstants(string guidAppPrjGen)
        {
            var res = new List<IConstant>();
            foreach (var t in this.ListConstants)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    res.Add(t);
                }
            }
            return res;
        }
    }
}
