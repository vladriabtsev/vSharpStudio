using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IAppProject : ITreeConfigNode, IvPluginGroupSettingsDic
    {
        IAppSolution AppSolution { get; }
        string GetProjectFolderPath();
        IvPluginGroupSettings GetGroupSettings(string groupSettingsGuid);
    }
}
