using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface ICatalogFolder : ITreeConfigNodeSortable, IGetNodeSetting, IItemWithDetails
    {
        ICatalog ParentCatalogI { get; }
        void GetSpecialProperties(List<IProperty> res, bool isOptimistic);
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();

        bool GetUseCodeProperty();
        bool GetUseNameProperty();
        bool GetUseDescriptionProperty();
        EnumPropertyAccess GetRolePropertyAccess(IRole role);
        EnumPrintAccess GetRolePropertyPrint(IRole role);
        EnumCatalogDetailAccess GetRoleCatalogAccess(IRole role);
        EnumPrintAccess GetRoleCatalogPrint(IRole role);
        IReadOnlyList<string> GetRolesByAccess(EnumCatalogDetailAccess access);
        IReadOnlyList<string> GetRolesByAccess(EnumPrintAccess access);
        string GetDebuggerDisplay(bool isOptimistic);
    }
}
