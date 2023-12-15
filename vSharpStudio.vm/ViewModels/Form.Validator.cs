using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class FormValidator
    {
        public FormValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.Name).Must(IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.Name).Must(IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.Name).Must((o, name) => { return this.IsUnique(o); }).WithMessage(Config.ValidationMessages.NAME_HAS_TO_BE_UNIQUE);
            this.RuleFor(x => x.EnumFormType).Custom((ft, cntx) =>
                {
                    if (ft== FormType.FormTypeNotSelected)
                    {
                        var vf = new ValidationFailure(cntx.PropertyPath, $"Form type is not selected");
                        vf.Severity = Severity.Warning;
                        cntx.AddFailure(vf);
                        return;
                    }
                    var instance = (Form)cntx.InstanceToValidate;
                    if (ft == FormType.ListCustom || ft == FormType.ListDefault)
                    {
                        if (instance.ListSelectedNotSpecialProperties.Count == 0)
                        {
                            var vf = new ValidationFailure(cntx.PropertyPath, $"No properties are selected for view");
                            vf.Severity = Severity.Warning;
                            cntx.AddFailure(vf);
                        }
                        if (instance.UseSeparateTreeForFolders && instance.ListSeparateTreeSelectedNotSpecialProperties.Count == 0)
                        {
                            var vf = new ValidationFailure(cntx.PropertyPath, $"No properties are selected for separate tree view");
                            vf.Severity = Severity.Warning;
                            cntx.AddFailure(vf);
                        }
                    }
                    else
                    {
                        var vf = new ValidationFailure(cntx.PropertyPath, $"Form type {Enum.GetName<FormType>(ft)} is not supported yet");
                        vf.Severity = Severity.Warning;
                        cntx.AddFailure(vf);
                    }
                });

            //    this.RuleFor(x => x.Name).Must((o, name) =>
            //    {
            //        if (o.ListEnumerationPairs.Count < 2)
            //            return true;
            //        foreach (var t in o.ListEnumerationPairs)
            //        {
            //            if (t.IsDefault)
            //                return true;
            //        }
            //        return false;
            //    }).WithMessage("One enumeration value expected be selected as default");

            //    this.RuleFor(p => p.DataTypeLength).Must((p, y) =>
            //    {
            //        if (p.DataTypeEnum != common.EnumEnumerationType.STRING_VALUE)
            //        {
            //            return true;
            //        }

            //        if (y < 0)
            //        {
            //            return false;
            //        }

            //        return true;
            //    }).WithMessage(Config.ValidationMessages.TYPE_LENGTH_POSITIVE);
            //    this.RuleFor(p => p.DataTypeLength).Must((p, y) =>
            //    {
            //        if (p.DataTypeEnum != common.EnumEnumerationType.STRING_VALUE)
            //        {
            //            return true;
            //        }

            //        if (y == 0)
            //        {
            //            return false;
            //        }

            //        return true;
            //    }).WithMessage(Config.ValidationMessages.TYPE_LENGTH_GREATER_THAN_ZERO);

            //    this.RuleFor(x => x.DataTypeLength).Custom((path, cntx) =>
            //    {
            //        var pg = (Enumeration)cntx.InstanceToValidate;
            //        var prev = (Enumeration?)pg.PrevCurrentVersion();
            //        var ver = "CURRENT";
            //        if (prev != null && pg.DataTypeEnum == prev.DataTypeEnum)
            //        {
            //            if (pg.DataTypeLength < prev.DataTypeLength)
            //            {
            //                var vf = new ValidationFailure(nameof(pg.DataTypeLength), $"Comparison with previous {ver} version. Length was reduced from '{prev.DataTypeLength}' to '{pg.DataTypeLength}'");
            //                vf.Severity = Severity.Warning;
            //                cntx.AddFailure(vf);
            //            }
            //        }
            //        prev = (Enumeration?)pg.PrevStableVersion();
            //        ver = "STABLE";
            //        if (prev != null && pg.DataTypeEnum == prev.DataTypeEnum)
            //        {
            //            if (pg.DataTypeLength < prev.DataTypeLength)
            //            {
            //                var vf = new ValidationFailure(nameof(pg.DataTypeLength), $"Comparison with previous {ver} version. Length was reduced from '{prev.DataTypeLength}' to '{pg.DataTypeLength}'");
            //                vf.Severity = Severity.Warning;
            //                cntx.AddFailure(vf);
            //            }
            //        }
            //    });

            //    this.RuleFor(x => x.DataTypeEnum).Custom((path, cntx) =>
            //    {
            //        var pg = (Enumeration)cntx.InstanceToValidate;
            //        var prev = (Enumeration?)pg.PrevCurrentVersion();
            //        var ver = "CURRENT";
            //        if (prev != null && pg.DataTypeEnum != prev.DataTypeEnum)
            //        {
            //            var vf = new ValidationFailure(nameof(pg.DataTypeEnum),
            //                $"Comparison with previous {ver} version. Data type was changed from '{Enum.GetName(typeof(EnumEnumerationType), prev.DataTypeEnum)}' to '{Enum.GetName(typeof(EnumEnumerationType), pg.DataTypeEnum)}'");
            //            vf.Severity = Severity.Warning;
            //            cntx.AddFailure(vf);
            //        }
            //        prev = (Enumeration?)pg.PrevStableVersion();
            //        ver = "STABLE";
            //        if (prev != null && pg.DataTypeEnum != prev.DataTypeEnum)
            //        {
            //            var vf = new ValidationFailure(nameof(pg.DataTypeEnum),
            //                $"Comparison with previous {ver} version. Data type was changed from '{Enum.GetName(typeof(EnumEnumerationType), prev.DataTypeEnum)}' to '{Enum.GetName(typeof(EnumEnumerationType), pg.DataTypeEnum)}'");
            //            vf.Severity = Severity.Warning;
            //            cntx.AddFailure(vf);
            //        }
            //    });
        }

        public static bool IsStartNotWithDigit(string val)
        {
            if (string.IsNullOrWhiteSpace(val))
            {
                return true;
            }

            char b = val[0];
            return !char.IsDigit(b);
        }

        public static bool IsNotContainsSpace(string val)
        {
            if (string.IsNullOrWhiteSpace(val))
            {
                return true;
            }

            return !val.Contains(" ");
        }

        private bool IsUnique(Form val)
        {
            if (val.Parent == null)
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(val.Name)) // handled by another rule
            {
                return true;
            }

            var p = val.ParentGroupListForms;
            foreach (var t in p.ListForms)
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
