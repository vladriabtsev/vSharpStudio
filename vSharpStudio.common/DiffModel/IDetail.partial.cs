using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;
using vSharpStudio.common.DiffModel;

namespace vSharpStudio.common
{
    public partial interface IDetail : ITreeConfigNodeSortable, IGetNodeSetting, ICompositeName
    {
        string GetDebuggerDisplay(bool isOptimistic);
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
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isOptimistic, bool isExcludeSpecial = false);
        IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjGen);

        IForm GetForm(FormType ftype, string guidAppPrjGen);
        IReadOnlyList<IForm> GetListForms(string guidAppPrjGen);
    }
}
