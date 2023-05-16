using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class RoleValidator
    {
        public RoleValidator()
        {
            this.RuleFor(x => x.DefaultConstantEditAccessSettings).NotNull().WithMessage("Can't be null");
            this.RuleFor(x => x.DefaultConstantEditAccessSettings).NotEqual(common.EnumConstantAccess.CN_BY_PARENT).WithMessage("Can't use 'By Parent'");
            this.RuleFor(x => x.DefaultConstantPrintAccessSettings).NotNull().WithMessage("Can't be null");
            this.RuleFor(x => x.DefaultConstantPrintAccessSettings).NotEqual(common.EnumPrintAccess.PR_BY_PARENT).WithMessage("Can't use 'By Parent'");

            this.RuleFor(x => x.DefaultCatalogEditAccessSettings).NotNull().WithMessage("Can't be null");
            this.RuleFor(x => x.DefaultCatalogEditAccessSettings).NotEqual(common.EnumCatalogDetailAccess.C_BY_PARENT).WithMessage("Can't use 'By Parent'");
            this.RuleFor(x => x.DefaultCatalogPrintAccessSettings).NotNull().WithMessage("Can't be null");
            this.RuleFor(x => x.DefaultCatalogPrintAccessSettings).NotEqual(common.EnumPrintAccess.PR_BY_PARENT).WithMessage("Can't use 'By Parent'");

            this.RuleFor(x => x.DefaultDocumentEditAccessSettings).NotNull().WithMessage("Can't be null");
            this.RuleFor(x => x.DefaultDocumentEditAccessSettings).NotEqual(common.EnumDocumentAccess.D_BY_PARENT).WithMessage("Can't use 'By Parent'");
            this.RuleFor(x => x.DefaultDocumentPrintAccessSettings).NotNull().WithMessage("Can't be null");
            this.RuleFor(x => x.DefaultDocumentPrintAccessSettings).NotEqual(common.EnumPrintAccess.PR_BY_PARENT).WithMessage("Can't use 'By Parent'");
        }
    }
}
