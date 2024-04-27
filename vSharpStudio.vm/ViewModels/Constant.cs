using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FluentValidation;
using Google.Protobuf;
using Proto.Config;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using static vSharpStudio.common.ModelVisitorNodeReferencesBase;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class Constant : IDataTypeObject, ICanGoLeft, ICanAddNode, INodeGenSettings, IEditableNode, IRoleAccess, IConstantAccessRoles, ILayoutFieldParameters
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Type:{DataType.GetTypeDesc(this.DataType)}";
        }
        [Browsable(false)]
        public GroupListConstants ParentGroupListConstants { get { Debug.Assert(this.Parent != null); return (GroupListConstants)this.Parent; } }
        [Browsable(false)]
        public IGroupListConstants ParentGroupListConstantsI { get { Debug.Assert(this.Parent != null); return (IGroupListConstants)this.Parent; } }
        [Browsable(false)]
        public bool IsPKey => false;
        [Browsable(false)]
        public bool IsRefParent => false;

        [Browsable(false)]
        // Can be used by a generator to keep calculated property data
        public object? Tag { get; set; }
        [Browsable(false)]
        public static IConfig? Config { get; set; }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return new ConfigNodesCollection<ITreeConfigNodeSortable>(this);
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListConstants.Children;
        }
        public override bool HasChildren()
        {
            return false;
        }
        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconConstant"; } }
        //protected override string GetNodeIconName() { return "iconConstant"; }
        partial void OnCreated()
        {
            this.IsIncludableInModels = true;
            //this._RefComplexObjectDescrPropertyGuid = System.Guid.NewGuid().ToString();
            //this._RefComplexObjectGdPropertyGuid = System.Guid.NewGuid().ToString();
            Init();
            //this.InitRoles();
        }
        [Browsable(false)]
        public bool IsComputed { get; set; }
        protected override void OnInitFromDto()
        {
            switch (this.DataType.DataTypeEnum)
            {
                case EnumDataType.ANY:
                case EnumDataType.CATALOG:
                case EnumDataType.CATALOGS:
                case EnumDataType.DOCUMENT:
                case EnumDataType.DOCUMENTS:
                    this.IsComputed = true;
                    break;
                default:
                    break;
            }
            Init();
        }
        private void Init()
        {
            this.DataType.PropertyChanged += DataType_PropertyChanged;
            //this.ListRoles.OnAddingAction = (t) =>
            //{
            //    t.IsNew = true;
            //};
            //this.ListRoles.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            //this.ListRoles.OnRemovedAction = (t) =>
            //{
            //    this.OnRemoveChild();
            //};
            //this.ListRoles.OnClearedAction = () =>
            //{
            //    this.OnRemoveChild();
            //};
        }

        public Constant(ITreeConfigNode parent, string name, EnumDataType type, string guidOfType)
            : this(parent)
        {
            this._Name = name;
            this._DataType = new DataType(this, type, guidOfType);
        }

        public Constant(ITreeConfigNode parent, string name, EnumDataType type, uint? length = null, uint? accuracy = null, bool? isPositive = null)
            : this(parent)
        {
            this._Name = name;
            this._DataType = new DataType(this, type, length, accuracy);
        }

        [Category("")]
        [PropertyOrderAttribute(10)]
        public string ClrType
        {
            get
            {
                if (this.IsNullable)
                    return this.DataType.ClrTypeName + "?";
                return this.DataType.ClrTypeName;
            }
        }
        [Browsable(false)]
        public IDataType IDataType { get { return this._DataType; } }
        private void DataType_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            this.Tag = null;
        }

        #region Editing logic
        partial void OnIsNullableChanged()
        {
            this.OnPropertyChanged(nameof(this.ClrType));
            this.Tag = null;
        }
        private void OnDataTypeEnumChanged()
        {
            switch (this.DataType.DataTypeEnum)
            {
                case EnumDataType.CHAR:
                case EnumDataType.BOOL:
                    this.Length = 0;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ConfigObjectGuid = string.Empty;
                    this.ListObjectRefs.Clear();
                    break;
                case EnumDataType.DATE:
                    this.Length = 0;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ConfigObjectGuid = string.Empty;
                    this.ListObjectRefs.Clear();
                    break;
                case EnumDataType.DATETIMELOCAL:
                case EnumDataType.DATETIMEUTC:
                //case EnumDataType.DATETIME:
                case EnumDataType.DATETIMEZ:
                case EnumDataType.DATETIMEOFFSET:
                case EnumDataType.TIME:
                    this.Length = 0;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ConfigObjectGuid = string.Empty;
                    this.ListObjectRefs.Clear();
                    break;
                case EnumDataType.CATALOGS:
                case EnumDataType.DOCUMENTS:
                case EnumDataType.ANY:
                    this.Length = 0;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ConfigObjectGuid = string.Empty;
                    this.ListObjectRefs.Clear();
                    break;
                case EnumDataType.CATALOG:
                case EnumDataType.DOCUMENT:
                case EnumDataType.ENUMERATION:
                    this.Length = 0;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ConfigObjectGuid = string.Empty;
                    this.ListObjectRefs.Clear();
                    break;
                case EnumDataType.NUMERICAL:
                    this.Length = 6;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ConfigObjectGuid = string.Empty;
                    this.ListObjectRefs.Clear();
                    break;
                case EnumDataType.STRING:
                    this.Length = 25;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ConfigObjectGuid = string.Empty;
                    this.ListObjectRefs.Clear();
                    break;
                default:
                    throw new NotSupportedException();
            }
            this.OnPropertyChanged(nameof(this.ClrType));
            this.Tag = null;
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            if (this.DataType.DataTypeEnum != EnumDataType.STRING)
            {
                lst.Add(nameof(this.MinLengthRequirement));
                lst.Add(nameof(this.MaxLengthRequirement));
            }
            if (this.DataType.DataTypeEnum != EnumDataType.NUMERICAL)
            {
                lst.Add(nameof(this.Accuracy));
                lst.Add(nameof(this.IsPositive));
            }
            if (this.DataType.DataTypeEnum != EnumDataType.STRING && this.DataType.DataTypeEnum != EnumDataType.NUMERICAL)
            {
                lst.Add(nameof(this.Length));
            }
            if (this.DataType.DataTypeEnum != EnumDataType.TIME &&
                this.DataType.DataTypeEnum != EnumDataType.DATETIMELOCAL &&
                this.DataType.DataTypeEnum != EnumDataType.DATETIMEUTC) // &&
                                                                        //this.DataType.DataTypeEnum != EnumDataType.DATETIME &&
                                                                        //this.DataType.DataTypeEnum != EnumDataType.DATETIMEZ)
            {
                lst.Add(nameof(this.AccuracyForTime));
            }
            if (this.DataType.DataTypeEnum != EnumDataType.CATALOGS &&
                this.DataType.DataTypeEnum != EnumDataType.DOCUMENTS)
            {
                lst.Add(nameof(this.ListObjectRefs));
                lst.Add(nameof(this.DefaultValue));
            }
            if (this.DataType.DataTypeEnum != EnumDataType.CATALOG &&
                this.DataType.DataTypeEnum != EnumDataType.DOCUMENT &&
                this.DataType.DataTypeEnum != EnumDataType.ENUMERATION &&
                this.DataType.DataTypeEnum != EnumDataType.ANY)
            {
                lst.Add(nameof(this.ConfigObjectGuid));
                lst.Add(nameof(this.DefaultValue));
            }
            if (this.Accuracy != 0)
            {
                lst.Add(nameof(this.IsPositive));
            }
            if (this.DataType.DataTypeEnum != EnumDataType.STRING &&
                this.DataType.DataTypeEnum != EnumDataType.CHAR &&
                this.DataType.DataTypeEnum != EnumDataType.DATE &&
                this.DataType.DataTypeEnum != EnumDataType.DATETIMELOCAL &&
                this.DataType.DataTypeEnum != EnumDataType.DATETIMEUTC &&
                //this.DataType.DataTypeEnum != EnumDataType.DATETIME &&
                this.DataType.DataTypeEnum != EnumDataType.DATETIMEZ &&
                this.DataType.DataTypeEnum != EnumDataType.DATETIMEOFFSET &&
                this.DataType.DataTypeEnum != EnumDataType.NUMERICAL)
            {
                lst.Add(nameof(this.RangeValuesRequirements));
            }
            return lst.ToArray();
        }
        [Category("")]
        [DisplayName("Type")]
        [PropertyOrderAttribute(11)]
        public EnumDataType DataTypeEnum
        {
            get { return this.DataType.DataTypeEnum; }
            set
            {
                this.DataType.DataTypeEnum = value;
                this.OnPropertyChanged();
                this.ValidateProperty();
                this.OnDataTypeEnumChanged();
                this.OnPropertyChanged(nameof(this.PropertyDefinitions));
                this.Tag = null;
            }
        }
        [Category("")]
        [DisplayName("Length")]
        [Description("Maximum length of data (characters in string, or decimal digits for numeric data)")]
        [PropertyOrderAttribute(12)]
        public uint Length
        {
            get { return this.DataType.Length; }
            set
            {
                this.DataType.Length = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.ClrType));
                this.ValidateProperty();
                this.Tag = null;
            }
        }
        [Category("")]
        [DisplayName("Accuracy")]
        [Description("Number of decimal places in fractional part for numeric data)")]
        [PropertyOrderAttribute(13)]
        public uint Accuracy
        {
            get { return this.DataType.Accuracy; }
            set
            {
                this.DataType.Accuracy = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.ClrType));
                this.ValidateProperty();
                this.OnPropertyChanged(nameof(this.PropertyDefinitions));
                this.Tag = null;
            }
        }
        [Category("")]
        [DisplayName("Positive")]
        [Description("Expected numerical value always >= 0")]
        [PropertyOrderAttribute(14)]
        public bool IsPositive
        {
            get { return this.DataType.IsPositive; }
            set
            {
                this.DataType.IsPositive = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.ClrType));
                this.ValidateProperty();
                this.Tag = null;
            }
        }
        [Category("")]
        [Editor(typeof(EditorDataTypeObjectName), typeof(EditorDataTypeObjectName))]
        [PropertyOrderAttribute(15)]
        public string ConfigObjectGuid
        {
            get { return this.DataType.ObjectRef.ForeignObjectGuid; }
            set
            {
                this.DataType.ObjectRef0.ForeignObjectGuid = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.ClrType));
                this.ValidateProperty();
                this.Tag = null;
            }
        }
        ////[DisplayName("Positive")]
        ////[Description("Expected always >= 0")]
        //[Editor(typeof(EditorDataTypeObjectName), typeof(EditorDataTypeObjectName))]
        //[PropertyOrderAttribute(6)]
        //public string ObjectName
        //{
        //    get { return this.DataType.ObjectName; }
        //    set
        //    {
        //        this.DataType.ObjectName = value;
        //        this.OnPropertyChanged();
        //        this.ValidateProperty();
        //    }
        //}
        //[DisplayName("Positive")]
        //[Description("Expected always >= 0")]
        [Category("")]
        [PropertyOrderAttribute(16)]
        public ObservableCollection<ComplexRef> ListObjectRefs
        {
            get { return this.DataType.ListObjectRefs; }
        }
        #endregion Editing logic

        [Browsable(false)]
        public PropertyRangeValuesRequirements? RangeValuesRequirements { get; set; }
        [Browsable(false)]
        public IPropertyRangeValuesRequirements? RangeValuesRequirementsI { get { return RangeValuesRequirements; } }
        [Browsable(false)]
        public string PropValueValue
        {
            get
            {
                if (!this.IsNullable)
                    return "";
                switch (this.DataTypeEnum)
                {
                    //case EnumDataType.CATALOG:
                    //    return "Catalog";
                    //case EnumDataType.CATALOGS:
                    //    return "Catalog";
                    //case EnumDataType.DOCUMENT:
                    //    return "Document";
                    //case EnumDataType.DOCUMENTS:
                    //    return "Documents";
                    //case EnumDataType.DATE:
                    //    return "Date" + sn;
                    //case EnumDataType.DATETIME:
                    //    return "DateTime" + sn;
                    //case EnumDataType.TIME:
                    //    return "Time" + sn;
                    //case EnumDataType.DATETIMEZ:
                    //    return "DateTimeZ" + sn;
                    //case EnumDataType.TIMEZ:
                    //    return "TimeZ" + sn;
                    case EnumDataType.ENUMERATION:
                        var en = (Enumeration)this.Cfg.DicNodes[this.ConfigObjectGuid];
                        return en.DefaultValue;
                    case EnumDataType.BOOL:
                        return ".Value";
                    case EnumDataType.STRING:
                        return "";
                    case EnumDataType.NUMERICAL:
                        return ".Value";
                    default:
                        return "null";
                }
            }
        }
        [Browsable(false)]
        public string? ComplexObjectName { get; set; }
        public string ComplexObjectNameWithDot() { if (!string.IsNullOrEmpty(this.ComplexObjectName)) return $"{this.ComplexObjectName}."; return ""; }

        #region IConfigObject
        // public void Create()
        // {
        //    GroupListConstants vm = (GroupListConstants)this.Parent;
        //    int icurr = vm.Children.IndexOf(this);
        //    vm.Children.Add(new Constant() { Parent = this.Parent });
        // }

        #endregion IConfigObject

        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
        }
        //[BrowsableAttribute(false)]
        //public string DefaultValue
        //{
        //    get
        //    {
        //        if (this.DataType.IsNullable)
        //            return "null";
        //        return this.DataType.DefaultValue;
        //    }
        //}
        [PropertyOrder(100)]
        [ReadOnly(true)]
        [DisplayName("Composite")]
        [Description("Composite name based on IsCompositeNames and IsUseGroupPrefix model parameters")]
        public string CompositeName
        {
            get
            {
                return GetCompositeName();
            }
        }
        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListConstants.ListConstants.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Constant?)this.ParentGroupListConstants.ListConstants.GetPrev(this);
            if (prev == null)
                return;
            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupListConstants.ListConstants.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListConstants.ListConstants.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Constant?)this.ParentGroupListConstants.ListConstants.GetNext(this);
            if (next == null)
                return;
            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupListConstants.ListConstants.MoveDown(this);
            this.SetSelected(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = Constant.Clone(this.ParentGroupListConstants, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupListConstants.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            var node = new Constant(this.ParentGroupListConstants);
            this.ParentGroupListConstants.Add(node);
            node.Position = this.ParentGroupListConstants.GetNextPosition();
            this.GetUniqueName(Defaults.ConstantName, node, this.ParentGroupListConstants.ListConstants);
            var model = this.ParentGroupListConstants.ParentGroupConstantGroups.ParentModel;
            node.ShortId = model.LastTypeShortIdForNode();
            node.ShortRefId = model.LastTypeShortRefIdForNode(node, node.ShortId);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListConstants.ListConstants.Remove(this);
        }
        #endregion Tree operations
        //protected override string[]? OnGetWhatHideOnPropertyGrid()
        //{
        //    var lst = new List<string>();
        //    lst.Add(this.GetPropertyName(() => this.Parent));
        //    return lst.ToArray();
        //}

        #region Roles
        public object GetRoleAccess(IRole role)
        {
            if (!this.dicConstantAccess.ContainsKey(role.Guid))
            {
                var rca = new RoleConstantAccess() { Guid = role.Guid };
                this.ListRoleConstantAccessSettings.Add(rca);
                this.dicConstantAccess[role.Guid] = rca;
            }
            return dicConstantAccess[role.Guid];
        }
        public void SetRoleAccess(IRole role, EnumConstantAccess? edit, EnumPrintAccess? print)
        {
            Debug.Assert(role != null);
            Debug.Assert(dicConstantAccess.ContainsKey(role.Guid));
            if (edit.HasValue)
                dicConstantAccess[role.Guid].EditAccess = edit.Value;
            if (print.HasValue)
                dicConstantAccess[role.Guid].PrintAccess = print.Value;
        }
        internal Dictionary<string, RoleConstantAccess> dicConstantAccess = new();
        public void InitRoles()
        {
            foreach (var tt in this.ListRoleConstantAccessSettings)
            {
                this.dicConstantAccess[tt.Guid] = tt;
            }
            foreach (var t in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (!this.dicConstantAccess.ContainsKey(t.Guid))
                {
                    var rca = new RoleConstantAccess() { Guid = t.Guid };
                    this.dicConstantAccess[t.Guid] = rca;
                }
            }
        }
        public void InitRoleAdd(IRole role)
        {
            var rca = new RoleConstantAccess() { Guid = role.Guid };
            this.ListRoleConstantAccessSettings.Add(rca);
            this.dicConstantAccess[rca.Guid] = rca;
        }
        public void InitRoleRemove(IRole role)
        {
            for (int i = 0; i < this.ListRoleConstantAccessSettings.Count; i++)
            {
                if (this.ListRoleConstantAccessSettings[i].Guid == role.Guid)
                {
                    this.ListRoleConstantAccessSettings.RemoveAt(i);
                    break;
                }
            }
            this.dicConstantAccess.Remove(role.Guid);
        }
        public EnumConstantAccess GetRoleConstantAccess(IRole role)
        {
            if (this.dicConstantAccess.TryGetValue(role.Guid, out var r) && r.EditAccess != EnumConstantAccess.CN_BY_PARENT)
                return r.EditAccess;
            return this.ParentGroupListConstants.GetRoleConstantAccess(role);
        }
        public EnumPrintAccess GetRoleConstantPrint(IRole role)
        {
            if (this.dicConstantAccess.TryGetValue(role.Guid, out var r) && r.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                return r.PrintAccess;
            return this.ParentGroupListConstants.GetRoleConstantPrint(role);
        }
        public IReadOnlyList<string> GetRolesByAccess(EnumConstantAccess access)
        {
            var roles = new List<string>();
            foreach (var role in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (GetRoleConstantAccess(role) == access)
                    roles.Add(role.Name);
            }
            return roles;
        }
        public IReadOnlyList<string> GetRolesByAccess(EnumPrintAccess access)
        {
            var roles = new List<string>();
            foreach (var role in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (GetRoleConstantPrint(role) == access)
                    roles.Add(role.Name);
            }
            return roles;
        }
        #endregion Roles

        #region Plugin group model
        public IConstant CreateConstantFromJson(string settings, string subName, IDataType dt)
        {
            var proto = CommonUtils.ParseJson<proto_constant>(settings, true);
            var p = Constant.ConvertToVM(proto, new Constant(this));
            p.DataType = (DataType)dt;
            p.Name = this.Name + subName;
            return p;
        }
        public string ConvertToJson()
        {
            var proto = Constant.ConvertToProto(this);
            return JsonFormatter.Default.Format(proto);
        }
        /// <summary>
        /// Parent property if extended property is created
        /// </summary>
        public IConstant? ParentConstant { get; set; }
        public string NameWithExtention { get { if (this.ParentConstant == null) return this.Name; return this.ParentConstant.Name + this.Name; } }
        public IConstant AddExtensionConstantRefId(string subName, string guid)
        {
            var node = new Constant(this) { Name = subName, ParentConstant = this };
            node.Guid = guid;
            node.DataType = (DataType)this.Cfg.Model.GetIdRefDataType(node, true);
            node.DataType.IsPKey = false;
            node.IsNullable = true;
            node.IsComplexRefId = true;
            return node;
        }
        public bool IsComplexRefId { get; private set; }
        public bool IsComplexRefGuid { get; private set; }
        public bool IsComplexDesc { get; private set; }
        public IConstant AddExtensionConstantGd(string subName, string guid)
        {
            var node = new Constant(this) { Name = subName };
            node.Guid = guid;
            node.DataType = (DataType)this.Cfg.Model.GetDataTypeInt(node, false, true);
            node.IsNullable = true;
            node.ParentConstant = this;
            node.IsComplexRefGuid = true;
            return node;
        }
        public IConstant AddExtensionConstantDesc(string subName, string guid)
        {
            var node = new Constant(this) { Name = subName };
            node.Guid = guid;
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.STRING, Length = this.Cfg.Model.ComplexPropertyRefDescrLength };
            node.IsNullable = true;
            node.ParentConstant = this;
            node.IsComplexDesc = true;
            return node;
        }
        public IConstant AddExtensionConstantString(string subName, uint length, string guid)
        {
            var node = new Constant(this) { Name = this.Name + subName };
            node.Guid = guid;
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.STRING, Length = length };
            return node;
        }
        public IConstant AddExtensionConstantNumerical(string subName, uint length, uint accuracy, string guid)
        {
            var node = new Constant(this) { Name = this.Name + subName };
            node.Guid = guid;
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.NUMERICAL, Length = length, Accuracy = accuracy };
            return node;
        }
        #endregion Plugin group model
    }
}
