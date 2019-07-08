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
        public void AddPropertiesTab(PropertiesTab node)
        {
            this.NodeAddNewSubNode(node);
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            PropertiesTab node = null;
            if (node_impl == null)
                node = new PropertiesTab();
            else
                node = (PropertiesTab)node_impl;
            this.Add(node);
            if (node_impl == null)
                GetUniqueName(PropertiesTab.DefaultName, node, this.ListPropertiesTabs);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
