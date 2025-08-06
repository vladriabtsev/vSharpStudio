namespace vSharpStudio.common
{
    public partial interface IGroupListMainViewForms : ITreeConfigNodeSortable
    {
        IGroupListCommon ParentGroupListCommonI { get; }
    }
}
