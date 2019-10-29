using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using FluentValidation;
using ViewModelBase;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    public partial class EnumerationPair
    {
        public static readonly string DefaultName = "Element";
        partial void OnInit()
        {
            this.IsIncludableInModels = true;
        }
    }
}
