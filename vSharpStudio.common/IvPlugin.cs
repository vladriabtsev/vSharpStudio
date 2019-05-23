using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;

namespace vSharpStudio.common
{
    /// <summary>
    /// Main plugin interface for all plugin types of vSharpStudio
    /// </summary>
    [InheritedExport(typeof(IvPlugin))] // metadata was not exported
    public interface IvPlugin
    {
        vPluginTypeEnum PluginType { get; }
        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }
        string Url { get; }
        string Licence { get; }
        // MVVM settings model (if settings == null then empty model will be created)
        INotifyPropertyChanged GetSettingsMvvm(Any settings);
        // current protobuf settings (reflecting all latest changes in MVVM settings model)
        Any Settings { get; }
    }
}
