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
    public interface IvPluginDb : IvPlugin
    {
        string ProviderName { get; }
    }
}
