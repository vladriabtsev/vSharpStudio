using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class FormAutoLayoutBlockValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public FormAutoLayoutBlockValidator()
        {
            this.GeneralRules();
        }
    }
}
