using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface IGroupListCatalogs : ITreeConfigNodeSortable, IGetNodeSetting
    {
        IModel ParentModelI { get; }
        int IndexOf(ICatalog cat);
        EnumCatalogDetailAccess GetRoleCatalogAccess(string roleGuid);
    }
}
