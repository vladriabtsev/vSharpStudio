namespace vSharpStudio.common
{
    public partial interface IGroupListCatalogs : ITreeConfigNodeSortable, IGetNodeSetting
    {
        IGroupCatalogs ParentGroupCatalogsI { get; }
        IModel ParentModelI { get; }
        int IndexOf(ICatalog cat);
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();
        EnumCatalogDetailAccess GetRoleCatalogAccess(IRole role);
        EnumPrintAccess GetRoleCatalogPrint(IRole role);
    }
}
