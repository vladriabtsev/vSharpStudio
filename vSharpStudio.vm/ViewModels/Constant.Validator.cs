using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ConstantValidator
    {
        public ConstantValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Must((o, name) => { return this.IsUnique(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            // RuleFor(x => x.MinValueString).NotEmpty().WithMessage("Please provide minimum value").WithSeverity(Severity.Warning);
            // RuleFor(x => x.MaxValueString).NotEmpty().WithMessage("Please provide maximum value").WithSeverity(Severity.Warning);
            // RuleFor(x => x.MinValueString).Must(ParsableToBigInteger).WithMessage("Can't parse to integer");
            // RuleFor(x => x.MaxValueString).Must(ParsableToBigInteger).WithMessage("Can't parse to integer");
            // RuleFor(x => x.Length).GreaterThan(0u);
            // RuleFor(x => x.Accuracy).LessThan(x => x.Length);
            // RuleFor(x => x.ObjectName).NotEmpty().When(x => x.DataTypeEnum == EnumDataType.Catalog).WithMessage("Please select catalog name");
            // RuleFor(x => x.ObjectName).NotEmpty().When(x => x.DataTypeEnum == EnumDataType.Document).WithMessage("Please select document name");
            this.RuleFor(x => x.IsMarkedForDeletion).Custom((name, cntx) =>
            {
                var p = (Constant)cntx.InstanceToValidate;
                if (p.IsMarkedForDeletion)
                    return;
                if (p.DataTypeEnum == EnumDataType.CATALOG || p.DataTypeEnum == EnumDataType.ENUMERATION || p.DataTypeEnum == EnumDataType.DOCUMENT)
                {
                    var cfg = p.Cfg;
                    Debug.Assert(cfg.DicNodes.ContainsKey(p.ObjectGuid));
                    var refObj = cfg.DicNodes[p.ObjectGuid];
                    Debug.Assert(refObj != null);
                    var refObjEditable = refObj as IEditableNode;
                    Debug.Assert(refObjEditable != null);
                    if (refObjEditable.IsMarkedForDeletion)
                    {
                        if (p.Parent is IEditableNode pe)
                        {
                            if (!p.IsMarkedForDeletion && !pe.IsMarkedForDeletion)
                            {
                                var vf = new ValidationFailure(nameof(p.IsMarkedForDeletion),
                                    $"Constant type is {refObj.GetType().Name}:'{refObj.Name}'. This type is marked for deletion, but this constant is not marked for deletion");
                                vf.Severity = Severity.Error;
                                cntx.AddFailure(vf);
                            }
                        }
                    }
                }
                else if (p.DataTypeEnum == EnumDataType.CATALOGS || p.DataTypeEnum == EnumDataType.DOCUMENTS)
                {
                    var cfg = p.Cfg;
                    foreach(var t in p.ListObjectGuids)
                    {
                        Debug.Assert(cfg.DicNodes.ContainsKey(t));
                        var refObj = cfg.DicNodes[t];
                        Debug.Assert(refObj != null);
                        var refObjEditable = refObj as IEditableNode;
                        Debug.Assert(refObjEditable != null);
                        if (refObjEditable.IsMarkedForDeletion)
                        {
                            if (p.Parent is IEditableNode pe)
                            {
                                if (!p.IsMarkedForDeletion && !pe.IsMarkedForDeletion)
                                {
                                    var vf = new ValidationFailure(nameof(p.IsMarkedForDeletion),
                                        $"Constant type is {refObj.GetType().Name}:'{refObj.Name}'. This type is marked for deletion, but this constant can use it as it's type");
                                    vf.Severity = Severity.Error;
                                    cntx.AddFailure(vf);
                                }
                            }
                        }

                    }
                }
            });
        }
        private bool IsUnique(Constant val)
        {
            if (val.Parent == null)
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
            {
                return true;
            }

            GroupListConstants p = (GroupListConstants)val.Parent;
            foreach (var t in p.ListConstants)
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
