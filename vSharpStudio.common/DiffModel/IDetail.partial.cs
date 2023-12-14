using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;
using vSharpStudio.common.DiffModel;

namespace vSharpStudio.common
{
    public partial interface IDetail : ITreeConfigNodeSortable, IGetNodeSetting, IItemWithSubItems
    {
        //IReadOnlyList<IProperty> GetIncludedViewProperties(string guidAppPrjDbGen);
        void GetSpecialProperties(List<IProperty> res, bool isOptimistic);
        ViewFormData GetFormViewData(FormType formType, string guidAppPrjGen);
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();
        IGroupListDetails ParentGroupListDetailsI { get; }

        EnumPropertyAccess GetRolePropertyAccess(IRole role);
        EnumPrintAccess GetRolePropertyPrint(IRole role);
        EnumCatalogDetailAccess GetRoleDetailAccess(IRole role);
        EnumPrintAccess GetRoleDetailPrint(IRole roles);
        IReadOnlyList<string> GetRolesByAccess(EnumCatalogDetailAccess access);
        IReadOnlyList<string> GetRolesByAccess(EnumPrintAccess access);
        string FullName { get; } // name with config name
        string GetDebuggerDisplay(bool isOptimistic);
    }
}
