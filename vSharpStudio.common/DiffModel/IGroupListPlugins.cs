using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public partial interface IGroupListPlugins : ITreeConfigNode
    {
        IConfig ParentConfigI { get; }
    }
}
