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
        Guid Guid { get; }
        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }
        string Url { get; }
        string Licence { get; }
        List<IvPluginCodeGenerator> ListGenerators { get; }
    }
    public interface IvPluginCodeGenerator
    {
        Guid Guid { get; }
        /// <summary>
        /// Plugin code generator name
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Default setings name
        /// </summary>
        string DefaultSettingsName { get; }
        string Description { get; }
        vPluginTypeEnum PluginType { get; }
        // MVVM settings model (if settings == null then empty model will be created)
        /// <summary>
        /// MVVM model for editing settings.
        /// INotifyPropertyChanged is minimum requirements for implemented interfaces.
        /// For validation settings INotifyDataErrorInfo is a minimum. IValidatableWithSeverity is recomended.
        /// </summary>
        /// <param name="settings, json format"></param>
        /// <returns>Stored outside plugin settings will be converted to a view model.</returns>
        IvPluginSettingsVM GetSettingsMvvm(string settings);
    }
    public interface IvPluginSettingsVM : IParent, INotifyPropertyChanged
    {
        /// <summary>
        /// Get protobuf model of settings from MVVM model (json format)
        /// </summary>
        string Settings { get; }
        /// <summary>
        /// Generate code for current settings
        /// </summary>
        /// <returns>Generated code</returns>
        string GenerateCode();
    }
}
