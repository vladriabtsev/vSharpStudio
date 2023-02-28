using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("ConstantGroups:{Name,nq} Count:{ListConstantGroups.Count,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class GroupConstantGroups : ITreeModel, ICanGoRight, ICanGoLeft, ICanAddSubNode, INodeGenSettings, IEditableNodeGroup
    {
        [BrowsableAttribute(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public Model ParentModel { get { Debug.Assert(this.Parent != null); return (Model)this.Parent; } }
        [Browsable(false)]
        public IModel ParentModelI { get { Debug.Assert(this.Parent != null); return (IModel)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentModel.Children;
        }
        #endregion ITree
        new public ConfigNodesCollection<GroupListConstants> Children { get { return this.ListConstantGroups; } }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            GroupListConstants node = null!;
            if (node_impl == null)
            {
                node = new GroupListConstants(this);
            }
            else
            {
                node = (GroupListConstants)node_impl;
            }
            this.ListConstantGroups.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.ConstantsGroupName, node, this.ListConstantGroups);
            }
            var model = this.ParentModel;
            node.ShortId = model.LastConstantGroupShortId + 1;
            model.LastConstantGroupShortId = node.ShortId;
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        //[Browsable(false)]
        //new public string IconName { get { return "iconFolder"; } }
        //protected override string GetNodeIconName() { return "iconFolder"; }
        partial void OnCreated()
        {
            this._Name = Defaults.GroupConstantGroupsName;
            this.PrefixForDbTables = "Cnst";
            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this.ListConstantGroups.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListConstantGroups.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListConstantGroups.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListConstantGroups.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
        }
        public GroupListConstants AddGroupConstants(string name)
        {
            var node = new GroupListConstants(this) { Name = name };
            this.GetUniqueName(Defaults.ConstantsGroupName, node, this.ListConstantGroups);
            this.NodeAddNewSubNode(node);
            return node;
        }
        public IReadOnlyList<IGroupListConstants> GetIncludedConstantGroups(string guidAppPrjGen)
        {
            var res = new List<IGroupListConstants>();
            foreach (var t in this.ListConstantGroups)
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
            lst.Add(this.GetPropertyName(() => this.Parent));
            lst.Add(this.GetPropertyName(() => this.Children));
            return lst.ToArray();
        }
    }
}
