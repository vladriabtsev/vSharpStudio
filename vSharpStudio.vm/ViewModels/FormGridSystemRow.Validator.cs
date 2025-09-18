using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class FormGridSystemRowValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public FormGridSystemRowValidator()
        {
            this.GeneralRules();
        }
    }
}
