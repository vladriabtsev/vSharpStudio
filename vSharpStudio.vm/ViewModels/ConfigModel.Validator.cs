using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Text;
using FluentValidation;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class ConfigModelValidator
    {
        public ConfigModelValidator()
        {
            this.RuleFor(x => x.IsUseGroupPrefix).Custom((val, cntx) =>
            {
                if (val)
                    return;
                var recom = " Conside change object name or enable group prefixes.";
                var m = (ConfigModel)cntx.InstanceToValidate;
                CheckObjectsWithDbTables(cntx, recom, m, false);
            });
            this.RuleFor(x => x.IsUseCompositeNames).Custom((val, cntx) =>
            {
                if (val)
                    return;
                var recom = " Conside change object name or enable usage composite names.";
                var m = (ConfigModel)cntx.InstanceToValidate;
                CheckObjectsWithDbTables(cntx, recom, m, true);
            });
        }

        private static void CheckObjectsWithDbTables(FluentValidation.Validators.CustomContext cntx, string recom, ConfigModel m, bool isCheckTabs)
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
                    CheckTabs(cntx, dic, t.GroupPropertiesTabs, recom);
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
                    CheckTabs(cntx, dic, t.GroupPropertiesTabs, recom);
            }
        }

        private static void CheckTabs(FluentValidation.Validators.CustomContext cntx, Dictionary<string, ITreeConfigNode> dic, GroupListPropertiesTabs tabs, string recom)
        {
            foreach (var t in tabs.ListPropertiesTabs)
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
                CheckTabs(cntx, dic, t.GroupPropertiesTabs, recom);
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
            else if (prev is PropertiesTab)
            {
                sb.Append("properties tab '");

            }
            else
                throw new Exception();
        }
    }
}
