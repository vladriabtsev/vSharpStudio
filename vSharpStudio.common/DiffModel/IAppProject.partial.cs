﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IAppProject : ITreeConfigNodeSortable, IvPluginGroupSettingsDic
    {
        IAppSolution ParentAppSolutionI { get; }
        string GetProjectFolderPath();
        IvPluginGroupSettings? GetGroupSettings(string groupSettingsGuid);
        string GetProjectPath();
    }
}
