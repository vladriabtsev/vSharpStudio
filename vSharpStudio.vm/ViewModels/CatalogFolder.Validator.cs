using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Results;

namespace vSharpStudio.vm.ViewModels
{
    public partial class CatalogFolderValidator
    {
        public CatalogFolderValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Custom((name, cntx) =>
            {
                var p = (CatalogFolder)cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                var c = (Catalog)p.Parent;
                if (c.UseTree && c.UseSeparateTreeForFolders)
                {
                    foreach (var t in c.GroupDetails.ListDetails)
                    {
                        if (name == t.Name)
                        {
                            var vf = new ValidationFailure(nameof(p.Name),
                                $"Catalog folder name can't be same as properties tab name '{name}'");
                            vf.Severity = Severity.Error;
                            cntx.AddFailure(vf);
                        }
                    }
                }
            });
        }
    }
}
