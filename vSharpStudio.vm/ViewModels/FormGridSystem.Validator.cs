using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class FormGridSystemValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public FormGridSystemValidator()
        {
            this.GeneralRules();
        }
    }
}
