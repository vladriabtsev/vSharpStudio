using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ReportValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(RoleValidator));
        public ReportValidator()
        {
            this.GeneralRules();
        }
    }
}
