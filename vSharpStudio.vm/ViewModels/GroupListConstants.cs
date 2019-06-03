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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListConstants.Count,nq}")]
    public partial class GroupListConstants : ICanAddSubNode, ICanGoRight
    {
        partial void OnInit()
        {
            this.Name = "Constants";
            this.IsEditable = false;
        }

        #region Tree operations
        public override ITreeConfigNode NodeAddNewSubNode()
        {
            var node = new Constant();
            this.Add(node);
            GetUniqueName(Constant.DefaultName, node, this.ListConstants);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
