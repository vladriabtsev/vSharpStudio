namespace vSharpStudio.common
{
    public partial interface IGroupListEnumerations : ITreeConfigNodeSortable, IGetNodeSetting
    {
        IModel ParentModelI { get; }
    }
}
