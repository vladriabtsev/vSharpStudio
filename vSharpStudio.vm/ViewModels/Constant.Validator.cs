using System;
using System.Diagnostics;
using FluentValidation;
using FluentValidation.Results;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ConstantValidator
    {
        //private readonly ILogger? _logger = AppLogger.CreateLogger(nameof(ConstantValidator));
        public ConstantValidator()
        {
            this.GeneralRules();
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Must((o, name) => { return this.IsUnique(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            this.RuleFor(x => x.Name).Custom((name, cntx) =>
            {
                var c = (IConstant)cntx.InstanceToValidate;
                var mes = c.Cfg.GroupAppSolutions.FieldNameValidation(c.Name);
                if (!string.IsNullOrEmpty(mes))
                {
                    var vf = new ValidationFailure(nameof(c.Name), mes)
                    {
                        Severity = Severity.Error
                    };
                    cntx.AddFailure(vf);
                }
            });
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
                if (p.DataTypeEnum == EnumDataType.ENUMERATION)
                {
                    if (string.IsNullOrWhiteSpace(p.ConfigObjectGuid))
                    {
                        var vf = new ValidationFailure(nameof(p.ConfigObjectGuid),
                            $"Constant general type is {Enum.GetName<EnumDataType>(p.DataTypeEnum)}, but subtype is not selected")
                        {
                            Severity = Severity.Error
                        };
                        cntx.AddFailure(vf);
                        return;
                    }
                    var cfg = p.Cfg;
                    Debug.Assert(cfg.DicNodes.ContainsKey(p.ConfigObjectGuid));
                    var refObj = cfg.DicNodes[p.ConfigObjectGuid];
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
                                    $"Constant type is {refObj.GetType().Name}:'{refObj.Name}'. This type is marked for deletion, but this constant is not marked for deletion")
                                {
                                    Severity = Severity.Error
                                };
                                cntx.AddFailure(vf);
                            }
                        }
                    }
                }
                else if (p.DataTypeEnum == EnumDataType.CATALOG || p.DataTypeEnum == EnumDataType.DOCUMENT)
                {
                    if (string.IsNullOrWhiteSpace(p.ConfigObjectGuid))
                    {
                        var vf = new ValidationFailure(nameof(p.ConfigObjectGuid),
                            $"Constant general type is {Enum.GetName<EnumDataType>(p.DataTypeEnum)}, but subtype is not selected")
                        {
                            Severity = Severity.Error
                        };
                        cntx.AddFailure(vf);
                        return;
                    }
                    var cfg = p.Cfg;
                    Debug.Assert(cfg.DicNodes.ContainsKey(p.ConfigObjectGuid));
                    var refObj = cfg.DicNodes[p.ConfigObjectGuid];
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
                                    $"Constant type is {refObj.GetType().Name}:'{refObj.Name}'. This type is marked for deletion, but this constant is not marked for deletion")
                                {
                                    Severity = Severity.Error
                                };
                                cntx.AddFailure(vf);
                            }
                        }
                    }
                }
                else if (p.DataTypeEnum == EnumDataType.CATALOGS || p.DataTypeEnum == EnumDataType.DOCUMENTS)
                {
                    if (p.ListObjectRefs.Count == 0)
                    {
                        var vf = new ValidationFailure(nameof(p.ConfigObjectGuid),
                            $"Constant general type is {Enum.GetName<EnumDataType>(p.DataTypeEnum)}, but subtypes are not selected")
                        {
                            Severity = Severity.Error
                        };
                        cntx.AddFailure(vf);
                        return;
                    }
                    var cfg = p.Cfg;
                    foreach (var t in p.ListObjectRefs)
                    {
                        Debug.Assert(cfg.DicNodes.ContainsKey(t.ForeignObjectGuid));
                        var refObj = cfg.DicNodes[t.ForeignObjectGuid];
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
                                        $"Constant type is {refObj.GetType().Name}:'{refObj.Name}'. This type is marked for deletion, but this constant can use it as it's type")
                                    {
                                        Severity = Severity.Error
                                    };
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
