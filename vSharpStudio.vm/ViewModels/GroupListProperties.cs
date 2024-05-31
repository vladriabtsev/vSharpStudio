using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    public interface IListProperties
    {
        ConfigNodesCollection<Property> ListProperties { get; }
        ConfigNodesCollection<Property> Children { get; }
        uint GetNextPosition();
        bool GetIsGridSortable();
        bool GetIsGridFilterable();
        bool GetIsGridSortableCustom();
        EnumPropertyAccess GetRolePropertyAccess(IRole role);
        EnumPrintAccess GetRolePropertyPrint(IRole role);
        void SetRoleAccess(IRole role, EnumPropertyAccess? edit, EnumPrintAccess? print);
    }
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class GroupListProperties : IListProperties, ITreeModel, ICanAddSubNode, ICanGoRight, ICanGoLeft, INodeGenSettings, IEditableNodeGroup, IRoleAccess
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{ListProperties.Count}";
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        public int IndexOf(IProperty p)
        {
            return this.ListProperties.IndexOf((Property)p);
        }
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

        public new ConfigNodesCollection<Property> Children { get { return this.ListProperties; } }
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
            this.ListProperties.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListProperties.OnAddedAction = (t) =>
            {
                t.OnAdded();
                t.InitRoles();
            };
            this.ListProperties.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListProperties.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
            if (this.Parent is GroupDocuments)
            {
                this._Name = Defaults.GroupSharedPropertiesName;
            }
            else
            {
                this._Name = Defaults.GroupPropertiesName;
            }
        }

        protected override void OnParentChanged()
        {
            if (this.Parent is GroupDocuments)
            {
                this._Name = Defaults.GroupSharedPropertiesName;
            }
            else
            {
                this._Name = Defaults.GroupPropertiesName;
            }
        }

        #region Tree operations
        public bool CanAddSubNode() { return true; }
        public Property AddProperty(string name, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddProperty(string name, EnumDataType type, uint length, uint accuracy, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = type, Length = length, Accuracy = accuracy };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyBool(string name, bool isNullable = false, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.BOOL };
            node.IsNullable = isNullable;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyChar(string name, bool isNullable = false, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.CHAR };
            node.IsNullable = isNullable;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyString(string name, uint length, bool isNullable = false, uint? min_length = null, uint? max_length = null, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.STRING, Length = length };
            node.IsNullable = isNullable;
            if (min_length != null)
                node.MinLengthRequirement = min_length.ToString()!;
            if (max_length != null)
                node.MaxLengthRequirement = max_length.ToString()!;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyNumerical(string name, uint length, uint accuracy, bool isNullable = false, bool isPositive = false, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.NUMERICAL, Length = length, Accuracy = accuracy, IsPositive = isPositive };
            node.IsNullable = isNullable;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyTime(string name, bool isNullable = false, EnumTimeAccuracyType accuracy = EnumTimeAccuracyType.SECOND_TIME_ACC, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.TIME, AccuracyForTime = accuracy };
            node.IsNullable = isNullable;
            this.NodeAddNewSubNode(node);
            return node;
        }
//        public Property AddPropertyTimeZ(string name, bool isNullable = false, EnumTimeAccuracyType accuracy = EnumTimeAccuracyType.SECOND_TIME_ACC, string? guid = null)
//        {
//            var node = new Property(this) { Name = name };
//#if DEBUG
//            if (guid != null) // for test model generation
//            {
//                if (this.Cfg.DicNodes.ContainsKey(guid))
//                    return node;
//                node.Guid = guid;
//            }
//#endif
//            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.TIMEZ, AccuracyForTime = accuracy };
//            node.IsNullable = isNullable;
//            this.NodeAddNewSubNode(node);
//            return node;
//        }
        public Property AddPropertyDate(string name, bool isNullable = false, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.DATE };
            node.IsNullable = isNullable;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyDateTimeUtc(string name, bool isNullable = false, EnumTimeAccuracyType accuracy = EnumTimeAccuracyType.SECOND_TIME_ACC, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.DATETIMEUTC, AccuracyForTime = accuracy };
            node.IsNullable = isNullable;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyDateTimeLocal(string name, bool isNullable = false, EnumTimeAccuracyType accuracy = EnumTimeAccuracyType.SECOND_TIME_ACC, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.DATETIMELOCAL, AccuracyForTime = accuracy };
            node.IsNullable = isNullable;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyDateTimeZ(string name, bool isNullable = false, EnumTimeAccuracyType accuracy = EnumTimeAccuracyType.SECOND_TIME_ACC, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.DATETIMEZ, AccuracyForTime = accuracy };
            node.IsNullable = isNullable;
            node.AccuracyForTime = accuracy;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyDateTimeOffset(string name, bool isNullable = false, EnumTimeAccuracyType accuracy = EnumTimeAccuracyType.SECOND_TIME_ACC, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.DATETIMEOFFSET, AccuracyForTime = accuracy };
            node.IsNullable = isNullable;
            node.AccuracyForTime = accuracy;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyTimeSpan(string name, bool isNullable = false, EnumTimespanBoundaryType accuracy = EnumTimespanBoundaryType.NOT_SELECTED_BNDR_ACC,
            EnumTimespanBoundaryType maxValue = EnumTimespanBoundaryType.NOT_SELECTED_BNDR_ACC, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.TIMESPAN };
            node.IsNullable = isNullable;
            node.TimespanAccuracy = accuracy;
            node.TimespanMaxValue = maxValue;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyTimeSpanTimeonly(string name, bool isNullable = false, EnumTimeAccuracyType accuracy = EnumTimeAccuracyType.SECOND_TIME_ACC, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.TIMESPAN_TIME_ONLY };
            node.IsNullable = isNullable;
            node.AccuracyForTime = accuracy;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyEnumeration(string name, Enumeration en, bool isNullable, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node);
            node.DataType.ObjectRef0.ForeignObjectGuid = en.Guid;
            node.DataType.DataTypeEnum = EnumDataType.ENUMERATION;
            node.IsNullable = isNullable;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyCatalog(string name, Catalog cat, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node);
            node.IsNullable = true;
            node.DataType.ObjectRef0.ForeignObjectGuid = cat.Guid;
            node.DataType.DataTypeEnum = EnumDataType.CATALOG;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyCatalogs(string name, Catalog cat, Catalog? cat2 = null, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node);
            node.IsNullable = true;
            node.DataType.ObjectRef0.ForeignObjectGuid = cat.Guid;
            if (cat2 != null)
                node.DataType.ListObjectRefs.Add(new ComplexRef(node.Guid, cat2.Guid));
            node.DataType.DataTypeEnum = EnumDataType.CATALOGS;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyDocument(string name, Document d, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node);
            node.DataType.ObjectRef0.ForeignObjectGuid = d.Guid;
            node.DataType.DataTypeEnum = EnumDataType.DOCUMENT;
            node.IsNullable = true;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyDocuments(string name, Document d, Document? d2 = null, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node);
            node.DataType.ObjectRef0.ForeignObjectGuid = d.Guid;
            if (d2 != null)
                node.DataType.ListObjectRefs.Add(new ComplexRef(node.Guid, d2.Guid));
            node.DataType.DataTypeEnum = EnumDataType.DOCUMENTS;
            node.IsNullable = true;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyAnyCatalogOrDocument(string name, string? guid = null)
        {
            var node = new Property(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node);
            node.DataType.DataTypeEnum = EnumDataType.ANY;
            node.IsNullable = true;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public uint GetNextPosition()
        {
            // For reserved positions see IProperty static members
            if (this.LastGenPosition == 0)
            {
                this.LastGenPosition = IProperty.PropertyStartingPosition;
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
                this.GetUniqueName(Defaults.PropertyName, node, this.ListProperties);
            }

            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                nameof(this.Description),
                nameof(this.Guid),
                nameof(this.NameUi),
                nameof(this.Parent),
                nameof(this.Children)
            };
            return lst.ToArray();
        }
        public bool GetIsGridSortable()
        {
            if (this.IsGridSortable == EnumUseType.Yes)
                return true;
            if (this.IsGridSortable == EnumUseType.No)
                return false;
            if (this.Parent is Detail dd)
                return dd.IsGridSortableGet();
            else if (this.Parent is Catalog c)
                return c.IsGridSortableGet();
            else if (this.Parent is Document d)
                return d.IsGridSortableGet();
            else if (this.Parent is CatalogFolder cf)
                return cf.IsGridSortableGet();
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
                return dd.IsGridFilterableGet();
            else if (this.Parent is Catalog c)
                return c.IsGridFilterableGet();
            else if (this.Parent is Document d)
                return d.IsGridFilterableGet();
            else if (this.Parent is CatalogFolder cf)
                return cf.IsGridFilterableGet();
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
                return dd.IsGridSortableCustomGet();
            else if (this.Parent is Catalog c)
                return c.IsGridSortableCustomGet();
            else if (this.Parent is Document d)
                return d.IsGridSortableCustomGet();
            else if (this.Parent is CatalogFolder cf)
                return cf.IsGridSortableCustomGet();
            else if (this.Parent is GroupDocuments gd)
                return gd.IsGridSortableCustomGet();
            else
                throw new NotImplementedException();
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
            return this.dicPropertyAccess[role.Guid];
        }
        public void SetRoleAccess(IRole role, EnumPropertyAccess? edit, EnumPrintAccess? print)
        {
            Debug.Assert(role != null);
            Debug.Assert(this.dicPropertyAccess.ContainsKey(role.Guid));
            if (edit.HasValue)
                this.dicPropertyAccess[role.Guid].EditAccess = edit.Value;
            if (print.HasValue)
                this.dicPropertyAccess[role.Guid].PrintAccess = print.Value;
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
            if (this.Parent is Detail dd)
                return dd.GetRolePropertyAccess(role);
            else if (this.Parent is Catalog c)
                return c.GetRolePropertyAccess(role);
            else if (this.Parent is Document d)
                return d.GetRolePropertyAccess(role);
            else if (this.Parent is CatalogFolder cf)
                return cf.GetRolePropertyAccess(role);
            else if (this.Parent is GroupDocuments gd)
                return gd.GetRolePropertyAccess(role);
            else
                throw new NotImplementedException();
        }
        public EnumPrintAccess GetRolePropertyPrint(IRole role)
        {
            if (this.dicPropertyAccess.TryGetValue(role.Guid, out var r) && r.PrintAccess != EnumPrintAccess.PR_BY_PARENT)
                return r.PrintAccess;
            if (this.Parent is Detail dd)
                return dd.GetRolePropertyPrint(role);
            else if (this.Parent is Catalog c)
                return c.GetRolePropertyPrint(role);
            else if (this.Parent is Document d)
                return d.GetRolePropertyPrint(role);
            else if (this.Parent is CatalogFolder cf)
                return cf.GetRolePropertyPrint(role);
            else if (this.Parent is GroupDocuments gd)
                return gd.GetRolePropertyPrint(role);
            else
                throw new NotImplementedException();
        }
        #endregion Roles
    }
}
