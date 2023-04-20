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

        bool GetUseCodeProperty();
        bool GetUseNameProperty();
        bool GetUseDescriptionProperty();
        EnumCatalogDetailAccess GetRoleDetailAccess(string roleGuid);
        EnumPrintAccess GetRoleDetailPrint(string roleGuid);
    }
}
