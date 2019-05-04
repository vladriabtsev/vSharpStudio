using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using ViewModelBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Constant:{Name,nq} Type:{DataType.GetTypeDesc(this.DataType),nq}")]
    public partial class Constant : ICanNotRight
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
        #endregion IConfigObject

        #region ITreeNode
        //        public string NodeText { get { return this.Name; } }

        #endregion ITreeNode
    }
}
