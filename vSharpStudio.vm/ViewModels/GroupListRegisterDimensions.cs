using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugInfo(),nq}")]
    public partial class GroupListRegisterDimensions : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup
    {
        partial void OnDebugStringExtend(StringBuilder sb)
        {
            sb.Append(" Count:");
            sb.Append(this.ListDimensions.Count);
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public Register ParentRegister { get { Debug.Assert(this.Parent != null); return (Register)this.Parent; } }
        [Browsable(false)]
        public IRegister ParentRegisterI { get { Debug.Assert(this.Parent != null); return (IRegister)this.Parent; } }

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

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentRegister.Children;
        }
        #endregion ITree

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            RegisterDimension node = null!;
            if (node_impl == null)
            {
                node = new RegisterDimension(this)
                {
                    Position = this.ParentRegister.GetNextFreePosition()
                };
            }
            else
            {
                node = (RegisterDimension)node_impl;
            }
            this.ListDimensions.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.RegisterDimensionName, node, this.ListDimensions);
            }
            //var model = this.ParentRegister;
            //node.ShortId = model.LastRegisterShortId + 1;
            //model.LastRegisterShortId = node.ShortId;
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        public new ConfigNodesCollection<RegisterDimension> Children { get { return this.ListDimensions; } }

        partial void OnCreated()
        {
            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        partial void OnSortTypeChanged() { this.ListDimensions.Sort(this.SortType); }
        private void Init()
        {
            OnSortTypeChanged();
            //if (this.Parent is Catalog)
            //{
            //    this.NameUi = "Sub Catalogs";
            //}
            this.ListDimensions.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListDimensions.OnAddedAction = (t) =>
            {
                t.OnAdded();
                //t.InitRoles();
            };
            this.ListDimensions.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListDimensions.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
            this._Name = Defaults.RegisterDimensionsGroupName;
        }
        public int IndexOf(IRegisterDimension dim)
        {
            return this.ListDimensions.IndexOf((dim as RegisterDimension)!);
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                nameof(this.Description),
                nameof(this.Guid),
                nameof(this.NameUi),
                nameof(this.Parent),
                nameof(this.Children)
            };
            //if (!this.UseCodeProperty)
            //    lst.Add(nameof(this.PropertyCodeName));
            //if (!this.UseNameProperty)
            //    lst.Add(nameof(this.PropertyNameName));
            return [.. lst];
        }

        #region Roles
        //public EnumCatalogDetailAccess GetRoleCatalogAccess(IRole role)
        //{
        //    return role.DefaultCatalogEditAccessSettings;
        //}
        //public EnumPrintAccess GetRoleCatalogPrint(IRole role)
        //{
        //    return role.DefaultCatalogPrintAccessSettings;
        //}
        #endregion Roles
    }
}
