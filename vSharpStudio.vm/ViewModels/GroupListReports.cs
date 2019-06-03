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
        public override ITreeConfigNode NodeAddNewSubNode()
        {
            var node = new Report();
            this.Add(node);
            GetUniqueName(Report.DefaultName, node, this.ListReports);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
