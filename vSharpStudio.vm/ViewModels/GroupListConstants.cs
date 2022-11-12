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
    public partial class GroupListConstants : ITreeModel, ICanAddSubNode, ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, IEditableNodeGroup, IEditableNode
    {
        [BrowsableAttribute(false)]
        public GroupConstantGroups ParentGroupConstantGroups { get { Debug.Assert(this.Parent != null); return (GroupConstantGroups)this.Parent; } }
        [BrowsableAttribute(false)]
        public IGroupConstantGroups ParentGroupConstantGroupsI { get { Debug.Assert(this.Parent != null); return (IGroupConstantGroups)this.Parent; } }

        partial void OnCreated()
        {
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

        [PropertyOrder(100)]
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

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupConstantGroups.Children;
        }
        [Browsable(false)]
        new public ConfigNodesCollection<Constant> Children { get { return this.ListConstants; } }
        #endregion ITree

        #region Tree operations
        public void Remove()
        {
            this.ParentGroupConstantGroups.ListConstantGroups.Remove(this);
        }
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupConstantGroups.ListConstantGroups.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (GroupListConstants)this.ParentGroupConstantGroups.ListConstantGroups.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupConstantGroups.ListConstantGroups.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupConstantGroups.ListConstantGroups.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (GroupListConstants)this.ParentGroupConstantGroups.ListConstantGroups.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupConstantGroups.ListConstantGroups.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = GroupListConstants.Clone(this.ParentGroupConstantGroups, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupConstantGroups.ListConstantGroups.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new GroupListConstants(this.ParentGroupConstantGroups);
            this.ParentGroupConstantGroups.ListConstantGroups.Add(node);
            this.GetUniqueName(Defaults.ConstantsGroupName, node, this.ParentGroupConstantGroups.ListConstantGroups);
            this.SetSelected(node);
            return node;
        }
        public bool CanAddSubNode() { return true; }
        public Constant AddConstant(string name)
        {
            Constant node = new Constant(this) { Name = name };
            node.DataType = new DataType(node);
            this.NodeAddNewSubNode(node);
            return node;
        }
        //public Constant AddConstant(string name, DataType type)
        //{
        //    Constant node = new Constant(this) { Name = name, DataType = type };
        //    this.NodeAddNewSubNode(node);
        //    return node;
        //}
        public Constant AddConstantString(string name)
        {
            Constant node = new Constant(this) { Name = name };
            node.DataType = new DataType(node);
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Constant AddConstantEnumeration(string name, Enumeration en)
        {
            var node = new Constant(this) { Name = name };
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.ENUMERATION, ObjectGuid = en.Guid };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Constant AddConstantCatalog(string name, Catalog cat)
        {
            var node = new Constant(this) { Name = name };
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.CATALOG, ObjectGuid = cat.Guid };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            Constant node = null!;
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
                this.GetUniqueName(Defaults.ConstantName, node, this.ListConstants);
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
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            lst.Add(this.GetPropertyName(() => this.Description));
            lst.Add(this.GetPropertyName(() => this.Guid));
            lst.Add(this.GetPropertyName(() => this.NameUi));
            lst.Add(this.GetPropertyName(() => this.Parent));
            lst.Add(this.GetPropertyName(() => this.Children));
            return lst.ToArray();
        }
    }
}
