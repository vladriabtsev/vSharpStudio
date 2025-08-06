namespace vSharpStudio.common
{
    public partial interface IGroupListBaseConfigLinks : ITreeConfigNodeSortable
    {
        IConfig ParentConfigI { get; }
    }
}
