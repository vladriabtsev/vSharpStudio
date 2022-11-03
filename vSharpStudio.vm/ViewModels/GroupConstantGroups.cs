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
    [DebuggerDisplay("ConstantGroups:{Name,nq} Count:{ListConstantGroups.Count,nq}")]
    public partial class GroupConstantGroups : ITreeModel, ICanGoRight, ICanGoLeft, ICanAddSubNode, INodeGenSettings, IEditableNodeGroup
    {
        [Browsable(false)]
        public Model ParentModel { get { return (Model)this.Parent; } }
        [Browsable(false)]
        public IModel ParentModelI { get { return (IModel)this.Parent; } }

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            return this.ParentModel.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        [Browsable(false)]
        public ObservableCollectionWithActions<GroupListConstants> Children { get { return this.ListConstantGroups; } }
        #endregion ITree

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            GroupListConstants node = null;
            if (node_impl == null)
            {
                node = new GroupListConstants(this);
                this.GetUniqueName(Defaults.ConstantsGroupName, node, this.ListConstantGroups);
            }
            else
            {
                node = (GroupListConstants)node_impl;
            }
            this.ListConstantGroups.Add(node);
            var cfg = (Config)this.GetConfig();
            node.ShortId = cfg.Model.LastConstantGroupShortId + 1;
            cfg.Model.LastConstantGroupShortId = node.ShortId;
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
            this.IsEditable = true;

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

            //VmBindable.IsNotifyingStatic = false;
            // VmBindable.IsNotifyingStatic = true;
            //this.GroupSharedProperties.ListProperties.OnAddingAction = (t) =>
            //{
            //    t.IsNew = true;
            //};
            //this.GroupSharedProperties.ListProperties.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            //this.GroupListDocuments.ListDocuments.OnAddingAction = (t) =>
            //{
            //    t.IsNew = true;
            //};
            //this.GroupListDocuments.ListDocuments.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            //this.GroupListDocuments.ListDocuments.OnRemovedAction = (t) => {
            //    this.GetIsHasMarkedForDeletion();
            //    this.GetIsHasNew();
            //};
            //this.GroupSharedProperties.ListProperties.OnRemovedAction = (t) => {
            //    this.GetIsHasMarkedForDeletion();
            //    this.GetIsHasNew();
            //};
            //this.GroupListDocuments.ListDocuments.OnClearedAction = () => {
            //    this.GetIsHasMarkedForDeletion();
            //    this.GetIsHasNew();
            //};
            //this.GroupSharedProperties.ListProperties.OnClearedAction = () => {
            //    this.GetIsHasMarkedForDeletion();
            //    this.GetIsHasNew();
            //};
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
