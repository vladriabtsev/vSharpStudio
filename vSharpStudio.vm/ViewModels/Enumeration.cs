using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugInfo(),nq}")]
    public partial class Enumeration : ICanAddNode, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNode, IEditableNodeGroup
    {
        partial void OnDebugStringExtend(StringBuilder sb)
        {
            sb.Append(" Type:");
            sb.Append(Enumeration.GetTypeDesc(this));
        }
        [Browsable(false)]
        public GroupListEnumerations ParentGroupListEnumerations { get { Debug.Assert(this.Parent != null); return (GroupListEnumerations)this.Parent; } }
        [Browsable(false)]
        public IGroupListEnumerations ParentGroupListEnumerationsI { get { Debug.Assert(this.Parent != null); return (IGroupListEnumerations)this.Parent; } }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListEnumerations.Children;
        }
        public new ConfigNodesCollection<EnumerationPair> Children { get { return this.ListEnumerationPairs; } }
        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconEnumerator"; } }
        //protected override string GetNodeIconName() { return "iconEnumerator"; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            this._DataTypeLength = 10;
            this._DataTypeEnum = EnumEnumerationType.INTEGER_VALUE;
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
        protected override ConfigNodesCollection<Enumeration>? GetParentCollection() { return this.ParentGroupListEnumerations.ListEnumerations; }
        public void OnAdded()
        {
            this.AddOrRestoreAllAppGenSettingsVmsToNode();
        }
        [Browsable(false)]
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
        public string GetClrValueType()
        {
            string res = "";
            if (this.DataTypeEnum == EnumEnumerationType.BYTE_VALUE)
            {
                res = "byte";
            }
            else if (this.DataTypeEnum == EnumEnumerationType.INTEGER_VALUE)
            {
                res = "int";
            }
            else if (this.DataTypeEnum == EnumEnumerationType.SHORT_VALUE)
            {
                res = "short";
            }
            else if (this.DataTypeEnum == EnumEnumerationType.STRING_VALUE)
            {
                res = "string";
            }
            else
            {
                throw new ArgumentException();
            }
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
        public EnumerationPair AddEnumerationPair(string name, string val, bool isDefault = false, string? guid = null)
        {
            EnumerationPair node = new EnumerationPair(this) { Name = name, Value = val, IsDefault = isDefault };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
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
            node.Parent = this;
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.EnumerationPairName, node, this.ListEnumerationPairs);
            }

            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = Enumeration.Clone(this.ParentGroupListEnumerations, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupListEnumerations.ListEnumerations.Add(node, this);
            this.Name = this.Name + "2";
            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Enumeration(this.ParentGroupListEnumerations);
            this.ParentGroupListEnumerations.ListEnumerations.Add(node, this);
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

        partial void OnDataTypeEnumChanged()
        {
            this.OnPropertyChanged(nameof(this.PropertyDefinitions));
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                nameof(this.Parent),
                nameof(this.Children)
            };
            if (this.DataTypeEnum != EnumEnumerationType.STRING_VALUE)
            {
                lst.Add(nameof(this.DataTypeLength));
            }
            return [.. lst];
        }
    }
}
