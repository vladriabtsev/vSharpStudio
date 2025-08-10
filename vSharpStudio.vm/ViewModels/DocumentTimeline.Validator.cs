using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class DocumentTimelineValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(DocumentTimelineValidator));
        public DocumentTimelineValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
        }
    }
}
