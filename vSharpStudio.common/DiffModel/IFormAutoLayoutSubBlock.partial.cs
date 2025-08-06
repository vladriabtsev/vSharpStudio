namespace vSharpStudio.common
{
    public partial interface IFormAutoLayoutSubBlock : ITreeConfigNodeSortable
    {
        IForm? ParentFormI { get; }
        IFormAutoLayoutBlock? ParentFormAutoLayoutBlockI { get; }
        IFormTabControlTab? ParentFormTabControlTabI { get; }
        IFormGridSystemColumn? ParentFormGridSystemColumnI { get; }
    }
}
