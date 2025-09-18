using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupListRolesValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public GroupListRolesValidator()
        {
            this.GeneralRules();
        }
    }
}
