namespace vSharpStudio.common
{
    public partial interface IGroupListDocuments : ITreeConfigNodeSortable, IGetNodeSetting
    {
        IGroupDocuments ParentGroupDocumentsI { get; }
        IModel ParentModelI { get; }
        int IndexOf(IDocument doc);
        bool IsGridSortableGet();
        bool IsGridFilterableGet();
        bool IsGridSortableCustomGet();
        EnumDocumentAccess GetRoleDocumentAccess(IRole role);
        EnumPrintAccess GetRoleDocumentPrint(IRole role);
    }
}
