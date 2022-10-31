using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public partial interface IPlugin : ITreeConfigNodeSortable
    {
        IGroupListPlugins ParentGroupListPluginsI { get; }
        IvPlugin VPlugin { get; }
    }
}
