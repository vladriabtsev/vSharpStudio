using System.ComponentModel;

namespace vSharpStudio.common
{
    /// <summary>
    /// Plugin generator settings
    /// </summary>
    public interface IvPluginGeneratorSettings : IvPluginGeneratorValidatableSettings, INotifyPropertyChanged
    {
        [Browsable(false)]
        IvPluginGenerator? Generator { get; set; }
        [Browsable(false)]
        IAppProjectGenerator? ParentAppProjectGenerator { get; } // set; }
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
