namespace vSharpStudio.common
{
    public partial interface IFormTabControl : ITreeConfigNodeSortable
    {
        IFormAutoLayoutBlock ParentFormAutoLayoutBlockI { get; }
    }
}
