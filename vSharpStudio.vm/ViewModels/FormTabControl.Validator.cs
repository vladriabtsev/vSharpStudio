using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class FormTabControlValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public FormTabControlValidator()
        {
            this.GeneralRules();
        }
    }
}
