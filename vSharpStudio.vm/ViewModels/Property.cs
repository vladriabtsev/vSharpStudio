using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return new List<ITreeConfigNode>();
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            var p = this.Parent as GroupListProperties;
            if (p == null)
                return new List<ITreeConfigNode>();
            return p.Children;
        }
        public override bool HasChildren()
        {
            return false;
        }
        #endregion ITree

        public ConfigNodesCollection<ITreeConfigNode> Children { get; private set; }
        public static readonly string DefaultName = "Property";
        [Browsable(false)]
        new public string IconName { get { return "iconProperty"; } }
        //protected override string GetNodeIconName() { return "iconProperty"; }
        [Browsable(false)]
        public string ComplexObjectName { get; set; }
        public string ComplexObjectNameWithDot() { if (!string.IsNullOrEmpty(this.ComplexObjectName)) return $"{this.ComplexObjectName}."; return ""; }
        partial void OnInit()
        {
            this.IsIncludableInModels = true;
            this.DataType.Parent = this;
            this.UpdateVisibility();
        }
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
        }
        internal Property(ITreeConfigNode parent, string guid, string name, DataType dataType)
            : this(parent)
        {
            this.Guid = guid;
            this._Name = name;
            this._DataType = dataType;
        }
        internal Property(ITreeConfigNode parent, string name, EnumDataType type, string guidOfType)
            : this(parent)
        {
            this._Name = name;
            this._DataType = new DataType(type, guidOfType);
        }
        internal Property(ITreeConfigNode parent, string name, EnumDataType type, uint? length = null, uint? accuracy = null, bool? isPositive = null)
            : this(parent)
        {
            this._Name = name;
            this._DataType = new DataType(type, length, accuracy);
        }
        [BrowsableAttribute(false)]
        public bool IsPKey { get; set; }
        [BrowsableAttribute(false)]
        public bool IsComputed { get; set; }

        [PropertyOrderAttribute(10)]
        public string ClrType
        {
            get { return this.DataType.ClrTypeName; }
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
                if ((this.Parent as GroupListProperties).ListProperties.CanUp(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeUp()
        {
            var prev = (Property)(this.Parent as GroupListProperties).ListProperties.GetPrev(this);
            if (prev == null)
            {
                return;
            }

            this.SetSelected(prev);
        }

        public override void NodeMoveUp()
        {
            (this.Parent as GroupListProperties).ListProperties.MoveUp(this);
            this.SetSelected(this);
        }

        public override bool NodeCanDown()
        {
            if (this.NodeCanAddClone())
            {
                if ((this.Parent as GroupListProperties).ListProperties.CanDown(this))
                {
                    return true;
                }
            }
            return false;
        }

        public override void NodeDown()
        {
            var next = (Property)(this.Parent as GroupListProperties).ListProperties.GetNext(this);
            if (next == null)
            {
                return;
            }

            this.SetSelected(next);
        }

        public override void NodeMoveDown()
        {
            (this.Parent as GroupListProperties).ListProperties.MoveDown(this);
            this.SetSelected(this);
        }

        public void NodeRemove(bool ask = true)
        {
            (this.Parent as GroupListProperties).Remove(this);
            this.Parent = null;
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = Property.Clone(this.Parent, this, true, true);
            (this.Parent as GroupListProperties).Add(node);
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
            var glp = (this.Parent as GroupListProperties);
            glp.Add(node);
            node.Position = glp.GetNextPosition();
            this.GetUniqueName(Property.DefaultName, node, (this.Parent as GroupListProperties).ListProperties);
            this.SetSelected(node);
            return node;
        }
        public void Remove()
        {
            var p = this.Parent as GroupListProperties;
            p.ListProperties.Remove(this);
        }
        #endregion Tree operations

        #region Editing logic
        private void OnDataTypeEnumChanged()
        {
            switch (this.DataType.DataTypeEnum)
            {
                case EnumDataType.CHAR:
                case EnumDataType.ANY:
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
                case EnumDataType.DATETIME:
                case EnumDataType.DATETIMEZ:
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
        }
        private void UpdateVisibility()
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
                lst.Add(this.GetPropertyName(() => this.MinValueRequirement));
                lst.Add(this.GetPropertyName(() => this.MaxValueRequirement));
            }
            if (this.DataType.DataTypeEnum != EnumDataType.STRING && this.DataType.DataTypeEnum != EnumDataType.NUMERICAL)
            {
                lst.Add(this.GetPropertyName(() => this.Length));
            }
            if (this.DataType.DataTypeEnum != EnumDataType.TIME &&
                this.DataType.DataTypeEnum != EnumDataType.DATETIME &&
                this.DataType.DataTypeEnum != EnumDataType.DATETIMEZ)
            {
                lst.Add(this.GetPropertyName(() => this.AccuracyOfTime));
            }
            if (this.DataType.DataTypeEnum != EnumDataType.DATE &&
                this.DataType.DataTypeEnum != EnumDataType.DATETIME &&
                this.DataType.DataTypeEnum != EnumDataType.DATETIMEZ)
            {
                lst.Add(this.GetPropertyName(() => this.MinDateRequirementEdit));
                lst.Add(this.GetPropertyName(() => this.MaxDateRequirementEdit));
            }
            if (this.DataType.DataTypeEnum != EnumDataType.CATALOGS &&
                this.DataType.DataTypeEnum != EnumDataType.DOCUMENTS)
            {
                lst.Add(this.GetPropertyName(() => this.ListObjectGuids));
            }
            if (this.DataType.DataTypeEnum != EnumDataType.CATALOG &&
                this.DataType.DataTypeEnum != EnumDataType.DOCUMENT &&
                this.DataType.DataTypeEnum != EnumDataType.ENUMERATION)
            {
                lst.Add(this.GetPropertyName(() => this.ObjectGuid));
            }
            if (this.Accuracy != 0)
            {
                lst.Add(this.GetPropertyName(() => this.IsPositive));
            }
            this.HidePropertiesForXceedPropertyGrid(lst.ToArray());
        }
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
                this.UpdateVisibility();
            }
        }
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
                this.ValidateProperty();
            }
        }
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
                this.ValidateProperty();
                this.UpdateVisibility();
            }
        }
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
                this.ValidateProperty();
            }
        }
        [DisplayName("Min Date")]
        [Description("Minimum value of valid date")]
        [PropertyOrderAttribute(36)]
        public DateTime? MinDateRequirementEdit
        {
            get { return this.MinDateRequirement?.ToDateTime(); }
            set
            {
                if (value != null)
                {
                    var t = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
                    this.MinDateRequirement = t.ToTimestamp();
                }
                else
                {
                    this.MinDateRequirement = null;
                }
                this.NotifyPropertyChanged();
                this.ValidateProperty();
            }
        }
        [DisplayName("Max Date")]
        [Description("Maximum value of valid date")]
        [PropertyOrderAttribute(37)]
        public DateTime? MaxDateRequirementEdit
        {
            get { return this.MaxDateRequirement?.ToDateTime(); }
            set
            {
                if (value != null)
                {
                    var t = DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
                    this.MaxDateRequirement = t.ToTimestamp();
                }
                else
                {
                    this.MaxDateRequirement = null;
                }
                this.NotifyPropertyChanged();
                this.ValidateProperty();
            }
        }
        [Editor(typeof(EditorDataTypeObjectName), typeof(EditorDataTypeObjectName))]
        [PropertyOrderAttribute(15)]
        public string ObjectGuid
        {
            get { return this.DataType.ObjectGuid; }
            set
            {
                this.DataType.ObjectGuid = value;
                this.NotifyPropertyChanged();
                this.ValidateProperty();
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
        [PropertyOrderAttribute(16)]
        public ObservableCollection<string> ListObjectGuids
        {
            get { return this.DataType.ListObjectGuids; }
        }
        #endregion Editing logic
    }
}
