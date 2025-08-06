namespace vSharpStudio.common
{
    public partial interface IGroupListPlugins : ITreeConfigNodeSortable
    {
        IConfig ParentConfigI { get; }
    }
}
