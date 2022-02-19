using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Numerics;
using System.Text;
using System.Windows;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?view=netframework-4.7.2
    public partial class FormCatalogEditSettings : IParent
    {
        partial void OnInit()
        {
            //this.IsUseCode = true;
            //this.IsUseName = true;
            //this.IsUseFolderCode = true;
            //this.IsUseFolderName = true;
        }
        [BrowsableAttribute(false)]
        public ITreeConfigNode Parent { get; set; }
        protected override void OnIsChangedChanged()
        {
            if (this.Parent != null && this.IsChanged)
                this.Parent.IsChanged = true;
        }
    }
}
