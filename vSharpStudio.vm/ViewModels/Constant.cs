using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Constant:{Name,nq} Type:{DataType.GetTypeDesc(this.DataType),nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class Constant : IDataTypeObject, ICanGoLeft, ICanAddNode, INodeGenSettings, IEditableNode
    {
        [Browsable(false)]
        public GroupListConstants ParentGroupListConstants { get { Debug.Assert(this.Parent != null); return (GroupListConstants)this.Parent; } }
        [Browsable(false)]
        public IGroupListConstants ParentGroupListConstantsI { get { Debug.Assert(this.Parent != null); return (IGroupListConstants)this.Parent; } }
        [Browsable(false)]
        public bool IsPKey => false;
        [Browsable(false)]
        public bool IsRefParent => false;

        [Browsable(false)]
        // Can be used by a generator to keep calculated property data
        public object? Tag { get; set; }
        [Browsable(false)]
        public static IConfig? Config { get; set; }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return new ConfigNodesCollection<ITreeConfigNodeSortable>(this);
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListConstants.Children;
        }
        public override bool HasChildren()
        {
            return false;
        }
        #endregion ITree

        [Browsable(false)]
        new public string IconName { get { return "iconConstant"; } }
        //protected override string GetNodeIconName() { return "iconConstant"; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this.DataType.PropertyChanged += DataType_PropertyChanged;
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

        private void DataType_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            this.Tag = null;
        }

        public Constant(ITreeConfigNode parent, string name, EnumDataType type, string guidOfType)
            : this(parent)
        {
            this._Name = name;
            this.DataType = new DataType(this, type, guidOfType);
        }

        public Constant(ITreeConfigNode parent, string name, EnumDataType type, uint? length = null, uint? accuracy = null, bool? isPositive = null)
            : this(parent)
        {
            this._Name = name;
            this.DataType = new DataType(this, type, length, accuracy);
        }

        public IDataType IDataType { get { return this._DataType; } }

        #region IConfigObject
        // public void Create()
        // {
        //    GroupListConstants vm = (GroupListConstants)this.Parent;
        //    int icurr = vm.Children.IndexOf(this);
        //    vm.Children.Add(new Constant() { Parent = this.Parent });
        // }

        #endregion IConfigObject

        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
        }
        //[BrowsableAttribute(false)]
        //public string DefaultValue
        //{
        //    get
        //    {
        //        if (this.DataType.IsNullable)
        //            return "null";
        //        return this.DataType.DefaultValue;
        //    }
        //}
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
        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListConstants.ListConstants.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Constant)this.ParentGroupListConstants.ListConstants.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupListConstants.ListConstants.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListConstants.ListConstants.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Constant)this.ParentGroupListConstants.ListConstants.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupListConstants.ListConstants.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = Constant.Clone(this.ParentGroupListConstants, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupListConstants.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Constant(this.ParentGroupListConstants);
            this.ParentGroupListConstants.Add(node);
            this.GetUniqueName(Defaults.ConstantName, node, this.ParentGroupListConstants.ListConstants);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListConstants.ListConstants.Remove(this);
        }
        #endregion Tree operations
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            lst.Add(this.GetPropertyName(() => this.Parent));
            return lst.ToArray();
        }
    }
}
