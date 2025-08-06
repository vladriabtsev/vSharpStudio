namespace vSharpStudio.common
{
    public partial interface IBaseConfigLink : ITreeConfigNodeSortable
    {
        IGroupListBaseConfigLinks ParentGroupListBaseConfigLinksI { get; }
        IConfig? ConfigBase { get; }
    }
}
