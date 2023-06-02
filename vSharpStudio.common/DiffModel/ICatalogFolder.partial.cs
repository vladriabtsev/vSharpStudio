using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vSharpStudio.common
{
    public partial interface ICatalogFolder : ITreeConfigNodeSortable, IGetNodeSetting, IDbTable
    {
        ICatalog ParentCatalogI { get; }
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isSupportVersion, bool isExcludeSpecial = false);
        IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjGen);
        void GetSpecialProperties(List<IProperty> res, bool isSupportVersion);
        IForm GetForm(FormType ftype, string guidAppPrjGen);
        IReadOnlyList<IForm> GetListForms(string guidAppPrjGen);
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
    }
}
