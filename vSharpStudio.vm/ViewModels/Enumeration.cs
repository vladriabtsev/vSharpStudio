﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Enumeration:{Name,nq} Type:{Enumeration.GetTypeDesc(this),nq}")]
    public partial class Enumeration : ICanAddNode, ICanGoRight, ICanGoLeft, INodeGenSettings, INewAndDeleteion
    {
        public static readonly string DefaultName = "Enumeration";
        [Browsable(false)]
        new public string IconName { get { return "iconEnumerator"; } }
        //protected override string GetNodeIconName() { return "iconEnumerator"; }
        partial void OnInit()
        {
            this.IsIncludableInModels = true;
            this.DataTypeLength = 10;
            this.DataTypeEnum = EnumEnumerationType.INTEGER_VALUE;
            this.ListEnumerationPairs.OnAddingAction = (t) => {
                t.IsNew = true;
            };
            this.ListEnumerationPairs.OnAddedAction = (t) => {
                t.OnAdded();
            };
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
        }

        public static string GetTypeDesc(Enumeration p)
        {
            Contract.Requires(p != null);
            string res = Enum.GetName(typeof(EnumDataType), (int)p.DataTypeEnum);
            // switch (p.DataTypeEnum)
            // {
            //    case Proto.Config.proto_enumeration.Types.EnumEnumerationType.Integer:
            //        break;
            //    case Proto.Config.proto_enumeration.Types.EnumEnumerationType.String:
            //        break;
            // }
            return res;
        }

        #region Tree operations
        public EnumerationPair AddEnumerationPair(string name, string val)
        {
            EnumerationPair node = new EnumerationPair(this) { Name = name, Value = val };
            this.NodeAddNewSubNode(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            EnumerationPair node = null;
            if (node_impl == null)
            {
                node = new EnumerationPair(this);
            }
            else
            {
                node = (EnumerationPair)node_impl;
            }

            this.ListEnumerationPairs.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(EnumerationPair.DefaultName, node, this.ListEnumerationPairs);
            }

            this.SetSelected(node);
            return node;
        }

        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListEnumerations).ListEnumerations.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Enumeration)(this.Parent as GroupListEnumerations).ListEnumerations.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            (this.Parent as GroupListEnumerations).ListEnumerations.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListEnumerations).ListEnumerations.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Enumeration)(this.Parent as GroupListEnumerations).ListEnumerations.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            (this.Parent as GroupListEnumerations).ListEnumerations.MoveDown(this);
            this.SetSelected(this);
        }

        public override void NodeRemove(bool ask = true)
        {
            (this.Parent as GroupListEnumerations).Remove(this);
            this.Parent = null;
        }
        public override void MarkForDeletion()
        {
            this.IsMarkedForDeletion = !this.IsMarkedForDeletion;
        }
        partial void OnIsMarkedForDeletionChanged()
        {
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
            foreach (var t in this.ListEnumerationPairs)
            {
                if (t.IsMarkedForDeletion || t.GetIsHasMarkedForDeletion())
                {
                    this.IsHasMarkedForDeletion = true;
                    return true;
                }
            }
            this.IsHasMarkedForDeletion = false;
            return false;
        }
        public bool GetIsHasNew()
        {
            foreach (var t in this.ListEnumerationPairs)
            {
                if (t.IsHasNew || t.GetIsHasNew())
                {
                    this.IsHasNew = true;
                    return true;
                }
            }
            this.IsHasNew = false;
            return false;
        }

        public override ITreeConfigNode NodeAddClone()
        {
            var node = Enumeration.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            (this.Parent as GroupListEnumerations).Add(node);
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Enumeration(this.Parent);
            (this.Parent as GroupListEnumerations).Add(node);
            this.GetUniqueName(Enumeration.DefaultName, node, (this.Parent as GroupListEnumerations).ListEnumerations);
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        [BrowsableAttribute(false)]
        public List<IEnumerationPair> ListAnnotated
        {
            get
            {
                var cfg = this.GetConfig();
                var diff = new DiffLists<IEnumerationPair>(
                    (cfg.DicNodes[this.Guid] as Enumeration).ListEnumerationPairs,
                    (cfg.PrevStableConfig?.DicNodes[this.Guid] as Enumeration)?.ListEnumerationPairs,
                    (cfg.OldStableConfig?.DicNodes[this.Guid] as Enumeration)?.ListEnumerationPairs);
                return diff.ListAll;
            }
        }
    }
}
