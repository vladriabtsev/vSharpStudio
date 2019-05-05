using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    public partial class Journal : ICanAddNode, ICanAddSubNode, ICanGoRight, ICanGoLeft
    {
        public static readonly string DefaultName = "Journal";
    }
}
