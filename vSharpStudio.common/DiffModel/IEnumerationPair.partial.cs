namespace vSharpStudio.common
{
    public partial interface IEnumerationPair : ITreeConfigNodeSortable, IGetNodeSetting
    {
        IEnumeration ParentEnumerationI { get; }
    }
}
