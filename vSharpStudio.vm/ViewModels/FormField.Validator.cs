using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class FormFieldValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public FormFieldValidator()
        {
            this.GeneralRules();
        }
    }
}
