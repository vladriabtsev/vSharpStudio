using System.Collections.Generic;

namespace vSharpStudio.common
{
    public partial interface IGroupListConstants : ITreeConfigNodeSortable, IGetNodeSetting, 
        ICompositeName, INodeWithPositionProperties
    {
        int IndexOf(IConstant cnst);
        IGroupConstantGroups ParentGroupConstantGroupsI { get; }
        IReadOnlyList<IProperty> GetIncludedConstantsAsProperties(string guidAppPrjGen, bool isOptimistic, bool isSkipComplexDescr, bool isExcludeSpecial = false);
        IRoleConstantsSettings GetRoleSettings(IRole role);
    }
}
