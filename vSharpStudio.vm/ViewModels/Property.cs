using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Property:{Name,nq} Type:{DataType.GetTypeDesc(this.DataType),nq}")]
    public partial class Property : IDataTypeObject, ICanAddNode, ICanGoLeft, INodeGenSettings, IEditableNode
    {
        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return new List<ITreeConfigNode>();
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as GroupListProperties;
            if (p == null)
                return new List<ITreeConfigNode>();
            return p.Children;
        }
        public override bool HasChildren()
        {
            return false;
        }
        #endregion ITree

        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }
        public static readonly string DefaultName = "Property";
        [Browsable(false)]
        new public string IconName { get { return "iconProperty"; } }
        //protected override string GetNodeIconName() { return "iconProperty"; }
        partial void OnInit()
        {
            this.IsIncludableInModels = true;
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
        }
        public Property(ITreeConfigNode parent, string guid, string name, DataType dataType)
            : this(parent)
        {
            this.Guid = guid;
            this.Name = name;
            this.DataType = dataType;
        }
        public Property(ITreeConfigNode parent, string name, EnumDataType type, string guidOfType)
            : this(parent)
        {
            this.Name = name;
            this.DataType = new DataType(type, guidOfType);
        }
        public Property(ITreeConfigNode parent, string name, EnumDataType type, uint? length = null, uint? accuracy = null, bool? isPositive = null)
            : this(parent)
        {
            this.Name = name;
            this.DataType = new DataType(type, length, accuracy);
        }

        public string ClrType
        {
            get { return this.DataType.ClrTypeName; }
        }

        public string ProtoType
        {
            get { return this.DataType.ProtoType; }
        }
        public IDataType IDataType { get { return this._DataType; } }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListProperties).ListProperties.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Property)(this.Parent as GroupListProperties).ListProperties.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            (this.Parent as GroupListProperties).ListProperties.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListProperties).ListProperties.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Property)(this.Parent as GroupListProperties).ListProperties.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            (this.Parent as GroupListProperties).ListProperties.MoveDown(this);
            this.SetSelected(this);
        }

        public void NodeRemove(bool ask = true)
        {
            (this.Parent as GroupListProperties).Remove(this);
            this.Parent = null;
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = Property.Clone(this.Parent, this, true, true);
            (this.Parent as GroupListProperties).Add(node);
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            if (!(this.Parent is GroupListProperties))
            {
                throw new Exception();
            }

            var node = new Property(this.Parent);
            var glp = (this.Parent as GroupListProperties);
            glp.Add(node);
            // TODO can be more economical?
            if (glp.LastGenPosition == 0)
            {
                glp.LastGenPosition = 1;
            }

            glp.LastGenPosition++;
            node.Position = glp.LastGenPosition;
            this.GetUniqueName(Property.DefaultName, node, (this.Parent as GroupListProperties).ListProperties);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            var p = this.Parent as GroupListProperties;
            p.ListProperties.Remove(this);
        }
        #endregion Tree operations
    }
}
