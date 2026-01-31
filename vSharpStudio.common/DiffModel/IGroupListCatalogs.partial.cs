namespace vSharpStudio.common
{
    public partial interface IGroupListCatalogs : ITreeConfigNodeSortable, IGetNodeSetting
    {
        IGroupCatalogs ParentGroupCatalogsI { get; }
        int IndexOf(ICatalog cat);
        //EnumCatalogDetailAccess GetRoleCatalogAccess(IRole role);
        //EnumPrintAccess GetRoleCatalogPrint(IRole role);
    }
}
