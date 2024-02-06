using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class RelationManyToManyValidator
    {
        public RelationManyToManyValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Must((o, name) => { return this.IsUnique(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            this.RuleFor(x => x.GuidObj1).NotEmpty().WithMessage("Catalog type is not selected");
            this.RuleFor(x => x.GuidObj1).Must((o, refobj) => { return string.IsNullOrWhiteSpace(refobj) || o.Cfg.DicNodes.ContainsKey(refobj); }).WithMessage("Selected catalog is not exists in configuration");
            this.RuleFor(x => x.GuidObj2).NotEmpty().WithMessage("Catalog type is not selected");
            this.RuleFor(x => x.GuidObj2).Must((o, refobj) => { return string.IsNullOrWhiteSpace(refobj) || o.Cfg.DicNodes.ContainsKey(refobj); }).WithMessage("Selected catalog is not exists in configuration");
        }
        private bool IsUnique(RelationManyToMany val)
        {
            if (val.Parent == null)
            {
                return true;
            }
            if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
            {
                return true;
            }
            RelationsManyToManyGroup p = (RelationsManyToManyGroup)val.Parent;
            foreach (var t in p.ListCatalogsRelations)
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
