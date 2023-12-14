using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IGroupListDetails : ITreeConfigNodeSortable
    {
        int IndexOf(IDetail det);
        EnumCatalogDetailAccess GetRoleDetailAccess(IRole role);
        EnumPrintAccess GetRoleDetailPrint(IRole role);
    }
}
