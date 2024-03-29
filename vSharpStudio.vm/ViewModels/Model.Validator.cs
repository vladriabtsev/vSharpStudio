using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ModelValidator
    {
        public ModelValidator()
        {
            this.RuleFor(x => x.IsUseNameComposition).Custom((val, cntx) =>
            {
                if (val)
                    return;
                var recom = " Conside change object name or enable usage composite names.";
                var m = (Model)cntx.InstanceToValidate;
                CheckObjectsWithDbTables(cntx, recom, m, true);
            });
            this.RuleFor(x => x.RecordVersionFieldName).NotEmpty();
            this.RuleFor(x => x.RecordVersionFieldName).Custom((name, cntx) => { CheckName(name, cntx); });
            this.RuleFor(x => x.PKeyName).Custom((name, cntx) => { CheckName(name, cntx); });
            this.RuleFor(x => x.PropertyCodeName).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.PropertyCodeName).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.PropertyCodeName).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.PropertyNameName).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.PropertyNameName).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.PropertyNameName).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.PropertyDescriptionName).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.PropertyDescriptionName).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.PropertyDescriptionName).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
            this.RuleFor(x => x.PropertyIsFolderName).NotEmpty().WithMessage(Config.ValidationMessages.NAME_CANT_BE_EMPTY);
            this.RuleFor(x => x.PropertyIsFolderName).Must(EnumerationValidator.IsStartNotWithDigit).WithMessage(Config.ValidationMessages.NAME_START_WITH_DIGIT);
            this.RuleFor(x => x.PropertyIsFolderName).Must(EnumerationValidator.IsNotContainsSpace).WithMessage(Config.ValidationMessages.NAME_CANT_CONTAINS_SPACE);
        }
        private static void CheckObjectsWithDbTables(ValidationContext<Model> cntx, string recom, Model m, bool isCheckTabs)
        {
            var dic = new Dictionary<string, ITreeConfigNode>();
            foreach (var t in m.GroupCatalogs.ListCatalogs)
            {
                if (string.IsNullOrWhiteSpace(t.CompositeName))
                    continue;
                if (dic.ContainsKey(t.CompositeName))
                {
                    var sb = GenMessage(dic, t, t.CompositeName);
                    sb.Append("'.");
                    sb.Append(recom);
                    cntx.AddFailure(sb.ToString());
                }
                else
                {
                    dic[t.CompositeName] = t;
                }
                if (isCheckTabs)
                    CheckTabs(cntx, dic, t.GroupDetails, recom);
            }
            foreach (var t in m.GroupDocuments.GroupListDocuments.ListDocuments)
            {
                if (string.IsNullOrWhiteSpace(t.CompositeName))
                    continue;
                if (dic.ContainsKey(t.CompositeName))
                {
                    var sb = GenMessage(dic, t, t.CompositeName);
                    sb.Append("'.");
                    sb.Append(recom);
                    cntx.AddFailure(sb.ToString());
                }
                else
                {
                    dic[t.CompositeName] = t;
                }
                if (isCheckTabs)
                    CheckTabs(cntx, dic, t.GroupDetails, recom);
            }
        }
        private static void CheckTabs(ValidationContext<Model> cntx, Dictionary<string, ITreeConfigNode> dic, IGroupListDetails tabs, string recom)
        {
            foreach (var t in tabs.ListDetails)
            {
                if (string.IsNullOrWhiteSpace(t.CompositeName))
                    continue;
                if (dic.ContainsKey(t.CompositeName))
                {
                    var sb = GenMessage(dic, t, t.CompositeName);
                    sb.Append("'.");
                    sb.Append(recom);
                    cntx.AddFailure(sb.ToString());
                }
                else
                {
                    dic[t.CompositeName] = t;
                }
                CheckTabs(cntx, dic, t.GroupDetails, recom);
            }
        }
        private static StringBuilder GenMessage(Dictionary<string, ITreeConfigNode> dic, ITreeConfigNode t, string tablename)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Same Db table name '");
            sb.Append(tablename);
            sb.Append("' is found for ");
            var prev = dic[tablename];
            GetObjectTypeDesc(sb, prev);
            sb.Append(prev.Name);
            sb.Append("' and for ");
            GetObjectTypeDesc(sb, t);
            sb.Append(t.Name);
            return sb;
        }
        private static void GetObjectTypeDesc(StringBuilder sb, ITreeConfigNode prev)
        {
            if (prev is Catalog)
            {
                sb.Append("catalog '");

            }
            else if (prev is Document)
            {
                sb.Append("document '");

            }
            else if (prev is Detail)
            {
                sb.Append("properties tab '");

            }
            else
                throw new Exception();
        }
        private void CheckName(string name, ValidationContext<Model> cntx)
        {
            var model = (Model)cntx.InstanceToValidate;
            int nerr = 0;
            int nerrMax = 10;

            foreach (var t in model.GroupConstantGroups.ListConstantGroups)
            {
                if (nerr >= nerrMax) return;
                foreach (var tt in t.ListConstants)
                {
                    if (nerr >= nerrMax) return;
                    if (model.RecordVersionFieldName == tt.Name)
                    {
                        nerr++;
                        cntx.AddFailure(new ValidationFailure(nameof(model.RecordVersionFieldName), $"Version name has to be unique. Same name is used for constant field in constant group '{t.Name}'"));
                    }
                }
            }
            if (nerr >= nerrMax) return;
            foreach (var t in model.GroupCatalogs.ListCatalogs)
            {
                foreach (var tt in t.GroupProperties.ListProperties)
                {
                    if (nerr >= nerrMax) return;
                    if (model.RecordVersionFieldName == tt.Name)
                    {
                        nerr++;
                        cntx.AddFailure(new ValidationFailure(nameof(model.RecordVersionFieldName), $"Version name has to be unique. Same name is used for field in catalog '{t.Name}'"));
                    }
                }
                foreach (var tt in t.GroupDetails.ListDetails)
                {
                    if (nerr >= nerrMax) return;
                    this.ValidateDetail(cntx, model, "catalog detail " + t.Name, tt, nerrMax, ref nerr);
                }
                if (t.UseTree && t.UseSeparateTreeForFolders)
                {
                    foreach (var tt in t.Folder.GroupProperties.ListProperties)
                    {
                        if (nerr >= nerrMax) return;
                        if (model.RecordVersionFieldName == tt.Name)
                        {
                            nerr++;
                            cntx.AddFailure(new ValidationFailure(nameof(model.RecordVersionFieldName), $"Version name has to be unique. Same name is used for field in catalog folder '{t.Name}'"));
                        }
                    }
                    foreach (var tt in t.Folder.GroupDetails.ListDetails)
                    {
                        if (nerr >= nerrMax) return;
                        this.ValidateDetail(cntx, model, "catalog folder detail " + t.Name, tt, nerrMax, ref nerr);
                    }
                }
            }
            if (nerr >= nerrMax) return;
            foreach (var t in model.GroupDocuments.DocumentTimeline.ListProperties)
            {
                if (nerr >= nerrMax) return;
                if (model.RecordVersionFieldName == t.Name)
                {
                    nerr++;
                    cntx.AddFailure(new ValidationFailure(nameof(model.RecordVersionFieldName), $"Version name has to be unique. Same name is used for shared field for documents"));
                }
            }
            if (nerr >= nerrMax) return;
            foreach (var t in model.GroupDocuments.GroupListDocuments.ListDocuments)
            {
                foreach (var tt in t.GroupProperties.ListProperties)
                {
                    if (nerr >= nerrMax) return;
                    if (model.RecordVersionFieldName == tt.Name)
                    {
                        nerr++;
                        cntx.AddFailure(new ValidationFailure(nameof(model.RecordVersionFieldName), $"Version name has to be unique. Same name is used for field in document '{t.Name}'"));
                    }
                }
                foreach (var tt in t.GroupDetails.ListDetails)
                {
                    if (nerr >= nerrMax) return;
                    this.ValidateDetail(cntx, model, "document detail " + t.Name, tt, nerrMax, ref nerr);
                }
            }
        }
        private void ValidateDetail(ValidationContext<Model> cntx, Model set, string path, IDetail t, int nerrMax, ref int nerr)
        {
            foreach (var tt in t.GroupProperties.ListProperties)
            {
                if (nerr >= nerrMax) return;
                if (set.RecordVersionFieldName == tt.Name)
                {
                    nerr++;
                    cntx.AddFailure(new ValidationFailure(nameof(set.RecordVersionFieldName), $"Version name has to be unique. Same name is used for field in '{path}.{t.Name}'"));
                }
            }
            foreach (var tt in t.GroupDetails.ListDetails)
            {
                if (nerr >= nerrMax) return;
                this.ValidateDetail(cntx, set, path + "." + t.Name, tt, nerrMax, ref nerr);
            }
        }
    }
}
