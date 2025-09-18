using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupListCommonValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public GroupListCommonValidator()
        {
            this.GeneralRules();
        }
    }
}
