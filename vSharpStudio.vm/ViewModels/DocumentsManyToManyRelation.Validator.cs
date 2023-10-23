using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class DocumentsManyToManyRelationValidator
    {
        public DocumentsManyToManyRelationValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Must((o, name) => { return this.IsUnique(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            this.RuleFor(x => x.RefDoc1Guid).NotEmpty().WithMessage("Document type is not selected");
            this.RuleFor(x => x.RefDoc1Guid).Must((o, refcat) => { return string.IsNullOrWhiteSpace(refcat) || o.Cfg.DicNodes.ContainsKey(refcat); }).WithMessage("Selected document is not exists in configuration");
            this.RuleFor(x => x.RefDoc2Guid).NotEmpty().WithMessage("Document type is not selected");
            this.RuleFor(x => x.RefDoc2Guid).Must((o, refcat) => { return string.IsNullOrWhiteSpace(refcat) || o.Cfg.DicNodes.ContainsKey(refcat); }).WithMessage("Selected document is not exists in configuration");
        }
        private bool IsUnique(DocumentsManyToManyRelation val)
        {
            if (val.Parent == null)
            {
                return true;
            }
            if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
            {
                return true;
            }
            GroupDocumentsManyToManyRelations p = (GroupDocumentsManyToManyRelations)val.Parent;
            foreach (var t in p.ListDocumentsManyToManyRelations)
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
