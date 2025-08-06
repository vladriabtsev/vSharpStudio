namespace vSharpStudio.common
{
    public partial interface IReport : ITreeConfigNodeSortable
    {
        IGroupListReports ParentGroupListReportsI { get; }
    }
}
