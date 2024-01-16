using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IGroupListConstants : ITreeConfigNodeSortable, IGetNodeSetting, ICompositeName
    {
        int IndexOf(IConstant cnst);
        IGroupConstantGroups ParentGroupConstantGroupsI { get; }
        IReadOnlyList<IProperty> GetIncludedConstantsAsProperties(string guidAppPrjGen, bool isOptimistic, bool isExcludeSpecial = false);
        EnumConstantAccess GetRoleConstantAccess(IRole role);
        EnumPrintAccess GetRoleConstantPrint(IRole role);
    }
}
