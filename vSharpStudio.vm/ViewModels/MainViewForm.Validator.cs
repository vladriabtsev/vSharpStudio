using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class MainViewFormValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public MainViewFormValidator()
        {
            this.GeneralRules();
        }
    }
}
