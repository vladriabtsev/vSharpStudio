namespace vSharpStudio.common
{
    public partial interface IGroupListCatalogs : ITreeConfigNodeSortable, IGetNodeSetting
    {
        IModel ParentModelI { get; }
        int IndexOf(ICatalog cat);
        EnumCatalogDetailAccess GetRoleCatalogAccess(IRole role);
        EnumPrintAccess GetRoleCatalogPrint(IRole role);
    }
}
