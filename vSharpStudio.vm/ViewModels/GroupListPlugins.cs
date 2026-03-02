using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugInfo(),nq}")]
    public partial class GroupListPlugins : ITreeModel, ICanGoRight, IEditableNodeGroup
    {
        partial void OnDebugStringExtend(StringBuilder sb)
        {
            sb.Append(" Count:");
            sb.Append(this.ListPlugins.Count);
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public Config ParentConfig { get { Debug.Assert(this.Parent != null); return (Config)this.Parent; } }
        [Browsable(false)]
        public IConfig ParentConfigI { get { Debug.Assert(this.Parent != null); return (IConfig)this.Parent; } }
        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentConfig.Children;
        }
        public new ConfigNodesCollection<Plugin> Children { get { return this.ListPlugins; } }
        #endregion ITree

        partial void OnCreated()
        {
            this.IsEditable = false;
            this.ListPlugins.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            //this.ListPlugins.OnAddedAction = (t) =>
            //{
            //};
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this._Name = Defaults.PluginsGroupName;
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
        public bool GetIsHasMarkedForDeletion()
        {
            return false;
        }
        public bool GetIsHasNew()
        {
            return false;
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                //lst.Add(nameof(this.Description));
                nameof(this.Guid),
                //lst.Add(nameof(this.NameUi));
                nameof(this.Parent),
                nameof(this.Children)
            };
            return [.. lst];
        }
    }
}
