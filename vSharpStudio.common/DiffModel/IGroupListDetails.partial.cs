namespace vSharpStudio.common
{
    public partial interface IGroupListDetails : ITreeConfigNodeSortable
    {
        int IndexOf(IDetail det);
        EnumCatalogDetailAccess GetRoleDetailAccess(IRole role);
        EnumPrintAccess GetRoleDetailPrint(IRole role);
    }
}
