namespace vSharpStudio.common
{
    public partial interface IGroupCatalogs : ITreeConfigNodeSortable
    {
        IModel ParentModelI { get; }
    }
}
