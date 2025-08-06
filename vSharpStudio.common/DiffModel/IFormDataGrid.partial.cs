namespace vSharpStudio.common
{
    public partial interface IFormDataGrid : ITreeConfigNodeSortable
    {
        IFormAutoLayoutBlock ParentFormAutoLayoutBlockI { get; }
    }
}
