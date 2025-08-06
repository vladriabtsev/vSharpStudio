namespace vSharpStudio.common
{
    public partial interface IRole : ITreeConfigNodeSortable
    {
        IGroupListRoles ParentGroupListRolesI { get; }
    }
}
