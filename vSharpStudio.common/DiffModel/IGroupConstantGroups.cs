using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public partial interface IGroupConstantGroups : ITreeConfigNodeSortable, IGetNodeSetting
    {
        IModel ParentModelI { get; }
        IReadOnlyList<IGroupListConstants> GetIncludedConstantGroups(string guidAppPrjGen);
    }
}
