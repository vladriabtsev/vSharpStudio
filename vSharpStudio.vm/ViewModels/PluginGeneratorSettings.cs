﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("PluginGeneratorSettings:{Name,nq} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class PluginGeneratorSettings : IParent
    {
        public IvPluginGeneratorSettings? SettingsVm { get; set; }
        [Browsable(false)]
        protected override void OnIsChangedChanged()
        {
            if (this.Parent != null && this.IsChanged)
                this.Parent.IsChanged = true;
        }
    }
}
