namespace vSharpStudio.common
{
    public partial interface IGroupDocuments : ITreeConfigNodeSortable
    {
        IModel ParentModelI { get; }
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();
        EnumDocumentAccess GetRoleDocumentAccess(IRole role);
        EnumPrintAccess GetRoleDocumentPrint(IRole role);
        string DocumentTimelineName { get; }
        string DocumentDocDateTimePropertyName { get; }
    }
}
