using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Enumeration:{Name,nq} Type:{Enumeration.GetTypeDesc(this),nq}")]
    public partial class Enumeration : ICanAddNode, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNode, IEditableNodeGroup
    {
        [BrowsableAttribute(false)]
        public GroupListEnumerations ParentGroupListEnumerations { get { Debug.Assert(this.Parent != null); return (GroupListEnumerations)this.Parent; } }
        [BrowsableAttribute(false)]
        public IGroupListEnumerations ParentGroupListEnumerationsI { get { Debug.Assert(this.Parent != null); return (IGroupListEnumerations)this.Parent; } }

        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            return this.ParentGroupListEnumerations.Children;
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ConfigNodesCollection<EnumerationPair> Children { get { return this.ListEnumerationPairs; } }

        [Browsable(false)]
        new public string IconName { get { return "iconEnumerator"; } }
        //protected override string GetNodeIconName() { return "iconEnumerator"; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            this.DataTypeLength = 10;
            this.DataTypeEnum = EnumEnumerationType.INTEGER_VALUE;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            this.ListEnumerationPairs.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListEnumerationPairs.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListEnumerationPairs.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListEnumerationPairs.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
        }
        [BrowsableAttribute(false)]
        public string DefaultValue
        {
            get
            {
                if (this.ListEnumerationPairs.Count == 0)
                    return "Enum" + this.Name + ".";
                EnumerationPair? tt = null;
                foreach (var t in this.ListEnumerationPairs)
                {
                    if (t.IsDefault)
                    {
                        tt = t;
                        break;
                    }
                }
                if (tt == null)
                    tt = this.ListEnumerationPairs[0];
                return "Enum" + this.Name + "." + tt.Name.ToUpper();
            }
        }
        public static string GetTypeDesc(Enumeration p)
        {
            Debug.Assert(p != null);
            string res = Enum.GetName(typeof(EnumDataType), (int)p.DataTypeEnum)!;
            Debug.Assert(res != null);
            // switch (p.DataTypeEnum)
            // {
            //    case Proto.Config.proto_enumeration.Types.EnumEnumerationType.Integer:
            //        break;
            //    case Proto.Config.proto_enumeration.Types.EnumEnumerationType.String:
            //        break;
            // }
            return res;
        }
        public string GetClrBase()
        {
            string res = "";
            if (this.DataTypeEnum == EnumEnumerationType.BYTE_VALUE)
            {
                res = " : byte";
            }
            else if (this.DataTypeEnum == EnumEnumerationType.INTEGER_VALUE)
            {
                res = " : int";
            }
            else if (this.DataTypeEnum == EnumEnumerationType.SHORT_VALUE)
            {
                res = " : short";
            }
            else
            {
                throw new ArgumentException();
            }
            return res;
        }

        #region Tree operations
        public EnumerationPair AddEnumerationPair(string name, string val, bool isDefault = false)
        {
            EnumerationPair node = new EnumerationPair(this) { Name = name, Value = val, IsDefault = isDefault };
            this.NodeAddNewSubNode(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            EnumerationPair node = null!;
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
                this.GetUniqueName(Defaults.EnumerationPairName, node, this.ListEnumerationPairs);
            }

            this.SetSelected(node);
            return node;
        }

        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListEnumerations.ListEnumerations.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Enumeration)this.ParentGroupListEnumerations.ListEnumerations.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupListEnumerations.ListEnumerations.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListEnumerations.ListEnumerations.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Enumeration)this.ParentGroupListEnumerations.ListEnumerations.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupListEnumerations.ListEnumerations.MoveDown(this);
            this.SetSelected(this);
        }

        public override ITreeConfigNode NodeAddClone()
        {
            var node = Enumeration.Clone(this.Parent, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupListEnumerations.Add(node);
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Enumeration(this.Parent);
            this.ParentGroupListEnumerations.Add(node);
            this.GetUniqueName(Defaults.EnumerationName, node, this.ParentGroupListEnumerations.ListEnumerations);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListEnumerations.ListEnumerations.Remove(this);
        }

        public bool CanAddSubNode()
        {
            return true;
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
