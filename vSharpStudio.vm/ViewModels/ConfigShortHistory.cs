using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Google.Protobuf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.vm.Migration;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
//using System.Linq.Expressions;
//using System.Linq.Dynamic.Core;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ConfigShortHistory
    {
        partial void OnIsNewChanged();
        partial void OnIsHasNewChanged();
        partial void OnIsHasChangedChanged();
        partial void OnIsMarkedForDeletionChanged();
        partial void OnIsHasMarkedForDeletionChanged();
    }
}
