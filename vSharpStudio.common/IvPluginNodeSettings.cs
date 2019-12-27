using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public interface IvPluginNodeSettings : INotifyPropertyChanged
    {
        string Guid { get; }
        /// <summary>
        /// Node selection path.
        /// Can contains list path templates separated by semicolon: "temp1;temp2"
        /// Template "Property" will find any property in all appropriate types (in "Document" for example)
        /// Template "Catalog.*.Property" will find any property only in "Catalog"
        /// Template "Group" will find any node which type name contains "Group"
        /// If template contains "." search is applicable for right side of node full type name
        /// </summary>
        string SearchPathInModel { get; }
        /// <summary>
        /// Get s Settings VM from JSON string
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        IvPluginNodeSettings GetAppGenerationNodeSettingsVm(string settings);
        /// <summary>
        /// Get JSON representation from a settings VM
        /// </summary>
        string SettingsAsJson { get; }
    }
}
