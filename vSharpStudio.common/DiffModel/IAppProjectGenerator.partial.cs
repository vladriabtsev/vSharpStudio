using System;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IAppProjectGenerator : ITreeConfigNodeSortable
    {
        IAppProject ParentAppProjectI { get; }
        IvPluginGenerator? PluginGenerator { get; }
        IvPluginDbGenerator? PluginDbGenerator { get; }
        IvPluginGeneratorSettings? DynamicGeneratorSettings { get; }
        IvPlugin Plugin { get; }
    }
}
