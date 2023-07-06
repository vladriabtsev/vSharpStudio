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
            IProperty? prp = null;
            if (string.IsNullOrWhiteSpace(this.SequenceGuid))
            {
                prp=this.GetDocNumberProperty(this.ParentDocument, this.SequenceType, this.MaxSequenceLength, this.Prefix);
            }
            else
            {
                var seq = (EnumeratorSequence)this.ParentDocument.Cfg.DicNodes[this.SequenceGuid];
                prp=this.GetDocNumberProperty(this.ParentDocument, seq.SequenceType, seq.MaxSequenceLength, seq.Prefix);
            }
            Debug.Assert(prp != null);
            return prp.DataType.ClrTypeName;
        }
        private IProperty GetDocNumberProperty(Document doc, EnumCodeType sequenceType, uint maxSequenceLength, string prefix)
        {
            string propertyDocNumberGuid = doc.PropertyDocNumberGuid;
            GroupListProperties groupListProperties = doc.GroupProperties;
            IProperty? prp = null;
            switch (sequenceType)
            {
                case EnumCodeType.Number:
                    prp = doc.Cfg.Model.GetPropertyDocNumberInt(groupListProperties, propertyDocNumberGuid,
                        maxSequenceLength);
                    break;
                case EnumCodeType.Text:
                    prp = doc.Cfg.Model.GetPropertyDocNumberString(groupListProperties, propertyDocNumberGuid,
                        maxSequenceLength + (uint)prefix.Length);
                    break;
                default:
                    throw new NotImplementedException();
            }
            Debug.Assert(prp != null);
            return prp;
        }
        private void GetNextCodeProc(StringBuilder sb, EnumCodeType sequenceType, uint maxSequenceLength, string prefix)
        {
            switch (sequenceType)
            {
                case EnumCodeType.Number:
                    sb.AppendLine("\tres = code.Value + 1;");
                    sb.Append("\tif (res > ");
                    sb.Append(System.Numerics.BigInteger.Parse(new string('9', (int)maxSequenceLength)));
                    sb.AppendLine(")");
                    sb.AppendLine("\t\tres = 1;");
                    break;
                case EnumCodeType.Text:
                    var pref = prefix.Trim();
                    if (!string.IsNullOrWhiteSpace(prefix))
                    {
                        sb.Append("\tcode = code.Substring(");
                        sb.Append(pref.Length);
                        sb.AppendLine(");");
                    }
                    sb.AppendLine("\tvar i = System.Numerics.BigInteger.Parse(code) + 1;");
                    sb.Append("\tif (i > ");
                    sb.Append(System.Numerics.BigInteger.Parse(new string('9', (int)maxSequenceLength)));
                    sb.AppendLine(")");
                    sb.Append("\t\tres = ");
                    sb.Append(this.GetCodeStartStr());
                    sb.AppendLine(";");
                    sb.AppendLine("\telse");
                    sb.Append("\t\tres = \"");
                    sb.Append(pref);
                    sb.Append("\" + i.ToString(\"D");
                    sb.Append(maxSequenceLength);
                    sb.AppendLine("\");");
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        public EnumCodeType GetSequenceType()
        {
            if (string.IsNullOrWhiteSpace(this.SequenceGuid))
                return this.SequenceType;
            var seq = (EnumeratorSequence)this.ParentDocument.Cfg.DicNodes[this.SequenceGuid];
            return seq.SequenceType;
        }
        public string GetNextCodeProc()
        {
            var sb = new StringBuilder();
            sb.Append(this.GetDocNumberClrTypeName());
            sb.Append(" res = ");
            sb.Append(this.GetCodeStartStr());
            sb.AppendLine(";");
            sb.AppendLine("if (code != null)");
            sb.AppendLine("{");
            if (string.IsNullOrWhiteSpace(this.SequenceGuid))
            {
                this.GetNextCodeProc(sb, this.SequenceType, this.MaxSequenceLength, this.Prefix);
            }
            else
            {
                var seq = (EnumeratorSequence)this.ParentDocument.Cfg.DicNodes[this.SequenceGuid];
                this.GetNextCodeProc(sb, seq.SequenceType, seq.MaxSequenceLength, seq.Prefix);
            }
            sb.Append("}");
            return sb.ToString();
        }
        private void GetCodeCheckProc(StringBuilder sb, EnumCodeType sequenceType, uint maxSequenceLength, string prefix, string pname)
        {
            sb.AppendLine("if (code == null) return false; // need auto set new code");
            switch (sequenceType)
            {
                case EnumCodeType.Number:
                    sb.Append("if (code < 1 || code > ");
                    var rmax = new string('9', (int)maxSequenceLength);
                    sb.Append(rmax);
                    sb.AppendLine(")");
                    sb.Append("\tthrow new BusinessException($\"Catalog property '");
                    sb.Append(pname);
                    sb.Append("'={code}. It is outside expected range from 1 to ");
                    sb.Append(rmax);
                    sb.AppendLine("\", EnumExceptionType.CodeOutsideAllowedRange);");
                    break;
                case EnumCodeType.Text:
                    var pref = prefix.Trim();
                    if (pref.Length > 0)
                    {
                        sb.Append("if (!code.StartsWith(\"");
                        sb.Append(pref);
                        sb.AppendLine("\"))");
                        sb.Append("\tthrow new BusinessException($\"Catalog property '");
                        sb.Append(pname);
                        sb.Append("'=\\\"{code}\\\". There is no expected prefix \\\"");
                        sb.Append(pref);
                        sb.AppendLine("\\\"\", EnumExceptionType.CodeOutsideAllowedRange);");
                        sb.Append("code = code.Substring(");
                        sb.Append(pref.Length);
                        sb.AppendLine(");");
                    }
                    sb.Append("if (code.Length != ");
                    sb.Append(maxSequenceLength);
                    sb.AppendLine(")");
                    sb.Append("\tthrow new BusinessException($\"Catalog property '");
                    sb.Append(pname);
                    sb.Append("'=\\\"");
                    sb.Append(pref);
                    sb.Append("{code}\\\". Length of sequence not equal ");
                    sb.Append(maxSequenceLength);
                    sb.AppendLine("\", EnumExceptionType.CodeOutsideAllowedRange);");
                    sb.AppendLine("if (!System.Numerics.BigInteger.TryParse(code, out var i))");
                    sb.Append("\tthrow new BusinessException($\"Catalog property '");
                    sb.Append(pname);
                    sb.Append("'=\\\"");
                    sb.Append(pref);
                    sb.AppendLine("{code}\\\". Can't parse sequence \\\"{code}\\\" to number\", EnumExceptionType.CodeOutsideAllowedRange);");
                    sb.Append("if (i < 1 || i > ");
                    var rmax2 = new string('9', (int)maxSequenceLength);
                    sb.Append(rmax2);
                    sb.AppendLine(")");
                    sb.Append("\tthrow new BusinessException($\"Catalog property '");
                    sb.Append(pname);
                    sb.Append("'=\\\"{code}\\\". It is outside expected range from '");
                    var rmin2 = new string('0', (int)maxSequenceLength - 1) + "1";
                    sb.Append(rmin2);
                    sb.Append("' to '");
                    sb.Append(rmax2);
                    sb.AppendLine("'\", EnumExceptionType.CodeOutsideAllowedRange);");
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        public string GetCodeCheckProc()
        {
            Debug.Assert(this.Parent != null);
            var cfg = this.Parent.Cfg;
            var pname = cfg.Model.PropertyCodeName;
            var sb = new StringBuilder();
            if (string.IsNullOrWhiteSpace(this.SequenceGuid))
            {
                this.GetCodeCheckProc(sb, this.SequenceType, this.MaxSequenceLength, this.Prefix, pname);
            }
            else
            {
                var seq = (EnumeratorSequence)this.ParentDocument.Cfg.DicNodes[this.SequenceGuid];
                this.GetCodeCheckProc(sb, seq.SequenceType, seq.MaxSequenceLength, seq.Prefix, pname);
            }
            sb.Append("return true; // code is valid");
            return sb.ToString();
        }
        private string GetCodeStartStr(EnumCodeType sequenceType, uint maxSequenceLength, string prefix)
        {
            switch (sequenceType)
            {
                case EnumCodeType.Number:
                    return "1";
                case EnumCodeType.Text:
                    var pref = prefix.Trim();
                    string fmt = "D" + maxSequenceLength;
                    return $"\"{pref}{1.ToString(fmt)}\"";
                default:
                    throw new NotImplementedException();
            }
        }
        public string GetCodeStartStr()
        {
            if (string.IsNullOrWhiteSpace(this.SequenceGuid))
            {
                return this.GetCodeStartStr(this.SequenceType, this.MaxSequenceLength, this.Prefix);
            }
            else
            {
                var seq = (EnumeratorSequence)this.ParentDocument.Cfg.DicNodes[this.SequenceGuid];
                return this.GetCodeStartStr(seq.SequenceType, seq.MaxSequenceLength, seq.Prefix);
            }
        }
        private string GetCodeEndStr(EnumCodeType sequenceType, uint maxSequenceLength, string prefix)
        {
            switch (sequenceType)
            {
                case EnumCodeType.Number:
                    return new string('9', (int)maxSequenceLength);
                case EnumCodeType.Text:
                    var pref = prefix.Trim();
                    return $"\"{pref}{new string('9', (int)maxSequenceLength)}\"";
                default:
                    throw new NotImplementedException();
            }
        }
        public string GetCodeEndStr()
        {
            if (string.IsNullOrWhiteSpace(this.SequenceGuid))
            {
                return this.GetCodeEndStr(this.SequenceType, this.MaxSequenceLength, this.Prefix);
            }
            else
            {
                var seq = (EnumeratorSequence)this.ParentDocument.Cfg.DicNodes[this.SequenceGuid];
                return this.GetCodeEndStr(seq.SequenceType, seq.MaxSequenceLength, seq.Prefix);
            }
        }
    }
}
