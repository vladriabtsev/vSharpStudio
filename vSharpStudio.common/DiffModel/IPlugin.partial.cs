namespace vSharpStudio.common
{
    public partial interface IPlugin : ITreeConfigNodeSortable
    {
        IGroupListPlugins ParentGroupListPluginsI { get; }
        IvPlugin? VPlugin { get; }
    }
}
