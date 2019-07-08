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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListReports.Count,nq}")]
    public partial class GroupListReports : ICanAddSubNode, ICanGoRight, ICanGoLeft
    {
        partial void OnInit()
        {
            this.Name = "Reports";
            this.IsEditable = false;
        }

        #region Tree operations
        public void AddReport(Report node)
        {
            this.NodeAddNewSubNode(node);
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Report node = null;
            if (node_impl == null)
                node = new Report();
            else
                node = (Report)node_impl;
            this.Add(node);
            if (node_impl == null)
                GetUniqueName(Report.DefaultName, node, this.ListReports);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
