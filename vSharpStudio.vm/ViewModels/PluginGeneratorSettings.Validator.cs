using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class PluginGeneratorSettingsValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public PluginGeneratorSettingsValidator()
        {
            this.GeneralRules();
        }
    }
}
