using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class FormDataGridValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public FormDataGridValidator()
        {
            this.GeneralRules();
        }
    }
}
