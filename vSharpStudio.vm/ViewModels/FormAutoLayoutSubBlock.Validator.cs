using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class FormAutoLayoutSubBlockValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public FormAutoLayoutSubBlockValidator()
        {
            this.GeneralRules();
        }
    }
}
