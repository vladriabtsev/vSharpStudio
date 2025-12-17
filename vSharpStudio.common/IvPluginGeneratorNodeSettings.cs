using System.Collections.Generic;
using System.ComponentModel;

namespace vSharpStudio.common
{
    /// <summary>
    /// Plugin generator settings attached to each model node
    /// </summary>
    public interface IvPluginGeneratorNodeSettings : IvPluginGeneratorValidatableSettings, INotifyPropertyChanged
    {
        //string Name { get; }
        /// <summary>
        /// Node selection path.
        /// Can contains list path templates separated by semicolon: "temp1;temp2"
        /// Template "Property" will find any property in all appropriate types (in "Document" for example)
        /// Template "Catalog.*.Property" will find any property only in "Catalog"
        /// Template "Group" will find any node which type name contains "Group"
        /// If template contains "." search is applicable for right side of node full type name
        /// </summary>
        //string SearchPathInModel { get; }

        /// <summary>
        /// Get JSON representation from a settings VM
        /// </summary>
        string SettingsAsJson { get; }
        string SettingsAsJsonDefault { get; }
        /// <summary>
        /// Control settings property visibility in node editor by plugin generator node settings.
        /// </summary>
        Dictionary<string, string?>? DicNodeExcludedProperties { get; }
        /// <summary>
        /// Application project generator GUID.
        /// </summary>
        string AppProjectGeneratorGuid { get; set; }
        //string[] GetListPropertiesToHideOnNodeSettings(ITreeConfigNode modelNode);
    }
}
