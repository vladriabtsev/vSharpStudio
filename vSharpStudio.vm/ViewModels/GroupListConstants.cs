using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListConstants.Count,nq}")]
    public partial class GroupListConstants : ITreeModel, ICanAddSubNode, ICanGoRight
    {
        public override IEnumerable<object> GetChildren(object parent)
        {
            return this.ListConstants;
        }

        public override bool HasChildren(object parent)
        {
            return this.ListConstants.Count > 0;
        }

        partial void OnInit()
        {
            this.Name = Defaults.ConstantsGroupName;
            this.IsEditable = false;
        }

        #region Tree operations
        public Constant AddConstant(string name, DataType type = null)
        {
            Constant node = new Constant(this) { Name = name, DataType = new DataType() };
            this.NodeAddNewSubNode(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Constant node = null;
            if (node_impl == null)
            {
                node = new Constant(this);
            }
            else
            {
                node = (Constant)node_impl;
            }

            this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Constant.DefaultName, node, this.ListConstants);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        [BrowsableAttribute(false)]
        public List<IConstant> ListAnnotated
        {
            get
            {
                var cfg = this.GetConfig();
                DiffLists<IConstant> diff = new DiffLists<IConstant>(
                    cfg.OldStableConfig?.IModel.IGroupConstants.IListConstants,
                    cfg.PrevStableConfig?.IModel.IGroupConstants.IListConstants,
                    cfg.IModel.IGroupConstants.IListConstants);
                return diff.ListAll;
            }
        }
    }
}
