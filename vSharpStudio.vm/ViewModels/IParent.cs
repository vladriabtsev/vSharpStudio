using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    public interface IParent
    {
        ITreeConfigNode Parent { get; set; }
    }
}
