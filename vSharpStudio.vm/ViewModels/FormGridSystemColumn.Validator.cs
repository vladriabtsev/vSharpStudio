using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class FormGridSystemColumnValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public FormGridSystemColumnValidator()
        {
            this.GeneralRules();
        }
    }
}
