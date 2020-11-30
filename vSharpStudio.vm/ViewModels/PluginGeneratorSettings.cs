using System;
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
    [DebuggerDisplay("PluginGeneratorSettings:{Name,nq}")]
    public partial class PluginGeneratorSettings : ISortingValue
    {
        public IvPluginGeneratorSettings SettingsVm { get; set; }
        partial void OnIsNewChanged();
        partial void OnIsHasNewChanged();
        partial void OnIsMarkedForDeletionChanged();
        partial void OnIsHasMarkedForDeletionChanged();
    }
}
