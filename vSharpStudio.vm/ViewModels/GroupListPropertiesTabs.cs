using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListPropertiesTabs.Count,nq}")]
    public partial class GroupListPropertiesTabs : ICanAddSubNode, ICanGoRight, ICanGoLeft
    {
        partial void OnInit()
        {
            this.Name = "Tabs";
            this.IsEditable = false;
        }

        #region Tree operations
        public override ITreeConfigNode NodeAddNewSubNode()
        {
            var node = new PropertiesTab();
            this.Add(node);
            GetUniqueName(PropertiesTab.DefaultName, node, this.ListPropertiesTabs);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
