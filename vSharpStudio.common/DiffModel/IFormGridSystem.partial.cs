namespace vSharpStudio.common
{
    public partial interface IFormGridSystem : ITreeConfigNodeSortable
    {
        IForm? ParentFormI { get; }
        IFormAutoLayoutBlock? ParentFormAutoLayoutBlockI { get; }
        IFormGridSystemRow AddGridRow(string name);
    }
}
