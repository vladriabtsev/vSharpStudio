using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    public interface INodeGenSettings
    {
        object GeneratorNodeSettings { get; }
        ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; }
        //ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings { get; }
        //void AddAllAppGenSettingsVmsToNewNode();
        void RestoreNodeAppGenSettingsVm();
        void SaveNodeAppGenSettings();
        void RemoveNodeAppGenSettings(string appGenGuid);
        void AddNodeAppGenSettings(string appGenGuid);
        //void CreatePropertyGridNodeGenSettings(INodeGenSettings p);
        //string GeneratorSettings { get; }
    }
}
