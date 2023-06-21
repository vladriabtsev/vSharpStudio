using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Google.Protobuf.WellKnownTypes;
using vSharpStudio.common;
using vSharpStudio.common.ViewModels;

namespace vSharpStudio.vm.ViewModels
{
    public partial class DocumentNumberPropertySettings : IParent
    {
        public override string ToString()
        {
            string unique = "";
            var conv = new EnumDescriptionTypeConverter(typeof(EnumMonths));
            switch (this.ScopeOfUnique)
            {
                case EnumDocNumberUniqueScope.DOC_UNIQUE_FOREVER:
                    unique = "Unique";
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_YEAR:
                    unique = $"Year: {conv.ConvertTo(null, null, this.ScopePeriodStartMonth, typeof(string))} {this.ScopePeriodStartMonthDay}";
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_QUATER:
                    unique = $"Quater: {conv.ConvertTo(null, null, this.ScopePeriodStartMonth, typeof(string))} {this.ScopePeriodStartMonthDay}";
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_MONTH:
                    unique = "Month";
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_WEEK:
                    unique = "Month";
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_DAY:
                    unique = "Month";
                    break;
                case EnumDocNumberUniqueScope.DOC_UNIQUE_NOT_REQUIRED:
                    unique = "Not Unique";
                    break;
                default:
                    throw new NotImplementedException();
            }
            if (string.IsNullOrWhiteSpace(this.SequenceGuid))
            {
                switch (this.SequenceType)
                {
                    case EnumCodeType.Number:
                        return $"Number{this.Prefix}{this.MaxSequenceLength}-{unique}";
                    case EnumCodeType.Text:
                        return $"Text{this.Prefix}{this.MaxSequenceLength}-{unique}";
                    default:
                        throw new NotImplementedException();
                }
            }
            return $"{this.ParentDocument.Cfg.DicNodes[this.SequenceGuid].Name}-{unique}";
        }
        [Browsable(false)]
        public Document ParentDocument { get { Debug.Assert(this.Parent != null); return (Document)this.Parent; } }
        partial void OnCreated()
        {
            this.ScopePeriodStartMonth = EnumMonths.MONTH_JANUARY;
            this.ScopePeriodStartMonthDay = 1;
            //Init();
        }
        //protected override void OnInitFromDto()
        //{
        //    Init();
        //}
        //private void Init()
        //{
        //    this.ListRoles.OnAddingAction = (t) =>
        //    {
        //        t.IsNew = true;
        //    };
        //    this.ListRoles.OnAddedAction = (t) =>
        //    {
        //        t.OnAdded();
        //    };
        //    this.ListRoles.OnRemovedAction = (t) =>
        //    {
        //        this.OnRemoveChild();
        //    };
        //    this.ListRoles.OnClearedAction = () =>
        //    {
        //        this.OnRemoveChild();
        //    };
        //}
        //protected override void OnIsChangedChanged()
        //{
        //    if (this.Parent != null && this.IsChanged)
        //        this.Parent.IsChanged = true;
        //}
        partial void OnMaxSequenceLengthChanged()
        {
            this.ParentDocument.NotifyCodePropertySettingsChanged();
        }
        partial void OnPrefixChanged()
        {
            this.ParentDocument.NotifyCodePropertySettingsChanged();
        }
        partial void OnSequenceTypeChanged()
        {
            this.ParentDocument.NotifyCodePropertySettingsChanged();
        }
        partial void OnScopeOfUniqueChanged()
        {
            this.ParentDocument.NotifyCodePropertySettingsChanged();
        }
        partial void OnScopePeriodStartMonthChanged()
        {
            this.ParentDocument.NotifyCodePropertySettingsChanged();
        }
        partial void OnScopePeriodStartMonthDayChanged()
        {
            this.ParentDocument.NotifyCodePropertySettingsChanged();
        }
        partial void OnSequenceGuidChanged()
        {
            this.NotifyPropertyChanged(() => this.PropertyDefinitions);
            this.ParentDocument.NotifyCodePropertySettingsChanged();
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            if (!string.IsNullOrWhiteSpace(this.SequenceGuid))
            {
                lst.Add(this.GetPropertyName(() => this.SequenceType));
                lst.Add(this.GetPropertyName(() => this.MaxSequenceLength));
                lst.Add(this.GetPropertyName(() => this.Prefix));
            }
            switch (this.ScopeOfUnique)
            {
                case EnumDocNumberUniqueScope.DOC_UNIQUE_QUATER:
                case EnumDocNumberUniqueScope.DOC_UNIQUE_YEAR:
                    break;
                default:
                    lst.Add(this.GetPropertyName(() => this.ScopePeriodStartMonth));
                    lst.Add(this.GetPropertyName(() => this.ScopePeriodStartMonthDay));
                    break;
            }
            return lst.ToArray();
        }
        public string GetDocNumberClrTypeName()
        {
            IConfig cfg = this.ParentDocument.Cfg;
            string propertyDocNumberGuid = this.ParentDocument.PropertyDocNumberGuid;
            GroupListProperties groupListProperties = this.ParentDocument.GroupProperties;
            IProperty? prp = null;
            switch (this.SequenceType)
            {
                case EnumCodeType.Number:
                    prp = cfg.Model.GetPropertyDocNumberInt(groupListProperties, propertyDocNumberGuid,
                        this.MaxSequenceLength);
                    break;
                case EnumCodeType.Text:
                    prp = cfg.Model.GetPropertyDocNumberString(groupListProperties, propertyDocNumberGuid,
                        this.MaxSequenceLength + (uint)this.Prefix.Length);
                    break;
                default:
                    throw new NotImplementedException();
            }
            Debug.Assert(prp != null);
            return prp.DataType.ClrTypeName;
        }
        public string GetCodeClrTypeName()
        {
            IConfig? cfg = null;
            string? propertyCodeGuid = null;

            GroupListProperties? groupListProperties = null;
            cfg = this.ParentDocument.Cfg;
            propertyCodeGuid = this.ParentDocument.PropertyDocNumberGuid;
            groupListProperties = this.ParentDocument.GroupProperties;
            Debug.Assert(cfg != null);
            Debug.Assert(propertyCodeGuid != null);
            Debug.Assert(groupListProperties != null);
            IProperty? prp = null;
            switch (this.SequenceType)
            {
                case EnumCodeType.Number:
                    prp = cfg.Model.GetPropertyDocNumberInt(groupListProperties, propertyCodeGuid,
                        this.MaxSequenceLength);
                    break;
                case EnumCodeType.Text:
                    prp = cfg.Model.GetPropertyDocNumberString(groupListProperties, propertyCodeGuid,
                        this.MaxSequenceLength + (uint)this.Prefix.Length);
                    break;
                default:
                    throw new NotImplementedException();
            }
            Debug.Assert(prp != null);
            return prp.DataType.ClrTypeName;
        }
        public string GetNextCodeProc()
        {
            var sb = new StringBuilder();
            sb.Append(this.GetCodeClrTypeName());
            sb.Append(" res = ");
            sb.Append(this.GetCodeStartStr());
            sb.AppendLine(";");
            sb.AppendLine("if (code != null)");
            sb.AppendLine("{");
            switch (this.SequenceType)
            {
                case EnumCodeType.Number:
                    sb.AppendLine("\tres = code.Value + 1;");
                    sb.Append("\tif (res > ");
                    sb.Append(System.Numerics.BigInteger.Parse(new string('9', (int)this.MaxSequenceLength)));
                    sb.AppendLine(")");
                    sb.AppendLine("\t\tres = 1;");
                    break;
                case EnumCodeType.Text:
                    var pref = this.Prefix.Trim();
                    if (!string.IsNullOrWhiteSpace(this.Prefix))
                    {
                        sb.Append("\tcode = code.Substring(");
                        sb.Append(pref.Length);
                        sb.AppendLine(");");
                    }
                    sb.AppendLine("\tvar i = System.Numerics.BigInteger.Parse(code) + 1;");
                    sb.Append("\tif (i > ");
                    sb.Append(System.Numerics.BigInteger.Parse(new string('9', (int)this.MaxSequenceLength)));
                    sb.AppendLine(")");
                    sb.Append("\t\tres = ");
                    sb.Append(this.GetCodeStartStr());
                    sb.AppendLine(";");
                    sb.AppendLine("\telse");
                    sb.Append("\t\tres = \"");
                    sb.Append(pref);
                    sb.Append("\" + i.ToString(\"D");
                    sb.Append(this.MaxSequenceLength);
                    sb.AppendLine("\");");
                    break;
                default:
                    throw new NotImplementedException();
            }
            sb.Append("}");
            return sb.ToString();
        }
        public string GetCodeCheckProc()
        {
            Debug.Assert(this.Parent != null);
            var cfg = this.Parent.Cfg;
            var pname = cfg.Model.PropertyCodeName;
            var sb = new StringBuilder();
            switch (this.SequenceType)
            {
                case EnumCodeType.Number:
                    sb.AppendLine("if (isMinAllowedInsert && code < 1) return true;");
                    sb.Append("if (code < 1 || code > ");
                    var rmax = new string('9', (int)this.MaxSequenceLength);
                    sb.Append(rmax);
                    sb.AppendLine(")");
                    sb.Append("\tthrow new BusinessException(EnumExceptionType.CodeOutsideAllowedRange, $\"Catalog property '");
                    sb.Append(pname);
                    sb.Append("'={code}. It is outside expected range from 1 to ");
                    sb.Append(rmax);
                    sb.AppendLine("\");");
                    break;
                case EnumCodeType.Text:
                    sb.AppendLine("if (isMinAllowedInsert && code.Length == 0) return true;");
                    var pref = this.Prefix.Trim();
                    if (pref.Length > 0)
                    {
                        sb.Append("if (!code.StartsWith(\"");
                        sb.Append(pref);
                        sb.AppendLine("\"))");
                        sb.Append("\tthrow new BusinessException(EnumExceptionType.CodeOutsideAllowedRange, $\"Catalog property '");
                        sb.Append(pname);
                        sb.Append("'=\\\"{code}\\\". There is no expected prefix \\\"");
                        sb.Append(pref);
                        sb.AppendLine("\\\"\");");
                        sb.Append("code = code.Substring(");
                        sb.Append(pref.Length);
                        sb.AppendLine(");");
                    }
                    sb.Append("if (code.Length != ");
                    sb.Append(this.MaxSequenceLength);
                    sb.AppendLine(")");
                    sb.Append("\tthrow new BusinessException(EnumExceptionType.CodeOutsideAllowedRange, $\"Catalog property '");
                    sb.Append(pname);
                    sb.Append("'=\\\"");
                    sb.Append(pref);
                    sb.Append("{code}\\\". Length of sequence not equal ");
                    sb.Append(this.MaxSequenceLength);
                    sb.AppendLine("\");");
                    sb.AppendLine("if (!System.Numerics.BigInteger.TryParse(code, out var i))");
                    sb.Append("\tthrow new BusinessException(EnumExceptionType.CodeOutsideAllowedRange, $\"Catalog property '");
                    sb.Append(pname);
                    sb.Append("'=\\\"");
                    sb.Append(pref);
                    sb.AppendLine("{code}\\\". Can't parse sequence \\\"{code}\\\" to number\");");
                    sb.AppendLine("if (isMinAllowedInsert && i < 1) return true;");
                    sb.Append("if (i < 1 || i > ");
                    var rmax2 = new string('9', (int)this.MaxSequenceLength);
                    sb.Append(rmax2);
                    sb.AppendLine(")");
                    sb.Append("\tthrow new BusinessException(EnumExceptionType.CodeOutsideAllowedRange, $\"Catalog property '");
                    sb.Append(pname);
                    sb.Append("'=\\\"{code}\\\". It is outside expected range from '");
                    var rmin2 = new string('0', (int)this.MaxSequenceLength - 1) + "1";
                    sb.Append(rmin2);
                    sb.Append("' to '");
                    sb.Append(rmax2);
                    sb.AppendLine("'\");");
                    break;
                default:
                    throw new NotImplementedException();
            }
            sb.Append("return true;");
            return sb.ToString();
        }
        public string GetCodeStartStr()
        {
            switch (this.SequenceType)
            {
                case EnumCodeType.Number:
                    return "1";
                case EnumCodeType.Text:
                    var pref = this.Prefix.Trim();
                    string fmt = "D" + this.MaxSequenceLength;
                    return $"\"{pref}{1.ToString(fmt)}\"";
                default:
                    throw new NotImplementedException();
            }
        }
        public string GetCodeEndStr()
        {
            switch (this.SequenceType)
            {
                case EnumCodeType.Number:
                    return new string('9', (int)this.MaxSequenceLength);
                case EnumCodeType.Text:
                    var pref = this.Prefix.Trim();
                    return $"\"{pref}{new string('9', (int)this.MaxSequenceLength)}\"";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
