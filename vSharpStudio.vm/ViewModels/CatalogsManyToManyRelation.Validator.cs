using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class CatalogsManyToManyRelationValidator
    {
        public CatalogsManyToManyRelationValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Must((o, name) => { return this.IsUnique(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            this.RuleFor(x => x.RefCat1Guid).NotEmpty().WithMessage("Catalog type is not selected");
            this.RuleFor(x => x.RefCat1Guid).Must((o, refcat) => { return string.IsNullOrWhiteSpace(refcat) || o.Cfg.DicNodes.ContainsKey(refcat); }).WithMessage("Selected catalog is not exists in configuration");
            this.RuleFor(x => x.RefCat2Guid).NotEmpty().WithMessage("Catalog type is not selected");
            this.RuleFor(x => x.RefCat2Guid).Must((o, refcat) => { return string.IsNullOrWhiteSpace(refcat) || o.Cfg.DicNodes.ContainsKey(refcat); }).WithMessage("Selected catalog is not exists in configuration");
        }
        private bool IsUnique(CatalogsManyToManyRelation val)
        {
            if (val.Parent == null)
            {
                return true;
            }
            if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
            {
                return true;
            }
            GroupCatalogsManyToManyRelations p = (GroupCatalogsManyToManyRelations)val.Parent;
            foreach (var t in p.ListCatalogsManyToManyRelations)
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
