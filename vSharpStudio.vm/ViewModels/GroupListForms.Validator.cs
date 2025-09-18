using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupListFormsValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public GroupListFormsValidator()
        {
            this.GeneralRules();
        }
    }
}
