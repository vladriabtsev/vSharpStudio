using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Google.Protobuf.WellKnownTypes;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class GroupCatalogs : ITreeModel, ICanGoRight, ICanGoLeft/*, INodeGenSettings*/, IEditableNodeGroup //, IRoleGlobalSetting //, IRoleAccess
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" 1to1:{this.GroupRelations.GroupListOneToOneRelations.ListRelations.Count} MtoM:{this.GroupRelations.GroupListOneToOneRelations.ListRelations.Count} Cats:{this.GroupListCatalogs.ListCatalogs.Count}";
        }
        [Browsable(false)]
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

        [Browsable(false)]
        public new string IconName { get { return "iconFolder"; } }
        //protected override string GetNodeIconName() { return "iconFolder"; }
        partial void OnCreated()
        {
            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this._Name = Defaults.CatalogsGroupName;
            if (this.Children.Count > 0)
                return;
            var children = (ConfigNodesCollection<ITreeConfigNodeSortable>)this.Children;
            children.Add(this.GroupRelations, 2);
            children.Add(this.GroupListCatalogs, 3);

            //this.ListRoles.OnAddingAction = (t) =>
            //{
            //    t.IsNew = true;
            //};
            //this.ListRoles.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            //this.ListRoles.OnRemovedAction = (t) =>
            //{
            //    this.OnRemoveChild();
            //};
            //this.ListRoles.OnClearedAction = () =>
            //{
            //    this.OnRemoveChild();
            //};
        }
        public Catalog AddCatalog()
        {
            var node = new Catalog(this.GroupListCatalogs);
            this.GroupListCatalogs.NodeAddNewSubNode(node);
            return node;
        }
        public Catalog AddCatalog(string name, string? guid = null, string? guidFolder = null)
        {
            var node = new Catalog(this.GroupListCatalogs) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
            if (guidFolder != null)
                node.Folder.Guid = guidFolder;
#endif
            this.GroupListCatalogs.NodeAddNewSubNode(node);
            return node;
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                nameof(this.Description),
                nameof(this.Guid),
                nameof(this.NameUi),
                nameof(this.Parent),
                nameof(this.Children),
                nameof(this.DynamicNodesSettings)
            };
            return [.. lst];
        }
    }
}
