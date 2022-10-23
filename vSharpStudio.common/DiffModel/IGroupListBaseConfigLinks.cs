using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public partial interface IGroupListBaseConfigLinks : ITreeConfigNode
    {
        IConfig ParentConfigI { get; }
    }
}
