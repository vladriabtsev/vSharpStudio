using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class GroupListMainViewFormsValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public GroupListMainViewFormsValidator()
        {
            this.GeneralRules();
        }
    }
}
