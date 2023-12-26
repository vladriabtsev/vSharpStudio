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
    public partial class JournalValidator
    {
        public JournalValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Custom((name, cntx) =>
            {
                if (string.IsNullOrEmpty(name))
                    return;
                var p = (Journal)cntx.InstanceToValidate;
                Debug.Assert(p.Parent != null);
                var pg = p.ParentGroupListJournals;
                if (name == pg.JournalAllDocumentsName)
                {
                    var vf = new ValidationFailure(nameof(p.Name),
                        $"Model is is configured to use '{pg.JournalAllDocumentsName}' name as a journal name for all documents. This reserved name is set Journals group settings.");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
                foreach (var t in pg.ListJournals)
                {
                    if ((p.Guid != t.Guid) && (name == t.Name))
                    {
                        var vf = new ValidationFailure(nameof(p.Name),
                            $"Not unique journal name '{name}'");
                        vf.Severity = Severity.Error;
                        cntx.AddFailure(vf);
                    }
                }
            });
        }
    }
}
