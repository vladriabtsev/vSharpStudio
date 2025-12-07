using System.Collections.Generic;

namespace vSharpStudio.common
{
    public partial interface ICatalogFolder : ITreeConfigNodeSortable, IGetNodeSetting, ICompositeName, IPKey
    {
        ICatalog ParentCatalogI { get; }
        string GetDebuggerDisplay(bool isOptimistic);

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
        //string GetDebuggerDisplay(bool isOptimistic);
        IProperty GetCodeProperty(List<IProperty> lst);
        IProperty GetNameProperty(List<IProperty> lst);
        IProperty GetDescriptionProperty(List<IProperty> lst);

        IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjDbGen, bool isOptimistic, bool isExcludeSpecial = false);
        IReadOnlyList<IDetail> GetIncludedDetails(string guidAppPrjGen);
        IForm GetForm(FormType ftype, string guidAppPrjGen);
        IReadOnlyList<IForm> GetListForms(string guidAppPrjGen);
    }
}
