using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using ViewModelBase;
using vSharpStudio.common;
using vSharpStudio.common.DiffModel;
using vSharpStudio.wpf.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace vSharpStudio.vm.ViewModels
{
    [DebuggerDisplay("{ToDebugString(),nq}")]
    public partial class GroupListConstants : ITreeModel, ICanAddSubNode, ICanGoLeft, ICanGoRight, ICanAddNode, INodeGenSettings, 
        IEditableNodeGroup, IEditableNode
    {
        public override string NameShortId { get { return $"gc{this.ShortId}"; } }
        partial void OnDebugStringExtend(ref string mes)
        {
            mes = mes + $" Count:{ListConstants.Count}";
        }
        [Browsable(false)]
        public GroupConstantGroups ParentGroupConstantGroups { get { Debug.Assert(this.Parent != null); return (GroupConstantGroups)this.Parent; } }
        [Browsable(false)]
        public IGroupConstantGroups ParentGroupConstantGroupsI { get { Debug.Assert(this.Parent != null); return (IGroupConstantGroups)this.Parent; } }

        partial void OnCreated()
        {
            this._Name = Defaults.ConstantsGroupName;
            this.IsEditable = true;
            Init();
        }
        protected override void OnInitFromDto()
        {
            Init();
        }
        partial void OnSortTypeChanged() { this.ListConstants.Sort(this.SortType); }
        private void Init()
        {
            OnSortTypeChanged();
            this.ListConstants.OnAddingAction = (t) =>
            {
                t.IsNew = true;
            };
            this.ListConstants.OnAddedAction = (t) =>
            {
                t.OnAdded();
            };
            this.ListConstants.OnRemovedAction = (t) =>
            {
                this.OnRemoveChild();
            };
            this.ListConstants.OnClearedAction = () =>
            {
                this.OnRemoveChild();
            };
        }
        protected override ConfigNodesCollection<GroupListConstants>? GetParentCollection() { return this.ParentGroupConstantGroups.ListConstantGroups; }
        //public SortedObservableCollection<AppProject> GetCollection()
        //{
        //    return this.ParentAppSolution.ListAppProjects;
        //}
        public int IndexOf(IConstant cnst)
        {
            return this.ListConstants.IndexOf((Constant)cnst);
        }

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
        public void OnAdded()
        {
            this.AddAllAppGenSettingsVmsToNode();
        }

        #region ITree
        public override IChildrenCollection GetListChildren()
        {
            return this.Children;
        }
        public override IChildrenCollection GetListSiblings()
        {
            return this.ParentGroupConstantGroups.Children;
        }
        [Browsable(false)]
        public new ConfigNodesCollection<Constant> Children { get { return this.ListConstants; } }
        #endregion ITree

        #region Tree operations
        public void Remove()
        {
            this.ParentGroupConstantGroups.ListConstantGroups.Remove(this);
        }
        public override ITreeConfigNode NodeAddClone()
        {
            var node = GroupListConstants.Clone(this.ParentGroupConstantGroups, this, true, true);
            node.Parent = this.Parent;
            this.ParentGroupConstantGroups.ListConstantGroups.Add(node, this);
            this._Name = this._Name + "2";
            var model = this.Cfg.Model;
            node.ShortId = ++this.ParentGroupConstantGroups.LastShortId;
            this.SetSelected(node);
            return node;
        }
        public override ITreeConfigNode NodeAddNew()
        {
            var node = new GroupListConstants(this.ParentGroupConstantGroups);
            this.ParentGroupConstantGroups.ListConstantGroups.Add(node, this);
            this.GetUniqueName(Defaults.ConstantsGroupName, node, this.ParentGroupConstantGroups.ListConstantGroups);
            var model = this.ParentGroupConstantGroups.ParentModel;
            node.ShortId = ++this.ParentGroupConstantGroups.LastShortId;
            this.SetSelected(node);
            return node;
        }
        public bool CanAddSubNode() { return true; }
        public Constant AddConstant(string name, string? guid = null)
        {
            Constant node = new Constant(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node);
            this.NodeAddNewSubNode(node);
            return node;
        }
        //public Constant AddConstant(string name, DataType type)
        //{
        //    Constant node = new Constant(this) { Name = name, DataType = type };
        //    this.NodeAddNewSubNode(node);
        //    return node;
        //}
        public Constant AddConstantString(string name, string? guid = null)
        {
            Constant node = new Constant(this) { Name = name };
#if DEBUG
            if (guid != null) // for test model generation
            {
                if (this.Cfg.DicNodes.ContainsKey(guid))
                    return node;
                node.Guid = guid;
            }
#endif
            node.DataType = new DataType(node);
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Constant AddConstantEnumeration(string name, Enumeration en, string? guid = null)
        {
            var node = new Constant(this) { Name = name };
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
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Constant AddConstantTypeRefCatalog(string name, Catalog cat, string? guid = null)
        {
            var node = new Constant(this) { Name = name };
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
        public Constant AddConstantTypeRefDocument(string name, Document d, string? guid = null)
        {
            var node = new Constant(this) { Name = name };
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
            node.DataType.ObjectRef0.ForeignObjectGuid = d.Guid;
            node.DataType.DataTypeEnum = EnumDataType.DOCUMENT;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Constant AddConstantRefAnyCatalogOrDocument(string name, string? guid = null)
        {
            var node = new Constant(this) { Name = name };
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
            node.DataType.DataTypeEnum = EnumDataType.ANY;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Constant AddConstantRefAnyCatalogOrDocument(string name, Catalog cat, string? guid = null)
        {
            var node = new Constant(this) { Name = name };
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
            node.DataType.DataTypeEnum = EnumDataType.ANY;
            node.DataType.ObjectRef0.ForeignObjectGuid = cat.Guid;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Constant AddConstantRefAnyCatalogOrDocument(string name, Document d, string? guid = null)
        {
            var node = new Constant(this) { Name = name };
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
            node.DataType.DataTypeEnum = EnumDataType.ANY;
            node.DataType.ObjectRef0.ForeignObjectGuid = d.Guid;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public Constant AddConstantTypeRefCatalogs(string name, Catalog cat, Catalog? cat2 = null, string? guid = null)
        {
            var node = new Constant(this) { Name = name };
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
        public Constant AddConstantTypeRefDocuments(string name, Document d, Document? d2 = null, string? guid = null)
        {
            var node = new Constant(this) { Name = name };
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
            Debug.Assert(node.DataType.ObjectRef.ForeignObjectGuid == string.Empty);
            node.DataType.ObjectRef0.ForeignObjectGuid = d.Guid;
            if (d2 != null)
                node.DataType.ListObjectRefs.Add(new ComplexRef(node.Guid, d2.Guid));
            node.DataType.DataTypeEnum = EnumDataType.DOCUMENTS;
            this.NodeAddNewSubNode(node);
            return node;
        }
        public uint GetNextPosition()
        {
            this.LastGenPosition++;
            return this.LastGenPosition;
        }
        public override ITreeConfigNode NodeAddNewSubNode(ITreeConfigNode? node_impl = null)
        {
            Constant node = null!;
            if (node_impl == null)
            {
                node = new Constant(this);
            }
            else
            {
                node = (Constant)node_impl;
            }
            this.Add(node);
            node.DataType.Parent = node;
            node.Position = this.GetNextPosition();
            if (node_impl == null)
            {
                this.GetUniqueName(Defaults.ConstantName, node, this.ListConstants);
            }
            var model = this.ParentGroupConstantGroups.ParentModel;
            node.ShortId = ++this.LastShortId;
            node.ShortRefId = model.LastTypeShortRefIdForNode(node, node.ShortId);
            this.SetSelected(node);
            return node;
        }
        #endregion Tree operations

        public void GetSpecialProperties(List<IProperty> res, bool isOptimistic)
        {
            var model = this.Cfg.Model;
            var prp = model.GetPropertySpecial(this, EnumSpecialPropertyType.RECORD_ID);
            res.Add(prp);
            if (isOptimistic)
            {
                prp = model.GetPropertyVersion(this);
                res.Add(prp);
            }
        }
        public IReadOnlyList<IProperty> GetIncludedConstantsAsProperties(string guidAppPrjGen, bool isOptimistic, bool isExcludeSpecial = false)
        {
            var res = new List<IProperty>();
            this.GetSpecialProperties(res, isOptimistic);
            VmBindable.IsNotValidateAll = true;
            foreach (var t in this.ListConstants)
            {
                if (t.IsIncluded(guidAppPrjGen))
                {
                    var p = new Property(this, t.Guid, t.Name, false) { DataType = t.DataType, IsCsNullable = true, IsNullable = true };
                    switch (t.DataType.DataTypeEnum)
                    {
                        case EnumDataType.CATALOG:
                        case EnumDataType.DOCUMENT:
                            if (string.IsNullOrWhiteSpace(t.RefComplexObjectDescrPropertyGuid))
                                t.RefComplexObjectDescrPropertyGuid = System.Guid.NewGuid().ToString();
                            p.RefComplexObjectDescrPropertyGuid = t.RefComplexObjectDescrPropertyGuid;
                            p.PositionOfDescr = t.PositionOfDescr;
                            break;
                        case EnumDataType.CATALOGS:
                        case EnumDataType.DOCUMENTS:
                        case EnumDataType.ANY:
                            if (string.IsNullOrWhiteSpace(t.RefComplexObjectDescrPropertyGuid))
                                t.RefComplexObjectDescrPropertyGuid = System.Guid.NewGuid().ToString();
                            p.RefComplexObjectDescrPropertyGuid = t.RefComplexObjectDescrPropertyGuid;
                            p.PositionOfDescr = t.PositionOfDescr;
                            if (string.IsNullOrWhiteSpace(t.RefComplexObjectGdPropertyGuid))
                                t.RefComplexObjectGdPropertyGuid = System.Guid.NewGuid().ToString();
                            p.RefComplexObjectGdPropertyGuid = t.RefComplexObjectGdPropertyGuid;
                            p.PositionOfGd = t.PositionOfGd;
                            break;
                        default:
                            break;
                    }
                    p.Guid = t.Guid;
                    p.Position = t.Position;
                    p.ShortId = t.ShortId;
                    res.Add(p);
                }
            }
            VmBindable.IsNotValidateAll = false;
            return res;
        }
        protected override string[]? OnGetWhatHideOnPropertyGrid()
        {
            var lst = new List<string>
            {
                //lst.Add(nameof(this.Description));
                nameof(this.Guid),
                //lst.Add(nameof(this.NameUi));
                nameof(this.Parent),
                nameof(this.Children)
            };
            return [.. lst];
        }

        #region Roles
        public IRoleConstantsSettings GetRoleSettings(IRole role)
        {
            var roles = this.Cfg.Model.GroupCommon.GroupRoles;
            var nodeRoleDic = roles.DicRoles[role.Guid];
            nodeRoleDic.DicNodeRules.TryGetValue(this.Guid, out var roleFromNode);
            var res = new RoleConstantsSettings(this);
            res.CanEdit = roleFromNode?.ConstantSettings.CanEdit ?? roles.DefaultConstantsRoleSettings.CanEdit;
            res.CanPrint = roleFromNode?.ConstantSettings.CanPrint ?? roles.DefaultConstantsRoleSettings.CanPrint;
            res.CanView = roleFromNode?.ConstantSettings.CanView ?? roles.DefaultConstantsRoleSettings.CanView;
            return res;
        }
        #endregion Roles
    }
}
