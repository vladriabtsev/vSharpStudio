using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class FormTreeValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public FormTreeValidator()
        {
            this.GeneralRules();
        }
    }
}
