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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListEnumerations.Count,nq}")]
    public partial class GroupListEnumerations : ICanAddSubNode, ICanGoRight
    {
        partial void OnInit()
        {
            this.Name = "Enumerations";
            this.IsEditable = false;
        }

        #region Tree operations
        public override ITreeConfigNode NodeAddNewSubNode()
        {
            var node = new Enumeration();
            this.Add(node);
            GetUniqueName(Enumeration.DefaultName, node, this.ListEnumerations);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
