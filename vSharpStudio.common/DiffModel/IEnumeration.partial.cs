namespace vSharpStudio.common
{
    public partial interface IEnumeration : ITreeConfigNodeSortable, IGetNodeSetting
    {
        IGroupListEnumerations ParentGroupListEnumerationsI { get; }
        string GetClrBase();
        string GetClrValueType();
        string DefaultValue { get; }
    }
}
