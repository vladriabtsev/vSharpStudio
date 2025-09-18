using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupListEnumerationsValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public GroupListEnumerationsValidator()
        {
            this.GeneralRules();
        }
    }
}
