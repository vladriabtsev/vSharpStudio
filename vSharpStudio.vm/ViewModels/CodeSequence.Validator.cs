﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Results;

namespace vSharpStudio.vm.ViewModels
{
    public partial class CodeSequenceValidator
    {
        public CodeSequenceValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Custom((name, cntx) =>
            {
                var p = (CodeSequence)cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                var gs = (GroupListSequences)p.Parent;
                foreach (var t in gs.ListSequences)
                {
                    if (t.Guid != p.Guid && name == t.Name)
                    {
                        var vf = new ValidationFailure(nameof(p.Name),
                            $"Sequence name is not unique '{name}'");
                        vf.Severity = Severity.Error;
                        cntx.AddFailure(vf);
                    }
                }
            });
            this.RuleFor(x => x.MaxSequenceLength).GreaterThan(0u);
            this.RuleFor(x => x.MaxSequenceLength).LessThan(15u);
            this.RuleFor(x => x.Prefix).Custom((prefix, cntx) =>
            {
                var p = (CodeSequence)cntx.InstanceToValidate;
                if (p.Parent == null)
                    return;
                if (string.IsNullOrWhiteSpace(p.Prefix))
                    p.Prefix = "";
                if (p.Prefix.Length > 0 && (p.SequenceType == common.EnumCodeType.Number || p.SequenceType == common.EnumCodeType.AutoNumber))
                {
                    var vf = new ValidationFailure(nameof(p.Prefix),
                        $"Prefix for numbers is not used. Expected to be empty");
                    vf.Severity = Severity.Error;
                    cntx.AddFailure(vf);
                }
            });
        }
    }
}