using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vSharpStudio.common.DiffModel;

namespace vSharpStudio.common
{
    public interface IvPluginGenerator
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        string Guid { get; }
        /// <summary>
        /// Short Plugin code generator name (without spaces)
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Short Plugin code generator name to show in a UI
        /// </summary>
        string NameUi { get; }
        /// <summary>
        /// Description of generator
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Type of generator
        /// </summary>
        vPluginLayerTypeEnum PluginGeneratorType { get; }
        IvPluginGeneratorNodeSettings GetGenerationNodeSettingsVmFromJson(string settings, ITreeConfigNode node);
        // MVVM settings model (if settings == null then empty model will be created)
        /// <summary>
        /// MVVM model for editing settings.
        /// INotifyPropertyChanged is minimum requirements for implemented interfaces.
        /// For validation settings INotifyDataErrorInfo is a minimum. IValidatableWithSeverity is recomended.
        /// </summary>
        /// <param name="settings, json format"></param>
        /// <returns>Stored outside plugin settings will be converted to a view model.</returns>
        IvPluginGeneratorSettings GetAppGenerationSettingsVmFromJson(string settings);
        /// <summary>
        /// Default setings name
        /// </summary>
        string DefaultSettingsName { get; }
        ITreeConfigNode Parent { get; set; }
    }
}
