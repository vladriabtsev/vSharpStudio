﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using ViewModelBase;

// https://www.codeproject.com/Articles/376033/From-Zero-to-Proficient-with-MEF
// https://docs.microsoft.com/en-us/dotnet/framework/mef/

namespace vSharpStudio.common
{
    public interface IvPluginGroupSettings : IvPluginGeneratorValidatableSettings
    {
        ITreeConfigNode? Parent { get; set; }
        string Guid { get; }
        string? Name { get; }
        string? Version { get; }
        string? Description { get; }
        /// <summary>
        /// Get s Settings VM from JSON string
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        IvPluginGroupSettings? GetPluginGroupSettingsVm(IAppSolution parent, string settings);
        /// <summary>
        /// Get protobuf model of settings from MVVM model (json format)
        /// </summary>
        string SettingsAsJson { get; }
    }
    public interface IvPluginGroupSettingsDic
    {
        DictionaryExt<string, IvPluginGroupSettings?> DicPluginsGroupSettings { get; }
    }
}
