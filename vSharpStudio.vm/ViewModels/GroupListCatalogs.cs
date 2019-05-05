using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{Children.Count,nq}")]
    public partial class GroupListCatalogs : IChildren, ICanAddSubNode, ICanGoRight
    {
        partial void OnInit()
        {
            this.Name = "Catalogs";
        }
        protected override void OnInitFromDto()
        {
            if (this.Parent is Catalog)
                this.NameUi = "Sub Catalogs";
        }
    }
}
