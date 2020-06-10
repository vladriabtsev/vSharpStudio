using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public interface IvPluginGeneratorSettings : INotifyPropertyChanged
    {
        /// <summary>
        /// Get protobuf model of settings from MVVM model (json format)
        /// </summary>
        string SettingsAsJson { get; }
        /// <summary>
        /// Generate code for current settings (if applicable for generator)
        /// </summary>
        /// <returns>Generated code</returns>
        string GenerateCode(IConfig cfg);
    }
}
