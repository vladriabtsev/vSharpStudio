using System;
using System.Collections.Generic;
using System.Text;

namespace vSharpStudio.common
{
    public partial interface IGroupConstantGroups : ITreeConfigNodeSortable, IGetNodeSetting
    {
        IModel ParentModelI { get; }
        IReadOnlyList<IGroupListConstants> GetIncludedConstantGroups(string guidAppPrjGen);
        EnumConstantAccess GetRoleConstantAccess(string roleGuid);
        EnumPrintAccess GetRoleConstantPrint(string roleGuid);
        IReadOnlyList<string> GetRolesByAccess(EnumConstantAccess access);
        IReadOnlyList<string> GetRolesByAccess(EnumPrintAccess access);
    }
}
