using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using ViewModelBase;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public interface INodeGenSettings : INodeGenDicSettings
    {
        ConfigNodesCollection<PluginGeneratorNodeSettings> ListNodeGeneratorsSettings { get; }
        void RestoreNodeAppGenSettingsVm();
        void SaveNodeAppGenSettings();
        void RemoveNodeAppGenSettings(string appGenGuid);
        void AddNodeAppGenSettings(string appGenGuid);
    }
    public interface INodeGenDicSettings
    {
        IReadOnlyDictionary<string, IvPluginGeneratorNodeSettings> DicGenNodeSettings { get; }
    }
}
