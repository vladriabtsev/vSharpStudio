using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Constant:{Name,nq} Type:{DataType.GetTypeDesc(this.DataType),nq}")]
    public partial class Constant : ICanGoLeft, ICanAddNode, ISetParent
    {
        public static readonly string DefaultName = "Constant";

        partial void OnInit()
        {
        }

        #region IConfigObject
        public void Create()
        {
            GroupListConstants vm = (GroupListConstants)this.Parent;
            int icurr = vm.Children.IndexOf(this);
            vm.Children.Add(new Constant() { Parent = this.Parent });
        }

        public void SetParent(object parent)
        {
            this.Parent = (GroupListConstants)parent;
        }
        #endregion IConfigObject
    }
}
