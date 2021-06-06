using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public partial interface IPlugin : ITreeConfigNode
    {
        IvPlugin VPlugin { get; }
    }
}
