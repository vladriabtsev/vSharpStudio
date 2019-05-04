using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Journal : ICanAddNode, ICanAddSubNode, ICanGoRight, ICanGoLeft
    {
        public static readonly string DefaultName = "Journal";

        #region ITreeNode
        //public string NodeText { get { return this.Name; } }

        #endregion ITreeNode
    }
}
