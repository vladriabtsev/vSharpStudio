using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupListBaseConfigLinksValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public GroupListBaseConfigLinksValidator()
        {
            this.GeneralRules();
        }
    }
}
