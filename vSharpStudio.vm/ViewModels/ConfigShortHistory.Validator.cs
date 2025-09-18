using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ConfigShortHistoryValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(DocumentTimelineValidator));
        public ConfigShortHistoryValidator()
        {
            this.GeneralRules();
        }
    }
}
