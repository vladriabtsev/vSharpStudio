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
    public interface IvPlugin
    {
        string Guid { get; }

        string Version { get; }

        string Name { get; }

        string NameUI { get; }

        string Description { get; }

        string Author { get; }

        string Url { get; }

        string Licence { get; }

        List<IvPluginGenerator> ListGenerators { get; }
    }
}
