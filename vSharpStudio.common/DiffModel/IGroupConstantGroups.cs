using System.Collections.Generic;

namespace vSharpStudio.common
{
    public partial interface IGroupConstantGroups : ITreeConfigNodeSortable, IGetNodeSetting
    {
        int IndexOf(IGroupListConstants cnstg);
        IModel ParentModelI { get; }
        IReadOnlyList<IGroupListConstants> GetIncludedConstantGroups(string guidAppPrjGen);
    }
}
