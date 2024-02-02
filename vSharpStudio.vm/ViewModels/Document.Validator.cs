using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class DocumentValidator
    {
        public DocumentValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Custom((name, cntx) =>
            {
                if (string.IsNullOrEmpty(name))
                    return;
                var p = (Document)cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (string.IsNullOrWhiteSpace(p.SequenceGuid))
                {
                    var vf = new ValidationFailure(nameof(p.SequenceGuid),
                        $"Document enumerator sequence is not selected.");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
                var pg = p.ParentGroupListDocuments;
                if (name == pg.ParentGroupDocuments.TimelineName)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Group documents parameter 'Timeline name' is set to '{pg.ParentGroupDocuments.TimelineName}'. This name is reverved for documents timeline.");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
                foreach (var t in pg.ListDocuments)
                {
                    if ((p.Guid != t.Guid) && (name == t.Name))
                    {
                        var vf = new ValidationFailure(nameof(p.Name),
                            $"Not unique document name '{name}'");
                        vf.Severity = Severity.Error;
                        cntx.AddFailure(vf);
                    }
                }
            });
        }
    }
}
