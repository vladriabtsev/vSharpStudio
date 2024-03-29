﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IAppSolution : ITreeConfigNodeSortable, IvPluginGroupSettingsDic
    {
        IGroupListAppSolutions ParentGroupListAppSolutionsI { get; }
        IvPluginGroupSettings? GetGroupSettings(string groupSettingsGuid);
    }
}
