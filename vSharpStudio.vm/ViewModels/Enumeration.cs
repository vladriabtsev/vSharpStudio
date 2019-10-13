using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Enumeration:{Name,nq} Type:{Enumeration.GetTypeDesc(this),nq}")]
    public partial class Enumeration : ICanAddNode, ICanGoRight, ICanGoLeft
    {
        public static readonly string DefaultName = "Enumeration";
        partial void OnInit()
        {
            this.DataTypeLength = 10;
            this.DataTypeEnum = EnumEnumerationType.INTEGER_VALUE;
        }
        public static string GetTypeDesc(Enumeration p)
        {
            string res = Enum.GetName(typeof(EnumDataType), (int)p.DataTypeEnum);
            //switch (p.DataTypeEnum)
            //{
            //    case Proto.Config.proto_enumeration.Types.EnumEnumerationType.Integer:
            //        break;
            //    case Proto.Config.proto_enumeration.Types.EnumEnumerationType.String:
            //        break;
            //}
            return res;
        }
        [PropertyOrderAttribute(11)]
        [Editor(typeof(EditorObjectModels), typeof(EditorObjectModels))]
        public bool Models { get; set; }

        #region Tree operations
        public void AddEnumerationPair(EnumerationPair node)
        {
            this.NodeAddNewSubNode(node);
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            EnumerationPair node = null;
            if (node_impl == null)
                node = new EnumerationPair();
            else
                node = (EnumerationPair)node_impl;
            this.ListEnumerationPairs.Add(node);
            if (node_impl == null)
                GetUniqueName(EnumerationPair.DefaultName, node, this.ListEnumerationPairs);
            SetSelected(node);
            return node;
        }
        public override bool NodeCanUp()
        {
            if (NodeCanAddClone())
            {
                if ((this.Parent as GroupListEnumerations).ListEnumerations.CanUp(this))
                    return true;
            }
            return false;
        }
        public override void NodeUp()
        {
            var prev = (Enumeration)(this.Parent as GroupListEnumerations).ListEnumerations.GetPrev(this);
            if (prev == null)
                return;
            SetSelected(prev);
        }
        public override void NodeMoveUp()
        {
            (this.Parent as GroupListEnumerations).ListEnumerations.MoveUp(this);
            SetSelected(this);
        }
        public override bool NodeCanDown()
        {
            if (NodeCanAddClone())
            {
                if ((this.Parent as GroupListEnumerations).ListEnumerations.CanDown(this))
                    return true;
            }
            return false;
        }
        public override void NodeDown()
        {
            var next = (Enumeration)(this.Parent as GroupListEnumerations).ListEnumerations.GetNext(this);
            if (next == null)
                return;
            SetSelected(next);
        }
        public override void NodeMoveDown()
        {
            (this.Parent as GroupListEnumerations).ListEnumerations.MoveDown(this);
            SetSelected(this);
        }
        public override void NodeRemove()
        {
            (this.Parent as GroupListEnumerations).Remove(this);
            this.Parent = null;
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = Enumeration.Clone(this, true, true);
            node.Parent = this.Parent;
            (this.Parent as GroupListEnumerations).Add(node);
            this.Name = this.Name + "2";
            SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Enumeration();
            (this.Parent as GroupListEnumerations).Add(node);
            GetUniqueName(Enumeration.DefaultName, node, (this.Parent as GroupListEnumerations).ListEnumerations);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
