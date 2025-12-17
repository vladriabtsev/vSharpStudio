namespace vSharpStudio.common
{
    public partial interface IPluginGeneratorNodeSettings : ITreeConfigNodeSortable
    {
        IvPluginGeneratorNodeSettings? SettingsVm { get; }
    }
}
