namespace vSharpStudio.common
{
    public partial interface IGroupCatalogs : ITreeConfigNodeSortable
    {
        IModel ParentModelI { get; }
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();
        EnumCatalogDetailAccess GetRoleCatalogAccess(IRole role);
        EnumPrintAccess GetRoleCatalogPrint(IRole role);
    }
}
