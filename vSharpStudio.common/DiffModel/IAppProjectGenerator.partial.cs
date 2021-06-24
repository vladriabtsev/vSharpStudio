using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public partial interface IAppProjectGenerator : ITreeConfigNode
    {
        IAppProject AppProject { get; }
        IvPluginGenerator PluginGenerator { get; }
        IvPluginDbGenerator PluginDbGenerator { get; }
        IvPluginGeneratorSettings DynamicGeneratorSettings { get; }
        IvPlugin Plugin { get; }
    }
}
