using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class RegisterDocToRegValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public RegisterDocToRegValidator()
        {
            this.GeneralRules();
        }
    }
}
