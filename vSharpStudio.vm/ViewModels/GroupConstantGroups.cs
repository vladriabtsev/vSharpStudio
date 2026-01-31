using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class GroupConstantGroups : ITreeModel, ICanGoRight, ICanGoLeft, ICanAddSubNode, INodeGenSettings, IEditableNodeGroup, IRoleGlobalSetting //, IRoleAccess
    {
        private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(GroupConstantGroups));
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{ListConstantGroups.Count}";
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public Model ParentModel { get { Debug.Assert(this.Parent != null); return (Model)this.Parent; } }
        [Browsable(false)]
        public IModel ParentModelI { get { Debug.Assert(this.Parent != null); return (IModel)this.Parent; } }
        public int IndexOf(IGroupListConstants cnstg)
        {
            return this.ListConstantGroups.IndexOf((GroupListConstants)cnstg);
        }
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
        public new ConfigNodesCollection<GroupListConstants> Children { get { return this.ListConstantGroups; } }

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
            node.ShortId = ++this.LastShortId;
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        //[Browsable(false)]
        //new public string IconName { get { return "iconFolder"; } }
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
            this._Name = Defaults.ConstantsGroupGroupsName;
            if (string.IsNullOrWhiteSpace(this._PrefixForCompositionNames)) this._PrefixForCompositionNames = "Cnst";
            this.ListConstantGroups.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListConstantGroups.OnAddedAction = (t) =>
            {
                t.OnAdded();
                t.InitRoles();
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
            _logger?.Debug("Previous Stable Config VM is created");
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
            var lst = new List<string>
            {
                nameof(this.Parent),
                nameof(this.Children)
            };
            return [.. lst];
        }

        #region Roles
        public EnumConstantAccess GetRoleConstantAccess(IRole role)
        {
            return role.DefaultConstantEditAccessSettings;
        }
        public EnumPrintAccess GetRoleConstantPrint(IRole role)
        {
            return role.DefaultConstantPrintAccessSettings;
        }
        #endregion Roles
    }
}
