namespace vSharpStudio.common
{
    public partial interface IFormTabControlTab : ITreeConfigNodeSortable
    {
        IFormTabControl ParentFormTabControlI { get; }
    }
}
