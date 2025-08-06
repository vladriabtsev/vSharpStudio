using System.Collections.Generic;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public interface IGroupGenSettings : INodeGenDicSettings
    {
        ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; }
        void RestoreNodeAppGenSettingsVm();
        void SaveNodeAppGenSettings();
        void RemoveNodeAppGenSettings(string appGenGuid);
        void AddNodeAppGenSettings(string appGenGuid);
        IReadOnlyDictionary<string, IvPluginGroupSettings> DicPluginsGroupSettings { get; }
    }
}
