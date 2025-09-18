using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupListPluginsValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public GroupListPluginsValidator()
        {
            this.GeneralRules();
        }
    }
}
