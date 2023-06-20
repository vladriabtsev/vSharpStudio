using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using vSharpStudio.common;

namespace vSharpStudio.vm.ViewModels
{
    public partial class CatalogCodePropertySettings : IParent
    {
        public override string ToString()
        {
            string unique = "";
            switch (this.UniqueScope)
            {
                case EnumCatalogCodeUniqueScope.code_uniqueness_by_folder_settings:
                    unique = "Unique Folder";
                    break;
                case EnumCatalogCodeUniqueScope.code_unique_in_whole_catalog:
                    unique = "Unique";
                    break;
                case EnumCatalogCodeUniqueScope.code_not_unique_settings:
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
            if (this.ParentCatalog != null)
                return $"{this.ParentCatalog.Cfg.DicNodes[this.SequenceGuid].Name}-{unique}";
            else if (this.ParentCatalogFolder != null)
                return $"{this.ParentCatalogFolder.Cfg.DicNodes[this.SequenceGuid].Name}-{unique}";
            throw new NotImplementedException();
        }
        [Browsable(false)]
        public Catalog? ParentCatalog
        {
            get
            {
                Debug.Assert(this.Parent != null);
                if (this.Parent is Catalog)
                    return (Catalog)this.Parent;
                else
                    return null;
            }
        }
        [Browsable(false)]
        public CatalogFolder? ParentCatalogFolder
        {
            get
            {
                Debug.Assert(this.Parent != null);
                if (this.Parent is CatalogFolder)
                    return (CatalogFolder?)this.Parent;
                else
                    return null;
            }
        }
        [Browsable(false)]
        public ICatalog ParentCatalogI { get { Debug.Assert(this.Parent != null); return (ICatalog)this.Parent; } }
        partial void OnCreated()
        {
            this.SequenceType = common.EnumCodeType.Text;
            this.MaxSequenceLength = 5;
            this.Prefix = "";
            this.UniqueScope = common.EnumCatalogCodeUniqueScope.code_unique_in_whole_catalog;
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
        //    this.ListRoles.OnRemovedAction = (t) => {
        //        this.OnRemoveChild();
        //    };
        //    this.ListRoles.OnClearedAction = () => {
        //        this.OnRemoveChild();
        //    };
        //}
        protected override void OnIsChangedChanged()
        {
            if (this.Parent != null && this.IsChanged)
                this.Parent.IsChanged = true;
        }
        partial void OnMaxSequenceLengthChanged()
        {
            this.ParentCatalog?.NotifyCodePropertySettingsChanged();
            this.ParentCatalogFolder?.NotifyCodePropertySettingsChanged();
        }
        partial void OnPrefixChanged()
        {
            this.ParentCatalog?.NotifyCodePropertySettingsChanged();
            this.ParentCatalogFolder?.NotifyCodePropertySettingsChanged();
        }
        partial void OnSequenceTypeChanged()
        {
            this.ParentCatalog?.NotifyCodePropertySettingsChanged();
            this.ParentCatalogFolder?.NotifyCodePropertySettingsChanged();
        }
        partial void OnUniqueScopeChanged()
        {
            this.ParentCatalog?.NotifyCodePropertySettingsChanged();
            this.ParentCatalogFolder?.NotifyCodePropertySettingsChanged();
        }
        partial void OnSequenceGuidChanged()
        {
            this.NotifyPropertyChanged(() => this.PropertyDefinitions);
            this.ParentCatalog?.NotifyCodePropertySettingsChanged();
            this.ParentCatalogFolder?.NotifyCodePropertySettingsChanged();
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
            return lst.ToArray();
        }
        public string GetCodeClrTypeName()
        {
            IConfig? cfg = null;
            string? propertyCodeGuid = null;
            GroupListProperties? groupListProperties = null;
            if (this.ParentCatalog != null)
            {
                cfg = this.ParentCatalog.Cfg;
                propertyCodeGuid = this.ParentCatalog.PropertyCodeGuid;
                groupListProperties = this.ParentCatalog.GroupProperties;
            }
            else
            {
                cfg = this.ParentCatalogFolder?.Cfg;
                propertyCodeGuid = this.ParentCatalogFolder?.PropertyCodeGuid;
                groupListProperties = this.ParentCatalogFolder?.GroupProperties;
            }
            Debug.Assert(cfg != null);
            Debug.Assert(propertyCodeGuid != null);
            Debug.Assert(groupListProperties != null);
            IProperty? prp = null;
            switch (this.SequenceType)
            {
                case EnumCodeType.Number:
                    prp = cfg.Model.GetPropertyCatalogCodeInt(groupListProperties, propertyCodeGuid,
                        this.MaxSequenceLength);
                    break;
                case EnumCodeType.Text:
                    prp = cfg.Model.GetPropertyCatalogCode(groupListProperties, propertyCodeGuid,
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
