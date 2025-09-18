using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class RelationsOneToOneGroupValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public RelationsOneToOneGroupValidator()
        {
            this.GeneralRules();
        }
    }
}
