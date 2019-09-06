using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListAppSolutions.Count,nq}")]
    public partial class GroupListAppSolutions : ICanAddSubNode, ICanGoRight, ICanGoLeft
    {
        partial void OnInit()
        {
            this.Name = "Apps";
            this.IsEditable = false;
            this.DefaultDb.Parent = this;
        }

        #region Tree operations
        public void AddAppSolution(AppSolution node)
        {
            this.NodeAddNewSubNode(node);
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            AppSolution node = null;
            if (node_impl == null)
                node = new AppSolution();
            else
                node = (AppSolution)node_impl;
            this.Add(node);
            if (node_impl == null)
                GetUniqueName(AppSolution.DefaultName, node, this.ListAppSolutions);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
