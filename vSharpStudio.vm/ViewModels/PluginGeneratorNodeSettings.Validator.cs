using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class PluginGeneratorNodeSettingsValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public PluginGeneratorNodeSettingsValidator()
        {
            this.GeneralRules();
        }
    }
}
