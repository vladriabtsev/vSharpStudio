using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupListAppSolutionsValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public GroupListAppSolutionsValidator()
        {
            this.GeneralRules();
        }
    }
}
