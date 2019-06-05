using System;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.common
{
    public interface IParent
    {
        ITreeConfigNode Parent { get; set; }
    }
}
