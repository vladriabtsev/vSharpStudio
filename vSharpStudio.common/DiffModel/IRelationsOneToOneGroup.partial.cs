namespace vSharpStudio.common
{
    public partial interface IRelationsOneToOneGroup : ITreeConfigNodeSortable, IGetNodeSetting
    {
        IRelationsGroup ParentGroupRelationsI { get; }
        int IndexOf(IRelationOneToOne rel);
    }
}
