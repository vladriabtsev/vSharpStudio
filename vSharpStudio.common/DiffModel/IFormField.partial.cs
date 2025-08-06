namespace vSharpStudio.common
{
    public partial interface IFormField : ITreeConfigNodeSortable
    {
        IFormAutoLayoutBlock ParentFormAutoLayoutBlockI { get; }
    }
}
