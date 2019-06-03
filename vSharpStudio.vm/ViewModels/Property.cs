﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Property:{Name,nq} Type:{DataType.GetTypeDesc(this.DataType),nq}")]
    public partial class Property : ICanAddNode, ICanGoLeft
    {
        public static readonly string DefaultName = "Property";
        partial void OnInit()
        {
        }
        public Property(string name, EnumDataType type, string guidOfType) : this()
        {
            this.Name = name;
            this.DataType = new DataType(type, guidOfType);
        }
        public Property(string name, EnumDataType type, uint? length = null, uint? accuracy = null, bool? isPositive = null) : this()
        {
            this.Name = name;
            this.DataType = new DataType(type, length, accuracy);
        }
        public string ClrType { get { return this.DataType.ClrType; } }
        public string ProtoType { get { return this.DataType.ProtoType; } }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (NodeCanAddClone())
            {
                if ((this.Parent as GroupListProperties).ListProperties.CanUp(this))
                    return true;
            }
            return false;
        }
        public override void NodeUp()
        {
            var prev = (Property)(this.Parent as GroupListProperties).ListProperties.GetPrev(this);
            if (prev == null)
                return;
            SetSelected(prev);
        }
        public override void NodeMoveUp()
        {
            (this.Parent as GroupListProperties).ListProperties.MoveUp(this);
            SetSelected(this);
        }
        public override bool NodeCanDown()
        {
            if (NodeCanAddClone())
            {
                if ((this.Parent as GroupListProperties).ListProperties.CanDown(this))
                    return true;
            }
            return false;
        }
        public override void NodeDown()
        {
            var next = (Property)(this.Parent as GroupListProperties).ListProperties.GetNext(this);
            if (next == null)
                return;
            SetSelected(next);
        }
        public override void NodeMoveDown()
        {
            (this.Parent as GroupListProperties).ListProperties.MoveDown(this);
            SetSelected(this);
        }
        public override void NodeRemove()
        {
            (this.Parent as GroupListProperties).Remove(this);
            this.Parent = null;
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = Property.Clone(this, true, true);
            node.Parent = this.Parent;
            (this.Parent as GroupListProperties).Add(node);
            this.Name = this.Name + "2";
            SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            if (!(this.Parent is GroupListProperties))
                throw new Exception();
            var node = new Property();
            (this.Parent as GroupListProperties).Add(node);
            GetUniqueName(Property.DefaultName, node, (this.Parent as GroupListProperties).ListProperties);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations

    }
}
