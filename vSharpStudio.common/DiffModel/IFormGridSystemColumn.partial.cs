namespace vSharpStudio.common
{
    public partial interface IFormGridSystemColumn : ITreeConfigNodeSortable
    {
        IFormGridSystemRow ParentFormGridSystemRowI { get; }
        IFormAutoLayoutBlock AddAutoLayoutBlock(string name = "");
    }
}
