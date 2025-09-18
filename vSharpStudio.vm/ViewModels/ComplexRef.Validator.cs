using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ComplexRefValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(DocumentTimelineValidator));
        public ComplexRefValidator()
        {
            this.GeneralRules();
        }
    }
}
