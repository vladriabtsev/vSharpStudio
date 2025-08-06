namespace vSharpStudio.common
{
    public partial interface IGroupListJournals : ITreeConfigNodeSortable, IGetNodeSetting, ICompositeName
    {
        IGroupDocuments ParentGroupDocumentsI { get; }
        string GetDebuggerDisplay(bool isOptimistic);
    }
}
