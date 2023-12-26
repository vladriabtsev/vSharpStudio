﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ManyToManyDocumentsRelationValidator
    {
        public ManyToManyDocumentsRelationValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Must((o, name) => { return this.IsUnique(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            this.RuleFor(x => x.GuidDoc1).NotEmpty().WithMessage("Document type is not selected");
            this.RuleFor(x => x.GuidDoc1).Must((o, refcat) => { return string.IsNullOrWhiteSpace(refcat) || o.Cfg.DicNodes.ContainsKey(refcat); }).WithMessage("Selected document is not exists in configuration");
            this.RuleFor(x => x.GuidDoc2).NotEmpty().WithMessage("Document type is not selected");
            this.RuleFor(x => x.GuidDoc2).Must((o, refcat) => { return string.IsNullOrWhiteSpace(refcat) || o.Cfg.DicNodes.ContainsKey(refcat); }).WithMessage("Selected document is not exists in configuration");
        }
        private bool IsUnique(ManyToManyDocumentsRelation val)
        {
            if (val.Parent == null)
            {
                return true;
            }
            if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
            {
                return true;
            }
            ManyToManyGroupDocumentsRelations p = (ManyToManyGroupDocumentsRelations)val.Parent;
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