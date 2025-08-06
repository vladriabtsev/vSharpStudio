namespace vSharpStudio.common
{
    public partial interface IDocumentEnumeratorSequence : ITreeConfigNodeSortable, ITextValue
    {
        IGroupListEnumeratorSequences ParentGroupListSequencesI { get; }
    }
}
