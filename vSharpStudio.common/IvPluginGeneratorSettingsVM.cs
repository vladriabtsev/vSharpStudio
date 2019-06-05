using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public interface IvPluginGeneratorSettingsVM : IParent, INotifyPropertyChanged
    {
        /// <summary>
        /// Get protobuf model of settings from MVVM model (json format)
        /// </summary>
        string Settings { get; }
        /// <summary>
        /// Generate code for current settings (if applicable for generator)
        /// </summary>
        /// <returns>Generated code</returns>
        string GenerateCode();
    }
}
