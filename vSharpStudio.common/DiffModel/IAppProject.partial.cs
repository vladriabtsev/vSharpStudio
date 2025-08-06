namespace vSharpStudio.common
{
    public partial interface IAppProject : ITreeConfigNodeSortable, IvPluginGroupSettingsDic
    {
        IAppSolution ParentAppSolutionI { get; }
        string GetProjectFolderPath();
        IvPluginGroupSettings? GetGroupSettings(string groupSettingsGuid);
        string GetProjectPath();
    }
}
