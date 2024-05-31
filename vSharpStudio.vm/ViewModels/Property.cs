using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.DirectoryServices;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using CommunityToolkit.Diagnostics;
using FluentValidation;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Proto.Config;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class Property : IDataTypeObject, ICanAddNode, ICanGoLeft, INodeGenSettings, IEditableNode, IRoleAccess, IPropertyAccessRoles, ILayoutFieldParameters
    {
        public const string SpecialPropertyNameRefParent = "RefParent";
        public const string SpecialPropertyNameRefTreeParent = "RefTreeParent";
        partial void OnDebugStringExtend(ref string mes)
        {
            if (this.ParentProperty != null)
            {
                mes = mes + " Complex:" + this.ParentProperty.Name;
            }
            mes = mes + $" Type:{DataType.GetTypeDesc(this.DataType)}";
            if (this.TagInList != null)
            {
                if (this.TagInList is string)
                    mes = mes + $" Tag:{(string)this.TagInList}";
                else
                    mes = mes + $" Tag:{this.TagInList?.ToString()}";
            }
        }
        public string RefIdName()
        {
            if (!string.IsNullOrWhiteSpace(this.DataType.ObjectRef.ForeignObjectGuid))
            {
                var fname = ((ICompositeName)this.Cfg.DicNodes[this.DataType.ObjectRef.ForeignObjectGuid]).CompositeName;
                switch (this.DataType.DataTypeEnum)
                {
                    case EnumDataType.REF_CATALOG_TO_SEPARATE_CATALOG_FOLDER:
                    case EnumDataType.REF_DETAIL_TO_PARENT_CATALOG:
                    case EnumDataType.REF_DETAIL_TO_PARENT_CATALOG_FOLDER:
                    case EnumDataType.REF_DETAIL_TO_PARENT_DETAIL:
                    case EnumDataType.REF_DETAIL_TO_PARENT_DOCUMENT:
                        return this.Name + "Ref" + fname;
                    case EnumDataType.REF_TO_SELF_TREE_CATALOG_FOLDER_PARENT:
                    case EnumDataType.REF_TO_SELF_TREE_CATALOG_PARENT:
                        return this.Name + "Ref" + fname;
                    case EnumDataType.REF_TIMELINE:
                        return this.Name + "Ref" + fname;
                    case EnumDataType.CATALOG:
                    case EnumDataType.DOCUMENT:
                        return this.Name + "Ref" + fname;
                }
            }
            return this.Name;
        }
        /// <summary>
        /// Property Path in object. Samples: Property1, Detail1->Property1
        /// </summary>
        [Browsable(false)]
        public string PathInObject { get; set; } = string.Empty;
        /// <summary>
        /// Tag to find property in list. To use for dynamically created property list
        /// </summary>
        [Browsable(false)]
        public string? TagInList { get; set; }
        /// <summary>
        /// Is hidden on UI
        /// </summary>
        [Browsable(false)]
        public bool IsHidden { get; set; }
        /// <summary>
        /// Is record version property
        /// </summary>
        [Browsable(false)]
        public bool IsRecordVersion { get; set; }
        [Browsable(false)]
        public bool IsCsNullable { get; set; }

        [Browsable(false)]
        public GroupListProperties ParentGroupListProperties { get { Debug.Assert(this.Parent != null); return (GroupListProperties)this.Parent; } }
        [Browsable(false)]
        public IGroupListProperties ParentGroupListPropertiesI { get { Debug.Assert(this.Parent != null); return (IGroupListProperties)this.Parent; } }
        [Browsable(false)]
        public IListProperties ParentListPropertiesI { get { Debug.Assert(this.Parent != null); return (IListProperties)this.Parent; } }
        [Browsable(false)]
        // Can be used by a generator to keep calculated property data
        public object? Tag { get; set; }
        [Browsable(false)]
        // Calculated position in list of properties
        // Useful for short names in indexes
        public int PositionInConfigObject { get; set; }
        public void SetPosition(uint position)
        {
            this.Position = position;
        }
        //[Browsable(false)]
        //public static IConfig? Config { get; set; }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return new ConfigNodesCollection<ITreeConfigNodeSortable>(this);
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentListPropertiesI.Children;
        }
        public override bool HasChildren()
        {
            return false;
        }
        #endregion ITree

        [Browsable(false)]
        public new string IconName { get { return "iconProperty"; } }
        //protected override string GetNodeIconName() { return "iconProperty"; }
        [Browsable(false)]
        public string? ComplexObjectName { get; set; }
        public string ComplexObjectNameWithDot() { if (!string.IsNullOrEmpty(this.ComplexObjectName)) return $"{this.ComplexObjectName}."; return ""; }
        partial void OnCreated()
        {
            this.DataType.Parent = this;
            this.IsNullable = false;
            this.IsUseHistory = false;
            this._MinLengthRequirement = "";
            this._MaxLengthRequirement = "";
            this._RangeValuesRequirementStr = "";
            this._LinesOnScreen = 1;
            this.IsIncludableInModels = true;
            Init();
        }
        // for new properties
        partial void OnNameChanged()
        {
            var sb = ToRoot(this);
            this.PathInObject = sb.ToString();
        }
        protected override void OnInitFromDto()
        {
            switch (this.DataType.DataTypeEnum)
            {
                case EnumDataType.ANY:
                case EnumDataType.CATALOG:
                case EnumDataType.CATALOGS:
                case EnumDataType.DOCUMENT:
                case EnumDataType.DOCUMENTS:
                case EnumDataType.REF_CATALOG_TO_SEPARATE_CATALOG_FOLDER:
                case EnumDataType.REF_DETAIL_TO_PARENT_CATALOG:
                case EnumDataType.REF_DETAIL_TO_PARENT_CATALOG_FOLDER:
                case EnumDataType.REF_DETAIL_TO_PARENT_DETAIL:
                case EnumDataType.REF_DETAIL_TO_PARENT_DOCUMENT:
                case EnumDataType.REF_TO_SELF_TREE_CATALOG_FOLDER_PARENT:
                case EnumDataType.REF_TO_SELF_TREE_CATALOG_PARENT:
                case EnumDataType.REF_TIMELINE:
                    this.IsComplex = true;
                    break;
            }
            Init();
            var sb = ToRoot(this);
            this.PathInObject = sb.ToString();
        }
        private StringBuilder ToRoot(ITreeConfigNode n, StringBuilder? sb = null)
        {
            if (sb == null)
                sb = new StringBuilder();
            if (n is Document)
                return sb;
            if (n is Catalog)
                return sb;
            if (n is GroupDocuments)
                return sb;
            if (n is GroupListRegisters)
                return sb;
            if (n is GroupListConstants)
                return sb;
            if (n is RelationManyToMany)
                return sb;
            if (n is RelationOneToOne)
                return sb;
            Debug.Assert(n.Parent != null);
            ToRoot(n.Parent, sb);
            if (n is Property p)
            {
                sb.Append(p.Name);
            }
            else if (n is Detail d)
            {
                sb.Append(d.Name);
                sb.Append('.');
            }
            return sb;
        }
        private void Init()
        {
            //this.ListMainViewForms.OnAddingAction = (t) =>
            //{
            //    t.IsNew = true;
            //};
            //this.ListMainViewForms.OnAddedAction = (t) =>
            //{
            //    t.OnAdded();
            //};
            //this.ListMainViewForms.OnRemovedAction = (t) =>
            //{
            //    this.OnRemoveChild();
            //};
            //this.ListMainViewForms.OnClearedAction = () =>
            //{
            //    this.OnRemoveChild();
            //};
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
        }
        internal bool isSpecialItself;
        internal Property(ITreeConfigNode parent, string guid, string name, bool isSpecial)
        : this(parent)
        {
            this.isSpecialItself = isSpecial;
            this._Guid = guid;
            this._Name = name;
        }
        internal Property(ITreeConfigNode parent, string name, EnumDataType type, string guidOfType)
            : this(parent)
        {
            this._Name = name;
            this._DataType = new DataType(this, type, guidOfType);
        }
        internal Property(ITreeConfigNode parent, string name, EnumDataType type, uint? length = null, uint? accuracy = null, bool? isPositive = null)
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
        //public string ProtoType
        //{
        //    get { return this.DataType.ProtoType; }
        //}
        [Browsable(false)]
        public IDataType IDataType { get { return this._DataType; } }
        //public string DefaultValue { get { return this.DataType.DefaultValue; } }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentListPropertiesI.ListProperties.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Property?)this.ParentListPropertiesI.ListProperties.GetPrev(this);
            if (prev == null)
                return;
            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentListPropertiesI.ListProperties.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentListPropertiesI.ListProperties.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Property?)this.ParentListPropertiesI.ListProperties.GetNext(this);
            if (next == null)
                return;
            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentListPropertiesI.ListProperties.MoveDown(this);
            this.SetSelected(this);
        }

        public void NodeRemove(bool ask = true)
        {
            this.ParentListPropertiesI.ListProperties.Remove(this);
            this.Parent = null;
        }
        public override ITreeConfigNode NodeAddClone()
        {
            Debug.Assert(this.Parent != null);
            var node = Property.Clone(this.Parent, this, true, true);
            this.ParentListPropertiesI.ListProperties.Add(node);
            this._Name = this._Name + "2";
            this.SetSelected(node);
            return node;
        }

        public override ITreeConfigNode NodeAddNew()
        {
            if (!(this.Parent is GroupListProperties))
            {
                throw new Exception();
            }

            var node = new Property(this.Parent);
            this.ParentListPropertiesI.ListProperties.Add(node);
            node.Position = this.ParentListPropertiesI.GetNextPosition();
            this.GetUniqueName(Defaults.PropertyName, node, this.ParentListPropertiesI.ListProperties);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentListPropertiesI.ListProperties.Remove(this);
        }
        #endregion Tree operations

        #region Editing logic
        //partial void OnIsNullableChanged()
        //{
        //    this.OnPropertyChanged(nameof(this.ClrType));
        //    this.Tag = null;
        //}
        private void OnDataTypeEnumChanged()
        {
            this.IsComplex = false;
            switch (this.DataType.DataTypeEnum)
            {
                case EnumDataType.CHAR:
                case EnumDataType.BOOL:
                    this.Length = 0;
                    break;
                case EnumDataType.DATE:
                    this.Length = 0;
                    break;
                case EnumDataType.DATETIMELOCAL:
                case EnumDataType.DATETIMEUTC:
                case EnumDataType.DATETIMEZ:
                case EnumDataType.DATETIMEOFFSET:
                case EnumDataType.TIME:
                //case EnumDataType.TIMEZ:
                case EnumDataType.TIMESPAN_TIME_ONLY:
                    this.Length = 0;
                    break;
                case EnumDataType.CATALOGS:
                case EnumDataType.DOCUMENTS:
                case EnumDataType.ANY:
                    this.Length = 0;
                    this.IsComplex = true;
                    this.IsNullable = true;
                    break;
                case EnumDataType.CATALOG:
                case EnumDataType.DOCUMENT:
                    this.Length = 0;
                    this.IsComplex = true;
                    this.IsNullable = true;
                    break;
                case EnumDataType.ENUMERATION:
                    this.Length = 0;
                    break;
                case EnumDataType.TIMESPAN:
                    this.Length = 6;
                    break;
                case EnumDataType.NUMERICAL:
                    this.Length = 6;
                    break;
                case EnumDataType.STRING:
                    this.Length = 25;
                    break;
                case EnumDataType.REF_CATALOG_TO_SEPARATE_CATALOG_FOLDER:
                case EnumDataType.REF_DETAIL_TO_PARENT_CATALOG:
                case EnumDataType.REF_DETAIL_TO_PARENT_CATALOG_FOLDER:
                case EnumDataType.REF_DETAIL_TO_PARENT_DETAIL:
                case EnumDataType.REF_DETAIL_TO_PARENT_DOCUMENT:
                case EnumDataType.REF_TO_SELF_TREE_CATALOG_FOLDER_PARENT:
                case EnumDataType.REF_TO_SELF_TREE_CATALOG_PARENT:
                case EnumDataType.REF_TIMELINE:
                    this.IsComplex = true;
                    break;
                default:
                    throw new NotSupportedException();
            }
            this.Tag = null;
            this.OnPropertyChanged(nameof(this.ClrType));
            this.OnPropertyChanged(nameof(this.PropertyDefinitions));
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            if (!(this.Parent.Parent is Catalog))
            {
                lst.Add(nameof(this.IsUseHistory));
            }
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
                //this.DataType.DataTypeEnum != EnumDataType.TIMEZ &&
                this.DataType.DataTypeEnum != EnumDataType.TIMESPAN_TIME_ONLY &&
                this.DataType.DataTypeEnum != EnumDataType.DATETIMEZ &&
                this.DataType.DataTypeEnum != EnumDataType.DATETIMEOFFSET &&
                this.DataType.DataTypeEnum != EnumDataType.DATETIMELOCAL &&
                this.DataType.DataTypeEnum != EnumDataType.DATETIMEUTC)
            {
                lst.Add(nameof(this.DataType.AccuracyForTime));
            }
            if (this.DataType.DataTypeEnum != EnumDataType.TIMESPAN)
            {
                lst.Add(nameof(this.DataType.TimespanAccuracy));
                lst.Add(nameof(this.DataType.TimespanMaxValue));
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
            //if (this.DataType.DataTypeEnum != EnumDataType.CATALOG &&
            //    this.DataType.DataTypeEnum != EnumDataType.CATALOGS)
            //{
            //    lst.Add(nameof(this.RelationType));
            //}
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
                this.Accuracy = 0;
                this.IsPositive = false;
                this.ConfigObjectGuid = string.Empty;

                this.DataType.DataTypeEnum = value;
                this.OnDataTypeEnumChanged();
                this.OnPropertyChanged();
                this.ValidateProperty();
                this.Tag = null;
            }
        }
        [Category("")]
        [DisplayName("Length")]
        [Description("Maximum length of data (characters in string, or decimal digits for numeric data")]
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
        [Description("Number of decimal places in fractional part for numeric data")]
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
        [DisplayName("Accuracy")]
        [Description("TimeSpan accuracy")]
        [PropertyOrderAttribute(12)]
        public EnumTimespanBoundaryType TimespanAccuracy
        {
            get { return this.DataType.TimespanAccuracy; }
            set
            {
                this.DataType.TimespanAccuracy = value;
                this.OnPropertyChanged();
                this.ValidateProperty();
            }
        }
        [Category("")]
        [DisplayName("Max value")]
        [Description("TimeSpan maximum value")]
        [PropertyOrderAttribute(13)]
        public EnumTimespanBoundaryType TimespanMaxValue
        {
            get { return this.DataType.TimespanMaxValue; }
            set
            {
                this.DataType.TimespanMaxValue = value;
                this.OnPropertyChanged();
                this.ValidateProperty();
            }
        }
        [Category("")]
        [DisplayName("Accuracy")]
        [Description("Time accuracy for TimeOnly type. Business model is expecting selected accuracy")]
        [PropertyOrderAttribute(13)]
        public EnumTimeAccuracyType AccuracyForTime
        {
            get { return this.DataType.AccuracyForTime; }
            set
            {
                this.DataType.AccuracyForTime = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.ClrType));
                this.ValidateProperty();
                this.OnPropertyChanged(nameof(this.PropertyDefinitions));
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
                this.ValidateProperty();
                this.OnPropertyChanged(nameof(this.ClrType));
                this.Tag = null;
            }
        }
        //[Category("")]
        //[DisplayName("Relation")]
        //[Description("Relation type with selected complex object/objects")]
        //[PropertyOrderAttribute(16)]
        //public EnumOneToRelationType RelationType
        //{
        //    get { return this.DataType.RelationType; }
        //    set
        //    {
        //        this.DataType.RelationType = value;
        //        this.OnPropertyChanged();
        //        this.OnPropertyChanged(nameof(this.ClrType));
        //        this.ValidateProperty();
        //        this.Tag = null;
        //    }
        //}
        [Category("")]
        [DisplayName("Can be NULL")]
        [Description("If unchecked always expected data")]
        [PropertyOrderAttribute(16)]
        public bool IsNullable
        {
            get { return this.DataType.IsNullable; }
            set
            {
                this.DataType.IsNullable = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.ClrType));
                this.ValidateProperty();
                this.OnPropertyChanged(nameof(this.PropertyDefinitions));
            }
        }
        [Category("")]
        [DisplayName("Use History")]
        [Description("Use history for property value")]
        [PropertyOrderAttribute(17)]
        public bool IsUseHistory
        {
            get { return this.DataType.IsUseHistory; }
            set
            {
                this.DataType.IsUseHistory = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.ClrType));
                this.ValidateProperty();
                this.OnPropertyChanged(nameof(this.PropertyDefinitions));
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
        [PropertyOrderAttribute(17)]
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
        partial void OnIsStopTabControlChanged()
        {
            this.OnPropertyChanged(nameof(this.NodeNameDecorations));
        }
        partial void OnIsStartNewTabControlChanged()
        {
            this.OnPropertyChanged(nameof(this.NodeNameDecorations));
        }
        partial void OnTabNameChanged()
        {
            this.OnPropertyChanged(nameof(this.NodeNameDecorations));
        }
        //partial void OnIsTryAttachChanged()
        //{
        //    this.OnPropertyChanged(nameof(this.NodeNameDecorations));
        //}
        partial void OnIsStartNewRowChanged()
        {
            this.OnPropertyChanged(nameof(this.NodeNameDecorations));
        }
        //[BrowsableAttribute(false)]
        //public new bool IsHasNew { get { return this.IsNew; } }
        public bool IsGridSortableGet()
        {
            if (this.IsGridSortable == EnumUseType.Yes)
                return true;
            if (this.IsGridSortable == EnumUseType.No)
                return false;
            return this.ParentListPropertiesI.GetIsGridSortable();
        }
        public bool IsGridFilterableGet()
        {
            if (this.IsGridFilterable == EnumUseType.Yes)
                return true;
            if (this.IsGridFilterable == EnumUseType.No)
                return false;
            return this.ParentListPropertiesI.GetIsGridFilterable();
        }
        public bool IsGridSortableCustomGet()
        {
            if (this.IsGridSortableCustom == EnumUseType.Yes)
                return true;
            if (this.IsGridSortableCustom == EnumUseType.No)
                return false;
            return this.ParentListPropertiesI.GetIsGridSortableCustom();
        }

        #region Roles
        public object GetRoleAccess(IRole role)
        {
            if (!this.dicPropertyAccess.ContainsKey(role.Guid))
            {
                var rca = new RolePropertyAccess() { Guid = role.Guid };
                this.ListRolePropertyAccessSettings.Add(rca);
                this.dicPropertyAccess[role.Guid] = rca;
            }
            return dicPropertyAccess[role.Guid];
        }
        public void SetRoleAccess(IRole role, EnumPropertyAccess? edit, EnumPrintAccess? print)
        {
            Debug.Assert(role != null);
            Debug.Assert(dicPropertyAccess.ContainsKey(role.Guid));
            if (edit.HasValue)
                dicPropertyAccess[role.Guid].EditAccess = edit.Value;
            if (print.HasValue)
                dicPropertyAccess[role.Guid].PrintAccess = print.Value;
        }
        internal Dictionary<string, RolePropertyAccess> dicPropertyAccess = new();
        public void InitRoles()
        {
            foreach (var tt in this.ListRolePropertyAccessSettings)
            {
                this.dicPropertyAccess[tt.Guid] = tt;
            }
            foreach (var t in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (!this.dicPropertyAccess.ContainsKey(t.Guid))
                {
                    var rca = new RolePropertyAccess() { Guid = t.Guid };
                    this.dicPropertyAccess[t.Guid] = rca;
                }
            }
        }
        public void InitRoleAdd(IRole role)
        {
            var rca = new RolePropertyAccess() { Guid = role.Guid };
            this.ListRolePropertyAccessSettings.Add(rca);
            this.dicPropertyAccess[rca.Guid] = rca;
        }
        public void InitRoleRemove(IRole role)
        {
            for (int i = 0; i < this.ListRolePropertyAccessSettings.Count; i++)
            {
                if (this.ListRolePropertyAccessSettings[i].Guid == role.Guid)
                {
                    this.ListRolePropertyAccessSettings.RemoveAt(i);
                    break;
                }
            }
            this.dicPropertyAccess.Remove(role.Guid);
        }
        public EnumPropertyAccess GetRolePropertyAccess(IRole role)
        {
            if (this.dicPropertyAccess.TryGetValue(role.Guid, out var r) && r.EditAccess != EnumPropertyAccess.P_BY_PARENT)
                return r.EditAccess;
            return this.ParentListPropertiesI.GetRolePropertyAccess(role);
        }
        public EnumPrintAccess GetRolePropertyPrint(IRole role)
        {
            if (this.dicPropertyAccess.TryGetValue(role.Guid, out var r) && r.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                return r.PrintAccess;
            return this.ParentListPropertiesI.GetRolePropertyPrint(role);
        }
        public IReadOnlyList<string> GetRolesByAccess(EnumPropertyAccess access)
        {
            var roles = new List<string>();
            foreach (var role in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (GetRolePropertyAccess(role) == access)
                    roles.Add(role.Name);
            }
            return roles;
        }
        public IReadOnlyList<string> GetRolesByAccess(EnumPrintAccess access)
        {
            var roles = new List<string>();
            foreach (var role in this.Cfg.Model.GroupCommon.GroupRoles.ListRoles)
            {
                if (GetRolePropertyPrint(role) == access)
                    roles.Add(role.Name);
            }
            return roles;
        }
        #endregion Roles

        #region Plugin group model
        public IProperty CreatePropertyFromJson(string settings, string subName, IDataType dt)
        {
            var proto = CommonUtils.ParseJson<proto_property>(settings, true);
            var p = Property.ConvertToVM(proto, new Property(this));
            p.DataType = (DataType)dt;
            p.Name = this.Name + subName;
            return p;
        }
        public string ConvertToJson()
        {
            var proto = Property.ConvertToProto(this);
            return JsonFormatter.Default.Format(proto);
        }
        /// <summary>
        /// Parent property if extended property is created
        /// </summary>
        public IProperty? ParentProperty { get; set; }
        public string NameWithExtention { get { if (this.ParentProperty == null) return this.Name; return this.ParentProperty.Name + this.Name; } }
        [Browsable(false)]
        public bool IsComplex { get; internal set; }
        [Browsable(false)]
        public bool IsComplexRefId { get; set; }
        [Browsable(false)]
        public bool IsComplexRefGuid { get; private set; }
        [Browsable(false)]
        public bool IsComplexDesc { get; private set; }
        [Browsable(false)]
        public bool IsPKey { get { return this.DataType.IsPKey; } }
        [Browsable(false)]
        public bool IsRefParent { get { return this.DataType.IsRefParent; } }
        [Browsable(false)]
        public bool IsDocShared { get; set; }
        public IProperty AddExtensionPropertyRefId(string subName, IComplexRef complexRef, bool isNullable, bool isCsNullable, int positionInConfigObject, uint position)
        {
            var node = new Property(this) { Name = subName, ParentProperty = this };
            node.Guid = complexRef.RefComplexObjectIdPropertyGuid;
            node.DataType = (DataType)this.Cfg.Model.GetIdRefDataType(node, true);
            node.DataType.IsPKey = false;
            node.IsNullable = isNullable;
            node.IsCsNullable = isCsNullable;
            node.IsComplexRefId = true;
            node.DataType.ObjectRef0.ForeignObjectGuid = complexRef.ForeignObjectGuid;
            node.DataType.ObjectRef0.RefComplexObjectIdPropertyGuid = complexRef.RefComplexObjectIdPropertyGuid;
            node.PositionInConfigObject = positionInConfigObject;
            node.Position = position;
            return node;
        }
        public IProperty AddExtensionPropertyGd(string subName, bool isNullable, bool isCsNullable, uint position)
        {
            var node = new Property(this) { Name = subName };
            if (string.IsNullOrEmpty(this.RefComplexObjectGdPropertyGuid))
                this.RefComplexObjectGdPropertyGuid = System.Guid.NewGuid().ToString();
            node.Guid = this.RefComplexObjectGdPropertyGuid;
            node.DataType = (DataType)this.Cfg.Model.GetDataTypeInt(node, false, isNullable);
            node.IsNullable = isNullable;
            node.IsCsNullable = isCsNullable;
            node.ParentProperty = this;
            node.IsComplexRefGuid = true;
            node.Position = position;
            return node;
        }
        public IProperty AddExtensionPropertyDesc(string subName, bool isNullable, bool isCsNullable, uint position)
        {
            var node = new Property(this) { Name = subName };
            if (string.IsNullOrEmpty(this.RefComplexObjectDescrPropertyGuid))
                this.RefComplexObjectDescrPropertyGuid = System.Guid.NewGuid().ToString();
            node.Guid = this.RefComplexObjectDescrPropertyGuid;
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.STRING, Length = this.Cfg.Model.ComplexPropertyRefDescrLength };
            node.IsNullable = isNullable;
            node.IsCsNullable = isCsNullable;
            node.ParentProperty = this;
            node.IsComplexDesc = true;
            node.Position = position;
            return node;
        }
        public IProperty AddExtensionPropertyString(string subName, uint length, string guid)
        {
            var node = new Property(this) { Name = this.Name + subName };
            node.Guid = guid;
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.STRING, Length = length };
            return node;
        }
        public IProperty AddExtensionPropertyNumerical(string subName, uint length, uint accuracy, string guid)
        {
            var node = new Property(this) { Name = this.Name + subName };
            node.Guid = guid;
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.NUMERICAL, Length = length, Accuracy = accuracy };
            return node;
        }
        #endregion Plugin group model

        /// <summary>
        /// Check if assignment is possible.
        /// </summary>
        /// <param name="from">Source property</param>
        /// <returns>Empty string is assigment is possible. If not, then returning a reason.</returns>
        public string CanAssignFrom(Property from)
        {
            if (this.DataType.DataTypeEnum == EnumDataType.ANY &&
                (from.DataType.DataTypeEnum == EnumDataType.CATALOG || from.DataType.DataTypeEnum == EnumDataType.CATALOGS
                || from.DataType.DataTypeEnum == EnumDataType.DOCUMENT || from.DataType.DataTypeEnum == EnumDataType.DOCUMENTS))
                return string.Empty;
            if (from.DataType.DataTypeEnum != this.DataType.DataTypeEnum)
                return $"Destination type '{System.Enum.GetName<EnumDataType>(this.DataType.DataTypeEnum)}' is not compatible with source type '{System.Enum.GetName<EnumDataType>(this.DataType.DataTypeEnum)}'.";
            if (!this.IsNullable && from.IsNullable)
                return "Can't assign nullable value to not nullable.";
            switch (from.DataType.DataTypeEnum)
            {
                case EnumDataType.ANY:
                case EnumDataType.BOOL:
                case EnumDataType.CHAR:
                case EnumDataType.DATE:
                case EnumDataType.DATETIMELOCAL:
                case EnumDataType.DATETIMEOFFSET:
                case EnumDataType.DATETIMEUTC:
                case EnumDataType.DATETIMEZ:
                case EnumDataType.TIME:
                case EnumDataType.TIMESPAN:
                case EnumDataType.TIMESPAN_TIME_ONLY:
                //case EnumDataType.TIMEZ:
                    break;
                case EnumDataType.CATALOG:
                    if (this.DataType.ObjectRef.ForeignObjectGuid != from.DataType.ObjectRef.ForeignObjectGuid)
                    {
                        var cf = (Catalog)this.Cfg.DicNodes[from.DataType.ObjectRef.ForeignObjectGuid];
                        var cd = (Catalog)this.Cfg.DicNodes[this.DataType.ObjectRef.ForeignObjectGuid];
                        return $"Destination catalog type '{cd.Name}' not equal source catalog type '{cf.Name}'.";
                    }
                    break;
                case EnumDataType.DOCUMENT:
                    if (this.DataType.ObjectRef.ForeignObjectGuid != from.DataType.ObjectRef.ForeignObjectGuid)
                    {
                        var cf = (Document)this.Cfg.DicNodes[from.DataType.ObjectRef.ForeignObjectGuid];
                        var cd = (Document)this.Cfg.DicNodes[this.DataType.ObjectRef.ForeignObjectGuid];
                        return $"Destination document type '{cd.Name}' not equal source document type '{cf.Name}'.";
                    }
                    break;
                case EnumDataType.CATALOGS:
                    if (from.DataType.DataTypeEnum == EnumDataType.CATALOG)
                    {
                        bool found = false;
                        foreach (var t in this.DataType.ListObjectRefs)
                        {
                            if (t.ForeignObjectGuid == from.DataType.ObjectRef.ForeignObjectGuid)
                            { found = true; break; }
                        }
                        if (!found)
                        {
                            var cf = (Catalog)this.Cfg.DicNodes[from.DataType.ObjectRef.ForeignObjectGuid];
                            return $"Destination catalogs type '{this.Name}' property is not supporting source catalog type '{cf.Name}'.";
                        }
                    }
                    if (from.DataType.DataTypeEnum == EnumDataType.CATALOGS)
                    {
                        ThrowHelper.ThrowNotSupportedException();
                        //return $"Destination '{this.Name}' property is not supporting source catalogs type of '{from.Name}' property";
                    }
                    break;
                case EnumDataType.DOCUMENTS:
                    if (from.DataType.DataTypeEnum == EnumDataType.DOCUMENT)
                    {
                        bool found = false;
                        foreach (var t in this.DataType.ListObjectRefs)
                        {
                            if (t.ForeignObjectGuid == from.DataType.ObjectRef.ForeignObjectGuid)
                            { found = true; break; }
                        }
                        if (!found)
                        {
                            var cf = (Document)this.Cfg.DicNodes[from.DataType.ObjectRef.ForeignObjectGuid];
                            return $"Destination documents type '{this.Name}' property is not supporting source document type '{cf.Name}'.";
                        }
                    }
                    if (from.DataType.DataTypeEnum == EnumDataType.DOCUMENTS)
                    {
                        ThrowHelper.ThrowNotSupportedException();
                        //bool found = false;
                        //foreach (var t in this.DataType.ListObjectGuids)
                        //{
                        //    foreach (var t in this.DataType.ListObjectGuids)
                        //    {
                        //        if (t == from.DataType.ObjectGuid)
                        //        { found = true; break; }
                        //    }
                        //}
                        //if (!found)
                        //{
                        //    return $"Destination '{this.Name}' property is not supporting source documents type of '{from.Name}' property";
                        //}
                    }
                    break;
                case EnumDataType.ENUMERATION:
                    if (this.DataType.EnumerationName != from.DataType.EnumerationName)
                        return $"Destination enumeration type '{this.DataType.EnumerationName}' not equal source enumeration type '{from.DataType.EnumerationName}'.";
                    break;
                case EnumDataType.NUMERICAL:
                    return CanAssignFromNumerical(from.Length, from.Accuracy, this.IsNullable);
                case EnumDataType.STRING:
                    if (this.Length < from.Length)
                        return "Destination length is shorter than source length.";
                    break;
                default:
                    ThrowHelper.ThrowNotSupportedException();
                    break;
            }
            return string.Empty;
        }
        public string CanAssignFromNumerical(uint fromLength, uint fromAccuracy, bool fromIsNullable)
        {
            if (!this.IsNullable && fromIsNullable)
                return "Can't assign nullable value to not nullable.";
            if (this.Length < fromLength)
                return "Destination length is shorter than source length.";
            if (this.Accuracy < fromAccuracy)
                return "Destination accuracy is less than source accuracy.";
            return string.Empty;
        }
        public string CanAssignToNumerical(uint toLength, uint toAccuracy, bool toIsNullable)
        {
            if (this.IsNullable && !toIsNullable)
                return "Can't assign nullable value to not nullable.";
            if (this.Length > toLength)
                return "Destination length is shorter than source length.";
            if (this.Accuracy > toAccuracy)
                return "Destination accuracy is less than source accuracy.";
            return string.Empty;
        }
        //public string CanAssignFromString(uint fromLength, bool fromIsNullable)
        //{
        //    if (!this.IsNullable && fromIsNullable)
        //        return "Can't assign nullable value to not nullable";
        //    if (this.Length < fromLength)
        //        return "Destination length is shorter than source length";
        //    return string.Empty;
        //}
        public string GetShortDescription(StringBuilder sb)
        {
            sb.Append(this.Name);
            sb.Append('[');
            sb.Append(this.Guid);
            sb.Append(']');
            if (this.IsNullable)
                sb.Append(" Nullable");
            sb.Append(" ");
            DataType.GetTypeDesc(this.DataType, sb);
            return sb.ToString();
        }
        //public IReadOnlyList<IProperty> GetPropertiesForRefTable(ITreeConfigNode parent, string propertyVersionGuid, bool isOptimistic)
        //{
        //    throw new NotImplementedException();
        //    var parentTable = ((ICompositeName)parent).CompositeName;
        //    var res = new List<IProperty>();
        //    var prp = this.Cfg.Model.GetPropertyPkId(this, this.Guid);
        //    res.Add(prp);
        //    //prp = this.Cfg.Model.GetPropertyRefParent(this.GroupProperties, this.PropertyRefParentGuid, "Ref" + parentTable, false);
        //    res.Add(prp);
        //    if (isOptimistic)
        //    {
        //        prp = this.Cfg.Model.GetPropertyVersion(this, propertyVersionGuid);
        //        res.Add(prp);
        //    }
        //    return res;
        //}
    }
}
