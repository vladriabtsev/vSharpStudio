﻿using System;
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
    [DebuggerDisplay("Constant:{Name,nq} Type:{DataType.GetTypeDesc(this.DataType),nq}")]
    public partial class Constant : IDataTypeObject, ICanGoLeft, ICanAddNode, INodeGenSettings, IEditableNode
    {
        [BrowsableAttribute(false)]
        public GroupListConstants ParentGroupListConstants { get { return (GroupListConstants)this.Parent; } }
        [BrowsableAttribute(false)]
        public IGroupListConstants ParentGroupListConstantsI { get { return (IGroupListConstants)this.Parent; } }
        [BrowsableAttribute(false)]
        public bool IsPKey => false;
        [BrowsableAttribute(false)]
        public bool IsRefParent => false;

        [Browsable(false)]
        // Can be used by a generator to keep calculated property data
        public object Tag { get; set; }
        [Browsable(false)]
        public static IConfig Config { get; set; }

        #region ITree
        public ObservableCollection<ITreeConfigNode> Children { get; private set; }
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return new List<ITreeConfigNode>();
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            return this.ParentGroupListConstants.Children;
        }
        public override bool HasChildren()
        {
            return false;
        }
        #endregion ITree

        public static readonly string DefaultName = "Constant";
        [Browsable(false)]
        new public string IconName { get { return "iconConstant"; } }
        //protected override string GetNodeIconName() { return "iconConstant"; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            this.DataType.Parent = this;
            this.DataType.PropertyChanged += DataType_PropertyChanged;
        }

        private void DataType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Tag = null;
        }

        public Constant(ITreeConfigNode parent, string name, EnumDataType type, string guidOfType)
            : this(parent)
        {
            this._Name = name;
            this.DataType = new DataType(type, guidOfType);
        }

        public Constant(ITreeConfigNode parent, string name, EnumDataType type, uint? length = null, uint? accuracy = null, bool? isPositive = null)
            : this(parent)
        {
            this._Name = name;
            this.DataType = new DataType(type, length, accuracy);
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
            var node = Constant.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupListConstants.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Constant(this.Parent);
            this.ParentGroupListConstants.Add(node);
            this.GetUniqueName(Constant.DefaultName, node, this.ParentGroupListConstants.ListConstants);
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
            lst.Add(this.GetPropertyName(() => this.Children));
            return lst.ToArray();
        }
    }
}
