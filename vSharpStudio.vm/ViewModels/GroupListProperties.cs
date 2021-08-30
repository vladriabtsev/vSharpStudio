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
    [DebuggerDisplay("Group:{Name,nq} Count:{ListProperties.Count,nq}")]
    public partial class GroupListProperties : ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNodeGroup
    {
        #region ITree
        public override IEnumerable<ITreeConfigNode> GetListChildren()
        {
            return this.Children;
        }
        public override IEnumerable<ITreeConfigNode> GetListSiblings()
        {
            if (this.Parent is Catalog)
            {
                var p = this.Parent as Catalog;
                return p.Children;
            }
            else if (this.Parent is CatalogFolder)
            {
                var p = this.Parent as CatalogFolder;
                return p.Children;
            }
            else if (this.Parent is Document)
            {
                var p = this.Parent as Document;
                return p.Children;
            }
            else if (this.Parent is PropertiesTab)
            {
                var p = this.Parent as PropertiesTab;
                return p.Children;
            }
            else if (this.Parent is GroupDocuments)
            {
                var p = this.Parent as GroupDocuments;
                return p.Children;
            }
            throw new NotImplementedException();
        }
        public override bool HasChildren()
        {
            return this.Children.Count > 0;
        }
        #endregion ITree

        public ConfigNodesCollection<Property> Children { get { return this.ListProperties; } }
        partial void OnInit()
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
                this._Name = "Shared";
            }
            else
            {
                this._Name = "Properties";
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
                this._Name = "Shared";
            }
            else
            {
                this._Name = "Properties";
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
        public Property AddProperty(string name, DataType type)
        {
            var node = new Property(this) { Name = name, DataType = type };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddProperty(string name, EnumDataType type, uint length, uint accuracy)
        {
            var node = new Property(this) { Name = name, DataType = new DataType() { DataTypeEnum = type, Length = length, Accuracy = accuracy } };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyChar(string name, bool isNullable = false)
        {
            var dt = new DataType() { DataTypeEnum = EnumDataType.CHAR, IsNullable = isNullable };
            var node = new Property(this) { Name = name, DataType = dt };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyString(string name, uint length, bool isNullable = false)
        {
            var dt = new DataType() { DataTypeEnum = EnumDataType.STRING, Length = length, IsNullable = isNullable };
            var node = new Property(this) { Name = name, DataType = dt };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyNumerical(string name, uint length, uint accuracy, bool isNullable = false, bool isPositive = false)
        {
            var dt = new DataType() { DataTypeEnum = EnumDataType.NUMERICAL, Length = length, Accuracy = accuracy, IsNullable = isNullable, IsPositive = isPositive };
            var node = new Property(this) { Name = name, DataType = dt };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyTime(string name, bool isNullable = false)
        {
            var dt = new DataType() { DataTypeEnum = EnumDataType.TIME, IsNullable = isNullable };
            var node = new Property(this) { Name = name, DataType = dt };
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
            var dt = new DataType() { DataTypeEnum = EnumDataType.DATE, IsNullable = isNullable };
            var node = new Property(this) { Name = name, DataType = dt };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyDateTime(string name, bool isNullable = false)
        {
            var dt = new DataType() { DataTypeEnum = EnumDataType.DATETIME, IsNullable = isNullable };
            var node = new Property(this) { Name = name, DataType = dt };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyDateTimeZ(string name, bool isNullable = false)
        {
            var dt = new DataType() { DataTypeEnum = EnumDataType.DATETIMEZ, IsNullable = isNullable };
            var node = new Property(this) { Name = name, DataType = dt };
            this.NodeAddNewSubNode(node);
            return node;
        }
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
            var dt = new DataType() { DataTypeEnum = EnumDataType.ENUMERATION, ObjectGuid = en.Guid };
            dt.IsNullable = isNullable;
            var node = new Property(this) { Name = name, DataType = dt };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public uint GetNextPosition()
        {
            if (this.LastGenPosition == 0)
            {
                if (this.Parent is Catalog)
                {
                    this.LastGenPosition = 7;

                }
                else if (this.Parent is PropertiesTab)
                {
                    this.LastGenPosition = 2;
                }
                else if (this.Parent is CatalogFolder)
                {
                    this.LastGenPosition = 7;
                }
                else if (this.Parent is Document)
                {
                    this.LastGenPosition = 4;
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
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode node_impl = null)
        {
            Property node = null;
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
    }
}
