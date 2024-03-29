﻿using System;
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
        /// Called for each generator before starting loop of real code generation
        /// Usefull if generator need special initialization when generator can be used more than once in the loop of code generation
        /// </summary>
        void Init();
        /// <summary>
        /// Unique identifier of generator
        /// </summary>
        string Guid { get; }
        /// <summary>
        /// Unique identifier for group of generators. Generators wiil applied by groups
        /// </summary>
        //string GroupGeneratorsGuid { get; }
        /// <summary>
        /// Unique identifier solution parameters for group of generators. Generators wiil applied by groups
        /// </summary>
        string SolutionParametersGuid { get; }
        /// <summary>
        /// Unique identifier project parameters for group of generators. Generators wiil applied by groups
        /// </summary>
        string ProjectParametersGuid { get; }
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
        /// <summary>
        /// MVVM model for editing node settings for generator
        /// </summary>
        /// <param name="node">Configuration node</param>
        /// <param name="settings">Json representation of node generator settings</param>
        /// <returns></returns>
        IvPluginGeneratorNodeSettings? GetGenerationNodeSettingsVmFromJson(ITreeConfigNode node, string? settings);
        // MVVM settings model (if settings == null then empty model will be created)
        /// <summary>
        /// MVVM model for editing settings.
        /// INotifyPropertyChanged is minimum requirements for implemented interfaces.
        /// For validation settings INotifyDataErrorInfo is a minimum. IValidatableWithSeverity is recomended.
        /// </summary>
        /// <param name="settings, json format"></param>
        /// <returns>Stored outside plugin settings will be converted to a view model.</returns>
        IvPluginGeneratorSettings? GetAppGenerationSettingsVmFromJson(IAppProjectGenerator parent, string? settings);
        /// <summary>
        /// If several plugins generators (same plugin or different) are having same GroupGuid on solution level, there are same parameters for 
        /// such plugins generators share create group of plugins parameters for solution.
        /// Same parameters for several plugins are combined in class with IvPluginGroupSolutionSettings interface.
        /// </summary>
        /// <paramref name="settings">
        /// Json representation of plugin group setting. If this parameter is null, default result will be returned. 
        /// </paramref>
        /// <returns>
        /// Returns null if group parameters are not supported.
        /// If supported, return class of group settings
        /// </returns>
        IvPluginGroupSettings? GetPluginGroupSolutionSettingsVmFromJson(IAppSolution parent, string? settings);
        /// <summary>
        /// If there are same parameters for several plugins on project level, 
        /// such plugins create group of plugins parameters for project.
        /// Same parameters for several plugins are combined in class with IvPluginGroupProjectSettings interface.
        /// </summary>
        /// <paramref name="settings">
        /// Json representation of plugin group setting. If this parameter is null, default result will be returned. 
        /// </paramref>
        /// <returns>
        /// Returns null if group parameters are not supported.
        /// If supported, return class of group settings
        /// </returns>
        IvPluginGroupSettings? GetPluginGroupProjectSettingsVmFromJson(IAppProject parent, string? settings);
        /// <summary>
        /// Default setings name
        /// </summary>
        string DefaultSettingsName { get; }
        ITreeConfigNode? Parent { get; set; }
        IvPluginGenerator CreateNew(IAppProjectGenerator appProjectGenerator);
    }
}
