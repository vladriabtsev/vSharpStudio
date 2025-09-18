using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupListReportsValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public GroupListReportsValidator()
        {
            this.GeneralRules();
        }
    }
}
