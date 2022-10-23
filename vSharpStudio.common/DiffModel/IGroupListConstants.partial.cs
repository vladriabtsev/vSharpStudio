using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IGroupListConstants : ITreeConfigNode, IGetNodeSetting, ICompositeName
    {
        IGroupConstantGroups ParentGroupConstantGroupsI { get; }
        IReadOnlyList<IConstant> GetIncludedConstants(string guidAppPrjGen);
    }
}
