namespace vSharpStudio.common
{
    public partial interface IGroupListDocuments : ITreeConfigNodeSortable, IGetNodeSetting
    {
        IGroupDocuments ParentGroupDocumentsI { get; }
        int IndexOf(IDocument doc);
        EnumDocumentAccess GetRoleDocumentAccess(IRole role);
        EnumPrintAccess GetRoleDocumentPrint(IRole role);
    }
}
