using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public partial interface IGroupConstantGroups : ITreeConfigNode, IGetNodeSetting
    {
        IReadOnlyList<IGroupListConstants> GetIncludedConstantGroups(string guidAppPrjGen);
    }
}
