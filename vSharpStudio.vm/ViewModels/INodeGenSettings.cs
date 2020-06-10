using System;
using System.Collections.Generic;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public interface INodeGenSettings
    {
        ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; }
        //ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings { get; }
        //void AddAllAppGenSettingsVmsToNewNode();
        void RestoreNodeAppGenSettingsVm();
        void SaveNodeAppGenSettings();
        void RemoveNodeAppGenSettings(string appGenGuid);
        void AddNodeAppGenSettings(string appGenGuid);
        //void CreatePropertyGridNodeGenSettings(INodeGenSettings p);
        //string GeneratorSettings { get; }
        DictionaryExt<string, IvPluginGeneratorNodeSettings> DicGenNodeSettings { get; }
    }
}
