using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.vm.ViewModels
{
    public interface INodeGenSettings
    {
        ConfigNodesCollection<GeneratorSettings> ListGeneratorsSettings { get; }
        //string GetNodeGenTypeSettings(AppProjectGenerator appgen, Type type);
        void AddAllAppGenSettingsVmsToNewNode();
        void RestoreNodeAppGenSettingsVm();
        void SaveNodeAppGenSettings();
        void RemoveNodeAppGenSettings(string appGenGuid);
        void AddNodeAppGenSettings(string appGenGuid);
    }
}
