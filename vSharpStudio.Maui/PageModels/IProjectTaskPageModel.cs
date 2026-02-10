using CommunityToolkit.Mvvm.Input;
using vSharpStudio.Maui.Models;

namespace vSharpStudio.Maui.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}