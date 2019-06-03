﻿using System;
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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListProperties.Count,nq}")]
    public partial class GroupListProperties : ICanAddSubNode, ICanGoRight, ICanGoLeft
    {
        partial void OnInit()
        {
            if (this.Parent is GroupDocuments)
                this.Name = "Shared";
            else
                this.Name = "Properties";
            this.IsEditable = false;
        }

        protected override void OnParentChanged()
        {
            if (this.Parent is GroupDocuments)
                this.Name = "Shared";
            else
                this.Name = "Properties";
        }
        protected override void OnInitFromDto()
        {
            if (this.Parent is GroupDocuments)
                this.Name = "Shared";
            else
                this.Name = "Properties";
        }

        #region Tree operations
        public override ITreeConfigNode NodeAddNewSubNode()
        {
            var node = new Property();
            this.Add(node);
            GetUniqueName(Property.DefaultName, node, this.ListProperties);
            SetSelected(node);
            return node;
        }
        #endregion Tree operations
    }
}
