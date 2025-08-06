namespace vSharpStudio.common
{
    public partial interface IGroupListCommon : ITreeConfigNodeSortable
    {
        IModel ParentModelI { get; }
    }
}
