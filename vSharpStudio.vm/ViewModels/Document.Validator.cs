using FluentValidation;
using FluentValidation.Results;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class DocumentValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(DocumentValidator));
        public DocumentValidator()
        {
            this.GeneralRules();
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Custom((name, cntx) =>
            {
                if (string.IsNullOrEmpty(name))
                    return;
                var p = cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (string.IsNullOrWhiteSpace(p.SequenceGuid))
                {
                    var vf = Common.CreateValidationFailure(nameof(p.SequenceGuid),
                        $"Document enumerator sequence is not selected.");
                    cntx.AddFailure(vf);
                }
                var pg = p.ParentGroupListDocuments;
                if (name == pg.ParentGroupDocuments.TimeLineDocDateTimePropertyName)
                {
                    var vf = Common.CreateValidationFailure(nameof(p.Name),
                        $"Document date and time property name is set to '{pg.ParentGroupDocuments.TimeLineDocDateTimePropertyName}'. This name is reserved for document timeline property.");
                    cntx.AddFailure(vf);
                }
                foreach (var t in pg.ListDocuments)
                {
                    if ((p.Guid != t.Guid) && (name == t.Name))
                    {
                        var vf = Common.CreateValidationFailure(nameof(p.Name),
                            $"Not unique document name '{name}'");
                        cntx.AddFailure(vf);
                    }
                }
            });
        }
    }
}
