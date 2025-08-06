namespace vSharpStudio.common
{
    public partial interface IFormGridSystemRow : ITreeConfigNodeSortable
    {
        IFormGridSystem ParentFormGridSystemI { get; }
        IFormGridSystemColumn AddGridSystemColumn(string name = "");
    }
}
