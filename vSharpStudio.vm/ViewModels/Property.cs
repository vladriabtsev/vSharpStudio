using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.DirectoryServices;
using System.Numerics;
using System.Text;
using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using ViewModelBase;
using vSharpStudio.common;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Property:{Name,nq} Type:{DataType.GetTypeDesc(this.DataType),nq}")]
    public partial class Property : IDataTypeObject, ICanAddNode, ICanGoLeft, INodeGenSettings, IEditableNode
    {
        [BrowsableAttribute(false)]
        public GroupListProperties ParentGroupListProperties { get { Debug.Assert(this.Parent != null); return (GroupListProperties)this.Parent; } }
        [BrowsableAttribute(false)]
        public IGroupListProperties ParentGroupListPropertiesI { get { Debug.Assert(this.Parent != null); return (IGroupListProperties)this.Parent; } }
        [Browsable(false)]
        // Can be used by a generator to keep calculated property data
        public object? Tag { get; set; }
        //[Browsable(false)]
        //public static IConfig? Config { get; set; }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return new ConfigNodesCollection<ITreeConfigNodeSortable>(this);
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupListProperties.Children;
        }
        public override bool HasChildren()
        {
            return false;
        }
        #endregion ITree

        public static readonly string DefaultName = "Property";
        [Browsable(false)]
        new public string IconName { get { return "iconProperty"; } }
        //protected override string GetNodeIconName() { return "iconProperty"; }
        [Browsable(false)]
        public string? ComplexObjectName { get; set; }
        public string ComplexObjectNameWithDot() { if (!string.IsNullOrEmpty(this.ComplexObjectName)) return $"{this.ComplexObjectName}."; return ""; }
        partial void OnCreated()
        {
            this.IsNullable = false;
            this.MinLengthRequirement = "";
            this.MaxLengthRequirement = "";
            this.RangeValuesRequirementStr = "";
            this.LinesOnScreen = 1;
            this.IsIncludableInModels = true;
            this.DataType.Parent = this;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
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
        internal Property(ITreeConfigNode parent, string guid, string name)
            : this(parent)
        {
            this.Guid = guid;
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
        [BrowsableAttribute(false)]
        public bool IsPKey { get { return this.DataType.IsPKey; } }
        [BrowsableAttribute(false)]
        public bool IsRefParent { get { return this.DataType.IsRefParent; } }
        [BrowsableAttribute(false)]
        public bool IsComputed { get; set; }
        [BrowsableAttribute(false)]
        public bool IsDocShared { get; set; }

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
        [BrowsableAttribute(false)]
        public IDataType IDataType { get { return this._DataType; } }
        //public string DefaultValue { get { return this.DataType.DefaultValue; } }

        #region Tree operations
        public override bool NodeCanUp()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListProperties.ListProperties.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Property)this.ParentGroupListProperties.ListProperties.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            this.ParentGroupListProperties.ListProperties.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if (this.ParentGroupListProperties.ListProperties.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Property)this.ParentGroupListProperties.ListProperties.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            this.ParentGroupListProperties.ListProperties.MoveDown(this);
            this.SetSelected(this);
        }

        public void NodeRemove(bool ask = true)
        {
            this.ParentGroupListProperties.Remove(this);
            this.Parent = null;
        }
        public override ITreeConfigNode NodeAddClone()
        {
            Debug.Assert(this.Parent != null);
            var node = Property.Clone(this.Parent, this, true, true);
            this.ParentGroupListProperties.Add(node);
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
            this.ParentGroupListProperties.Add(node);
            node.Position = this.ParentGroupListProperties.GetNextPosition();
            this.GetUniqueName(Property.DefaultName, node, this.ParentGroupListProperties.ListProperties);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            this.ParentGroupListProperties.ListProperties.Remove(this);
        }
        #endregion Tree operations

        #region Editing logic
        partial void OnIsNullableChanged()
        {
            this.NotifyPropertyChanged(() => this.ClrType);
            this.Tag = null;
        }
        private void OnDataTypeEnumChanged()
        {
            switch (this.DataType.DataTypeEnum)
            {
                case EnumDataType.CHAR:
                //case EnumDataType.ANY:
                case EnumDataType.BOOL:
                    this.Length = 0;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ObjectGuid = string.Empty;
                    this.ListObjectGuids.Clear();
                    break;
                case EnumDataType.DATE:
                    this.Length = 0;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ObjectGuid = string.Empty;
                    this.ListObjectGuids.Clear();
                    break;
                case EnumDataType.DATETIMELOCAL:
                case EnumDataType.DATETIMEUTC:
                //case EnumDataType.DATETIME:
                //case EnumDataType.DATETIMEZ:
                case EnumDataType.TIME:
                    this.Length = 0;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ObjectGuid = string.Empty;
                    this.ListObjectGuids.Clear();
                    break;
                case EnumDataType.CATALOGS:
                case EnumDataType.DOCUMENTS:
                    this.Length = 0;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ObjectGuid = string.Empty;
                    this.ListObjectGuids.Clear();
                    break;
                case EnumDataType.CATALOG:
                case EnumDataType.DOCUMENT:
                case EnumDataType.ENUMERATION:
                    this.Length = 0;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ObjectGuid = string.Empty;
                    this.ListObjectGuids.Clear();
                    break;
                case EnumDataType.NUMERICAL:
                    this.Length = 6;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ObjectGuid = string.Empty;
                    this.ListObjectGuids.Clear();
                    break;
                case EnumDataType.STRING:
                    this.Length = 25;
                    this.Accuracy = 0;
                    this.IsPositive = false;
                    this.ObjectGuid = string.Empty;
                    this.ListObjectGuids.Clear();
                    break;
                default:
                    throw new NotSupportedException();
            }
            this.NotifyPropertyChanged(() => this.ClrType);
            this.Tag = null;
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            if (this.DataType.DataTypeEnum != EnumDataType.STRING)
            {
                lst.Add(this.GetPropertyName(() => this.MinLengthRequirement));
                lst.Add(this.GetPropertyName(() => this.MaxLengthRequirement));
            }
            if (this.DataType.DataTypeEnum != EnumDataType.NUMERICAL)
            {
                lst.Add(this.GetPropertyName(() => this.Accuracy));
                lst.Add(this.GetPropertyName(() => this.IsPositive));
            }
            if (this.DataType.DataTypeEnum != EnumDataType.STRING && this.DataType.DataTypeEnum != EnumDataType.NUMERICAL)
            {
                lst.Add(this.GetPropertyName(() => this.Length));
            }
            if (this.DataType.DataTypeEnum != EnumDataType.TIME &&
                this.DataType.DataTypeEnum != EnumDataType.DATETIMELOCAL &&
                this.DataType.DataTypeEnum != EnumDataType.DATETIMEUTC) // &&
                                                                        //this.DataType.DataTypeEnum != EnumDataType.DATETIME &&
                                                                        //this.DataType.DataTypeEnum != EnumDataType.DATETIMEZ)
            {
                lst.Add(this.GetPropertyName(() => this.AccuracyForTime));
            }
            if (this.DataType.DataTypeEnum != EnumDataType.CATALOGS &&
                this.DataType.DataTypeEnum != EnumDataType.DOCUMENTS)
            {
                lst.Add(this.GetPropertyName(() => this.ListObjectGuids));
                lst.Add(this.GetPropertyName(() => this.DefaultValue));
            }
            if (this.DataType.DataTypeEnum != EnumDataType.CATALOG &&
                this.DataType.DataTypeEnum != EnumDataType.DOCUMENT &&
                this.DataType.DataTypeEnum != EnumDataType.ENUMERATION)
            {
                lst.Add(this.GetPropertyName(() => this.ObjectGuid));
                lst.Add(this.GetPropertyName(() => this.DefaultValue));
            }
            if (this.Accuracy != 0)
            {
                lst.Add(this.GetPropertyName(() => this.IsPositive));
            }
            if (this.DataType.DataTypeEnum != EnumDataType.STRING &&
                this.DataType.DataTypeEnum != EnumDataType.CHAR &&
                this.DataType.DataTypeEnum != EnumDataType.DATE &&
                this.DataType.DataTypeEnum != EnumDataType.DATETIMELOCAL &&
                this.DataType.DataTypeEnum != EnumDataType.DATETIMEUTC &&
                //this.DataType.DataTypeEnum != EnumDataType.DATETIME &&
                //this.DataType.DataTypeEnum != EnumDataType.DATETIMEZ &&
                this.DataType.DataTypeEnum != EnumDataType.NUMERICAL)
            {
                lst.Add(this.GetPropertyName(() => this.RangeValuesRequirements));
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
                this.NotifyPropertyChanged();
                this.ValidateProperty();
                this.OnDataTypeEnumChanged();
                this.NotifyPropertyChanged(() => this.PropertyDefinitions);
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
                this.NotifyPropertyChanged();
                this.NotifyPropertyChanged(() => this.ClrType);
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
                this.NotifyPropertyChanged();
                this.NotifyPropertyChanged(() => this.ClrType);
                this.ValidateProperty();
                this.NotifyPropertyChanged(() => this.PropertyDefinitions);
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
                this.NotifyPropertyChanged();
                this.NotifyPropertyChanged(() => this.ClrType);
                this.ValidateProperty();
                this.Tag = null;
            }
        }
        [Category("")]
        [Editor(typeof(EditorDataTypeObjectName), typeof(EditorDataTypeObjectName))]
        [PropertyOrderAttribute(15)]
        public string ObjectGuid
        {
            get { return this.DataType.ObjectGuid; }
            set
            {
                this.DataType.ObjectGuid = value;
                this.NotifyPropertyChanged();
                this.NotifyPropertyChanged(() => this.ClrType);
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
        //        this.NotifyPropertyChanged();
        //        this.ValidateProperty();
        //    }
        //}
        //[DisplayName("Positive")]
        //[Description("Expected always >= 0")]
        [Category("")]
        [PropertyOrderAttribute(16)]
        public ObservableCollection<string> ListObjectGuids
        {
            get { return this.DataType.ListObjectGuids; }
        }
        #endregion Editing logic

        [BrowsableAttribute(false)]
        public PropertyRangeValuesRequirements? RangeValuesRequirements { get; set; }
        [BrowsableAttribute(false)]
        public IPropertyRangeValuesRequirements? RangeValuesRequirementsI { get { return RangeValuesRequirements; } }
        [BrowsableAttribute(false)]
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
                        var en = (Enumeration)this.Cfg.DicNodes[this.ObjectGuid];
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
            this.NotifyPropertyChanged(() => this.NodeNameDecorations);
        }
        partial void OnIsStartNewTabControlChanged()
        {
            this.NotifyPropertyChanged(() => this.NodeNameDecorations);
        }
        partial void OnTabNameChanged()
        {
            this.NotifyPropertyChanged(() => this.NodeNameDecorations);
        }
        //partial void OnIsTryAttachChanged()
        //{
        //    this.NotifyPropertyChanged(() => this.NodeNameDecorations);
        //}
        partial void OnIsStartNewRowChanged()
        {
            this.NotifyPropertyChanged(() => this.NodeNameDecorations);
        }
        //[BrowsableAttribute(false)]
        //public new bool IsHasNew { get { return this.IsNew; } }
        public bool IsGridSortableGet()
        {
            if (this.IsGridSortable == EnumUseType.Yes)
                return true;
            if (this.IsGridSortable == EnumUseType.No)
                return false;
            return this.ParentGroupListProperties.GetIsGridSortable();
        }
        public bool IsGridFilterableGet()
        {
            if (this.IsGridFilterable == EnumUseType.Yes)
                return true;
            if (this.IsGridFilterable == EnumUseType.No)
                return false;
            return this.ParentGroupListProperties.GetIsGridFilterable();
        }
        public bool IsGridSortableCustomGet()
        {
            if (this.IsGridSortableCustom == EnumUseType.Yes)
                return true;
            if (this.IsGridSortableCustom == EnumUseType.No)
                return false;
            return this.ParentGroupListProperties.GetIsGridSortableCustom();
        }
    }
}
