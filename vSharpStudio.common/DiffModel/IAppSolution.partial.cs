using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IAppSolution : ITreeConfigNode
    {
        IConfig Config { get; }
        IvPluginGroupSolutionSettings GetGroupSettings(string groupSettingsGuid);
    }
}
