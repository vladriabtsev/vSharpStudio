namespace vSharpStudio.common
{
    public partial interface IRelationsManyToManyGroup : ITreeConfigNodeSortable, IGetNodeSetting
    {
        IRelationsGroup ParentGroupRelationsI { get; }
        int IndexOf(IRelationManyToMany rel);
    }
}
