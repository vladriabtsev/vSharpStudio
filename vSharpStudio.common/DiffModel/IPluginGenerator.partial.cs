namespace vSharpStudio.common
{
    public partial interface IPluginGenerator : ITreeConfigNodeSortable
    {
        IPlugin ParentPluginI { get; }
        IvPluginGenerator? Generator { get; }
    }
}
