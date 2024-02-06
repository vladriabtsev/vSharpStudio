using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;

namespace vSharpStudio.common
{
    public partial interface IRelationManyToMany : ITreeConfigNodeSortable, IGetNodeSetting, ISortingValue
    {
        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial = false);
        void GetSpecialProperties(List<IProperty> res, bool isOptimistic);
        //EnumPropertyAccess GetRolePropertyAccess(IRole role);
        //EnumPrintAccess GetRolePropertyPrint(IRole role);
        //EnumCatalogDetailAccess GetRoleCatalogAccess(IRole role);
        //EnumPrintAccess GetRoleCatalogPrint(IRole role);
        //IReadOnlyList<string> GetRolesByAccess(EnumCatalogDetailAccess access);
        //IReadOnlyList<string> GetRolesByAccess(EnumPrintAccess access);
        string FullName { get; } // name with config name
    }
}
