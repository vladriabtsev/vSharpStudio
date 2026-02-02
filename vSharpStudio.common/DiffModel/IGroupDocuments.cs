namespace vSharpStudio.common
{
    public partial interface IGroupDocuments : ITreeConfigNodeSortable
    {
        IModel ParentModelI { get; }
        string TimeLineDocDateTimePropertyName { get; }
    }
}
