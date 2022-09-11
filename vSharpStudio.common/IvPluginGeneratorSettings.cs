using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public interface IvPluginGeneratorSettings : IvPluginGeneratorValidatableSettings, INotifyPropertyChanged
    {
        [BrowsableAttribute(false)]
        IvPluginGenerator Generator { get; set; }
        [BrowsableAttribute(false)]
        IAppProjectGenerator Parent { get; set; }
        /// <summary>
        /// Get protobuf model of settings from MVVM model (json format)
        /// </summary>
        string SettingsAsJson { get; }
        /// <summary>
        /// Generate code for current settings (if applicable for generator)
        /// </summary>
        /// <returns>Generated code</returns>
        string GenerateCode(IConfig cfg, IAppSolution sln, IAppProject prj, IAppProjectGenerator prjGen);
    }
}
