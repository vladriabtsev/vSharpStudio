using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Results;

namespace vSharpStudio.vm.ViewModels
{
    public partial class RelationOneToOneValidator
    {
        public RelationOneToOneValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Must((o, name) => { return this.IsUnique(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            this.RuleFor(x => x.GuidObj1).Custom((guid, cntx) =>
            {
                var rel = (RelationOneToOne)cntx.InstanceToValidate;
                if (string.IsNullOrEmpty(guid))
                {
                    var vf = new ValidationFailure(nameof(rel.GuidObj1), "Configuration object type is not selected.");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                    return;
                }
                if (!rel.Cfg.DicNodes.ContainsKey(guid))
                {
                    var vf = new ValidationFailure(nameof(rel.GuidObj1),
                        "Selected object type is not exists in configuration.");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
            this.RuleFor(x => x.GuidObj2).Custom((guid, cntx) =>
            {
                var rel = (RelationOneToOne)cntx.InstanceToValidate;
                if (string.IsNullOrEmpty(guid))
                {
                    var vf = new ValidationFailure(nameof(rel.GuidObj2), "Configuration object type is not selected.");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                    return;
                }
                if (!rel.Cfg.DicNodes.ContainsKey(guid))
                {
                    var vf = new ValidationFailure(nameof(rel.GuidObj2),
                        "Selected object type is not exists in configuration.");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
        }
        private bool IsUnique(RelationOneToOne val)
        {
            if (val.Parent == null)
            {
                return true;
            }
            if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
            {
                return true;
            }
            RelationsOneToOneGroup p = (RelationsOneToOneGroup)val.Parent;
            foreach (var t in p.ListDocumentsRelations)
            {
                if ((val.Guid != t.Guid) && (val.Name == t.Name))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
