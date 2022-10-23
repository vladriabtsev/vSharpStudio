using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public partial interface IPluginGenerator : ITreeConfigNode
    {
        IPlugin ParentPluginI { get; }
        IvPluginGenerator Generator { get; }
    }
}
