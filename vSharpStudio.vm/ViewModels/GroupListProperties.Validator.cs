using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupListPropertiesValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public GroupListPropertiesValidator()
        {
            this.GeneralRules();
        }
    }
}
