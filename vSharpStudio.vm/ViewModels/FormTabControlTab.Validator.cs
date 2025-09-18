using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class FormTabControlTabValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public FormTabControlTabValidator()
        {
            this.GeneralRules();
        }
    }
}
