using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupListRegistersValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public GroupListRegistersValidator()
        {
            this.GeneralRules();
        }
    }
}
