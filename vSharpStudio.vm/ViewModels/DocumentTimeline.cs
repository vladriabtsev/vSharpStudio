using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Polly.Caching;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class DocumentTimeline : ITreeModel, ICanAddSubNode, ICanGoRight, INodeGenSettings, IEditableNodeGroup
    {
        public override string NameShortId { get { return "tm"; } }
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
                return $"{this.ParentGroupDocuments.GroupListDocuments.PrefixForCompositionNames}{this._Name}";
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
            node.Position = this.GetNextFreePosition();
            //this.Add(node);
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.PropertyName, node, this.ListProperties);
            }
            var model = this.ParentGroupDocuments.ParentModel;
            node.ShortId = ++this.LastShortId;
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        partial void OnCreated()
        {
            this._TimeLineDocDateTimePropertyName = "DocDateTime";
            this._LastPosition = IProperty.PositionReservation;
            this.IsEditable = false;

            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }

        private void Init()
        {
            this._Name = Defaults.DocumentsTimelineName;
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
            };
            this.ListProperties.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListProperties.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
            if (this.Children.Count > 0)
                return;
            var children = (ConfigNodesCollection<ITreeConfigNodeSortable>)this.Children;
            children.Add(this.GroupProperties, 1);
            this.GetSpecialProperties(new List<IProperty>(), true); // position ang guids for special properties
        }
        public int IndexOf(IProperty p)
        {
            return this.ListProperties.IndexOf((p as Property)!);
        }
        //public uint GetNextPosition()
        //{
        //    // Reserved positions
        //    // 1  not used
        //    // 2  not used
        //    // 3  not used
        //    // 4  __is_need_insert
        //    // 5  __is_need_update
        //    // 6  PropertyId
        //    // 7  PropertyObjectVersion
        //    // 8  Document data and time
        //    // 9  Document type ID
        //    // 10 IsPosted
        //    if (this.LastGenPosition == 0)
        //    {
        //        this.LastGenPosition = 15;
        //    }
        //    this.LastGenPosition++;
        //    return this.LastGenPosition;
        //}
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
            return [.. lst];
        }

        #region Get Properties and Details
        public uint GetNextFreePosition() { return ++this.LastPosition; }
        //public Property AddProperty()
        //{
        //    var node = new Property(this);
        //    this.NodeAddNewSubNode(node);
        //    node.ShortId = ++this.GroupProperties.LastShortId;
        //    return node;
        //}
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
                IsNullable = isNullable,
                DataTypeEnum = EnumDataType.CATALOG,
            };
            node.ConfigObjectGuid = catGuid;
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
            {
                node.DataType.ListObjectRefs.Add(new ComplexRef(node.Guid, cat2.Guid));
            }
            node.DataType.DataTypeEnum = EnumDataType.CATALOGS;
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
        public Property AddPropertyStringFixed(string name, uint length, string? guid = null)
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
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.STRING_FIXED, Length = length };
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Property AddPropertyUlid(string name, string? guid = null)
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
            node.DataType = new DataType(node) { DataTypeEnum = EnumDataType.ULID };
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
        public IProperty? GetDateTimeUtcProperty(bool? isRegisterBalance = null)
        {
            return null;
        }
        public IReadOnlyList<IProperty> GetListIdPKeyProperties(bool? isRegisterBalance = null)
        {
            Debug.Assert(isRegisterBalance == null);
            var res = new List<IProperty>();
            var model = this.Cfg.Model;
            var prp = model.GetPropertySpecial(this, EnumSpecialPropertyType.RECORD_ID);
            res.Add(prp);
            return res;
        }
        public void GetSpecialProperties(List<IProperty> lst, bool isOptimistic)
        {
            var model = this.Cfg.Model;
            // Field PK
            var prp = model.GetPropertySpecial(this, EnumSpecialPropertyType.RECORD_ID);
            lst.Add(prp);

            // Field document date and time value
            prp = model.GetPropertyDocumentDate(this);
            //prp = model.GetPropertyDateTimeUtc(this, this.PropertyTimelineDocDateTimeGuid, this.TimeLineDocDateTimePropertyName, 1, true, this.TimelineTimeAccuracy);
            //prp.SetPosition(IProperty.PropertyDocumentDatePosition);
            lst.Add(prp);
            prp = model.GetPropertyTimelineShortTypeId(this, false);
            //prp = model.GetPropertyInt(this, model.PropertyDocShortTypeIdGuid, this.ParentGroupDocuments.GroupListDocuments.PropertyDocShortTypeIdName, IProperty.PropertyShortTypeIdPosition, false, false);
            lst.Add(prp);
            prp = model.GetPropertyTimelineIsPosted(this, true);
            //prp = model.GetPropertyBool(this, model.PropertyDocIsPostedGuid, "IsPosted", (uint)lst.Count, true);
            //prp.SetPosition(IProperty.PropertyIsPostedPosition);
            lst.Add(prp);
            // Field record version
            if (isOptimistic)
            {
                prp = model.GetPropertyVersion(this);
                lst.Add(prp);
            }
        }
        /// <summary>
        /// Only shared properties
        /// </summary>
        /// <param name="guidAppPrjGen"></param>
        /// <returns></returns>
        public IReadOnlyList<IProperty> GetIncludedProperties(string guidAppPrjGen, bool isOptimistic, bool isExcludeSpecial)
        {
            var lst = new List<IProperty>();

            if (!isExcludeSpecial)
                this.GetSpecialProperties(lst, isOptimistic);
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
        #endregion Get Properties and Details

        public bool GetIsGridSortable()
        {
            if (this.IsGridSortable == EnumUseType.Yes)
                return true;
            if (this.IsGridSortable == EnumUseType.No)
                return false;
            if (this.Parent is Detail dd)
                return dd.IsGridSortableGet();
            //else if (this.Parent is Catalog c)
            //    return c.IsGridSortableGet();
            else if (this.Parent is Document d)
                return d.IsGridSortableGet();
            //else if (this.Parent is CatalogFolder cf)
            //    return cf.IsGridSortableGet();
            else if (this.Parent is GroupListDocuments gd)
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
            //else if (this.Parent is Catalog c)
            //    return c.IsGridFilterableGet();
            else if (this.Parent is Document d)
                return d.IsGridFilterableGet();
            //else if (this.Parent is CatalogFolder cf)
            //    return cf.IsGridFilterableGet();
            else if (this.Parent is GroupListDocuments gd)
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
            //else if (this.Parent is Catalog c)
            //    return c.IsGridSortableCustomGet();
            else if (this.Parent is Document d)
                return d.IsGridSortableCustomGet();
            //else if (this.Parent is CatalogFolder cf)
            //    return cf.IsGridSortableCustomGet();
            else if (this.Parent is GroupListDocuments gd)
                return gd.IsGridSortableCustomGet();
            else
                throw new NotImplementedException();
        }
        #region Roles
        public IRolePropertiesSettings GetRoleSettings(IRole role)
        {
            var roles = this.Cfg.Model.GroupCommon.GroupRoles;
            var nodeRoleDic = roles.DicRoles[role.Guid];
            nodeRoleDic.DicNodeRules.TryGetValue(this.Guid, out var roleFromNode);
            var res = new RolePropertiesSettings(this);
            res.CanEdit = roleFromNode?.DetailSettings.CanEdit ?? roles.DefaultPropertiesRoleSettings.CanEdit;
            res.CanPrint = roleFromNode?.DetailSettings.CanPrint ?? roles.DefaultPropertiesRoleSettings.CanPrint;
            res.CanView = roleFromNode?.DetailSettings.CanView ?? roles.DefaultPropertiesRoleSettings.CanView;
            return res;
        }
        #endregion Roles
    }
}
