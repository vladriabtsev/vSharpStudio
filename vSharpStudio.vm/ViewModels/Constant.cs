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
    [DebuggerDisplay("Constant:{Name,nq} Type:{DataType.GetTypeDesc(this.DataType),nq}")]
    public partial class Constant : IDataTypeObject, ICanGoLeft, ICanAddNode, INodeGenSettings, INewAndDeleteion
    {
        public static readonly string DefaultName = "Constant";
        [Browsable(false)]
        new public string IconName { get { return "iconConstant"; } }
        //protected override string GetNodeIconName() { return "iconConstant"; }
        partial void OnInit()
        {
            this.IsIncludableInModels = true;
        }

        public Constant(ITreeConfigNode parent, string name, EnumDataType type, string guidOfType)
            : this(parent)
        {
            this.Name = name;
            this.DataType = new DataType(type, guidOfType);
        }

        public Constant(ITreeConfigNode parent, string name, EnumDataType type, uint? length = null, uint? accuracy = null, bool? isPositive = null)
            : this(parent)
        {
            this.Name = name;
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

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListConstants).ListConstants.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Constant)(this.Parent as GroupListConstants).ListConstants.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            (this.Parent as GroupListConstants).ListConstants.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListConstants).ListConstants.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Constant)(this.Parent as GroupListConstants).ListConstants.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            (this.Parent as GroupListConstants).ListConstants.MoveDown(this);
            this.SetSelected(this);
        }

        public override void NodeRemove(bool ask = true)
        {
            (this.Parent as GroupListConstants).Remove(this);
            this.Parent = null;
        }
        public bool IsHasNew { get { return false; } set { } }
        public bool IsHasMarkedForDeletion { get { return false; } set { } }

        public override void MarkForDeletion()
        {
            this.IsMarkedForDeletion = !this.IsMarkedForDeletion;
        }
        partial void OnIsMarkedForDeletionChanged()
        {
            if (this.IsNotNotifying)
                return;
            if (this.IsMarkedForDeletion)
            {
                (this.Parent as INewAndDeleteion).IsHasMarkedForDeletion = true;
            }
            else
            {
                var p = (this.Parent as INewAndDeleteion);
                p.GetIsHasMarkedForDeletion();
            }
        }
        partial void OnIsNewChanged()
        {
            if (this.IsNotNotifying)
                return;
            if (this.IsNew)
            {
                (this.Parent as INewAndDeleteion).IsHasNew = true;
            }
            else
            {
                var p = (this.Parent as INewAndDeleteion);
                p.GetIsHasNew();
            }
        }
        //partial void OnIsHasMarkedForDeletionChanged()
        //{
        //    if (this.IsNotNotifying)
        //        return;
        //    if (this.IsHasMarkedForDeletion)
        //    {
        //        (this.Parent as INewAndDeleteion).IsHasMarkedForDeletion = true;
        //    }
        //    else
        //    {
        //        var p = (this.Parent as INewAndDeleteion);
        //        p.GetIsHasMarkedForDeletion();
        //    }
        //}
        //partial void OnIsHasNewChanged()
        //{
        //    if (this.IsNotNotifying)
        //        return;
        //    if (this.IsHasNew)
        //    {
        //        (this.Parent as INewAndDeleteion).IsHasNew = true;
        //    }
        //    else
        //    {
        //        var p = (this.Parent as INewAndDeleteion);
        //        p.GetIsHasNew();
        //    }
        //}
        public bool GetIsHasMarkedForDeletion()
        {
            //foreach (var t in this.ListDocuments)
            //{
            //    if (t.IsMarkedForDeletion || t.GetIsHasMarkedForDeletion())
            //    {
            //        this.IsHasMarkedForDeletion = true;
            //        return true;
            //    }
            //}
            this.IsHasMarkedForDeletion = false;
            return false;
        }
        public bool GetIsHasNew()
        {
            //foreach (var t in this.ListDocuments)
            //{
            //    if (t.IsHasNew || t.GetIsHasNew())
            //    {
            //        this.IsHasNew = true;
            //        return true;
            //    }
            //}
            this.IsHasNew = false;
            return false;
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = Constant.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            (this.Parent as GroupListConstants).Add(node);
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Constant(this.Parent);
            (this.Parent as GroupListConstants).Add(node);
            this.GetUniqueName(Constant.DefaultName, node, (this.Parent as GroupListConstants).ListConstants);
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
