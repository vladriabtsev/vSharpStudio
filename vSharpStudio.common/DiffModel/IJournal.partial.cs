namespace vSharpStudio.common
{
    public partial interface IJournal : ITreeConfigNodeSortable, IGetNodeSetting
    {
        IGroupListJournals ParentGroupListJournalsI { get; }
    }
}
