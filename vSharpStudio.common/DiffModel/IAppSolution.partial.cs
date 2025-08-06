namespace vSharpStudio.common
{
    public partial interface IAppSolution : ITreeConfigNodeSortable, IvPluginGroupSettingsDic
    {
        IGroupListAppSolutions ParentGroupListAppSolutionsI { get; }
        IvPluginGroupSettings? GetGroupSettings(string groupSettingsGuid);
    }
}
