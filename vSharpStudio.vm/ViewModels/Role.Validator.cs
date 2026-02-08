using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class RoleValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public RoleValidator()
        {
            this.GeneralRules();
        }
    }
}
