namespace vSharpStudio.common
{
    public partial interface IFormTree : ITreeConfigNodeSortable
    {
        IFormAutoLayoutBlock ParentFormAutoLayoutBlockI { get; }
    }
}
