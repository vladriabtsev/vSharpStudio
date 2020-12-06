using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public partial interface IAppProjectGenerator : ITreeConfigNode
    {
        IvPluginDbGenerator PluginDbGenerator { get; }
        IvPluginGeneratorSettings DynamicGeneratorSettings { get; }
    }
}
