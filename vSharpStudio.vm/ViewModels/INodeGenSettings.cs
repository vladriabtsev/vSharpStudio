using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public interface INodeGenSettings : INodeGenDicSettings
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
    }
    public interface INodeGenDicSettings
    {
        IReadOnlyDictionary<string, IvPluginGeneratorNodeSettings> DicGenNodeSettings { get; }
    }
}
