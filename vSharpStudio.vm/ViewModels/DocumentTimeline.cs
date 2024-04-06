using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CommunityToolkit.Diagnostics;
using FluentValidation;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class DocumentTimeline : IListProperties, ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup, IRoleGlobalSetting, IRoleAccess
    {
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{ListProperties.Count}";
        }
        [Browsable(false)]
        public bool IsNew { get { return false; } }
        [Browsable(false)]
        public GroupDocuments ParentGroupDocuments { get { Debug.Assert(this.Parent != null); return (GroupDocuments)this.Parent; } }
        [Browsable(false)]
        public IGroupDocuments ParentGroupDocumentsI { get { Debug.Assert(this.Parent != null); return (IGroupDocuments)this.Parent; } }

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

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupDocuments.ParentModel.Children;
        }
        #endregion ITree

        #region Tree operations
        public bool CanAddSubNode() { return true; }
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
            this.ListProperties.Add(node);
            //this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.PropertyName, node, this.ListProperties);
            }
            var model = this.ParentGroupDocuments.ParentModel;
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        public new ConfigNodesCollection<Property> Children { get { return this.ListProperties; } }

        partial void OnCreated()
        {
            this._Name = Defaults.DocumentsTimelineName;
            this._PropertyTimelineDocDateTimeGuid = System.Guid.NewGuid().ToString();
            this._TimeLineDocDateTimePropertyName = "DocDateTime";
            this.IsEditable = false;
            this._ShortIdTypeForCacheKey = "tl";
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }

        private void Init()
        {
            //if (this.Parent is Catalog)
            //{
            //    this.NameUi = "Sub Catalogs";
            //}
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
        }
        public int IndexOf(IProperty p)
        {
            return this.ListProperties.IndexOf((p as Property)!);
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
            // 8  Document data and time
            // 9  Document type ID
            // 10 IsPosted
            if (this.LastGenPosition == 0)
            {
                this.LastGenPosition = 15;
            }
            this.LastGenPosition++;
            return this.LastGenPosition;
        }
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
            //if (!this.UseCodeProperty)
            //    lst.Add(nameof(this.PropertyCodeName));
            //if (!this.UseNameProperty)
            //    lst.Add(nameof(this.PropertyNameName));
            return lst.ToArray();
        }
        public Property AddProperty()
        {
            var node = new Property(this);
            this.NodeAddNewSubNode(node);
            return node;
        }
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
        public Property AddProperty(string name, DataType dataType, string? guid = null)
        {
            var node = new Property(this) { Name = name, DataType = dataType };
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
        public Property AddPropertyCatalog(string name, string catGuid, bool isNullable = false, bool isCsNullable = true, string? guidProperty = null)
        {
            var node = new Property(this) { Name = name, IsNullable = isNullable, IsCsNullable = isCsNullable };
#if DEBUG
            if (guidProperty != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guidProperty))
                    return node;
                node.Guid = guidProperty;
            }
#endif
            node.DataType = new DataType(node)
            {
                DataTypeEnum = EnumDataType.CATALOG,
                IsNullable = isNullable,
                ObjectRef = new ComplexRef(node.Guid, catGuid)
            };
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
            node.DataType.ObjectRef.ForeignObjectGuid = cat.Guid;
            node.DataType.DataTypeEnum = EnumDataType.CATALOG;
            node.IsNullable = true;
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
            node.DataType.ListObjectRefs.Add(new ComplexRef(node.Guid, cat.Guid));
            if (cat2 != null)
                node.DataType.ListObjectRefs.Add(new ComplexRef(node.Guid, cat2.Guid));
            node.DataType.DataTypeEnum = EnumDataType.CATALOGS;
            node.IsNullable = true;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyString(string name, uint length, string? guid = null)
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
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyNumerical(string name, uint length, uint accuracy, string? guid = null)
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
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.NUMERICAL, Length = length, Accuracy = accuracy };
            this.NodeAddNewSubNode(node);
            return node;
        }

        /// <summary>
        /// Only shared properties
        /// </summary>
        /// <param name="guidAppPrjGen"></param>
        /// <returns></returns>
        public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isOptimistic, bool isExcludeSpecial)
        {
            var lst = new List<IProperty>();
            var m = this.ParentGroupDocuments.ParentModel;

            // Field PK
            var p = m.GetPropertyPkId(this, this.Guid);
            (p as Property).Position = 6;
            lst.Add(p);

            // Field record version
            if (isOptimistic)
            {
                p = m.GetPropertyVersion(this, this.ParentGroupDocuments.ParentModel.PropertyVersionGuid);
                (p as Property).Position = 7;
                lst.Add(p);
            }
            // Field document date and time value
            p = m.GetPropertyDateTimeUtc(this, this.PropertyTimelineDocDateTimeGuid, this.TimeLineDocDateTimePropertyName, 1, false, this.TimeLineTimeAccuracy);
            (p as Property).Position = 8;
            lst.Add(p);
            p = m.GetPropertyInt(this, m.PropertyDocShortTypeIdGuid, this.ParentGroupDocuments.DocShortTypeIdPropertyName, false, false);
            (p as Property).Position = 9;
            lst.Add(p);
            p = m.GetPropertyBool(this, m.PropertyDocIsPostedGuid, "IsPosted", (uint)lst.Count, true);
            (p as Property).Position = 10;
            lst.Add(p);

            // shared properties
            foreach (var t in this.ListProperties)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    t.IsDocShared = true;
                    lst.Add(t);
                }
            }
            return lst;
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
