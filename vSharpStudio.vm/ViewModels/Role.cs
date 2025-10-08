using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class Role : ICanGoLeft, ICanAddNode, INodeGenSettings, IEditableNode
    {
        [Browsable(false)]
        public GroupListRoles ParentGroupListRoles { get { Debug.Assert(this.Parent != null); return (GroupListRoles)this.Parent; } }
        [Browsable(false)]
        public IGroupListRoles ParentGroupListRolesI { get { Debug.Assert(this.Parent != null); return (IGroupListRoles)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListRoles.Children;
        }
        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconWindowsForm"; } }
        //protected override string GetNodeIconName() { return "iconWindowsForm"; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            this._DefaultCatalogEditAccessSettings = EnumCatalogDetailAccess.C_MARK_DEL;
            this._DefaultCatalogPrintAccessSettings = EnumPrintAccess.PR_PRINT;
            this._DefaultConstantEditAccessSettings = EnumConstantAccess.CN_EDIT;
            this._DefaultConstantPrintAccessSettings = EnumPrintAccess.PR_PRINT;
            this._DefaultDocumentEditAccessSettings = EnumDocumentAccess.D_UNPOST;
            this._DefaultDocumentPrintAccessSettings = EnumPrintAccess.PR_PRINT;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            //this.ListMainViewForms.OnAddingAction = (t) =>
            //{
            //    t.IsNew = true;
            //};
            //this.ListMainViewForms.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            //this.ListMainViewForms.OnRemovedAction = (t) =>
            //{
            //    this.OnRemoveChild();
            //};
            //this.ListMainViewForms.OnClearedAction = () =>
            //{
            //    this.OnRemoveChild();
            //};
        }
        protected override ConfigNodesCollection<Role>? GetParentCollection() { return this.ParentGroupListRoles.ListRoles; }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
            //this.GroupProperties.AddAllAppGenSettingsVmsToNode();
            //this.GroupForms.AddAllAppGenSettingsVmsToNode();
            //this.GroupReports.AddAllAppGenSettingsVmsToNode();
        }

        #region Tree operations
        public override ITreeConfigNode NodeAddClone()
        {
            Debug.Assert(this.Parent != null);
            var node = Role.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupListRoles.ListRoles.Add(node, this);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Role(this.Parent);
            this.ParentGroupListRoles.ListRoles.Add(node, this);
            this.GetUniqueName(Defaults.RoleName, node, this.ParentGroupListRoles.ListRoles);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListRoles.ListRoles.Remove(this);
        }
        #endregion Tree operations
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                nameof(this.Parent),
                nameof(this.Children)
            };
            return [.. lst];
        }
    }
}
