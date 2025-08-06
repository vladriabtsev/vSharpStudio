namespace vSharpStudio.common
{
    public partial interface IGroupListRoles : ITreeConfigNodeSortable
    {
        IGroupListCommon ParentGroupListCommonI { get; }
    }
}
