using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("Group:{Name,nq} Count:{ListProperties.Count,nq} HasChanged:{IsHasChanged} HasErrors:{CountErrors}-{HasErrors}")]
    public partial class GroupListProperties : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNodeGroup
    {
        [BrowsableAttribute(false)]
        public bool IsNew { get { return false; } }
        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            if (this.Parent is Catalog c)
            {
                return c.Children;
            }
            else if (this.Parent is CatalogFolder cf)
            {
                return cf.Children;
            }
            else if (this.Parent is Document d)
            {
                return d.Children;
            }
            else if (this.Parent is Detail dt)
            {
                return dt.Children;
            }
            else if (this.Parent is GroupDocuments gd)
            {
                return gd.Children;
            }
            throw new NotImplementedException();
        }
        #endregion ITree

        new public ConfigNodesCollection<Property> Children { get { return this.ListProperties; } }
        partial void OnCreated()
        {
            this.IsEditable = false;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        private void Init()
        {
            if (this.Parent is GroupDocuments)
            {
                this._Name = Defaults.PropertiesSharedGroupName;
            }
            else
            {
                this._Name = Defaults.PropertiesGroupName;
            }
            this.ListProperties.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListProperties.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListProperties.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListProperties.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
        }

        protected override void OnParentChanged()
        {
            if (this.Parent is GroupDocuments)
            {
                this._Name = Defaults.PropertiesSharedGroupName;
            }
            else
            {
                this._Name = Defaults.PropertiesGroupName;
            }
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public Property AddProperty(string name)
        {
            var node = new Property(this) { Name = name };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddProperty(string name, EnumDataType type, uint length, uint accuracy)
        {
            var node = new Property(this) { Name = name };
            node.DataType = new DataType(node) { DataTypeEnum = type, Length = length, Accuracy = accuracy };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyBool(string name, bool isNullable = false)
        {
            var node = new Property(this) { Name = name };
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.BOOL };
            node.IsNullable = isNullable;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyChar(string name, bool isNullable = false)
        {
            var node = new Property(this) { Name = name };
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.CHAR };
            node.IsNullable = isNullable;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyString(string name, uint length, bool isNullable = false, uint? min_length = null, uint? max_length = null)
        {
            var node = new Property(this) { Name = name };
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.STRING, Length = length };
            node.IsNullable = isNullable;
            if (min_length != null)
                node.MinLengthRequirement = min_length.ToString()!;
            if (max_length != null)
                node.MaxLengthRequirement = max_length.ToString()!;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyNumerical(string name, uint length, uint accuracy, bool isNullable = false, bool isPositive = false)
        {
            var node = new Property(this) { Name = name };
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.NUMERICAL, Length = length, Accuracy = accuracy, IsPositive = isPositive };
            node.IsNullable = isNullable;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyTime(string name, bool isNullable = false, EnumTimeAccuracyType accuracy = EnumTimeAccuracyType.SECOND)
        {
            var node = new Property(this) { Name = name };
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.TIME };
            node.IsNullable = isNullable;
            node.AccuracyForTime = accuracy;
            this.NodeAddNewSubNode(node);
            return node;
        }
        //public Property AddPropertyTimeZ(string name, bool isNullable = false)
        //{
        //    var dt = new DataType() { DataTypeEnum = EnumDataType.TIMEZ, IsNullable = isNullable };
        //    var node = new Property(this) { Name = name, DataType = dt };
        //    this.NodeAddNewSubNode(node);
        //    return node;
        //}
        public Property AddPropertyDate(string name, bool isNullable = false)
        {
            var node = new Property(this) { Name = name };
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.DATE };
            node.IsNullable = isNullable;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyDateTimeUtc(string name, bool isNullable = false, EnumTimeAccuracyType accuracy = EnumTimeAccuracyType.SECOND)
        {
            var node = new Property(this) { Name = name };
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.DATETIMEUTC };
            node.IsNullable = isNullable;
            node.AccuracyForTime = accuracy;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyDateTimeLocal(string name, bool isNullable = false, EnumTimeAccuracyType accuracy = EnumTimeAccuracyType.SECOND)
        {
            var node = new Property(this) { Name = name };
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.DATETIMELOCAL };
            node.IsNullable = isNullable;
            node.AccuracyForTime = accuracy;
            this.NodeAddNewSubNode(node);
            return node;
        }
        //public Property AddPropertyDateTimeZ(string name, bool isNullable = false, EnumTimeAccuracyType accuracy = EnumTimeAccuracyType.SECOND)
        //{
        //    var dt = new DataType() { DataTypeEnum = EnumDataType.DATETIMEZ };
        //    var node = new Property(this) { Name = name, DataType = dt };
        //    node.IsNullable = isNullable;
        //    node.AccuracyForTime = accuracy;
        //    this.NodeAddNewSubNode(node);
        //    return node;
        //}
        //public Property AddPropertyDateTimeOffset(string name, bool isNullable = false)
        //{
        //    var dt = new DataType() { DataTypeEnum = EnumDataType.DATETIMEOFFSET, IsNullable = isNullable };
        //    var node = new Property(this) { Name = name, DataType = dt };
        //    this.NodeAddNewSubNode(node);
        //    return node;
        //}
        //public Property AddPropertyTimeSpan(string name, bool isNullable = false)
        //{
        //    var dt = new DataType() { DataTypeEnum = EnumDataType.TIMESPAN, IsNullable = isNullable };
        //    var node = new Property(this) { Name = name, DataType = dt };
        //    this.NodeAddNewSubNode(node);
        //    return node;
        //}
        public Property AddPropertyEnumeration(string name, Enumeration en, bool isNullable)
        {
            var node = new Property(this) { Name = name };
            node.DataType = new DataType(node);
            node.DataType.ObjectGuid = en.Guid;
            node.DataType.DataTypeEnum = EnumDataType.ENUMERATION;
            node.IsNullable = isNullable;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public uint GetNextPosition()
        {
            // Reserved positions
            // 1  not used
            // 2  not used
            // 3  not used
            // 4  __is_need_insert
            // 5  __is_need_update
            // 6  PropertyId
            // 7  PropertyObjectVersion
            // 8  PropertyRefParent, PropertyDocumentDate
            // 9  PropertyCatalogCode, PropertyCatalogCodeInt, PropertyDocumentCodeString, PropertyDocumentCodeInt
            // 10 PropertyCatalogName
            // 11 PropertyCatalogDescription
            // 12 PropertyIsFolder
            // 13 PropertyIsOpen
            if (this.LastGenPosition == 0)
            {
                if (this.Parent is Catalog)
                {
                    this.LastGenPosition = 13;

                }
                else if (this.Parent is Detail)
                {
                    this.LastGenPosition = 8;
                }
                else if (this.Parent is CatalogFolder)
                {
                    this.LastGenPosition = 13;
                }
                else if (this.Parent is Document)
                {
                    this.LastGenPosition = 10;
                }
                else if (this.Parent is GroupDocuments)
                {
                    //this.LastGenPosition = 0;
                }
                else
                    throw new NotImplementedException();
            }
            this.LastGenPosition++;
            return this.LastGenPosition;
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            Property node = null!;
            if (node_impl == null)
            {
                node = new Property(this);
            }
            else
            {
                node = (Property)node_impl;
            }

            this.Add(node);
            node.DataType.Parent = node;
            node.Position = this.GetNextPosition();
            if (node_impl == null)
            {
                this.GetUniqueName(Property.DefaultName, node, this.ListProperties);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>();
            lst.Add(this.GetPropertyName(() => this.Description));
            lst.Add(this.GetPropertyName(() => this.Guid));
            lst.Add(this.GetPropertyName(() => this.NameUi));
            lst.Add(this.GetPropertyName(() => this.Parent));
            lst.Add(this.GetPropertyName(() => this.Children));
            return lst.ToArray();
        }
        public bool GetIsGridSortable()
        {
            if (this.IsGridSortable == EnumUseType.Yes)
                return true;
            if (this.IsGridSortable == EnumUseType.No)
                return false;
            if (this.Parent is Detail dd)
                return dd.ParentGroupListDetails.GetIsGridSortable();
            else if (this.Parent is Catalog c)
                return c.ParentGroupListCatalogs.GetIsGridSortable();
            else if (this.Parent is Document d)
                return d.ParentGroupListDocuments.ParentGroupDocuments.IsGridSortableGet();
            else if (this.Parent is CatalogFolder cf)
                return cf.ParentCatalog.IsGridSortableGet();
            else if (this.Parent is GroupDocuments gd)
                return gd.IsGridSortableGet();
            else
                throw new NotImplementedException();
        }
        public bool GetIsGridFilterable()
        {
            if (this.IsGridFilterable == EnumUseType.Yes)
                return true;
            if (this.IsGridFilterable == EnumUseType.No)
                return false;
            if (this.Parent is Detail dd)
                return dd.ParentGroupListDetails.GetIsGridFilterable();
            else if (this.Parent is Catalog c)
                return c.ParentGroupListCatalogs.GetIsGridFilterable();
            else if (this.Parent is Document d)
                return d.ParentGroupListDocuments.ParentGroupDocuments.IsGridFilterableGet();
            else if (this.Parent is CatalogFolder cf)
                return cf.ParentCatalog.IsGridFilterableGet();
            else if (this.Parent is GroupDocuments gd)
                return gd.IsGridFilterableGet();
            else
                throw new NotImplementedException();
        }
        public bool GetIsGridSortableCustom()
        {
            if (this.IsGridSortableCustom == EnumUseType.Yes)
                return true;
            if (this.IsGridSortableCustom == EnumUseType.No)
                return false;
            if (this.Parent is Detail dd)
                return dd.ParentGroupListDetails.GetIsGridSortableCustom();
            else if (this.Parent is Catalog c)
                return c.ParentGroupListCatalogs.GetIsGridSortableCustom();
            else if (this.Parent is Document d)
                return d.ParentGroupListDocuments.ParentGroupDocuments.IsGridSortableCustomGet();
            else if (this.Parent is CatalogFolder cf)
                return cf.ParentCatalog.IsGridSortableCustomGet();
            else if (this.Parent is GroupDocuments gd)
                return gd.IsGridSortableCustomGet();
            else
                throw new NotImplementedException();
        }
    }
}
