using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;

// https://www.codeproject.com/Articles/376033/From-Zero-to-Proficient-with-MEF
// https://docs.microsoft.com/en-us/dotnet/framework/mef/

namespace vSharpStudio.common
{
    /// <summary>
    /// Main plugin interface for all plugin types of vSharpStudio
    /// </summary>
    [InheritedExport(typeof(IvPlugin))] // metadata was not exported
    public interface IvPlugin //: IvPluginGroup
    {
        /// <summary>
        /// Plugin guid
        /// </summary>
        string Guid { get; }
        /// <summary>
        /// Plugin version
        /// </summary>
        string Version { get; }
        /// <summary>
        /// Plugin name
        /// </summary>
        string Name { get; }
        string NameUI { get; }
        /// <summary>
        /// Plugin description
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Plugin author
        /// </summary>
        string Author { get; }
        /// <summary>
        /// Download link
        /// </summary>
        string Url { get; }
        /// <summary>
        /// Licence
        /// </summary>
        string Licence { get; }
        /// <summary>
        /// If there are same parameters for several plugins, such plugins create group of plugins.
        /// Same parameters for several plugins are combined in class with IvPluginGroupSolutionSettings interface.
        /// </summary>
        /// <paramref name="settings">
        /// Json representation of plugin group setting. If this parameter is null, default result will be returned. 
        /// </paramref>
        /// <returns>
        /// Returns null if group parameters are not supported.
        /// If supported, return class of group settings
        /// </returns>
        IvPluginGroupSolutionSettings GetPluginGroupSolutionSettingsVmFromJson(string settings);
        /// <summary>
        /// Plugin generators
        /// </summary>
        List<IvPluginGenerator> ListGenerators { get; }
    }
}
